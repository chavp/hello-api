using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class RelationshipPartyRole : EffectiveModel
    {
        protected RelationshipPartyRole() { }
        public RelationshipPartyRole(RelationshipPartyRoleType relationshipPartyRoleType
            , PartyRole fromParty
            , PartyRole toParty)
        {
            RelationshipPartyRoleType = relationshipPartyRoleType;
            FromPartyRole = fromParty;
            ToPartyRole = toParty;
        }

        public Guid? FromPartyRoleId { get; set; }
        public PartyRole FromPartyRole { get; set; }

        public Guid? ToPartyRoleId { get; set; }
        public PartyRole ToPartyRole { get; set; }

        public Guid RelationshipPartyRoleTypeId { get; set; }
        public RelationshipPartyRoleType RelationshipPartyRoleType { get; set; }
    }
}
