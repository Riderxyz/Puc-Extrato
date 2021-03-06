﻿using System;
using NPOI.HSSF.UserModel;
using Negocio;
using System.IO;
using System.Data;
using System.Web;
using NPOI.SS.UserModel;
using Puc.Negocios_C;
using System.Data.SqlClient;

namespace Negocios_C
{
    public class Usuarios
    {
        Puc.Negocios_C.logErro log = new logErro();
        Negocio.clBanco banco = new clBanco();
        NegocioUtil util = new NegocioUtil();

        public string ValidarUsuarioCoordenador(string _usuario, string _senha)
        {
            string lResult = null;
            clBanco banco = new clBanco();
            banco.parametros.Add(new SqlParameter("coordenador", _usuario));
            banco.parametros.Add(new SqlParameter("pass", _senha));

            banco.ExecuteAndReturnData("sp_internet_ValidarUsuario", "tabUsuario");
            if (((banco.tabela != null)))
            {
                if ((banco.tabela.Rows.Count > 0))
                {
                    //  Mapear(banco.tabela)
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }



        public string Autenticar(string usuario, string pass)
        {
            
            string lResult = "";
            //pass = util.decripto(pass);
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("usuario", usuario));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("pass", pass));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_UsuariosAutenticar", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string Incluir(string usuario, string nome, int idGrupo)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("LoginName", usuario));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("fullname", nome));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idGrupo", idGrupo));

            banco.ExecuteAndReturnData("sp_CtrlUsuarios_UsuariosInsert", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Excluir(int idUsuario)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", idUsuario));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_UsuariosDelete", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Atualizar(int id, string usuario, string nome, int idGrupo)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("LoginName", usuario));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("fullname", nome));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idGrupo", idGrupo));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_UsuariosUpdate", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string Listar(string nome)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("nome", nome));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_UsuariosListar", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string SetarSenhaInicial(int id, string pass)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idusuario", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("novasenha", pass));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_setarSenhaInicial", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string AlterarSenha(int id, string senhaatual, string novasenha)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("idusuario", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("senhaAtual", senhaatual));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("novasenha", novasenha));
            banco.ExecuteAndReturnData("sp_CtrlUsuarios_AlterarSenha");
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
