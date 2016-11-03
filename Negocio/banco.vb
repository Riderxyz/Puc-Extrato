
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization
Imports System.Collections.Specialized

Public Class clBanco
#Region "Inicialização"
    Public Sub New(Optional ByVal nomeBanco As string = "FPLF_DES")
        NomeDB = nomeBanco
    End Sub


#End Region

#Region "Propriedades"

    Public Property TipoConexao As Integer = CType(ConfigurationSettings.AppSettings("TipoConexao"), Integer)
    Public Property TipoAmbiente As Integer = 0
    Public property Catalogo As String = ""
    Public property NomeDB As string
    Public function GetDbName(db as string) as string
        Dim appSettings As NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        Return appSettings.Get(db)
    End function

    Public ReadOnly Property ConectionString As String
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
            Dim conf As String = System.Configuration.ConfigurationManager.ConnectionStrings("FPLFConnection").ConnectionString
            Dim schemaName As String = GetDbName(Nomedb)
            Return conf.Replace("NOME_DO_BANCO", schemaName)
        End Get
    End Property
    Public Property Comando As StringBuilder
    Public Property parametros As List(Of SqlParameter) = New List(Of SqlParameter)
    Public Property UltimoErro As StringBuilder
    'Public Property log As LogEvent.log = New LogEvent.log
    Public Property adapter As SqlDataAdapter
    Public Property ds As System.Data.DataSet
    Public Property tabela As System.Data.DataTable
    Public Property Conexao As SqlConnection
    Public Property OraCommand As SqlCommand


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
            OraCommand = New SqlCommand(comando, Conexao)
            Conexao.Open()
            result = OraCommand.ExecuteNonQuery()
            ' log Logging.Log.listLog.Add(New LogEvent.baseLog())
        Else
            result = -1
        End If
        Return (result = 0)
    End Function
    ''' <summary>
    ''' Executa um comando exec e retorna os dados na tabela TABELA interna do objeto
    ''' </summary>
    ''' <param name="comando">Comando a ser executado</param>

    Public Sub ExecuteAndReturnData(comando As String, Optional tablename As String = "tabela")
        If (comando <> "") Then
            Dim reader As SqlDataReader
            Conexao = New SqlConnection(ConectionString)
            OraCommand = New SqlCommand(comando, Conexao)
            If (parametros.Count > 0)
                For Each p In parametros
                    OraCommand.Parameters.Add(p)
                Next
            End If
            OraCommand.CommandType = CommandType.StoredProcedure
            Conexao.Open()
            reader = OraCommand.ExecuteReader()
            ds = New DataSet
            tabela = New DataTable
            tabela.TableName = tablename
            tabela.Load(reader)
            ds.Tables.Add(tabela)
            parametros.Clear  ' log Logging.Log.listLog.Add(New LogEvent.baseLog())
        End If

    End Sub

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
    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")> Public Function CarregarTabela(comando As String, Optional tablename As String = "tab1") As Boolean
        If comando <> "" Then
            Dim result As Integer = -1
            If (comando <> "") Then
                Conexao = New SqlConnection(ConectionString)
                OraCommand = New SqlCommand(comando, Conexao)
                adapter = New SqlDataAdapter(comando, Conexao)
                ds = New DataSet
                Try
                    adapter.Fill(ds, tablename)
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
                OraCommand = New SqlCommand(comando, Conexao)
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

#Region "Json"
    'kkk
    ' 'http://stackoverflow.com/questions/34272427/c-sharp-custom-json-using-json-net-from-dataset-or-datatable
    <System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "String.Format can be used")>
    Public Function GetJsonTabela() As String
        Dim jss As JsonSerializerSettings = New JsonSerializerSettings()
        jss.ContractResolver = New CamelCasePropertyNamesContractResolver()

        If (Not IsNothing(tabela))
            Return JsonConvert.SerializeObject(ds, Formatting.Indented, jss)
        ElseIf (Not IsNothing(ds.Tables(0)))
            Return JsonConvert.SerializeObject(ds.Tables(0), Formatting.Indented, jss)
        Else
            Return JsonConvert.SerializeObject("Sem dados na tabela", jss)
        End If

    End Function
#End Region

#Region "Check Null"
    Public Function GetNullable(Of T)(dataobj As Object) As T
        If Convert.IsDBNull(dataobj) Then
            Return Nothing
        Else
            Return CType(dataobj, T)

        End If

    End Function
#End Region

End Class

Class TiposConexaoBanco
    Public Property Desenvolvimento = 0
    Public Property Homologacao As Integer = 1
    Public Property Producao As Integer = 2
End Class