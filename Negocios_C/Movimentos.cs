using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace Puc.Negocios_C
{

    public class Movimentos
    {
        Puc.Negocios_C.logErro log = new logErro();
        Negocio.clBanco banco = new clBanco();
        public string Listar(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), string dataInicio = default(string), string dataFim = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            if (projeto != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            }
            if (conta != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            }
            if (coordenador != default(int))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("coordenador", coordenador));
            }
            if (rubrica != default(int))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", rubrica));
            }
            if (historico != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            }
            if (dataInicio != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataInicio", dataInicio));
            }
            if (dataFim != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataFim", dataFim));
            }
            #endregion
            try
            {
                int a = 1;
                a = a / 0;
            }
            catch (Exception ex)
            {
                log.GravarLog(ex);
            }

            banco.ExecuteAndReturnData("sp_CtrlProjetos_MovimentosLista", "tabmovimento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string Incluir(DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosIncluir]", "tabcoordenador");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;


        }
        public string Atualizar(int id, DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string lote = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabmovimento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string ExecutarPagamento(int id, DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string lote = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabpagamento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string AtualizarPagamento(int id, DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string lote = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabpagamento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string ListarSaldoConta(string conta, string data = default(string))
        {
            string lResult = "";
            conta = conta.Substring(0, conta.Length - conta.IndexOf("."));
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            if (data != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_SaldosContaListar]", "tabsaldoconta");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;

            return lResult;
        }
    }
}
