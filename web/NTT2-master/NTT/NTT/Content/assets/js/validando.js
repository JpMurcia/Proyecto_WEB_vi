function soloLetras(e){
   key = e.keyCode || e.which;
   tecla = String.fromCharCode(key).toLowerCase();
   letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
   especiales = "8-37-39-46";

   tecla_especial = false
   for(var i in especiales){
        if(key == especiales[i]){
            tecla_especial = true;
            break;
        }
    }

    if(letras.indexOf(tecla)==-1 && !tecla_especial){
        return false;
    }
}

function soloNumeros(e){
   key = e.keyCode || e.which;
   tecla = String.fromCharCode(key).toLowerCase();
   letras = "0123456789";
   especiales = "8-37-39-46";

   tecla_especial = false
   for(var i in especiales){
        if(key == especiales[i]){
            tecla_especial = true;
            break;
        }
    }

    if(letras.indexOf(tecla)==-1 && !tecla_especial){
        return false;
    }
}

 

function correovalidate(id_correo) {
    //Creamos un objeto 
    object = document.getElementById(id_correo);
    valueForm = object.value;

    // Patron para el correo
    var patron = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
    if (valueForm.search(patron) == 0) {
        //Correo correcto
        object.style.color = black;
        return;
    }
    //Correo incorrecto
    object.style.color = red;
    alert("Correo no valido")
    return;


}


function validarPass()
{
  var p1 = document.getElementById("pass").value;
  var p2 = document.getElementById("repass").value;

  var espacios = false;
  var cont = 0;
   
  while (!espacios && (cont < p1.length)) {
    if (p1.charAt(cont) == " ")
      espacios = true;
    cont++;
  }
   
  if (espacios) {
    alert ("La contraseña no puede contener espacios en blanco");
    return false;
  }

  if (p1.length == 0 || p2.length == 0) {
    alert("Los campos de la Contraseña no pueden ser vacios");
    return false;
  }

  if (p1 != p2) {
    alert("Las Contraseñas deben Coincidir");
    return false;
  } else {
    alert("Todo esta correcto");
    return true; 
  }
}

/*
function formatear() {
    var val = document.getElementById("nombre").value;
    var tam = val.length;
    for(i = 0; i < tam; i++) {
        if(!isNaN(val[i]))
            document.getElementById("nombre").value = '';
    }
}
*/
