using Pennant.ClassFolder;
using Pennant.DataFolder;
using Pennant.ListFolder;
using Pennant.WindowFolder.CabInfo;
using Pennant.WindowFolder.InfoWindow;
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

namespace Pennant.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
            LoadCustomerData(SessionInfo.UserId);
            OpenListUsers();
        }

        private void OpenListUsers()
        {
            int userId = SessionInfo.UserId;
            CustInfo cust = new CustInfo(userId);
            MainFrameCatalog.Navigate(cust);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrameCatalog.Navigate(new Warehouses());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrameCatalog.Navigate(new CustomerPage());
        }

        private void ButWare_Click(object sender, RoutedEventArgs e)
        {
            MainFrameCatalog.Navigate(new AddWarehouses());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void ExitIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Svor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        Customers selectedCustemerSa;

        private void CabinetBtn_Click(object sender, RoutedEventArgs e)
        {
            int userId = SessionInfo.UserId;
           MainFrameCatalog.NavigationService.Navigate(new CabInfo.CustInfo(userId));
        }

        private void LoadCustomerData(int userId)
        {
            // Предположим, что у вас есть метод для получения данных клиента по userId
            var customerData = GetCustomerDataById(userId);
            this.DataContext = customerData;
        }

        private dynamic GetCustomerDataById(int userId)
        {
            // Замените реальной реализацией получения данных клиента
            return DBEntities.getContex().Users.Where(u => u.UserID == userId).FirstOrDefault();
        }

        private void MyEq_Click(object sender, RoutedEventArgs e)
        {
            MainFrameCatalog.Navigate(new MyEquipment());
        }
    }
}

