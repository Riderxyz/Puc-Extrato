Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient

Public Class ExtratoNegocios
#Region "Propriedades"
    'Property extrato  As Dominio.extratoNegocios
    'Property ListaProjetos As List(Of Dominio.projeto)
#End Region

#Region "Processos"
    Public Function GetExtrato(projeto As Integer, dataInicial As String, datafinal As string, pagina As Int16, pagina_tamanho As int16) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("di", datainicial))
        banco.parametros.Add(New SqlParameter("df", datafinal))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))
        banco.parametros.Add(New SqlParameter("pagTamanho", pagina_tamanho))
        banco.parametros.Add(New SqlParameter("pagAtual", pagina))

        banco.ExecuteAndReturnData("sp_internet_movimentos", "tabExtrato")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

    Public Function GetSaldoProjeto(projeto As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("cdProjeto", projeto))

        banco.ExecuteAndReturnData("sp_internet_Saldo_Projeto", "tabSaldoProjeto")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

    Public Function GetSaldoContas(coordenador As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_internet_saldos_contas", "tabSaldoContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

    Public Function GetAnaliseContas(coordenador As Integer, conta As String, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("data", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("contaMae", conta))

        banco.ExecuteAndReturnData("sp_internet_Analise_Contas", "tabAnaliseContas")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                'Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function




    'Sub Mapear(tabela As DataTable)
    '    ListaProjetos = New List(Of Dominio.projeto)
    '    projeto = New Dominio.projeto
    '    For Each r As DataRow In tabela.Rows
    '        projeto.codigo = GetNullable(Of Integer)(r("codigo"))
    '        projeto.coordenador = GetNullable(Of Integer)(r("coordenador"))
    '        projeto.despesa = GetNullable(Of Double)(r("despesa"))
    '        projeto.receita = GetNullable(Of Double)(r("receita"))
    '        projeto.nome = GetNullable(Of String)(r("projeto"))
    '        ListaProjetos.Add(projeto)
    '    Next
    'End Sub

    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 0 id, 0 projeto, ' ' nome, getdate() as data, ' ' as texto, 0 as receita, 0 as despesa, 0 as saldo"))
        'Mapear(banco.tabela)
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
#End Region

    Private Function GetNullable(Of T)(dataobj As Object) As T
        If Convert.IsDBNull(dataobj) Then
            Return Nothing
        Else
            Return CType(dataobj, T)

        End If

    End Function

End Class
