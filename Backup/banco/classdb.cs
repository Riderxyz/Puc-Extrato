using System;
using System.Data;
using System.Data.SqlClient;
using NovaEraPortais;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Web;

namespace NovaEraPortais.banco
{
    public enum tipos_Campos { Integer = 1, texto, data, dinheiro };
    public class basecampos
    {

        string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        tipos_Campos _tipo;

        public tipos_Campos Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        string _conteudo;

        public string Conteudo
        {
            get { return _conteudo; }
            set { _conteudo = value; }
        }

        bool _campoChave;

        public bool CampoChave
        {
            get { return _campoChave; }
            set { _campoChave = value; }
        }

        bool _identity = false;

        public bool Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        public string ValorFormatado
        {
            get
            {
                string retorno = "";
                switch (_tipo)
                {
                    case tipos_Campos.texto:
                        retorno = "'" + _conteudo.Trim() + "'";
                        break;
                    case tipos_Campos.dinheiro:
                        retorno = "CAST(" + _conteudo.Trim() + " AS money) ";
                        break;
                    case tipos_Campos.data:
                        retorno = "CONVERT(datetime, '" + _conteudo.Trim() + "', 104)";
                        break;
                    case tipos_Campos.Integer:
                        retorno = _conteudo.Trim();
                        break;
                }
                return retorno;
            }
        }


    }


    public class DB
    {
        #region Propriedades
        private bool _conectado;
        private string _mensagemErro;
        private SqlConnection _conexao;
        string _fieldclause = "";
        string _nometabela;
        string _complementocomando;

        public string Complementocomando
        {
            get { return " " + _complementocomando + " "; }
            set { _complementocomando = value; }
        }

        List<String> _filtro;

        List<NovaEraPortais.banco.basecampos> _campsoinsert;

        public List<NovaEraPortais.banco.basecampos> Campsoinsert
        {
            get { return _campsoinsert; }
            set { _campsoinsert = value; }
        }

        List<String> _camposupdate;

        public List<String> Camposupdate
        {
            get { return _camposupdate; }
            set { _camposupdate = value; }
        }


        public string CaminhoArquivos
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("/ArquivosCoordenadores") + "//";
            }

        }

        public List<String> Filtro
        {
            get
            {
                if (_filtro == null)
                {
                    _filtro = new List<string>();
                }
                return _filtro;
            }
            set { _filtro = value; }
        }

        public string Nometabela
        {
            get { return _nometabela; }
            set { _nometabela = value; }
        }

        public string Fieldclause
        {
            get { return _fieldclause; }
            set { _fieldclause = value; }
        }

        public SqlConnection Conexao
        {
            get { return _conexao; }
            set { _conexao = value; }
        }

        public string MensagemErro
        {
            get { return _mensagemErro; }
            set { _mensagemErro = value; }
        }

        public bool Conectado
        {
            get { return _conectado; }
            set { _conectado = value; }
        }

        DataTable _tabela;
        List<string> _campos;

        public List<string> Campos
        {
            get { return _campos; }
            set { _campos = value; }
        }

        public DataTable Tabela
        {
            get { return _tabela; }
            set { _tabela = value; }
        }

        string TextoCamposInsert
        {
            get
            {
                string retorno = "(";
                foreach (basecampos campo in _campsoinsert)
                {
                    if (!campo.Identity)
                    {
                        retorno = retorno + campo.Nome + ",";
                    }
                }

                retorno = retorno.Substring(0, retorno.Length - 1) + ")";
                return retorno;
            }
        }

        public tipos_Campos TipoCampo(string tipo)
        {
            tipos_Campos retorno = tipos_Campos.texto;
            switch (tipo.ToLower())
            {
                case "datetime":
                    retorno = tipos_Campos.data;
                    break;
                case "int32":
                    retorno = tipos_Campos.Integer;
                    break;
                case "decimal":
                    retorno = tipos_Campos.dinheiro;
                    break;
                default:
                    retorno = tipos_Campos.texto;
                    break;

            }
            return retorno;
        }

        string TextoCamposUpdate
        {
            get
            {
                List<string> retorno = new List<string>();
                retorno.Add("(");
                foreach (basecampos campo in _campsoinsert)
                {
                    retorno.Add(campo.Nome + " = " + campo.ValorFormatado);
                    retorno.Add(",");
                }
                retorno[retorno.Count - 1] = ")";
                return retorno.ToString();
            }
        }

        string TextoValuesInsert
        {
            get
            {
                List<string> retorno = new List<string>();
                retorno.Add("(");
                foreach (basecampos campo in _campsoinsert)
                {
                    retorno.Add(campo.ValorFormatado);
                    retorno.Add(",");
                }
                retorno[retorno.Count - 1] = ")";
                return String.Join(" ", retorno.ToArray());
            }
        }

        string TextoClausulaWhere
        {
            get
            {
                List<string> retorno = new List<string>();
                retorno.Add(" WHERE ");
                foreach (basecampos campo in _campsoinsert)
                {
                    if (campo.CampoChave)
                    {
                        retorno.Add(campo.Nome + " = " + campo.ValorFormatado);
                        retorno.Add("  ,  ");
                    }
                }
                retorno[retorno.Count - 1] = ")";
                return retorno.ToString();
            }
        }
        #endregion

        #region Montagem de comandos do Banco

        public String ComandoInsert()
        {
            List<string> _comando = new List<string>();
            _comando.Add(" INSERT INTO ");
            _comando.Add(_nometabela + "  ");
            _comando.Add(TextoCamposInsert);
            _comando.Add(" VALUES ");
            _comando.Add(TextoValuesInsert);
            _comando.Add(";");
            return String.Join(" ", _comando.ToArray());
        }

        string ComandoUpdate()
        {
            List<string> _comando = new List<string>();
            _comando.Add(" UPDATE ");
            _comando.Add(_nometabela + "  ");
            _comando.Add(" SET ");
            _comando.Add(TextoCamposInsert);
            _comando.Add(" VALUES ");
            _comando.Add(TextoCamposUpdate);
            _comando.Add(TextoClausulaWhere);
            _comando.Add(";");
            return _comando.ToString();
        }



        SqlConnectionStringBuilder CriarConexao()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "139.82.24.10";
            //scsb.DataSource = "192.168.178.140\\novaera";
            scsb.InitialCatalog = "FPLF";
            scsb.IntegratedSecurity = false; // set to true is using Windows Authentication
            scsb.UserID = "sa";
            scsb.Password = "Sdfplf98#";
            return scsb;
        }

        #endregion

        public void LimparFiltro()
        {
            _filtro.Clear();
        }
        public void conectarBanco()
        {
            SqlConnectionStringBuilder scsb = CriarConexao();
            SqlConnection conn = new SqlConnection(scsb.ConnectionString);
            try
            {
                conn.Open();
                _conexao = conn;
                _conectado = true;
            }
            catch (SqlException loginError)
            {
                _mensagemErro = loginError.Message;
                _conexao = new SqlConnection();
                _conectado = false;
            }
        }

        void MontarCampos()
        {
            foreach (string campo in _campos) // Loop through List with foreach
            {
                _fieldclause += campo + ",";
            }
            _fieldclause = _fieldclause.Substring(0, _fieldclause.Length - 1);
        }

        string MontarClausulaSelect()
        {
            string ClausulaSelect = "";
            ClausulaSelect = "SELECT " + _fieldclause + " FROM " + _nometabela + "  " + MontarFiltro() + _complementocomando;
            return ClausulaSelect;
        }

        string MontarClausulaSelect(string ClausulaSelect)
        {
            return ClausulaSelect;
        }

        string MontarFiltro()
        {
            string stringfiltro = "";
            if (_filtro.Count > 0)
            {
                stringfiltro = " Where ";
                foreach (string clausula in _filtro)
                    stringfiltro = stringfiltro + clausula + "  ";
            }
            return stringfiltro;
        }
        public void getData()
        {
            conectarBanco();
            if (Conectado)
            {
                SqlCommand cmd = new SqlCommand();
                MontarCampos();
                //                cmd.CommandText = "SELECT "+Fieldclause+" FROM "+ Nometabela;
                cmd.Connection = Conexao;
                System.Data.SqlClient.SqlCommand _SqlCommand = new System.Data.SqlClient.SqlCommand(MontarClausulaSelect(), Conexao);

                System.Data.SqlClient.SqlDataAdapter _SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = _SqlCommand;

                try
                {
                    Tabela = new System.Data.DataTable();
                    Tabela.TableName = Nometabela;
                    _SqlDataAdapter.Fill(_tabela);
                }
                catch (Exception _Exception)
                {
                    // Error occurred while trying to execute reader
                    // send error message to console (change below line to customize error handling)
                    MensagemErro = _Exception.Message;
                }
            }
        }

        public void getData(string _comando)
        {
            conectarBanco();
            if (Conectado)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conexao;
                System.Data.SqlClient.SqlCommand _SqlCommand = new System.Data.SqlClient.SqlCommand(_comando, Conexao);

                System.Data.SqlClient.SqlDataAdapter _SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = _SqlCommand;
                try
                {
                    _tabela = new System.Data.DataTable();
                    _tabela.TableName = Nometabela;
                    _SqlDataAdapter.Fill(_tabela);
                }
                catch (Exception _Exception)
                {
                    // Error occurred while trying to execute reader
                    // send error message to console (change below line to customize error handling)
                    MensagemErro = _Exception.Message;
                }
            }
        }
        public void GravarRegistro()
        {
            conectarBanco();
            if (Conectado)
            {
                SqlTransaction myTrans;

                // Start a local transaction
                myTrans = Conexao.BeginTransaction(IsolationLevel.ReadCommitted, "InsertPadrao");
                // Must assign both transaction object and connection
                // to Command object for a pending local transaction


                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = Conexao;
                //cmd.Transaction = myTrans;
                try
                {
                    System.Data.SqlClient.SqlCommand _SqlCommand = new System.Data.SqlClient.SqlCommand(ComandoInsert(), Conexao);
                    _SqlCommand.Transaction = myTrans;
                    _SqlCommand.ExecuteNonQuery();
                    myTrans.Commit();
                }
                catch (Exception e)
                {
                    try
                    {
                        myTrans.Rollback("InsertPadrao");
                    }
                    catch (SqlException ex)
                    {
                        if (myTrans.Connection != null)
                        {
                            MensagemErro = ex.Message;
                        }
                    }

                    MensagemErro = e.Message;
                }
                finally
                {
                    Conexao.Close();
                }
            }
        }

    }
}
