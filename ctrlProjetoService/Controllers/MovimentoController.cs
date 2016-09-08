using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    public class MovimentoController : ApiController
    {
        // GET: api/Movimento
        //public IEnumerable<string> ListarMovimentoPorPorProjeto(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), DateTime? dataInicio = null, DateTime? dataFim = null)
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("api/movimentos/ListarMovimentoPorProjeto/CodigoProjeto/{CodigoProjeto}/dataFim/{dataFim?}")]
        public IEnumerable<string> ListarMovimentoPorProjeto(string CodigoProjeto,  string dataFim = null )
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Listar(CodigoProjeto, dataFim: dataFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("api/movimentos/ListarSaldoConta/Conta/{Conta}/dataFim/{dataFim?}")]
        public IEnumerable<string> ListarSaldoConta(string Conta, string dataFim = null)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldoConta(Conta, data: dataFim);
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movimento/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movimento
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Movimento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movimento/5
        public void Delete(int id)
        {
        }
    }
}
