using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class RelationshipPartyRoleType : TypeModel
    {
        public const string BringUp = "BRING_UP";
        protected RelationshipPartyRoleType() { }

        public RelationshipPartyRoleType(string code)
        {
            Code = code;
        }
    }
}
