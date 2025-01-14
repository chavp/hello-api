using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dto;
using MooDeng.Parties.Mappings;
using MooDeng.Parties.Models;
using System.Collections.Immutable;

namespace MooDeng.Parties.Services
{
    public class PartiesService : IPartiesService
    {
        readonly IDbContextFactory<PartiesContext> _contextFactory;
        public PartiesService(IDbContextFactory<PartiesContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime? activeDate)
        {
            using(var db = _contextFactory.CreateDbContext())
            {
                var qOrgs = from z in db.Parties.OfType<Organization>()
                            join pr in db.PartyRoles on z equals pr.Party
                            where pr.PartyRoleType.Code == roleTypeCode
                            select new { z, pr };

                if (activeDate.HasValue)
                {
                    qOrgs = qOrgs.Where(x => x.pr.EffectiveDateTime <= activeDate
                             && activeDate <= x.pr.ExpiryDateTime);
                }

                var orgs = (from x in qOrgs
                            select new OrganizationDto
                            {
                                PartyId = x.pr.PartyId,
                                PartyCode = x.z.Code,
                                PartyName = x.z.Name,
                                PartyRoleEffectiveDateTime = x.pr.EffectiveDateTime,
                                PartyRoleExpiryDateTime = x.pr.ExpiryDateTime,
                                PartyRoleTypeCode = x.pr.PartyRoleType.Code,
                            }).ToList();

                return orgs.ToImmutableList();
            }
        }

        public async Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime? activeDate)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var qPets = from p in db.Parties
                            join prr in db.RelationshipPartyRoles
                                .Include(x => x.RelationshipPartyRoleType)
                                .Include(x => x.FromPartyRole.PartyRoleType)
                                .Include(x => x.ToPartyRole.PartyRoleType)
                            on p.Id equals prr.ToPartyRole.PartyId
                            where prr.RelationshipPartyRoleType.Code == relationshipPartyRoleTypeCode
                            && prr.FromPartyRole.PartyId == fromPartyId
                            select new { p, prr };

                if (activeDate.HasValue)
                {
                    qPets = qPets.Where(x => x.prr.EffectiveDateTime <= activeDate
                             && activeDate <= x.prr.ExpiryDateTime);
                }

                var pets = (from x in qPets
                            select new PartyDto
                            {
                                PartyId = x.p.Id,
                                PartyName = x.p.Name,
                                PartyRoleTypeCode = x.prr.ToPartyRole.PartyRoleType.Code,
                                PartyRoleEffectiveDateTime = x.prr.ToPartyRole.EffectiveDateTime,
                                PartyRoleExpiryDateTime = x.prr.ToPartyRole.ExpiryDateTime,

                                RelationshipPartyRoleTypeCode = x.prr.RelationshipPartyRoleType.Code,
                                RelationshipPartyRoleEffectiveDateTime = x.prr.EffectiveDateTime,
                                RelationshipPartyRoleExpiryDateTime = x.prr.ExpiryDateTime,
                            }).ToList();
                return pets.ToImmutableList();
            }
        }

        public async Task SaveOrganizationAsync(Guid partyId, OrganizationDataDto organizationData)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var org = await db.Parties.OfType<Organization>().SingleAsync(x => x.Id == partyId);
                org.Code = organizationData.PartyCode;
                org.Name = organizationData.PartyName;
                org.Update();

                db.SaveChanges();
            }
        }
    }
}
