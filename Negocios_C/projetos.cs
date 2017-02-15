using System;
using NPOI.HSSF.UserModel;
using Negocio;
using System.IO;
using System.Data;
using System.Web;
using NPOI.SS.UserModel;

namespace Negocios_C
{
    public class projetos
    {
        Negocio.clBanco banco = new clBanco();
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
            sh.GetRow(1).GetCell(0).SetCellValue(objCoordenador.banco.tabela.Rows[0]["nome"].ToString().Trim().ToUpper());
            //sh.GetRow(2).GetCell(1).SetCellValue("Senha: " + objCoordenador.banco.tabela.Rows[0]["senha"].ToString().Trim());
            Negocio.projetoNegocios objProjeto = new projetoNegocios();
            objProjeto.GetProjetos(coordenador);
            Int32 numlinha = 4;
            foreach (DataRow r in objProjeto.banco.tabela.Rows)
            {
                totalProjetos++;
                //var linha = sh.CreateRow(numlinha);
                sh.GetRow(numlinha).GetCell(0).SetCellValue(r["projeto"].ToString().Trim());
                //  sh.GetRow(numlinha).GetCell(1).SetCellValue(r["conta_principal"].ToString().Trim());
                numlinha++;
            }
            HSSFFont hFont = (HSSFFont)wb.CreateFont();

            hFont.FontHeightInPoints = 14;
            hFont.FontName = "Calibri";
            hFont.Underline = FontUnderlineType.Double;
            hFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            HSSFCellStyle hStyle = (HSSFCellStyle)wb.CreateCellStyle();
            hStyle.SetFont(hFont);

            numlinha += 2;
            sh.GetRow(numlinha).GetCell(1).SetCellValue("Estes são os seus dados para realizar o acesso via internet");
            sh.GetRow(numlinha).GetCell(1).CellStyle = hStyle;
            sh.GetRow(numlinha).RemoveCell(sh.GetRow(numlinha).GetCell(2));
            sh.GetRow(numlinha).RemoveCell(sh.GetRow(numlinha).GetCell(3));
            sh.GetRow(numlinha).RemoveCell(sh.GetRow(numlinha).GetCell(4));
            numlinha++;
            sh.GetRow(numlinha).GetCell(2).SetCellValue("Código");
            sh.GetRow(numlinha).GetCell(4).SetCellValue("Senha");
            numlinha++;
            sh.GetRow(numlinha).GetCell(2).SetCellValue(objCoordenador.banco.tabela.Rows[0]["coordenador"].ToString().Trim().ToUpper());
            sh.GetRow(numlinha).GetCell(4).SetCellValue(objCoordenador.banco.tabela.Rows[0]["senha"].ToString().Trim().ToUpper());

            //for (int i = 0; i <= 7; i++)
            //{
            //    sh.GetRow(numlinha).GetCell(i).CellStyle.BorderTop = BorderStyle.Medium;
            //    sh.GetRow(numlinha + 4).GetCell(i).CellStyle.BorderTop = BorderStyle.Medium;
            //}
            //for (int i = numlinha - 4; i <= numlinha; i++)
            //{
            //    sh.GetRow(i).GetCell(0).CellStyle.BorderLeft = BorderStyle.Medium;
            //    sh.GetRow(i).GetCell(7).CellStyle.BorderRight = BorderStyle.Medium;
            //}

            wb.SetPrintArea(0, 0, 7, 0, numlinha);
            //sh.PrintSetup.PaperSize = (short)PaperSize.A4;
            sh.FitToPage = false;// RowBreak(8);
            Char delimiter = ' ';
            ExportarArquivo(wb, "SenhaCoordenador_" + objCoordenador.banco.tabela.Rows[0]["nome"].ToString().Split(delimiter)[0].Trim() + ".xls");
            return "";
        }

    }
}
