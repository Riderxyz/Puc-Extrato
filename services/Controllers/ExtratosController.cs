using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio; 

namespace services.Controllers
{
    public class ExtratosController : ApiController
    {
        // GET: api/Projetos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/Projetos
        public IEnumerable<string> Get(int projeto, string di, string df)
        {
            ExtratoNegocios extratos = new ExtratoNegocios(); 
            yield return extratos.GetExtrato (projeto,di,df);
        }
        // GET: api/Projetos/5
        public string Get(int id)
        {
            return "value";
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
