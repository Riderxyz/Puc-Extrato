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
    [RoutePrefix("Rubrica")]
    public class RubricaController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("Listar")]
        [HttpGet]
        public IEnumerable<string> Listar(string nome = "")
        {
           rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetListaRubricas(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("Incluir")]
        [HttpGet]
        public IEnumerable<string> Incluir(int Numrubrica, string nome, string tipo)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasIncluir(Numrubrica, nome, tipo);
        }

        [EnableCors("*", "*", "*")]
        [Route("Atualizar")]
        [HttpGet]
        public IEnumerable<string> Atualizar(Int32 Numrubrica, string nome, string tipo)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasAtualizar(Numrubrica, nome, tipo);
        }

        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        [HttpGet]
        public IEnumerable<string> Excluir(Int32 Numrubrica)
        {
            rubricaNegocio rubrica = new rubricaNegocio();
            yield return rubrica.GetRubricasExcluir(Numrubrica);
        }

    }
}
