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

#if DEBUG
            App.Session.Branch = "J1234";
            App.Session.PassMd5 = Tools.Tools.CreateMD5("1234");

            Login();
#endif
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            App.Session.Branch = TxtUser.Text.Trim();
            App.Session.PassMd5 = Tools.Tools.CreateMD5(TxtPass.Password.Trim());

            Login();
        }

        private void Login()
        {
            VerifyUserEnum v = App.Session.VerifyUser(out string message);
            if (v == VerifyUserEnum.Success)
            {
                //Cargar ventana principal
                var main = new WindowMain();

                //Buscar franquicia
                var branch = new Branch();
                branch.SelectFromDatabase(new[] { App.Session.Branch });

                //Asignar franquicia
                main.Branch = branch;

                //Cerrar login
                Close();

                //Mostrar ventana principal
                main.ShowDialog();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
