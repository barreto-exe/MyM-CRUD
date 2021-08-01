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
using System.Linq;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para WdwActivitiesSelect.xaml
    /// </summary>
    public partial class WdwActivities : Window
    {
        private List<ServiceActivity> activities;
        public List<ServiceActivity> SelectedActivities
        {
            get => (from activity in activities
                   where activity.IsSelected
                   select activity).ToList();
        }
        public bool Canceled { get; set; }

        public WdwActivities()
        {
            InitializeComponent();
            Canceled = true;

            activities = ServiceActivity.ServiceActivities();
            Datagrid.ItemsSource = activities;
        }

        private void Datagrid_CurrentCellValueChanged(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValueChangedEventArgs e)
        {
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Canceled = false;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
