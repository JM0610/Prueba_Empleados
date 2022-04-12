$(function () {

    $('.list-group-item').on('click', function () {
        alert('hoa')
        $('.glyphicon', this)
            .toggleClass('glyphicon-chevron-right')
            .toggleClass('glyphicon-chevron-down');
       
    });

});

$(document).ready(function () {
    obtener_area();
});
function obtener_area() {
    $.ajax({
        url: '../Area/GetAreas',
        type: 'Get',

    }).done(function (data) {
        $(data).each(function (item, val) {
            //$('#seccion_area').append('<option value="' + val.idArea + '">' + val.nombre + '</option>');
            $('#seccion_area').append('<a href="#item' + item +'" class="list-group-item" data-toggle="collapse"><i class="glyphicon glyphicon-chevron-right"></i>'+val.nombre+'</a>')
            $('#seccion_area').append('<div class="list-group collapse" id="item'+item+'"></div>')
            obtener_superior('item' + item, val.idArea);
        });

    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function obtener_superior(parametro,filtro) {
    $.ajax({
        url: '../Empleado/GetSuperior',
        type: 'Get',
        data: { id: filtro }
    }).done(function (data) {
        //console.log(data);
        $(data).each(function (item, val) {
            //$('#seccion_area').append('<option value="' + val.idArea + '">' + val.nombre + '</option>');

            $('#' + parametro).append('<a href="#da' + item + '" class="list-group-item" data-toggle="collapse><i class="glyphicon glyphicon-chevron-right"></i>'+val.nombreCompleto+'</a>')
            $('#' + parametro).append('<div class="list-group collapse show" id="da'+item+'"></div>')
            obtener_inferior(('da' + item), val.idEmpleado)
            //$('#' + parametro).append('<div class="list-group collapse" id="' + (parametro + item) + '">a href="#" class="list-group-item">' + val.nombreCompleto + '</a></div>')
        });

    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}
function obtener_inferior(parametro, filtro) {
    $.ajax({
        url: '../Empleado/GetInferior',
        type: 'Get',
        data: { id: filtro }
    }).done(function (data) {
        console.log(data);
        $(data).each(function (item, val) {
            //$('#seccion_area').append('<option value="' + val.idArea + '">' + val.nombre + '</option>');

            $('#' + parametro).append('<a href="#" class="list-group-item"><i class="glyphicon glyphicon-chevron-down"></i>'+val.nombreCompleto+'</a>')

        });

    }).fail(function () {
        alert('Ocurrio un error al procesar la información')

    })
}