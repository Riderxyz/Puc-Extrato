using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;

namespace NovaEraPortais.vw_contamae
{
    public class basecampos_vw_contamae
    {
        string _conta_mae;
        string _descricaocontamae;
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





    public class base_vw_contamae
    {
        DataTable _listavw_contamae;
        public DataTable ListaVw_contamae
        {
            get { return _listavw_contamae; }
            set { _listavw_contamae = value; }
        }
    }





    public class class_vw_contamae
    {
        List<basecampos_vw_contamae> _linhas;
        public List<basecampos_vw_contamae> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        void ListaVw_contamae(String parm_coordenador, List<String> _filtro)
        {
            base_vw_contamae vw_contamae = new base_vw_contamae();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("conta_mae");
            BancoOrigem.Campos.Add("descricaoContaMae");
            BancoOrigem.Nometabela = "vw_contamae";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.Complementocomando = "order by descricaoContaMae";
            BancoOrigem.getData();
            Linhas = new List<basecampos_vw_contamae>();
            basecampos_vw_contamae linha = new basecampos_vw_contamae();
            NovaEraPortais.ExportarArquivos.CsvFileWriter csvFile = new ExportarArquivos.CsvFileWriter(BancoOrigem.CaminhoArquivos + "Vw_contamae_" + parm_coordenador + ".csv");
            NovaEraPortais.ExportarArquivos.CsvRow row = new NovaEraPortais.ExportarArquivos.CsvRow();
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("");
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_vw_contamae();
                linha.Conta_mae = dataRow["conta_mae"].ToString();
                linha.Descricaocontamae = dataRow["descricaoContaMae"].ToString();
                Linhas.Add(linha);
                csvFile.WriteLine(dataRow["conta_mae"].ToString() + ";" + dataRow["descricaoContaMae"].ToString() + ";");
                csvFile.WriteRow(row);
            }
            csvFile.Close();
        }
        public void GetListOf_Vw_contamae(String parm_coordenador)
        {
            List<String> _filtro = new List<String>();
            ListaVw_contamae(parm_coordenador, _filtro);
        }
        public void GetRangeOf_Vw_contamae(String parm_coordenador, String parm_chave, String inicio, String final)
        {
            List<String> _filtro = new List<String>();
            ListaVw_contamae(parm_coordenador, _filtro);
        }
        public void GetUnique_Vw_contamae(String parm_coordenador, String parm_chave)
        {
            List<String> _filtro = new List<String>();
            ListaVw_contamae(parm_coordenador, _filtro);
        }
    }
}
