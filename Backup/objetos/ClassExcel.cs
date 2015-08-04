using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Net;


namespace NovaEraPortais.Excel
{
    public class Excel
    {
        FileStream _fs;
        MemoryStream _ms = new MemoryStream();

        Int32 _numLinha;

        public Int32 NumLinha
        {
            get { return _numLinha; }
            set { _numLinha = value; }
        }
        HSSFWorkbook _workbook;

        public HSSFWorkbook Workbook
        {
            get { return _workbook; }
            set { _workbook = value; }
        }

        String _nomeplanilha;

        public String Nomeplanilha
        {
            get { return _nomeplanilha; }
            set { _nomeplanilha = value; }
        }

        NPOI.SS.UserModel.ISheet _sheet;

        public NPOI.SS.UserModel.ISheet Sheet
        {
            get { return _sheet; }
            set { _sheet = value; }
        }

        public void NovaLinha()
        {
            _numLinha++;
        }

        public void InicializarSheet()
        {
            _sheet = _workbook.GetSheet(_nomeplanilha);
        }

        public void InicializarWorkBook()
        {
            if (_nomeplanilha != "")
            {
                _fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("/ArquivosCoordenadores") + "//" + _nomeplanilha.Trim() + ".xlt", FileMode.Open, FileAccess.Read);
                _workbook = new HSSFWorkbook(_fs, true);
            }
            
        }


        public void ExportDataTableToExcel(String NomeArquivo)
        {
            _workbook.ForceFormulaRecalculation = true;
            _workbook.Write(_ms);
            FileStream fs2 = new FileStream(System.Web.HttpContext.Current.Server.MapPath("/ArquivosCoordenadores") + "//"+ NomeArquivo + ".xls", FileMode.Create, FileAccess.ReadWrite);
            _workbook.Write(fs2);
            fs2.Flush();
            fs2.Close();
            fs2.Dispose();
        }
        public void xpto()
        {
            // Open Template
            FileStream fs = new FileStream("c:\\lixo\\Extrato.xlt", FileMode.Open, FileAccess.Read);

            // Load the template into a NPOI workbook
            HSSFWorkbook templateWorkbook = new HSSFWorkbook(fs, true);

            // Load the sheet you are going to use as a template into NPOI
            NPOI.SS.UserModel.ISheet asheet = templateWorkbook.GetSheet("SaldoProjetos");

            // Insert data into template
            asheet.GetRow(10).GetCell(3).SetCellValue(100);  // Inserting a string value into Excel

            // Save the NPOI workbook into a memory stream to be sent to the browser, could have saved to disk.
            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);

            // Send the memory stream to the browser
           // ExportDataTableToExcel(ms, "c:\\lixo\\EventExpenseReport.xls");

        }
    }



    /// <summary>
    /// Creates a new Excel spreadsheet based on a template using the ExcelPackage library.
    /// A new file is created on the server based on a template.
    /// </summary>
    /// <returns>Excel report</returns>
    /// /*
    /// /
    /*
    [AcceptVerbs(HttpVerbs.Post)]
public ActionResult ExcelPackageCreate()
{
    try
    {
        FileInfo template = new FileInfo(Server.MapPath(@"\Content\ExcelPackageTemplate.xlsx"));

        FileInfo newFile = new FileInfo(Server.MapPath(@"\Content\ExcelPackageNewFile.xlsx"));

        // Using the template to create the newFile...
        using(ExcelPackage excelPackage = new ExcelPackage(newFile, template))
        {
            // Getting the complete workbook...
            ExcelWorkbook myWorkbook = excelPackage.Workbook;

            // Getting the worksheet by its name...
            ExcelWorksheet myWorksheet = myWorkbook.Worksheets["Sheet1"];

            // Setting the value 77 at row 5 column 1...
            myWorksheet.Cell(5, 1).Value = 77.ToString();

            // Saving the change...
            excelPackage.Save();
        }

        TempData["Message"] = "Excel report created successfully!";

        return RedirectToAction("ExcelPackage");
    }
    catch(Exception ex)
    {
        TempData["Message"] = "Oops! Something went wrong.";

        return RedirectToAction("ExcelPackage");
    }
}*/
}