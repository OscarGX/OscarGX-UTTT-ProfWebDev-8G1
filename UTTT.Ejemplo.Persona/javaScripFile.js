const txtClaveUnica = document.querySelector('#txtClaveUnica');
const ddlSexo = document.querySelector('#ddlSexo');
const txtNombre = document.querySelector('#txtNombre');
const txtAPaterno = document.querySelector('#txtAPaterno');
const dtFechaNacimiento = document.querySelector('#dtFechaUI');
const errores = document.querySelector('#errores');
const txtNumHermanos = document.querySelector('#txtNumHermanos');
const erroresArray = [];
function myFunction() {
    document.getElementById("demo").innerHTML = "My First JavaScript Function";
}

function validate() {
    if (validTemp()) {
        return true;
    }
    alert('No se puede registrar porque los datos son incorrectos.');
    return false;
}

/* const requiredFields = () => {
    return ddlSexo.value < 0 || txtClaveUnica.value.length === 0 || txtNombre.value.length === 0 || txtAPaterno.value.length === 0
        || dtFechaNacimiento.value.length === 0;
}

const claveUnica = () => {
    return /^([0-9])*$/.test(txtClaveUnica.value) && txtClaveUnica.value.length === 3;
}

const nameAndlastNameLength = () => {
    return (txtNombre.value.length >= 3 && txtNombre.value.length <= 15) && (txtAPaterno.value.length >= 3 && txtAPaterno.value.length <= 15);
}

const numeroHermanos = () => {
    return /^([0-9])*$/.test(txtNumHermanos.value) && Number.parseInt(txtNumHermanos.value) > 0;
} */

/* const validDate = () => {
    const today = new Date();
    const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    const personDate = new Date(`${strDate[1]}/${strDate[0]}/${strDate[2]}`);
    console.log(today);
    console.log(personDate);
    // return false;
    return (today.getFullYear() - personDate.getFullYear()) >= 18;
}*/

/* const validateAll = () => {
    if (ddlSexo.value < 0 || txtClaveUnica.value.length === 0 || txtNombre.value.length === 0 || txtAPaterno.value.length === 0
        || dtFechaNacimiento.value.length === 0) {
        erroresArray.push({
            desc: `Error, los siguientes campos son requeridos: 
                   Sexo, Clave Única, Nombre, Apellido Paterno,
                   Fecha de Nacimiento`
        });
    } else if (!(/^([0-9])*$/.test(txtClaveUnica.value) && txtClaveUnica.value.length === 3)) {
        erroresArray.push({
            desc: `Error, el campo clave única debe tener 3 caracteres como mínimo
                   y debe ser numérico.`
        });
    } else if (!(txtNombre.value.length >= 3 && txtNombre.value.length <= 15 && txtAPaterno.value.length >= 3 && txtAPaterno.value.length <= 15)) {
        erroresArray.push({
            desc: `Error, los campos Nombre y apellido paterno deben tener al menos 3 caracteres
                   y máximo 15.`
        });
    } else if (/^([0-9])*$/.test(txtNumHermanos.value)) {
        erroresArray.push({
            desc: `Error, el número de hermanos deben ser números enteros.`
        });
    } else if (validate()) {
        erroresArray.push({
            desc: `Error, debes ser mayor a 18 años.`
        });
    }
} */

const validTemp = () => {
    const today = new Date();
    const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    const personDate = new Date(`${strDate[0]}/${strDate[1]}/${strDate[2]}`);
    return ((ddlSexo.value > 0 && txtClaveUnica.value.length > 0 && txtNombre.value.length > 0 && txtAPaterno.value.length > 0
        && dtFechaNacimiento.value.length > 0) && (/^([0-9])*$/.test(txtClaveUnica.value) && txtClaveUnica.value.length === 3)
        && (txtNombre.value.length >= 3 && txtNombre.value.length <= 15) && (txtAPaterno.value.length >= 3 && txtAPaterno.value.length <= 15)
        && (/^([0-9])*$/.test(txtNumHermanos.value) && Number.parseInt(txtNumHermanos.value) >= 0) && ((today.getFullYear() - personDate.getFullYear()) >= 18));
}

txtClaveUnica.addEventListener('keyup', function () {
    if (/^([0-9])*$/.test(this.value) && this.value.length === 3) {
        this.className = '';
    } else {
        this.className = 'error';
    }
});

txtNombre.addEventListener('keyup', function () {
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.className = '';
    } else {
        this.className = 'error';
    }
});

txtAPaterno.addEventListener('keyup', function () {
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.className = '';
    } else {
        this.className = 'error';
    }
});

txtNumHermanos.addEventListener('keyup', function () {
    if (/^([0-9])*$/.test(this.value) && Number.parseInt(this.value) >=  0) {
        this.className = '';
    } else {
        this.className = 'error';
    }
});