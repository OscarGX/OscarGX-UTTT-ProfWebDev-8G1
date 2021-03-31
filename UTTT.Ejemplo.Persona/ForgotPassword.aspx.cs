using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using System.Security.Cryptography;
using System.Text;

namespace UTTT.Ejemplo.Persona
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            } try
            {
                CtrlEmail email = new CtrlEmail();
                DataContext db = new DcGeneralDataContext();
                var persona = db.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(p => p.strEmail == this.txtEmail.Text.Trim());
                if (persona == null)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El correo ingresado no existe en nuestro sistema.";
                    return;
                }
                var usuario = db.GetTable<Usuarios>().FirstOrDefault(u => u.idComPersona == persona.id);
                if (usuario == null)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El correo ingresado no está asociado a un usuario.";
                    return;
                }
                if (usuario.idEstado > 1)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "El usuario asignado a este correo no está activo, consulte con el administrador del sistema.";
                    return;
                }
                var token = this.GetSHA256(Guid.NewGuid().ToString());
                usuario.strRecoveryToken = token;
                db.SubmitChanges();
                if (email.recoveryPasswordEmail(persona.strEmail, persona.strNombre, token))
                {
                    Response.Redirect("~/GenericPages/Recovery.html", false);
                } else
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = "Hubo un error al enviar el correo, intente más tarde.";
                    return;
                }

            } catch(Exception ex)
            {
                CtrlEmail email = new CtrlEmail();
                email.sendEmail(ex.Message, "ForgotPassword.aspx.cs", "En el método btnAceptar_Click", ex.GetType().ToString());
                this.Response.Redirect("~/ErrorPages/ErrorPage.html", false);
            }
        }

        public string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}