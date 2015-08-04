using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using NovaEraPortais.banco;

namespace NovaEraPortais.projetos2
{
    public class basecampos_projetos
    {
        int _codigo;
        string _projeto;
        DateTime _inicio;
        int _coordenador;
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

    }





    public class base_projetos
    {
        DataTable _listaprojetos;
        public DataTable ListaProjetos
        {
            get { return _listaprojetos; }
            set { _listaprojetos = value; }
        }
    }





    public class class_projetos
    {
        List<basecampos_projetos> _linhas;
        public List<basecampos_projetos> Linhas
        {
            get { return _linhas; }
            set { _linhas = value; }
        }

        public static List<String> CamposChave
        {
            get
            {
                List<String> campos_ = new List<string>();
                campos_.Add("codigo");
                campos_.Add("nome");
                return campos_;
            }
        }

        public static List<String> CamposIdentity
        {
            get
            {
                List<String> campos_ = new List<string>();
                campos_.Add("codigo");
                return campos_;
            }
        }
        public bool isCampoChave(string campo)
        {
            bool retorno = false;
            foreach (string _campo in CamposChave)
            {
                if ((_campo.Trim() == campo.Trim()) && (!retorno))
                {
                    retorno = true;
                }
            }
            return retorno;
        }

        public bool isCampoIdentity(string campo)
        {
            bool retorno = false;
            campo = campo.Trim().ToLower();
            foreach (string _campo in CamposIdentity)
            {
                if ((_campo.Trim().ToLower() == campo ) && (!retorno))
                {
                    retorno = true;
                }
            }
            return retorno;
        }

        //criar campos chave e identity como propriedades no gerador. Criar neste ponto();
        void ListaProjetos(String parm_coordenador, List<String> _filtro)
        {
            base_projetos projetos = new base_projetos();
            DB BancoOrigem = new DB();
            BancoOrigem.Campos = new List<string>();
            BancoOrigem.Campos.Add("Codigo");
            BancoOrigem.Campos.Add("Projeto");
            BancoOrigem.Campos.Add("Inicio");
            BancoOrigem.Campos.Add("Coordenador");
            BancoOrigem.Nometabela = "projetos";
            BancoOrigem.Filtro = new List<string>();
            BancoOrigem.Filtro = _filtro;
            BancoOrigem.getData();
            Linhas = new List<basecampos_projetos>();
            basecampos_projetos linha = new basecampos_projetos();
            NovaEraPortais.ExportarArquivos.CsvFileWriter csvFile = new ExportarArquivos.CsvFileWriter(BancoOrigem.CaminhoArquivos + "Projetos_" + parm_coordenador + ".csv");
            NovaEraPortais.ExportarArquivos.CsvRow row = new NovaEraPortais.ExportarArquivos.CsvRow();
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("--");
            csvFile.WriteLine("");
            foreach (DataRow dataRow in BancoOrigem.Tabela.Rows)
            {
                linha = new basecampos_projetos();
                linha.Codigo = Convert.ToInt32(dataRow["Codigo"].ToString());
                linha.Projeto = dataRow["Projeto"].ToString();
                linha.Inicio = Convert.ToDateTime(dataRow["Inicio"].ToString());
                linha.Coordenador = Convert.ToInt32(dataRow["Coordenador"].ToString());
                Linhas.Add(linha);
                csvFile.WriteLine(dataRow["Codigo"].ToString() + ";" + dataRow["Projeto"].ToString() + ";" + dataRow["Inicio"].ToString() + ";" + dataRow["Coordenador"].ToString() + ";");
                csvFile.WriteRow(row);
            }
            csvFile.Close();
        }
        public void GetListOf_Projetos(String parm_coordenador)
        {
            List<String> _filtro = new List<String>();
            ListaProjetos(parm_coordenador,_filtro);
        }
        public void GetRangeOf_Projetos(String parm_coordenador, String parm_chave, String inicio, String final)
        {
            List<String> _filtro = new List<String>();
            _filtro.Add("CampoComparacao >= Coordenador = " + parm_coordenador);
            ListaProjetos(parm_coordenador, _filtro);
        }
        public void GetUnique_Projetos(String parm_coordenador, String parm_chave)
        {
            List<String> _filtro = new List<String>();
            ListaProjetos(parm_coordenador, _filtro);
        }

        public void inserirRegistro(basecampos_projetos campo )
        {
            Type myType = campo.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            NovaEraPortais.banco.DB BancoOrigem = new DB();
            BancoOrigem.Campsoinsert = new List<basecampos>();
            foreach (PropertyInfo prop in props)
            {
                if (!isCampoIdentity(prop.Name))
                {
                    basecampos _campo = new basecampos();
                    _campo.CampoChave = false;
                    _campo.Conteudo = prop.GetValue(campo, null).ToString();
                    _campo.Nome = prop.Name;
                    _campo.Tipo = BancoOrigem.TipoCampo(prop.PropertyType.Name);
                    BancoOrigem.Campsoinsert.Add(_campo);
                }
            }
            BancoOrigem.Nometabela = "projetos";
            BancoOrigem.GravarRegistro();
        }

        public void AtualizarRegistro(basecampos_projetos campo)
        {
            Type myType = campo.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            banco.DB BancoOrigem = new DB() { Campsoinsert = new List<basecampos>() };
            foreach (PropertyInfo prop in props)
            {
                if (!isCampoIdentity(prop.Name))
                {
                    basecampos _campo = new basecampos();
                    _campo.CampoChave = false;
                    _campo.Conteudo = prop.GetValue(campo, null).ToString();
                    _campo.Nome = prop.Name;
                    _campo.Tipo = BancoOrigem.TipoCampo(prop.PropertyType.Name);
                    BancoOrigem.Campsoinsert.Add(_campo);
                }
            }
            BancoOrigem.Nometabela = "projetos";
            BancoOrigem.GravarRegistro();
        }
    }
}
