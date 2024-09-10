using EchoWebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EchoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController(ILogger<EchoController> logger) : ControllerBase
    {
        [HttpPost]
        public IActionResult Echo([FromBody] RequestMsg request)
        {
            logger.LogInformation("Echoing message: {0}", request.Message);
            return Ok(new ResponseMsg(request.Message) );
        }
    }
}
