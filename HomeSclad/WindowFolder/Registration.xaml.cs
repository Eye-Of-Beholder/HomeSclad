using Pennant.ClassFolder;
using Pennant.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Authorization().Show();
            Close();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string username = ButLog.Text;
            string password = ButPas.Password;
            string confirmPassword = DubPas.Password;

            if (string.IsNullOrWhiteSpace(ButLog.Text))
            {
                MBClass.ErrorMB("Введите логин");
                ButPas.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(ButPas.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                ButPas.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(DubPas.Password))
            {
                MBClass.ErrorMB("Введите повторно пароль");
                ButPas.Focus();
                return;
            }
            else if(password.Length > 5)
            {
                MBClass.ErrorMB("Пароль должен сожержать " +
                    "минимум 6 символов");
                return;
            }
            else if(password != ButPas.Password)
            {
                MBClass.ErrorMB("Пароль не совпадают");
                ButPas.Focus();
                return;
            }
            else
            {
                try
                {
                    DBEntities.getContex().Users.Add(new Users()
                    {
                        Login = ButLog.Text,
                        Password = ButPas.Password,
                        RoleID = 1
                    });
                    DBEntities.getContex().SaveChanges();
                    MBClass.InfoMB("Вы зарегистрировались");

                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            
        }
    }
}
