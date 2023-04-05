using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class CashBookDetailsViewModel
    {
        public int Id { get; set; } //vote id
        public string VoteName { get; set; }
        public decimal Amount { get; set; }
    }
}
