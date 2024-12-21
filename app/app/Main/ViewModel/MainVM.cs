using app.app.Login.ViewModel;
using app.app.Navigation;
using app.db.Init;
using app.Main.View;
using app.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace app.app.Main.ViewModel
{
    public class MainVM : ViewModelBase
    {
        private readonly PageNavigation _navigator;
        private readonly MainWindow _main;

        public ICommand CloseWindowCommand {  get; }
        public ICommand HideWindowCommand {  get; }
        public ICommand DragWindowCommand { get; }

        public object CurrentView
        {
            get { return _navigator.CurrentView; }
            set { _navigator.CurrentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }
        public MainVM(PageNavigation navigator, MainWindow main)
        {
            _main = main;
            _navigator = navigator;
            _navigator.PropertyChanged += (s, e) => OnPropertyChanged(nameof(CurrentView));
            CloseWindowCommand = new RelayCommand(OnCloseWindow);
            HideWindowCommand = new RelayCommand(OnHideWindow);
            DragWindowCommand = new RelayCommand(OnDragWindow);
            CurrentView = new LogInVM(_navigator);
            InitDatabase.Init();
        }
        private void OnCloseWindow(object value)
        {
            Window window = Window.GetWindow(_main);
                if (window != null)
                    window.Close();
        }
        private void OnHideWindow(object value)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void OnDragWindow(object value)
        {
            Window window = Window.GetWindow(_main);
            if (window != null)
                window.DragMove();
        }
    }
}
