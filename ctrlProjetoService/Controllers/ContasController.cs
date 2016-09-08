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
    public class ContasController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("api/contas/GetContaLista/nome/{nome?}")]
        public IEnumerable<string> GetContaLista(string nome = "")
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/contas/GetContaExcluir/NumConta/{NumConta}")]
        public IEnumerable<string> GetContaExcluir(string NumConta)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasExcluir(NumConta);
        }


        [EnableCors("*", "*", "*")]
        [Route("api/contas/GetContaIncluir/NumConta/{NumConta}/Descricao/{Descricao}")]
        public IEnumerable<string> GetContaIncluir(string NumConta, string Descricao)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasIncluir(NumConta, Descricao);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/contas/GetContaAtualizar/NumConta/{NumConta}/Descricao/{Descricao}")]
        public IEnumerable<string> GetContaAtualizar(string NumConta, string Descricao)
        {
            contaNegocio conta = new contaNegocio();
            yield return conta.GetContasAtualizar(NumConta, Descricao);
        }
        // GET: api/Contas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contas/5
        public string Get(int id)
        {
            return "value";
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
