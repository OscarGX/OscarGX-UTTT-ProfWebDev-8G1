<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="UTTT.Ejemplo.Persona.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Forgot Password</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous" />
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
    <div class="container-fluid mt-3">
        <div class="row justify-content-md-center mt-3">
            <div class="col-md-6">
                <h1 class="text-center">Recuperación de contraseña</h1>
                <hr/>
            </div>
        </div>
        <div class="row justify-content-md-center visible alert-errores-js">
            <div class="col-md-6">
                <div class="alert alert-danger" role="alert">
                </div>
            </div>
        </div>
        <form id="form1" runat="server">
            <div class="row justify-content-md-center">
                <div class="col-md-6">
                    <asp:Label runat="server" ID="lblError" Visible="false" CssClass="text-danger"></asp:Label>
                    <div class="mb-3">
                        <label class="form-label">Correo electrónico</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail" EnableClientScript="False" ErrorMessage="El correo no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                    </div>
                    <div class="mb-3">
                        <asp:Button CssClass="btn btn-success" ID="btnAceptar" runat="server" Text="Enviar" ViewStateMode="Disabled" OnClick="btnAceptar_Click" ValidationGroup="validForm"/>
                        <a href="Login.aspx" class="btn btn-secondary">Regresar</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
