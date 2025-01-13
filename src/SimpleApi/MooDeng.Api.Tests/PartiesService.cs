using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dto;
using MooDeng.Parties.Mappings;
using MooDeng.Parties.Models;
using System.Collections.Immutable;

namespace MooDeng.Api.Tests
{
    public class PartiesService : IPartiesService
    {
        readonly PartiesContext _dbContext;
        public PartiesService(PartiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime activeDate)
        {
            var orgs = (from z in _dbContext.Parties.OfType<Organization>()
                        join pr in _dbContext.PartyRoles on z equals pr.Party
                        where pr.PartyRoleType.Code == roleTypeCode
                        && pr.EffectiveDateTime <= activeDate
                         && activeDate <= pr.ExpiryDateTime
                        select new OrganizationDto
                        {
                            PartyId = pr.PartyId,
                            PartyCode = z.Code,
                            PartyName = z.Name,
                            PartyRoleEffectiveDateTime = pr.EffectiveDateTime,
                            PartyRoleExpiryDateTime = pr.ExpiryDateTime,
                            PartyRoleTypeCode = pr.PartyRoleType.Code,
                        }).ToList();

            return orgs.ToImmutableList();
        }

        public async Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime activeDate)
        {
            var pets = (from p in _dbContext.Parties
                        join prr in _dbContext.RelationshipPartyRoles on p.Id equals prr.ToPartyRole.PartyId
                        where prr.RelationshipPartyRoleType.Code == relationshipPartyRoleTypeCode
                        && prr.EffectiveDateTime <= activeDate
                        && activeDate <= prr.ExpiryDateTime
                        && prr.FromPartyRole.PartyId == fromPartyId
                        select new PartyDto
                        {
                            PartyId = p.Id,
                            PartyName = p.Name,
                        }).ToList();
           return pets.ToImmutableList();
        }

        public async Task SaveOrganizationAsync(Guid partyId, OrganizationDataDto organizationData)
        {
            var org = await _dbContext.Parties.OfType<Organization>().SingleAsync(x => x.Id == partyId);
            org.Code = organizationData.PartyCode;
            org.Name = organizationData.PartyName;
            org.Update();
        }
    }
}
