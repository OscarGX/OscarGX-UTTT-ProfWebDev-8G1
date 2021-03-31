<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="UTTT.Ejemplo.Persona.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Principal</title>
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
    <div class="container">

        <div class="row mt-3">
            <div class="col-md-12">
                <h1 class="text-center">Bienvenido a la pantalla principal</h1>
                <hr />
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item active" aria-current="page">Inicio</li>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="row justify-content-md-center mt-3">
            <h4>¿A dónde desea ir?</h4>
            <div class="col-md-4">
                <div class="card">
                    <%--<img src="Images/person.png" class="card-img-top" alt="persona">--%>
                    <img src="https://firebasestorage.googleapis.com/v0/b/fir-fotos-f57cd.appspot.com/o/img%2Fperson.png?alt=media&token=09ef1b8b-6257-4859-9294-884d484ef623" class="card-img-top" alt="persona"/>
                    <div class="card-body">
                        <a class="btn btn-primary" href="PersonaPrincipal.aspx">Ir a persona</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <img src="https://firebasestorage.googleapis.com/v0/b/fir-fotos-f57cd.appspot.com/o/img%2Fuser-icon.jpg?alt=media&token=c2eac7c3-2423-49e7-a2ff-af04ecb9f51d" class="card-img-top" alt="usuarios">
                    <div class="card-body">
                        <a class="btn btn-success" href="UsuariosPrincipal.aspx">Ir a usuarios</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
