using MyM_CRUD.DataBase;
using MyM_CRUD.Model;
using MyM_CRUD.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyM_CRUD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Session Session { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ReconnectServers();

            Session = new Session();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //Cerrar todas las conexiones abiertas
            if (PostgreOp.ConexionGlobal != null)
                PostgreOp.ConexionGlobal.CloseAsync();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            string message =
                "Hubo un error en el programa: \n";

            string error =
                $"{e.Exception.Message} \n" +
                $"HRESULT: {e.Exception.HResult} \n";

            //MessageBox.Show(message, "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Error);
            Log.Add("Error no controlado: " + error);
        }

        #region Métodos
        public static void ReconnectServers()
        {
            PostgreOp.ConexionGlobal = new PostgreOp().NuevaConexion();
        }
        #endregion
    }
}
