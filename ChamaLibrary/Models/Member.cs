using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaLibrary.Models
{
    public class Member
    {
        public int Id { get; set; } 
        public string IdentityNo { get; set; }    
        public string MemberName { get; set; }
        public int IsActive { get; set; }= 1;
        public int IsMember { get; set; } = 1;
       
    }
}
