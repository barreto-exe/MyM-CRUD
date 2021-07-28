using MaterialDesignThemes.Wpf;
using MyM_CRUD.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Vehicle>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageVehicles.xaml
    /// </summary>
    public partial class PageVehicles : Page, ICrudPage<Vehicle>
    {
        private List<Vehicle> vehicles;
        public State CurrentState { get; set; }


        public PageVehicles()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Vehicle> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += page.Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            vehicles = Vehicle.SearchVehicles("");
            Datagrid.ItemsSource = vehicles;
        }


        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            vehicles = Vehicle.SearchVehicles(TxtSearch.Text);
            Datagrid.ItemsSource = vehicles;
        }
        public void Datagrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            Vehicle vehicle = (Vehicle)Datagrid.SelectedItem;
            LoadFields(vehicle);
            SetReading();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtId.Focus();
        }


        public void LoadFields(Vehicle selected)
        {
            if(selected == null)
            {
                TxtId.Text = "";
                TxtOilType.Text = "";
                TxtDateAcquired.SelectedDate = null;
                TxtMechanicName.Text = "";
                TxtMechanicPhone.Text = "";
                TxtModelName.Text = "";
                TxtOwnerId.Text = "";
                return;
            }

            TxtId.Text = selected.Id;
            TxtOilType.Text = selected.OilType;
            TxtDateAcquired.SelectedDate = selected.DateAcquired;
            TxtMechanicName.Text = selected.MechanicName;
            TxtMechanicPhone.Text = selected.MechanicPhone;
            TxtModelName.Text = selected.ModelName;
            TxtOwnerId.Text = selected.OwnerId;
        }
        public Vehicle GetObjectsFromFields() => new Vehicle
        {
            Id = TxtId.Text,
            OilType = TxtOilType.Text,
            DateAcquired = (DateTime)TxtDateAcquired.SelectedDate,
            MechanicName = TxtMechanicName.Text,
            MechanicPhone = TxtMechanicPhone.Text,
            ModelName = TxtModelName.Text,
            OwnerId = TxtOwnerId.Text,
        };


        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtId.IsEnabled = true;
            TxtOilType.IsEnabled = true;
            TxtDateAcquired.IsEnabled = true;
            TxtMechanicName.IsEnabled = true;
            TxtMechanicPhone.IsEnabled = true;
            TxtModelName.IsEnabled = true;
            TxtOwnerId.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtId.IsEnabled = false;
            TxtOilType.IsEnabled = false;
            TxtDateAcquired.IsEnabled = false;
            TxtMechanicName.IsEnabled = false;
            TxtMechanicPhone.IsEnabled = false;
            TxtModelName.IsEnabled = false;
            TxtOwnerId.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtId.IsEnabled = true;
            TxtOilType.IsEnabled = true;
            TxtDateAcquired.IsEnabled = true;
            TxtMechanicName.IsEnabled = true;
            TxtMechanicPhone.IsEnabled = true;
            TxtModelName.IsEnabled = true;
            TxtOwnerId.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Vehicle selected = (Vehicle)Datagrid.SelectedItem;

            if (selected == null) return;

            LoadFields(selected);
            SetUpdating();
        }
    }
}
