using Microsoft.AspNetCore.Mvc;
using pruebaWebHook.Models;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace pruebaWebHook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteControl : ControllerBase
    {
        private readonly Datos _datos;

        public ClienteControl(IConfiguration configuration)
        {
            _datos = new Datos(configuration);
        }

        [HttpGet("webhook")]
        public IActionResult Webhook(
            [FromQuery(Name = "hub.mode")] string mode,
            [FromQuery(Name = "hub.challenge")] string challenge,
            [FromQuery(Name = "hub.verify_token")] string verify_token)
        {
            if (verify_token == "hola")
            {
                return Ok(challenge);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("webhook")]
        public IActionResult Webhook([FromBody] WebHookResponseModel entry)
        {
            if (entry != null)
            {
                string mensajeRecibido = entry.entry[0].changes[0].value.messages[0].text.body;
                string idWa = entry.entry[0].changes[0].value.messages[0].id;
                string telefonoWa = entry.entry[0].changes[0].value.messages[0].from;

                _datos.Insertar(mensajeRecibido, idWa, telefonoWa);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

