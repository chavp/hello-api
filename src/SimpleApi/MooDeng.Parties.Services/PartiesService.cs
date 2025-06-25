using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dtos;
using MooDeng.Parties.IServices.Values;
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

        public async Task DeletePartyAsync(Guid partyId)
        {
            using var db = _contextFactory.CreateDbContext();

            var target = db.Parties.Single(x => x.Id == partyId);

            // remove relationship
            var relationships = db.RelationshipParties
                .Where(x => x.FromPartyRole.PartyId == target.Id
                || x.ToPartyRole.PartyId == target.Id);
            db.RemoveRange(relationships);

            // remove role
            var roles = db.PartyRoles.Where(x => x.PartyId == target.Id);
            db.RemoveRange(roles);

            db.Remove(target);

            await db.SaveChangesAsync();
        }

        public async Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime? roleEffectiveDate)
        {
            using var db = _contextFactory.CreateDbContext();

            var qOrgs = from z in db.Parties.OfType<Organization>()
                        join pr in db.PartyRoles on z equals pr.Party
                        where pr.PartyRoleType.Code == roleTypeCode
                        select new { z, pr };

            if (roleEffectiveDate.HasValue)
            {
                qOrgs = qOrgs.Where(x => x.pr.EffectiveDateTime <= roleEffectiveDate
                         && roleEffectiveDate <= x.pr.ExpiryDateTime);
            }

            var orgs = (from x in qOrgs
                        select new OrganizationDto
                        {
                            PartyId = x.pr.PartyId,
                            Info = new OrganizationValue
                            {
                                Code = x.z.Code,
                                Name = x.z.Name,
                            },
                            PartyRole = new PartyRoleDto
                            {
                                Id = x.pr.Id,
                                EffectiveDateTime = x.pr.EffectiveDateTime,
                                ExpiryDateTime = x.pr.ExpiryDateTime,
                                TypeCode = x.pr.PartyRoleType.Code,
                            }
                        }).ToList();

            return orgs.ToImmutableList();
        }

        public async Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime? relationshipPartyEffectiveDate)
        {
            using var db = _contextFactory.CreateDbContext();

            var qPets = from p in db.Parties
                        join prr in db.RelationshipParties
                            .Include(x => x.RelationshipPartyRoleType)
                            .Include(x => x.FromPartyRole.PartyRoleType)
                            .Include(x => x.ToPartyRole.PartyRoleType)
                        on p.Id equals prr.ToPartyRole.PartyId
                        where prr.RelationshipPartyRoleType.Code == relationshipPartyRoleTypeCode
                        && prr.FromPartyRole.PartyId == fromPartyId
                        select new { p, prr };

            if (relationshipPartyEffectiveDate.HasValue)
            {
                qPets = qPets.Where(x => x.prr.EffectiveDateTime <= relationshipPartyEffectiveDate
                         && relationshipPartyEffectiveDate <= x.prr.ExpiryDateTime);
            }

            var pets = (from x in qPets
                        select new PartyDto
                        {
                            PartyId = x.p.Id,
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
                                FromPartyRoleId = x.prr.FromPartyRoleId,
                                TypeCode = x.prr.RelationshipPartyRoleType.Code,
                                EffectiveDateTime = x.prr.EffectiveDateTime,
                                ExpiryDateTime = x.prr.ExpiryDateTime,
                            }
                        }).ToList();
            return pets.ToImmutableList();
        }

        public async Task<OrganizationDto> NewOrganizationAsync(NewOrganizationInfoDto newOrganization)
        {
            using var db = _contextFactory.CreateDbContext();

            var org = new Organization(newOrganization.Info.Code) { Name = newOrganization.Info.Name };
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
                Info = new OrganizationValue
                {
                    Code = org.Code,
                    Name = org.Name,
                },
                PartyRole = new PartyRoleDto
                {
                    Id = partyRole.Id,
                    EffectiveDateTime = partyRole.EffectiveDateTime,
                    ExpiryDateTime = partyRole.ExpiryDateTime,
                    TypeCode = partyRole.PartyRoleType.Code,
                }
            };
        }

        public async Task<PartyDto> NewPartyAsync(NewPartyInfoDto newParty)
        {
            using var db = _contextFactory.CreateDbContext();

            // new party
            var party = new Animal
            {
                Name = newParty.Name,
            };
            db.Add(party);

            // set party role
            var partyRoleType = db.PartyRoleTypes.Single(x => x.Code == newParty.PartyRole.TypeCode);
            var partyRole = new PartyRole(partyRoleType, party)
            {
                EffectiveDateTime = newParty.PartyRole.EffectiveDateTime,
                ExpiryDateTime = newParty.PartyRole.ExpiryDateTime,
            };
            db.Add(partyRole);

            // set relationship
            var relaPartyType = db.RelationshipPartyTypes.Single(x => x.Code == newParty.RelationshipParty.TypeCode);
            var fromPartyRole = db.PartyRoles.Single(x => x.Id == newParty.RelationshipParty.FromPartyRoleId);
            var relaParty = new RelationshipParty(relaPartyType, fromPartyRole, partyRole)
            {
                EffectiveDateTime = newParty.RelationshipParty.EffectiveDateTime,
                ExpiryDateTime = newParty.RelationshipParty.ExpiryDateTime,
            };
            db.Add(relaParty);

            await db.SaveChangesAsync();

            return new PartyDto 
            { 
                PartyId = party.Id,
                Name = party.Name,
                PartyRole = new PartyRoleDto
                {
                    Id = partyRole.Id,
                    TypeCode = partyRoleType.Code,
                    EffectiveDateTime = partyRole.EffectiveDateTime,
                    ExpiryDateTime= partyRole.ExpiryDateTime,
                },
                RelationshipParty = new RelationshipPartyDto
                {
                    Id = relaParty.Id,
                    FromPartyRoleId = fromPartyRole.Id,
                    TypeCode = relaPartyType.Code,
                    EffectiveDateTime = relaParty.EffectiveDateTime,
                    ExpiryDateTime = relaParty.ExpiryDateTime,
                }
            };
        }

        public async Task SaveOrganizationAsync(Guid partyId, OrganizationValue organization)
        {
            using var db = _contextFactory.CreateDbContext();

            var org = await db.Parties.OfType<Organization>().SingleAsync(x => x.Id == partyId);
            org.Code = organization.Code;
            org.Name = organization.Name;
            org.Update();

            db.SaveChanges();
        }

        public async Task SavePartyAsync(Guid partyId, PartyInfoDto partyInfoDto)
        {
            using var db = _contextFactory.CreateDbContext();

            var target = await db.Parties.SingleAsync(x => x.Id == partyId);
            target.Name = partyInfoDto.Name;
            target.Update();

            db.SaveChanges();
        }
    }
}
