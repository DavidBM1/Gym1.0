$(document).ready(function () {
    $.ajax({
        type: "post",
        url: "/Compras/Combo",
        success: function (data) {
            $("#comboDiv").html(data.result);
        }
    });
    $.ajax({
        type: "post",
        url: "/Compras/Tabla",
        success: function (data) {
            $("#resultado").html(data.result);
        }
    });

    $("#btnProdIn").click(function () {
        ProuctoId = $("#rol").val();
        Cantidad = $("#cantidadIn").val();
        Tabla = $("#Tabla2").html();
        $.ajax({
            type: "post",
            url: "/Compras/Tabla2",
            data: { id: ProuctoId, can: Cantidad },
            success: function (data) {
                $("#Tabla2").html(Tabla + data.result);
                totalPasado = $("#Total").val();
                totalint = parseInt(totalPasado);
                $("#Total").val(totalint + data.total);
            }
        });
    });

    $("#btnCompra").click(function () {
        totalPasado = $("#Total").val();
        correo = $("#EmailIn").val();
        $.ajax({
            type: "post",
            url: "/Compras/Insertar",
            data: { total: totalPasado, correo: correo },
            success: function (data) {
                $("#cerrar").click();
                $.ajax({
                    type: "post",
                    url: "/Compras/Tabla",
                    success: function (data) {
                        $("#resultado").html(data.result);
                    }
                });
            }
        });
    });
    
});

function eliminar(id) {
    $.ajax({
        type: "post",
        url: "/Compras/EliminarProd",
        data: {idd: id},
        success: function (data) {
            $('#' + id).remove();
            totalPasado = $("#Total").val();
            totalint = parseInt(totalPasado);
            $("#Total").val(totalint - data.result);
        }
    });
}

function cargarTabla(id) {
    $.ajax({
        type: "post",
        url: "/Compras/Segunda",
        data: {
            id: id
        },
        success: function (data) {
            $("#tabla2").html(data.result);
        }
    });
}