using MyM_CRUD.DataBase;
using MyM_CRUD.Model;
using MyM_CRUD.Tools;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialLight.WPF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            SynfusionThemeSettings();

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
        private static void SynfusionThemeSettings()
        {
            MaterialLightThemeSettings settings = new MaterialLightThemeSettings();
            settings.PrimaryBackground = new SolidColorBrush(Color.FromRgb(54, 128, 57));
            settings.BodyFontSize = 14;
            settings.HeaderFontSize = 16;
            SfSkinManager.RegisterThemeSettings("MaterialLight", settings);
        }
        #endregion
    }
}
