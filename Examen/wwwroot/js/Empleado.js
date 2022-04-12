$(document).ready(function () {
    obtener_area();
    obtener_jefe();
    obtenerHabilidades_grl();
});
var flag = 0;
$('#btn_nuevo').on('click', function () {
    flag = 1;
    $('#accion').html('Registro de nuevo empleado');
    $('#md_empleado').modal('show');
    $('#NombreCompleto').val('')
    $('#Cedula').val('')
    $('#Correo').val('')
    $('#Fechanacimiento').val('')
    $('#Fechaingreso').val(0)
    $('#slt_jefe').val(0)
    $('#slt_area_emp').val(0)
    $('#idEmpleado').val('')
})

$('#tbl_empleado').on('click', '.btn_actualizar', function () {
    flag = 2;
    $('#accion').html('Actualización de datos');
    $('#md_empleado').modal('show');
    $('#accion').html('Registro de nuevo empleado');
    $('#md_empleado').modal('show');
    $('#idEmpleado').val($(this).closest('tr').find('td').eq(0).html())
    $('#NombreCompleto').val($(this).closest('tr').find('td').eq(1).html())
    $('#Cedula').val($(this).closest('tr').find('td').eq(2).html())
    $('#Correo').val($(this).closest('tr').find('td').eq(3).html())
    $('#Fechanacimiento').val($(this).closest('tr').find('td').eq(11).html())
    $('#Fechaingreso').val($(this).closest('tr').find('td').eq(10).html())
    $('#slt_jefe').val($(this).closest('tr').find('td').eq(9).html())
    $('#slt_area_emp').val($(this).closest('tr').find('td').eq(8).html())
    
    console.log($(this).closest('tr').find('td').eq(6).html())
});
$('#tbl_empleado').on('click', '.btn_eliminar', function () {
    flag = 3;
    $('#idEmpleado').val($(this).closest('tr').find('td').eq(0).html());
    $('#md_confirmar_accion').modal('show');
});
$('#tbl_empleado').on('click', '.btn_ver', function () {
    $('#md_emp_grl').modal('show');
    $('#idEmpleado').val($(this).closest('tr').find('td').eq(0).html());
    obtener_datos_generales();
    obtenerHabilidades();
});
$('#tbl_empleado').on('click', '.btn_habilida', function () {
    flag = 4;
    $('#md_habilidades').modal('show');
    $('#idEmpleado').val($(this).closest('tr').find('td').eq(0).html());
});
$('#btn_guardar').on('click', function () {
    //if (validar_requeridos() == true) {
        $('#md_confirmar_accion').modal('show');
    //} else {
      //  alert('Datos incompletos o erroneos, verificar formulario');
    //}

});
$('#btn_guardar_habilidades').on('click', function () {
    guardar_habilidad();
   
});
$('#btn_procesar').on('click', function () {
    switch (flag) {
        case 1:
            guardar_empleado();
            break;
        case 2:
            actualizar_empleado();
            break;
        case 3:
            borrar_empleado();
            break;
        case 4:
            guardar_habilidad();
            break;
        default:
            break;
    }

})
function obtener_area() {
    $.ajax({
        url: '../Area/GetAreas',
        type: 'Get',
        
    }).done(function (data) {
        $(data).each(function (item, val) {
            $('#slt_area_emp').append('<option value="' + val.idArea + '">' + val.nombre + '</option>');
            $('#idArea').append('<option value="' + val.idArea + '">' + val.nombre + '</option>');
        });
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function obtener_jefe() {
    $.ajax({
        url: '../Empleado/GetListEmpleado',
        type: 'Get',

    }).done(function (data) {
        $(data).each(function (item, val) {
            $('#slt_jefe').append('<option value="' + val.idEmpleado + '">' + val.nombreCompleto + '</option>');
        });

    }).fail(function () {
        alert('Ocurrio un error al procesar la informaciónXXX')

    })
}
function guardar_empleado() {
    var parametros = new FormData();
    var file_data = $('#FotoAdj')[0].files[0];
    parametros.append('Nombrecompleto', $('#NombreCompleto').val());
    parametros.append('Cedula', $('#Cedula').val());
    parametros.append('Correo' ,$('#Correo').val());
    parametros.append('Fechanacimiento', $('#Fechanacimiento').val());
    parametros.append('Fechaingreso', $('#Fechaingreso').val());
    parametros.append('Idjefe', $('#slt_jefe').val());
    parametros.append('idArea', $('#slt_area_emp').val());
    parametros.append('FotoAdj', file_data);
    $.ajax({
        url: '../Empleado/AddEmpleado',
        type: 'Post',
        data: parametros,
        processData: false,
        contentType: false
    }).done(function (data) {
        if (data == true) {
            $('#busqueda').val('');
            alert('Datos registrados exitosamente');
            window.location.reload();
        } else {
            $('#md_confirmar_accion').modal('hide');
            alert('No se pudo completar la operación,pueden existir dependencias del registro')
        }
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function actualizar_empleado() {
    var parametros = new FormData();
    var file_data = $('#FotoAdj')[0].files[0];
    parametros.append('IdEmpleado', $('#idEmpleado').val());
    parametros.append('Nombrecompleto', $('#NombreCompleto').val());
    parametros.append('Cedula', $('#Cedula').val());
    parametros.append('Correo', $('#Correo').val());
    parametros.append('Fechanacimiento', $('#Fechanacimiento').val());
    parametros.append('Fechaingreso', $('#Fechaingreso').val());
    parametros.append('Idjefe', $('#slt_jefe').val());
    parametros.append('idArea', $('#slt_area_emp').val());
    parametros.append('FotoAdj', file_data);
    $.ajax({
        url: '../Empleado/UpdateEmpleado',
        type: 'Post',
        data: parametros,
        processData: false,
        contentType: false
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
function obtener_datos_generales() {
    $.ajax({
        url: '../Empleado/GetDatosGenerales',
        type: 'Get',
        data: { id: $('#idEmpleado').val()}
    }).done(function (data) {
        console.log(data);
        $('#lbl_codigo').html(data.codigo);
        $('#lbl_nombre').html(data.nombreCompleto);
        $('#lbl_celula').html(data.cedula);
        $('#lbl_correo_grl').html(data.correo);
        $('#fecha_nacimiento').html(data.fechaNacimiento.substring(0,10));
        $('#fecha_ingreso').html(data.fechaIngreso.substring(0, 10));
        $('#lbl_jefe_grl').html(data.jefe);
        $('#lbl_edad_grl').html(data.edad <=0 ? 0 : data.edad);
        $('#lbl_tiempo_grl').html(data.tiempo <= 0 ? 0 : data.tiempo);
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function borrar_empleado() {
    $.ajax({
        url: '../Empleado/DeleteEmpleado',
        type: 'Delete',
        data: {
            id: $('#idEmpleado').val()
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
function guardar_habilidad() {
    $.ajax({
        url: '../Habilidad/AddHabilidad',
        type: 'Post',
        data: {
            idEmpleado: $('#idEmpleado').val(),
            NombreHabilidad: ($('#txt_habilidad').val() == '' || $('#txt_habilidad').val() == null) ? $('#slt_habilidad').val() : $('#txt_habilidad').val()
        }
    }).done(function (data) {
        if (data == true) {
            $('#busqueda').val('');
            alert('Datos registrados exitosamente');
            window.location.reload();
        } else {
            $('#md_confirmar_accion').modal('hide');
            alert('No se pudo completar la operación,pueden existir dependencias del registro')
        }
        
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function obtenerHabilidades() {
    $.ajax({
        url: '../Habilidad/GetHabilidad',
        type: 'Get',
        data: { id: $('#idEmpleado').val() }
    }).done(function (data) {
        console.log(data);
        $('#section_habilidades').html('')
        $(data).each(function (item, val) {
            $('#section_habilidades').append('<h5 class="col-12">'+val.nombreHabilidad+'</h5>')
        });
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function obtenerHabilidades_grl() {
    $.ajax({
        url: '../Habilidad/GetHabilidades',
        type: 'Get'
    }).done(function (data) {
        console.log(data);
        
        $(data).each(function (item, val) {
            $('#slt_habilidad').append('<option>' + val + '</option>')
        });
    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}