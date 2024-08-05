using CsvHelper;
using Pennant.ClassFolder;
using Pennant.DataFolder;
using Pennant.WindowFolder.InfoWindow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Xceed.Words.NET;

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для ListUsers.xaml
    /// </summary>
    public partial class ListUsers : Page
    {
        public ListUsers()
        {
            InitializeComponent();

            NameStaff.ItemsSource = DBEntities.getContex().Customers
            .ToList().OrderBy(u => u.NameCust);

            SearchTbPost.ItemsSource = DBEntities.getContex().Post.ToList();
        }

        private void NameStaff_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void NameStaff_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            NameStaff.ItemsSource = DataFolder.DBEntities.getContex().Customers
                .Where(u => u.SurnameCust.Contains(SearchTb.Text) ||
                            u.NameCust.Contains(SearchTb.Text) ||
                            u.PatronomycCust.Contains(SearchTb.Text) ||
                            u.Users.Login.Contains(SearchTb.Text)).ToList();
        }

        //private void GrillChessInfoMI_Click(object sender, RoutedEventArgs e)
        //{
        //    new CustomInfoWindow().Show();
        //}

        private void OpenUserDetails(object sender, RoutedEventArgs e)
        {
            //// Получите выбранного пользователя (предположим, что у вас DataGrid)
            //var selectedUser = NameStaff.SelectedItem as Customers; // или ваш тип данных
            //if (selectedUser != null)
            //{
            //    int userId = selectedUser.UserID; // Предположим, что у вашего объекта есть UserID
            //    var window = new CustomInfoWindow(userId);
            //    window.Show();
            //}
        }

        private void GrillChessEditMI_Click(object sender, RoutedEventArgs e)
        {
            if (NameStaff.SelectedItem == null)
            {
                MBClass.ErrorMB("Выберите пользователя для редактирования");
            }
            else
            {
                NavigationService.Navigate(new UserEdit(NameStaff.SelectedItem as Customers));
            }
        }

        private void GrillChessRemoveMI_Click(object sender, RoutedEventArgs e)
        {
            Customers customers = NameStaff.SelectedItem as Customers;
            try
            {
                if (MBClass.QuestionMB("Вы действительно хотите удалить этого сотрудника"))
                {

                    Customers customers1 = DBEntities.getContex().Customers.FirstOrDefault(u => u.CustomersID == customers.CustomersID);
                    DBEntities.getContex().Customers.Remove(customers);
                    DBEntities.getContex().SaveChanges();

                    UpdateList();
                }
            }
            catch (Exception ex)
            {

                MBClass.ErrorMB(ex);
            }
        }

        private void UpdateList()
        {

            NameStaff.ItemsSource = DataFolder.DBEntities.getContex().Customers
                 .Where(u => u.SurnameCust.Contains(SearchTb.Text) ||
                             u.NameCust.Contains(SearchTb.Text) ||
                             u.PatronomycCust.Contains(SearchTb.Text) ||
                             u.Users.Login.Contains(SearchTb.Text)).ToList();

        }

        private void SearchTbPost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPost = SearchTbPost.SelectedItem as Post; 
            if (selectedPost != null)
            {
                NameStaff.ItemsSource = DataFolder.DBEntities.getContex().Customers
                   .Where(c => c.PostID == selectedPost.PostID).ToList();
            }
        }

        private void GrillChessInfoMI_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedUser = NameStaff.SelectedItem as Customers; 
            if (selectedUser != null)
            {
                int userId = selectedUser.UserID; 
                var window = new CustomInfoWindow(userId);
                window.Show();
            }
        }

        private void ex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем список сотрудников
                var employees = DBEntities.getContex().Customers.ToList();

                // Получаем путь к папке "Загрузки" пользователя
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

                // Указываем путь для сохранения файла в папке "Загрузки"
                string filePath = System.IO.Path.Combine(downloadsPath, "Сотрудники.docx");

                // Создаем документ Word
                var doc = DocX.Create(filePath);

                // Добавляем заголовок
                doc.InsertParagraph("Список сотрудников").FontSize(16).Bold().Alignment = Xceed.Document.NET.Alignment.center;

                // Добавляем пустую строку
                doc.InsertParagraph(Environment.NewLine);

                foreach (var employee in employees)
                {
                    // Получаем должность сотрудника
                    var post = DBEntities.getContex().Posts.FirstOrDefault(p => p.PostID == employee.PostID);

                    // Добавляем строку с информацией о сотруднике в формате "Имя Отчество Фамилия - Должность"
                    string employeeInfo = $"{employee.NameCust} {employee.PatronomycCust} {employee.SurnameCust} - {(post != null ? post.PostName : "Должность не указана")}";
                    doc.InsertParagraph(employeeInfo).FontSize(12);
                }

                // Сохраняем документ
                doc.Save();

                MessageBox.Show("Данные успешно экспортированы", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка экспорта данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
