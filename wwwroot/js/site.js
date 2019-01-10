// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


        function dibujar()
        {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Hombres');
        data. addColumn('string','Mujeres');
        data.addRows(
        [
        ['Hombres',counterh],
        ['Mujeres',counterm]
        ]
        );
        var opciones = {'title':'Género',
        'width':500,
        'height':300};
        var grafica = new google.visualization.PieChart(document.getElementById('charts'));
        grafica.draw(data, opciones);
    }