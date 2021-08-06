using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using MyM_CRUD.DataBase;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageExample.xaml
    /// </summary>
    public partial class PageEstadisticas : Page
    {

        public PageEstadisticas()
        {
            InitializeComponent();
            //Ventas de productos
            cartesianChart1.Series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 20 },
                    Stroke=Brushes.GreenYellow,
                    Fill=Brushes.Transparent
                },
                new LineSeries()
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4, 15 },
                    Stroke=Brushes.Green,
                    Fill=Brushes.Transparent
                }

            };
            //Clientes mas frecuentes
            cartesianChart2.Series = new SeriesCollection()
            {
                new RowSeries(){
                    Values = new ChartValues<double> {2,7,15,20,30},
                    Fill = Brushes.Green                   
                },
                               
            };

            
            
            //Comparacion entre franquicias
            cartesianChart4.Series = new SeriesCollection()
            {
                new StackedAreaSeries(){
                    Values = new ChartValues<double> {25,15,20,2},
                    Fill = Brushes.GreenYellow
                }
            };
            //Marcas de vehiculo mas atendidas por tipo de servicio
            cartesianChart6.Series = new SeriesCollection()
            {
                new LineSeries(){
                    Values = new ChartValues<double> {25,15,20,23,7},
                    Stroke = Brushes.LimeGreen,
                    Fill = Brushes.Transparent
                },

            };


            /*string query =
                "SELECT s.nombreS, ma.nombreMarca, COUNT(r.*) AS cantidad " +
                "INTO TEMP R1  " +
                "FROM registro r, ordenes_servicio os, servicios s, vehiculos v, modelo mo, marca ma " +
                "WHERE r.placaVehiculo=v.placaVehiculo " +
                "AND v.modelo = mo.nombreMo " +
                "AND r.numFicha = os.numFicha " +
                "AND r.franquicia = ?" +
                "GROUP BY 1,2;" +

                "SELECT R1.nombreS, MAX(R1.cantidad) AS maximo" +
                "INTO TEMP R2" +
                "FROM R1" +
                "GROUP BY 1;" +

                "SELECT R1.*" +
                "FROM R1, R2" +
                "WHERE R1.nombreS=R2.nombreS"+
                "AND R1.cantidad=R2.maximo;";

            PostgreOp op = new PostgreOp(query);*/

            //Servicios mas solcitado
            cartesianChart7.Series = new SeriesCollection()
            {
                new RowSeries(){

                    Values = new ChartValues<double> {21,8,15,20,2,19},
                    DataLabels = true,
                    LabelPoint = point => point.Y + " Nombre",
                    Fill = Brushes.LimeGreen

                },

            };

            //Personal que realiza mas servicios
            cartesianChart3.Series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<decimal> { 8, 10, 5, 25, 30 },
                    Stroke = Brushes.Green,
                    Fill = Brushes.PaleGreen
                },


            };

            //Proveedores que suminstran
            cartesianChart5.Series = new SeriesCollection()
            {
                new StackedAreaSeries(){
                    Values = new ChartValues<double> {2, 13, 10, 25, 6, 19},
                    Fill = Brushes.GreenYellow
                }
            };

        }
    }
}
