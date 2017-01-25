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
    [RoutePrefix("planocontas")]
    public class ContasController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("Listar")]
        [HttpGet]
        public IEnumerable<string> Listar(string nome = "")
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        [HttpGet]
        public IEnumerable<string> Excluir(int id)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasExcluir(id);
        }


        [EnableCors("*", "*", "*")]
        [Route("Incluir")]
        [HttpGet]
        public IEnumerable<string> Incluir(string NumConta, string Descricao)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasIncluir(NumConta, Descricao);
        }

        [EnableCors("*", "*", "*")]
        [Route("Atualizar")]
        [HttpGet]
        public IEnumerable<string> Atualizar(int id, string NumConta, string Descricao)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasAtualizar(id, NumConta, Descricao);
        }
    }
}
