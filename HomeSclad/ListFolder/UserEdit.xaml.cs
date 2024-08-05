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
using System.IO;

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        Customers customers;
        Users users;
        private string selectedPhotoFile;
        private string selectedPhoto;

        public UserEdit(Customers customers)
        {
            InitializeComponent();

            try
            {
            
                Gender.ItemsSource = DBEntities.getContex().Gender.ToList();
                Pos.ItemsSource = DBEntities.getContex().Post.ToList();
                Numca.ItemsSource = DBEntities.getContex().NumCab.ToList();

                this.customers = customers;

                //Вывод данных для редактирования
                Surn.Text = customers.SurnameCust.ToString();
                Log.Text = customers.Users.Login.ToString();
                Nam.Text = customers.NameCust.ToString();
                Patro.Text = customers.PatronomycCust.ToString();
                Phone.Text = customers.PhoneCust.ToString();
                Email.Text = customers.EmailCust.ToString();
                Gender.SelectedValue = customers.GenderID;
                Pos.SelectedValue = customers.PostID;
                Numca.SelectedValue = customers.NumCabID;
                Pas.Password = customers.Users.Password;
                DubPas.Password = customers.Users.Password;
                LoadCustomerPhoto(customers.Photo);


            }

            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

       
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(Surn.Text) ||
                    string.IsNullOrWhiteSpace(Nam.Text) ||
                    string.IsNullOrWhiteSpace(Patro.Text) ||
                    string.IsNullOrWhiteSpace(Phone.Text) ||
                    string.IsNullOrWhiteSpace(Email.Text) ||
                    string.IsNullOrWhiteSpace(Log.Text) ||
                    string.IsNullOrWhiteSpace(Pas.Password) ||
                    string.IsNullOrWhiteSpace(DubPas.Password) ||
                    Gender.SelectedValue == null ||
                    Pos.SelectedValue == null ||
                    Numca.SelectedValue == null)
                {
                    MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Pas.Password != DubPas.Password)
                {
                    MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                customers = DBEntities.getContex().Customers.FirstOrDefault(u => u.CustomersID == customers.CustomersID);
                customers.NameCust = Nam.Text;
                customers.PhoneCust = Phone.Text;
                customers.EmailCust = Email.Text;
                customers.Users.Login = Log.Text;
                customers.Users.Password = Pas.Password;
                customers.GenderID = Convert.ToInt32(Gender.SelectedValue);
                customers.NumCabID = Convert.ToInt32(Numca.SelectedValue);
                customers.PostID = Convert.ToInt32(Pos.SelectedValue);
                customers.SurnameCust = Surn.Text;
                customers.PatronomycCust = Patro.Text;
                if (selectedPhoto != "Картинка кароче есть")
                    customers.Photo = ImageClass.ImageToByte(selectedPhotoFile);

                DBEntities.getContex().SaveChanges();

                MBClass.InfoMB("Данные успешно отредактированы");

                NavigationService.Navigate(new ListUsers());

                if (string.IsNullOrEmpty(selectedPhotoFile))
                {
                    MessageBox.Show("Добавьте фотографию", "Внимание");
                    return;
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }


        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Nam.Text = this.customers.NameCust;
            Phone.Text = this.customers.PhoneCust;
            Email.Text = this.customers.EmailCust;
            Gender.SelectedIndex = this.customers.GenderID;
            //Role.SelectedIndex = this.customers.RoleID;
            Log.Text = this.customers.Users.Login;
            Pas.Password = this.customers.Users.Password;
            Surn.Text = this.customers.SurnameCust;
            Patro.Text = this.customers.PatronomycCust;
            Pos.SelectedIndex = this.customers.PostID;
            Numca.SelectedIndex = this.customers.NumCabID;
            selectedPhoto = "Картинка кароче есть";
        }

        private void LoadCustomerPhoto(byte[] photoData)
        {
            if (photoData != null && photoData.Length > 0)
            {
                using (var stream = new MemoryStream(photoData))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    PhotoIB.ImageSource = bitmap;
                }
            }
            else
            {
                PhotoIB.ImageSource = new BitmapImage(new Uri("/ImageFolder/male.png", UriKind.Relative));
            }
        }
    }
}

