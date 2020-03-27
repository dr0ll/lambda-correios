using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ewcorreios;
using Microsoft.AspNetCore.Authorization;

namespace ewcorreios.Controllers
{
    [Route("api/correios/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        // GET: api/Cep
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { };
        }

        // GET: api/Cep/65045070
        [HttpGet("{cep}", Name = "Get")]
        public IActionResult Get(string cep)
        {

            if (!Request.Headers["Authorization"].Equals("aXRhbGxvOmxpbmRv")) return Unauthorized();
            cep = cep.Replace("-", "");
            (string response, bool sucesso) = ConsultaCepModel.ConsultaViaCep(cep);
            if (!sucesso) (response, sucesso) = ConsultaCepModel.ConsultaCorreios(cep);
            if (!sucesso) return NotFound(response);
            else return Ok(response);
        }
    }
}
