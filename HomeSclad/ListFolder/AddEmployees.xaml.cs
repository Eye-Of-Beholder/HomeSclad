using Pennant.ClassFolder;
using Pennant.DataFolder;
using Microsoft.Win32;
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
using System.Text.RegularExpressions;

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для AddEmployees.xaml
    /// </summary>
    public partial class AddEmployees : Page
    {
        
        public AddEmployees()
        {
            InitializeComponent();

            Gender.ItemsSource = DBEntities.getContex().Gender.ToList();
            //Role.ItemsSource = DBEntities.getContex().Role.ToList();
            Pos.ItemsSource = DBEntities.getContex().Post.ToList();
            Numca.ItemsSource = DBEntities.getContex().NumCab.ToList();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string logi = Log.Text;
            string password = Pas.Password;
            string dpas = DubPas.Password;
            string gend = Gender.ToString();
            string post = Pos.ToString();
            string numc = Numca.ToString();
            string surna = Surn.Text;
            string nume = Nam.Text;
            string em = Email.Text;
            string phon = Phone.Text;
            string photoPath = PhotoIB.ImageSource?.ToString();


              if (string.IsNullOrEmpty(Surn.Text))
              {
                MBClass.InfoMB("Введите фамилию сотрудника");
                Surn.Focus();
                return;
              }

            else if (string.IsNullOrEmpty(Nam.Text))
            {
                MBClass.InfoMB("Введите имя сотрудника");
                Nam.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(Phone.Text))
            {
                MBClass.InfoMB("Введите телефон сотрудника");
                Phone.Focus();
                return;
            }
            else if (!Regex.IsMatch(Phone.Text, @"^\+?\d+$"))
            {
                MBClass.InfoMB("Некорректный номер телефона. Номер должен содержать только цифры и может начинаться с '+'.");
                Phone.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(Email.Text))
            {
                MBClass.InfoMB("Ввидите почту");
                Email.Focus();
                return;
            }
            else if (!Email.Text.Contains("@") || !Email.Text.Contains("."))
            {
                MBClass.InfoMB("Некорректный адрес электронной почты. Почта должна содержать '@' и '.'");
                Email.Focus();
                return;
            }

            else if (string.IsNullOrEmpty(Gender.Text))
            {
                MBClass.InfoMB("Выбирите Пол сотрудника");
                Gender.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(Pos.Text))
            {
                MBClass.InfoMB("Выбирите должность сотрудника");
                Pos.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(Numca.Text))
            {
                MBClass.InfoMB("Выбирите номекр кабинета");
                Numca.Focus();
                return;
            }

            else if (string.IsNullOrWhiteSpace(Log.Text))
            {
                MBClass.ErrorMB("Введите логин");
                Pas.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(Pas.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                Pas.Focus();
                return;
            }
            else if (string.IsNullOrWhiteSpace(DubPas.Password))
            {
                MBClass.ErrorMB("Введите повторно пароль");
                DubPas.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(selectedPhotoFile))
            {
                MessageBox.Show("Добавьте фотографию", "Внимание");
                return;
            }

         
            else if (password.Length < 5)
            {
                MBClass.ErrorMB("Пароль должен сожержать " +
                    "минимум 5 символов");
                return;
            }
            else if (password != Pas.Password)
            {
                MBClass.ErrorMB("Пароль не совпадают");
                Pas.Focus();
                return;
            }

            string log = Log.Text.Trim();
            string pas = Pas.Password.Trim();
            string sur = Surn.Text.Trim();
            string nam = Nam.Text.Trim();
            string patr = Patro.Text.Trim();
            string pho = Phone.Text.Trim();
            string ema = Email.Text.Trim();
            string pos = Pos.Text.Trim();
            string gen = Gender.Text.Trim();
            string num = Numca.Text.Trim();
            //string rol = Role.Text.Trim();

            if (string.IsNullOrWhiteSpace(Nam.Text))
            {
                MessageBox.Show("Введите имя!", "Внимание");
                return;
            }

            try
            {
              

                Customers customers = new Customers();

                if (selectedPhotoFile == "Фото есть" || selectedPhotoFile == "")
                {
                    MessageBox.Show("Добавьте фотографию", "Внимание");
                    return;
                }

                customers.Photo = ImageClass.ImageToByte(selectedPhotoFile);

                customers.NameCust = Nam.Text;
                customers.SurnameCust = Surn.Text;
                customers.PatronomycCust = Patro.Text;
                customers.PhoneCust = Phone.Text;
                customers.EmailCust = Email.Text;
                customers.GenderID = ((Gender)Gender.SelectedItem).GenderID;
                customers.PostID = ((Post)Pos.SelectedItem).PostID;
                customers.NumCabID = ((NumCab)Numca.SelectedItem).NumCabID;
                //customers.RoleID = Convert.ToInt32(Role.SelectedValue);

                Users users = new Users();

                users.Login = Log.Text;
                users.Password = Pas.Password;
                if (customers.PostID == 4 )
                    users.RoleID = 3;
                else users.RoleID = 2;

                DBEntities.getContex().Users.Add(users);
                customers.UserID = users.UserID;
                DBEntities.getContex().Customers.Add(customers);
                DBEntities.getContex().SaveChanges();


                MBClass.InfoMB("Сотрудник добавлен");
            }

            catch (Exception ex)
            {
                throw;
                MBClass.ErrorMB(ex);
            }
        }

        string selectedPhotoFile = null;

        private void PhotoB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files (.png.jpeg)|*.png;*.jpeg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            if (openFileDialog.ShowDialog() == true)
            {
                PhotoIB.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                selectedPhotoFile = openFileDialog.FileName;

               
            }
           
        }
    }
}
