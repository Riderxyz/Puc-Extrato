Imports System.Data.SqlClient
Imports Dominio
Imports Newtonsoft.Json

Public Class usuariosNegocios
#Region "Propriedades"
    Public Property usuario As Dominio.usuario
    Public Property ListaUsuarios As List(Of Dominio.usuario) = New List(Of Dominio.usuario)
    Public property nometabela As String ="tabUsuario"
#End Region

#Region "Processos"
    Public Function ValidarUsuario(_usuario As String, _senha As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", _usuario))
        banco.parametros.Add(New SqlParameter("pass", _senha))

        banco.ExecuteAndReturnData("sp_internet_ValidarUsuario", nometabela)
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                '  Mapear(banco.tabela)
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
        banco.CarregarTabela(String.Format("SELECT 0 as Coordenador, '  ' as Nome , ' ' as senha"), nometabela )
        MapearUsuario(banco.tabela)
        usuario.conectado = False
        lResult = banco.GetJsonTabela
        Return lResult

    End Function

    Sub MapearUsuario(tabela As DataTable)
        ListaUsuarios = New List(Of usuario)
        usuario = New Dominio.usuario
        For Each r As DataRow In tabela.Rows
            usuario.coordenador = r("Coordenador")
            usuario.nome = r("nome")
            'usuario.descricao = r("descricao")
            usuario.conectado = True
        Next
    End Sub
#End Region

    #Region "Atualizar Coordenadores"
    Public function GravarCoordenador(coordenador As Integer, nome As String, email As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("email", email))
        banco.parametros.Add(New SqlParameter("nome", nome))

        banco.ExecuteAndReturnData("sp_internet_CoordenadorAtualizar", nometabela)
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                '  Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End function
#End Region
End Class
