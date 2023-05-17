using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.ViewModel
{
    public class AccountVM
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
    }
}
