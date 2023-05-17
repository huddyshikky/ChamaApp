using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class LoanTransaction
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int CashBookId { get; set; }
    }
}
