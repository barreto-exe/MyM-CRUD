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
using static MyM_CRUD.Model.Session;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            App.Session.User = TxtUser.Text.Trim();
            App.Session.PassMd5 = TxtPass.Password.Trim();

            VerifyUserEnum v = App.Session.VerifyUser(out string message);
            if(v == VerifyUserEnum.Success)
            {
                var main = new WindowMain();
                main.ShowDialog();

                Close();
            }
        }
    }
}
