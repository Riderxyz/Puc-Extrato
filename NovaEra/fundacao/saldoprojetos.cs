using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NovaEraPortais.banco;
using NovaEraPortais.ExportarArquivos;
using NovaEraPortais.Excel;

namespace NovaEraPortais.SaldoProjetos
{
    public class basecampos_vw_int_saldosProjetos
    {
        string _projeto;
        int _coordenador;
        Decimal _receita;
        Decimal _despesa;
        Decimal _saldo;

        public Decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }
        public string Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public int Coordenador
        {
            get { return _coordenador; }
            set { _coordenador = value; }
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
    }

    public class base_vw_int_saldosProjetos
    {
        DataTable _listavw_int_saldosprojetos;
        public DataTable ListaVw_int_saldosprojetos
        {
            get { return _listavw_int_saldosprojetos; }
            set { _listavw_int_saldosprojetos = value; }
        }
    }


    public class class_vw_int_saldosProjetos
    {
        List<basecampos_vw_int_saldosProjetos> _linhas;
        public List<basecampos_vw_int_saldosProjetos> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        public void ListaVw_int_saldosprojetos(string coordenador, string _inicio)
        {
            base_vw_int_saldosProjetos vw_int_saldosProjetos = new base_vw_int_saldosProjetos();
            DB BancoOrigem = new DB();
            String comandosql = "";
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Nometabela = "vw_int_saldosProjetos";
            comandosql = comandosql + " SELECT     dbo.Projetos.Projeto, dbo.Projetos.Coordenador, SUM(dbo.Movimentos.Receita) AS Receita, SUM(dbo.Movimentos.Despesa) AS Despesa";
            comandosql = comandosql + " FROM         dbo.Projetos LEFT OUTER JOIN";
            comandosql = comandosql + " dbo.Movimentos ON dbo.Projetos.Codigo = dbo.Movimentos.Projeto";
            comandosql = comandosql + " WHERE (dbo.Projetos.Tipo_Projeto <> 'EC') and (dbo.Movimentos.Data < convert(datetime, '"+_inicio+"',104)) and Coordenador = "+coordenador;
           // comandosql = comandosql + " WHERE Coordenador = "+coordenador;
            comandosql = comandosql + " GROUP BY dbo.Projetos.Projeto, dbo.Projetos.Coordenador";
            BancoOrigem.getData(comandosql.ToString());
            Linhas = new List<basecampos_vw_int_saldosProjetos>();
            basecampos_vw_int_saldosProjetos linha = new basecampos_vw_int_saldosProjetos();

            /*NovaEraPortais.ExportarArquivos.CsvFileWriter csvFile = new ExportarArquivos.CsvFileWriter(BancoOrigem.CaminhoArquivos + "Saldo dos Projetos - Coordenador " + coordenador + ".csv");
            csvFile.WriteLine("FUNDAÇÃO PADRE LEONAL FRANCA");
            csvFile.WriteLine("CONTROLE DE PROJETOS - WEB SALDO DOS PROJETOS Versão 2012");
            csvFile.WriteLine("");
            csvFile.WriteLine("SALDO DOS PROJETOS DO COORDENADOR - " + coordenador + "  EM  " + _inicio);
            csvFile.WriteLine("Projeto;Receitas;Despesas;Saldo");
            csvFile.WriteLine("");*/
            Excel.Excel planilha = new Excel.Excel();
            planilha.Nomeplanilha = "SaldoProjetos";
            planilha.InicializarWorkBook();
            planilha.NumLinha = 13;
            planilha.InicializarSheet();

            planilha.Sheet.GetRow(6).GetCell(0).SetCellValue("Saldo para movimento até o dia " + _inicio);
                 

            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_vw_int_saldosProjetos();
                linha.Projeto = dataRow["Projeto"].ToString();
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                linha.Receita = Convert.ToDecimal(dataRow["Receita"].ToString());
                linha.Despesa = Convert.ToDecimal(dataRow["Despesa"].ToString());
                linha.Saldo = Convert.ToDecimal(dataRow["Receita"].ToString()) - Convert.ToDecimal(dataRow["Despesa"].ToString());
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue(linha.Projeto);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(1).SetCellValue(Convert.ToDouble(linha.Receita));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(2).SetCellValue(Convert.ToDouble(linha.Despesa));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(3).SetCellValue(Convert.ToDouble(linha.Saldo));
                planilha.NovaLinha();
                Linhas.Add(linha);
               // csvFile.WriteLine(dataRow["Projeto"].ToString() + ";" + String.Format("{0:F2}", linha.Receita) + ";" + String.Format("{0:F2}", linha.Despesa) + ";" + String.Format("{0:F2}", linha.Saldo));
            }
           // csvFile.Close();
            planilha.ExportDataTableToExcel(coordenador);
        }
    }
}

