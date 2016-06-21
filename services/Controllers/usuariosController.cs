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
        public IEnumerable<string> Get(string _usuario, string _senha)
        {
            usuariosNegocios usuario = new usuariosNegocios();
            yield return usuario.ValidarUsuario(_usuario, _senha);
        }
        // GET: api/usuarios
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //[Route("api/usuarios/GravarCoordenador/{coordenador:int}, {nomeusuario}, {email}, {senha}")]
        //[Route("customers/{customerId}/orders/{orderId}")]
        [Route("api/usuarios/GravarCoordenador/coordenador/{coordenador}/nomeusuario/{nomeusuario}/email/{email}")]
        //[ActionName("GravarCoordenador")]
        [EnableCors("*", "*", "*")]
        public IEnumerable<string> GravarCoordenador(int coordenador, string nomeusuario, string email)
        {
            usuariosNegocios usuario = new usuariosNegocios();
            yield return usuario.GravarCoordenador(coordenador, nomeusuario, email);
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
