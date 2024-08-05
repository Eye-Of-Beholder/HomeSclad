using Pennant.AdminFolder;
using Pennant.ClassFolder;
using Pennant.DataFolder;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string log = ButLog.Text.Trim();
            string pas = ButPas.Password.Trim();

            if (log.Length < 5)
            {
                ButLog.ToolTip = "Логин должен содержать не менее 5 символов!";
                ButLog.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                ButLog.ToolTip = "";
                ButLog.Background = Brushes.Transparent;
            }
            if (pas.Length < 5)
            {
                ButPas.ToolTip = "Пароль должен содержать не менее 5 символов!";
                ButPas.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                ButPas.ToolTip = "";
                ButPas.Background = Brushes.Transparent;
            }

            try
            {

                var user = DBEntities.getContex().Users.FirstOrDefault(u => u.Login == ButLog.Text);
                if (user == null || user.Password != ButPas.Password)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                // Сохраняем userId в SessionInfo
                var customers = DBEntities.getContex().Customers.FirstOrDefault(u => u.UserID == user.UserID);
                SessionInfo.UserId = user.UserID;
                if (customers != null) 
                SessionInfo.CustomersID = customers.CustomersID;


                switch (user.RoleID)
                {
                    case 1:
                        new AdminWindow().Show();
                        break;
                    case 2:
                        new Catalog().Show();
                        break;
                    case 3:
                        new ResponsibleWindow().Show();
                        break;
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Registration().Show();
            Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            

        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }
    }
}
