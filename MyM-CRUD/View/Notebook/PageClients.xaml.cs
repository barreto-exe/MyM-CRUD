using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Client>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageClients.xaml
    /// </summary>
    public partial class PageClients : ICrudPage<Client>
    {
        private List<Client> clients;
        public State CurrentState { get; set; }

        public PageClients()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Client> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += page.Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            clients = Client.SearchClients("");
            Datagrid.ItemsSource = clients;
        }

        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            clients = Client.SearchClients(TxtSearch.Text);
            Datagrid.ItemsSource = clients;
        }
        public void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = (Client)Datagrid.SelectedItem;
            LoadFields(client);
            SetReading();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtId.Focus();
        }


        public void LoadFields(Client selected)
        {
            if(selected == null)
            {
                TxtId.Text = "";
                TxtName.Text = "";
                TxtPhone.Text = "";
                TxtPhoneAlt.Text = "";
                TxtEmail.Text = "";

                return;
            }

            TxtId.Text = selected.Id;
            TxtName.Text = selected.Name;
            TxtPhone.Text = selected.Phone;
            TxtPhoneAlt.Text = selected.PhoneAlt;
            TxtEmail.Text = selected.Email;
        }
        public Client GetObjectsFromFields() => new Client
        {
            Id = TxtId.Text,
            Name = TxtName.Text,
            Phone = TxtPhone.Text,
            PhoneAlt = TxtPhoneAlt.Text,
            Email = TxtEmail.Text,
        };


        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtId.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPhone.IsEnabled = true;
            TxtPhoneAlt.IsEnabled = true;
            TxtEmail.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtId.IsEnabled = false;
            TxtName.IsEnabled = false;
            TxtPhone.IsEnabled = false;
            TxtPhoneAlt.IsEnabled = false;
            TxtEmail.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtId.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPhone.IsEnabled = true;
            TxtPhoneAlt.IsEnabled = true;
            TxtEmail.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Client selected = (Client)Datagrid.SelectedItem;

            if (selected == null) return;

            LoadFields(selected);
            SetUpdating();
        }
    }
}
