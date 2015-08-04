using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;

namespace NovaEraPortais.Autenticar
{
    public class basecamposcoordenadores
    {
        int _coordenador;

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
        }
        string _projeto;

        public string Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }
        Boolean _conectado;

        public Boolean Conectado
        {
            get { return _conectado; }
            set { _conectado = value; }
        }
    }

    public class Autenticar
    {
        bool _autenticado;

        public bool Autenticado
        {
            get { return _autenticado; }
            set { _autenticado = value; }
        }
        string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        string _pass;

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        DB _bancoorigem;

        public DB Bancoorigem
        {
            get { return _bancoorigem; }
            set { _bancoorigem = value; }
        }

        public List<basecamposcoordenadores> Conectar(string u, string p)
        {
            Bancoorigem = new DB();
            Bancoorigem.conectarBanco();
            List<basecamposcoordenadores> Linhas = new List<basecamposcoordenadores>();
            basecamposcoordenadores linha = new basecamposcoordenadores();
            if (Bancoorigem.Conectado)
            {
                Bancoorigem.Campos = new List<string>();
                Bancoorigem.Campos.Add("Coordenador");
                Bancoorigem.Campos.Add("Nome");
                //Bancoorigem.Campos.Add("Senha");
                Bancoorigem.Nometabela = "vw_Int_Coordenador";
                Bancoorigem.Filtro.Add("Coordenador = '" + u + "' and senha = '" + p + "'");
                Bancoorigem.getData();
                if (Bancoorigem.Tabela.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in Bancoorigem.Tabela.Rows)
                    {
                        linha = new basecamposcoordenadores();
                        linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                        linha.Projeto = dataRow["nome"].ToString();
                        linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                        linha.Conectado = true;
                        Linhas.Add(linha);
                    }
                }
                else
                {
                    linha.Conectado = false;
                    linha.Projeto = "Não encontrei dados para o coordenador "+ u;
                    linha.Coordenador = Convert.ToInt32(u);
                    Linhas.Add(linha);
                }
                
            }
            else
            {
                linha.Conectado = false;
                linha.Projeto = Bancoorigem.MensagemErro;
                linha.Coordenador = Convert.ToInt32(u);
                Linhas.Add(linha);
            }
            return Linhas;
        }   
    }
}
