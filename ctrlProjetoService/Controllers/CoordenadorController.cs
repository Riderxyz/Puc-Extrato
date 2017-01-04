using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Cors.ConfigProfiles;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Coordenador")]
    public class CoordenadorController : ApiController
    {

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<string> Listar(string nome = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GetCoordenadorById")]
        public IEnumerable<string> GetCoordenadorById(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorById(id);
        }
               
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Incluir")]
        public IEnumerable<string> Incluir(string nome, string email?)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorIncluir(nome, email);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Atualizar")]
        public IEnumerable<string> Atualizar(int id, string nome = "", string email = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorUpdate(id, nome, email);
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        public IEnumerable<string> Excluir(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.CoordenadorExcluir(id);
        }
    }
}
