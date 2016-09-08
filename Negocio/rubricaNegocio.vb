Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient
Public Class rubricaNegocio
#Region "Propriedades"

#End Region
#Region "Processos"

    Public Function GetListaRubricas(optional nome As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("nome", nome))

        banco.ExecuteAndReturnData("sp_CtrlProjetos_RubricaLista", "tabRubricas")
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
    Public Function GetRubricasById(id As integer) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("rubrica", id))

        banco.ExecuteAndReturnData("sp_CtrlProjetos_RubricaListaById", "tabRubricas")
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
    Public Function GetRubricasIncluir(rubrica As Int32, descricao As String, tipo_rubrica As string) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("rubrica", rubrica))
        banco.parametros.Add(New SqlParameter("descricao", descricao))
        banco.parametros.Add(New SqlParameter("tipo_rubrica", tipo_rubrica))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_RubricaIncluir", "tabrubrica")
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
    Public Function GetRubricasExcluir(rubrica As Int32) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("rubrica", rubrica))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_RubricaExcluir", "tabrubrica")
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
    Public Function GetRubricasAtualizar(rubrica As Int32, descricao As String, tipo_rubrica As string) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("rubrica", rubrica))
        banco.parametros.Add(New SqlParameter("descricao", descricao))
        banco.parametros.Add(New SqlParameter("tipo_rubrica", tipo_rubrica))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_RubricaAtualizar", "tabrubrica")
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

    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela("SELECT -1 [Rubrica] ,null [Descricao]  ,null [Tipo_Rubrica]")
        lResult = banco.GetJsonTabela
        Return lResult
    End Function
#End Region
End Class
