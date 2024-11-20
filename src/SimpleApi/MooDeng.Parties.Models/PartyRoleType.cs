using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class PartyRoleType : TypeModel
    {
        public const string Pet = "PET";
        public const string Zoo = "ZOO";

        protected PartyRoleType() { }
        
        public PartyRoleType(string code)
        {
            Code = code;
        }
    }
}
