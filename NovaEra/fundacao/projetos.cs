using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;

    namespace NovaEraPortais.Projetos
{
    public class basecampos_Projetos
    {
        int _codigo;
        string _projeto;
        string _nome;
        string _descricao;
        DateTime _inicio;
        int _coordenador;
        string _conta_principal;
        string _tipo_projeto;
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public DateTime Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
        }

        public string Conta_principal
        {
            get { return _conta_principal; }
            set { _conta_principal = value; }
        }

        public string Tipo_projeto
        {
            get { return _tipo_projeto; }
            set { _tipo_projeto = value; }
        }

    }


    public class base_Projetos
    {
        DataTable _listaprojetos;
        public DataTable ListaProjetos
        {
            get { return _listaprojetos; }
            set { _listaprojetos = value; }
        }
    }





    public class class_Projetos
    {
        List<basecampos_Projetos> _linhas;
        public List<basecampos_Projetos> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        public void ListaProjetos(string Coordenador)
        {
            base_Projetos Projetos = new base_Projetos();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Codigo");
            BancoOrigem.Nometabela = "VIEW_projetos_e_cordenadores";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro.Add(" Coordenador = " + Coordenador);
            
            BancoOrigem.getData();
            Linhas = new List<basecampos_Projetos>();
            basecampos_Projetos linha = new basecampos_Projetos();
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_Projetos();
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                linha.Projeto = dataRow["Projeto"].ToString();
                linha.Codigo = Convert.ToInt32(dataRow["Codigo"].ToString());
                Linhas.Add(linha);
            }
        }

        public void ListaProjetosFull(string Coordenador)
        {
            base_Projetos Projetos = new base_Projetos();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Codigo");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Nome");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Inicio");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Tipo_Projeto");
            BancoOrigem.Nometabela = "Projetos";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro.Add(" Coordenador = " + Coordenador);
            BancoOrigem.getData();
            Linhas = new List<basecampos_Projetos>();
            basecampos_Projetos linha = new basecampos_Projetos();
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_Projetos();
                linha.Codigo = Convert.ToInt32(dataRow["Codigo"].ToString());
                linha.Projeto = dataRow["Projeto"].ToString();
                linha.Nome = dataRow["Nome"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Inicio = Convert.ToDateTime(dataRow["Inicio"].ToString());
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                linha.Conta_principal = dataRow["Conta_Principal"].ToString();
                linha.Tipo_projeto = dataRow["Tipo_Projeto"].ToString();
                Linhas.Add(linha);
            }
        }

        public string GravarProjeto(string dadosgravar)
        {
            string[] words = dadosgravar.Split(';');
            DB BancoOrigem = new DB();
            basecampos camposInsert = new basecampos();
            BancoOrigem.Campsoinsert = new List<basecampos>();
            for (int i = 0; i <= words.Count() - 1;i+=3)
            {
                camposInsert = new basecampos();
                camposInsert.Nome = words[i];
                camposInsert.Conteudo = words[i+1];
                camposInsert.Tipo = tipos_Campos.texto;
                BancoOrigem.Campsoinsert.Add(camposInsert);
            }
            return  BancoOrigem.ComandoInsert();

        }

    }
}

