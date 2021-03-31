const txtUsername = document.querySelector('#txtUsername');
const txtPassword = document.querySelector('#txtPassword');
const alertErroresParent = document.querySelector(".alert-errores-js");
const alertErrores = alertErroresParent.firstElementChild.firstElementChild;
let errores = "";
const spaceRegex = /\s\s+/;

function validate() {
    if (validateForm()) {
        return true;
    }
    if (errores.length > 0) {
        removeAlertParentItems();
        const errorFragment = document.createDocumentFragment();
        const strong = document.createElement('strong');
        strong.textContent = errores;
        const br = document.createElement('br');
        const btnCerrar = document.createElement('button');
        btnCerrar.setAttribute('type', 'button');
        btnCerrar.classList.add('btn');
        btnCerrar.classList.add('btn-link');
        btnCerrar.setAttribute('onClick', 'hideAlert()');
        btnCerrar.textContent = 'Cerrar';
        errorFragment.appendChild(strong);
        errorFragment.appendChild(br);
        errorFragment.appendChild(btnCerrar);
        alertErrores.appendChild(errorFragment);
        showAlert();
    }
    return false;
}

const showAlert = () => {
    alertErroresParent.classList.remove('visible');
}

const hideAlert = () => {
    alertErroresParent.classList.add('visible');
    removeAlertParentItems();
}

const removeAlertParentItems = () => {
    if (alertErrores.children.length > 0) {
        alertErrores.children[0].remove();
        alertErrores.children[1].remove();
        alertErrores.children[0].remove();
    }
}

const validateForm = () => {
    if (txtUsername.value.trim().length === 0) {
        errores = "El campo nombre de usuario es requerido.";
        return false;
    }
    if (txtUsername.value.trim().length > 15) {
        errores = "El campo nombre de usuario es muy grande, debe contener máximo 15 caracteres.";
        return false;
    }
    if (spaceRegex.test(txtUsername.value)) {
        errores = "El campo nombre no puede tener más de 1 espacio seguido.";
        return false;
    }
    if (txtPassword.value.trim().length === 0) {
        errores = "El campo contraseña es requerido.";
        return false;
    }
    if (txtPassword.value.trim().length > 15) {
        errores = "El campo contraseña es muy grande, debe contener máximo 15 caracteres.";
        return false;
    }
    if (spaceRegex.test(txtPassword.value)) {
        errores = "El campo contraseña no puede tener más de 1 espacio seguido.";
        return false;
    }
    return true;
}