<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UTTT.Ejemplo.Persona._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Ejemplo MVC </title>
    <link href="controlformato.css" type="text/css" rel="stylesheet" />
    <script>
        
        function validar() {
            //obteniendo el valor que se puso en campo text del formulario
            miCampoTexto = document.getElementById("txtClave").value;
            //la condición
            //obligatorios
            //enteros y cadena
            //expresion regular

            if (miCampoTexto.length == 0) {
                alert('El campo clave está vacio!');
                return false;
            }
            return true;
        }
</script>

</head>
<body>
    
</body>
</html>
