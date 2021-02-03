#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using System.Text.RegularExpressions;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        public DateTime dt = DateTime.Now;
        private readonly Regex emailRegex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        private readonly Regex rfcRegex = new Regex(@"^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$");
        private readonly Regex onlyLetters = new Regex(@"^[a-zA-ZÀ-ÿ\s\u00f1\u00d1]+$");
        
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    //catTemp.id = -1;
                    //catTemp.strValor = "Seleccionar";
                    //lista.Insert(0, catTemp);
                    //this.ddlSexo.DataTextField = "strValor";
                    //this.ddlSexo.DataValueField = "id";
                    //this.ddlSexo.DataSource = lista;
                    //this.ddlSexo.DataBind();

                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;
                    if (this.idPersona == 0)
                    {
                        //
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataTextField = "strValor";
                        this.ddlSexo.DataValueField = "id";
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        //
                        this.lblAccion.Text = "Agregar";
                        this.dtFechaUI.Value = null;
                        DateTime date = new DateTime(2000, 1, 9);
                        this.dtFechaNacimiento.TodaysDate = date;
                        this.dtFechaNacimiento.SelectedDate = date;
                    }
                    else
                    {
                        //
                        catTemp.id = baseEntity.CatSexo.strValor == "Masculino" ? 1 : 2;
                        catTemp.strValor = "Selecciona"; // baseEntity.CatSexo.strValor;
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataTextField = "strValor";
                        this.ddlSexo.DataValueField = "id";
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        //
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.txtNumHermanos.Text = this.baseEntity.intNumHermanos.ToString();
                        DateTime? fechaNacimiento = this.baseEntity.dtFechaNacimiento;
                        this.txtEmail.Text = this.baseEntity.strEmail;
                        this.txtCP.Text = this.baseEntity.strCP;
                        this.txtRFC.Text = this.baseEntity.strRFC;
                        if (fechaNacimiento != null)
                        {
                            this.dtFechaNacimiento.TodaysDate = (DateTime)fechaNacimiento;
                            this.dtFechaNacimiento.SelectedDate = (DateTime)fechaNacimiento;
                            this.dtFechaUI.Value = fechaNacimiento.ToString();
                        }
                        else
                        {
                            DateTime date = new DateTime(2000, 1, 9);
                            this.dtFechaNacimiento.TodaysDate = date;
                            // this.dtFechaNacimiento.SelectedDate = date;
                        }
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);
                    }
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                // this.showMessageException("Ups, parece que hay algunos errores en el formulario, por favor, intenta nuevamente.");
                return;
            }
            try
            {
                // throw new Exception("Excepción de prueba para correo.");
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "validate", "validate();", true);
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                int i = 0;
                if (this.idPersona == 0)
                {
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.dtFechaNacimiento = this.dtFechaNacimiento.SelectedDate.Date;
                    persona.intNumHermanos = this.txtNumHermanos.Text.Trim().Length > 0 ? (int.TryParse(this.txtNumHermanos.Text.Trim(), out i) ? int.Parse(this.txtNumHermanos.Text.Trim()) : 0) : 0;
                    persona.strEmail = this.txtEmail.Text.Trim();
                    persona.strCP = this.txtCP.Text.Trim();
                    // persona.intCP = this.txtCP.Text.Trim().Length > 0 ? (int.TryParse(this.txtCP.Text.Trim(), out i) ? int.Parse(this.txtCP.Text.Trim()) : 0) : 0;
                    persona.strRFC = this.txtRFC.Text.Trim();
                    String mensaje = String.Empty;
                    if (!this.validacion(persona, ref mensaje))
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
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);

                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                    persona.dtFechaNacimiento = this.dtFechaNacimiento.SelectedDate.Date;
                    persona.intNumHermanos = this.txtNumHermanos.Text.Trim().Length > 0 ? (int.TryParse(this.txtNumHermanos.Text.Trim(), out i) ? int.Parse(this.txtNumHermanos.Text.Trim()) : 0) : 0;
                    persona.strEmail = this.txtEmail.Text.Trim();
                    persona.strCP = this.txtCP.Text.Trim();
                    // persona.intCP = this.txtCP.Text.Trim().Length > 0 ? (int.TryParse(this.txtCP.Text.Trim(), out i) ? int.Parse(this.txtCP.Text.Trim()) : 0) : 0;
                    persona.strRFC = this.txtRFC.Text.Trim();
                    String mensaje = String.Empty;
                    if (!this.validacion(persona, ref mensaje))
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
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                }
            }
            catch (Exception _e)
            {
                CtrlEmail email = new CtrlEmail();
                //String messageException = _e.Message;
                //String archivo = "PersonaManager.aspx.cs";
                //String at = DateTime.Now.ToString();
                //String customMessage = String.Format("Ocurrió una excepción en el sistema. {0} Mensaje de la excepción: {1}{0} " +
                //    "En el archivo: {2}{0} Fecha y Hora: {3}", Environment.NewLine, messageException, archivo, at);
                email.sendEmail(_e.Message, "PersonaManager.aspx.cs", "En el método btnAceptar_Click", _e.GetType().ToString());
                this.Response.Redirect("~/ErrorPages/ErrorPage.html", false);
                // this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

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

        #endregion

        #region MetodosValidaciones
        /// <summary>
        /// Validación datos
        /// </summary>
        /// <param name="_persona"></param>
        /// <param name="_mensaje"></param>
        /// <returns></returns>

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
            int i = 0;
            if (_persona.idCatSexo < 0)
            {
                _mensaje = "El campo sexo es requerido";
                return false;
            }
            if (_persona.strClaveUnica.Equals(String.Empty))
            {
                _mensaje = "El campo clave única es requerido";
                return false;
            }
            if (!int.TryParse(_persona.strClaveUnica, out i))
            {
                _mensaje = "La clave única debe ser un número";
                return false;
            }
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave única está fuera de rango.";
                return false;
            }
            if (_persona.strClaveUnica.Length > 3 || _persona.strClaveUnica.Length < 3)
            {
                _mensaje = "La clave única debe tener 3 caracteres de longitud";
                return false;
            }
            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "El nombre está vacío";
                return false;
            }
            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "La longitud de caracteres del campo nombre rebasa lo permitido.";
                return false;
            }
            if (_persona.strNombre.Length < 3)
            {
                _mensaje = "El campo nombre debe tener una longitud al menos de 3 caracteres.";
                return false;
            }
            if (!this.onlyLetters.IsMatch(_persona.strNombre))
            {
                _mensaje = "El campo nombre debe contener solo letras.";
                return false;
            }
            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "El campo apellido paterno está vacío";
                return false;
            }
            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "La longitud de caracteres del campo apellido paterno rebasa lo permitido.";
                return false;
            }
            if (_persona.strAPaterno.Length < 3)
            {
                _mensaje = "El campo apellido paterno debe tener una longitud al menos de 3 caracteres.";
                return false;
            }
            if (!this.onlyLetters.IsMatch(_persona.strAPaterno))
            {
                _mensaje = "El campo apellido paterno debe contener solo letras.";
                return false;
            }
            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = "La longitud de caracteres del campo apellido materno rebasa lo permitido.";
                return false;
            }
            if (_persona.strAMaterno.Length > 0 && _persona.strAMaterno.Length < 3)
            {
                _mensaje = "Si el campo apellido materno va a ser llenado, debe contener 3 caracteres como mínimo.";
                return false;
            }
            if (!this.onlyLetters.IsMatch(_persona.strAMaterno) && _persona.strAMaterno.Length > 0)
            {
                _mensaje = "El campo apellido materno debe contener solo letras.";
                return false;
            }
            if (_persona.intNumHermanos.ToString().Length > 2)
            {
                _mensaje = "La longitud de caracteres del campo número de hermanos sobrepasa lo permitido";
                return false;
            }
            if (!emailRegex.IsMatch(_persona.strEmail))
            {
                _mensaje = "El correo electrónico no es válido";
                return false;
            }
            if (_persona.strEmail.Length > 100)
            {
                _mensaje = "El correo electrónico rebasa la longitud de caracteres permitida.";
                return false;
            }
            if (_persona.strCP.Length != 5)
            {
                _mensaje = "El código postal debe tener una longitud de 5 caracteres";
                return false;
            }
            if (!this.rfcRegex.IsMatch(_persona.strRFC))
            {
                _mensaje = "El formato del campo RFC no es válido";
                return false;
            }
            if (_persona.strRFC.Length > 13)
            {
                _mensaje = "La longitud de caracteres para el campo RFC sobrepasa lo permitido.";
                return false;
            }
            if (_persona.dtFechaNacimiento.Equals(String.Empty))
            {
                _mensaje = "El campo fecha de nacimiento es requerido";
                return false;
            }
            var time = DateTime.Now - _persona.dtFechaNacimiento.Value.Date;
            if (time.Days < 6570)
            {
                _mensaje = "Debes ser mayor de 18 años";
                return false;
            }
            return true;
        }

        public bool sqlInjectionValida (ref String _mensaje)
        {
            CtrlValidaInjection valida = new CtrlValidaInjection();
            String _mensajeFuncion = String.Empty;
            if (valida.sqlInjectionValida(this.txtClaveUnica.Text.Trim(), ref _mensajeFuncion, "Clave Única", ref this.txtClaveUnica))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(txtNombre.Text.Trim(), ref _mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtAPaterno.Text.Trim(), ref _mensajeFuncion, "Apellido Paterno", ref this.txtAPaterno))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtAMaterno.Text.Trim(), ref _mensajeFuncion, "Apellido Materno", ref this.txtAMaterno))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtNumHermanos.Text.Trim(), ref _mensajeFuncion, "Numero de hermanos", ref this.txtNumHermanos))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtEmail.Text.Trim(), ref _mensajeFuncion, "Email", ref this.txtEmail))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtCP.Text.Trim(), ref _mensajeFuncion, "Código Postal", ref this.txtCP))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            if (valida.sqlInjectionValida(this.txtRFC.Text.Trim(), ref _mensajeFuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = _mensajeFuncion;
                return false;
            }
            return true;
        }

        public bool htmlInjectionValida (ref String _mensaje)
        {
            CtrlValidaInjection valida = new CtrlValidaInjection();
            String mensajeFuncion = String.Empty;
            if (valida.htmlInjectionValida(this.txtClaveUnica.Text.Trim(), ref mensajeFuncion, "Clave Única", ref this.txtClaveUnica))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "Apellido Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "Apellido Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtNumHermanos.Text.Trim(), ref mensajeFuncion, "Número de Hermanos", ref this.txtNumHermanos))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtEmail.Text.Trim(), ref mensajeFuncion, "Email", ref this.txtEmail))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtCP.Text.Trim(), ref mensajeFuncion, "Código postal", ref this.txtCP))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInjectionValida(this.txtRFC.Text.Trim(), ref mensajeFuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }

        #endregion
        protected void dtFechaNacimiento_SelectionChanged(object sender, EventArgs e)
        {
            this.dtFechaUI.Value = this.dtFechaNacimiento.SelectedDate.ToString();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int sexIndex = int.Parse(this.ddlSexo.SelectedValue);
            args.IsValid = sexIndex > 0;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = this.txtCP.Text.Trim().Length == 5;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtNombre.Text.Contains("  ");
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtAPaterno.Text.Contains("  ");
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !this.txtAMaterno.Text.Contains("  ");
        }
    }
}