using System;
using NPOI.HSSF.UserModel;
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

        #region Estilos
        HSSFFont hFontNormal;
        ICellStyle cellCurrencyStyleBold;
        HSSFFont FonteBold(ref HSSFSheet sh, ref HSSFWorkbook wb, short tamanho = 12)
        {
            hFontNormal.FontHeightInPoints = tamanho;
            hFontNormal.FontName = "Calibri";
            hFontNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            return hFontNormal;
        }
        HSSFFont FonteCabecalho(ref HSSFSheet sh, ref HSSFWorkbook wb, short tamanho = 12)
        {
            // HSSFFont hFontNormal = (HSSFFont)wb.CreateFont();

            hFontNormal.FontHeightInPoints = tamanho;
            hFontNormal.FontName = "Calibri";
            hFontNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            return hFontNormal;
        }
        HSSFFont FontePadrao(ref HSSFSheet sh, ref HSSFWorkbook wb, short tamanho = 11)
        {
            // HSSFFont hFontNormal = (HSSFFont)wb.CreateFont();

            hFontNormal.FontHeightInPoints = tamanho;
            hFontNormal.FontName = "Calibri";
            hFontNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            return hFontNormal;
        }
        ICellStyle estiloBold(ref HSSFSheet sh, ref HSSFWorkbook wb, Boolean borda = true, int tamanho = 12, bool currency = false)
        {

            if (currency)
                cellCurrencyStyleBold.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");
            cellCurrencyStyleBold.VerticalAlignment = VerticalAlignment.Center;
            cellCurrencyStyleBold.SetFont(FonteBold(ref sh, ref wb));
            if (borda)
                cellCurrencyStyleBold.BorderBottom = BorderStyle.Medium;
            return cellCurrencyStyleBold;
        }
        ICellStyle estiloPadrao(ref HSSFSheet sh, ref HSSFWorkbook wb, Boolean borda = true, short tamanho = 12, bool currency = false)
        {
            // ICellStyle cellCurrencyStyleBold = wb.CreateCellStyle();
            cellCurrencyStyleBold.VerticalAlignment = VerticalAlignment.Center;
            if (currency)
                cellCurrencyStyleBold.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");
            cellCurrencyStyleBold.SetFont(FontePadrao(ref sh, ref wb, tamanho));
            if (borda)
                cellCurrencyStyleBold.BorderBottom = BorderStyle.Medium;
            return cellCurrencyStyleBold;
        }
        ICellStyle estiloCabecalho(ref HSSFSheet sh, ref HSSFWorkbook wb, Boolean borda = true, short tamanho = 16, bool currency = false)
        {
            // ICellStyle cellCurrencyStyleBold = wb.CreateCellStyle();
            cellCurrencyStyleBold.VerticalAlignment = VerticalAlignment.Center;
            if (currency)
                cellCurrencyStyleBold.DataFormat = wb.CreateDataFormat().GetFormat("$#,##0.00");
            cellCurrencyStyleBold.SetFont(FonteCabecalho(ref sh, ref wb, tamanho));
            if (borda)
                cellCurrencyStyleBold.BorderBottom = BorderStyle.Medium;
            return cellCurrencyStyleBold;
        }
        #endregion

        #region Metodos de relatórios

        public int teste(int n)
        {
            int x = 1, k;
            if (n == 1) return x;
            for (k = 1; k < n; ++k)
            {
                x = x + (teste(k) * teste(n - k));
            }
            return x;
        }

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
                if (proj != Convert.ToInt32(r["projeto"].ToString()))
                {
                    cabecalhoprojeto(ref sh, ref numlinha, SaldoProjeto, cellCurrencyStyleBold);
                    SaldoProjeto = 0;
                    proj = Convert.ToInt32(r["projeto"].ToString());
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

            ExportarArquivo(wb, "Pagamento_Lote " + lote + " de " + Convert.ToDateTime(data).ToString("dd-MM-yyyy") + ".xls");
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

        void ExportarArquivo(HSSFWorkbook wb, string nomearquivo)
        {
            MemoryStream memoryStream = new MemoryStream();
            //string fileName = HttpContext.Current.Server.MapPath("~/arquivosgerados/Pagamento_" + lote + "_" + data.ToString("dd-MM-yyyy") + ".xls"); ;
            //string fileName = "Pagamento_Lote " + lote + " de " + data.ToString("dd-MM-yyyy") + ".xls";

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
                response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", nomearquivo));
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

        #region Extratos de Projetos

        void CabecalhoExtrato(ref HSSFSheet sh, ref HSSFWorkbook wb, ref int numlinha, DataRow r, DateTime dataInicio, DateTime dataFim)
        {
            sh.CreateRow(numlinha).CreateCell(0).SetCellValue("Fundação Padre Leonel Franca");
            sh.GetRow(numlinha).GetCell(0).CellStyle = estiloCabecalho(ref sh, ref wb, false, tamanho: 17);

            sh.GetRow(numlinha).CreateCell(3).SetCellValue("Extrato de Projetos");
            sh.GetRow(numlinha).GetCell(3).CellStyle.SetFont(FonteCabecalho(ref sh, ref wb));
            //sh.GetRow(numlinha).GetCell(0).CellStyle.SetFont(FonteCabecalho(ref sh, ref wb));
            numlinha++;
            sh.CreateRow(numlinha).CreateCell(0).SetCellValue("Projeto : " + r["nomeprojeto"].ToString());
            sh.GetRow(numlinha).GetCell(0).CellStyle = estiloPadrao(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(3).SetCellValue("de: : " + dataInicio.ToString("dd/MM/yyyy") + " a " + dataFim.ToString("dd/MM/yyyy"));
            sh.GetRow(numlinha).GetCell(3).CellStyle = estiloPadrao(ref sh, ref wb, false);
            numlinha++;

            sh.CreateRow(numlinha).CreateCell(0).SetCellValue("Coordenador: " + r["nomecoordenador"].ToString());
            sh.GetRow(numlinha).GetCell(0).CellStyle = estiloPadrao(ref sh, ref wb, false);
            numlinha += 2;
            sh.CreateRow(numlinha).CreateCell(0).SetCellValue("Data");
            sh.GetRow(numlinha).GetCell(0).CellStyle = estiloBold(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(1).SetCellValue("Fatura");
            sh.GetRow(numlinha).GetCell(1).CellStyle = estiloBold(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(2).SetCellValue("Histórico");
            sh.GetRow(numlinha).GetCell(2).CellStyle = estiloBold(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(3).SetCellValue("Receita");
            sh.GetRow(numlinha).GetCell(3).CellStyle = estiloBold(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(4).SetCellValue("Despesa");
            sh.GetRow(numlinha).GetCell(4).CellStyle = estiloBold(ref sh, ref wb, false);

            sh.GetRow(numlinha).CreateCell(5).SetCellValue("Saldo");
            sh.GetRow(numlinha).GetCell(5).CellStyle = estiloBold(ref sh, ref wb, false);

            numlinha++;
        }
        public string GerarExcelExtrato(int idprojeto, string dataInicio, string dataFim)
        {
            int numlinha = 0;
            Double SaldoProjeto;
            int linhasporpagina = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("qtd_linhas_extrato_por_pagina"));

            HSSFWorkbook wb;// = new HSSFWorkbook();
            HSSFSheet sh;// = (HSSFSheet)wb.GetSheet("Extrato");
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathModeloExtratos"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheet("extrato");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }

            ListarExtratoProjeto(idprojeto, dataInicio, dataFim);
            SaldoProjeto = 0;
            numlinha = 4;
            sh.GetRow(1).GetCell(1).SetCellValue(banco.tabela.Rows[1]["NomeProjeto"].ToString());
            sh.GetRow(2).GetCell(1).SetCellValue(banco.tabela.Rows[1]["NomeCoordenador"].ToString());
            sh.GetRow(2).GetCell(3).SetCellValue(Convert.ToDateTime(dataInicio).ToString("dd/MM/yyyy"));
            sh.GetRow(2).GetCell(4).SetCellValue("a");
            sh.GetRow(2).GetCell(5).SetCellValue(Convert.ToDateTime(dataFim).ToString("dd/MM/yyyy"));
            foreach (DataRow r in banco.tabela.Rows)
            {
                SaldoProjeto -= Convert.ToDouble(r["despesa"].ToString());
                SaldoProjeto += Convert.ToDouble(r["receita"].ToString());
                var linha = sh.GetRow(numlinha);
                linha.GetCell(0).SetCellValue(Convert.ToDateTime(r["data"].ToString()).ToString("dd/MM/yyyy"));
                linha.GetCell(1).SetCellValue(r["fatura"].ToString());
                linha.GetCell(2).SetCellValue(r["historico"].ToString());
                linha.GetCell(3).SetCellValue(Convert.ToDouble(r["receita"].ToString()));
                linha.GetCell(4).SetCellValue(Convert.ToDouble(r["despesa"].ToString()));
                linha.GetCell(5).SetCellValue(SaldoProjeto);
                numlinha++;
            }
            wb.SetPrintArea(0, 0, 5, 0, numlinha);
            sh.FitToPage = false;// RowBreak(8);
            ExportarArquivo(wb, "Extrato.xls");
            return "";
        }
        public string GerarExcelExtratoRubrica(int rubrica, string dtInicio, string dtFim, int projeto = -1)
        {
            int numlinha = 0;
            Double SaldoProjeto;
            HSSFWorkbook wb;// = new HSSFWorkbook();
            HSSFSheet sh;// = (HSSFSheet)wb.GetSheet("Extrato");
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathModeloExtratosRubrica"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheetAt(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }
            if (projeto != -1)
                Listar(projeto: projeto.ToString(), dataInicio: dtInicio, dataFim: dtFim, rubrica: rubrica);
            else
                Listar(dataInicio: dtInicio, dataFim: dtFim, rubrica: rubrica);

            SaldoProjeto = 0;
            numlinha = 5;
            sh.GetRow(1).GetCell(1).SetCellValue(banco.tabela.Rows[1]["NomeProjeto"].ToString());
            sh.GetRow(2).GetCell(1).SetCellValue(banco.tabela.Rows[1]["NomeCoordenador"].ToString());
            sh.GetRow(2).GetCell(3).SetCellValue(Convert.ToDateTime(dtInicio).ToString("dd/MM/yyyy"));
            sh.GetRow(2).GetCell(4).SetCellValue("a");
            sh.GetRow(2).GetCell(5).SetCellValue(Convert.ToDateTime(dtFim).ToString("dd/MM/yyyy"));
            sh.GetRow(3).GetCell(1).SetCellValue(banco.tabela.Rows[1]["nomerubricacompleta"].ToString());
            foreach (DataRow r in banco.tabela.Rows)
            {
                SaldoProjeto -= Convert.ToDouble(r["despesa"].ToString());
                SaldoProjeto += Convert.ToDouble(r["receita"].ToString());
                var linha = sh.GetRow(numlinha);
                linha.GetCell(0).SetCellValue(Convert.ToDateTime(r["data"].ToString()).ToString("dd/MM/yyyy"));
                linha.GetCell(1).SetCellValue(r["fatura"].ToString());
                linha.GetCell(2).SetCellValue(r["historico"].ToString());
                linha.GetCell(3).SetCellValue(Convert.ToDouble(r["receita"].ToString()));
                linha.GetCell(4).SetCellValue(Convert.ToDouble(r["despesa"].ToString()));
                linha.GetCell(5).SetCellValue(SaldoProjeto);
                numlinha++;
            }
            wb.SetPrintArea(0, 0, 5, 0, numlinha);
            sh.FitToPage = false;// RowBreak(8);
            ExportarArquivo(wb, "Extrato_Rubrica.xls");
            return "";
        }

        public string GerarExcelSaldoRubrica(string data, string conta, int projeto)
        {
            int numlinha = 0;
            Double SaldoProjeto;
            int linhasporpagina = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("qtd_linhas_extrato_por_pagina"));

            HSSFWorkbook wb;// = new HSSFWorkbook();
            HSSFSheet sh;// = (HSSFSheet)wb.GetSheet("Extrato");
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathModeloSaldoRubricas"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheetAt(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }

            ListarSaldosProjetosRubricas(data, conta, projeto);
            SaldoProjeto = 0;
            numlinha = 4;
            sh.GetRow(1).GetCell(0).SetCellValue("Projeto: "+banco.tabela.Rows[1]["nome"].ToString());
            sh.GetRow(2).GetCell(0).SetCellValue("Coordenador: "+banco.tabela.Rows[1]["nomecoordenador"].ToString());
            sh.GetRow(2).GetCell(1).SetCellValue(Convert.ToDateTime(data).ToString("dd/MM/yyyy"));
            foreach (DataRow r in banco.tabela.Rows)
            {
                SaldoProjeto -= Convert.ToDouble(r["Despesa"].ToString());
                SaldoProjeto += Convert.ToDouble(r["Receita"].ToString());
                var linha = sh.GetRow(numlinha);
                linha.GetCell(0).SetCellValue(r["rubrica"].ToString());
                linha.GetCell(1).SetCellValue(SaldoProjeto);
                numlinha++;
            }
            wb.SetPrintArea(0, 0, 5, 0, numlinha);
            sh.FitToPage = false;// RowBreak(8);
            ExportarArquivo(wb, "Saldo por Rubricas.xls");
            return "";
        }
        public string GerarExcelSaldoProjeto(string conta, string data)
        {
            int numlinha = 0;
            Double SaldoProjeto;
            String _conta = "";
            int linhasporpagina = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("qtd_linhas_extrato_por_pagina"));

            HSSFWorkbook wb;// = new HSSFWorkbook();
            //wb.CreateSheet("Extrato");
            HSSFSheet sh;// = (HSSFSheet)wb.GetSheet("Extrato");
                         //            sh.rep
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathModeloSaldoProjetos"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheetAt(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }

            ListarSaldosProjetos(conta, data);

            SaldoProjeto = 0;
            numlinha = 4;
            _conta = "";
            foreach (DataRow r in banco.tabela.Rows)
            {
                SaldoProjeto -= Convert.ToDouble(r["despesa"].ToString());
                SaldoProjeto += Convert.ToDouble(r["receita"].ToString());
                var p = numlinha % linhasporpagina;
                if (_conta != r["conta"].ToString())
                {
                    sh.GetRow(numlinha).GetCell(0).SetCellValue(r["conta"].ToString().Trim() + " - " + r["descricao"].ToString().Trim());
                    _conta = r["conta"].ToString();
                    numlinha++;
                }
                //var linha = sh.CreateRow(numlinha);
                try
                {
                    sh.GetRow(numlinha).GetCell(0).SetCellValue(r["nome"].ToString());
                    sh.GetRow(numlinha).GetCell(1).SetCellValue(Convert.ToDouble(r["saldo"].ToString()));
                }
                catch
                {

                }
                numlinha++;
            }

            wb.SetPrintArea(0, 0, 2, 0, numlinha);
            //sh.PrintSetup.PaperSize = (short)PaperSize.A4;
            sh.FitToPage = false;// RowBreak(8);

            ExportarArquivo(wb, "Extrato.xls");
            return "";
        }
        public string GerarSenhaCoordenador(Int32 coordenador)
        {
            Int32 totalProjetos = 0;
            HSSFWorkbook wb;// = new HSSFWorkbook();
            HSSFSheet sh;// = (HSSFSheet)wb.GetSheet("Extrato");
                         //            sh.rep
            try
            {
                string x = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("pathsenhacoordenador"));
                using (FileStream file = new FileStream(x, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                    file.Close();
                    sh = (HSSFSheet)wb.GetSheetAt(0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao abrir o arquivo de modelo. Processo encerrado. Erro " + ex.Message);
            }
            Negocio.coordenadorNegocio objCoordenador = new coordenadorNegocio();
            objCoordenador.GetCoordenadorById(coordenador);
            sh.GetRow(2).GetCell(0).SetCellValue(objCoordenador.banco.tabela.Rows[0]["nomeCompleto"].ToString());
            sh.GetRow(2).GetCell(1).SetCellValue("Senha: " + objCoordenador.banco.tabela.Rows[0]["senha"].ToString());
            Negocio.projetoNegocios objProjeto = new projetoNegocios();
            objProjeto.GetProjetos(coordenador);
            Int32 numlinha = 4;
            foreach (DataRow r in objProjeto.banco.tabela.Rows)
            {
                totalProjetos++;
                //var linha = sh.CreateRow(numlinha);
                sh.GetRow(numlinha).GetCell(0).SetCellValue(r["projeto"].ToString());
                sh.GetRow(numlinha).GetCell(1).SetCellValue(r["conta_principal"].ToString());
                numlinha++;
            }

            wb.SetPrintArea(0, 0, 2, 0, numlinha);
            //sh.PrintSetup.PaperSize = (short)PaperSize.A4;
            sh.FitToPage = false;// RowBreak(8);

            ExportarArquivo(wb, "SenhaCoordenador.xls");
            return "";
        }

        #endregion

        #region Modulos de consulta e CRUD
        public string SaldosProjeto(string data, int projeto)
        {
            string lResult = "";

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_SaldosProjeto", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }


        public string ListarSaldosProjetosRubricas(string data, string conta, int projeto)
        {
            string lResult = "";

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", projeto));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_ListarSaldosProjetosRubricas", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }
        public string ListarSaldosProjetos(string conta, string data)
        {
            string lResult = "";

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("data", data));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("conta", conta));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_ListarSaldosProjetos", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }


        public string ListarPagamentosPorAno(int ano)
        {
            string lResult = "";

            DateTime dtInicio = new DateTime(ano, 1, 1);
            DateTime dtFim = new DateTime(ano, 12, 31);

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataInicio", dtInicio));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataFim", dtFim));
            banco.ExecuteAndReturnData("sp_CtrlProjetos_MovimentosListaPagamentos", "tabela");
            if (banco.tabela != null)
            {
                if (banco.tabela.Rows.Count > 0)
                {
                    lResult = banco.GetJsonTabela();
                }
            }
            return lResult;
        }

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

        public string ListarExtratoProjeto(int idProjeto, string dataInicio, string dataFim)
        {
            string lResult = "";
            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("projeto", idProjeto));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataInicio", Convert.ToDateTime(dataInicio).ToString("yyyy-MM-dd")));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("dataFim", Convert.ToDateTime(dataFim).ToString("yyyy-MM-dd")));
            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosListaExtrato]", "tabmovimento");
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

            banco.ExecuteAndReturnData("[sp_CtrlProjetos_MovimentosIncluir]", "tabela");
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
