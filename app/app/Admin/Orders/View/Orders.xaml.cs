using app.app.Admin.Orders.ViewModel;
using app.app.Admin.Reviews.ViewModel;
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

namespace app.app.Admin.Orders.View
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
        }
   
        private void ViewStatus(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;
                if (data is db.Entities.Orders order)
                {
                    if (DataContext is OrdersVM viewModel)
                    {
                        viewModel.Order = order;
                        if (viewModel.OpenOrderStatusCommand.CanExecute(null))
                            viewModel.OpenOrderStatusCommand.Execute(null);
                    }
                }

            }
        }
    }
}
