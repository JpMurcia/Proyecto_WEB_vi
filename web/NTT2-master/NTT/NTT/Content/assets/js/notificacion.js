var btnNotificacion = document.getElementById("buttonN"),  
    btnPermiso = document.getElementById("buttonP")
    titulo = "Fili Santillán",
    opciones = {
        icon: "logo.png",
        body: "Notificación de prueba"
    };

function permiso() {  
        Notification.requestPermission();
};

function mostrarNotificacion() {  
    if(Notification) {
        if (Notification.permission == "granted") {
            var n = new Notification(titulo, opciones);
        }

        else if(Notification.permission == "default") {
            alert("Primero da los permisos de notificación");
        }

        else {
            alert("Bloqueaste los permisos de notificación");
        }
    }
};

btnPermiso.addEventListener("click", permiso);  
btnNotificacion.addEventListener("click", mostrarNotificacion);  