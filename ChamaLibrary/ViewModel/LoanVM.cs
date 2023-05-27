using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.ViewModel
{
    public class LoanVM
    {
        //Id,PayeeId,Payee,AccountId,LoanTypeId,TransDate,VoucherNo,PaymentCsbkId,PaymentId,
        //PrincipleAmount,AmountWords,PayMode,PayModeNo,InterestRate,InterestAmount,RepayPeriod,RepayPeriodType,
        //PeriodicRepayAmount,PeriodicPenaltyAmount,TotalAmount

        public int Id { get; set; }
        public int PayeeId { get; set; }
        public string Payee { get; set; }
        public int AccountId { get; set; }
        public int LoanTypeId { get; set; }
        public DateTime TransDate { get; set; }
        public int VoucherNo { get; set; }
        public int PaymentCsbkId { get; set; }
        public int PaymentId { get; set; }
        public decimal PrincipleAmount { get; set; }
        public string AmountWords { get; set; }
        public string PayMode { get; set; }
        public string PayModeNo { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public int RepayPeriod { get; set; }
        public string RepayPeriodType { get; set; }
        public decimal PeriodicRepayAmount { get; set; }
        public decimal PeriodicPenaltyAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
