using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class RelationshipParty : EffectiveModel
    {
        protected RelationshipParty() { }
        public RelationshipParty(RelationshipPartyType relationshipPartyRoleType
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
        public RelationshipPartyType RelationshipPartyRoleType { get; set; }
    }
}
