using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class LoanRepayment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int ReceiptId { get; set; }
    }
}
