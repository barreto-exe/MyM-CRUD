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
            var labels = new List<string>();
            IChartValues values;

            //Ventas de productos
            (labels, values) = Stadistics.ChartProductSellCollection();
            ChartProductSell.AxisX[0].Labels = labels.ToArray();
            ChartProductSell.Series = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Values = values,
                    DataLabels = true,
                    Stroke=Brushes.GreenYellow,
                    Fill=Brushes.Green
                },

            };

            //Clientes mas frecuentes
            (labels, values) = Stadistics.ChartFrequentClientsCollection();
            ChartFrequentClients.AxisY[0].Labels = labels.ToArray();
            ChartFrequentClients.Series = new SeriesCollection()
            {
                new RowSeries(){
                    Values = values,
                    DataLabels = true,
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
            (labels, values) = Stadistics.ChartVehicleBrandsCollection();
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

            //Servicios mas solicitado
            (labels, values) = Stadistics.ChartHighServiceCollection();
            ChartHighService.AxisY[0].Labels = labels.ToArray();
            ChartHighService.Series = new SeriesCollection()
            {
                new RowSeries(){

                    Values = values,
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
                    DataLabels = true,
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
