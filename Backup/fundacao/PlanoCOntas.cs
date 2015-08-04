using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;

namespace NovaEraPortais.vw_PlanoContas
{
    public class basecampos_vw_PlanoContas
    {
        string _conta;
        string _descricao;
        string _contareceita;
        string _descricaoreceita;
        string _conta_mae;
        string _descricaocontamae;
        public string Conta
        {
            get { return _conta; }
            set { _conta = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public string Contareceita
        {
            get { return _contareceita; }
            set { _contareceita = value; }
        }

        public string Descricaoreceita
        {
            get { return _descricaoreceita; }
            set { _descricaoreceita = value; }
        }

        public string Conta_mae
        {
            get { return _conta_mae; }
            set { _conta_mae = value; }
        }

        public string Descricaocontamae
        {
            get { return _descricaocontamae; }
            set { _descricaocontamae = value; }
        }

    }





    public class base_vw_PlanoContas
    {
        DataTable _listavw_planocontas;
        public DataTable ListaVw_planocontas
        {
            get { return _listavw_planocontas; }
            set { _listavw_planocontas = value; }
        }
    }





    public class class_vw_PlanoContas
    {
        List<basecampos_vw_PlanoContas> _linhas;
        public List<basecampos_vw_PlanoContas> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        public void ListaVw_planocontas()
        {
            base_vw_PlanoContas vw_PlanoContas = new base_vw_PlanoContas();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Conta");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("ContaReceita");
            BancoOrigem.Campos.Add("DescricaoReceita");
            BancoOrigem.Campos.Add("conta_mae");
            BancoOrigem.Campos.Add("descricaoContaMae");
            BancoOrigem.Nometabela = "vw_PlanoContas";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.getData();
            Linhas = new List<basecampos_vw_PlanoContas>();
            basecampos_vw_PlanoContas linha = new basecampos_vw_PlanoContas();
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_vw_PlanoContas();
                linha.Conta = dataRow["Conta"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Contareceita = dataRow["ContaReceita"].ToString();
                linha.Descricaoreceita = dataRow["DescricaoReceita"].ToString();
                linha.Conta_mae = dataRow["conta_mae"].ToString();
                linha.Descricaocontamae = dataRow["descricaoContaMae"].ToString();
                Linhas.Add(linha);
            }
        }
    }
}
