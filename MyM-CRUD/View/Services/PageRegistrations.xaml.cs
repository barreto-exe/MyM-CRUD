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
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Registration>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageRegistrations.xaml
    /// </summary>
    public partial class PageRegistrations : Page, ICrudPage<Registration>
    {
        private List<Registration> registrations;
        public State CurrentState { get; set; }
        private int timesMessageShowed;

        public PageRegistrations()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Registration> page = this;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += page.Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            registrations = Registration.GetRegistrationsFromBD();
            Datagrid.ItemsSource = registrations;

            timesMessageShowed = 0;
        }

        public void Datagrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            if (Datagrid.SelectedItem == null) return;
            Registration registration = (Registration)Datagrid.SelectedItem;
            LoadFields(registration);
            SetReading();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtNumber.Focus();
        }
        private void Page_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timesMessageShowed >= 2) return;

            DrawerHost.IsBottomDrawerOpen = true;

            timesMessageShowed++;
        }


        public void LoadFields(Registration selected)
        {
            if(selected == null)
            {
                TxtNumber.Text = "";
                TxtDateIn.SelectedDate = null;
                TxtHourIn.SelectedTime = null;
                TxtDateOut.SelectedDate = null;
                TxtHourOut.SelectedTime = null;
                TxtAuthPersonId.Text = "";
                TxtVehicleId.Text = "";
                return;
            }

            TxtNumber.Text = selected.Number;
            TxtDateIn.SelectedDate = selected.In;
            TxtHourIn.SelectedTime = selected.In;
            TxtDateOut.SelectedDate = selected.EstimatedOut;
            TxtHourOut.SelectedTime = selected.EstimatedOut;
            TxtAuthPersonId.Text = selected.AuthPersonId;
            TxtVehicleId.Text = selected.AuthPersonId;
        }
        public Registration GetObjectsFromFields() => new Registration
        {
            Number = TxtNumber.Text,
            In = 
                Convert.ToDateTime
                (
                    TxtDateIn.SelectedDate.Value.ToString("dd-MM-yyyy") + " " + 
                    TxtHourIn.SelectedTime.Value.ToString("HH:mm:ss")
                ),
            EstimatedOut =
                Convert.ToDateTime
                (
                    TxtDateOut.SelectedDate.Value.ToString("dd-MM-yyyy") + " " +
                    TxtHourOut.SelectedTime.Value.ToString("HH:mm:ss")
                ),
            RealOut = null,
            AuthPersonId = TxtAuthPersonId.Text,
            VehicleId = TxtVehicleId.Text,
        };


        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtNumber.IsEnabled = true;
            TxtDateIn.IsEnabled = true;
            TxtHourIn.IsEnabled = true;
            TxtDateOut.IsEnabled = true;
            TxtHourOut.IsEnabled = true;
            TxtAuthPersonId.IsEnabled = true;
            TxtVehicleId.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtNumber.IsEnabled = false;
            TxtDateIn.IsEnabled = false;
            TxtHourIn.IsEnabled = false;
            TxtDateOut.IsEnabled = false;
            TxtHourOut.IsEnabled = false;
            TxtAuthPersonId.IsEnabled = false;
            TxtVehicleId.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtNumber.IsEnabled = true;
            TxtDateIn.IsEnabled = true;
            TxtHourIn.IsEnabled = true;
            TxtDateOut.IsEnabled = true;
            TxtHourOut.IsEnabled = true;
            TxtAuthPersonId.IsEnabled = true;
            TxtVehicleId.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Registration selected = (Registration)Datagrid.SelectedItem;

            if (selected == null) return;

            LoadFields(selected);
            SetUpdating();
        }


        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            registrations = Registration.GetRegistrationsFromBD();
            Datagrid.ItemsSource = registrations;
        }
    }
}
