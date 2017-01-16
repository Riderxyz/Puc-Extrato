<%@ Page Title="Home Page" Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Extrato_projeto._Default" %>


<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <title>FPLF - Controle de Projetos</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>

<body style="background-image: url('fundo.jpg')">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" Modal="True" ShowOnPageLoad="True" Theme="PlasticBlue" HeaderText="Acesso ao Sistema" Width="400px" EnableTheming="True" FooterText="Fundação Padre Leonel Franca" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" PopupAnimationType="Fade" CloseAction="None" ShowCloseButton="False">
                <Windows>
                    <dx:PopupWindow Width="500px" ShowOnPageLoad="true" FooterText="Fundação Padre Leonel Franca" ShowFooter="True" Modal="True">
                        <HeaderStyle Font-Bold="True" Font-Size="Medium" />
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <dx:ASPxTextBox ID="edUsuario" runat="server" Caption="Usuário" Theme="PlasticBlue" Width="98%">
                                    <CaptionSettings Position="Top" />
                                    <CaptionStyle Font-Bold="True" Font-Size="Small">
                                    </CaptionStyle>
                                </dx:ASPxTextBox>
                                <br />
                                <dx:ASPxTextBox ID="edSenha" runat="server" Caption="Senha" Theme="PlasticBlue" Width="98%" Password="True">
                                    <CaptionSettings Position="Top" />
                                    <CaptionStyle Font-Bold="True" Font-Size="Small">
                                    </CaptionStyle>
                                </dx:ASPxTextBox>
                                <br />
                                <dx:ASPxButton ID="btLogin" Width="100%" runat="server" Text="Entrar" Font-Size="Medium" Height="30px" HorizontalAlign="Center" Theme="PlasticBlue" VerticalAlign="Middle"></dx:ASPxButton>

                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                </Windows>
                <ContentCollection>
                </ContentCollection>
                <ContentStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </ContentStyle>
            </dx:ASPxPopupControl>

        </div></div>
    </form>
</body>
</html>
