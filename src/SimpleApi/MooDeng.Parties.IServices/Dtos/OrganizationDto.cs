using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class OrganizationDto : OrganizationInfoDto
    {
        public Guid? PartyId { get; set; }
        public TypeInfoDto? PartyRole { get; set; }
    }
}
