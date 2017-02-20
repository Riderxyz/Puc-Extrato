using System;
using NPOI.HSSF.UserModel;
using Negocio;
using System.IO;
using System.Data;
using System.Web;
using NPOI.SS.UserModel;

namespace Negocios_C
{
    public class NegocioBancos
    {
        Negocio.clBanco banco = new clBanco();
        public string Listar(string nome, int codigo = -1)
        {
            string lResult = "";

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("nome", nome));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("codigo", codigo));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_BancoListar");
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
