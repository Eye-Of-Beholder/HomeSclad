using Pennant.ClassFolder;
using Pennant.ListFolder;
using Pennant.WindowFolder.CabInfo;
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
using System.Windows.Shapes;

namespace Pennant.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для ResponsibleWindow.xaml
    /// </summary>
    public partial class ResponsibleWindow : Window
    {
        public ResponsibleWindow()
        {
            InitializeComponent();
            OpenListUsers();
        }

        private void OpenListUsers()
        {
            int userId = SessionInfo.UserId;
            CustInfo cust = new CustInfo(userId);
            MainFrameResponsible.Navigate(cust);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void ButWare_Click(object sender, RoutedEventArgs e)
        {
            MainFrameResponsible.Navigate(new Warehouses());
        }

        private void ExitIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Svor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CabinetBtn_Click(object sender, RoutedEventArgs e)
        {
            int userId = SessionInfo.UserId;
            MainFrameResponsible.NavigationService.Navigate(new CabInfo.CustInfo(userId));
        }

        private void Ware_Click(object sender, RoutedEventArgs e)
        {
            MainFrameResponsible.Navigate(new WarehousesRes());
        }

        private void Wz_Click(object sender, RoutedEventArgs e)
        {
            MainFrameResponsible.Navigate(new WarehousesEV());
        }
    }
}
