using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dtos;
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
                                Code = x.z.Code,
                                Name = x.z.Name,
                                PartyRole = new TypeInfoDto
                                {
                                    EffectiveDateTime = x.pr.EffectiveDateTime,
                                    ExpiryDateTime = x.pr.ExpiryDateTime,
                                    TypeCode = x.pr.PartyRoleType.Code,
                                }
                            }).ToList();

                return orgs.ToImmutableList();
            }
        }

        public async Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime? activeDate)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var qPets = from p in db.Parties
                            join prr in db.RelationshipParties
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
                                Id = x.p.Id,
                                Name = x.p.Name,
                                PartyRole = new PartyRoleDto
                                {
                                    Id = x.prr.ToPartyRole.Id,
                                    TypeCode = x.prr.ToPartyRole.PartyRoleType.Code,
                                    EffectiveDateTime = x.prr.ToPartyRole.EffectiveDateTime,
                                    ExpiryDateTime = x.prr.ToPartyRole.ExpiryDateTime,
                                },

                                RelationshipParty = new RelationshipPartyDto
                                {
                                    Id = x.prr.Id,
                                    TypeCode = x.prr.RelationshipPartyRoleType.Code,
                                    EffectiveDateTime = x.prr.EffectiveDateTime,
                                    ExpiryDateTime = x.prr.ExpiryDateTime,
                                }
                            }).ToList();
                return pets.ToImmutableList();
            }
        }

        public async Task<OrganizationDto> NewOrganizationAsync(NewOrganizationInfoDto newOrganization)
        {
            var db = _contextFactory.CreateDbContext();

            var org = new Organization(newOrganization.Organization.Code) { Name = newOrganization.Organization.Name };
            db.Add(org);

            var roleType = db.PartyRoleTypes.Single(x => x.Code == newOrganization.Role.TypeCode);
            var partyRole = new PartyRole(roleType, org) 
            { 
                EffectiveDateTime = newOrganization.Role.EffectiveDateTime,
                ExpiryDateTime = newOrganization.Role.ExpiryDateTime,
            };
            db.Add(partyRole);

            await db.SaveChangesAsync();

            return new OrganizationDto
            {
                PartyId = org.Id,
                Code = org.Code,
                Name = org.Name,
                PartyRole = new TypeInfoDto
                {
                    EffectiveDateTime = partyRole.EffectiveDateTime,
                    ExpiryDateTime = partyRole.ExpiryDateTime,
                    TypeCode = partyRole.PartyRoleType.Code,
                }
            };
        }

        public async Task SaveOrganizationAsync(Guid partyId, OrganizationInfoDto organization)
        {
            var db = _contextFactory.CreateDbContext();

            var org = await db.Parties.OfType<Organization>().SingleAsync(x => x.Id == partyId);
            org.Code = organization.Code;
            org.Name = organization.Name;
            org.Update();

            db.SaveChanges();
        }

        public async Task SavePartyAsync(Guid partyId, PartyInfoDto partyInfoDto)
        {
            var db = _contextFactory.CreateDbContext();

            var target = await db.Parties.SingleAsync(x => x.Id == partyId);
            target.Name = partyInfoDto.Name;
            target.Update();

            db.SaveChanges();
        }
    }
}
