function validarEmail(valor) {
    //Creamos un objeto 
    object = document.getElementById(valor);
    valueForm = object.value;
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(valueForm)) {
     } else {
        alert("La dirección de email >"+valueForm+"< es incorrecta.");
    }
}