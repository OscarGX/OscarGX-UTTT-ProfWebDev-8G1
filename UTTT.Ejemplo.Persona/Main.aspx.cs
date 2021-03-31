using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTT.Ejemplo.Persona
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsernameSession"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }
            this.lblUser.Text = Session["UsernameSession"] as string;
        }
    }
}