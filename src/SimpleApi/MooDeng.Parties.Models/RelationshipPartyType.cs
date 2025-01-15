using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class RelationshipPartyType : TypeModel
    {
        public const string BringUp = "BRING_UP";
        protected RelationshipPartyType() { }

        public RelationshipPartyType(string code)
        {
            Code = code;
        }
    }
}
