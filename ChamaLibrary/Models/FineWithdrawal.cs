using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class FineWithdrawal
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
    }
}
