
Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        Dim a As wsExtrato.Service1SoapClient = New wsExtrato.Service1SoapClient()
        If (a.ValidarCoordenador(edUsuario.Text, edSenha.Text).Count > 0) Then
            Session("coordenador") = edUsuario.Text
            Response.Redirect("/consulta.aspx")
        Else

        End If
    End Sub
End Class