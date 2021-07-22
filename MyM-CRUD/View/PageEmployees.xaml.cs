using MyM_CRUD.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageWorkers.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        private DataTable employees;

        public PageEmployees()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            employees = Employee.SearchEmployees("");
            DgEmployees.ItemsSource = employees.DefaultView;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            employees = Employee.SearchEmployees(TxtSearch.Text);
            DgEmployees.ItemsSource = employees.DefaultView;
        }
    }
}
