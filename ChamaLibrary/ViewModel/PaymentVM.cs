using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.ViewModel
{
    public class PaymentVM
    {
        public int Id { get; set; }
        public int VoucherNo { get; set; }
        public int PayeeId { get; set; }
        public string Payee { get; set; }
        public string PaymentDetails { get; set; }
        public int CashBookId { get; set; }
        public DateTime TransDate { get; set; }
        public string Month { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string AmountWords { get; set; }
        public string PayMode { get; set; }
        public string PayModeNo { get; set; }
        public string CreditOrDebit { get; set; } //Credit,Debit
        public string Category { get; set; } //MemberDeposits,NonMemberDeposits 
        public DateTime BankDate { get; set; }
        public int UserId { get; set; }
    }
}
