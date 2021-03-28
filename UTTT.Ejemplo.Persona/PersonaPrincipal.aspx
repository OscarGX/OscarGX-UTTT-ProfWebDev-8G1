<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal"  debug=false%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous"/>
</head>
<body class="bg-light">
    <style type="text/css">
        .grid-view {
            width: 100%; 
            word-wrap:break-word;
            table-layout: fixed; 
        }

        .visible {
            display: none;
        }

        /*.table-responsive {
            display: table !important;
        }*/
        /* 
            
            Width="1400px"
        */
    </style>
    <div class="container-fluid bg-dark">
        <nav class="navbar navbar-dark bg-dark">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Oscar Reyes</a>
        </div>
    </nav>
    </div>
    <div class="container-fluid">
    <form id="form1" runat="server">
    <div style="color: #000000; font-size: medium; font-family: Arial; font-weight: bold" class="mt-5"> 
        <h1 class="text-center">Persona</h1>
        <hr />
    </div>
        <%--<div class="row mt-4">
            <label class="col-md-1 col-form-label">Nombre</label>
            <div class="col-md-2">
                <asp:TextBox ID="txtNombre" runat="server" Width="174px" 
            ViewStateMode="Disabled"  CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-8">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
            onclick="btnBuscar_Click" ViewStateMode="Disabled"  CssClass="btn btn-secondary ms-3"/>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
            onclick="btnAgregar_Click" ViewStateMode="Disabled"  CssClass="btn btn-primary ms-3"/>
            </div>
        </div>--%>
        
    <%--<div class="row mt-3">
        <label class="col-md-1 col-form-label">Sexo</label>
    <div class="col-md-10">
      <asp:DropDownList ID="ddlSexo" runat="server"  Width="177px" CssClass="form-select">
        </asp:DropDownList>
    </div>
    </div>--%>
        <div class="row">
            <div class="col-md-2">
                <label class="form-label">Nombre</label>
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="paneltxtName" runat="server">
                    <ContentTemplate>
                        <%--<asp:TextBox ID="txtNombre" runat="server"  
                      CssClass="form-control"  AutoPostBack="true" OnTextChanged="onTxtNombreTextChange"></asp:TextBox>--%>
                        <asp:Button style="display: none;" ID="btnTrick" runat="server" OnClick="btnTrick_Click"/>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger  ControlID="txtNombre" EventName="TextChanged"/>
                    </Triggers>--%>
                </asp:UpdatePanel>
                <asp:TextBox ID="txtNombre" runat="server"  
                      CssClass="form-control"></asp:TextBox>
                
                
            </div>
            <div class="col-md-2">
                <label class="form-label">Sexo</label>
                <asp:DropDownList ID="ddlSexo" runat="server"  CssClass="form-select">
        </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label class="form-label">Estado Civil</label>
                <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <div class="col-md-3 d-flex align-items-end">
                 <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
            onclick="btnBuscar_Click" ViewStateMode="Disabled"  CssClass="btn btn-secondary"/>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
            onclick="btnAgregar_Click" ViewStateMode="Disabled"  CssClass="btn btn-primary ms-3"/>
            </div>
            
        </div>
    <div style="font-weight: bold" class="mt-4">
        <h4 class="text-center">Detalle
        </h4>
    </div>
        <div>
        
        </div>
       
            <div class="row mt-3">
            <div class="col-md-12 ">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="panelGrid">
                    <ContentTemplate>
             <asp:GridView ID="dgvPersonas" runat="server" 
                AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona" 
                CellPadding="4" GridLines="None"
                 Width="1450px"
                 onrowcommand="dgvPersonas_RowCommand" 
                ViewStateMode="Disabled" ForeColor="#333333" OnSelectedIndexChanged="dgvPersonas_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica" 
                        ReadOnly="True" SortExpression="strClaveUnica" />
                    <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True" 
                        SortExpression="strNombre" />
                    <asp:BoundField DataField="strAPaterno" HeaderText="APaterno" ReadOnly="True" 
                        SortExpression="strAPaterno" />
                    <asp:BoundField DataField="strAMaterno" HeaderText="AMaterno" ReadOnly="True" 
                        SortExpression="strAMaterno" />
                    <asp:BoundField DataField="CatSexo" HeaderText="Sexo" 
                        SortExpression="CatSexo" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                    
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')"/>
                            </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>

                      <asp:TemplateField HeaderText="Direccion">
                        <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" />
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
    <asp:LinqDataSource ID="DataSourcePersona" runat="server" 
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" 
        onselecting="DataSourcePersona_Selecting" 
        Select="new (strNombre, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id)" 
        TableName="Persona" EntityTypeName="">
    </asp:LinqDataSource>
    </form>
    </div>
    <%--<div class="container-fluid mt-5">
        <div class="row justify-content-md-center bg-dark">
            <div class="col-md-10 bg-dark">
                <p class="text-center text-white">Ingeniería en Desarrollo y Gestión de Software - 8IDGS-G1</p>
            </div>
        </div>
    </div>--%>
    <script type="text/javascript">
        
        document.querySelector('#txtNombre').addEventListener('keyup', function () {
            const btnTrick = document.querySelector('#btnTrick');
            console.log('olv');
            // __doPostBack(this.name, 'OnKeyPress');
            btnTrick.click();
        });
     </script>
</body>
</html>
