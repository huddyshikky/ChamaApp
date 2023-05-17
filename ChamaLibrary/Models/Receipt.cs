using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public int ReceiptNo { get; set; }
        public int PayerId { get; set; }
        public string Payer { get; set; }
        public string ReceiptDetails { get; set; }
        public int CashBookId { get; set; }
    }
}
