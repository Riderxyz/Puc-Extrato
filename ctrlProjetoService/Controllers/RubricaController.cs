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
    public class RubricaController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("api/rubrica/GetRubricaLista/nome/{nome?}")]
        public IEnumerable<string> GetRubricaLista(string nome = "")
        {
           rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetListaRubricas(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/rubrica/GetRubricaIncluir/Numrubrica/{Numrubrica}/nome/{nome}/tipo/{tipo}")]
        public IEnumerable<string> GetRubricaIncluir(int Numrubrica, string nome, string tipo)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasIncluir(Numrubrica, nome, tipo);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/rubrica/GetRubricaAtualizar/Numrubrica/{Numrubrica}/nome/{nome}/tipo/{tipo}")]
        public IEnumerable<string> GetRubricaAtualizar(Int32 Numrubrica, string nome, string tipo)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasAtualizar(Numrubrica, nome, tipo);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/rubrica/GetRubricaExcluir/Numrubrica/{Numrubrica}")]
        public IEnumerable<string> GetRubricaExcluir(Int32 Numrubrica)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasExcluir(Numrubrica);
        }


        // GET: api/Rubrica
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Rubrica/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rubrica
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rubrica/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Rubrica/5
        public void Delete(int id)
        {
        }
    }
}
