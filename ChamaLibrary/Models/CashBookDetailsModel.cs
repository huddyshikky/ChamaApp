using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class CashBookDetailsModel
    {
        public int Id { get; set; }
        public int Csbk_Id { get; set; }
        public int Vote_Id { get; set; }
        public decimal Amount { get; set; }
    }
}
