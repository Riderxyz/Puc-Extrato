using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Puc.Negocios_C;

namespace Negocios_C
{
    public class Empresas
    {
        Puc.Negocios_C.logErro log = new logErro();
        Negocio.clBanco banco = new clBanco();
        public string Listar(string nome = default(string))
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("nome", nome));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_EmpresaListar", "tabempresas");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Incluir(string nome, Int32 banco_num, string CNPJ = null, string Agencia = null, string conta = null, string optanteSimples = null, string Observacao = null, string ISS = null, string Cidade = null)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("nome", nome));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("cnpj", CNPJ));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", banco_num));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("agencia", Agencia));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("optanteSimples", optanteSimples));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("Observacao", Observacao));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("iss", ISS));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("cidade", Cidade));
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_EmpresasIncluir]", "tabempresas");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Atualizar(Int64 id, string nome = null , string CNPJ = null, int banco_num = 0, string Agencia = null, string conta = null, string optanteSimples = null, string Observacao = null, string ISS = null, string Cidade = null)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("nome", nome));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("cnpj", CNPJ));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", banco_num));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("agencia", Agencia));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("optanteSimples", optanteSimples));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("Observacao", Observacao));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("iss", ISS));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("cidade", Cidade));
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_EmpresasAtualizar]", "tabempresa");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Excluir(Int64 id)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_EmpresasExcluir]", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
    }
}
