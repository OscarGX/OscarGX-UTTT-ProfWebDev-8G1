<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuariosManager" %>
<% @ Register Assembly = "AjaxControlToolkit" Namespace = "AjaxControlToolkit" TagPrefix = "asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Usuarios Manager</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
    <style type="text/css">
        .visible {
            display: none !important;
        }
    </style>
</head>
<body>
    <div class="container-fluid bg-dark">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <div class="container-fluid">
                <a class="navbar-brand" href="#">Oscar Reyes</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <asp:Label ID="lblUser" runat="server"></asp:Label>
                            </a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li><a class="dropdown-item" href="Login.aspx?logout=true">Cerrar sesión</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </div>
    <div class="container-fluid">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <h1 class="text-center mt-3">Usuario</h1>
            <hr />
            <div class="row justify-content-md-center">
                <div class="col-md-6">
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True" CssClass="h4"></asp:Label>
                    <hr />
                </div>
            </div>
            <div class="row justify-content-md-center">
                <div class="col-md-6 mb-2 mt-2">
                    <asp:Label ID="lblErrorM3V" runat="server" ForeColor="Red" Visible="False" CssClass="text-center"></asp:Label>
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
                    <%--<div class="mb-3">
                        <label class="form-label">Persona</label>
                        <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>--%>
                    <div class="mb-3">
                        <asp:Label ID="lblPersonaEditar" runat="server" CssClass="form-label">Persona</asp:Label>
                        <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-select"></asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtPersonaEditar" ReadOnly="true" CssClass="form-control" ViewStateMode="Disabled" Visible="false" MaxLength="50"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Nombre de usuario</label>
                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" ViewStateMode="Disabled" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtUsername" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" ViewStateMode="Disabled" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtPassword" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Repetir contraseña</label>
                        <asp:TextBox runat="server" ID="txtPassword2" CssClass="form-control" ViewStateMode="Disabled" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword2" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtPassword2" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtPassword2" EnableClientScript="False" ErrorMessage="Las contraseñas no coinciden" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblEstadoEditar" runat="server" Visible="false" CssClass="form-label">Estado</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-select" Visible="false"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <asp:label runat="server" ID="lblFecha" CssClass="form-label">Fecha de ingreso</asp:label>
                        <asp:TextBox runat="server" ID="txtDateAd" CssClass="form-control" ViewStateMode="Disabled"></asp:TextBox>
                        <asp:Label runat="server" ID="lblCalendarBtn">
                            <button class="btn btn-secondary" type="button" id="btnCalendar"><i class="far fa-calendar-alt"></i></button>
                        </asp:Label>
                        <ajaxToolkit:CalendarExtender ID="cldFechaNacimiento" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateAd" PopupButtonID="btnCalendar"/>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateAd" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtDateAd" EnableClientScript="False" ErrorMessage="El formato de fecha no es válido" OnServerValidate="CustomValidator5_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>--%>
                    </div>
                    <div class="mt-3 mb-3">
                        <asp:Button runat="server" ID="btnAceptar" Text="Aceptar" ViewStateMode="Disabled" CssClass="btn btn-success" OnClick="btnAceptar_Click" ValidationGroup="validForm" OnClientClick="return validate();"/>
                        <%--<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click" CssClass="btn btn-secondary ms-2" ViewStateMode="Disabled" />--%>
                        <a href="UsuariosPrincipal.aspx" class="btn btn-secondary ms-2">Cancelar</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script type="text/javascript" src="ValidScripts/usuariosmanager.js"></script>
</body>
</html>
