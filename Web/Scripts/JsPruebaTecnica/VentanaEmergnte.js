var popup = document.getElementById("popup-form");
var openButton = document.getElementById("open-popup");
var closeButton = document.getElementById("close-popup");
var pageContent = document.getElementById("page-content");

openButton.addEventListener("click", function () {
    popup.style.display = "block";
    pageContent.style.pointerEvents = "none";
});

closeButton.addEventListener("click", function () {
    popup.style.display = "none";
    pageContent.style.pointerEvents = "auto";
    $('#id').val(0);
});



// sen data ajax
$(document).ready(function () {
    $("form").submit(function (event) {
        event.preventDefault();

        // get form values
        var formData = new FormData(this);

        // send data to server
        $.ajax({
            type: "POST",
            url: $(this).attr("action"),
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                console.log(response)
                if (response.success) {
                    alert('Datos enviados correctamente');
                    popup.style.display = "none";
                    pageContent.style.pointerEvents = "auto";
                } else {
                    alert('Ha ocurrido un error al enviar los datos');
                }
            },
            error: function () {
                alert('Ha ocurrido un error al enviar los datos');
            }
        });
    });
});


//Editar empleado
function editarEmpleado(employeeId) {
    // obtener la fila correspondiente al id del empleado
    var $row = $('#' + employeeId);

    // obtener los valores de los campos en la fila
    var name = $row.find('td:eq(0)').text().trim();
    console.log($row.find('td:eq(0)').text().trim());
    var type = $row.find('td:eq(1)').text().trim();
    var telephone = $row.find('td:eq(2)').text().trim();
    var address = $row.find('td:eq(3)').text().trim();
    var employmentDate = $row.find('td:eq(4)').text().trim();
    console.log(employmentDate);
    var DateFormate = formatoDate(employmentDate);
    console.log(DateFormate);

    // cargar los valores en el formulario
    $('#id').val(employeeId);
    $('#name').val(name);
    $('#type').val(type);
    $('#telephone').val(telephone);
    $('#address').val(address);
    $('#employmentDate').val(DateFormate);

    // Mostrar la ventana emergente
    popup.style.display = "block";
    pageContent.style.pointerEvents = "none";
}

function formatoDate(datetimeString) {
    // Separar el string de fecha y hora en dos partes
    const [dateString, timeString] = datetimeString.split(' ');

    // Separar el string de fecha en sus componentes (día, mes, año)
    const [day, month, year] = dateString.split('/');

    // Dar formato a la fecha en el formato esperado para el input de tipo date
    const formattedDate = `${year}-${month}-${day}`;

    // Establecer la fecha en un input de tipo date
    return formattedDate;
}

//eliminar empleado
function eliminarEmpleado(employeeId) {
    if (confirm("¿Está seguro de que desea eliminar este empleado?")) {
        $.ajax({
            url:`/Employee/EliminarEmpleado?employeeId=${employeeId}`,
            type: "POST",
            success: function (respuesta) {
                if (respuesta.success) {
                    alert("Se ha eliminado con exito al empleado");
                    var $row = $('#' + employeeId);
                    $row.remove();
                }
                else {
                    alert(respuesta.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                try {
                    alert("Ocurrió un error al eliminar el empleado. Error: " + errorThrown);
                } catch (ex) {
                    alert("Ocurrió un error al eliminar el empleado.");
                }
            }
        });
    }
}