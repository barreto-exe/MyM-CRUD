using MaterialDesignThemes.Wpf;
using MyM_CRUD.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MyM_CRUD.Model.Product;
using static MyM_CRUD.View.ICrudPage<MyM_CRUD.Model.Product>;

namespace MyM_CRUD.View
{
    /// <summary>
    /// Lógica de interacción para PageProducts.xaml
    /// </summary>
    public partial class PageProducts : Page, ICrudPage<Product>
    {
        private List<Product> products;
        public State CurrentState { get; set; }

        public PageProducts()
        {
            InitializeComponent();
            SetReading();

            //Eventos
            ICrudPage<Product> page = this;
            TxtSearch.TextChanged += page.TxtSearch_TextChanged;
            BtnEditSave.Click += page.BtnEditSave_Click;
            Datagrid.SelectionChanged += Datagrid_SelectionChanged;
            BtnAdd.Click += page.BtnAdd_Click;

            //Inicializar datagrid
            products = Product.SearchProducts("");
            Datagrid.ItemsSource = products;
        }

        public void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            products = Product.SearchProducts(TxtSearch.Text);
            Datagrid.ItemsSource = products;
        }
        public void Datagrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            Product product = (Product)Datagrid.SelectedItem;
            LoadFields(product);
            SetReading();
        }

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LoadFields(null);
            SetCreating();
            TxtCode.Focus();
        }


        public Product GetObjectsFromFields() => new Product()
        {
            Code = TxtCode.Text,
            Name = TxtName.Text,
            Price = Convert.ToDecimal(TxtPrice.Text),
            Description = TxtDescription.Text,
            ManufacturerCode = TxtManufacturerCode.Text,
            Type = CbTypeProduct.SelectedIndex == 0 ? ProductType.ForService : ProductType.ForSell,
            IsEcologic = (bool)CheckIsEcologic.IsChecked,
            LineCode = TxtLineCode.Text,
        };

        public void LoadFields(Product selected)
        {
            if(selected == null)
            {
                TxtCode.Text = "";
                TxtName.Text = "";
                TxtPrice.Text = "";
                TxtDescription.Text = "";
                TxtManufacturerCode.Text = "";
                CbTypeProduct.SelectedIndex = -1;
                CheckIsEcologic.IsChecked = false;
                TxtLineCode.Text = "";

                return;
            }

            TxtCode.Text = selected.Code;
            TxtName.Text = selected.Name;
            TxtPrice.Text = selected.Price.ToString();
            TxtDescription.Text = selected.Description;
            TxtManufacturerCode.Text = selected.ManufacturerCode;
            switch(selected.Type)
            {
                case ProductType.ForSell:
                    {
                        CbTypeProduct.SelectedIndex = 1;
                        break;
                    }
                case ProductType.ForService:
                    {
                        CbTypeProduct.SelectedIndex = 0;
                        CheckIsEcologic.IsChecked = selected.IsEcologic;
                        TxtLineCode.Text = selected.LineCode;
                        break;
                    }
            }
        }
        private void CbTypeProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(CbTypeProduct.SelectedIndex)
            {
                case 0:
                    StackEcologic.Visibility = Visibility.Visible;
                    break;
                case 1:
                    CheckIsEcologic.IsChecked = false;
                    StackEcologic.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void SetCreating()
        {
            CurrentState = State.Creating;

            TxtCode.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPrice.IsEnabled = true;
            TxtDescription.IsEnabled = true;
            TxtManufacturerCode.IsEnabled = true;
            CheckIsEcologic.IsEnabled = true;
            CbTypeProduct.IsEnabled = true;
            TxtLineCode.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void SetReading()
        {
            CurrentState = State.Reading;

            TxtCode.IsEnabled = false;
            TxtName.IsEnabled = false;
            TxtPrice.IsEnabled = false;
            TxtDescription.IsEnabled = false;
            TxtManufacturerCode.IsEnabled = false;
            CheckIsEcologic.IsEnabled = false;
            CbTypeProduct.IsEnabled = false;
            TxtLineCode.IsEnabled = false;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.Edit;
        }
        public void SetUpdating()
        {
            CurrentState = State.Updating;

            TxtCode.IsEnabled = true;
            TxtName.IsEnabled = true;
            TxtPrice.IsEnabled = true;
            TxtDescription.IsEnabled = true;
            TxtManufacturerCode.IsEnabled = true;
            CheckIsEcologic.IsEnabled = true;
            CbTypeProduct.IsEnabled = true;
            TxtLineCode.IsEnabled = true;

            BtnEditSave.Visibility = Visibility.Visible;
            IconEdit.Kind = PackIconKind.ContentSave;
        }
        public void BeginUpdating()
        {
            Product selected = (Product)Datagrid.SelectedItem;

            if (selected == null) return;

            LoadFields(selected);
            SetUpdating();
        }
    }
}
