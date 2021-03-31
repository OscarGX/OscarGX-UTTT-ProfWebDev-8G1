<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuariosPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Usuarios Principal</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
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
    <div class="container-fluid mt-3">
        <form id="form1" runat="server">
            <h1 class="text-center">Usuarios</h1>
            <hr />
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Main.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Usuarios Principal</li>
                </ol>
            </nav>
            <div class="row">
                <div class="col-md-3">
                    <label class="form-label">Nombre de usuario</label>
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="paneltxtName" runat="server">
                            <ContentTemplate>
                                <asp:Button style="display: none;" ID="btnTrick" runat="server" OnClick="btnTrick_Click"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Estado</label>
                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlEstado"></asp:DropDownList>
                </div>
                <div class="col-md-3 d-flex align-items-end mt-2">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" ViewStateMode="Disabled" CssClass="btn btn-secondary"/>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ViewStateMode="Disabled" CssClass="btn btn-primary ms-3" />
                </div>
            </div>
            <div class="row mt-5">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:UpdatePanel runat="server" ID="panelGrid">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AutoGenerateColumns="false" DataSourceID="DataSourceUsuarios" CellPadding="4" GridLines="None" Width="1450px" OnRowCommand="GridView1_RowCommand" ViewStateMode="Disabled" ForeColor="#333333" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <%--<asp:BoundField DataField="id" HeaderText="Id" ReadOnly="true" SortExpression="id" />--%>
                                        <asp:BoundField DataField="Persona.strNombre" HeaderText="Persona" ReadOnly="true" SortExpression="Persona.strNombre" />
                                        <asp:BoundField DataField="strUsername" HeaderText="Nombre de usuario" ReadOnly="true" SortExpression="strUsername" />
                                        <asp:BoundField DataField="EstadoUsuarios.strValor" HeaderText="Estado" ReadOnly="true" SortExpression="EstadoUsuarios.strValor" />
                                        <asp:BoundField DataField="dtFechaIngreso" HeaderText="Fecha de Ingreso" ReadOnly="true" SortExpression="dtFechaIngreso" DataFormatString="{0:d}"  />
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <asp:LinqDataSource ID="DataSourceUsuarios" runat="server" ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" EntityTypeName="" OnSelecting="LinqDataSource1_Selecting" Select="new (strUsername, idEstado, dtFechaIngreso, id, EstadoUsuarios, Persona)" TableName="Usuarios"></asp:LinqDataSource>
        </form>
    </div>
    <script type="text/javascript">
        let timeout;
        //document.querySelector('#txtUsername').addEventListener('keyup', () => {
        //    clearTimeout(timeout)
        //    timeout = setTimeout(() => {
        //        // console.log('Has dejado de escribir en el input')
        //        const btnTrick = document.querySelector('#btnTrick');
        //        btnTrick.click();
        //        clearTimeout(timeout)
        //    }, 200)
        //});
        document.querySelector('#txtUsername').addEventListener('keyup', () => {
            const btnTrick = document.querySelector('#btnTrick');
            btnTrick.click();
        });
    </script>
</body>
</html>

