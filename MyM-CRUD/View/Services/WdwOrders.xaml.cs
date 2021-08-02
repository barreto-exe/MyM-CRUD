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
using System.Windows.Shapes;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para WdwFillOrders.xaml
    /// </summary>
    public partial class WdwOrders : Window
    {
        public ServiceOrder Order { get; set; }

        public bool Canceled { get; set; }

        //Propiedades de la orden
        private decimal manPowerCost;
        private int productQuantity;
        private decimal productPrice;
        private string employeeName;
        private string productName;

        public decimal ManPowerCost
        {
            get { return manPowerCost; }
            set 
            { 
                TxtManPowerCost.Text = "$" + value;
                manPowerCost = value; 
            }
        }
        public int ProductQuantity
        {
            get { return productQuantity; }
            set 
            { 
                productQuantity = value; 
                TxtProductPrice.Text = $"{productQuantity} * ${productPrice}";
            }
        }
        public decimal ProductPrice
        {
            get { return productPrice; }
            set 
            {
                productPrice = value; 
                TxtProductPrice.Text = $"{productQuantity} * ${productPrice}";
            }
        }

        public WdwOrders(ServiceOrder order)
        {
            InitializeComponent();
            Canceled = true;

            Order = order;

            TxtServiceName.Text = Order.AssociatedActivity.Servicio;
            TxtActivityName.Text = Order.AssociatedActivity.Description;
            ManPowerCost = Order.AssociatedActivity.Cost;
            ProductQuantity = 0;
            ProductPrice = 0;
            TxtOrderNumber.Text = Order.OrderNumber;
        }

        private void FillObjectFromFields()
        {
            Order.EmployeeId = TxtEmployeeId.Text;
            Order.ProductId = TxtProductCode.Text;
            Order.ManPowerCost = manPowerCost;
            Order.ProductQuantity = productQuantity;
            Order.ProductPrice = ProductPrice;
            Order.EmployeeName = employeeName;
            Order.ProductName = productName;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            FillObjectFromFields();
            Canceled = false;
            Close();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Employee.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                var employee = (Employee)objSel.Selected;
                TxtEmployeeId.Text = employee.Id;
                employeeName = employee.Name;
            }
        }
        private void BtnFindProduct_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Product.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                var product = (Product)objSel.Selected;

                TxtProductCode.Text = product.Code;
                ProductPrice = product.Price;
                productName = product.Name;

                //Preguntar por cantidad
                TxtProductNameDialog.Text = product.Name;
                TxtQuantityDialog.Text = "";
                DialogHost.IsOpen = true;
            }
        }

        private void BtnCancelDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
        }

        private void BtnAcceptDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            int.TryParse(TxtQuantityDialog.Text, out int q);
            ProductQuantity = q;
        }
    }
}
