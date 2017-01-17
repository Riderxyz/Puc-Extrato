using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Usuarios")]
    public class UsuarioController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<string> Listar(string nome)
        {
            Negocios_C.Usuarios usuario = new Negocios_C.Usuarios();
            yield return usuario.Listar(nome);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("AtualizarCampos")]
        public IEnumerable<string> AtualizarDadosCinemas(string campo, int id, string valor)
        {
            Negocio_C.NegociosGenericos generico = new Negocio_C.NegociosGenericos();
            yield return generico.AtualizarCamposTabela("usuarios",campo, id, valor);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Autenticar")]
        public IEnumerable<string> Autenticar(string usuario, string pass)
        {
            Negocios_C.Usuarios usuarios = new Negocios_C.Usuarios();
            yield return usuarios.Autenticar(usuario, pass);
        }


        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("SetarSenhaInicial")]
        public IEnumerable<string> SetarSenhaInicial(int id, string pass)
        {
            Negocios_C.Usuarios usuarios = new Negocios_C.Usuarios();
            yield return usuarios.SetarSenhaInicial(id, pass);
        }
    }
}
