using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;


namespace NovaEraPortais.fplf.bases
{
    public class basecamposcoordenadoresprojetos
    {
        int _coordenador;

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
        }
        string _nomeprojeto;

        public string Nomeprojeto
        {
            get { return _nomeprojeto; }
            set { _nomeprojeto = value; }
        }
        string _contaprincipal;

        public string Contaprincipal
        {
            get { return _contaprincipal; }
            set { _contaprincipal = value; }
        }
        string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        int _codigo;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
    }

    public class basecorrdenadorprojetos
    {
        DataTable _listaprojetoscoordenador;

        public DataTable Listaprojetoscoordenador
        {
            get { return _listaprojetoscoordenador; }
            set { _listaprojetoscoordenador = value; }
        }

        int _coordenador;

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
        }

    }

    public class CoordenadorProjetos
    {
        List<basecamposcoordenadoresprojetos> _linhas;

        public  List<basecamposcoordenadoresprojetos> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }

        public void listaProjetos()
        {
            basecorrdenadorprojetos coordenador = new basecorrdenadorprojetos();
            DB BancoFPLF = new DB();
            BancoFPLF.Campos = new List<string>();
            BancoFPLF.Campos.Add("projeto");
            BancoFPLF.Campos.Add("codigo");
            BancoFPLF.Nometabela = "VIEW_projetos_e_cordenadores";
            BancoFPLF.Filtro.Add("Coordenador = 23233");
            BancoFPLF.getData();
            Linhas = new List<basecamposcoordenadoresprojetos>();
            basecamposcoordenadoresprojetos linha = new basecamposcoordenadoresprojetos();
            foreach (DataRow dataRow in BancoFPLF.Tabela.Rows)
            {
                linha = new basecamposcoordenadoresprojetos();
                linha.Nome = dataRow["codigo"].ToString();
                linha.Nomeprojeto = dataRow["Projeto"].ToString();
                Linhas.Add(linha);
            }
            //return linhas;
        }
    }
}
