$(document).ready(function () {
    $("#btnLog").click(function () {
        User = $("#user").val();
        Pass = $("#pass").val();
        $.ajax({
            type: "post",
            url: "/Home/LoginEmp",
            data: { user: User, pass: Pass },
            success: function (data) {
                if (data.boole) {
                    window.location.href = "/Home/Login2"
                } else {
                    $("#error").html("<br></br> <div class=\"alert alert-danger\">" +
  "<strong>Alerta!</strong> Error usuario o contraseña"+
"</div>")
                }
            }
        });
    });
    $("#btnLog1").click(function () {
        User = $("#user").val();
        Pass = $("#pass").val();
        $.ajax({
            type: "post",
            url: "/Home/LoginEmp2",
            data: { user: User, pass: Pass },
            success: function (data) {
                if (data.boole) {
                    window.location.href = "/Clientes/TablaClient"
                } else {
                    $("#error").html("<br></br> <div class=\"alert alert-danger\">" +
   "<strong>Alerta!</strong> Error usuario o contraseña" +
 "</div>")
                }
            }
        });
    });

    $("#eliminar").click(function () {
        User = $("#user").val();
        Pass = $("#pass").val();
        $.ajax({
            type: "post",
            url: "/Home/LoginEmp2",
            data: { user: User, pass: Pass },
            success: function (data) {
                if (data.boole) {
                    window.location.href = "/Clientes/TablaClient"
                } else {
                    $("#error").html("<br></br> <div class=\"alert alert-danger\">" +
  "<strong>Alerta!</strong> Error usuario o contraseña" +
"</div>")
                }
            }
        });
    });
});