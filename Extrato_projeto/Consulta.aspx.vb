Imports System.Data.SqlClient
Imports Negocio.clBanco
Public Class Consulta
    Inherits Page
    Public Property DataInicial As Date
    Public Property DataFinal As Date
    Public Property codigoProjeto As String


    Public Sub btnExtratoProjeto_Click(sender As Object, e As EventArgs) Handles btExtratoProjetos.ServerClick
        PanelExtrato.Visible = True

    End Sub
    Sub CarregarComboProjetos()

        Dim banco As Negocio.clBanco = New Negocio.clBanco
        banco.CarregarTabela("SELECT Codigo, Projeto, Nome, Nm_Cord_Projeto  FROM gif_vw_proj_faturamento WHERE     (CHARINDEX('ENCERRADOS', Nm_Cord_Projeto) = 0)")
        comboProjetos.DataSource = banco.tabela
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then

            gridProjetos.DataSource = Session("ds")
            gridProjetos.DataBind()
        End If
        CarregarComboProjetos()
    End Sub
    Protected Sub comboProjetos_ValueChanged(sender As Object, e As EventArgs) Handles comboProjetos.ValueChanged
        CarregarMovimentos(comboProjetos.SelectedItem.Value, dtInicio.Value, dtFinal.Value)
    End Sub

    Protected Sub ASPxMenu1_ItemClick(source As Object, e As DevExpress.Web.MenuItemEventArgs) Handles ASPxMenu1.ItemClick

    End Sub
End Class