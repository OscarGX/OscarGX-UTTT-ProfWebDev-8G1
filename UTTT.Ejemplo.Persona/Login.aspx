<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous">
    <style type="text/css">
        .visible {
            display: none !important;
        }
    </style>
</head>
<body>
    <div class="container-fluid bg-dark">
        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">Oscar Reyes</a>
            </div>
        </nav>
    </div>
    <div class="container-fluid">
        <div class="row justify-content-md-center mt-3">
            <div class="col-md-6">
                <h1 class="text-center">Login</h1>
                 <hr/>
            </div>
        </div>
        <div class="row justify-content-md-center visible alert-errores-js">
            <div class="col-md-6">
                <div class="alert alert-danger" role="alert">
                </div>
            </div>
        </div>
        <div class="row justify-content-md-center mt-3">
            <div class="col-md-6">
                <form id="form1" runat="server">
                    <asp:Label runat="server" ID="lblError" Visible="false" CssClass="text-danger"></asp:Label>
                    <div class="mt-3 mb-3">
                        <label class="form-label">Nombre de usuario</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUsername" MaxLength="15" ViewStateMode="Disabled"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtUsername" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" MaxLength="15" ViewStateMode="Disabled" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                    </div>
                    <div class="mb-3">
                        <asp:Button runat="server" ID="btnAceptar" Text="Entrar" ViewStateMode="Disabled" CssClass="btn btn-success" OnClick="btnAceptar_Click" OnClientClick="return validate();" ValidationGroup="validForm"/>
                    </div>
                    <div class="mb-1">
                        <a href="ForgotPassword.aspx">Olvidé mi contraseña</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="ValidScripts/login.js"></script>
</body>
</html>
