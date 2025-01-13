using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MooDeng.Parties.IServices;
using MooDeng.Parties.IServices.Dto;
using MooDeng.Parties.Models;

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
        public async Task<OrganizationDto> Zoos([FromQuery] string code)
        {
            var zoos = await _partiesService.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, DateTime.Today);
            return zoos.SingleOrDefault(x => x.PartyCode == code);
        }
    }
}
