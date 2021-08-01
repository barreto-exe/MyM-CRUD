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
                TxtProductPrice.Text = $"{ProductQuantity} * ${ProductPrice}";
            }
        }
        public decimal ProductPrice
        {
            get { return ProductPrice; }
            set 
            { 
                ProductPrice = value; 
                TxtProductPrice.Text = $"{ProductQuantity} * ${ProductPrice}";
            }
        }

        public WdwOrders(ServiceOrder order)
        {
            InitializeComponent();
            Canceled = true;

            Order = order;

            TxtServiceName.Text = Order.AsociatedActivity.Servicio;
            TxtActivityName.Text = Order.AsociatedActivity.Description;
            ManPowerCost = 0;
            ProductQuantity = 0;
            ProductPrice = 0;
            TxtOrderNumber.Text = Order.OrderNumber;
        }

        private void FillObjectFromFields()
        {
            Order.EmployeeId = TxtEmployeeId.Text;
            Order.ProductId = TxtProductId.Text;
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
    }
}
