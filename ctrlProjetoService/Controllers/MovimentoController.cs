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
        public IEnumerable<string> ListarMovimentoPorProjeto(string CodigoProjeto, string dataFim = null)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Listar(CodigoProjeto, dataFim: dataFim);
        }

        // GET: api/Movimento
        //public IEnumerable<string> ListarMovimentoPorPorProjeto(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), DateTime? dataInicio = null, DateTime? dataFim = null)
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("api/movimentos/MovimentoIncluir/data/{data}/projeto/{projeto?}/historico/{historico?}/receita/{receita?}/despesa/{despesa?}/rubrica/{rubrica?}/codbanco/{codbanco?}/tipo_lancamento/{tipo_lancamento?}/fatura/{fatura?}/lote/{lote?}/")]
        public IEnumerable<string> Incluir(DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string fatura = default(string), int? lote = -1)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Incluir(data, projeto, historico, receita, despesa, rubrica, codbanco, tipo_lancamento, fatura, lote);
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
