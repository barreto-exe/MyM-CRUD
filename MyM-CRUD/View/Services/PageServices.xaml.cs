﻿using MaterialDesignThemes.Wpf;
using MyM_CRUD.Model;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        private List<ServiceActivity> activities;

        private State state;
        private State pastState;
        public State CurrentState
        {
            get => state;
            set 
            {
                pastState = state;
                state = value; 
            }
        }

        public PageServices()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Service> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            Datagrid.SelectionChanged += Datagrid_SelectionChangedAsync;
            BtnAdd.Click += page.BtnAdd_Click;
            BtnEditSave.Click += page.BtnEditSave_Click;
            BtnEditSave.Click += BtnEditSavePlus_Click;

            //Inicializar datagrid
            services = Service.SearchServices("");
            Datagrid.ItemsSource = services;
        }
        
        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            services = Service.SearchServices(TxtSearch.Text);
            Datagrid.ItemsSource = services;
        }
        public async void Datagrid_SelectionChangedAsync(object sender, GridSelectionChangedEventArgs e)
        {
            if (Datagrid.SelectedItem == null) return;

            Service service = (Service)Datagrid.SelectedItem;
            LoadFields(service);
            SetReading();

            //Buscar actividades asíncronamente
            await Task.Run(() => 
            {
                activities = ServiceActivity.SearchActivities(service.Code);
            });
            DgActivities.ItemsSource = activities;
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtCode.Focus();

            activities = new List<ServiceActivity>();
            DgActivities.ItemsSource = activities;
        }
        public void BtnEditSavePlus_Click(object sender, RoutedEventArgs e)
        {
            if(pastState == State.Creating)
            {
                foreach (var activity in activities)
                {
                    activity.ServiceCode = TxtCode.Text;
                    activity.InsertTupleDatabase();
                }
            }
            if (pastState == State.Updating)
            {
                foreach (var activity in activities)
                {
                    activity.ServiceCode = TxtCode.Text;
                    try
                    {
                        activity.InsertTupleDatabase();
                    }
                    catch
                    {
                        activity.UpdateTupleDataBase();
                    }
                }
            }
        }
        private void DgActividades_AddNewRowInitiating(object sender, AddNewRowInitiatingEventArgs e)
        {
            var item = e.NewObject as ServiceActivity;
            item.Number = activities.Count + 1;
        }
        private void DgActividades_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
        {
            //var item = e. as ServiceActivity;
            //var currentActivies = ServiceActivity.SearchActivities()
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
            if (TxtReserveTime == null) return;

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
            DgActivities.IsEnabled = true;

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
            DgActivities.IsEnabled = false;

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
            DgActivities.IsEnabled = true;

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


        private void DgActivities_RowValidating(object sender, RowValidatingEventArgs e)
        {
            if (DgActivities.IsAddNewIndex(e.RowIndex))
            {
                var data = e.RowData as ServiceActivity;
                int mustBe = activities.Count + 1;

                if (data.Number != mustBe)
                {
                    e.IsValid = false;
                    e.ErrorMessages.Add("Número", "Número debe ser: " + mustBe);
                }
            }
        }
    }
}
