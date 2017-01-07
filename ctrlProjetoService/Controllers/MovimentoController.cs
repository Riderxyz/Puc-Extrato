using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix ("Movimentos")]
    public class MovimentoController : ApiController
    {

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarPagamentos")]
        public IEnumerable<string> ListarPagamentos(string lote , string dataFim)
        {
            if (dataFim.Length < 8)
                dataFim = null;

            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarPagamentos(lote, dataFim);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarMovimentoPorProjeto")]
        public IEnumerable<string> ListarMovimentoPorProjeto(string CodigoProjeto, string dataFim)
        {
            if (dataFim.Length < 8)
                dataFim = null;

            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Listar(CodigoProjeto, dataFim: dataFim);
        }

        // GET: api/Movimento
        //public IEnumerable<string> ListarMovimentoPorPorProjeto(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), DateTime? dataInicio = null, DateTime? dataFim = null)
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Incluir")]
        public IEnumerable<string> Incluir(DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string fatura = default(string), int? lote = -1)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Incluir(data, projeto, historico, receita, despesa, rubrica, codbanco, tipo_lancamento, fatura, lote);
        }

        // GET: api/Movimento
        //public IEnumerable<string> ListarMovimentoPorPorProjeto(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), DateTime? dataInicio = null, DateTime? dataFim = null)
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Atualizar")]
        public IEnumerable<string> Atualizar(Int64 id, string projeto, string historico, double? receita=null, double? despesa=null, int? rubrica=null, string codbanco = default(string), string tipo_lancamento = default(string), string fatura = default(string), int? lote = -1, DateTime? data = null)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Atualizar(id, projeto, historico, receita, despesa, rubrica, codbanco, tipo_lancamento, fatura, lote, data);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Excluir")]
        public IEnumerable<string> Excluir(Int64 id)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.Excluir(id);
        }


        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarSaldoConta")]
        public IEnumerable<string> ListarSaldoConta(string Conta, string dataFim = null)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldoConta(Conta, data: dataFim);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("AtualizarCampoGenerico")]
        public IEnumerable<string> AtualizarCampoGenerico(string campo, int id, string valor)
        {
            Negocio_C.NegociosGenericos gen = new Negocio_C.NegociosGenericos();
            yield return gen.AtualizarCamposTabela("movimentos", campo, id, valor);
        }

    }
}
