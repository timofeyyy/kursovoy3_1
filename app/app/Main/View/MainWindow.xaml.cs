using app.app.Client.Products.View;
using app.app.Login.ViewModel;
using app.app.Main.ViewModel;
using app.app.Navigation;
using app.db.Context;
using app.db.Entities;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace app.Main.View
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {           
            
            InitializeComponent();

            var navigation = new PageNavigation();


            var mainVM = new MainVM(navigation, this);

            DataContext = mainVM;
        }

        private void CloseApp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is MainVM viewModel)
                if (viewModel.CloseWindowCommand.CanExecute(null))
                    viewModel.CloseWindowCommand.Execute(null);
        }

        private void HideApp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainVM viewModel)
                if (viewModel.HideWindowCommand.CanExecute(null))
                    viewModel.HideWindowCommand.Execute(null);
        }

        private void ControlPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MainVM viewModel)
                if (viewModel.DragWindowCommand.CanExecute(null))
                    viewModel.DragWindowCommand.Execute(null);
        }

    }
}