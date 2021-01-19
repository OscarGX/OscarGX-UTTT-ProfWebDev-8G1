const txtClaveUnica = document.querySelector('#txtClaveUnica');
const ddlSexo = document.querySelector('#ddlSexo');
const txtNombre = document.querySelector('#txtNombre');
const txtAPaterno = document.querySelector('#txtAPaterno');
const dtFechaNacimiento = document.querySelector('#dtFechaUI');
const errores = document.querySelector('#errores');
const txtNumHermanos = document.querySelector('#txtNumHermanos');
const txtEmail = document.querySelector('#txtEmail');
const txtCP = document.querySelector('#txtCP');
const txtRFC = document.querySelector('#txtRFC');
const lblAction = document.querySelector('#lblAccion');
const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const rfcRegex = /^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z\d]{3})?$/;
let isValid = false;
// let touched = false;

function myFunction() {
    document.getElementById("demo").innerHTML = "My First JavaScript Function";
}

function validate() {
    // return true;
    /*if (lblAction.textContent.trim().includes('Editar') && !touched) {
        return true;
    }
    if (isValid && Number.parseInt(ddlSexo.value) > 0 && validDate()) {
        return true;
    }*/
    if (validTemp()) {
        return true;
    }
    alert('No se puede registrar porque los datos son incorrectos.');
    return false;
}


const validTemp = () => {
    const today = new Date();
    const strDate = dtFechaNacimiento.value.split(' ')[0].split('/');
    // const personDate = new Date(`${strDate[1]}/${strDate[0]}/${strDate[2]}`);
    const personDate = new Date(`${strDate[0]}/${strDate[1]}/${strDate[2]}`);
    const validEmail = emailRegex.test(txtEmail.value.toLowerCase());
    const validCP = /^([0-9])*$/.test(txtCP.value) && txtCP.value.length === 5;
    const validRFC = rfcRegex.test(txtRFC.value);
    return ((ddlSexo.value > 0 && txtClaveUnica.value.length > 0 && txtNombre.value.length > 0 && txtAPaterno.value.length > 0
        && dtFechaNacimiento.value.length > 0) && (/^([0-9])*$/.test(txtClaveUnica.value) && txtClaveUnica.value.length === 3)
        && (txtNombre.value.length >= 3 && txtNombre.value.length <= 15) && (txtAPaterno.value.length >= 3 && txtAPaterno.value.length <= 15)
        && (/^([0-9])*$/.test(txtNumHermanos.value) && Number.parseInt(txtNumHermanos.value) >= 0) && ((today.getFullYear() - personDate.getFullYear()) >= 18))
        && validEmail && validCP && validRFC;
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
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtNombre.addEventListener('keyup', function () {
    // touched = true;
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtAPaterno.addEventListener('keyup', function () {
    // touched = true;
    if (this.value.length >= 3 && this.value.length <= 15) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtNumHermanos.addEventListener('keyup', function () {
    // touched = true;
    if (/^([0-9])*$/.test(this.value) && Number.parseInt(this.value) >=  0) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtEmail.addEventListener('keyup', function () {
    // touched = true;
    if (emailRegex.test(this.value.toLowerCase())) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtCP.addEventListener('keyup', function () {
    // touched = true;
    if (/^([0-9])*$/.test(this.value) && this.value.length === 5) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});

txtRFC.addEventListener('keyup', function () {
    // touched = true;
    this.value = this.value.toUpperCase();
    if (rfcRegex.test(this.value)) {
        this.className = '';
        isValid = true;
    } else {
        this.className = 'error';
        isValid = false;
    }
});