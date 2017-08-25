var idPersonaA;
var bol = false;

$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Instructores/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });
    $("#IngresoPersonaBtn").click(function () {
        NombreIng = $("#NombreIng").val();
        PrimerApeIng = $("#PrimerApeIng").val();
        SegundoApeIng = $("#SegundoApeIng").val();
        TelefonoIng = $("#TelefonoIng").val();

        UsuarioIng = $("#UsuarioIng").val();
        ContrasenaIng = $("#ContrasenaIng").val();
        $.ajax({
            type: "post",
            url: "/Instructores/Ingreso",
            data: {
                nombreing: NombreIng, primerapeing: PrimerApeIng, segundoapeing: SegundoApeIng, telefonoing: TelefonoIng, usuarioing: UsuarioIng, passing: ContrasenaIng
            },
            success: function (data) {
                if (data.bol) {
                    $("#btnIn").click;
                } else {
                    $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                    "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                    "</div>");
                }
            }
        });
        $("#btnIn").click();
        $.ajax({
            type: "post",
            url: "/Instructores/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
    });

    $("#btnGuardarEdit22").click(function () {
        NombreIng = $("#NombreEd2").val();
        PrimerApeIng = $("#PrimerApeEd2").val();
        SegundoApeIng = $("#SegundoApeEd2").val();
        TelefonoIng = $("#TelefonoEd2").val();

        UsuarioIng = $("#UsuarioEd2").val();
        ContrasenaIng = $("#ContrasenaEd2").val();

        if (bol) {
            $.ajax({
                type: "post",
                url: "/Instructores/GuardarEdicion",
                data: {
                    nombreing: NombreIng, primerapeing: PrimerApeIng, segundoapeing: SegundoApeIng, telefonoing: TelefonoIng,
                    usuarioing: UsuarioIng, passing: ContrasenaIng, idInstructor: idPersonaA
                },
                success: function (data) {
                    if (data.bol) {
                    } else {
                        $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                        "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                        "</div>");
                    }
                }
            });
        } else {
            $.ajax({
                type: "post",
                url: "/Instructores/GuardarEdicion2",
                data: {
                    nombreing: NombreIng, primerapeing: PrimerApeIng, segundoapeing: SegundoApeIng, telefonoing: TelefonoIng, idInstructor: idPersonaA
                },
                success: function (data) {
                    if (data.bol) {
                    } else {
                        $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                            "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                            "</div>");
                    }
                }
            });
        }
        $("#CerrarBtn").click();
        $.ajax({
            type: "post",
            url: "/Instructores/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
    });
});


function EditarAjax(idPersona) {
    $.ajax({
        type: "post",
        url: "/Instructores/Editar",
        data: { idInstructor: idPersona },
        success: function (data) {
            $("#NombreEd2").val(data.nombre);
            $("#PrimerApeEd2").val(data.apellido1);
            $("#SegundoApeEd2").val(data.apellido2);
            $("#TelefonoEd2").val(data.telefono);
            $("#UsuarioEd2").val(data.usuario);
            $("#ContrasenaEd2").val("123456");
            idPersonaA = data.idIns;
        }
    });
}

function Eliminar(id) {
    alert(id);
    idPersonaA = id;
}

function switchi() {
    if (!bol) {
        $("#ContrasenaEd2").prop({
            disabled: false
        });
        bol = true;
    } else {
        $("#ContrasenaEd2").prop({
            disabled: true
        });
        bol = false;
    }
}