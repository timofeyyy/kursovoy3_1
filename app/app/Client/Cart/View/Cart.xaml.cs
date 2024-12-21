using app.app.Admin.Reviews.ViewModel;
using app.app.Client.Cart.ViewModel;
using app.app.Client.Products.Headphones.ViewModel;
using application.app.Client.Products.SmartWatches.Model;
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

namespace app.app.Client.Cart.View
{
    /// <summary>
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : UserControl
    {
        public Cart()
        {
            InitializeComponent();
        }

        private void OrderItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;

                    if (DataContext is CartVM viewModel)
                    {              

                        viewModel.Obj = data;
                        if (viewModel.OpenOrderFormCommand.CanExecute(null))
                            viewModel.OpenOrderFormCommand.Execute(null);
                    }
               
            }
        }
    }
}
