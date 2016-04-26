using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio; 

namespace services.Controllers
{
    public class ProjetosController : ApiController
    {
        // GET: api/Projetos
        public IEnumerable<string> Get()
        {
            return new string[] { "valssssue1", "value2" };
        }
        // GET: api/Projetos
        public IEnumerable<string> Get(int coordenador)
        {
            projetoNegocios projetos = new projetoNegocios();
            yield return projetos.GetProjetos(coordenador);
        }

        // POST: api/Projetos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Projetos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Projetos/5
        public void Delete(int id)
        {
        }
    }
}
