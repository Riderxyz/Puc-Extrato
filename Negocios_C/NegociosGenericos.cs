using System.Data.SqlClient;

namespace Negocio_C
{
    public class NegociosGenericos
    {
        Negocio.clBanco banco = new Negocio.clBanco();
        public string AtualizarCamposTabela(string tabela, string campo, int id, string valor)
        {
            string lResult = "";
            string ret = "tabela";
            banco = new Negocio.clBanco();
            banco.parametros.Add(new SqlParameter("id", id));
            banco.parametros.Add(new SqlParameter("nomecampo", campo));
            banco.parametros.Add(new SqlParameter("valorcampo", valor));
            banco.parametros.Add(new SqlParameter("tabela", tabela));
            banco.ExecuteAndReturnData("dbo.[sp_AtualizarCampoGenerico]", ret);
            if (!(banco.tabela == null))
            {
                lResult = banco.GetJsonTabela();
            }
            return lResult;
        }
    }
}
