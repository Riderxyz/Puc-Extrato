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
            sh.GetRow(2).GetCell(0).SetCellValue(objCoordenador.banco.tabela.Rows[0]["Nome"].ToString().Trim().ToUpper());
            sh.GetRow(2).GetCell(1).SetCellValue("Senha: " + objCoordenador.banco.tabela.Rows[0]["senha"].ToString().Trim());
            Negocio.projetoNegocios objProjeto = new projetoNegocios();
            objProjeto.GetProjetos(coordenador);
            Int32 numlinha = 4;
            foreach (DataRow r in objProjeto.banco.tabela.Rows)
            {
                totalProjetos++;
                //var linha = sh.CreateRow(numlinha);
                sh.GetRow(numlinha).GetCell(0).SetCellValue(r["projeto"].ToString().Trim());
                sh.GetRow(numlinha).GetCell(1).SetCellValue(r["conta_principal"].ToString().Trim());
                numlinha++;
            }

            wb.SetPrintArea(0, 0, 2, 0, numlinha);
            //sh.PrintSetup.PaperSize = (short)PaperSize.A4;
            sh.FitToPage = false;// RowBreak(8);
            Char delimiter = ' ';
            ExportarArquivo(wb, "SenhaCoordenador_"+objCoordenador.banco.tabela.Rows[0]["nome"].ToString().Split(delimiter)[0].Trim() +".xls");
            return "";
        }

    }
}
