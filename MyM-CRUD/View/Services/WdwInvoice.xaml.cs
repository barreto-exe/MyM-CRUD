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
            invoice.Date = DateTime.Now;

            //Buscar franquicia
            var branch = new Branch();
            branch.SelectFromDatabase(new[] { App.Session.Branch });

            TxtInvoiceNumber.Text = invoice.Number;
            TxtBranch.Text = branch.Name + ", " + branch.Address;
            TxtDateTime.Text = invoice.Date.ToString("dd/MM/yyyy hh:mm tt");
            TxtVehicle.Text = invoice.AssociatedRegistration.VehicleDescription;
            TxtTotalAmount.Text = $"${invoice.TotalAmount}";
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

        private void BtnFindClient_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Client.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                var client = (Client)objSel.Selected;
                invoice.ClientId = client.Id;
                TxtClientId.Text = client.Id;
                TxtClientName.Text = client.Name;
            }
        }

        private void BtnAddPayment_Click(object sender, RoutedEventArgs e)
        {
            var pay = new WdwPayment();
            pay.Owner = this;
            pay.ShowDialog();

            if (pay.Canceled) return;

            pay.Payment.Number = invoice.Payments.Count + 1;
            invoice.Payments.Add(pay.Payment);
            DgPaymets.ItemsSource = null;
            DgPaymets.ItemsSource = invoice.Payments;

            TxtTotalChange.Text = "$" + invoice.PendingChange;

            BtnAccept.IsEnabled = ((List<Payment>)DgPaymets.ItemsSource).Count > 0;
        }

        private void DgPaymets_RecordDeleted(object sender, Syncfusion.UI.Xaml.Grid.RecordDeletedEventArgs e)
        {
            invoice.Payments = (List<Payment>)DgPaymets.ItemsSource;
            TxtTotalChange.Text = "$" + invoice.PendingChange;

            DgPaymets.ItemsSource = null;
            DgPaymets.ItemsSource = invoice.Payments;
        }
    }
}
