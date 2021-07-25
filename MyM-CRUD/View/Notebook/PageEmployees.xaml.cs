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
using static MyM_CRUD.View.ICrudPage;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageWorkers.xaml
    /// </summary>
    public partial class PageEmployees : Page, ICrudPage
    {
        private IEnumerable<Employee> employees;

        public State CurrentState { get; set; }

        public PageEmployees()
        {
            InitializeComponent();
            SetReading();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            employees = Employee.SearchEmployees("");
            DgEmployees.ItemsSource = employees;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            employees = Employee.SearchEmployees(TxtSearch.Text);
            DgEmployees.ItemsSource = employees;
        }

        private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedE = (Employee)DgEmployees.SelectedItem;
            LoadFields(selectedE);
            SetReading();
        }

        private void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            switch(CurrentState)
            {
                //Click en editar
                case State.Reading:
                    {
                        BeginUpdating();
                        break;
                    }

                //Click en guardar
                case State.Creating:
                case State.Updating:
                    {
                        SetReading();
                        try
                        {
                            Employee employee = GetEmployeeFromFields();
                            if(CurrentState == State.Creating)
                            {
                                //employee.InsertTupleDatabase();
                            }
                            if(CurrentState == State.Updating)
                            {
                                //employee.UpdateTupleDataBase();
                            }
                        }
                        catch (Exception) { }
                        break;
                    }
            }

            void BeginUpdating()
            {
                Employee selectedE = (Employee)DgEmployees.SelectedItem;

                if (selectedE == null) return;

                LoadFields(selectedE);
                SetUpdating();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtId.Focus();
        }

        private void LoadFields(Employee selectedE)
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
        private Employee GetEmployeeFromFields() => new Employee
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
    }
}
