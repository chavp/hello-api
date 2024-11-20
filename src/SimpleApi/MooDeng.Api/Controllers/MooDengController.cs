using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MooDeng.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MooDengController : ControllerBase
    {
        [HttpGet(Name = "deng")]
        public string Deng([FromQuery] int deng)
        {
            var dengs = string.Empty;
            for (int i = 0; i < deng; ++i)
            {
                dengs += "deng ";
            }
            return dengs;
        }
    }
}
