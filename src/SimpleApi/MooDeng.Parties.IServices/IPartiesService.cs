using MooDeng.Parties.IServices.Dtos;
using System.Collections.Immutable;

namespace MooDeng.Parties.IServices
{
    public interface IPartiesService
    {
        Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime? roleEffectiveDate);
        Task SaveOrganizationAsync(Guid partyId, OrganizationInfoDto organization);
        Task<OrganizationDto> NewOrganizationAsync(NewOrganizationInfoDto newOrganization);

        Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime? relationshipPartyEffectiveDate);

        Task SavePartyAsync(Guid partyId, PartyInfoDto partyInfoDto);

        Task<PartyDto> NewPartyAsync(NewPartyInfoDto newParty);

        Task DeletePartyAsync(Guid partyId);
    }
}
