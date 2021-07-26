using MaterialDesignThemes.Wpf;
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
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Employee>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageWorkers.xaml
    /// </summary>
    public partial class PageEmployees : Page, ICrudPage<Employee>
    {
        private List<Employee> employees;
        public State CurrentState { get; set; }

        public PageEmployees()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Employee> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            BtnEditSave.Click +=  page.BtnEditSave_Click;
            Datagrid.SelectionChanged += page.Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            employees = Employee.SearchEmployees("");
            Datagrid.ItemsSource = employees;
        }

        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            employees = Employee.SearchEmployees(TxtSearch.Text);
            Datagrid.ItemsSource = employees;
        }
        public void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedE = (Employee)Datagrid.SelectedItem;
            LoadFields(selectedE);
            SetReading();
        }
        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtId.Focus();
        }


        public void LoadFields(Employee selectedE)
        {
            if (selectedE == null)
            {
                TxtId.Text = "";
                TxtName.Text = "";
                TxtPhone.Text = "";
                TxtSalary.Text = "";
                TxtAddress.Text = "";
                CbTypeEmployee.SelectedIndex = -1;

                return;
            }

            TxtId.Text = selectedE.Id;
            TxtName.Text = selectedE.Name;
            TxtPhone.Text = selectedE.Phone;
            TxtSalary.Text = selectedE.Salary.ToString();
            TxtAddress.Text = selectedE.Address;
            CbTypeEmployee.SelectedIndex = selectedE.IsManager ? 1 : 0;
        }
        public Employee GetObjectsFromFields() => new Employee
        {
            Id = TxtId.Text,
            Name = TxtName.Text,
            Phone = TxtPhone.Text,
            Salary = Convert.ToDecimal(TxtSalary.Text),
            Address = TxtAddress.Text,
            IsManager = CbTypeEmployee.SelectedIndex == 1,
        };


        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtId.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPhone.IsEnabled = true;
            TxtSalary.IsEnabled = true;
            TxtAddress.IsEnabled = true;
            CbTypeEmployee.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtId.IsEnabled = false;
            TxtName.IsEnabled = false;
            TxtPhone.IsEnabled = false;
            TxtSalary.IsEnabled = false;
            TxtAddress.IsEnabled = false;
            CbTypeEmployee.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtId.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPhone.IsEnabled = true;
            TxtSalary.IsEnabled = true;
            TxtAddress.IsEnabled = true;
            CbTypeEmployee.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Employee selectedE = (Employee)Datagrid.SelectedItem;

            if (selectedE == null) return;

            LoadFields(selectedE);
            SetUpdating();
        }
    }
}
