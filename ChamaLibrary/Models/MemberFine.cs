using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class MemberFine
    {
        public int Id { get; set; }
        public int ReceiptCsbkId { get; set; }
        public int PaymentCsbkId { get; set; }
    }
}
