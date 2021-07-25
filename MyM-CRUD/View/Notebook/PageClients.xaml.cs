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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MyM_CRUD.View.ICrudPage;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageClients.xaml
    /// </summary>
    public partial class PageClients : Page, ICrudPage
    {
        public State CurrentState { get; set; }

        public PageClients()
        {
            InitializeComponent();
        }


        public void SetCreating()
        {
            throw new NotImplementedException();
        }

        public void SetReading()
        {
            throw new NotImplementedException();
        }

        public void SetUpdating()
        {
            throw new NotImplementedException();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {

        }
    }
}
