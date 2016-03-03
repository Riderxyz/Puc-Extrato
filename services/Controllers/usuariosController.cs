using System.Collections.Generic;
using System.Web.Http;
using Negocio;
using Cors.ConfigProfiles;

namespace services.Controllers
{
    [EnableCors("*", "*", "*")]
    public class usuariosController : ApiController
    {
        // GET: api/usuarios
        public IEnumerable<string> Get()
        {
            usuariosNegocios usuario = new usuariosNegocios();
            return new string[] { "value1", "value2" };
        }

        // GET: api/usuarios
        public IEnumerable<string> Get(string _usuario , string _senha)
        {
            usuariosNegocios usuario = new usuariosNegocios();
            yield return usuario.ValidarUsuario(_usuario, _senha);
        }


        // GET: api/usuarios/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/usuarios
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/usuarios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/usuarios/5
        public void Delete(int id)
        {
        }
    }
}
