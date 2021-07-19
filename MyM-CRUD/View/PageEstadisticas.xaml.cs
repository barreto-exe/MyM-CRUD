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

            cartesianChart1.Series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 7 }
                },
                new LineSeries()
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                }
                
            };
        }

        private void cartesianChart1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
