using MyM_CRUD.DataBase;
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
using System.Linq;
using System.Data.Common;
using System.Dynamic;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para WdwPayment.xaml
    /// </summary>
    public partial class WdwPayment : Window
    {
        private string bankName;
        public Payment Payment { get; set; }
        public bool Canceled { get; set; }
        public WdwPayment()
        {
            InitializeComponent();
            Canceled = true;

            CbPayMethod.ItemsSource = Payment.PayMethods();
            Payment = new Payment();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            Payment.Method = CbPayMethod.SelectedItem.ToString();
            Payment.Amount = Convert.ToDecimal(TxtAmout.Text);
            Payment.BankCode = TxtBankCode.Text;
            Payment.BankName = bankName;
            Payment.CardNumber = TxtCardCode.Text;

            Canceled = false;
            Close();
        }

        private void BtnFindBank_Click(object sender, RoutedEventArgs e)
        {
            var objSel = new WdwObjSelector(Bank.GetAllFromDB);
            objSel.ShowDialog();

            if (!objSel.Canceled)
            {
                Bank bank = (Bank)objSel.Selected;
                TxtBankCode.Text = bank.Code;
                bankName = bank.Name;
            }
        }
    }
}
