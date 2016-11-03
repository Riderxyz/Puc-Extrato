using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace Puc.Negocios_C
{
    public class SegurancaNegocios
    {
        Puc.Negocios_C.logErro log = new logErro();
        Negocio.clBanco banco = new clBanco("DbSeguranca");
        
        public string loginvalidar(string loginname, string senha)
        {

            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("loginname", loginname));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("pass", senha));
            #endregion
            banco.ExecuteAndReturnData("sp_UsuarioValidar", "tab");
            return banco.GetJsonTabela();
        }

        public string modulosListarporusuario(int idusuario)
        {
            return "";
        }

        public string senhaAlterar(int usuario, string senhaAtual, string novaSenha)
        {
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idusuario", usuario ));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("senhaatual", senhaAtual));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("novaSenha", novaSenha));
            #endregion
            banco.ExecuteAndReturnData("sp_setarsenha", "tabela");
            return banco.GetJsonTabela();
        }

        public string novo(string usuario, string nome, string email)
        {
            return "";
        }

        public string logout(string usuario)
        {
            return "";
        }

        public string senhasetarinicial(string usuario, string senha)
        {
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idusuario", usuario));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("novaSenha", senha));
            #endregion
            banco.ExecuteAndReturnData("sp_setarSenhaInicial", "tabela");
            return banco.GetJsonTabela();
        }

        public String grupoincluir(string nome, string destcricao)
        {
            return "";
        }

        public string grupoListar(string nome)
        {
            return "";
        }
    }

}
