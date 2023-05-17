using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class CashBookVote
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public decimal VoteAmount { get; set; }
        public int CashBookId { get; set; }
    }
}
