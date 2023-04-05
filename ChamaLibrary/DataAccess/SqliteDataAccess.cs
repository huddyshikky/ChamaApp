using ChamaLibrary.Models;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ChamaLibrary.DataAccess
{
    public class SqliteDataAccess
    {
        //Helpers
        private static string LoadConnectionString(string Id = "Default")=>ConfigurationManager.ConnectionStrings[Id].ToString();
        public static string ToPropercase(string text)
        {
            TextInfo myTI = new CultureInfo("en-GB", false).TextInfo;
            return myTI.ToTitleCase(text.ToLower());
        }

        //votes

        public static List<VoteModel> GetALLVotes()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<VoteModel>("Select * From Votes", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;
               

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
                    MessageBox.Show(" Error checking related student", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static VoteModel GetVoteByName(string VoteName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<VoteModel>("Select * From Votes where VoteName=@VoteName", new { VoteName = ToPropercase(VoteName) });
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
        public static VoteModel GetVoteById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<VoteModel>("Select * From Votes where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertVote(VoteModel vote)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                   var result= cnn.Execute("insert into votes(VoteName) values(@VoteName)", vote);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }    
        public static int UpdateVote(VoteModel vote)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update votes set VoteName=@VoteName where Id=@Id", vote);
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

        //Members

        public static List<MemberModel> GetALLMembers()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<MemberModel>("Select * From Members", new DynamicParameters());
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
                    MessageBox.Show(" Error checking related LoanType", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static MemberModel GetMemberByName(string MemberName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<MemberModel>("Select * From Members where MemberName=@MemberName", new { MemberName = ToPropercase(MemberName) });
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
        public static MemberModel GetMemberById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<MemberModel>("Select * From Members where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertMember(MemberModel Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into Members(IdentityNo,MemberName) values(@IdentityNo,@MemberName)", Member);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateMember(MemberModel Member)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update Members set MemberName=@MemberName where Id=@Id", Member);
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

        //Loan Types

        public static List<LoanTypeModel> GetALLLoanTypes()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<LoanTypeModel>("Select * From LoanTypes", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static bool CheckIfLoanTypeExist(string LoanType)
        {
            bool check = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    int Found = cnn.ExecuteScalar<int>("select count(*) from LoanTypes WHERE LoanType=@LoanType", new { LoanType = ToPropercase(LoanType) });
                    if (Found > 0) { check = true; }

                }
                catch (Exception)
                {
                    check = true;
                    MessageBox.Show(" Error checking related LoanType", "Esent@ Accountant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return check;
        }
        public static LoanTypeModel GetLoanTypeByName(string LoanType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.QuerySingleOrDefault<LoanTypeModel>("Select * From LoanTypes where LoanType=@LoanType", new { LoanType = ToPropercase(LoanType) });
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
        public static LoanTypeModel GetLoanTypeById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<LoanTypeModel>("Select * From LoanTypes where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertLoanType(LoanTypeModel LoanType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("insert into LoanTypes(LoanType) values(@LoanType)", LoanType);
                    return result;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int UpdateLoanType(LoanTypeModel LoanType)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Execute("update LoanTypes set LoanType=@LoanType where Id=@Id", LoanType);
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

        public static List<CashBookModel> GetALLCashBook()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var OutPut = cnn.Query<CashBookModel>("Select * From CashBooks", new DynamicParameters());
                    return OutPut.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static List<CashBookModel> GetCashBookByMemberId(int Member_Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Query<CashBookModel>("Select * From CashBooks where Member_Id=@Member_Id", new { Member_Id = Member_Id });
                    return result.ToList();
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static CashBookModel GetCashBookById(int Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.QueryFirstOrDefault<CashBookModel>("Select * From CashBooks where Id=@Id", new { Id = Id });
                    return result;
                }
            }
            catch (Exception)
            {

                throw;


            }
        }
        public static int InsertCashBook(CashBookModel CashBook, List<CashBookDetailsModel> cashBookDetailsList)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        int CsbkId = cnn.ExecuteScalar<int>("insert into CashBooks(Member_Id,Trans_Date,Paymode,TransType,TransCategory) " +
                            "values(@Member_Id,@Trans_Date,@Paymode,@TransType,@TransCategory); SELECT last_insert_rowid();", CashBook);
                        if (CsbkId > 0)
                        {
                            foreach (var cashBookDetails in cashBookDetailsList)
                            {
                                //insert the csbkid
                                cashBookDetails.Csbk_Id=CsbkId;  
                                if (!(SqliteDataAccess.InsertCashBookDetails(cashBookDetails, cnn) > 0))
                                {
                                    Trans.Rollback();
                                    throw new Exception();
                                }
                            }
                        }
                        else
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                        Trans.Commit();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
                return 0;
            }
        }
        public static int UpdateCashBook(CashBookModel CashBook, List<CashBookDetailsModel> cashBookDetailsList)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                       var result = cnn.Execute("update CashBooks set Trans_Date=@Trans_Date,Paymode=@Paymode where Id=@Id", CashBook);

                        if (result > 0)
                        {
                            var Delresult = UpdateCashBookDetails(cashBookDetailsList, CashBook.Id, cnn);
                            if (Delresult < 1)
                            {
                                Trans.Rollback();
                                throw new Exception();
                            }
                        }
                        else
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }
                        Trans.Commit();
                        return 1;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int DeleteCashBook(int Csbk_Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Open();
                    using (var Trans = cnn.BeginTransaction())
                    {
                        var Delresult = DeleteCashBookDetailsByCsbkId(Csbk_Id, cnn);
                        if (Delresult > 0)
                           {
                             var result = cnn.Execute("delete from CashBooks where Id=@Id", new { Id = Csbk_Id });
                            if (result < 1)
                            {
                                Trans.Rollback();
                                throw new Exception();
                            }
                        }
                        else
                        {
                            Trans.Rollback();
                            throw new Exception();
                        }

                        Trans.Commit();
                        return 1;
                    }
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        //CashBook Votes
        
        //public static List<CashBookDetailsModel> GetALLCashBookDetails()
        //{
        //    try
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //        {
        //            var OutPut = cnn.Query<CashBookDetailsModel>("Select * From LoanTypes", new DynamicParameters());
        //            return OutPut.ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;


        //    }
        //}
        //public static CashBookDetailsModel CashBookDetailsById(int Id)
        //{
        //    try
        //    {
        //        using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
        //        {
        //            var result = cnn.QueryFirstOrDefault<CashBookDetailsModel>("Select * From LoanTypes where Id=@Id", new { Id = Id });
        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;


        //    }
        //}
        
        public static List<CashBookDetailsViewModel> CashBookDetailsByCsbkId(int Csbk_Id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var result = cnn.Query<CashBookDetailsViewModel>("Select Vote_Id as Id,VoteName,Amount From CashBookDetails,Votes" +
                        " where Csbk_Id=@Csbk_Id and Votes.Id=Vote_Id", new { Csbk_Id = Csbk_Id });
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int InsertCashBookDetails(CashBookDetailsModel CashBookDetails, IDbConnection cnn)
        {
            try
            {
                var result = cnn.Execute("insert into CashBookDetails(Csbk_Id,Vote_Id,Amount) values(@Csbk_Id,@Vote_Id,@Amount)", CashBookDetails);
                    return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static int UpdateCashBookDetails(List<CashBookDetailsModel> cashBookDetailsList, int Csbk_Id, IDbConnection cnn)
        {
            try
            {
                var Delresult = DeleteCashBookDetailsByCsbkId(Csbk_Id, cnn);
                if (Delresult > 0)
                {
                    foreach (var cashBookDetails in cashBookDetailsList)
                    {
                        //insert 
                        if (!(SqliteDataAccess.InsertCashBookDetails(cashBookDetails, cnn) > 0))
                        {
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }


        }
        public static int DeleteCashBookDetailsByCsbkId(int Csbk_Id, IDbConnection cnn)
        {
            try
            {
                var result = cnn.Execute("delete from CashBookDetails where Csbk_Id=@Csbk_Id", new { Csbk_Id});
                    return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        //memberReceipting
    }
}
