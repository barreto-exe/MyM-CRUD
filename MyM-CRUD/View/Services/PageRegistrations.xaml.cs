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
using System.Linq;
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Registration>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageRegistrations.xaml
    /// </summary>
    public partial class PageRegistrations : Page, ICrudPage<Registration>
    {
        private List<Registration> registrations;
        private int timesMessageShowed;
        public State CurrentState { get; set; }
        public Window Owner { get; set; }


        public PageRegistrations()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Registration> page = this;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += page.Datagrid_SelectionChangedAsync;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            registrations = Registration.GetRegistrationsFromBD();
            Datagrid.ItemsSource = registrations;

            timesMessageShowed = 0;
        }


        public void Datagrid_SelectionChangedAsync(object sender, GridSelectionChangedEventArgs e)
        {
            if (Datagrid.SelectedItem == null) return;
            Registration registration = (Registration)Datagrid.SelectedItem;
            LoadFields(registration);
            SetReading();
        }
        public void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            switch (CurrentState)
            {
                //Click en editar
                case State.Reading:
                    {
                        BeginUpdating();
                        break;
                    }

                //Click en guardar
                case State.Creating:
                    {
                        var invoice = new Invoice();

                        if(FillRegister(invoice))
                        {
                            FillInvoice(invoice);
                            SetReading();
                            TxtSearch_TextChanged(null, null);
                        }

                        break;
                    }
            }
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = null;

#if DEBUG
            reg = new Registration()
            {
                In = DateTime.Now,
                EstimatedOut = DateTime.Now.AddHours(1),
                AuthPersonId = "27506801",
                VehicleId = "AA386AK",
            };
#endif

            LoadFields(reg);
            SetCreating();
            TxtNumber.Focus();
        }
        private void Page_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timesMessageShowed >= 1) return;

            DrawerHost.IsBottomDrawerOpen = true;

            timesMessageShowed++;
        }


        private bool FillRegister(Invoice invoice)
        {
            var registration = GetObjectsFromFields();

            //Seleccionar actividades deseadas
            var w = new WdwActivities
            {
                Owner = Owner,
            };
            w.ShowDialog();

            //Salir si cancela
            if (w.Canceled) return false;

            //Llenando info de cada orden de servicio
            var orders = new List<ServiceOrder>();
            foreach (var act in w.SelectedActivities)
            {
                //Asociar id de ficha de registro a la orden
                var order = new ServiceOrder(act);
                order.RegistrationNumber = registration.Number;

                //Obtener info de las órdenes
                var o = new WdwOrders(order)
                {
                    Owner = Owner,
                };
                o.ShowDialog();


                //Agregar a las órdenes
                orders.Add(o.Order);

                //Salir si cancela
                if (o.Canceled) return false;
            }

            //Insertar datos en BD
            registration.InsertTupleDatabase();
            foreach (var order in orders)
            {
                order.InsertTupleDatabase();
            }

            invoice.Details = orders;
            return true;
        }
        private void FillInvoice(Invoice invoice)
        {
            invoice.Payments = new List<Payment>();
            var windowInvoice = new WdwInvoice(invoice);
            windowInvoice.ShowDialog();
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
            TxtVehicleId.Text = selected.VehicleId;
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
            BtnFindPerson.IsEnabled = true;
            BtnFindVehicle.IsEnabled = true;

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
            BtnFindPerson.IsEnabled = false;
            BtnFindVehicle.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Collapsed;
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
            BtnFindPerson.IsEnabled = true;
            BtnFindVehicle.IsEnabled = true;

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

        private void BtnFindPerson_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Client.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                TxtAuthPersonId.Text = ((Client)objSel.Selected).Id;
            }
        }
        private void BtnFindVehicle_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Vehicle.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                TxtVehicleId.Text = ((Vehicle)objSel.Selected).Id;
            }
        }
    }
}
