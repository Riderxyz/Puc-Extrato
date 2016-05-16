using Cors.ConfigProfiles;
using System.Collections.Generic;
using System.Web.Http;

namespace services.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ContasController : ApiController
    {
        // GET: api/Contas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contas/5
        public IEnumerable<string> GetContasCoordenador(int coordenador, bool LIstarMaes = false)
        {
            if (!LIstarMaes)
            {
                Negocio.contaNegocio contas = new Negocio.contaNegocio();
                yield return contas.GetContas(coordenador);
            }
            else
            {
                Negocio.contaNegocio contas = new Negocio.contaNegocio();
                yield return contas.GetContasMae(coordenador);
            }
        }

        // POST: api/Contas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Contas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contas/5
        public void Delete(int id)
        {
        }
    }
}
