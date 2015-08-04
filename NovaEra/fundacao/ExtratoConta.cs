using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;
using System.Net;
using System.IO;
using System.Web.Hosting;
using NovaEraPortais.Excel;

namespace NovaEraPortais.View_Extrato2
{
    public class basecampos_View_Extrato2
    {
        string _conta_principal;
        string _descricao;
        DateTime _data;
        Decimal _receita;
        Decimal _despesa;
        string _historico;
        Decimal _saldo;
        public string Conta_principal
        {
            get { return _conta_principal; }
            set { _conta_principal = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Decimal Receita
        {
            get { return _receita; }
            set { _receita = value; }
        }

        public Decimal Despesa
        {
            get { return _despesa; }
            set { _despesa = value; }
        }

        public string Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }

        public Decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

    }





    public class base_View_Extrato2
    {
        DataTable _listaview_extrato2;
        public DataTable ListaView_extrato2
        {
            get { return _listaview_extrato2; }
            set { _listaview_extrato2 = value; }
        }
    }

    public class class_View_Extrato2
    {
        List<basecampos_View_Extrato2> _linhas;
        public List<basecampos_View_Extrato2> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        void ListaView_extrato2(String parm_coordenador, List<String> _filtro, string titulos)
        {
            base_View_Extrato2 View_Extrato2 = new base_View_Extrato2();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Nometabela = "View_Extrato2";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.getData();
            Linhas = new List<basecampos_View_Extrato2>();
            basecampos_View_Extrato2 linha = new basecampos_View_Extrato2();
            /*NovaEraPortais.ExportarArquivos.CsvFileWriter csvFile = new ExportarArquivos.CsvFileWriter(BancoOrigem.CaminhoArquivos + parm_coordenador + ".csv");
            NovaEraPortais.ExportarArquivos.CsvRow row = new NovaEraPortais.ExportarArquivos.CsvRow();
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("");
            csvFile.WriteLine("CONTA;Histórico;Data;Receitas;Despesas");
            csvFile.WriteLine("");*/
            Excel.Excel planilha = new Excel.Excel();
            planilha.Nomeplanilha = "ExtratoContas";
            planilha.InicializarWorkBook();
            planilha.NumLinha = 13;
            planilha.InicializarSheet();

            List<String> _titulos = new List<String>();
            string[] namesArray = titulos.Split(';');
            _titulos.AddRange(namesArray);

            planilha.Sheet.GetRow(5).GetCell(0).SetCellValue(_titulos[0]);
            planilha.Sheet.GetRow(6).GetCell(0).SetCellValue(_titulos[1]);

            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_View_Extrato2();
                linha.Conta_principal = dataRow["Conta_Principal"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Data = Convert.ToDateTime(dataRow["Data"].ToString());
                linha.Receita = Convert.ToDecimal(dataRow["Receita"].ToString());
                linha.Despesa = Convert.ToDecimal(dataRow["Despesa"].ToString());
                linha.Historico = dataRow["Historico"].ToString();
                linha.Saldo = Convert.ToDecimal(dataRow["Saldo"].ToString());
                Linhas.Add(linha);
                //csvFile.WriteLine(dataRow["Conta_Principal"].ToString() + dataRow["Historico"].ToString() + ";" + String.Format("{0:dd/MM/yyyy}", linha.Data) + ";" + String.Format("{0:F2}", linha.Receita) + ";" + String.Format("{0:F2}", linha.Despesa) + ";" );
                //csvFile.WriteRow(row);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue(linha.Historico);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(1).SetCellValue(linha.Descricao);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(2).SetCellValue(linha.Data);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(3).SetCellValue(Convert.ToDouble(linha.Receita));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(4).SetCellValue(Convert.ToDouble(linha.Despesa));
                planilha.NovaLinha();

            }
            //csvFile.Close();
            planilha.ExportDataTableToExcel(parm_coordenador);
        }
        public void GetListOf_View_extrato2(String parm_coordenador)
        {
            List<String> _filtro = new List<String>();
            ListaView_extrato2(parm_coordenador, _filtro,"");
        }
        public void GetRangeOf_View_extrato2(String parm_coordenador, String parm_chave, String inicio, String final, String titulos)
        {
            List<String> _filtro = new List<String>();
            _filtro.Add("conta_mae = '"+parm_chave+"' and ");
            _filtro.Add("( ( data >= convert(datetime, '"+inicio+"',104) and " );
            _filtro.Add(" data <= convert(datetime, '" + final + "',104) ) ) ");
            ListaView_extrato2(parm_coordenador, _filtro, titulos);
        }
        public void GetUnique_View_extrato2(String parm_coordenador, String parm_chave, String titulos)
        {
            List<String> _filtro = new List<String>();
            ListaView_extrato2(parm_coordenador, _filtro, titulos);
        }
    }
}
