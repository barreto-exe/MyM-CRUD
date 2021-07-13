using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        private PageExample page;

        public WindowMain()
        {
            InitializeComponent();

            page = new PageExample();

            DrawerList.SelectedIndex = 0;
            HamburguerButton.IsChecked = true;
        }

        private void HamburguerButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility textOnItems;
            if ((bool)HamburguerButton.IsChecked)
            {
                DrawerCol.Width = 200;
                textOnItems = Visibility.Visible;
                TxtBranch.Visibility = Visibility.Collapsed;
                TxtManager.Visibility = Visibility.Collapsed;
            }
            else
            {
                DrawerCol.Width = 70;
                textOnItems = Visibility.Hidden;
                TxtBranch.Visibility = Visibility.Visible;
                TxtManager.Visibility = Visibility.Visible;
            }

            //Setting visibility just for icons (collapsed drawer)
            foreach (StackPanel item in DrawerList.Items)
            {
                //Hidding or showing text label
                item.Children[1].Visibility = textOnItems;
            }
        }

        private void DrawerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (DrawerList.SelectedIndex)
            {
                case 0:
                    {
                        MainFrame.Content = page;
                        break;
                    }
                case 1:
                    {
                        MainFrame.Content = page;
                        break;
                    }
                case 2:
                    {
                        MainFrame.Content = page;
                        break;
                    }
                case 3:
                    {
                        MainFrame.Content = page;
                        break;
                    }
            }

        }
    }
}
