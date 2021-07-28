using MyM_CRUD.Model;
using Syncfusion.UI.Xaml.Grid;
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
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Service>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageServices.xaml
    /// </summary>
    public partial class PageServices : Page, ICrudPage<Service>
    {
        private List<Service> services;
        public State CurrentState { get; set; }

        public PageServices()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Service> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            services = Service.SearchServices("");
            Datagrid.ItemsSource = services;
        }
        

        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public void Datagrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Service GetObjectsFromFields()
        {
            throw new NotImplementedException();
        }

        public void LoadFields(Service selected)
        {
            throw new NotImplementedException();
        }
        private void CheckUnderReserve_Checked(object sender, RoutedEventArgs e)
        {

        }

        public void SetCreating()
        {
            throw new NotImplementedException();
        }
        public void SetReading()
        {
            throw new NotImplementedException();
        }
        public void SetUpdating()
        {
            throw new NotImplementedException();
        }
        public void BeginUpdating()
        {
            throw new NotImplementedException();
        }
    }
}
