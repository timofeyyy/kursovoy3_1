using app.app.Client.Products.Laptop.ViewModel;
using app.app.Client.Products.Phone.ViewModel;
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

namespace app.app.Client.Products.Laptop.View
{
    /// <summary>
    /// Логика взаимодействия для Fourth.xaml
    /// </summary>
    public partial class Laptop : UserControl
    {
        public Laptop()
        {
            InitializeComponent();
        }

        private void SelectStar(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;

                if (DataContext is LaptopVM viewModel)
                {
                    if (data is Star star)
                    {
                        viewModel.Star = star;
                        if (viewModel.SelectStarCommand.CanExecute(null))
                            viewModel.SelectStarCommand.Execute(null);
                    }

                }


            }
        }
    }
}
