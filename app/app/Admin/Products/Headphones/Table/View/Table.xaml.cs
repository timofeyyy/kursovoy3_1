using app.app.Admin.Products.Headphones.Table.ViewModel;
using app.app.Admin.Users.ViewModel;
using app.app.Main.ViewModel;
using app.db.Entities;
using app.db.Entities.Phone;
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

namespace app.app.Admin.Products.Headphones.Table.View
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
        }

        private void Remove(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;
                if (data is db.Entities.Headphones.Headphones headphones)
                {
                    MessageBox.Show(headphones.Id.ToString());

                    if (DataContext is TableVM viewModel)
                    {
                        viewModel.Headphones = headphones;
                        if (viewModel.RemoveCommand.CanExecute(null))
                            viewModel.RemoveCommand.Execute(null);
                    }
                }

            }
        }
        private void Update(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var data = button.DataContext;
                if (data is db.Entities.Headphones.Headphones headphones)
                {
                    if (DataContext is TableVM viewModel)
                    {
                        viewModel.Headphones = headphones;
                        if (viewModel.EditCommand.CanExecute(null))
                            viewModel.EditCommand.Execute(null);
                    }
                }

            }
        }
    }
}
