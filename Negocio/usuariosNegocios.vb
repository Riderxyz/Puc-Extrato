Imports Dominio
Imports Newtonsoft.Json

Public Class usuariosNegocios
#Region "Propriedades"
    Public Property usuario As Dominio.usuario
    Public Property ListaUsuarios As List(Of Dominio.usuario) = New List(Of Dominio.usuario)
#End Region

#Region "Processos"
    Public Function ValidarUsuario(_usuario As String, _senha As String) As String
        Dim lResult As String
        If String.IsNullOrEmpty(_usuario) OrElse String.IsNullOrEmpty(_senha) Then
            Return ""
        Else
            Dim banco As clBanco = New clBanco
            banco.CarregarTabela(String.Format("SELECT [Coordenador], [Nome] ,[senha] FROM vw_Int_Coordenador WHERE Coordenador = {0} and SENHA = {1}", _usuario, _senha))
            If (Not IsNothing(banco.tabela)) Then
                if (banco.tabela.Rows.Count > 0) Then
                    MapearUsuario(banco.tabela)
                    usuario.conectado = banco.tabela.Rows.Count
                    lResult = banco.GetJsonTabela
                Else
                    lResult = EmptyUsuario()
                End If
            Else
                lResult = EmptyUsuario() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
            End If
        End If
        Return lResult
    End Function

    Function EmptyUsuario() As string
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("SELECT 0 as Coordenador, '  ' as Nome , ' ' as senha"))
        MapearUsuario(banco.tabela)
        usuario.conectado = false
        lResult = banco.GetJsonTabela
        Return lResult

    End Function

    Sub MapearUsuario(tabela As DataTable)
        ListaUsuarios = New List(Of usuario)
        usuario = New Dominio.usuario
        For Each r As DataRow In tabela.Rows
            usuario.Coordenador = r("Coordenador")
            usuario.nome = r("nome")
            'usuario.descricao = r("descricao")
            usuario.conectado = True
        Next
    End Sub
#End Region
End Class
