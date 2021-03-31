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
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        private String recoveryToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataContext db = new DcGeneralDataContext();
            var token = Request.QueryString["token"] as String;
            if (token == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            this.recoveryToken = token;
            var user = db.GetTable<Usuarios>().FirstOrDefault(u => u.strRecoveryToken == token);
            if (user == null)
            {
                Response.Redirect("~/ForgotPassword.aspx", false);
                return;
            } else
            {
                var persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.id == user.idComPersona);
                this.txtUsername.Text = user.strUsername;
                this.txtEmail.Text = persona.strEmail;

            }
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

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtPassword2.Text.Contains("  ");
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!this.txtPassword2.Text.Trim().Equals(this.txtPassword.Text.Trim()))
            {
                args.IsValid = false;
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
                var user = db.GetTable<Usuarios>().FirstOrDefault(u => u.strRecoveryToken == this.recoveryToken);
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
                user.strPassword = this.Encriptar(this.txtPassword.Text.Trim());
                user.strRecoveryToken = null;
                db.SubmitChanges();
                Session["UsernameSession"] = user.strUsername;
                Response.Redirect("~/Main.aspx", false);
            } catch(Exception ex)
            {
                CtrlEmail email = new CtrlEmail();
                email.sendEmail(ex.Message, "RecuperarContraseña.aspx.cs", "En el método btnAceptar_Click", ex.GetType().ToString());
                this.Response.Redirect("~/ErrorPages/ErrorPage.html", false);
            }
        }

        private bool validacion(ref String mensaje)
        {
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
            if (this.txtPassword.Text.Trim().Length < 5)
            {
                mensaje = "El campo contraseña debe tener 5 caracteres como mínimo.";
                return false;
            }
            if (txtPassword2.Text.Trim().Length == 0)
            {
                mensaje = "El campo contraseña es requerido.";
                return false;
            }
            if (!txtPassword2.Text.Trim().Equals(txtPassword.Text.Trim()))
            {
                mensaje = "Las contraseñas no coinciden.";
                return false;
            }
            return true;
        }
        public bool sqlInjectionValida(ref String _mensaje)
        {
            CtrlValidaInjection valida = new CtrlValidaInjection();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtPassword.Text.Trim(), ref _mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtPassword2.Text.Trim(), ref _mensajeFuncion, "Repetir contraseña", ref this.txtPassword2))
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
            if (valida.htmlInjectionValida(this.txtPassword.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPassword))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtPassword2.Text.Trim(), ref mensajeFuncion, "Repetir contraseña", ref this.txtPassword2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }
    }
}