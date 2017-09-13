Imports System.Data.SqlClient

Public Class coordenadorNegocio
    Public banco As clBanco = New clBanco
#Region "Metodos"
    Public Function GetCoordenadorLista(Optional nome As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_CoordenadorList", "tabcoordenador")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function
    Public Function Coordenador_ProjetosListar(Optional nome As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_Coordenador_ProjetosListar")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function

    Public Function GetCoordenadorById(id As Integer) As String
        Dim lResult As String

        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("id", id))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_GetCoordenadorById", "tabcoordenador")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function

    Public Function GetCoordenadorIncluir(nome As String, email As String, senha As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.parametros.Add(New SqlParameter("email", email))
        banco.parametros.Add(New SqlParameter("senha", senha))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_CoordenadorIncluir", "tabcoordenador")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function

    Public Function GetCoordenadorUpdate(id As Integer, Optional nome As String = "", Optional email As String = "", Optional senha As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("id", id))
        If (nome <> "") Then
            banco.parametros.Add(New SqlParameter("nome", nome))
        End If

        If (email <> "") Then
            banco.parametros.Add(New SqlParameter("email", email))
        End If

        If (senha <> "") Then
            banco.parametros.Add(New SqlParameter("senha", senha))
        End If

        banco.ExecuteAndReturnData("sp_CtrlProjetos_CoordenadorAtualizar", "tabcoordenador")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function

    Public Function CoordenadorExcluir(id As Integer) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("codigo", id))

        banco.ExecuteAndReturnData("sp_CtrlProjetos_CoordenadorExcluir", "tabcoordenador")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty()
        End If
        Return lResult
    End Function


#End Region

#Region "Emptys functions"
    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 0 coordenador, ' ' Nome, ' ' senha "))
        lResult = banco.GetJsonTabela
        Return lResult
    End Function

#End Region

End Class
