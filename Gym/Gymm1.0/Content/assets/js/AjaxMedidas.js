var idPersonaA;
var bol = false;

$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Medidas/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });
    $("#MostrarGrafica").click(function () {
        medidaGraf = $("#medidaGraf").val();
        $.ajax({
            type: "post",
            url: "/Medidas/Grafico",
            data: {
                medidaGraf: medidaGraf, idPersona: idPersonaA
            },
            success: function (data) {
                window.open('Grafica', '_blank');
            }
        });
    });

    $("#AgregarMedidas").click(function () {
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
            url: "/Medidas/Ingreso",
            data: {
                fechaing: FechaIng, pechoing: PechoIng, espaldaing: EspaldaIng, cinturaing: CinturaIng, caderaing: CaderaIng, piernaing: PiernaIng,
                pantorrillaing: PantorrillaIng, brazoing: BrazoIng, antebrazoing: AntebrazoIng, tricepsing: TricepsIng, abdominaling: AbdominalIng,
                supralliacoing: SupralliacoIng, subscapularing: SubscapularIng, pesoing: PesoIng, estaturaing: EstaturaIng, imging: IMGIng, grasaing: GrasaIng, idPersona: idPersonaA
            },
            success: function (data) {
                if (data.bol) {
                } else {
                    $("#error").html("<br></br><div class=\"alert alert-danger\">" +
                                        "<strong>Alerta!</strong> Error a la hora de ingresar" +
                                        "</div>")
                }
            }
        });
        $("#btnEd").click();
        CargarTabla2(idPersonaA);
    });
});


function Eliminar(id) {
    idPersonaA = id;
}

function CargarTabla2(id) {
    idPersonaA = id;
    $.ajax({
        type: "post",
        url: "/Medidas/Tabla2",
        data: { idPersona: id },
        success: function (data) {
            $("#resultadoMed").html(data.result);
            $("#UsuarioMed").val(data.nombre);
        }
    });
}