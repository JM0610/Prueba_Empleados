var flag = 0;
$('#btn_nuevo').on('click', function () {
    flag = 1;
    $('#accion').html('Registro de nueva area');
    $('#md_area').modal('show');
    $('#idArea').val('');
    $('#Nombre').val('');
    $('#Descripcion').val('');
})
$('.btn_cancelar').on('click', function () {
    flag = 0;
    $('#accion').html('');
    $('#idArea').val('');
    $('#Nombre').val('');
    $('#Descripcion').val('');
});
$('#tbl_area').on('click', '.btn_actualizar', function () {
    flag = 2;
    $('#accion').html('Actualización de datos');
    $('#md_area').modal('show');
    $('#idArea').val($(this).closest('tr').find('td').eq(0).html());
    $('#Nombre').val($(this).closest('tr').find('td').eq(1).html());
    $('#Descripcion').val($(this).closest('tr').find('td').eq(2).html());
});
$('#tbl_area').on('click', '.btn_eliminar', function () {
    flag = 3;
    $('#idArea').val($(this).closest('tr').find('td').eq(0).html());
    $('#md_confirmar_accion').modal('show');
});

$('#btn_guardar').on('click', function () {
    if (validar_requeridos() == true) {
        $('#md_confirmar_accion').modal('show');
    } else {
        alert('Datos incompletos o erroneos, verificar formulario');
    }
    
});
$('#btn_procesar').on('click', function () {
    
        switch (flag) {
            case 1:
                guardar_area();
                break;
            case 2:
                actualizar_area();
                break;
            case 3:
                borrar_area();
                break;
            default:
                break;
   
   
    }
    
})
function guardar_area() {
    $.ajax({
        url: '../Area/AddArea',
        type: 'Post',
        data: {
            idArea: $('#idArea').val(),
            Nombre: $('#Nombre').val(),
            Descripcion: $('#Descripcion').val()
        }
    }).done(function (data) {
        if (data == true) {
            $('#busqueda').val('');
            alert('Datos almacenados exitosamente');
            window.location.reload();
        } else {
            $('#md_confirmar_accion').modal('hide');
            alert('No se pudo completar la operación,pueden existir dependencias del registro')
        }
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function actualizar_area() {
    $.ajax({
        url: '../Area/UpdateArea',
        type: 'Post',
        data: {
            idArea: $('#idArea').val(),
            Nombre: $('#Nombre').val(),
            Descripcion: $('#Descripcion').val()
        }
    }).done(function (data) {
        if (data == true) {
            $('#busqueda').val('');
            alert('Datos actualizados exitosamente');
            window.location.reload();
        } else {
            $('#md_confirmar_accion').modal('hide');

            alert('No se pudo completar la operación,pueden existir dependencias del registro')
        }
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}

function borrar_area() {
    $.ajax({
        url: '../Area/DeleteArea',
        type: 'Delete',
        data: {
            id: $('#idArea').val()
        }
    }).done(function (data) {
        if (data == true) {
            $('#busqueda').val('');
            alert('Datos eliminados exitosamente');
            window.location.reload();
        } else {
            $('#md_confirmar_accion').modal('hide');
            alert('No se pudo completar la operación,pueden existir dependencias del registro')
        }
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}

function validar_requeridos() {

    var error = 0;
    if ($('#Nombre').val() == '' || $('#Nombre').val() == null) {
        error += 1;
        $('#Nombre').addClass('is-invalid');
        $('#lbl_nombre').addClass('text-danger');
        $('#lbl_nombre').html('* Nombre(completar)');
    } else
    {
        
        if ($('#Nombre').val().length <= 100) {
            $('#Nombre').removeClass('is-invalid');
            $('#lbl_nombre').removeClass('text-danger');
            $('#lbl_nombre').html('Nombre:');
        } else {
            error += 1;
            $('#Nombre').addClass('is-invalid');
            $('#lbl_nombre').addClass('text-danger');
            $('#lbl_nombre').html('* Nombre(maximo 100 caracteres)');
        }
    }

    if ($('#Descripcion').val() == '' || $('#Descripcion').val() == null) {
        error += 1;
        $('#Descripcion').addClass('is-invalid');
        $('#lbl_descripcion').addClass('text-danger');
        $('#lbl_descripcion').html('* Descripcion(completar)');
    } else {
        if ($('#Descripcion').val().length <= 2000) {
            $('#Descripcion').removeClass('is-invalid');
            $('#lbl_descripcion').removeClass('text-danger');
            $('#lbl_descripcion').html('Descripcion:');
        } else {
            error += 1;
            $('#Nombre').addClass('is-invalid');
            $('#lbl_nombre').addClass('text-danger');
            $('#lbl_nombre').html('* Descripcion(maximo 2000 caracteres)');
        }
    }
    return error > 0 ? false : true;
}