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

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageServicesTab.xaml
    /// </summary>
    public partial class PageServicesTab : Page
    {
        private Window owner;
        public Window Owner
        {
            get => owner;
            set 
            { 
                PageRegistrations.Owner = value;
                owner = value; 
            }
        }

        public PageServicesTab()
        {
            InitializeComponent();
        }
    }
}
