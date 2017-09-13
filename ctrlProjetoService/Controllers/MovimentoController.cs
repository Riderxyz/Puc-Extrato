using Cors.ConfigProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Movimentos")]
    public class MovimentoController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("buscarfatura")]
        public IEnumerable<string> buscarfatura(string fatura, int id)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.buscarfatura(fatura, id);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("SaldoPorRubricaCamarao")]
        public IEnumerable<string> SaldoPorRubricaCamarao(string rubrica, string dtInicio, string dtFim)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.SaldoPorRubricaCamarao(rubrica: Convert.ToInt32(rubrica), dtInicio: dtInicio, dtFim: dtFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelExtratoPorRubrica")]
        public IEnumerable<string> GerarExcelExtratoPorRubrica(string rubrica, string dtInicio, string dtFim, int projeto = -1)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            if (projeto != -1)
                yield return movimento.GerarExcelExtratoRubrica(projeto: projeto, rubrica: Convert.ToInt32(rubrica), dtInicio: dtInicio, dtFim: dtFim);
            else
                yield return movimento.GerarExcelExtratoRubrica(rubrica: Convert.ToInt32(rubrica), dtInicio: dtInicio, dtFim: dtFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelSaldoProjeto")]
        public IEnumerable<string> GerarExcelSaldoProjeto(string Conta, string data)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelSaldoProjeto(conta: Conta, data: data);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelSaldoRubricas")]
        public IEnumerable<string> GerarExcelSaldoRubricas(string data, string conta, int projeto)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelSaldoRubrica(data, conta, projeto);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarSaldosRubricas")]
        public IEnumerable<string> ListarSaldosRubricas(string data, string conta, int projeto)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldosProjetosRubricas(conta: conta, data: data, projeto: projeto);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarSaldosProjetos")]
        public IEnumerable<string> ListarSaldosProjetos(string data)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldosProjetos(data);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("SaldosProjeto")]
        public IEnumerable<string> SaldosProjeto(string data, int projeto)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.SaldosProjeto(data: data, projeto: projeto);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarSaldosProjetos")]
        public IEnumerable<string> ListarSaldosProjetos(string Conta, string data)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldosProjetos(conta: Conta, data: data);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarExtratoProjeto")]
        public IEnumerable<string> ListarExtratoProjeto(string CodigoProjeto, string dataInicio, string dataFim, bool FiltrarDataAlteracao = false)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            //yield return movimento.Listar(CodigoProjeto, dataInicio: dataInicio, dataFim: dataFim, FiltrarDataAlteracao: FiltrarDataAlteracao, incluirSaldo: false );
            yield return movimento.ListarExtratoProjeto(Convert.ToInt32(CodigoProjeto), dataInicio: dataInicio, dataFim: dataFim);//, FiltrarDataAlteracao: FiltrarDataAlteracao);//, incluirSaldo: false);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelExtrato")]
        public IEnumerable<string> GerarExcelExtrato(int idprojeto, string dataInicio, string dataFim)
        {
            if (dataFim.Length < 8)
                dataFim = null;

            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelExtrato(idprojeto, dataInicio, dataFim);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarListagemPagamentos")]
        public IEnumerable<string> GerarListagemPagamentos(string lote, string dataFim)
        {
            if (dataFim.Length < 8)
                dataFim = null;

            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarListagemPagamentos(lote, dataFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarPagamentos")]
        public IEnumerable<string> ListarPagamentos(string lote, string dataFim)
        {
            if (dataFim.Length < 8)
                dataFim = null;

            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarPagamentos(lote, dataFim);
        }


        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarPagamentosPorAno")]
        public IEnumerable<string> ListarPagamentosPorAno(int ano)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarPagamentosPorAno(ano);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarExtratoPorRubrica")]
        public IEnumerable<string> ListarExtratoPorRubrica(int rubrica, string dtInicio, string dtFim, int projeto = -1)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            if (projeto != -1)
                yield return movimento.Listar(projeto: projeto.ToString(), dataInicio: dtInicio, dataFim: dtFim, rubrica: rubrica);
            else
                yield return movimento.Listar(dataInicio: dtInicio, dataFim: dtFim, rubrica: rubrica);
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
        public IEnumerable<string> Atualizar(Int64 id, string projeto, string historico, double? receita = null, double? despesa = null, int? rubrica = null, string codbanco = default(string), string tipo_lancamento = default(string), string fatura = default(string), int? lote = -1, DateTime? data = null)
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

        #region Antigo FlexBuilder
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarContaMae")]
        public IEnumerable<string> ListarContaMae(int coordenador)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarContaMae(coordenador);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarMovimentosContaFlexBuilder")]
        public IEnumerable<string> ListarMovimentosContaFlexBuilder(string datainicial, string datafinal, string conta)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarMovimentosContaFlexBuilder(datainicial, datafinal, conta);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarSaldoContaFlexBuilder")]
        public IEnumerable<string> ListarSaldoContaFlexBuilder(string data, int coordenador)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarSaldoContaFlexBuilder(coordenador, data);
        }
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("ListarMovimentoProjetosFlexBuilder")]
        public IEnumerable<string> ListarMovimentoProjetosFlexBuilder(int projeto, string dataInicio, string dataFim)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.ListarMovimentoProjetosFlexBuilder(projeto, dataInicio, dataFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelMovimentoProjetosFlexBuilder")]
        public IEnumerable<string> GerarExcelMovimentoProjetosFlexBuilder(int projeto, string dataInicio, string dataFim)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelMovimentoProjetosFlexBuilder(projeto, dataInicio, dataFim);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelSaldoContaFlexBuilder")]
        public IEnumerable<string> GerarExcelSaldoContaFlexBuilder(int coordenador, string data)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelSaldoContaFlexBuilder(coordenador, data);
        }


        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarExcelMovimentoContasFlexBuilder")]
        public IEnumerable<string> GerarExcelMovimentoContasFlexBuilder(string datainicial, string datafinal, string conta)
        {
            Puc.Negocios_C.Movimentos movimento = new Puc.Negocios_C.Movimentos();
            yield return movimento.GerarExcelMovimentoContasFlexBuilder(datainicial, datafinal, conta);
        }
        #endregion

    }
}
