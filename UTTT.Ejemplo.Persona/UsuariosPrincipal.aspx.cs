using System;
using System.Collections.Generic;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuariosPrincipal : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
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
                // Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<EstadoUsuarios> estadoUsuarios = dcTemp.GetTable<EstadoUsuarios>().ToList();
                    EstadoUsuarios estadoTemp = new EstadoUsuarios();
                    estadoTemp.id = -1;
                    estadoTemp.strValor = "Todos";
                    estadoUsuarios.Insert(0, estadoTemp);
                    this.ddlEstado.DataTextField = "strValor";
                    this.ddlEstado.DataValueField = "id";
                    this.ddlEstado.DataSource = estadoUsuarios;
                    this.ddlEstado.DataBind();
                }
            } catch(Exception ex)
            {
                
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            this.session.Pantalla = "~/UsuariosManager.aspx";
            Hashtable parametrosRagion = new Hashtable();
            parametrosRagion.Add("idUsuario", "0");
            this.session.Parametros = parametrosRagion;
            this.Session["SessionManager"] = this.session;
            this.Response.Redirect(this.session.Pantalla, false);
        }

        protected void LinqDataSource1_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            if (Session["UsernameSession"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool estadoBool = false;
                if (!this.txtUsername.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlEstado.Text != "-1")
                {
                    estadoBool = true;
                }

                Expression<Func<Usuarios, bool>>
                    predicate =
                    (c =>
                    ((estadoBool) ? c.idEstado == int.Parse(this.ddlEstado.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strUsername.Contains(this.txtUsername.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<Usuarios> usersList = dcConsulta.GetTable<Usuarios>().Where(predicate).ToList();
                e.Result = usersList;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.DataSourceUsuarios.RaiseViewChanged();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUser = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idUser);
                        break;
                    case "Eliminar":
                        this.eliminar(idUser);
                        break;
                }
            } catch(Exception ex)
            {

            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editar(int idUser)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idUsuario", idUser.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/UsuariosManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int idUser)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                Usuarios users = dcDelete.GetTable<Usuarios>().First(
                    c => c.id == idUser);
                dcDelete.GetTable<Usuarios>().DeleteOnSubmit(users);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se agrego correctamente.");
                this.DataSourceUsuarios.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void btnTrick_Click(object sender, EventArgs e)
        {
            this.DataSourceUsuarios.RaiseViewChanged();
        }
    }
}