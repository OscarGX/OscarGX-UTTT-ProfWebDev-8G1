using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Linq;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using System.Globalization;
using static BCrypt.Net.BCrypt;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuariosManager : System.Web.UI.Page
    {
        private Usuarios baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private SessionManager session = new SessionManager();
        private int idUsuarios = 0;
        private int tipoAccion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsernameSession"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            this.lblUser.Text = Session["UsernameSession"] as string;
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idUsuarios = this.session.Parametros["idUsuario"] != null ?
                    int.Parse(this.session.Parametros["idUsuario"].ToString()) : 0;
                if (this.idUsuarios == 0)
                {
                    this.baseEntity = new Usuarios();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Usuarios>().First(u => u.id == this.idUsuarios);
                    this.tipoAccion = 2;
                }
                this.txtPassword.Attributes.Add("type", "password");
                this.txtPassword2.Attributes.Add("type", "password");
                if (!IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<Linq.Data.Entity.Persona> personas = this.dcGlobal.GetTable<Linq.Data.Entity.Persona>().ToList();
                    this.ddlPersona.DataTextField = "NombreCompleto";
                    this.ddlPersona.DataValueField = "id";
                    if (this.idUsuarios == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        this.ddlPersona.DataSource = personas;
                        this.ddlPersona.DataBind();
                    } else
                    {
                        this.lblAccion.Text = "Editar";
                        this.lblEstadoEditar.Visible = true;
                        this.txtPersonaEditar.Visible = true;
                        this.ddlEstado.Visible = true;
                        this.ddlPersona.Visible = false;
                        this.txtDateAd.Visible = false;
                        this.lblCalendarBtn.Visible = false;
                        this.lblFecha.Visible = false;
                        List<EstadoUsuarios> estadosUsuario = dcGlobal.GetTable<EstadoUsuarios>().ToList();
                        this.ddlEstado.DataTextField = "strValor";
                        this.ddlEstado.DataValueField = "id";
                        this.ddlEstado.DataSource = estadosUsuario;
                        this.ddlEstado.DataBind();
                        this.setItem(ref this.ddlEstado, baseEntity.EstadoUsuarios.strValor);
                        this.txtPersonaEditar.Text = baseEntity.Persona.NombreCompleto;
                        this.txtUsername.Text = baseEntity.strUsername;
                        this.txtDateAd.Text = baseEntity.dtFechaIngreso.ToString("dd/MM/yyyy");
                        //this.txtPassword.Attributes["type"] = "text";
                        this.txtPassword.Text = this.DesEncriptar(baseEntity.strPassword);
                        //this.txtPassword.Attributes["type"] = "password";
                        //this.txtPassword2.Attributes["type"] = "text";
                        this.txtPassword2.Text = this.DesEncriptar(baseEntity.strPassword);
                        //this.txtPassword2.Attributes["type"] = "password";
                        // editar
                    }
                }
                if (this.idUsuarios > 0)
                {
                    this.txtPersonaEditar.Text = baseEntity.Persona.NombreCompleto;
                    this.txtPersonaEditar.Visible = true;
                    this.txtDateAd.Visible = false;
                    this.lblCalendarBtn.Visible = false;
                    this.lblFecha.Visible = false;
                }
            } catch(Exception ex)
            {
                this.Response.Redirect("~/UsuariosPrincipal.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!IsValid)
            {
                return;
            }
            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                Usuarios usuario = new Usuarios();
                DateTime dateValue = DateTime.Now;
                if (this.idUsuarios == 0)
                {
                    // agregar
                    usuario.idComPersona = int.Parse(this.ddlPersona.SelectedValue);
                    usuario.strUsername = this.txtUsername.Text.Trim();
                    usuario.strPassword = this.Encriptar(this.txtPassword.Text.Trim());
                    usuario.idEstado = 1;
                    if (DateTime.TryParseExact(this.txtDateAd.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    {
                        usuario.dtFechaIngreso = dateValue;
                    }
                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.sqlQueryValidation(usuario, ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    // usuario.dtFechaIngreso = (!DateTime.TryParseExact(this.txtDateAd.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue)) ? dateValue : dateValue;
                    dcGuardar.GetTable<Usuarios>().InsertOnSubmit(usuario);
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/UsuariosPrincipal.aspx", false);
                }
                if (this.idUsuarios > 0)
                {
                    usuario = dcGuardar.GetTable<Usuarios>().First(u => u.id == this.idUsuarios);
                    // pendiente usuario.idComPersona = 1;
                    usuario.strUsername = this.txtUsername.Text.Trim();
                    usuario.strPassword = this.Encriptar(this.txtPassword.Text.Trim());
                    usuario.idEstado = int.Parse(this.ddlEstado.SelectedValue);
                    //if (DateTime.TryParseExact(this.txtDateAd.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    //{
                    //    usuario.dtFechaIngreso = dateValue;
                    //}
                    String mensaje = String.Empty;
                    if (!this.validacion(usuario, ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.sqlInjectionValida(ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.htmlInjectionValida(ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    if (!this.sqlQueryValidationEditar(usuario, ref mensaje))
                    {
                        this.lblErrorM3V.Text = mensaje;
                        this.lblErrorM3V.Visible = true;
                        return;
                    }
                    dcGuardar.SubmitChanges();
                    this.Response.Redirect("~/UsuariosPrincipal.aspx", false);
                    // editar
                }
            } catch(Exception ex)
            {
                CtrlEmail email = new CtrlEmail();
                email.sendEmail(ex.Message, "UsuariosManager.aspx.cs", "En el método btnAceptar_Click", ex.GetType().ToString());
                this.Response.Redirect("~/ErrorPages/ErrorPage.html", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/UsuariosPrincipal.aspx", false);
        }

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        private bool validacion(Usuarios _usuario, ref String mensaje)
        {
            DateTime dateValue;
            if (_usuario.strUsername.Length == 0)
            {
                mensaje = "El campo nombre de usuario es requerido.";
                return false;
            }
            if (_usuario.strUsername.Length > 15)
            {
                mensaje = "El campo nombre de usuario no puede ser tan grande, escribe 15 caracteres como máximo.";
                return false;
            }
            if (_usuario.strUsername.Length < 3)
            {
                mensaje = "El campo nombre de usuario debe tener 3 letras como mínimo.";
                return false;
            }
            if (_usuario.strPassword.Length == 0)
            {
                mensaje = "El campo contraseña es requerido.";
                return false;
            }
            if (this.txtPassword.Text.Trim().Length > 15)
            {
                mensaje = "El campo contraseña no puede ser tan grande, escribe 15 caracteres como máximo.";
                return false;
            }
            if (this.txtPassword.Text.Trim().Length < 5)
            {
                mensaje = "El campo contraseña debe tener 5 caracteres como mínimo.";
                return false;
            }
            //if (!Verify(this.txtPassword2.Text.Trim(), _usuario.strPassword))
            //{
            //    mensaje = "Las contraseñas no coinciden.";
            //    return false;
            //}
            if (!this.txtPassword.Text.Trim().Equals(this.txtPassword2.Text.Trim()))
            {
                mensaje = "Las contraseñas no coinciden.";
                return false;
            }
            //if (_usuario.dtFechaIngreso.Date.ToString().Contains("01/01/0001") && this.idUsuarios == 0)
            //{
            //    mensaje = "El campo fecha de ingreso no contiene una fecha válida.";
            //    return false;
            //}
            if (this.txtDateAd.Text.Trim().Length == 0 && this.idUsuarios == 0)
            {
                mensaje = "La fecha es requerida";
                return false;
            }
            if ((!DateTime.TryParseExact(this.txtDateAd.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue)) && this.idUsuarios == 0)
            {
                mensaje = "La fecha ingresada no es válida.";
                return false;
            }
            DateTime now = DateTime.Now;
            DateTime oldxd = DateTime.Parse("01/01/1753");
            if ((_usuario.dtFechaIngreso >= now || _usuario.dtFechaIngreso <= oldxd) && this.idUsuarios == 0)
            {
                mensaje = "La fecha ingresada está fuera de rango, el rango es de 01/01/1753 hasta el día de hoy.";
                return false;
            }
            return true;
        }

        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlValidaInjection valida = new CtrlValidaInjection();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtUsername.Text.Trim(), ref _mensajeFuncion, "Nombre de usuario", ref this.txtUsername))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtPassword.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtPassword2.Text.Trim(), ref _mensajeFuncion, "Repetir Contraseña", ref this.txtPassword2))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.sqlInjectionValida(this.txtDateAd.Text.Trim(), ref _mensajeFuncion, "Fecha de ingreso", ref this.txtDateAd))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool htmlInjectionValida(ref String _mensaje)
        {
            CtrlValidaInjection valida = new CtrlValidaInjection();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtUsername.Text.Trim(), ref mensajeFuncion, "Nombre de usuario", ref this.txtUsername))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtPassword.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtPassword2.Text.Trim(), ref mensajeFuncion, "Repetir Contraseña", ref this.txtPassword2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (this.idUsuarios == 0)
            {
                return true;
            }
            if (valida.htmlInjectionValida(this.txtDateAd.Text.Trim(), ref mensajeFuncion, "Fecha de ingreso", ref this.txtDateAd))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool sqlQueryValidation(Usuarios usuario, ref String mensaje)
        {
            var user = dcGlobal.GetTable<Usuarios>().FirstOrDefault(u => u.strUsername == usuario.strUsername);
            if (user != null)
            {
                mensaje = "Ya existe un usuario con ese nombre, por favor, intenta con uno nuevo.";
                return false;
            }
            var userInPerson = dcGlobal.GetTable<Usuarios>().FirstOrDefault(u => u.idComPersona == usuario.idComPersona);
            if (userInPerson != null)
            {
                mensaje = "La persona elegida ya está asociada con un usuario, por favor, elige otra.";
                return false;
            }
            return true;
        }

        public bool sqlQueryValidationEditar(Usuarios usuario, ref String mensaje)
        {
            var userCount = dcGlobal.GetTable<Usuarios>().Where(u => u.strUsername == usuario.strUsername && u.id != usuario.id).Count();
            if (userCount > 0)
            {
                mensaje = "Ya existe un usuario con ese nombre, por favor, intenta con uno nuevo.";
                return false;
            }
            //var user = dcGlobal.GetTable<Usuarios>().FirstOrDefault(u => u.strUsername == usuario.strUsername);
            //if (user != null)
            //{
            //    mensaje = "Ya existe un usuario con ese nombre, por favor, intenta con uno nuevo.";
            //    return false;
            //}
            return true;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtUsername.Text.Contains("  ");
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtPassword.Text.Contains("  ");
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtPassword2.Text.Contains("  ");
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.txtPassword.Text.Trim().Equals(this.txtPassword2.Text.Trim());
        }
        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        //protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    DateTime dateValue;
        //    if (!DateTime.TryParseExact(this.txtDateAd.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
        //    {
        //        args.IsValid = false;
        //    }
        //}
    }
}