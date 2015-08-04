using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using NovaEraPortais.banco;
using NovaEraPortais.Excel;

namespace NovaEraPortais.Movimentos
{
    public class basecampos_view_extrato
    {
        int _codigo_lancamento;
        int _projeto;
        int _rubrica;
        string _fatura;
        DateTime _data;
        Decimal _valor;
        Decimal _saldo;
        Decimal _despesa;
        string _pre_lancamento;
        Decimal _receita;
        string _historico;
        string _expr1;
        int _coordenador;
        string _tipo_projeto;
        string _conta_principal;
        string _descricao;
        string _nome;
        public int Codigo_lancamento
        {
            get { return _codigo_lancamento; }
            set { _codigo_lancamento = value; }
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

        public Decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public Decimal Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public Decimal Despesa
        {
            get { return _despesa; }
            set { _despesa = value; }
        }

        public string Pre_lancamento
        {
            get { return _pre_lancamento; }
            set { _pre_lancamento = value; }
        }

        public Decimal Receita
        {
            get { return _receita; }
            set { _receita = value; }
        }

        public string Historico
        {
            get { return _historico; }
            set { _historico = value; }
        }

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

    }


    public class base_view_extrato
    {
        DataTable _listaview_extrato;
        public DataTable ListaView_extrato
        {
            get { return _listaview_extrato; }
            set { _listaview_extrato = value; }
        }
    }


    public class tratarSaldo
    {
        Decimal _saldoReceita;

        public Decimal SaldoReceita
        {
            get { return _saldoReceita; }
            set { _saldoReceita = value; }
        }
        Decimal _saldoDespesa;

        public Decimal SaldoDespesa
        {
            get { return _saldoDespesa; }
            set { _saldoDespesa = value; }
        }
        public void GetSaldo(string _projeto, string _data)
        {
            SqlCommand cmd = new SqlCommand();
            string _comando = "SELECT SUM(dbo.Movimentos.Receita) AS Receita, SUM(dbo.Movimentos.Despesa) AS Despesa FROM  dbo.Movimentos WHERE data <convert(datetime, '"+_data+"',104) and projeto = '"+_projeto+"'";
            NovaEraPortais.banco.DB BancoOrigem = new DB();
            BancoOrigem.conectarBanco();
            cmd.Connection = BancoOrigem.Conexao;
            System.Data.SqlClient.SqlCommand _SqlCommand = new System.Data.SqlClient.SqlCommand(_comando, BancoOrigem.Conexao);
            System.Data.SqlClient.SqlDataAdapter _SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            _SqlDataAdapter.SelectCommand = _SqlCommand;
            try
            {
                BancoOrigem.Tabela = new System.Data.DataTable();
                BancoOrigem.Tabela.TableName = "Saldos";
                _SqlDataAdapter.Fill(BancoOrigem.Tabela);
                _saldoDespesa = Convert.ToDecimal(BancoOrigem.Tabela.Rows[0]["Despesa"]);
                _saldoReceita = Convert.ToDecimal(BancoOrigem.Tabela.Rows[0]["Receita"]);
                BancoOrigem.Conexao.Close();
            }
            catch (Exception _Exception)
            {
                // Error occurred while trying to execute reader
                // send error message to console (change below line to customize error handling)
                BancoOrigem.MensagemErro = _Exception.Message;
            }      
        }
    }



    public class class_view_extrato
    {
        List<basecampos_view_extrato> _linhas;
        public List<basecampos_view_extrato> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }
        public void ListaView_extrato(string parmCoordenador, string projeto, string inicio, string fim, string titulos)
        {
            base_view_extrato view_extrato = new base_view_extrato();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Codigo_Lancamento");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Rubrica");
            BancoOrigem.Campos.Add("Fatura");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Valor");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Pre_lancamento");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Expr1");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Tipo_Projeto");
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Nome");
            BancoOrigem.Nometabela = "view_extrato";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro.Add(" projeto = '" + projeto + "' and ");
            if (inicio != "")
            {
                BancoOrigem.Filtro.Add(" data >= convert(datetime, '" + inicio + "',104) and ");
            }
            if (fim != "")
            {
                BancoOrigem.Filtro.Add(" data <= convert(datetime, '" + fim + "',104) ");
            }
            BancoOrigem.getData();
            Linhas = new List<basecampos_view_extrato>();
            basecampos_view_extrato linha = new basecampos_view_extrato();
            tratarSaldo getsaldo = new tratarSaldo();
            getsaldo.GetSaldo(projeto, inicio);
            Decimal saldo = 0;

            saldo = saldo + getsaldo.SaldoReceita - getsaldo.SaldoDespesa;
            linha = new basecampos_view_extrato();
            linha.Data = Convert.ToDateTime(inicio);
            linha.Saldo = saldo;
            linha.Despesa = getsaldo.SaldoDespesa;
            linha.Receita = getsaldo.SaldoReceita;
            linha.Historico = "Saldo Anterior";
            linha.Descricao = "Saldo Anterior";
            Linhas.Add(linha);
            Excel.Excel planilha = new Excel.Excel();
            planilha.Nomeplanilha = "ExtratoProjeto";
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
                saldo = saldo + Convert.ToDecimal(dataRow["Receita"]) - Convert.ToDecimal(dataRow["Despesa"]);
                linha = new basecampos_view_extrato();
                linha.Codigo_lancamento = Convert.ToInt32(dataRow["Codigo_Lancamento"].ToString());
                linha.Projeto = Convert.ToInt32(dataRow["Projeto"].ToString());
                linha.Rubrica = Convert.ToInt32(dataRow["Rubrica"].ToString());
                linha.Fatura = dataRow["Fatura"].ToString();
                linha.Data = Convert.ToDateTime(dataRow["Data"].ToString());
                linha.Valor = Convert.ToDecimal(dataRow["Valor"].ToString());
                linha.Saldo = saldo;
                linha.Despesa = Convert.ToDecimal(dataRow["Despesa"].ToString());
                linha.Pre_lancamento = dataRow["Pre_lancamento"].ToString();
                linha.Receita = Convert.ToDecimal(dataRow["Receita"].ToString());
                linha.Historico = dataRow["Historico"].ToString();
                linha.Expr1 = dataRow["Expr1"].ToString();
                linha.Tipo_projeto = dataRow["Tipo_Projeto"].ToString();
                linha.Conta_principal = dataRow["Conta_Principal"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Nome = dataRow["Nome"].ToString();
                Linhas.Add(linha);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue(linha.Historico);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(1).SetCellValue(linha.Data);
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(2).SetCellValue(Convert.ToDouble(linha.Receita));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(3).SetCellValue(Convert.ToDouble(linha.Despesa));
                planilha.Sheet.GetRow(planilha.NumLinha).GetCell(4).SetCellValue(Convert.ToDouble(saldo));
                planilha.NovaLinha();
            }
            planilha.ExportDataTableToExcel(parmCoordenador);
        }

        public void ListaView_extrato(string inicio, string fim)
        {
            base_view_extrato view_extrato = new base_view_extrato();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Codigo_Lancamento");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Rubrica");
            BancoOrigem.Campos.Add("Fatura");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Valor");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Pre_lancamento");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Expr1");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Campos.Add("Tipo_Projeto");
            BancoOrigem.Campos.Add("Conta_Principal");
            BancoOrigem.Campos.Add("Descricao");
            BancoOrigem.Campos.Add("Nome");
            BancoOrigem.Nometabela = "view_extrato";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro.Add(" data >= " + inicio + " and data <= " + fim);
            BancoOrigem.getData();
            Linhas = new List<basecampos_view_extrato>();
            basecampos_view_extrato linha = new basecampos_view_extrato();
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_view_extrato();
                linha.Codigo_lancamento = Convert.ToInt32(dataRow["Codigo_Lancamento"].ToString());
                linha.Projeto = Convert.ToInt32(dataRow["Projeto"].ToString());
                linha.Rubrica = Convert.ToInt32(dataRow["Rubrica"].ToString());
                linha.Fatura = dataRow["Fatura"].ToString();
                linha.Data = Convert.ToDateTime(dataRow["Data"].ToString());
                linha.Valor = Convert.ToDecimal(dataRow["Valor"].ToString());
                linha.Saldo = Convert.ToDecimal(dataRow["Saldo"].ToString());
                linha.Despesa = Convert.ToDecimal(dataRow["Despesa"].ToString());
                linha.Pre_lancamento = dataRow["Pre_lancamento"].ToString();
                linha.Receita = Convert.ToDecimal(dataRow["Receita"].ToString());
                linha.Historico = dataRow["Historico"].ToString();
                linha.Expr1 = dataRow["Expr1"].ToString();
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                linha.Tipo_projeto = dataRow["Tipo_Projeto"].ToString();
                linha.Conta_principal = dataRow["Conta_Principal"].ToString();
                linha.Descricao = dataRow["Descricao"].ToString();
                linha.Nome = dataRow["Nome"].ToString();
                Linhas.Add(linha);
            }
        }
        void GerarDadosExcel_View_extrato2(String parm_coordenador, List<String> _filtro, String NomePlanilha, List<String> _titulos)
        {
            base_view_extrato view_extrato = new base_view_extrato();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();

            BancoOrigem.Campos.Add("Historico");
            BancoOrigem.Campos.Add("Data");
            BancoOrigem.Campos.Add("Receita");
            BancoOrigem.Campos.Add("Despesa");
            BancoOrigem.Campos.Add("Saldo");
            BancoOrigem.Nometabela = "view_extrato2";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.getData();
            Linhas = new List<basecampos_view_extrato>();
            basecampos_view_extrato linha = new basecampos_view_extrato();
            Excel.Excel planilha = new Excel.Excel();
            planilha.Nomeplanilha = NomePlanilha;
            planilha.InicializarWorkBook();
            planilha.NumLinha = 8;
            planilha.InicializarSheet();

            planilha.Sheet.GetRow(5).GetCell(1).SetCellValue(_titulos[0]);
            planilha.Sheet.GetRow(6).GetCell(1).SetCellValue(_titulos[1]);


            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                try
                {
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue(dataRow["Historico"].ToString());
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(1).SetCellValue(Convert.ToDateTime(dataRow["Data"].ToString()));
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(2).SetCellValue(Convert.ToDouble(dataRow["Receita"].ToString()));
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(3).SetCellValue(Convert.ToDouble(dataRow["Despesa"].ToString()));
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(4).SetCellValue(Convert.ToDouble(dataRow["Saldo"].ToString()));
                }
                catch
                {
                    planilha.Sheet.GetRow(planilha.NumLinha).GetCell(0).SetCellValue("Erro de dados");
                }
                planilha.NovaLinha();

            }
            planilha.ExportDataTableToExcel(parm_coordenador);
        }
        public void _GetExcelOf_View_extrato2(String parm_coordenador, String parmfiltro, String NomePlanilha, String parmtitulos)
        {
            List<String> _filtro = new List<String>();
            _filtro.Add(parmfiltro);

            List<String> _titulos = new List<String>();
            string[] namesArray = parmtitulos.Split(';');
            _titulos.AddRange(namesArray);
            GerarDadosExcel_View_extrato2(parm_coordenador, _filtro, NomePlanilha, _titulos);
        }

    }
}

