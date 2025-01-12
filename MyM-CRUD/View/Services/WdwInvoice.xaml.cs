﻿using MyM_CRUD.Model;
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

            //Inicializar factura
            this.invoice = invoice;
            this.invoice.Date = DateTime.Now;

            FillData();
        }

        public WdwInvoice(Registration registration)
        {
            InitializeComponent();
            Canceled = true;

            BtnFinClient.Visibility = Visibility.Collapsed;
            BtnAddPayment.Visibility = Visibility.Collapsed;
            BtnAccept.IsEnabled = true;
            DgPaymets.AllowDeleting = false;

            invoice = new Invoice();
            invoice.AssociatedRegistration = registration;
            invoice.BuildByAssociatedRegistration();

            FillData();
        }

        private void FillData()
        {
            //Buscar franquicia
            var branch = new Branch();
            branch.SelectFromDatabase(new[] { App.Session.Branch });

            //Llenar campos de texto
            TxtInvoiceNumber.Text = invoice.Number;
            TxtBranch.Text = branch.Name + ", " + branch.Address;
            TxtDateTime.Text = invoice.Date.ToString("dd/MM/yyyy hh:mm tt");
            TxtVehicle.Text = invoice.AssociatedRegistration.VehicleId;
            if(invoice.AssociatedRegistration.VehicleDescription != null &&
                    invoice.AssociatedRegistration.VehicleDescription != "")
            {
                TxtVehicle.Text += " - " + invoice.AssociatedRegistration.VehicleDescription;
            }
            TxtClientId.Text = invoice.ClientId;
            TxtClientName.Text = invoice.ClientName;
            TxtTotalAmount.Text = $"${invoice.TotalAmount}";
            TxtTotalChange.Text = $"${invoice.PendingChange}";

            DgDetails.ItemsSource = null;
            DgDetails.ItemsSource = invoice.Details;
            DgPaymets.ItemsSource = null;
            DgPaymets.ItemsSource = invoice.Payments;

            (TxtPhrase.Text, TxtPhraseAuth.Text) = Tools.Tools.GetRandomPhraseAndAuth();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool cantClose =
                invoice.ClientId == null ||
                invoice.TotalAmount - invoice.TotalPayed > 0;
            if(cantClose)
            {
                MessageBox.Show("Debe seleccionar un cliente y pagar la factura por completo.");
                e.Cancel = true;
            }
        }
    }
}
