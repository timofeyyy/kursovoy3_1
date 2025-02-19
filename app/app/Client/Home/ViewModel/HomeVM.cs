using app.app.Admin.Navigation;
using app.app.Diag.ViewModel;
using app.db.Context;
using app.utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace app.app.Client.Home.ViewModel
{
    public class HomeVM : ViewBase
    {
        private readonly ContentNavigation _contentNavigation;
        private readonly DiagVM _diagVM;
        private readonly Context _context;
        private readonly Session.Session _session;
        public ICommand CatalogLaptopCommand { get; }
        public ICommand CatalogPhoneCommand { get; }
        public ICommand CatalogHeadphonesCommand { get; }
        public ICommand CatalogSmartWacthesCommand { get; }
        public HomeVM(ContentNavigation contentNavigation, DiagVM diag, Session.Session session)
        {
            _context = new Context();
            _contentNavigation = contentNavigation;
            _diagVM = diag;
            _session = session;
            CatalogLaptopCommand = new RelayCommand(OpenLaptopCatalog);
            CatalogPhoneCommand = new RelayCommand(OpenPhoneCatalog);
            CatalogHeadphonesCommand = new RelayCommand(OpenHeadphonesCatalog);
            CatalogSmartWacthesCommand = new RelayCommand(OpenSmartWacthesCatalog);
        }

        private void OpenLaptopCatalog(object value)
        {
            _contentNavigation.CurrentView = new Products.ViewModel.ProductsVM(_session, _diagVM, _contentNavigation, _context.Laptop.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList(), "Laptops");
        }
        
        private void OpenPhoneCatalog(object value)
        {
            _contentNavigation.CurrentView = new Products.ViewModel.ProductsVM(_session, _diagVM, _contentNavigation, _context.Phone.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList(), "Phones");
        }
        
        private void OpenHeadphonesCatalog(object value)
        {
            _contentNavigation.CurrentView = new Products.ViewModel.ProductsVM(_session, _diagVM, _contentNavigation, _context.Headphones.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList(), "Headphones");
        }
        
        private void OpenSmartWacthesCatalog(object value)
        {
            _contentNavigation.CurrentView = new Products.ViewModel.ProductsVM(_session, _diagVM, _contentNavigation, _context.SmartWatches.Include(l => l.Color).Include(l => l.Producer).Include(l => l.ProductImages).ToList(), "SmartWatches");
        }
        
    }
}
