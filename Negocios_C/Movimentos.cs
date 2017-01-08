using System;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using System.IO;
using System.Data;
using System.Web;
using NPOI.SS.UserModel;

namespace Puc.Negocios_C
{

    public class Movimentos
    {
        Puc.Negocios_C.logErro log = new logErro();
        Negocio.clBanco banco = new clBanco();

        #region Metodos de relatórios
        public string GerarListagemPagamentos(string lote, string data)
        {
            int proj;
            string grupo;
            int numlinha;
            Double SaldoProjeto;
            Double SaldoGrupo;
            #region Abertura do arquivo e carga dos dados
            HSSFWorkbook wb;
            HSSFSheet sh;
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathModeloPagamento"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheet("pagamentos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }
            ListarPagamentos(lote, data);

            #endregion

            sh.GetRow(2).GetCell(1).SetCellValue("Data: " + Convert.ToDateTime(data).ToString("dd/MM/yyyy"));
            sh.GetRow(3).GetCell(1).SetCellValue("Lote: " + lote);
            sh.GetRow(3).GetCell(3).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            #region stilos
            HSSFFont hFontNormal = (HSSFFont)wb.CreateFont();

            hFontNormal.FontHeightInPoints = 11;
            hFontNormal.FontName = "Calibri";
            hFontNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.None;

            HSSFFont hFontBold = (HSSFFont)wb.CreateFont();

            hFontBold.FontHeightInPoints = 12;
            hFontBold.FontName = "Calibri";
            hFontBold.Boldweight = (short)FontBoldWeight.Bold;

            ICellStyle cellCurrencyStyle = wb.CreateCellStyle();
            cellCurrencyStyle.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");

            ICellStyle cellCurrencyStyleBold = wb.CreateCellStyle();
            cellCurrencyStyleBold.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");
            cellCurrencyStyleBold.VerticalAlignment = VerticalAlignment.Center;
            cellCurrencyStyleBold.SetFont(hFontBold);
            cellCurrencyStyleBold.BorderBottom = BorderStyle.Medium;
            #endregion
            proj = Convert.ToInt32(banco.tabela.Rows[0]["projeto"]);
            grupo = "xx";
            SaldoGrupo = 0;
            SaldoProjeto = 0;
            numlinha = 6;
            foreach (DataRow r in banco.tabela.Rows)
            {
                if (proj != Convert.ToInt32(r["projeto"]))
                {
                    cabecalhoprojeto(ref sh, ref numlinha, SaldoProjeto, cellCurrencyStyleBold);
                    SaldoProjeto = 0;
                    //var linhar = sh.CreateRow(numlinha);
                    //linhar.HeightInPoints = 25;
                    //NPOI.SS.Util.CellRangeAddress cra = new NPOI.SS.Util.CellRangeAddress(numlinha, numlinha, 1, 4);
                    //sh.AddMergedRegion(cra);
                    //linhar.CreateCell(1).SetCellValue(SaldoProjeto);
                    //linhar.GetCell(1).SetCellType(NPOI.SS.UserModel.CellType.Numeric);
                    //linhar.GetCell(1).CellStyle = cellCurrencyStyleBold;
                    //for (int i = 2; i <= 4; i++)
                    //{
                    //    linhar.CreateCell(i);
                    //    linhar.GetCell(i).CellStyle = cellCurrencyStyleBold;
                    //}
                    //proj = Convert.ToInt32(r["projeto"]);
                    //SaldoProjeto = 0;
                    //numlinha++;
                }
                if (grupo != r["tipo_projeto"].ToString())
                {
                    var linhar = sh.CreateRow(numlinha);
                    linhar.HeightInPoints = 25;
                    NPOI.SS.Util.CellRangeAddress cra = new NPOI.SS.Util.CellRangeAddress(numlinha, numlinha, 1, 4);
                    sh.AddMergedRegion(cra);
                    linhar.CreateCell(1).SetCellValue(r["nometipoprojeto"].ToString().ToUpper());
                    linhar.GetCell(1).SetCellType(NPOI.SS.UserModel.CellType.String);
                    linhar.GetCell(1).CellStyle = cellCurrencyStyleBold;
                    //linhar.GetCell(1).CellStyle.Alignment = HorizontalAlignment.Center;
                    for (int i = 2; i <= 4; i++)
                    {
                        linhar.CreateCell(i);
                        linhar.GetCell(i).CellStyle = cellCurrencyStyleBold;
                    }
                    SaldoGrupo = 0;
                    numlinha++;
                    grupo = r["tipo_projeto"].ToString();
                }
                var linha = sh.CreateRow(numlinha);
                linha.CreateCell(1).SetCellValue(r["nomeprojeto"].ToString());
                linha.GetCell(1).SetCellType(NPOI.SS.UserModel.CellType.String);

                linha.CreateCell(2).SetCellValue(r["historico"].ToString());
                linha.GetCell(2).CellStyle = cellCurrencyStyle;

                linha.CreateCell(3).SetCellValue(r["banco"].ToString());
                linha.GetCell(3).SetCellType(NPOI.SS.UserModel.CellType.String);

                linha.CreateCell(4).SetCellValue(Convert.ToDouble(r["despesa"].ToString()));
                linha.GetCell(4).SetCellType(NPOI.SS.UserModel.CellType.Numeric);
                linha.GetCell(4).CellStyle = cellCurrencyStyle;
                for (int i = 1; i <= 4; i++)
                {
                    linha.GetCell(i).CellStyle.SetFont(hFontNormal);
                }
                numlinha++;
                SaldoProjeto += Convert.ToDouble(r["despesa"]);
                SaldoGrupo += Convert.ToDouble(r["despesa"]);

            }
            if (SaldoProjeto != 0)
            {
                cabecalhoprojeto(ref sh, ref numlinha, SaldoProjeto, cellCurrencyStyleBold);
            }

            wb.SetPrintArea(0, 1, 4, 1, numlinha);

            ExportarArquivo(wb, lote, Convert.ToDateTime(data));
            return "";
        }

        void cabecalhoprojeto(ref HSSFSheet sh, ref int numlinha, double SaldoProjeto, ICellStyle cellCurrencyStyleBold)
        {
            var linhar = sh.CreateRow(numlinha);
            linhar.HeightInPoints = 25;
            NPOI.SS.Util.CellRangeAddress cra = new NPOI.SS.Util.CellRangeAddress(numlinha, numlinha, 1, 4);
            sh.AddMergedRegion(cra);
            linhar.CreateCell(1).SetCellValue(SaldoProjeto);
            linhar.GetCell(1).SetCellType(NPOI.SS.UserModel.CellType.Numeric);
            linhar.GetCell(1).CellStyle = cellCurrencyStyleBold;
           // linhar.GetCell(1).CellStyle.Alignment = HorizontalAlignment.Right;
            for (int i = 2; i <= 4; i++)
            {
                linhar.CreateCell(i);
                linhar.GetCell(i).CellStyle = cellCurrencyStyleBold;
                //linhar.GetCell(i).CellStyle.Alignment = HorizontalAlignment.Right;
            }
            numlinha++;
        }

        void ExportarArquivo(HSSFWorkbook wb, string lote, DateTime data)
        {
            MemoryStream memoryStream = new MemoryStream();
            //string fileName = HttpContext.Current.Server.MapPath("~/arquivosgerados/Pagamento_" + lote + "_" + data.ToString("dd-MM-yyyy") + ".xls"); ;
            string fileName = "Pagamento_Lote " + lote + " de " + data.ToString("dd-MM-yyyy") + ".xls";

            wb.Write(memoryStream);
            memoryStream.Flush();

            try
            {
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                //response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Length", memoryStream.Length.ToString());
                response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
                response.BinaryWrite(memoryStream.GetBuffer());
                response.Flush();
                response.End();
                // Do nothing, error expected due to Flush();
                // return "Arquivo Gerado com êxito";
            }
            catch
            {
                //return "Erro na geração do arquivo!";
            }


        }
        #endregion


        #region Modulos de consulta e CRUD
        public string ListarPagamentos(string lote, string data)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataInicio", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataFim", data));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_MovimentosListaPagamentos", "tabmovimento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Listar(string projeto = default(string), string conta = default(string), string historico = default(string), int? coordenador = default(int), int? rubrica = default(int), string dataInicio = default(string), string dataFim = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            if (projeto != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            }
            if (conta != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            }
            if (coordenador != default(int))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("coordenador", coordenador));
            }
            if (rubrica != default(int))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", rubrica));
            }
            if (historico != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            }
            if (dataInicio != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataInicio", dataInicio));
            }
            if (dataFim != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataFim", dataFim));
            }
            #endregion

            banco.ExecuteAndReturnData("sp_CtrlProjetos_MovimentosLista", "tabmovimento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Incluir(DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string fatura = default(string), int? lote = -1)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("despesa", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", rubrica));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("fatura", fatura));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosIncluir]", "tabcoordenador");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

        public string Excluir(Int64 id)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosExcluir]", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string Atualizar(Int64 id, string projeto = null, string historico = null, double? receita = null, double? despesa = null, int? rubrica = null, string codbanco = null, string tipo_lancamento = null, string fatura = null, int? lote = null, DateTime? data = null)
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("despesa", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", rubrica));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("fatura", fatura));
            //banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabmovimento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string ExecutarPagamento(int id, DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string lote = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabpagamento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string AtualizarPagamento(int id, DateTime data, string projeto, string historico, double receita, double despesa, int rubrica, string codbanco, string tipo_lancamento = default(string), string lote = default(string))
        {
            string lResult = "";
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("id", id));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("historico", historico));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("receita", receita));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("rubrica", despesa));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("lote", lote));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("banco", codbanco));
            if (tipo_lancamento != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("tipo_lancamento", tipo_lancamento));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosAtualizar]", "tabpagamento");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string ListarSaldoConta(string conta, string data = default(string))
        {
            string lResult = "";
            try
            {
                conta = conta.Substring(0, conta.Length - conta.IndexOf("."));
            }
            catch
            {
                //conta = conta;
            }
            #region Preparação dos parametros
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            if (data != default(string))
            {
                banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            }
            #endregion

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_SaldosContaListar]", "tabsaldoconta");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;


        }
    }
    #endregion
}
