﻿
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Collections.Specialized

Public Class clBanco
#Region "Propriedades"
    Public Property TipoConexao As Integer = CType(ConfigurationSettings.AppSettings("TipoConexao"), Integer)
    Public Property TipoAmbiente As Integer = 0
    Public Shared ReadOnly Property ConectionString As String
        Get
            'Dim appSettings As NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
            'Dim ConexaoAtiva As String = appSettings.Get("ConexaoAtiva")
            'Dim conexao As String
            'If (ConexaoAtiva = 2) Then
            '    conexao = System.Configuration.ConfigurationManager.ConnectionStrings(appSettings.Get("ConexaoProducao")).ConnectionString
            'ElseIf (ConexaoAtiva = 1) Then
            '    conexao = System.Configuration.ConfigurationManager.ConnectionStrings(appSettings.Get("ConexaoProducao")).ConnectionString
            'ElseIf (ConexaoAtiva = 0) Then
            '    conexao = System.Configuration.ConfigurationManager.ConnectionStrings(appSettings.Get("ConexaoProducao")).ConnectionString
            'Else
            '    conexao = ""
            'End If
            'Return ConfigurationSettings.AppSettings.Set   Manager.ConnectionStrings("Plantao2014ConnectionString").ConnectionString
            Return System.Configuration.ConfigurationManager.ConnectionStrings("FPLFConnection").ConnectionString
        End Get
    End Property
    Public Property Comando As StringBuilder
    Public Property UltimoErro As StringBuilder
    'Public Property log As LogEvent.log = New LogEvent.log
    Public Property adapter As SqlDataAdapter
    Public Property ds As System.Data.DataSet
    Public Property tabela As System.Data.DataTable
    Public Property Conexao As SqlConnection
    Public Property OraCOmmand As SqlCommand

#End Region

#Region "Funções básicas"
    Public Function executarComando() As Boolean

        If Comando.ToString <> "" Then
            Return executarComando(Comando.ToString)
        Else
            Return False
        End If
    End Function
    Public Function executarComando(comando As StringBuilder) As Boolean

        If comando.ToString <> "" Then
            Return executarComando(comando.ToString)
        Else
            Return False
        End If
    End Function
    Public Function executarComando(comando As String) As Boolean


        Dim result As Integer = -1
        If (comando <> "") Then
            Conexao = New SqlConnection(ConectionString)
            OraCOmmand = New SqlCommand(comando, Conexao)
            Conexao.Open()
            result = OraCOmmand.ExecuteNonQuery()
            ' log Logging.Log.listLog.Add(New LogEvent.baseLog())
        Else
            result = -1
        End If
        Return (result = 0)
    End Function

    Public Function CarregarTabela() As Boolean

        If Comando.ToString <> "" Then
            Return CarregarTabela(Comando.ToString)
        Else
            Return False
        End If

    End Function
    Public Function CarregarTabela(comando As StringBuilder) As Boolean
        If comando.ToString <> "" Then
            Return CarregarTabela(comando.ToString)
        Else
            Return False
        End If
    End Function
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")> Public Function CarregarTabela(comando As String) As Boolean
        If comando <> "" Then
            Dim result As Integer = -1
            If (comando <> "") Then
                Conexao = New SqlConnection(ConectionString)
                OraCOmmand = New SqlCommand(comando, Conexao)
                adapter = New SqlDataAdapter(comando, Conexao)
                ds = New DataSet
                Try
                    adapter.Fill(ds, "tab1")
                    tabela = ds.Tables(0)
                    result = 0
                Catch ex As Exception
                    'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                    result = -1
                End Try
            Else
                result = -1
            End If
            Return (result = 0)
        Else
            Return False
        End If
    End Function

    Public Function CarregarDataset() As Boolean

        If Comando.ToString <> "" Then
            Return CarregarDataset(Comando.ToString)
        Else
            Return False
        End If

    End Function
    Public Function CarregarDataset(comando As StringBuilder) As Boolean
        If comando.ToString <> "" Then
            Return CarregarDataset(comando.ToString)
        Else
            Return False
        End If
    End Function
    Public Function CarregarDataset(comando As String) As Boolean
        If comando <> "" Then
            Dim result As Integer = -1
            If (comando <> "") Then
                Conexao = New SqlConnection(ConectionString)
                OraCOmmand = New SqlCommand(comando, Conexao)
                adapter = New SqlDataAdapter(comando, Conexao)
                ds = New DataSet
                Try
                    adapter.Fill(ds, "tab1")
                    result = 0
                Catch ex As Exception
                    'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
                    result = -1
                End Try
            Else
                result = -1
            End If
            Return (result = 0)
        Else
            Return False
        End If
    End Function

#End Region

End Class

Class TiposConexaoBanco
    Public Property Desenvolvimento = 0
    Public Property Homologacao As Integer = 1
    Public Property Producao As Integer = 2
End Class