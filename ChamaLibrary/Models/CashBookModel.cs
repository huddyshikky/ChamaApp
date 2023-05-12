using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class CashBookModel
    {
        public int Id { get; set; }
        public int Member_Id { get; set; }
        public string Name { get; set; }
        public string Trans_Date { get; set; }
        public string Paymode { get; set; }
        public string ModeNo { get; set; }
        public string TransType { get; set; } //Credit,Debit
        public string TransCategory { get; set; } //MemberDeposits,NonMemberDeposits
        public decimal Amount { get; set; }      
    }
}
