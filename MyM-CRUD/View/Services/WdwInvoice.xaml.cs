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
    /// Lógica de interacción para WdwInvoice.xaml
    /// </summary>
    public partial class WdwInvoice : Window
    {
        public bool Canceled { get; set; }
        private Invoice invoice;

        public WdwInvoice(Invoice invoice)
        {
            InitializeComponent();
            Canceled = true;
            this.invoice = invoice;

            DgDetails.ItemsSource = invoice.Details;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Canceled = false;
            Close();
        }

        private void BtnFinClient_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
