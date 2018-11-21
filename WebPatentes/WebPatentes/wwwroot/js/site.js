
var principal = new Principal();
var usuarios = new Usuarios();

var imageUser = (evt) => {
    usuarios.archivo(evt, "imageRegistrar");
}

$().ready(() => {

    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //$('.sidenav').sidenav();
    M.AutoInit();

    if (URLactual == "/Usuarios/Registrar/Registrar" || URLactual == "/Usuarios/Registrar/Registrar/") {
        document.getElementById('files').addEventListener('change', imageUser,false)
    }

});