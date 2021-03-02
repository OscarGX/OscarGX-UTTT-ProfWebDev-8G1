const txtClaveUnica = document.querySelector('#txtClaveUnica');
const ddlSexo = document.querySelector('#ddlSexo');
const txtNombre = document.querySelector('#txtNombre');
const txtAPaterno = document.querySelector('#txtAPaterno');
const txtAMaterno = document.querySelector('#txtAMaterno');
// const dtFechaNacimiento = document.querySelector('#dtFechaUI');
const errores = document.querySelector('.alert-danger');
const erroresParent = document.querySelector('#errores').firstElementChild;
const txtNumHermanos = document.querySelector('#txtNumHermanos');
const txtEmail = document.querySelector('#txtEmail');
const txtCP = document.querySelector('#txtCP');
const txtRFC = document.querySelector('#txtRFC');
const lblAction = document.querySelector('#lblAccion');
const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const rfcRegex = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
const onlyStringsRegex = /^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$/;
const spaceRegex = /\s\s+/;
let isValid = false;
let erroresArray = [];
// let touched = false;

function myFunction() {
    document.getElementById("demo").innerHTML = "My First JavaScript Function";
}

const closeAlert = (fromBtnAgregar = true) => {
    if (fromBtnAgregar) {
        erroresParent.classList.remove('visible');
    } else {
        erroresParent.classList.add('visible');
    }
}

function validate() {
    if (validateForm()) {
        return true;
    }
    if (erroresArray.length > 0) {
        let erroresStr = '';
        erroresArray.forEach((errorObj, index) => {
            erroresStr += `
                <strong>Hay errores en el formulario!<br>Error ${index + 1}:</strong><u> ${errorObj.message}</u><br><strong>Campo:</strong> <u>${errorObj.field}</u>
                <br><button type="button" class="btn btn-link" onClick="closeAlert(false)">Cerrar</button>
            `;
        });
        errores.innerHTML = erroresStr;
        closeAlert();
        window.scrollTo(0,0);
    }
    return false;
}


const validTemp = () => {
    const today = new Date();
    // const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    // development
    // const personDate = new Date(`${strDate[1]}/${strDate[0]}/${strDate[2]}`);
    // end
    // const personDate = new Date(`${strDate[0]}/${strDate[1]}/${strDate[2]}`);
    const validEmail = emailRegex.test(txtEmail.value.toLowerCase());
    const validCP = /^([0-9])*$/.test(txtCP.value) && txtCP.value.length === 5;
    const validRFC = rfcRegex.test(txtRFC.value);
    return ((ddlSexo.value > 0 && txtClaveUnica.value.length > 0 && txtNombre.value.length > 0 && txtAPaterno.value.length > 0
        && dtFechaNacimiento.value.length > 0) && (/^([0-9])*$/.test(txtClaveUnica.value) && txtClaveUnica.value.length === 3)
        && (txtNombre.value.length >= 3 && txtNombre.value.length <= 15) && (txtAPaterno.value.length >= 3 && txtAPaterno.value.length <= 15)
        && (/^([0-9])*$/.test(txtNumHermanos.value) && Number.parseInt(txtNumHermanos.value) >= 0) && ((today.getFullYear() - personDate.getFullYear()) >= 18))
        && validEmail && validCP && validRFC;
}

const validateForm = () => {
    erroresArray = [];
    const today = new Date();
    // const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    // dev enviroment
    // const personDate = new Date(`${strDate[1]}/${strDate[0]}/${strDate[2]}`);
    // end dev-e
    // const personDate = new Date(`${strDate[0]}/${strDate[1]}/${strDate[2]}`);
    if (Number.parseInt(ddlSexo.value) < 0) {
        erroresArray.push({
            field: 'Sexo',
            message: 'El campo sexo es requerido.'
        });
        return false;
    }
    if (txtClaveUnica.value.length === 0) {
        erroresArray.push({
            field: 'Clave Única',
            message: 'El campo clave única es requerido.'
        });
        return false;
    }
    if (!(/^([0-9])*$/.test(txtClaveUnica.value)) && txtClaveUnica.value.length > 0) {
        erroresArray.push({
            field: 'Clave Única',
            message: 'La clave única debe ser un número'
        });
        return false;
    }
    if (txtClaveUnica.value.length !== 3) {
        erroresArray.push({
            field: 'Clave Única',
            message: 'La clave única debe tener una longitud de 3 caracteres.'
        });
        return false;
    }
    if (txtNombre.value.trim().length === 0) {
        erroresArray.push({
            field: 'Nombre',
            message: 'El campo nombre es requerido.'
        });
        return false;
    }
    if (txtNombre.value.trim().length < 3 || txtNombre.value.trim().length > 50) {
        erroresArray.push({
            field: 'Nombre',
            message: 'El campo nombre debe tener una longitud entre 3 y 50 caracteres.'
        });
        return false;
    }
    if (spaceRegex.test(txtNombre.value)) {
        erroresArray.push({
            field: 'Nombre',
            message: 'Hay demasiados espacios en el campo nombre.'
        });
        return false;
    }
    if (!onlyStringsRegex.test(txtNombre.value)) {
        erroresArray.push({
            field: 'Nombre',
            message: 'Solo se aceptan letras en el campo nombre.'
        });
        return false;
    }
    if (txtAPaterno.value.length === 0) {
        erroresArray.push({
            field: 'Apellido Paterno',
            message: 'El campo apellido paterno es requerido.'
        });
        return false;
    }
    if (txtAPaterno.value.length < 3 || txtAPaterno.value.length > 50) {
        erroresArray.push({
            field: 'Apellido Paterno',
            message: 'El campo apellido paterno debe tener una longitud entre 3 y 15 caracteres.'
        });
        return false;
    }
    if (spaceRegex.test(txtAPaterno.value)) {
        erroresArray.push({
            field: 'Apellido Paterno',
            message: 'Hay demasiados espacios en el campo apellido paterno'
        });
        return false;
    }
    if (!onlyStringsRegex.test(txtAPaterno.value)) {
        erroresArray.push({
            field: 'Apellido Paterno',
            message: 'Solo se aceptan letras en el campo apellido paterno.'
        });
        return false;
    }
    if (spaceRegex.test(txtAMaterno.value)) {
        erroresArray.push({
            field: 'Apellido Materno',
            message: 'Hay demasiados espacios en el campo apellido materno.'
        });
        return false;
    }
    if (txtAMaterno.value.trim().length > 0 && txtAMaterno.value.trim().length < 3) {
        erroresArray.push({
            field: 'Apellido Materno',
            message: 'Si el campo apellido materno va a ser llenado debe tener mínimo 3 caracteres.'
        });
        return false;
    }
    if (!onlyStringsRegex.test(txtAMaterno.value) && txtAMaterno.value.trim().length > 1) {
        erroresArray.push({
            field: 'Apellido Materno',
            message: 'Solo se aceptan letras en el campo apellido materno.'
        });
        return false;
    }
    //if (dtFechaNacimiento.value.length === 0) {
    //    erroresArray.push({
    //        field: 'Fecha de nacimiento',
    //        message: 'La fecha de nacimiento es requerida.'
    //    });
    //    return false;
    //}
    if (!(/^([0-9])*$/.test(txtNumHermanos.value.length > 0 ? txtNumHermanos.value : 'abcd'))) {
        erroresArray.push({
            field: 'Número de Hermanos',
            message: 'El campo número de hermanos debe ser numérico.'
        });
        return false;
    }
    if (txtNumHermanos.value.length > 1 && txtNumHermanos.value[0] === "0") {
        erroresArray.push({
            field: 'Número de Hermanos',
            message: 'Por favor ingresa un número entre 0 y 30.'
        });
        return false;
    }
    //if (today.getFullYear() - personDate.getFullYear() < 18) {
    //    erroresArray.push({
    //        field: 'Fecha de Nacimiento',
    //        message: 'Debes er mayor de edad para acceder al sistema.'
    //    });
    //    return false;
    //}
    if (!emailRegex.test(txtEmail.value.toLowerCase())) {
        erroresArray.push({
            field: 'Email',
            message: 'El campo email no es válido.'
        });
        return false;
    }
    if (!(/^([0-9])*$/.test(txtCP.value.length > 0 ? txtCP.value : 'abcd'))) {
        erroresArray.push({
            field: 'Código Postal',
            message: 'El campo código postal debe ser número'
        });
        return false;
    }
    if (txtCP.value.length !== 5) {
        erroresArray.push({
            field: 'Código Postal',
            message: 'El campo código postal debe tener una longitud de 5 caracteres.'
        });
        return false;
    }
    if (!rfcRegex.test(txtRFC.value)) {
        erroresArray.push({
            field: 'RFC',
            message: 'El formato del RFC no es válido.'
        });
        return false;
    }
    return true;
}

/* const validDate = () => {
    const today = new Date();
    const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    const personDate = new Date(`${strDate[1]}/${strDate[0]}/${strDate[2]}`);
    return today.getFullYear() - personDate.getFullYear() >= 18;
} */

txtClaveUnica.addEventListener('keyup', function () {
    // touched = true;
    if (/^([0-9])*$/.test(this.value) && this.value.length === 3) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtNombre.addEventListener('keyup', function () {
    // touched = true;
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtAPaterno.addEventListener('keyup', function () {
    // touched = true;
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtNumHermanos.addEventListener('keyup', function () {
    // touched = true;
    if (/^([0-9])*$/.test(this.value) && Number.parseInt(this.value) >=  0) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtEmail.addEventListener('keyup', function () {
    // touched = true;
    if (emailRegex.test(this.value.toLowerCase())) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtCP.addEventListener('keyup', function () {
    // touched = true;
    if (/^([0-9])*$/.test(this.value) && this.value.length === 5) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});

txtRFC.addEventListener('keyup', function () {
    // touched = true;
    this.value = this.value.toUpperCase();
    if (rfcRegex.test(this.value)) {
        this.classList.remove('is-invalid');
        isValid = true;
    } else {
        this.classList.add('is-invalid');
        isValid = false;
    }
});