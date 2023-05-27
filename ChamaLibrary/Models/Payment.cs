using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class Payment
    {
        //Id,VoucherNo,PayeeId,Payee,PaymentDetails,CashBookId
        public int Id { get; set; }
        public int VoucherNo { get; set; }
        public int PayeeId { get; set; }
        public string Payee{ get; set; }
        public string PaymentDetails { get; set; }
        public int CashBookId { get; set; }
    }
}
