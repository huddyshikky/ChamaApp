using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class MemberModel
    {
        public int Id { get; set; } 
        public Int64 IdentityNo { get; set; }    
        public string MemberName { get; set; }
        public int IsActive { get; set; }= 1;

    }
}
