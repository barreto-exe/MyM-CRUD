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

            TxtServiceName.Text = Order.AsociatedActivity.Servicio;
            TxtActivityName.Text = Order.AsociatedActivity.Description;
            ManPowerCost = Order.AsociatedActivity.Cost;
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
                TxtEmployeeId.Text = ((Employee)objSel.Selected).Id;
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
