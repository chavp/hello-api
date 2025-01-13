using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dto
{
    public class OrganizationDto : OrganizationDataDto
    {
        public Guid PartyId { get; set; }
        public string PartyRoleTypeCode { get; set; }
        public DateTime PartyRoleEffectiveDateTime { get; set; }
        public DateTime PartyRoleExpiryDateTime { get; set; }
    }

    public class OrganizationDataDto
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
    }
}
