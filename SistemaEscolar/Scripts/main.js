
//icono que permite ocultar o mostrar el menú lateral izquierdo
let ActivarmenuLateral = document.querySelectorAll('.iconomenu-lateral');

//contenedor del menú 
let menu_lateral = document.querySelectorAll('.menu-lateral');

//variable para guardar el Id del alumno
let estudiantetId =0;

//variable que tendrá la URL del controlador al que se realizara la petición AJAX
let urlConsulta = "";

//variable el cual tendrá la función de lamacenar el nombre del elemento HTML en el cual se mostrarán el resultado de la petición AJAX
let contenedorResultado = "";

let parametrosConsulta;

//recorremos todos los elementos con clase ActivarmenuLateral
ActivarmenuLateral.forEach((elemento, indice) => {
    elemento.addEventListener('click', e => {
        menu_lateral[indice].classList.toggle('menu-lateral_Show');
    });
});

//recorremos todos los elementos con clase menu_lateral
menu_lateral.forEach(elemento=> {
    elemento.addEventListener("click", e => {
        elemento.classList.toggle('menu-lateral_Show');
    });
});

//filtramos los Estudiantes mediante carácteres
$(document).ready(function () {
    // Captura el evento keyup del input
    $('.registro-cajatexto__input--estudiante').on('keyup', function () {
        // Obtén el valor del input
        var searchTerm = $(this).val();

        // Realiza la llamada AJAX al controlador
        $.ajax({
            type: 'POST',
            url: '/Estudiante/FiltrarEstudiante', // Ruta al controlador
            data: { searchTerm: searchTerm }, // Envia los caracteres al servidor
            success: function (data) {
                // Actualiza la vista con los resultados devueltos por el controlador
                $('.registro__lectura').html(data);

                //Función que nos permite realizar buscar la información del estudiante mediante su Código por defecto se mostraran los datos coun ID 1
                BuscarEstudiantesPorIDAJAX(1);
            }
        });

    });
});

// variable que nos permite Obtener el elemento que fue clicado (en este caso, los enlaces)
let enlace="";

//variable que nos permite extraer el parametro que se envia mediante el elemento @Url.Action
let searchParams=0;

//utilizamos la delegación de eventos nos permite que los eventos estén activos desde el inicio y también funcionen en elementos agregados dinámicamente
//este apartado es unicamnete para elementos que posean el evento click
document.addEventListener("click", e => {

    // registro__id es el contenedor que muestra los datos del estudiante mediante su ID o codigo 
    let registro__id = document.querySelector(".registro__id");


    //elemento que está ubicado en _BusquedastudianteID
    if(e.target.matches(".detalle__boton")) {
        //Mostramos el contenedor  que muestra los detalles del estudiante
        registro__id.classList.toggle("registro__id-mostrar");
    }

    //botón para poder editar los datos del estudiante mediante su ID
    if(e.target.matches(".detalle__editar")) {
        e.preventDefault();

        console.log("Boton editar estudiante");
        // Obtener el elemento que fue clicado (en este caso, el enlace de editar estudiante)
        enlace = e.target;

        //dirección del controlador
        urlConsulta = enlace.getAttribute("href");

        //extraemos el parametro(id) del estudiante a editar
         searchParams = urlConsulta.split("?")[1];

        //extraemos unicamente la url Obtiene el path ("/Estudiante/EditarEstudiante")
        urlConsulta = urlConsulta.split("?")[0];

        // Convierte la cadena de parámetros en un objeto URLSearchParams
        const params = new URLSearchParams(searchParams);
        let idestudiante = params.get("idestudiante");

        //contenedor en el que se mostraran los datos
        contenedorResultado = $('.registro__id');

        // Llamar a la función para una consulta específica
         parametrosConsulta = { estudiantetId: idestudiante };

        //llamamos a la Función AJAX para enviar los parametros
        realizarConsultaAjax(urlConsulta, parametrosConsulta, function (data) {
            contenedorResultado.html(data); // Actualiza la vista con los resultados

            // Removemos el evento click anteriores antes de agregar los nuevos
            $('.registro__lectura').off('click', '.datos');
        });
    }

    //buscarestudianteID obtiene un lodList con todos los elementos html que tiene la información de cada uno de los estudiantes
    let buscarestudianteID = document.querySelectorAll(".datos");

    buscarestudianteID.forEach(elemento => {
        elemento.addEventListener("click", e => {

            //detenemos el evento al realizar el primer click
            e.stopImmediatePropagation();

            // Obtener el ID del producto desde el atributo data
            estudiantetId = elemento.getAttribute("data-id");

            //Función que nos permite realizar buscar la información del estudiante mediante su Código
            BuscarEstudiantesPorIDAJAX(estudiantetId);

            //Mostramos el contenedor  que muestra los detalles del estudiante
            registro__id.classList.toggle("registro__id-mostrar");
        });
    });
});

//let btnAgregarEstudiante = document.querySelector(".nav__boton-agregar");

document.querySelector(".nav__boton-agregar").addEventListener("click", e => {

    $.ajax({
        url: "/Estudiante/AgregarEstudiante",// Ruta al método del controlador
        type: "GET", // Método HTTP GET
        dataType: "html", // Tipo de datos esperados
        success: function (data) {
            // Cargar la vista parcial en el contenedor
            $(".registro__lectura").html(data);
        },
        error: function () {
            alert("Se produjo un error al cargar la vista parcial.");
        }
    });

});



//Función que nos permite realizar buscar la información del estudiante mediante su Código
function BuscarEstudiantesPorIDAJAX(id) {

    //dirección del controlador
    urlConsulta = "/Estudiante/BuscarEstudiantesPorID";

    //contenedor en el que se mostraran los datos
    contenedorResultado = $('.registro__id');

    // Llamar a la función para una consulta específica
    parametrosConsulta = { estudiantetId: id };

    //llamamos a la Función AJAX para enviar los parametros
    realizarConsultaAjax(urlConsulta, parametrosConsulta, function (data) {
        contenedorResultado.html(data); // Actualiza la vista con los resultados

        // Removemos el evento click anteriores antes de agregar los nuevos
        $('.registro__lectura').off('click', '.datos');
    });
}

//Función generica para realizar peticiones AJAX
function realizarConsultaAjax(url, parametros, successCallback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: parametros,
        success: function (data) {
            successCallback(data);
        }
    });
}

//ejecutamos esta función al cargar nuestro sistema 
BuscarEstudiantesPorIDAJAX(1);
