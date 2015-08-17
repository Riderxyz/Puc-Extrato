Imports System.Data.SqlClient
Imports Negocio.clBanco
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Public Class Consulta
    Inherits System.Web.UI.Page
    'Public Property DataInicial As Date
    'Public Property DataFinal As Date
    'Public Property Projeto As String
    Public Property Coordenador As String
    Public Property Conta As String
    Dim ws As wsExtrato.Service1SoapClient = New wsExtrato.Service1SoapClient

    Public Sub btnExtratoProjeto_Click(sender As Object, e As EventArgs) Handles btExtratoProjetos.ServerClick
        PanelExtrato.Visible = True
        PanelMovimentoConta.Visible = False
        Session("Modulo") = "projeto".ToUpper
        CarregarComboProjetos()
        CarregarMovimentos()
    End Sub

    Public Sub btbtMovimentoContas_Click(sender As Object, e As EventArgs) Handles btMovimentoContas.ServerClick
        PanelExtrato.Visible = False
        PanelMovimentoConta.Visible = True
        Session("Modulo") = "Conta".ToUpper
        CarregarComboContas()
    End Sub
    Sub CarregarComboProjetos()
        comboProjetos.DataSource = ws.LIstarProjetos(Coordenador)
        comboProjetos.TextField = "Projeto"
        comboProjetos.ValueField = "Codigo"
        comboProjetos.DataBind()
    End Sub
    Sub CarregarComboContas()
        ComboContas.DataSource = ws.GetListOf_Vw_contamae(Coordenador)
        ComboContas.TextField = "descricaoContaMae"
        ComboContas.ValueField = "conta_mae"
        ComboContas.DataBind()
    End Sub


    Sub CarregarMovimentos()
        If Not IsNothing(comboProjetos.SelectedItem) Then
            Dim banco As Negocio.clBanco = New Negocio.clBanco
            Dim comando As StringBuilder = New StringBuilder
            banco.CarregarTabela(String.Format(" EXEC	[dbo].[sp_internet_movimentos] @Di = '{0}',  @Df = '{1}',  @cdProjeto= '{2}'", dtInicio.Date.ToString("dd/MM/yyyy"), dtFinal.Date.ToString("dd/MM/yyyy"), comboProjetos.SelectedItem.Value))
            gridProjetos.DataSource = banco.tabela
            Session("ds") = banco.tabela
            gridProjetos.KeyFieldName = "id"
            gridProjetos.DataBind()
        End If
    End Sub
    Sub CarregarMovimentosConta(conta As String, datainicio As Date, datafinal As Date)
        Dim banco As Negocio.clBanco = New Negocio.clBanco
        Dim comando As StringBuilder = New StringBuilder
        banco.CarregarTabela("SELECT * FROM View_extrato2 where conta_mae = '" + conta.ToString.Trim + "' and data >= cast('" + datainicio.ToString("yyyy/MM/dd") + "' as datetime) and data <= cast('" + datafinal.ToString("yyyy/MM/dd") + "' as datetime)")
        Session("ds") = banco.tabela
        gridMovimentoContas.DataSource = banco.tabela ' ws.GetRangeOf_View_extrato2(Coordenador, ComboContas.SelectedItem.Value.ToString, datainicio.ToString("dd/MM/yyyy"), datafinal.ToString("dd/MM/yyyy"), titulo)
        gridMovimentoContas.KeyFieldName = "Codigo_Lancamento"
        gridMovimentoContas.DataBind()
    End Sub


    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Coordenador = Session("coordenador")
            'If IsNothing(comboProjetos.SelectedItem) Then
            '    Projeto = -1
            'Else
            '    Projeto = comboProjetos.SelectedItem.Value
            'End If
            If IsNothing(ComboContas.SelectedItem) Then
                Conta = -1
            Else
                Conta = ComboContas.SelectedItem.Value
            End If
            'If IsNothing(dtFinal.Date) Then
            '    DataFinal = Now
            'Else
            '    DataInicial = Now
            'End If
        Catch ex As Exception
            Coordenador = -1
            Response.Redirect("/default.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Page.IsPostBack Then
        If (Session("modulo") = "projeto".ToUpper) Then
            gridProjetos.DataSource = Session("ds")
            gridProjetos.DataBind()
        Else
            gridMovimentoContas.DataSource = Session("ds")
            gridMovimentoContas.DataBind()
        End If
        'End If

    End Sub
    Protected Sub comboProjetos_ValueChanged(sender As Object, e As EventArgs) Handles comboProjetos.ValueChanged

        CarregarMovimentos()
        gridProjetos.Caption = comboProjetos.SelectedItem.Text
        gridProjetos.SettingsText.Title = "PROJETO " + comboProjetos.SelectedItem.Text + " Período " + dtInicio.Text + " a " + dtFinal.Text
        'gridProjetos.DataBind()
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

    Protected Sub ComboContas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboContas.SelectedIndexChanged
        CarregarMovimentos()
    End Sub

    Protected Sub gridProjetos_DataBinding(sender As Object, e As EventArgs) Handles gridProjetos.DataBinding
        If IsNothing(ComboContas.SelectedItem) Then
            CarregarMovimentosConta(-1, dtInicio.Value, dtFinal.Value)
        Else
            CarregarMovimentosConta(ComboContas.SelectedItem.Value, dtInicio.Value, dtFinal.Value)
        End If
    End Sub

End Class