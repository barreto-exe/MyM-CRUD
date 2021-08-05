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

            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            Axis axis = new Axis();
            sep.IsEnabled = false;
            sep.Step = 5;
            axis.Separator = sep;
            cartesianChart2.AxisX.Add(axis);
            cartesianChart4.AxisX.Add(axis);
            cartesianChart1.AxisX.Add(axis);
            cartesianChart3.AxisX.Add(axis);
            cartesianChart2.AxisY.Add(axis);
            cartesianChart4.AxisY.Add(axis);
            cartesianChart1.AxisY.Add(axis);
            cartesianChart3.AxisY.Add(axis);

            cartesianChart2.Series = new SeriesCollection()
            {
                new RowSeries(){
                    Values = new ChartValues<double> {25,15,20},
                    Fill = Brushes.Green
                    
                },
                
                
            };
            
            cartesianChart4.Series = new SeriesCollection()
            {
                new StackedAreaSeries(){
                    Values = new ChartValues<double> {25,15,20},
                    Fill = Brushes.GreenYellow
                }
            };
            
            cartesianChart1.Series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<decimal> { 5, 6, 2, 20 },

                },
                new LineSeries()
                {
                    Values = new ChartValues<double> { 3, 5, 7, 4 }
                }

            };

            cartesianChart3.Series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<decimal> { 8, 10, 2, 25 },
                    Fill = Brushes.LightYellow
                },
                

            };
        }
    }
}
