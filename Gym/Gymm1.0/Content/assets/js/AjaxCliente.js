var idPersonaA;

$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Clientes/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });
    $.ajax({
        type: "post",
        url: "/Clientes/Combo",
        success: function (data) {
            $("#comboDiv").html(data.result);
        }
    });
    $.ajax({
        type: "post",
        url: "/Clientes/Combo2",
        success: function (data) {
            $("#comboDiv2").html(data.result);
        }
    });

    $("#IngresoPersonaBtn").click(function () {
        NombreIng = $("#NombreIng").val();
        PrimerApeIng = $("#PrimerApeIng").val();
        SegundoApeIng = $("#SegundoApeIng").val();
        TelefonoIng = $("#TelefonoIng").val();
        FechaIngresoIng = $("#FechaIngresoIng").val();
        InstructoresIng = $("#InstructoresIng").val();
        EnfermedadIng = $("#EnfermedadIng").val();
        ObservacionesIng = $("#ObservacionesIng").val();

        FechaIng = $("#FechaIng").val();
        PechoIng = $("#PechoIng").val();
        EspaldaIng = $("#EspaldaIng").val();
        CinturaIng = $("#CinturaIng").val();
        CaderaIng = $("#CaderaIng").val();
        PiernaIng = $("#PiernaIng").val();
        PantorrillaIng = $("#PantorrillaIng").val();
        BrazoIng = $("#BrazoIng").val();
        AntebrazoIng = $("#AntebrazoIng").val();
        TricepsIng = $("#TricepsIng").val();
        AbdominalIng = $("#AbdominalIng").val();
        SupralliacoIng = $("#SupralliacoIng").val();
        SubscapularIng = $("#SubscapularIng").val();
        PesoIng = $("#PesoIng").val();
        EstaturaIng = $("#EstaturaIng").val();
        IMGIng = $("#IMGIng").val();
        GrasaIng = $("#GrasaIng").val();
        $.ajax({
            type: "post",
            url: "/Clientes/Ingreso",
            data: {
                nombreing: NombreIng, primerapeing: PrimerApeIng, segundoapeing: SegundoApeIng, telefonoing: TelefonoIng, fechaingresoing: FechaIngresoIng,
                instructoresing: InstructoresIng, enfermedading: EnfermedadIng, observacionesing: ObservacionesIng, fechaing: FechaIng,
                pechoing: PechoIng, espaldaing: EspaldaIng, cinturaing: CinturaIng, caderaing: CaderaIng, piernaing: PiernaIng, pantorrillaing: PantorrillaIng, brazoing: BrazoIng,
                antebrazoing: AntebrazoIng, tricepsing: TricepsIng, abdominaling: AbdominalIng, supralliacoing: SupralliacoIng, subscapularing: SubscapularIng,
                pesoing: PesoIng, estaturaing: EstaturaIng, imging: IMGIng, grasaing: GrasaIng
            },
            success: function (data) {
                if (data.bol) {
                    $("#cerrarcli").click();
                } else {
                    $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                        "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                        "</div>");
                }
            }
        });
        $.ajax({
            type: "post",
            url: "/Clientes/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#cerrarcli").click();
    });

    $("#btnGuardarEdit").click(function () {
        NombreIng = $("#NombreIng2").val();
        PrimerApeIng = $("#PrimerApeIng2").val();
        SegundoApeIng = $("#SegundoApeIng2").val();
        TelefonoIng = $("#TelefonoIng2").val();
        FechaIngresoIng = $("#FechaIngresoIng2").val();
        InstructoresIng = $("#InstructoresIng2").val();
        EnfermedadIng = $("#EnfermedadIng2").val();
        ObservacionesIng = $("#ObservacionesIng2").val();
        $.ajax({
            type: "post",
            url: "/Clientes/GuardarEdicion",
            data: {
                nombreing: NombreIng, primerapeing: PrimerApeIng, segundoapeing: SegundoApeIng, telefonoing: TelefonoIng, fechaingresoing: FechaIngresoIng,
                instructoresing: InstructoresIng, enfermedading: EnfermedadIng, observacionesing: ObservacionesIng, idpersona: idPersonaA
            },
            success: function (data) {
                if (data.bol) {
                    $("#CerrarcliBtn").click();
                } else {
                    $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                        "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                        "</div>")
                }
            }
        });
        $.ajax({
            type: "post",
            url: "/Clientes/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#CerrarcliBtn").click();
    });

    $("#btnEliminar").click(function () {
        $.ajax({
            type: "post",
            url: "/Clientes/Eliminar",
            data: { idpersona: idPersonaA },
            success: function (data) {
                
            }
        });
        $.ajax({
            type: "post",
            url: "/Clientes/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
    });
});


function EditarAjax(idPersona) {
    $.ajax({
        type: "post",
        url: "/Clientes/Editar",
        data: {idpersona: idPersona},
        success: function (data) {
            $("#NombreIng2").val(data.nombre);
            $("#PrimerApeIng2").val(data.apellido1);
            $("#SegundoApeIng2").val(data.apellido2);
            $("#TelefonoIng2").val(data.telefono);
            $("#InstructoresIng2").val(data.instructor);
            document.getElementById("FechaIngresoIng2").value = data.fechaP;
            $("#EnfermedadIng2").val(data.enfermedad);
            $("#ObservacionesIng2").val(data.observaciones);
            idPersonaA = data.idPersona;
        }
    });
}



