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
using MyM_CRUD.Model;

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
            BuildCharts();
        }

        private void BuildCharts()
        {
            //Ventas de productos
            ChartProductSell.Series = new SeriesCollection()
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
            ChartFrequentClients.Series = new SeriesCollection()
            {
                new RowSeries(){
                    Values = new ChartValues<double> {2,7,15,20,30},
                    Fill = Brushes.Green
                },
            };

            //Comparacion entre franquicias
            ChartBranches.Series = new SeriesCollection()
            {
                new StackedAreaSeries(){
                    Values = new ChartValues<double> {25,15,20,2},
                    Fill = Brushes.GreenYellow
                }
            };

            //Marcas de vehiculo mas atendidas por tipo de servicio
            (var labels, var values) = Stadistics.ChartVehicleBrandsCollection();
            ChartVehicleBrands.AxisX[0].Labels = labels.ToArray();
            ChartVehicleBrands.Series = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Values = values,
                    DataLabels = true,
                    Fill = Brushes.LimeGreen
                },

            };

            //Servicios mas solcitado
            ChartHighService.Series = new SeriesCollection()
            {
                new RowSeries(){

                    Values = new ChartValues<double> {21,8,15,20,2,19},
                    LabelPoint = point => point.Y + " Nombre",
                    DataLabels = true,
                    Fill = Brushes.LimeGreen
                },

            };

            //Personal que realiza mas servicios
            (labels, values) = Stadistics.ChartHighWorkerCollection();
            ChartHighWorker.AxisY[0].Labels = labels.ToArray();
            ChartHighWorker.Series = new SeriesCollection()
            {
                new RowSeries()
                {
                    Values = values,
                    Fill = Brushes.PaleGreen
                },


            };

            //Proveedores que suminstran
            ChartHighSupplier.Series = new SeriesCollection()
            {
                new StackedAreaSeries(){
                    Values = new ChartValues<double> {2, 13, 10, 25, 6, 19},
                    Fill = Brushes.GreenYellow
                }
            };
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BuildCharts();
        }
    }
}
