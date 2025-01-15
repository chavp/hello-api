using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class NewPartyInfoDto
    {
        public string? Name { get; set; }
        public TypeInfoDto? PartyRole { get; set; }
        public RelationshipPartyInfoDto? RelationshipParty { get; set; }
    }
}
