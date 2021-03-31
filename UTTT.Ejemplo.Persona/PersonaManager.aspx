<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" debug=false%>
<% @ Register Assembly = "AjaxControlToolkit" Namespace = "AjaxControlToolkit" TagPrefix = "asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        .error {
            border-color: red;
        }
        input:focus {
            outline: none;
        }
        .visible {
            display: none !important;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.14.0/css/all.css" integrity="sha384-HzLeBuhoNPvSl5KYnjx0BT+WB0QEEqLprO+NBkkk5gbc67FTaL7XIGa2w1L0Xbgc" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
</head>
<body class="bg-light">
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
    <div style="font-family: Arial; font-size: medium; font-weight: bold" class="mt-5">
        <h1 class="text-center">Persona</h1>
        <hr />
    </div>

        <div>
        
        </div>
        <div class="row justify-content-md-center">
           <div class="col-md-6">
               <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True" CssClass="h4"></asp:Label>
               <hr />
           </div>
        </div>
        <div>

        </div>
        <div class="row justify-content-md-center">
            <div class="col-md-6 mb-2 mt-2">
                <asp:Label ID="lblErrorM3V" runat="server" ForeColor="Red" Visible="False" CssClass="text-center"></asp:Label>
            </div>
        </div>
        <div id="errores" class="mt-2" style="text-align: center; color: red;">
            <div class="row justify-content-md-center visible">
                <div class="col-md-6">
                    <div class="alert alert-danger" role="alert">
                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-md-center mt-3">
            <div class="col-md-6">
                <div class="mb-3">
                    <label class="form-label">Sexo</label>
                    <asp:DropDownList ID="ddlSexo" runat="server" 
                         CssClass="form-select">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlSexo" EnableClientScript="False" ErrorMessage="Debes seleccionar una opción" OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Clave Única</label>
                    <asp:TextBox ID="txtClaveUnica" runat="server" 
                     ViewStateMode="Disabled" MaxLength="3" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClaveUnica" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClaveUnica" EnableClientScript="False" ErrorMessage="El campo debe ser un número" ValidationExpression="^\d+$" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" ViewStateMode="Disabled" MaxLength="15" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtNombre" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Apellido Paterno</label>
                    <asp:TextBox ID="txtAPaterno" runat="server" ViewStateMode="Disabled" MaxLength="15" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAPaterno" EnableClientScript="False" ErrorMessage="Este campo es requerido" ValidationGroup="validForm"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtAPaterno" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Apellido Materno</label>
                    <asp:TextBox ID="txtAMaterno" runat="server" ViewStateMode="Disabled" MaxLength="15" CssClass="form-control"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtAMaterno" EnableClientScript="False" ErrorMessage="Este campo contiene demasiados espacios" OnServerValidate="CustomValidator5_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Fecha de Nacimiento</label>
                    <asp:TextBox ID="txtFechaNacimientoAjax" runat="server" CssClass="form-control"></asp:TextBox>
                    <button class="btn btn-secondary" type="button" id="btnCalendar"><i class="far fa-calendar-alt"></i></button>
                    <ajaxToolkit:CalendarExtender ID="cldFechaNacimiento" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaNacimientoAjax" PopupButtonID="btnCalendar"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">Número de hermanos</label>
                    <asp:TextBox ID="txtNumHermanos" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumHermanos" EnableClientScript="False" ErrorMessage="El campo debe ser un número entero" ValidationExpression="^\d+$" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNumHermanos" EnableClientScript="False" ErrorMessage="El valor debe estar entre 0 y 30" MaximumValue="30" MinimumValue="0" Type="Integer" ValidationGroup="validForm"></asp:RangeValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Correo electrónico</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail" EnableClientScript="False" ErrorMessage="El correo no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Código Postal</label>
                    <asp:TextBox ID="txtCP" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCP" EnableClientScript="False" ErrorMessage="Solo se permiten números" ValidationExpression="^\d+$" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCP" EnableClientScript="False" ErrorMessage="La longitud debe ser de 5 caracteres" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="validForm"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">RFC</label>
                    <asp:TextBox ID="txtRFC" runat="server" MaxLength="13" CssClass="form-control"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtRFC" EnableClientScript="False" ErrorMessage="El RFC no coincide con el formato oficial" ValidationExpression="^([aA-zZñÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([aA-zZ\d]{3})?$" ValidationGroup="validForm"></asp:RegularExpressionValidator>
                </div>
                <div class="mb-3">
                    <label class="form-label">Estado Civil</label>
                    <asp:DropDownList ID="ddlEstadoCivil" CssClass="form-select" runat="server"></asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="ddlEstadoCivil" EnableClientScript="False" ErrorMessage="Debes seleccionar una opción" ValidationGroup="validForm" OnServerValidate="CustomValidator6_ServerValidate"></asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <%--<label class="form-label">Fecha de nacimiento</label>--%>
                    <asp:Calendar ID="dtFechaNacimiento" runat="server" OnSelectionChanged="dtFechaNacimiento_SelectionChanged" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px" Visible="False">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                        <DayStyle BackColor="#CCCCCC" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    </asp:Calendar>
                    <asp:HiddenField ID="dtFechaUI" runat="server" />
                </div>
                <div class="mt-3">
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click" CssClass="btn btn-success" ViewStateMode="Disabled" OnClientClick="return validate();" ValidationGroup="validForm" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click" CssClass="btn btn-secondary ms-2" ViewStateMode="Disabled" />
                </div>
            </div>
        </div>
        <p>
            &nbsp;</p>
    </form>
        </div>
    <%--<div class="container-fluid mt-3">
        <div class="row justify-content-md-center bg-dark">
            <div class="col-md-10 bg-dark">
                <p class="text-center text-white">Ingeniería en Desarrollo y Gestión de Software - 8IDGS-G1</p>
            </div>
        </div>
    </div>--%>
    <script src="javaScripFile.js" type="text/javascript"></script>
</body>
</html>
