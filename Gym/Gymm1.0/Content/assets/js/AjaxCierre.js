$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Cierres/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });
});

function cargarTabla(id){
    $.ajax({
        type: "post",
        url: "/Cierres/Tabla2",
        data: {
            id: id
        },
        success: function (data) {
            $("#tabla2").html(data.result);
        }
    });
}