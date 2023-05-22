using ChamaLibrary.Models;
using ChamaLibrary.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dapper.SqlMapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace ChamaLibrary.DataAccess
{
    public class SqliteDataAccess
    {
        //global Variables
        public static int CurUserId;
        public static string CurUserName;
        public static string CurUserRole ;

        public static string CurFinYear;
        public static int CurFinYearId;
        public static DateTime FinYearStart;
        public static DateTime FinYearEnd;

        //Helpers
        private static string LoadConnectionString(string Id = "Default")=>ConfigurationManager.ConnectionStrings[Id].ToString();
        public static string ToPropercase(string text)
        {
            TextInfo myTI = new CultureInfo("en-GB", false).TextInfo;
            return myTI.ToTitleCase(text.ToLower());
        }
        public static bool IsDateWithinFinancialYear(DateTime MyDate)
        {
            if((MyDate>= FinYearStart) && (MyDate <= FinYearEnd) )
            {
                return true;
            }
            return false;
        }

        //votes
        public static int GetVoteId(string VoteName)
        {
            try
            {
                Vote FoundVote = GetVoteByName(VoteName);
                if (FoundVote == null)
                {
                    if (InsertVoteByName(VoteName) > 0)
                    {
                        FoundVote = GetVoteByName(VoteName);
                        if (FoundVote == null) 
                        {
                            MessageBox.Show("Failed to create Vote", "@Chamaz", MessageBoxButtons.OK);
                            return 0;
                        }
                        return FoundVote.Id;
                    }
                }
                return FoundVote.Id;
            }
            catch (Exception)
            {
                throw;
                return 0;
            }
        }
        //private void LoadFineAndFineWithdrawalVote()
        //{
        //    //check default Non Member
        //    int VoteId = SqliteDataAccess.GetFineVote();

        //    if (FineVote == null)
        //    {
        //        if (SqliteDataAccess.InsertFineVote() > 0)
        //        {
        //            FineVote = SqliteDataAccess.GetFineVote();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to create Fine Vote", "@Chamaz", MessageBoxButtons.OK);
        //        }
        //    } //check default Non Member
        //    FineWithdrawal = SqliteDataAccess.GetFineWithdrawalVote();

        //    if (FineWithdrawal == null)
        //    {
        //        if (SqliteDataAccess.InsertDefaultNonMember() > 0)
        //        {
        //            FineWithdrawal = SqliteDataAccess.GetFineWithdrawalVote();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to create FineWithdrawal Vote", "@Chamaz", MessageBoxButtons.OK);
        //        }
        //    }

        //}

        //public static Vote GetFineVote()
        //{
        //    try
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //        {
        //            var result = cnn.QueryFirstOrDefault<Vote>("Select * From Votes where VoteName='Fine'");
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show(" Error getting Fine vote", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}FineWithdrawal Fine
        public static int InsertVoteByName(string VoteName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into votes(VoteName,VoteAbbrev) values(@VoteName,@VoteName)", new { VoteName });
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //public static Vote GetFineWithdrawalVote()
        //{
        //    try
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //        {
        //            var result = cnn.QueryFirstOrDefault<Vote>("Select * From Votes where VoteName='FineWithdrawal'");
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show(" Error getting Fine FineWithdrawal", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}
        public static int InsertFineWithdrawalVote()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into votes(VoteName,VoteAbbrev) values('FineWithdrawal','FineW')");
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static List<Vote> GetALLVotes()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<Vote>("Select * From Votes", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting vote(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            } 
        }
        public static bool CheckIfVoteExist(string VoteName, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                { 
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Votes WHERE VoteName=@VoteName and Id<>@Id", new { VoteName,Id });
                    if (Found > 0 ) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related vote", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfVoteIsReferenced(int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from CashbookVotes WHERE VoteId=@Id", new { Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show("Error checking if Vote is in use", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static Vote GetVoteByName(string VoteName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<Vote>("Select * From Votes where VoteName=@VoteName", new { VoteName = ToPropercase(VoteName) });
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error checking related vote", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
           
            }
        }
        public static Vote GetVoteById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<Vote>("Select * From Votes where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting vote by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertVote(Vote vote)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                   var result= cnn.Execute("insert into votes(VoteName,VoteAbbrev) values(@VoteName,@VoteAbbrev)", vote);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }    
        public static int UpdateVote(Vote vote)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update votes set VoteName=@VoteName,VoteAbbrev=@VoteAbbrev where Id=@Id", vote);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteVote(int Id)
        {
            try
            {              
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from votes where Id=@Id", new { Id = Id });
                    return result;
                }
                   
            }
            catch (Exception)
            {
                return 0;   
            }
        }


        //Banks

        public static List<Bank> GetALLBanks()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<Bank>("Select * From Banks", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting Bank(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static bool CheckIfBankExist(string BankName, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Banks WHERE BankName=@BankName and Id<>@Id", new { BankName = ToPropercase(BankName), Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Bank", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfBankIsReferenced(int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Accounts WHERE BankId=@Id", new { Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if Bank has been referenced on another table", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static Bank GetBankByName(string BankName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<Bank>("Select * From Banks where BankName=@BankName", new { BankName = ToPropercase(BankName) });
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error checking related Bank", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
        }
        public static Bank GetBankById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<Bank>("Select * From Votes where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting Bank by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertBank(Bank Bank)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Banks(BankName) values(@BankName)", Bank);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateBank(Bank Bank)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Banks set BankName=@BankName where Id=@Id", Bank);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteBank(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from Banks where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }


        //FinYearModels

        public static bool SetFinYearVariables(int Id)
        {
            try
            {
                var result = GetFinYearById(Id);
                if (result != null)
                {
                    CurFinYear = result.YearName;
                    FinYearStart = result.StartDate;
                    FinYearEnd = result.EndDate;
                    return true;
                }
                else
                {
                    return false;   
                }
   
            }
            catch (Exception)
            {
                MessageBox.Show(" Error setting FinYear Variables", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static List<FinYear> GetALLFinYears()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<FinYear>("Select * From FinYears", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting FinYear(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static bool CheckIfFinYearExist(string YearName, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from FinYears WHERE YearName=@YearName and Id<>@Id", new { YearName = ToPropercase(YearName), Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related FinYear", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfFinYearIsReferenced(DateTime StartDate,DateTime EndDate)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Cashbooks WHERE TransDate BETWEEN @StartDate AND @EndDate", new { StartDate, EndDate });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if FinYear has been referenced on another table", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;

            //ToDo verify if year is referenced
        }
        public static FinYear GetFinYearByName(string YearName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<FinYear>("Select * From FinYears where YearName=@YearName", new { YearName});
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error checking related FinYear", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
        }
        public static FinYear GetFinYearById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<FinYear>("Select * From FinYears where Id=@Id", new {Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting FinYear by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertFinYear(FinYear FinYear)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into FinYears(YearName,StartDate,EndDate) values(@YearName,@StartDate,@EndDate)", FinYear);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateFinYear(FinYear FinYear)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update FinYears set YearName=@YearName,StartDate=@StartDate,EndDate=@EndDate where Id=@Id", FinYear);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteFinYear(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from FinYears where Id=@Id", new {Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }


        //Accounts

        public static List<AccountVM> GetALLAccounts()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<AccountVM>("select a.*,b.BankName From ((Select * From Accounts) a left join (Select * from Banks) b on a.BankId=b.Id);", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting Account(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static bool CheckIfAccountExist(string AccountName, int BankId)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Accounts WHERE AccountName=@AccountName and BankId<>@BankId", new { BankName = ToPropercase(AccountName), BankId = BankId });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Account", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfAccountIsReferenced(int AccountId)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from CashBooks WHERE AccountId=@AccountId", new { AccountId });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if account has been referenced", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static AccountVM GetAccountById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<AccountVM>("select * From ((Select * From Accounts where Id=@Id) a left join (Select BankName from Banks) b on a.BankId=b.Id)", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting Account by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertAccount(AccountVM Account)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Accounts(AccountName,BankId,Branch,AccountNumber) values(@AccountName,@BankId,@Branch,@AccountNumber)", Account);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateAccount(AccountVM Account)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Accounts set AccountName=@AccountName,BankId=@BankId,Branch=@Branch,AccountNumber=@AccountNumber where Id=@Id", Account);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteAccount(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from Accounts where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Users

        public static bool VerifyUserLogin(string UserName, string Password,int FinYearId)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                   var FoundUser = cnn.QueryFirstOrDefault<User>("select * from Users WHERE UserName=@UserName AND PassWord=@PassWord", new { UserName, Password });
                    if (FoundUser != null)
                    {
                        CurUserId = FoundUser.Id; 
                        CurUserName= FoundUser.UserName;
                        CurUserRole = GetUserRoleById(FoundUser.UserRoleId).Role;

                        CurFinYearId = FinYearId;
                        if (!SetFinYearVariables(FinYearId))
                        {
                            throw new Exception();
                        }
                        return true;
                    }

                }
                catch (Exception)
                { 
                    MessageBox.Show(" Error during user login verification", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return false;
        }
        public static List<User> GetALLUsers()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<User>("select * From Users;", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting User(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static bool CheckIfUserExist(string UserName)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Users WHERE UserName=@UserName", new { UserName });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related User", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfUserIsReferenced(int UserId)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Cashbooks WHERE UserId=@UserId", new { UserId });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if User has been referenced on another table", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static User GetUserById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<User>("select * From Users where Id=@Id", new { Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting User by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertUser(User User)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Users(FirstName,LastName,UserName,PassWord,Telephone,UserRoleId) values(@FirstName,@LastName,@UserName,@PassWord,@Telephone,@UserRoleId)", User);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateUser(User User)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Users set FirstName=@FirstName,LastName=@LastName,UserName=@UserName,PassWord=@PassWord,Telephone=@Telephone,UserRoleId=@UserRoleId where Id=@Id", User);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteUser(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from Users where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }


        //UserRoles

        public static List<UserRole> GetALLUserRoles()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<UserRole>("select * From UserRoles;", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting UserRole(s)", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static bool CheckIfUserRoleExist(string Role)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from UserRoles WHERE Role=@Role", new { Role });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related User Role", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfUserRoleIsReferenced(int UserRoleId)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Users WHERE UserRoleId=@UserRoleId", new { UserRoleId });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if User Role has been referenced by user", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static UserRole GetUserRoleById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<UserRole>("select * From UserRoles where Id=@Id", new { Id });
                    return result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Error getting User Role by id", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertUserRole(UserRole Role)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into UserRoles(Role) values(@Role)", Role);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateUserRole(UserRole UserRole)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update UserRoles set Role=@Role where Id=@Id", UserRole);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteUserRole(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from UserRoles where Id=@Id", new {Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }


        //Members

        public static List<Member> GetALLMembers(int IsMember)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<Member>("Select * From Members where IsMember=@IsMember", new { IsMember });
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static bool CheckIfMemberExist(string MemberName, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Members WHERE MemberName=@MemberName  and Id<>@Id", new { MemberName = ToPropercase(MemberName), Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Member", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfIdNumberExist(Int64 IdentityNo, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Members WHERE IdentityNo=@IdNumber and Id<>@Id", new { IdNumber = IdentityNo,Id=Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Id Number", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfMemberIsReferenced(int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Foundrct = cnn.ExecuteScalar<int>("select count(*) from Receipts WHERE PayerId=@Id", new { Id });
                    int Foundpay = cnn.ExecuteScalar<int>("select count(*) from Payments WHERE PayeeId=@Id", new { Id });

                    if (Foundrct > 0 || Foundpay>0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if Member has been referenced by existing transactions", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static Member GetMemberByName(string MemberName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<Member>("Select * From Members where MemberName=@MemberName and IsMember=1", new { MemberName = ToPropercase(MemberName) });
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static Member GetMemberById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<Member>("Select * From Members where Id=@Id and IsMember=1", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertMember(Member Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Members(IdentityNo,MemberName,IsActive,IsMember) values(@IdentityNo,@MemberName,@IsActive,@IsMember)", Member);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateMember(Member Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Members set MemberName=@MemberName,IsActive=@IsActive,IsMember=@IsMember where Id=@Id", Member);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteMember(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from Members where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static Member GetOnlyNonMemberById()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<Member>("Select * From Members where IsMember=0", new { });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertDefaultNonMember()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Members(IdentityNo,MemberName,IsActive,IsMember) values(@IdentityNo,@MemberName,@IsActive,@IsMember)", new { IdentityNo=0, MemberName="Non Member", IsActive=1, IsMember =0});
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        
        //NonMembers

        public static List<Member> GetALLNonMembers()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<Member>("Select * From Members where IsMember=0", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static bool CheckIfNonMemberExist(string MemberName, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Members WHERE MemberName=@MemberName and Id<>@Id", new { MemberName = ToPropercase(MemberName), Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Non Member", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfNonMemberIdNumberExist(Int64 IdentityNo, int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Members WHERE IdentityNo=@IdNumber and Id<>@Id", new { IdNumber = IdentityNo, Id = Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related Id Number", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static Member GetNonMemberByName(string MemberName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<Member>("Select * From Members where MemberName=@MemberName and IsMember=0", new { MemberName = ToPropercase(MemberName) });
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static Member GetNonMemberById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<Member>("Select * From Members where Id=@Id and IsMember=0", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertNonMember(Member Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Members(IdentityNo,MemberName,IsActive,IsMember) values(@IdentityNo,@MemberName,@IsActive,@IsMember)", Member);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateNonMember(Member Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Members set MemberName=@MemberName,IsActive=@IsActive,IsMember=@IsMember where Id=@Id", Member);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteNonMember(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from Members where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }
        
        //Loan Types

        public static List<LoanType> GetALLLoanTypes()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<LoanType>("Select * From LoanTypes", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static bool CheckIfLoanTypeExist(string LoanTypeName)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from LoanTypes WHERE LoanTypeName=@LoanTypeName", new { LoanTypeName = ToPropercase(LoanTypeName) });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related LoanType Name", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }   
        public static bool CheckIfLoanTypeIsReferenced(int Id)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Loans WHERE LoanTypeId=@Id", new {Id });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if Loan Type has been referenced by existing loans", "@Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static LoanType GetLoanTypeByName(string LoanTypeName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<LoanType>("Select * From LoanTypes where LoanTypeName=@LoanTypeName", new { LoanTypeName = ToPropercase(LoanTypeName) });
                    if (OutPut == null)
                    {
                        return null;
                    }
                    else
                    {
                        return OutPut;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static LoanType GetLoanTypeById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<LoanType>("Select * From LoanTypes where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertLoanType(LoanType LoanType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into LoanTypes(LoanTypeName) values(@LoanTypeName)", LoanType);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateLoanType(LoanType LoanType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update LoanTypes set LoanTypeName=@LoanTypeName where Id=@Id", LoanType);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteLoanType(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("delete from LoanTypes where Id=@Id", new { Id = Id });
                    return result;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        //CashBook

        public static List<CashBook> GetALLCashBook()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<CashBook>("Select * From CashBooks", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static List<CashBook> GetALLCashBookOtherPayment()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<CashBook>("Select * From CashBooks where TransType='Debit' And TransCategory ='OtherPayments'", new {});
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static List<CashBook> GetALLCashBookByTransType(string TransType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<CashBook>("Select * From CashBooks where TransType=@TransType", new { TransType= TransType });
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static CashBook GetCashBookById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<CashBook>("Select * From CashBooks where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertCashBook(CashBook CashBook, List<CashBookVoteVM> cashBookVotes, IDbConnection cnn)
        {
            try
            {
               
                int CashBookId = cnn.ExecuteScalar<int>("insert into CashBooks(TransDate,Month,AccountId,Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate, UserId) " +
                                    "values(@TransDate,@Month,@AccountId,@Amount,@AmountWords,@PayMode,@PayModeNo,@CreditOrDebit,@Category,@BankDate,@UserId); SELECT last_insert_rowid();", CashBook);
                if (CashBookId < 0) { return 0; }
                if (!(SqliteDataAccess.InsertCashBookVotes(cashBookVotes, cnn, CashBookId) > 0)) { return 0; }

                return CashBookId;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
                return 0;
            }
        }
        public static int UpdateCashBook(CashBook CashBook, List<CashBookVoteVM> cashBookVotes, IDbConnection cnn)
        {
            try
            {

                var Upresult = cnn.Execute("update CashBooks set TransDate=@TransDate,Month=@Month,AccountId=@AccountId," +
                    "Amount=@Amount,AmountWords=@AmountWords,PayMode=@PayMode,PayModeNo=@PayModeNo,CreditOrDebit=@CreditOrDebit," +
                    "Category=@Category,BankDate=@BankDate,UserId=@UserId where Id=@Id", CashBook);
                if (Upresult < 0) { return 0; }
                if (!(UpdateCashBookVotes(cashBookVotes, cnn, CashBook.Id) > 0)) { return 0; }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateCashBookForFine(CashBook CashBook, int ReceiptCsbkId, int PaymentCsbkId, IDbConnection cnn)
        {
            try
            {

                var Upresult = cnn.Execute("update CashBooks set TransDate=@TransDate,Month=@Month,AccountId=@AccountId," +
                    "Amount=@Amount,AmountWords=@AmountWords,BankDate=@BankDate,UserId=@UserId where (Id=@ReceiptCsbkId or Id=@PaymentCsbkId)", new { CashBook, ReceiptCsbkId, PaymentCsbkId });
                if (Upresult < 0) { return 0; }
                
                var Vresult = cnn.Execute("update CashBookVotes set VoteAmount=@VoteAmount where (CashBookId=@ReceiptCsbkId or CashBookId=@PaymentCsbkId)", new { VoteAmount= CashBook.Amount, ReceiptCsbkId, PaymentCsbkId });
                if(Vresult < 0) { return 0; }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int DeleteCashBook(int CashBookId, IDbConnection cnn)
        {
            try
            {
               var Delresult = DeleteCashBookVotes(CashBookId, cnn);
                if (Delresult < 0) { return 0; }

                var result = cnn.Execute("delete from CashBooks where Id=@Id", new { Id = CashBookId });
                if (result < 0) { return 0; }

                 return 1;
                    
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //CashBook Votes

        public static List<CashBookVoteVM> CashBookVotesByCsbkId(int CashBookId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Query<CashBookVoteVM>("Select CashBookVotes.Id as Id,VoteId,VoteName,VoteAbbrev,VoteAmount,CashBookId " +
                                                        "From CashBookVotes,Votes " +
                                                        "where CashBookId=@CashBookId " +
                                                        "and Votes.Id=CashBookVotes.VoteId", new {CashBookId });
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int InsertCashBookVotes(List<CashBookVoteVM> CashBookVotes, IDbConnection cnn,int CashBookId)
        {
            try
            {
                foreach (var CashBookVote in CashBookVotes) 
                {
                    CashBookVote.CashBookId = CashBookId;   
                    var result = cnn.Execute("insert into CashBookVotes(VoteId,VoteAmount,CashBookId) values(@VoteId,@VoteAmount,@CashBookId)", CashBookVote);
                    if (result<1) { throw new Exception(); }
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateCashBookVotes(List<CashBookVoteVM> cashBookVotes, IDbConnection cnn, int CashBookId)
        {
            try
            {
                var Delresult = DeleteCashBookVotes(CashBookId, cnn);
                if (Delresult <1){ throw new Exception();}
                var Inresult = InsertCashBookVotes(cashBookVotes, cnn, CashBookId);
                if (Inresult < 1) { throw new Exception(); }
                
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateCashBookVotes(int CashBookId,decimal VoteAmount, IDbConnection cnn )
        {
            try
            {
                var result = cnn.Execute("update CashBookVotes set VoteAmount=@VoteAmount where CashBookId=@CashBookId", new { CashBookId , VoteAmount });
                if (result < 1) { throw new Exception(); }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static int DeleteCashBookVotes(int CashBookId, IDbConnection cnn)
        {
            try
            {
                var result = cnn.Execute("delete from CashBookVotes where CashBookId=@CashBookId", new { CashBookId });
                    return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        //Receipting

        public static List<ReceiptVM> GetReceiptByPayerId(int PayerId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                     
        var sql = "Select r.Id as Id,ReceiptNo,PayerId,Payer,ReceiptDetails,CashBookId,TransDate,Month,AccountId," +
                        "Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate,UserId " +
                        "From (" +
                            "(Select * From Receipts where PayerId=@PayerId) r " +
                            "Left Join " +
                            "(Select * From CashBooks) c " +
                            "On r.CashBookId=c.Id " +
                            ")";
                    var result = cnn.Query<ReceiptVM>(sql, new {PayerId});
                    return result.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static CashBook GetReceiptById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<CashBook>("Select * From CashBooks where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertReceipt(ReceiptVM receipt, IDbConnection cnn)
        {
            try
            {
                var Result = cnn.Execute("insert into Receipts(ReceiptNo,PayerId,Payer,ReceiptDetails,CashBookId) " +
                                     "values(@ReceiptNo,@PayerId,@Payer,@ReceiptDetails,@CashBookId);SELECT last_insert_rowid();", receipt);
                if (Result < 1) { throw new Exception(); }

                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int InsertReceipt(ReceiptVM receipt, List<CashBookVoteVM> cashBookVotes)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //get cashbook
                            CashBook CashBook = new CashBook
                            {
                                Id = 0,
                                TransDate = receipt.TransDate,
                                Month = receipt.Month,
                                AccountId = receipt.AccountId,
                                Amount = receipt.Amount,
                                AmountWords = receipt.AmountWords,
                                PayMode = receipt.PayMode,
                                PayModeNo = receipt.PayModeNo,
                                CreditOrDebit = receipt.CreditOrDebit, //Credit,Debit
                                Category = receipt.Category, //MemberDeposits,NonMemberDeposits 
                                BankDate = receipt.BankDate,
                                UserId = CurUserId,
                            };

                            //insert on cashbook and cashbookvotes
                            int CsbkId = InsertCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }
                            receipt.CashBookId = CsbkId;

                            //insert on receipt 
                            int Result = cnn.Execute("insert into Receipts(ReceiptNo,PayerId,Payer,ReceiptDetails,CashBookId) " +
                                    "values(@ReceiptNo,@PayerId,@Payer,@ReceiptDetails,@CashBookId);", receipt);
                            if (Result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int UpdateReceipt(ReceiptVM receipt, List<CashBookVoteVM> cashBookVotes)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //get cashbook
                            CashBook CashBook = new CashBook
                            {
                                Id = receipt.CashBookId,
                                TransDate = receipt.TransDate,
                                Month = receipt.Month,
                                AccountId = receipt.AccountId,
                                Amount = receipt.Amount,
                                AmountWords = receipt.AmountWords,
                                PayMode = receipt.PayMode,
                                PayModeNo = receipt.PayModeNo,
                                CreditOrDebit = receipt.CreditOrDebit, //Credit,Debit
                                Category = receipt.Category, //MemberDeposits,NonMemberDeposits 
                                BankDate = receipt.BankDate,
                                UserId = CurUserId,
                            };

                            //update on cashbook and cashbookvotes
                            int CsbkId = UpdateCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }

                            //update on receipt 
                            int Result = cnn.Execute("update Receipts set ReceiptNo=@ReceiptNo,PayerId=@PayerId,Payer=@Payer,ReceiptDetails=@ReceiptDetails " +
                                            "where CashBookId=@CashBookId;", receipt);
                            if (Result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int DeleteReceipt(int CashBookId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //delete on cashbook and cashboovotes
                            var Delresult = DeleteCashBook(CashBookId, cnn);
                            if (Delresult < 1) { throw new Exception(); }

                            //delete from receipts
                            var result = cnn.Execute("delete from receipts where CashBookId=@CashBookId", new { CashBookId = CashBookId });
                            if (result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;

                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int GetNextReceiptNo()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                try
                {
                    int ReceiptNo = 0;
                    ReceiptNo = cnn.ExecuteScalar<int>($"SELECT CASE WHEN MAX(ReceiptNo) IS NULL THEN 1 ELSE MAX(ReceiptNo) + 1 END FROM receipts,cashbooks where receipts.cashbookid=cashbooks.id and cashbooks.transdate BETWEEN @FinYearStart AND @FinYearEnd;", new { FinYearStart,FinYearEnd });
                    return ReceiptNo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in generating receipt number");
                    return 0;
                }
            }

        }


        //memberPayments

        public static List<PaymentVM> GetPaymentByPayeeId(int PayeeId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {

                    var sql = "Select v.Id as Id,VoucherNo,PayeeId,Payee,PaymentDetails,CashBookId,TransDate,Month,AccountId," +
                                    "Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate,UserId " +
                                    "From (" +
                                        "(Select * From Payments where PayeeId=@PayeeId) v " +
                                        "Left Join " +
                                        "(Select * From CashBooks) c " +
                                        "On v.CashBookId=c.Id " +
                                        ")";
                    var result = cnn.Query<PaymentVM>(sql, new { PayeeId });
                    return result.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static CashBook GetPaymentById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<CashBook>("Select * From CashBooks where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertPayment(PaymentVM payment, IDbConnection cnn)
        {
            try
            {
                int Result = cnn.Execute("insert into Payments(VoucherNo,PayeeId,Payee,PaymentDetails,CashBookId) " +
                        "values(@VoucherNo,@PayeeId,@Payee,@PaymentDetails,@CashBookId);SELECT last_insert_rowid();", payment);
                if (Result < 1) { throw new Exception(); }

                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int InsertPayment(PaymentVM payment, List<CashBookVoteVM> cashBookVotes)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //get cashbook
                            CashBook CashBook = new CashBook
                            {
                                Id = 0,
                                TransDate = payment.TransDate,
                                Month = payment.Month,
                                AccountId = payment.AccountId,
                                Amount = payment.Amount,
                                AmountWords = payment.AmountWords,
                                PayMode = payment.PayMode,
                                PayModeNo = payment.PayModeNo,
                                CreditOrDebit = payment.CreditOrDebit, //Credit,Debit
                                Category = payment.Category, //MemberDeposits,NonMemberDeposits 
                                BankDate = payment.BankDate,
                                UserId = CurUserId,
                            };

                            //insert on cashbook and cashbookvotes
                            int CsbkId = InsertCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }
                            payment.CashBookId = CsbkId;

                            //insert on payment 
                            int Result = cnn.Execute("insert into Payments(VoucherNo,PayeeId,Payee,PaymentDetails,CashBookId) " +
                                    "values(@VoucherNo,@PayeeId,@Payee,@PaymentDetails,@CashBookId);", payment);
                            if (Result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int UpdatePayment(PaymentVM payment, List<CashBookVoteVM> cashBookVotes)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //get cashbook
                            CashBook CashBook = new CashBook
                            {
                                Id = payment.CashBookId,
                                TransDate = payment.TransDate,
                                Month = payment.Month,
                                AccountId = payment.AccountId,
                                Amount = payment.Amount,
                                AmountWords = payment.AmountWords,
                                PayMode = payment.PayMode,
                                PayModeNo = payment.PayModeNo,
                                CreditOrDebit = payment.CreditOrDebit, //Credit,Debit
                                Category = payment.Category, //MemberDeposits,NonMemberDeposits 
                                BankDate = payment.BankDate,
                                UserId = CurUserId,
                            };

                            //update on cashbook and cashbookvotes
                            int CsbkId = UpdateCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }

                            //update on payment 
                            int Result = cnn.Execute("update Payments set VoucherNo=@VoucherNo,PayeeId=@PayeeId,Payee=@Payee,PaymentDetails=@PaymentDetails " +
                                            "where CashBookId=@CashBookId;", payment);
                            if (Result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int DeletePayment(int CashBookId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //delete on cashbook and cashboovotes
                            var Delresult = DeleteCashBook(CashBookId, cnn);
                            if (Delresult < 1) { throw new Exception(); }

                            //delete from payments
                            var result = cnn.Execute("delete from payments where CashBookId=@CashBookId", new { CashBookId });
                            if (result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;

                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int GetNextVoucherNo()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                try
                {
                    int PaymentNo = 0;
                    PaymentNo = cnn.ExecuteScalar<int>($"SELECT CASE WHEN MAX(VoucherNo) IS NULL THEN 1 ELSE MAX(VoucherNo) + 1 END FROM payments,cashbooks where payments.cashbookid=cashbooks.id and cashbooks.transdate BETWEEN @FinYearStart AND @FinYearEnd;", new { FinYearStart, FinYearEnd });
                    return PaymentNo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in generating Voucher number");
                    return 0;
                }
            }

        }


        //Member Fine

        public static List<MemberFineVM> GetFineByPayerId(int PayerId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    //Id,PayerId,Payer,Details,ReceiptCsbkId,PaymentCsbkId,TransDate,AccountId,Amount,
                    //PayMode,PayModeNo,Category,BankDate

                    var sql = "Select MemberFines.Id as Id,PayerId,Payer,Receipts.ReceiptDetails as Details," +
                              "ReceiptCsbkId,PaymentCsbkId,TransDate,AccountId,Amount,PayMode,PayModeNo,Category,BankDate " +
                                    "From Receipts,MemberFines,Cashbooks " +
                                    "Where Receipts.PayerId=@PayerId " +
                                    "And Cashbooks.Id=Receipts.CashBookId " +
                                    "And MemberFines.ReceiptCsbkId=Receipts.CashBookId ";
                    var result = cnn.Query<MemberFineVM>(sql, new { PayerId });
                    return result.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static MemberFine GetFineById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<MemberFine>("Select * From MemberFines where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertFine(MemberFineVM MemberFine)
        {
            try
            {
                //Get fine and fine withdraw votes
                int FineVoteID = GetVoteId("Fine");
                int FineWithdrawalID = GetVoteId("FineWithdrawal");

                if (FineVoteID < 1 || FineWithdrawalID < 1)
                {
                    MessageBox.Show("Error in creating fine and fine wthdrawal vote"); throw new Exception();
                }

                //get cashbook
                CashBook CashBook = new CashBook
                {
                    Id = 0,
                    TransDate = MemberFine.TransDate,
                    Month = UsableFunctions.GetMonthInWords(MemberFine.TransDate.Month),
                    AccountId = MemberFine.AccountId,
                    Amount = MemberFine.Amount,
                    AmountWords = UsableFunctions.ToWords(MemberFine.Amount),
                    PayMode = MemberFine.PayMode,
                    PayModeNo = MemberFine.PayModeNo,
                    CreditOrDebit = "Credit", //Credit,Debit
                    Category = MemberFine.Category, //MemberDeposits,NonMemberDeposits 
                    BankDate = MemberFine.BankDate,
                    UserId = CurUserId,
                };

                //create receipt cashbookvotes

                List<CashBookVoteVM> cashBookVote = new List<CashBookVoteVM>();
                cashBookVote.Add(new CashBookVoteVM { VoteId = FineVoteID, VoteAmount=MemberFine.Amount });
               
                // insert fine details

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            int ReceiptCsbkId = 0;
                            int PaymentCsbkId = 0;

                            #region Receipt

                            ReceiptVM receipt = new ReceiptVM
                            {
                                ReceiptNo = 0,
                                PayerId = MemberFine.PayerId,
                                Payer = MemberFine.Payer,
                                Details = MemberFine.Details
                            };

                            //insert on cashbook and cashbookvotes
                            int CsbkRctId = InsertCashBook(CashBook, cashBookVote, cnn);
                            if (CsbkRctId < 1) { throw new Exception(); }
                            receipt.CashBookId = CsbkRctId;

                            //insert on receipt 
                             ReceiptCsbkId = InsertReceipt(receipt, cnn);
                            if (ReceiptCsbkId < 1) { throw new Exception(); }


                            #endregion

                            #region Payment

                            PaymentVM Payment = new PaymentVM
                            {
                                
                                VoucherNo = 0,
                                PayeeId = MemberFine.PayerId,
                                Payee = MemberFine.Payer,
                                PaymentDetails = MemberFine.Details,
                            };

                            CashBook.CreditOrDebit = "Debit";
                            cashBookVote.FirstOrDefault().VoteId = FineWithdrawalID;  
                            
                            //insert on cashbook and cashbookvotes
                            int CsbkPayId = InsertCashBook(CashBook, cashBookVote, cnn);
                            if (CsbkPayId < 1) { throw new Exception(); }
                            Payment.CashBookId = CsbkPayId;

                            //insert on payment 
                            PaymentCsbkId = InsertPayment(Payment, cnn);
                            if (PaymentCsbkId < 1) { throw new Exception(); }

                            #endregion


                            //insert on MemberFines 
                            int Result = cnn.Execute("insert into MemberFines(ReceiptCsbkId,PaymentCsbkId) " +
                                    "values(@ReceiptCsbkId,@PaymentCsbkId);", receipt);
                            if (Result < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static int UpdateFine(MemberFineVM MemberFine)
        {
            try
            {
               
                //get cashbook
                CashBook CashBook = new CashBook
                {
                    Id = 0,
                    TransDate = MemberFine.TransDate,
                    Month = UsableFunctions.GetMonthInWords(MemberFine.TransDate.Month),
                    AccountId = MemberFine.AccountId,
                    Amount = MemberFine.Amount,
                    AmountWords = UsableFunctions.ToWords(MemberFine.Amount),
                    BankDate = MemberFine.BankDate,
                    UserId = CurUserId,
                };

                
                // insert fine details

                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            #region update Receipt,cashbook,cashbookvotes,payments

                            //update on cashbook and cashbookvotes both receipt and payment
                            int CsbkRctId = UpdateCashBookForFine(CashBook, MemberFine.ReceiptCsbkId, MemberFine.PaymentCsbkId, cnn);
                            if (CsbkRctId < 1) { throw new Exception(); }
                           
                            //update on receipt 

                            var Rctresult = cnn.Execute("update receipts set PayerId=@PayerId,Payer=@Payer,ReceiptDetails=@Details," +
                                                        "where CashBookId=@ReceiptCsbkId )", MemberFine );
                            if (Rctresult < 0) { return 0; }

                            //update on payment 

                            var Payresult = cnn.Execute("update payments set PayeeId=@PayerId,Payee=@Payer,PaymentDetails=@Details," +
                                                        "where CashBookId=@PaymentCsbkId )", MemberFine);
                            if (Payresult < 0) { return 0; }

                            #endregion

                            #region memberfines 
                            //insert on MemberFines 
                           //nothing to update here
                            #endregion

                          
                            Trans.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        public static int DeleteFine(int ReceiptCsbkId, int PaymentCsbkId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        try
                        {
                            //delete on cashbook and cashboovotes
                            var DelRctresult = DeleteCashBook(ReceiptCsbkId, cnn);
                            if (DelRctresult < 1) { throw new Exception(); }

                            var DelPayresult = DeleteCashBook(PaymentCsbkId, cnn);
                            if (DelPayresult < 1) { throw new Exception(); }

                            //delete from receipts
                            var rctresult = cnn.Execute("delete from receipts where CashBookId=@ReceiptCsbkId", ReceiptCsbkId );
                            if (rctresult < 1) { throw new Exception(); }

                            var payresult = cnn.Execute("delete from payments where CashBookId=@PaymentCsbkId", PaymentCsbkId);
                            if (payresult < 1) { throw new Exception(); }

                            Trans.Commit();
                            return 1;

                        }
                        catch (Exception)
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

    }
}
