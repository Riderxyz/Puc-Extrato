Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports System.web

Public Class ExtratoNegocios
#Region "Propriedades"
    'Property extrato  As Dominio.extratoNegocios
    'Property ListaProjetos As List(Of Dominio.projeto)
    public Property NomeArquivoPlanilha As String = ""
    Public Property workbook As IWorkbook
    'Public Property estiloNumero As HSSFCellStyle = CType(workbook.CreateCellStyle, HSSFCellStyle)

    ' Public Property dirplanilha As String = Server.MapPath("~\App_data\planilha") + "\planilha.xls"

#End Region

#Region "Processos"
    Public Function GetExtrato(projeto As Integer, dataInicial As String, datafinal As String, pagina As Int16, pagina_tamanho As Int16) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("di", dataInicial))
        banco.parametros.Add(New SqlParameter("df", datafinal))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))
        banco.parametros.Add(New SqlParameter("pagTamanho", pagina_tamanho))
        banco.parametros.Add(New SqlParameter("pagAtual", pagina))

        banco.ExecuteAndReturnData("sp_internet_movimentos", "tabExtrato")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

    Public Function GetExtratoExcel(projeto As Integer, dataInicial As String, datafinal As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("di", dataInicial))
        banco.parametros.Add(New SqlParameter("df", datafinal))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))
        banco.parametros.Add(New SqlParameter("pagTamanho", 99999))
        banco.parametros.Add(New SqlParameter("pagAtual", 1))

        banco.ExecuteAndReturnData("sp_internet_movimentos", "tabExtrato")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                GerarXls(banco.tabela, dataInicial, datafinal)
                lResult = ExcelEmpty()
            Else
                lResult = ExcelError()
            End If
        Else
            lResult = ExcelEmpty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function
    Public Function GetSaldoProjeto(projeto As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))

        banco.ExecuteAndReturnData("sp_internet_Saldo_Projeto", "tabSaldoProjeto")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function
    Public Function GetSaldoContas(coordenador As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_internet_saldos_contas", "tabSaldoContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function
    Public Function GetAnaliseContas(coordenador As Integer, conta As String, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("contaMae", conta))

        banco.ExecuteAndReturnData("sp_internet_Analise_Contas", "tabAnaliseContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function
    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 0 id, 0 projeto, ' ' nome, getdate() as data, ' ' as texto, 0 as receita, 0 as despesa, 0 as saldo"))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
    Function ExcelEmpty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select '{0}' nomeArquivo", NomeArquivoPlanilha))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
    Function ExcelError() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 'erro' nomeArquivo", NomeArquivoPlanilha))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
#End Region

    Private Function GetNullable(Of T)(dataobj As Object) As T
        If Convert.IsDBNull(dataobj) Then
            Return Nothing
        Else
            Return CType(dataobj, T)

        End If

    End Function

#Region "Excel"
    Private Sub GerarXls(tabela As DataTable, inicio As String, fim As string)
        Dim ws As ISheet
        Dim rec As DataRow
        Dim linha As Integer = 7
        Dim projeto As string
        Dim _doubleCellStyle as ICellStyle
        Dim dataFormatCustom As IDataFormat

        Dim fs As FileStream = New FileStream(System.Web.HttpContext.Current.Server.MapPath("\templates\padrao_extrato.xls"), FileMode.Open, FileAccess.Read)
        workbook = New HSSFWorkbook(fs)
        _doubleCellStyle = workbook.CreateCellStyle()
        _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
         dataFormatCustom  = workbook.CreateDataFormat()
        ws = workbook.GetSheetAt(0)
        rec = tabela.Rows(0)
        ws.GetRow(3).GetCell(1).SetCellValue(rec("nome").ToString)
        ws.GetRow(4).GetCell(1).SetCellValue(inicio)
        ws.GetRow(4).GetCell(3).SetCellValue(fim)
        projeto = rec("nome").ToString

        For Each r As DataRow In tabela.Rows
            
            ws.CreateRow(linha)
            ws.GetRow(linha).CreateCell(0).SetCellValue(Date.Parse(r("data").ToString))
            ws.GetRow(linha).GetCell(0).CellStyle.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy")
            ws.GetRow(linha).CreateCell(1).SetCellValue(r("texto").ToString)
            ws.GetRow(linha).CreateCell(8).SetCellValue(Double.Parse(r("receita").ToString))
            ws.GetRow(linha).GetCell(8).SetCellType(CellType.Numeric)
            ws.GetRow(linha).GetCell(8).CellStyle = _doubleCellStyle
            ws.GetRow(linha).CreateCell(9).SetCellValue(Double.Parse(r("despesa").ToString))
            ws.GetRow(linha).GetCell(9).SetCellType(CellType.Numeric)
            ws.GetRow(linha).GetCell(9).CellStyle = _doubleCellStyle
            ws.GetRow(linha).CreateCell(10).SetCellValue(Double.Parse(r("saldo").ToString))
            ws.GetRow(linha).GetCell(10).SetCellType(CellType.Numeric)
            ws.GetRow(linha).GetCell(10).CellStyle = _doubleCellStyle
            ws.GetRow(linha).Height = 600
            linha = linha + 1
        Next
        SalvarPlanilha(workbook, projeto)
        fs.Close()
    End Sub

    Private sub SalvarPlanilha(workbook as HSSFWorkbook, projeto As string)
        dim dldir As String = AppDomain.CurrentDomain.BaseDirectory + "Download\\excel\\"
        dim uldir As string = AppDomain.CurrentDomain.BaseDirectory + "Upload\\default_export_file\\"
        NomeArquivoPlanilha = "extrato_projeto_" + projeto.Replace(" ", String.Empty).Replace("/", String.Empty).Replace("\", String.Empty) + ".xls"
        Dim xfile As FileStream = new FileStream(Path.Combine(dldir, NomeArquivoPlanilha), FileMode.Create, System.IO.FileAccess.Write)
        workbook.Write(xfile)
        xfile.Close()
    End sub
#End Region


End Class
