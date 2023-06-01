using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.ViewModel
{
    public class LoanRepaymentVM
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int PayerId { get; set; }
        public string Payer { get; set; }
        public int AccountId { get; set; }
        public int ReceiptId { get; set; }
        public DateTime TransDate { get; set; }
        public int ReceiptNo { get; set; }
        public int ReceiptCsbkId { get; set; }
        public decimal Amount { get; set; }
        public string AmountWords { get; set; }
        public string PayMode { get; set; }
        public string PayModeNo { get; set; }

    }
}
