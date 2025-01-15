using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dtos;
using MooDeng.Parties.Models;
using System.Collections.Immutable;

namespace MooDeng.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MooDengController : ControllerBase
    {
        IPartiesService _partiesService;
        public MooDengController(IPartiesService partiesService)
        {
            _partiesService = partiesService;
        }

        [HttpGet("deng")]
        public async Task<string> Deng([FromQuery] int deng)
        {
            var dengs = string.Empty;
            for (int i = 0; i < deng; ++i)
            {
                dengs += "deng ";
            }
            return dengs;
        }

        [HttpGet("zoos")]
        public async Task<IImmutableList<OrganizationDto>> Zoos([FromQuery] DateTime? activeDate = null)
        {
            var zoos = await _partiesService.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, activeDate);
            return zoos;
        }

        [HttpPut("zoos/{partyId}")]
        public async Task PutZoos(Guid partyId, OrganizationInfoDto data)
        {
            await _partiesService.SaveOrganizationAsync(partyId, data);
        }

        [HttpPost("zoos")]
        public async Task<OrganizationDto> PostZoos(NewOrganizationInfoDto data)
        {
            return await _partiesService.NewOrganizationAsync(data);
        }

        [HttpGet("zoos/{partyId}/bring-up")]
        public async Task<IImmutableList<PartyDto>> ZoosBringUp(Guid partyId, [FromQuery] DateTime? activeDate = null)
        {
            var zoos = await _partiesService.GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(partyId,
                RelationshipPartyType.BringUp, activeDate);
            return zoos;
        }

        [HttpPut("parties/{partyId}")]
        public async Task PutParties(Guid partyId, PartyInfoDto data)
        {
            await _partiesService.SavePartyAsync(partyId, data);
        }
    }
}
