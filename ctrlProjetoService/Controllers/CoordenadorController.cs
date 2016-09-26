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
    public class CoordenadorController : ApiController
    {

        [EnableCors("*", "*", "*")]
        [Route("api/coordenador/GetCoordenadorLista/nome/{nome?}")]
        public IEnumerable<string> GetCoordenadorLista(string nome = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/coordenador/GetCoordenadorLista/nome/{nome?}")]
        public IEnumerable<string> GetCoordenadorById(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorById(id);
        }
               
        [EnableCors("*", "*", "*")]
        [Route("api/coordenador/GetCoordenadorIncluir/nome/{nome}/email/{email?}")]
        public IEnumerable<string> GetCoordenadorIncluir(string nome, string email)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorIncluir(nome, email);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/coordenador/GetCoordenadorUpdate/id/{id}/nome/{nome?}/email/{email?}")]
        public IEnumerable<string> GetCoordenadorUpdate(int id, string nome = "", string email = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorUpdate(id, nome, email);
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        [Route("api/coordenador/ExcluirCoordenador/id/{id}")]
        public IEnumerable<string> ExcluirCoordenador(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.CoordenadorExcluir(id);
        }


        // GET: api/Coordenador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Coordenador/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Coordenador
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Coordenador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Coordenador/5
        public void Delete(int id)
        {
        }
    }
}
