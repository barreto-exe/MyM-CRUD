using MaterialDesignThemes.Wpf;
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
using System.Xml.Linq;
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
            services = Service.SearchServices(TxtSearch.Text);
            Datagrid.ItemsSource = services;
        }
        public void Datagrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            Service service = (Service)Datagrid.SelectedItem;
            LoadFields(service);
            SetReading();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtCode.Focus();
        }

        public Service GetObjectsFromFields()
        {
            Int32.TryParse(TxtReserveTime.Text, out int reserveTime);

            return new Service()
            {
                Code = TxtCode.Text,
                Name = TxtName.Text,
                Description = TxtDescription.Text,
                ReserveTime = reserveTime,
                UnderReserve = (bool)CheckUnderReserve.IsChecked,
            };
        }
            
            

        public void LoadFields(Service selected)
        {
            if(selected == null)
            {
                TxtCode.Text = "";
                TxtName.Text = "";
                TxtDescription.Text = "";
                CheckUnderReserve.IsChecked = false;
                TxtReserveTime.Text = "";

                return;
            }

            TxtCode.Text = selected.Code;
            TxtName.Text = selected.Name;
            TxtDescription.Text = selected.Description;
            CheckUnderReserve.IsChecked = selected.UnderReserve;
            TxtReserveTime.Text = selected.ReserveTime.ToString();
        }
        private void CheckUnderReserve_Checked(object sender, RoutedEventArgs e)
        {
            if(CheckUnderReserve.IsChecked == true)
            {
                TxtReserveTime.Visibility = Visibility.Visible;
            }
            else
            {
                TxtReserveTime.Visibility = Visibility.Collapsed;
                TxtReserveTime.Text = "";
            }
        }

        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtCode.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtDescription.IsEnabled = true;
            CheckUnderReserve.IsEnabled = true;
            TxtReserveTime.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtCode.IsEnabled = false;
            TxtName.IsEnabled = false;
            TxtDescription.IsEnabled = false;
            CheckUnderReserve.IsEnabled = false;
            TxtReserveTime.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtCode.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtDescription.IsEnabled = true;
            CheckUnderReserve.IsEnabled = true;
            TxtReserveTime.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Service selected = (Service)Datagrid.SelectedItem;

            if (selected == null) return;

            LoadFields(selected);
            SetUpdating();
        }
    }
}
