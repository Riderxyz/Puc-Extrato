using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Bancos")]
    public class BancoController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<string> Listar(string nome, int banco = -1)
        {
            Negocios_C.NegocioBancos bc = new Negocios_C.NegocioBancos();
            yield return bc.Listar(nome, banco);
        }
    }
}
