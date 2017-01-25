Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient

Public Class contaNegocio
#Region "Propriedades"
    Public property conta As Dominio.conta = New Dominio.conta
    Public listaConta As List(Of Dominio.conta) = New List(Of Dominio.conta)
#End Region

#Region "Processos"
    Public Function GetContasLista(Optional nome As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ContasLista", "tabcontas")
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
    Public Function GetContasIncluir(conta As string, nome as string) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("conta", conta))
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ContasIncluir", "tabcontas")
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
    Public Function GetContasAtualizar(id As Integer, conta As String, nome As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("id", id))
        banco.parametros.Add(New SqlParameter("conta", conta))
        banco.parametros.Add(New SqlParameter("descricao", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ContasAtualizar", "tabcontas")
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
    Public Function GetContasExcluir(id As Integer) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear()
        banco.parametros.Add(New SqlParameter("id", id))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ContasExcluir")
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
