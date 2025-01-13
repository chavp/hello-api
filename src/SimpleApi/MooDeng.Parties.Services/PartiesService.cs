﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IImmutableList<OrganizationDto>> GetOrganizationByRoleTypeCodeAsync(string roleTypeCode, DateTime activeDate)
        {
            using(var db = _contextFactory.CreateDbContext())
            {
                var orgs = (from z in db.Parties.OfType<Organization>()
                            join pr in db.PartyRoles on z equals pr.Party
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
        }

        public async Task<IImmutableList<PartyDto>> GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(Guid fromPartyId, string relationshipPartyRoleTypeCode, DateTime activeDate)
        {
            using (var db = _contextFactory.CreateDbContext())
            {
                var pets = (from p in db.Parties
                            join prr in db.RelationshipPartyRoles on p.Id equals prr.ToPartyRole.PartyId
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
