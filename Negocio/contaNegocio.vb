﻿Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient

Public Class contaNegocio
#Region "Propriedades"
    Public property conta As Dominio.conta = New Dominio.conta
    Public listaConta As List(Of Dominio.conta) = New List(Of Dominio.conta)
#End Region

#Region "Processos"
    Public function GetContas(coordenador As int32) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.ExecuteAndReturnData("sp_internet_contasGet", "tabContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End function

    Public function GetContasMae(coordenador As int32) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.ExecuteAndReturnData("sp_internet_contasMaeGet", "tabContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End function

    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("SELECT 0 conta, '' descricao, '' contaReceita, '' descricaoReceita"))
        lResult = banco.GetJsonTabela
        Return lResult
    End Function

    Public function getConta(conta As string) As Dominio.conta
        Dim banco As clBanco = New clBanco
        banco.CarregarTabela(String.Format("SELECT * FROM PLANO_DE_CONTAS WHERE CONTA = '{0}'", conta))
        Dim _conta As New Dominio.conta
        If (banco.tabela.Rows.Count > 0) Then
            _conta.conta = banco.tabela.Rows(0)("conta").tostring
            _conta.nome = banco.tabela.Rows(0)("Descricao").tostring
        Else
            _conta.conta = conta
            _conta.nome = "Conta não encontrada no banco"
        End If
        Return _conta

    End function
#End Region
End Class
