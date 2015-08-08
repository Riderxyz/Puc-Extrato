Imports System.Data.SqlClient
Imports Negocio.clBanco
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Public Class Consulta
    Inherits System.Web.UI.Page
    Public Property DataInicial As Date
    Public Property DataFinal As Date
    Public Property codigoProjeto As String
    Public Property Coordenador As String
    Dim ws As wsExtrato.Service1SoapClient = New wsExtrato.Service1SoapClient


    Public Sub btnExtratoProjeto_Click(sender As Object, e As EventArgs) Handles btExtratoProjetos.ServerClick
        PanelExtrato.Visible = True

    End Sub
    Sub CarregarComboProjetos()

        Dim banco As Negocio.clBanco = New Negocio.clBanco
        'banco.CarregarTabela("SELECT Codigo, Projeto, Nome, Nm_Cord_Projeto  FROM gif_vw_proj_faturamento WHERE     (CHARINDEX('ENCERRADOS', Nm_Cord_Projeto) = 0)")
        comboProjetos.DataSource = ws.LIstarProjetos(Coordenador)
        comboProjetos.TextField = "Projeto"
        comboProjetos.ValueField = "Codigo"
        comboProjetos.DataBind()
    End Sub

    Sub CarregarMovimentos(projeto As String, datainicio As Date, datafinal As Date)
        Dim banco As Negocio.clBanco = New Negocio.clBanco
        Dim comando As StringBuilder = New StringBuilder
        banco.CarregarTabela(String.Format(" EXEC	[dbo].[sp_internet_movimentos] @Di = '{0}',  @Df = '{1}',  @cdProjeto= '{2}'", datainicio.ToString("dd/MM/yyyy"), datafinal.ToString("dd/MM/yyyy"), projeto))
        gridProjetos.DataSource = banco.tabela
        Session("ds") = banco.tabela
        gridProjetos.KeyFieldName = "id"

        gridProjetos.DataBind()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Coordenador = Session("coordenador")
        Catch ex As Exception
            Coordenador = -1
            Response.Redirect("/default.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then

            gridProjetos.DataSource = Session("ds")
            gridProjetos.DataBind()
        End If
        CarregarComboProjetos()
    End Sub
    Protected Sub comboProjetos_ValueChanged(sender As Object, e As EventArgs) Handles comboProjetos.ValueChanged
        CarregarMovimentos(comboProjetos.SelectedItem.Value, dtInicio.Value, dtFinal.Value)
        gridProjetos.Caption = comboProjetos.SelectedItem.Text
        gridProjetos.SettingsText.Title = "PROJETO " + comboProjetos.SelectedItem.Text + " Período " + dtInicio.Text + " a " + dtFinal.Text
    End Sub
    Protected Sub UpdateExportMode()
        '  gridProjetos.SettingsDetail.ExportMode = CType(System.Enum.Parse(GetType(GridViewDetailExportMode), ddlExportMode.SelectedItem.Value), GridViewDetailExportMode)
    End Sub
    Protected Sub ASPxMenu1_ItemClick(source As Object, e As DevExpress.Web.MenuItemEventArgs) Handles ASPxMenu1.ItemClick
        If e.Item.Name.ToLower = "xls".ToLower Then
            UpdateExportMode()
            Exporter.WriteXlsToResponse(New XlsExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})
        ElseIf e.Item.Name.ToLower = "xlsx".ToLower Then
            UpdateExportMode()
            Exporter.WriteXlsxToResponse(New XlsxExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})

        ElseIf e.Item.Name.ToLower = "pdf".ToLower Then
            UpdateExportMode()
            Exporter.WritePdfToResponse()
        ElseIf e.Item.Name.ToLower = "rtf".ToLower Then
            UpdateExportMode()
            Exporter.WriteRtfToResponse()
        ElseIf e.Item.Name.ToLower = "csv".ToLower Then
            UpdateExportMode()
            Exporter.WriteCsvToResponse(New CsvExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})
        End If
    End Sub

    Protected Sub dtFinal_DateChanged(sender As Object, e As EventArgs) Handles dtFinal.DateChanged

    End Sub
End Class