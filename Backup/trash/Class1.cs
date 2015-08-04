using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;
using NovaEraPortais.Excel;

namespace NovaEraPortais.view_extrato2
{
    public class basecampos_view_extrato2
    {
        string _expr1;
        int _coordenador;
        string _tipo_projeto;
        string _conta_principal;
        string _descricao;
        string _nome;
        int _projeto;
        int _rubrica;
        string _fatura;
        DateTime _data;
        string _tipo_lancamento;
        Decimal _valor;
        Decimal _receita;
        Decimal _despesa;
        string _historico;
        string _banco;
        string _favorecido;
        string _documento;
        string _pre_lancamento;
        Decimal _saldo;
        int _codigo_lancamento;
        string _conta_mae;
        string _descricaocontamae;
        public string Expr1
        {
            get { return _expr1; }
            set { _expr1 = value; }
        }

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
        }

        public string Tipo_projeto
        {
            get { return _tipo_projeto; }
            set { _tipo_projeto = value; }
        }

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

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public int Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public int Rubrica
        {
            get { return _rubrica; }
            set { _rubrica = value; }
        }

        public string Fatura
        {
            get { return _fatura; }
            set { _fatura = value; }
        }

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string Tipo_lancamento
        {
            get { return _tipo_lancamento; }
            set { _tipo_lancamento = value; }
        }

        public Decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
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

        public string Banco
        {
            get { return _banco; }
            set { _banco = value; }
        }

        public string Favorecido
        {
            get { return _favorecido; }
            set { _favorecido = value; }
        }

        public string Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }

        public string Pre_lancamento
        {
            get { return _pre_lancamento; }
            set { _pre_lancamento = value; }
        }

        public Decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public int Codigo_lancamento
        {
            get { return _codigo_lancamento; }
            set { _codigo_lancamento = value; }
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





    public class base_view_extrato2
    {
        DataTable _listaview_extrato2;
        public DataTable ListaView_extrato2
        {
            get { return _listaview_extrato2; }
            set { _listaview_extrato2 = value; }
        }
    }





    public class class_view_extrato2
    {
        List<basecampos_view_extrato2> _linhas;
        public List<basecampos_view_extrato2> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        void ListaView_extrato2(String parm_coordenador, List<String> _filtro)
        {
            base_view_extrato2 view_extrato2 = new base_view_extrato2();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Expr1");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Tipo_Projeto");
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Nome");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Rubrica");
            BancoOrigem.Campos.Add("Fatura");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Tipo_Lancamento");
            BancoOrigem.Campos.Add("Valor");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Banco");
            BancoOrigem.Campos.Add("Favorecido");
            BancoOrigem.Campos.Add("Documento");
            BancoOrigem.Campos.Add("Pre_lancamento");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Campos.Add("Codigo_Lancamento");
            BancoOrigem.Campos.Add("conta_mae");
            BancoOrigem.Campos.Add("descricaoContaMae");
            BancoOrigem.Nometabela = "view_extrato2";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.getData();
            Linhas = new List<basecampos_view_extrato2>();
            basecampos_view_extrato2 linha = new basecampos_view_extrato2();
            NovaEraPortais.ExportarArquivos.CsvFileWriter csvFile = new ExportarArquivos.CsvFileWriter(BancoOrigem.CaminhoArquivos + "View_extrato2_" + parm_coordenador + ".csv");
            NovaEraPortais.ExportarArquivos.CsvRow row = new NovaEraPortais.ExportarArquivos.CsvRow();
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("");
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_view_extrato2();
                linha.Expr1 = dataRow["Expr1"].ToString();
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                linha.Tipo_projeto = dataRow["Tipo_Projeto"].ToString();
                linha.Conta_principal = dataRow["Conta_Principal"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Nome = dataRow["Nome"].ToString();
                linha.Projeto = Convert.ToInt32(dataRow["Projeto"].ToString());
                linha.Rubrica = Convert.ToInt32(dataRow["Rubrica"].ToString());
                linha.Fatura = dataRow["Fatura"].ToString();
                linha.Data = Convert.ToDateTime(dataRow["Data"].ToString());
                linha.Tipo_lancamento = dataRow["Tipo_Lancamento"].ToString();
                linha.Valor = Convert.ToDecimal(dataRow["Valor"].ToString());
                linha.Receita = Convert.ToDecimal(dataRow["Receita"].ToString());
                linha.Despesa = Convert.ToDecimal(dataRow["Despesa"].ToString());
                linha.Historico = dataRow["Historico"].ToString();
                linha.Banco = dataRow["Banco"].ToString();
                linha.Favorecido = dataRow["Favorecido"].ToString();
                linha.Documento = dataRow["Documento"].ToString();
                linha.Pre_lancamento = dataRow["Pre_lancamento"].ToString();
                linha.Saldo = Convert.ToDecimal(dataRow["Saldo"].ToString());
                linha.Codigo_lancamento = Convert.ToInt32(dataRow["Codigo_Lancamento"].ToString());
                linha.Conta_mae = dataRow["conta_mae"].ToString();
                linha.Descricaocontamae = dataRow["descricaoContaMae"].ToString();
                Linhas.Add(linha);
                csvFile.WriteLine(dataRow["Expr1"].ToString() + ";" + dataRow["Coordenador"].ToString() + ";" + dataRow["Tipo_Projeto"].ToString() + ";" + dataRow["Conta_Principal"].ToString() + ";" + dataRow["Descricao"].ToString() + ";" + dataRow["Nome"].ToString() + ";" + dataRow["Projeto"].ToString() + ";" + dataRow["Rubrica"].ToString() + ";" + dataRow["Fatura"].ToString() + ";" + dataRow["Data"].ToString() + ";" + dataRow["Tipo_Lancamento"].ToString() + ";" + dataRow["Valor"].ToString() + ";" + dataRow["Receita"].ToString() + ";" + dataRow["Despesa"].ToString() + ";" + dataRow["Historico"].ToString() + ";" + dataRow["Banco"].ToString() + ";" + dataRow["Favorecido"].ToString() + ";" + dataRow["Documento"].ToString() + ";" + dataRow["Pre_lancamento"].ToString() + ";" + dataRow["Saldo"].ToString() + ";" + dataRow["Codigo_Lancamento"].ToString() + ";" + dataRow["conta_mae"].ToString() + ";" + dataRow["descricaoContaMae"].ToString() + ";");
                csvFile.WriteRow(row);
            }
            csvFile.Close();
        }
        public void GetListOf_View_extrato2(String parm_coordenador)
        {
            List<String> _filtro = new List<String>();
            ListaView_extrato2(parm_coordenador, _filtro);
        }
        public void GetRangeOf_View_extrato2(String parm_coordenador, String parm_chave, String inicio, String final)
        {
            List<String> _filtro = new List<String>();
            ListaView_extrato2(parm_coordenador, _filtro);
        }
        public void GetUnique_View_extrato2(String parm_coordenador, String parm_chave)
        {
            List<String> _filtro = new List<String>();
            ListaView_extrato2(parm_coordenador, _filtro);
        }
        void GerarDadosExcel_View_extrato2(String parm_coordenador, List<String> _filtro, String NomePlanilha, List<String> _titulos)
        {
            base_view_extrato2 view_extrato2 = new base_view_extrato2();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Expr1");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Tipo_Projeto");
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Nome");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Rubrica");
            BancoOrigem.Campos.Add("Fatura");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Tipo_Lancamento");
            BancoOrigem.Campos.Add("Valor");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Banco");
            BancoOrigem.Campos.Add("Favorecido");
            BancoOrigem.Campos.Add("Documento");
            BancoOrigem.Campos.Add("Pre_lancamento");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Campos.Add("Codigo_Lancamento");
            BancoOrigem.Campos.Add("conta_mae");
            BancoOrigem.Campos.Add("descricaoContaMae");
            BancoOrigem.Nometabela = "view_extrato2";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.getData();
            Linhas = new List<basecampos_view_extrato2>();
            basecampos_view_extrato2 linha = new basecampos_view_extrato2();
            Excel.Excel planilha = new Excel.Excel();
            planilha.Nomeplanilha = NomePlanilha;
            planilha.InicializarWorkBook();
            planilha.NumLinha = 8;
            planilha.InicializarSheet();
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_view_extrato2();
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue(dataRow["Expr1"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(1).SetCellValue(Convert.ToInt32(dataRow["Coordenador"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(2).SetCellValue(dataRow["Tipo_Projeto"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(3).SetCellValue(dataRow["Conta_Principal"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(4).SetCellValue(dataRow["Descricao"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(5).SetCellValue(dataRow["Nome"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(6).SetCellValue(Convert.ToInt32(dataRow["Projeto"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(7).SetCellValue(Convert.ToInt32(dataRow["Rubrica"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(8).SetCellValue(dataRow["Fatura"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(9).SetCellValue(Convert.ToDateTime(dataRow["Data"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(10).SetCellValue(dataRow["Tipo_Lancamento"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(11).SetCellValue(Convert.ToDouble(dataRow["Valor"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(12).SetCellValue(Convert.ToDouble(dataRow["Receita"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(13).SetCellValue(Convert.ToDouble(dataRow["Despesa"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(14).SetCellValue(dataRow["Historico"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(15).SetCellValue(dataRow["Banco"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(16).SetCellValue(dataRow["Favorecido"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(17).SetCellValue(dataRow["Documento"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(18).SetCellValue(dataRow["Pre_lancamento"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(19).SetCellValue(Convert.ToDouble(dataRow["Saldo"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(20).SetCellValue(Convert.ToInt32(dataRow["Codigo_Lancamento"].ToString()));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(21).SetCellValue(dataRow["conta_mae"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(22).SetCellValue(dataRow["descricaoContaMae"].ToString());
            }
            planilha.ExportDataTableToExcel("extrato");
        }
        void _GetExcelOf_View_extrato2(String parm_coordenador, String parmfiltro, String NomePlanilha, String parmtitulos)
        {
            List<String> _filtro = new List<String>();
            List<String> _titulos = new List<String>();
            GerarDadosExcel_View_extrato2(parm_coordenador, _filtro, NomePlanilha, _titulos);
        }
    }
}
