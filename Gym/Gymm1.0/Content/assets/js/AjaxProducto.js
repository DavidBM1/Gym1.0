var idPersonaA;

$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Producto/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });

    $("#IngresoPersonaBtn").click(function () {
        NombreIng = $("#NombreIng2").val();
        CantidadIng = $("#CantidadIng2").val();
        PrecioVentaIng = $("#PrecioVentaIng2").val();
        PrecioCompraIng = $("#PrecioCompraIng2").val();
        $.ajax({
            type: "post",
            url: "/Producto/Ingreso",
            data: {
                nombre: NombreIng, cantidad: CantidadIng, precioCompra: PrecioVentaIng, precioVenta: PrecioCompraIng
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
            url: "/Producto/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#btnIn").click();
    }); 

    $("#btnGuardarEdit").click(function () {
        NombreIng = $("#NombreIng23").val();
        CantidadIng = $("#CantidadIng23").val();
        PrecioVentaIng = $("#PrecioVentaIng23").val();
        PrecioCompraIng = $("#PrecioCompraIng23").val();
        $.ajax({
            type: "post",
            url: "/Producto/GuardarEdicion",
            data: {
                nombre: NombreIng, cantidad: CantidadIng, precioCompra: PrecioVentaIng, precioVenta: PrecioCompraIng, idpro: idPersonaA
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
            url: "/Producto/Tabla",
            success: function (data) {
                $("#resultado").html(data.result);
            }
        });
        $("#CerrarEditar").click();
    });

});


function EditarAjax(idPersona) {
    $.ajax({
        type: "post",
        url: "/Producto/Editar",
        data: { idpersona: idPersona },
        success: function (data) {
            $("#NombreIng23").val(data.nombre);
            $("#CantidadIng23").val(data.cantidad);
            $("#PrecioVentaIng23").val(data.precioVenta);
            $("#PrecioCompraIng23").val(data.precioCompra);
            idPersonaA = data.idProducto;
        }
    });
}

function Eliminar(id) {
    idPersonaA = id;
}