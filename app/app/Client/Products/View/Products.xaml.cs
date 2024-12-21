using app.app.Admin.Users.ViewModel;
using app.app.Client.Products.ViewModel;
using app.app.Login.ViewModel;
using app.db.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace app.app.Client.Products.View
{
    public partial class Products : UserControl
    {
        public Products()
        {
            InitializeComponent();
        }

        private void OpenItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;
              
                    if (DataContext is ProductsVM viewModel)
                    {
                        viewModel.Obj = data;
                        if (viewModel.OpenItemCommand.CanExecute(null))
                            viewModel.OpenItemCommand.Execute(null);
                    }
                

            }
        }

        private void AddOrRemove(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;

                    if (DataContext is ProductsVM viewModel)
                    {
                        viewModel.Obj = data;
                        if (viewModel.AddOrRemoveCommand.CanExecute(null))
                            viewModel.AddOrRemoveCommand.Execute(null);
                    }
            }
        }
    }
}
