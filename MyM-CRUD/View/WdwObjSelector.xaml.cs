using MyM_CRUD.Model;
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
using System.Windows.Shapes;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para WdwObjSelector.xaml
    /// </summary>
    public partial class WdwObjSelector : Window 
    {
        public object Selected { get => Datagrid.SelectedItem; }
        public bool Canceled { get; set; }
        public WdwObjSelector(Func<dynamic> getAllFromDB)
        {
            InitializeComponent();
            Canceled = true;

            var objects = getAllFromDB();
            Datagrid.ItemsSource = objects;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid.SelectedItem == null) return;

            Canceled = false;
            Close();
        }

        private void Datagrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            BtnAccept.IsEnabled = Datagrid.SelectedItem != null;
        }
    }
}
