var idPersonaA;
var intruc;

$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Usuarios/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });

    $("#IngresoPersonaBtn").click(function () {
        UsuarioIng = $("#UsuarioIng").val();
        ContrasenaIng = $("#ContrasenaIng").val();
        emailIng = $("#emailIng").val();
        Rol = $("#rol").val();
        $.ajax({
            type: "post",
            url: "/Usuarios/Ingreso",
            data: {
                usuarioing: UsuarioIng, contrasenaing: ContrasenaIng, rol: Rol, emailIn: emailIng
            },
           
            success: function (data) {
                if (data.bol) {
                } else {
                    $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                        "<strong>Alerta!</strong> Error a la hora de ingresar, revise que el usuario no se repita" +
                                        "</div>");
                }
             }
        });
        $.ajax({
            type: "post",
            url: "/Usuarios/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#btnIn").click();
    });

    $("#btnGuardarEdit").click(function () {
        UsuarioIng = $("#UsuarioEd").val();
        ContrasenaIng = $("#ContrasenaEd").val();
        var rolEd;
        if (intruc) {
            rolEd = "Instructor";
        } else {
            rolEd = $("#rolEd").val();
        }
        $.ajax({
            type: "post",
            url: "/Usuarios/GuardarEdicion",
            data: {
                usuario: UsuarioIng, contrasena: ContrasenaIng, rol: rolEd
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
        $.ajax({
            type: "post",
            url: "/Usuarios/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#btnEdi").click();
    });

});


function EditarAjax(idPersona) {
    $.ajax({
        type: "post",
        url: "/Usuarios/Editar",
        data: { usuario: idPersona },
        success: function (data) {
            $("#UsuarioEd").val(data.usuario);
            $("#ContrasenaEd").val(data.pass);
            if (data.rol != "Instructor") {
                $("#rolEd").val(data.rol);
                intruc = false;
                $("#rolCombo").html('<select id="rolEd"><option value="admin">Administrador</option><option value="ventas">Vendedor</option></select>');
            } else {
                intruc = true;
                $("#rolCombo").html("");
            }
            idPersonaA = data.usuario;
        }
    });
}

function Eliminar(id) {
    idPersonaA = id;
}