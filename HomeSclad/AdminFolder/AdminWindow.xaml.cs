using Pennant.ClassFolder;
using Pennant.ListFolder;
using Pennant.WindowFolder;
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

namespace Pennant.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            OpenListUsers();
        }

        private void OpenListUsers()
        {
            ListUsers listUsers = new ListUsers();
            MainFrameAdmin.Navigate(listUsers);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }

        private void Svor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CustListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new ListUsers());
        }

        private void AddCust_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new AddEmployees());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void Sprav_Click(object sender, RoutedEventArgs e)
        {
            MainFrameAdmin.Navigate(new GuidePage());
        }
    }
}
