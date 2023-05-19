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
        public static int UserId;
        public static string UserName;
        public static DateTime FinYearStart;
        public static DateTime FinYearEnd;
        public static string FinYear;

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
                MessageBox.Show(" Error getting vote(s)", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Votes WHERE VoteName=@VoteName and Id<>@Id", new { VoteName = ToPropercase(VoteName), Id=Id });
                    if (Found > 0 ) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related vote", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error checking related vote", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting vote by id", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting Bank(s)", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related Bank", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking if Bank has been referenced on another table", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error checking related Bank", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting Bank by id", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting FinYear(s)", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related FinYear", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static bool CheckIfFinYearIsReferenced(int FinYearId)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from Cashbooks WHERE FinYearId=@FinYearId", new { FinYearId });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking if FinYear has been referenced on another table", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
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
                MessageBox.Show(" Error checking related FinYear", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting FinYear by id", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting Account(s)", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related Account", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting Account by id", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public static bool VerifyUserLogin(string UserName, string Password)
        {
            return true;
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
                MessageBox.Show(" Error getting User(s)", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related User", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking if User has been referenced on another table", "@ Chamaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Error getting User by id", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static int InsertUser(User User)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Users(FirstName,LastName,UserName,PassWord,Telephone) values(@FirstName,@LastName,@UserName,@PassWord,@Telephone)", User);
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
                    var result = cnn.Execute("update Users set FirstName=@FirstName,LastName=@LastName,UserName=@UserName,PassWord=@PassWord,Telephone=@Telephone where Id=@Id", User);
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

        //Members

        public static List<Member> GetALLMembers()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<Member>("Select * From Members where IsMember=1", new DynamicParameters());
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
                    MessageBox.Show(" Error checking related Member", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related Id Number", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related Non Member", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related Id Number", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show(" Error checking related LoanType Name", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
               
                int CashBookId = cnn.ExecuteScalar<int>("insert into CashBooks(Trans_Date,Month,AccountId,Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate, UserId,FinYearId) " +
                                    "values(@Trans_Date,@Month,@AccountId,@Amount,@AmountWords,@PayMode,@PayModeNo,@CreditOrDebit,@Category,@BankDate,@UserId,@FinYearId); SELECT last_insert_rowid();", CashBook);
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
                    "Category=@Category,BankDate=@BankDate,UserId=@UserId,FinYearId=@FinYearId where Id=@Id", CashBook);
                if (Upresult < 0) { return 0; }
                if (!(UpdateCashBookVotes(cashBookVotes, cnn, CashBook.Id) > 0)) { return 0; }

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
                                                        "and Votes.Id=CashBookVotes.VoteId", new { Csbk_Id = CashBookId });
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
                    var result = cnn.Execute("insert into CashBookVotes(VoteId,VoteAmount,CashBookId) values(@VoteId,@VoteAmount,@CashBookId)", CashBookVotes);
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


        //memberReceipting

        public static List<ReceiptVM> GetReceiptByPayerId(int PayerId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                     //Id,ReceiptNo,PayerId,Payer,ReceiptDetails,CashBookId,Trans_Date,Month,AccountId,
                     //Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate,UserId,FinYearId
        var sql = "Select r.Id as Id,ReceiptNo,PayerId,ReceiptDetails,CashBookId,TransDate,Month,AccountId," +
                        "Amount,AmountWords,PayMode,PayModeNo,CreditOrDebit,Category,BankDate,UserId,FinYearId " +
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
                                UserId = receipt.UserId,
                                FinYearId = receipt.FinYearId,
                            };

                            //insert on cashbook and cashbookvotes
                            int CsbkId = InsertCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }
                            
                            //insert on receipt 
                            int Result = cnn.ExecuteScalar<int>("insert into Receipts(ReceiptNo,PayerId,Payer,ReceiptDetails,CashBookId) " +
                                    "values(@ReceiptNo,@PayerId,@Payer,@ReceiptDetails,@CashBookId);", receipt);
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
                                UserId = receipt.UserId,
                                FinYearId = receipt.FinYearId,
                            };

                            //update on cashbook and cashbookvotes
                            int CsbkId = UpdateCashBook(CashBook, cashBookVotes, cnn);
                            if (CsbkId < 1) { throw new Exception(); }

                            //update on receipt 
                            int Result = cnn.ExecuteScalar<int>("update Receipts set ReceiptNo=@ReceiptNo,PayerId=@PayerId,Payer=@Payer,ReceiptDetails=@ReceiptDetails " +
                                            "where CashBookId=CashBookId;", receipt);
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

    }
}
