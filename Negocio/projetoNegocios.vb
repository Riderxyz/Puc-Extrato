Imports Dominio
Imports Newtonsoft.Json
Imports System.Text
Imports System.Data.SqlClient

Public Class projetoNegocios
#Region "Propriedades"
    Property projeto As Dominio.projeto
    Property ListaProjetos As List(Of Dominio.projeto)
#End Region

#Region "Processos dos Serviços"
    Public Function GetProjetosLista(optional nome As String = "") As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ProjetosLista", "tabprojetos")
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
    Public Function GetProjetosIncluir(projeto As string, nome as string, descricao As String, inicio As Date, coordenador As Integer, contaPrincipal As String, tipo_Projeto As string) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("projeto", projeto))
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.parametros.Add(New SqlParameter("descricao", descricao))
        banco.parametros.Add(New SqlParameter("inicio", inicio))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("conta_Principal", contaPrincipal))
        banco.parametros.Add(New SqlParameter("tipo_projeto", tipo_Projeto))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ProjetosIncluir", "tabprojetos")
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
    Public Function GetProjetosAtualizar(codigo As Integer, projeto As string, nome as string, descricao As String, inicio As Date, coordenador As Integer, contaPrincipal As String, tipo_Projeto As string) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("codigo", codigo))
        banco.parametros.Add(New SqlParameter("projeto", projeto))
        banco.parametros.Add(New SqlParameter("nome", nome))
        banco.parametros.Add(New SqlParameter("descricao", descricao))
        banco.parametros.Add(New SqlParameter("inicio", inicio))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))
        banco.parametros.Add(New SqlParameter("conta_Principal", contaPrincipal))
        banco.parametros.Add(New SqlParameter("tipo_projeto", tipo_Projeto))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ProjetosAtualizar", "tabprojetos")
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
    Public Function GetProjetosExcluir(codigo As Integer) As string
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Clear
        banco.parametros.Add(New SqlParameter("codigo", codigo))
        banco.ExecuteAndReturnData("sp_CtrlProjetos_ProjetosExcluir", "tabprojetos")
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

#Region "Processos"
    Public Function GetProjetos(coordenador As Integer, data As String) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("datafim", data))
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_int_saldos", "tabProjetos")
        If (Not IsNothing(banco.tabela)) Then
            If (banco.tabela.Rows.Count > 0) Then
                Mapear(banco.tabela)
                lResult = banco.GetJsonTabela
            Else
                lResult = Empty()
            End If
        Else
            lResult = Empty() ' JsonConvert.SerializeObject(New Dominio.usuario With {.coordenador = 0, .senha = "", .nome = "", .descricao = "", .conectado = False, .status = ""})
        End If
        Return lResult
    End Function

    Public Function GetProjetos(coordenador As Integer) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_internet_ProjetosGet", "tabProjetos")
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

    Public Function GetListaProjetosCoordenador(coordenador As Integer) As String
        Dim lResult As String
        Dim banco As clBanco = New clBanco
        banco.parametros.Add(New SqlParameter("coordenador", coordenador))

        banco.ExecuteAndReturnData("sp_CtrlProjetos_GetProjetosCoordenador", "tabProjetos")
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

    Sub Mapear(tabela As DataTable)
        ListaProjetos = New List(Of Dominio.projeto)
        projeto = New Dominio.projeto
        For Each r As DataRow In tabela.Rows
            projeto.codigo = GetNullable(Of Integer)(r("codigo"))
            projeto.coordenador = GetNullable(Of Integer)(r("coordenador"))
            '  projeto.despesa = GetNullable(Of Double)(r("despesa"))
            '  projeto.receita = GetNullable(Of Double)(r("receita"))
            projeto.nome = GetNullable(Of String)(r("projeto"))
            ListaProjetos.Add(projeto)
        Next
    End Sub
    Function Empty() As String
        Dim banco As clBanco = New clBanco
        Dim lResult As String
        banco.CarregarTabela(String.Format("select 0 as codigo, ' ' as projeto, 0 as coordenador, 0 as receita, 0 as despesa, 0 as saldo"))
        Mapear(banco.tabela)
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
