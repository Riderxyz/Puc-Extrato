using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Puc.Negocios_C;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Seguranca")]
    public class SegurancaController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ValidarLogin")]
        public IEnumerable<string> ValidarLogin(string usuario, string senha)
        {
            Puc.Negocios_C.SegurancaNegocios seguranca = new Puc.Negocios_C.SegurancaNegocios();
            yield return seguranca.loginvalidar(usuario, senha);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("SetarSenha")]
        public IEnumerable<string> SetarSenha(int usuario, string senhaatual, string novasenha)
        {
            Puc.Negocios_C.SegurancaNegocios seguranca = new Puc.Negocios_C.SegurancaNegocios();
            yield return seguranca.senhaAlterar(usuario, senhaatual, novasenha);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("SetarSenhaInicial")]
        public IEnumerable<string> SetarSenhaInicial(string usuario, string novasenha)
        {
            Puc.Negocios_C.SegurancaNegocios seguranca = new Puc.Negocios_C.SegurancaNegocios();
            yield return seguranca.senhasetarinicial(usuario, novasenha);
        }
    }
}
