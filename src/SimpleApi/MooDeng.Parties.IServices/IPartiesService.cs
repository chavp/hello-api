using MooDeng.Parties.IServices.Dto;
using System.Collections.Immutable;

namespace MooDeng.Parties.IServices
{
    public interface IPartiesService
    {
        Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime activeDate);
        Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime activeDate);

        Task SaveOrganizationAsync(Guid partyId, OrganizationDataDto organizationData);
    }
}
