using app.app.Admin.Users.ViewModel;
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

namespace app.app.Admin.Users.View
{
    public partial class Users : UserControl
    {
        public Users()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var hit = e.OriginalSource as FrameworkElement;
            if (hit != null)
            {
                DependencyObject parent = hit;
                while (parent != null && !(parent is DataGridRow))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                if (parent is DataGridRow row)
                {
                    int rowIndex = row.GetIndex();

                    var rowData = row.DataContext;

                    if (rowData is User user && DataContext is UsersVM viewModel)
                            viewModel.OpenProfile(user);
                }
            }
        }
    }
}
