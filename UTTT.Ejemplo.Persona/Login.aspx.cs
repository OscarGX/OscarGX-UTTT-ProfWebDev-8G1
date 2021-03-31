using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string logout = Request.QueryString["logout"];
            if (logout != null)
            {
                Session["UsernameSession"] = null;
            }
            if (Session["UsernameSession"] != null)
            {
                Response.Redirect("~/Main.aspx", false);
                return;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            try
            {
                DataContext db = new DcGeneralDataContext();
                String mensaje = String.Empty;
                if (!this.validacion(ref mensaje))
                {
                    this.lblError.Text = mensaje;
                    this.lblError.Visible = true;
                    return;
                }
                if (!this.sqlInjectionValida(ref mensaje))
                {
                    this.lblError.Text = mensaje;
                    this.lblError.Visible = true;
                    return;
                }
                if (!this.htmlInjectionValida(ref mensaje))
                {
                    this.lblError.Text = mensaje;
                    this.lblError.Visible = true;
                    return;
                }
                var dbUser = db.GetTable<Usuarios>().FirstOrDefault(u => u.strUsername == this.txtUsername.Text.Trim());
                if (dbUser == null)
                {
                    this.lblError.Text = "Nombre de usuario o contraseña incorrectos.";
                    this.lblError.Visible = true;
                    return;
                }
                if (dbUser.idEstado != 1)
                {
                    this.lblError.Text = "Ups! Parece que este usuario no está activo, consulta con el administrador de sistemas.";
                    this.lblError.Visible = true;
                    return;
                }
                var passDec = this.DesEncriptar(dbUser.strPassword);
                if (!passDec.Equals(this.txtPassword.Text.Trim()))
                {
                    this.lblError.Text = "Nombre de usuario o contraseña incorrectos.";
                    this.lblError.Visible = true;
                    return;
                }
                Session["UsernameSession"] = dbUser.strUsername; 
                this.Response.Redirect("~/Main.aspx", false);

            } catch(Exception ex)
            {
                CtrlEmail email = new CtrlEmail();
                email.sendEmail(ex.Message, "Login.aspx.cs", "En el método btnAceptar_Click", ex.GetType().ToString());
                this.Response.Redirect("~/ErrorPages/ErrorPage.html", false);
            }
        }

        private bool validacion(ref String mensaje)
        {
            if (txtUsername.Text.Trim().Length == 0)
            {
                mensaje = "El campo nombre de usuario es requerido.";
                return false;
            }
            if (txtUsername.Text.Trim().Length > 15)
            {
                mensaje = "El campo nombre de usuario no puede ser tan grande, escribe 15 caracteres como máximo.";
                return false;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                mensaje = "El campo contraseña es requerido.";
                return false;
            }
            if (this.txtPassword.Text.Trim().Length > 15)
            {
                mensaje = "El campo contraseña no puede ser tan grande, escribe 15 caracteres como máximo.";
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
    }
}