Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports System.Web

Public Class ExtratoNegocios
#Region "Propriedades"
    'Property extrato  As Dominio.extratoNegocios
    'Property ListaProjetos As List(Of Dominio.projeto)
    Private _ifont As IFont
    Private _nomeprojeto As String
    Private _nomeconta As String
    Public Property wb As HSSFWorkbook
    Public Property NomeArquivoPlanilha As String = ""
    Public ReadOnly CaracteresValidos As String = " abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789_-#"
    Public Property workbook As IWorkbook
    Public ReadOnly Property FontePadrao As IFont
        Get
            _ifont = wb.CreateFont()
            _ifont.FontHeightInPoints = 11
            _ifont.FontName = "Verdana"
            _ifont.Boldweight = CType(FontBoldWeight.Normal, Short)
            Return _ifont
        End Get
    End Property
    Public Property NomeProjeto As String
        Get
            Dim result As String = ""
            For Each c In _nomeprojeto
                If (CaracteresValidos.IndexOf(c) <> -1) Then
                    result = result + c
                End If
            Next
            Return result
        End Get
        Set(value As String)
            _nomeprojeto = value
        End Set
    End Property
    Public Property NomeConta As String
        Get
            Dim result As String = ""
            For Each c In _nomeconta
                If (CaracteresValidos.IndexOf(c) <> -1) Then
                    result = result + c
                End If
            Next
            Return result
        End Get
        Set(value As String)
            _nomeconta = value
        End Set
    End Property
    Public ReadOnly Property NomeArquivoExtrato As String
        Get
            Return String.Format("Extrato de projetos - {0}.xls", NomeProjeto)
        End Get
    End Property
    Public ReadOnly Property NomeArquivoSaldoProjetos As String
        Get
            Return "Total das Contas.xls "
        End Get
    End Property

    Public ReadOnly Property NomeArquivoAnaliseContas As String
        Get
            Return String.Format("Saldo dos Projetos - {0}.xls", NomeConta.Trim)
        End Get
    End Property



    'Public Property estiloNumero As HSSFCellStyle = CType(workbook.CreateCellStyle, HSSFCellStyle)

    ' Public Property dirplanilha As String = Server.MapPath("~\App_data\planilha") + "\planilha.xls"

#End Region

#Region "Get Extrato Projeto"
    Public Function GetExtrato(projeto As Integer, dataInicial As String, datafinal As String, pagina As Int16, pagina_tamanho As Int16) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
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
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("di", dataInicial))
        banco.parametros.Add(New SqlParameter("df", datafinal))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))
        banco.parametros.Add(New SqlParameter("pagTamanho", 99999))
        banco.parametros.Add(New SqlParameter("pagAtual", 1))

        banco.ExecuteAndReturnData("sp_internet_movimentos", "tabExtrato")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                GerarXlsExtrato(banco.tabela, dataInicial, datafinal)
                lResult = ExcelEmpty(NomeArquivoExtrato)
            Else
                lResult = ExcelError("Erro na geração do arquivo")
            End If
        Else
            lResult = ExcelEmpty(NomeArquivoExtrato)
        End If
        Return lResult
    End Function
#End Region

#Region "Get Saldo dos Projetos"
    Public Function GetSaldoProjeto(projeto As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
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
    Public Function GetSaldoProjetoExcel(coordenador As Integer, data As String, conta As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_internet_saldos_contas", "tabSaldoContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                GetSaldoProjetoExcel(coordenador, data, conta)
                lResult = ExcelEmpty(NomeArquivoSaldoProjetos)
            Else
                lResult = ExcelError("Erro na geracao do arquivo")
            End If
        Else
            lResult = ExcelEmpty(NomeArquivoSaldoProjetos) ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

#End Region

#Region "Get Saldo Contas"
    Public Function GetSaldoContas(coordenador As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
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
    Public Function GetSaldoContasExcel(coordenador As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_internet_saldos_contas", "tabSaldoContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                GerarXlsSaldoContas(banco.tabela, data, coordenador)
                lResult = ExcelEmpty(NomeArquivoSaldoProjetos)
            Else
                lResult = ExcelError("Erro na geracao do arquivo")
            End If
        Else
            lResult = ExcelEmpty(NomeArquivoSaldoProjetos)
        End If
        Return lResult
    End Function

#End Region

#Region "Get Detalhes das contas analisadas"
    Public Function GetAnaliseContas(coordenador As Integer, conta As String, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
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
    Public Function GetAnaliseContasExcel(coordenador As Integer, conta As String, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("contaMae", conta))

        banco.ExecuteAndReturnData("sp_internet_Analise_Contas", "tabAnaliseContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                GerarXlsAnaliseContas(banco.tabela, data, conta)
                lResult = ExcelEmpty(NomeArquivoAnaliseContas)
            Else
                lResult = ExcelError("Erro na geracao do arquivo")
            End If
        Else
            lResult = ExcelEmpty(NomeArquivoAnaliseContas) ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function
#End Region

#Region "Emptys functions"
    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 0 id, 0 projeto, ' ' nome, getdate() as data, ' ' as texto, 0 as receita, 0 as despesa, 0 as saldo"))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
    Function ExcelEmpty(nomeArquivo As String) As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select '{0}' nomeArquivo", nomeArquivo))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
    Function ExcelError(nomeArquivo As String) As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 'erro' nomeArquivo", nomeArquivo))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
#End Region

#Region "Excel"
    Private Sub GerarXlsExtrato(tabela As DataTable, inicio As String, fim As String)
        Dim ws As ISheet
        Dim rec As DataRow
        Dim linha As Integer = 5
        Dim projeto As String
        Dim _doubleCellStyle As ICellStyle
        Dim dataFormatCustom As IDataFormat

        Dim fs As FileStream = New FileStream(System.Web.HttpContext.Current.Server.MapPath("\templates\padrao_extrato.xls"), FileMode.Open, FileAccess.Read)
        workbook = New HSSFWorkbook(fs)
        wb = workbook
        _doubleCellStyle = workbook.CreateCellStyle()
        _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        dataFormatCustom = workbook.CreateDataFormat()
        ws = workbook.GetSheetAt(0)
        rec = tabela.Rows(0)
        ws.GetRow(2).GetCell(1).SetCellValue(rec("nome").ToString)
        ws.GetRow(3).GetCell(1).SetCellValue(Date.Parse(inicio).ToString("dd/MM/yyyy") + " a " + Date.Parse(fim).ToString("dd/MM/yyyy"))
        'ws.GetRow(3).GetCell(3).SetCellValue(Date.Parse(fim))
        projeto = rec("nome").ToString
        NomeProjeto = projeto
        For Each r As DataRow In tabela.Rows
            ws.GetRow(linha).GetCell(0).SetCellValue(Date.Parse(r("data").ToString))
            ws.GetRow(linha).GetCell(2).SetCellValue(r("texto").ToString)
            ws.GetRow(linha).GetCell(1).SetCellValue(r("fatura").ToString)
            If (linha <> 5) Then
                ws.GetRow(linha).GetCell(3).SetCellValue(Convert.ToDouble(r("receita").ToString))
                ws.GetRow(linha).GetCell(4).SetCellValue(Convert.ToDouble(r("despesa").ToString))
            End If


            ws.GetRow(linha).GetCell(5).SetCellValue(Convert.ToDouble(r("saldo").ToString))
            linha = linha + 1
        Next
        wb.SetPrintArea(0, 0, 5, 0, linha)
        ws.FitToPage = False
        ws.ForceFormulaRecalculation = True
        SalvarPlanilha(workbook, NomeArquivoExtrato)
        fs.Close()
    End Sub
    Private Sub GerarXlsSaldoContas(tabela As DataTable, data As String, coordenador As Int32)
        Dim objConta As Negocio.contaNegocio = New contaNegocio
        Dim ws As ISheet
        Dim rec As DataRow
        Dim linha As Integer = 7
        Dim _doubleCellStyle As ICellStyle
        Dim dataFormatCustom As IDataFormat

        Dim fs As FileStream = New FileStream(System.Web.HttpContext.Current.Server.MapPath("\templates\padrao_saldo_contas.xls"), FileMode.Open, FileAccess.Read)
        workbook = New HSSFWorkbook(fs)
        wb = workbook
        NomeConta = coordenador.ToString()

        _doubleCellStyle = workbook.CreateCellStyle()
        _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        dataFormatCustom = workbook.CreateDataFormat()
        ws = workbook.GetSheetAt(0)
        rec = tabela.Rows(0)

        ws.GetRow(4).GetCell(1).SetCellValue(Date.Parse(data))
        ws.GetRow(4).GetCell(1).CellStyle.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy")
        For Each r As DataRow In tabela.Rows
            ws.CreateRow(linha)
            ws.GetRow(linha).CreateCell(0).SetCellValue(r("descricao").ToString)
            ws.GetRow(linha).GetCell(0).SetCellType(CellType.String)
            ws.GetRow(linha).CreateCell(8).SetCellValue(Double.Parse(r("saldo").ToString))
            ws.GetRow(linha).GetCell(8).SetCellType(CellType.Numeric)
            ws.GetRow(linha).GetCell(8).CellStyle = _doubleCellStyle
            ws.GetRow(linha).GetCell(0).CellStyle.SetFont(FontePadrao)
            ws.GetRow(linha).GetCell(8).CellStyle.SetFont(FontePadrao)
            ws.GetRow(linha).Height = 600
            linha = linha + 1
        Next
        ws.ForceFormulaRecalculation = True
        SalvarPlanilha(workbook, NomeArquivoSaldoProjetos)
        fs.Close()
    End Sub
    Private Sub GerarXlsAnaliseContas(tabela As DataTable, data As String, conta As String)
        Dim objConta As Negocio.contaNegocio = New contaNegocio
        Dim ws As ISheet
        Dim rec As DataRow
        Dim linha As Integer = 7
        Dim _doubleCellStyle As ICellStyle
        Dim dataFormatCustom As IDataFormat

        Dim fs As FileStream = New FileStream(System.Web.HttpContext.Current.Server.MapPath("\templates\padrao_Saldo_Projetos.xls"), FileMode.Open, FileAccess.Read)
        workbook = New HSSFWorkbook(fs)
        wb = workbook

        NomeConta = objConta.getConta(conta).nome
        _doubleCellStyle = workbook.CreateCellStyle()
        _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00")
        dataFormatCustom = workbook.CreateDataFormat()
        ws = workbook.GetSheetAt(0)
        rec = tabela.Rows(0)

        ws.GetRow(1).GetCell(0).SetCellValue("CONTROLE DE PROJETOS - SALDO DOS PROJETOS")
        ws.GetRow(3).GetCell(1).SetCellValue(NomeConta)
        ws.GetRow(4).GetCell(1).SetCellValue(Date.Parse(data))
        ws.GetRow(4).GetCell(1).CellStyle.DataFormat = dataFormatCustom.GetFormat("dd/MM/yyyy")
        For Each r As DataRow In tabela.Rows
            ws.CreateRow(linha)
            ws.GetRow(linha).CreateCell(0).SetCellValue(r("nomeprojeto").ToString)
            ws.GetRow(linha).GetCell(0).SetCellType(CellType.String)
            ws.GetRow(linha).CreateCell(8).SetCellValue(Double.Parse(r("saldo").ToString))
            ws.GetRow(linha).GetCell(8).SetCellType(CellType.Numeric)
            ws.GetRow(linha).GetCell(8).CellStyle = _doubleCellStyle
            ws.GetRow(linha).GetCell(0).CellStyle.SetFont(FontePadrao)
            ws.GetRow(linha).GetCell(8).CellStyle.SetFont(FontePadrao)
            ws.GetRow(linha).Height = 600
            linha = linha + 1
        Next
        ws.ForceFormulaRecalculation = True
        SalvarPlanilha(workbook, NomeArquivoAnaliseContas)
        fs.Close()
    End Sub

    Private Sub SalvarPlanilha(workbook As HSSFWorkbook, nomeArquivo As String)
        Dim dldir As String = AppDomain.CurrentDomain.BaseDirectory + "Download\\excel\\"
        Dim uldir As String = AppDomain.CurrentDomain.BaseDirectory + "Upload\\default_export_file\\"
        NomeArquivoPlanilha = nomeArquivo
        Dim xfile As FileStream = New FileStream(Path.Combine(dldir, NomeArquivoPlanilha), FileMode.Create, System.IO.FileAccess.Write)
        workbook.Write(xfile)
        xfile.Close()
    End Sub
#End Region

#Region "Outros acesso a banco"
    Private Function GetNullable(Of T)(dataobj As Object) As T
        If Convert.IsDBNull(dataobj) Then
            Return Nothing
        Else
            Return CType(dataobj, T)

        End If

    End Function
#End Region

End Class
