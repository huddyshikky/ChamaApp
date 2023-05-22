using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.ViewModel
{
    public class MemberFineVM
    {
        public int Id { get; set; }
        public int PayerId { get; set; }
        public string Payer { get; set; }
        public string Details { get; set; }
        public int ReceiptCsbkId { get; set; }
        public int PaymentCsbkId { get; set; }
        public DateTime TransDate { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string PayMode { get; set; }
        public string PayModeNo { get; set; }
        public string Category { get; set; } //MemberDeposits,NonMemberDeposits 
        public DateTime BankDate { get; set; }

        //Id,PayerId,Payer,Details,ReceiptCsbkId,PaymentCsbkId,TransDate,AccountId,Amount,PayMode,PayModeNo,Category,BankDate
    }
}
