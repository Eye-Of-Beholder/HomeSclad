using Pennant.ClassFolder;
using Pennant.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Condition = Pennant.DataFolder.Condition;

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для Warehouses.xaml
    /// </summary>
    public partial class Warehouses : Page
    {
        private string selectedPhotoFile = "";

        public Warehouses()
        {
            InitializeComponent();

            TypeofEquipment.ItemsSource = DBEntities.getContex().TypeofEquipment.ToList();
            Con.ItemsSource = DBEntities.getContex().Condition.ToList();
            //CustomRespo.ItemsSource = DBEntities.getContex().CustomersResponsible.ToList();

        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Equipment newEquipment = new Equipment();

                if (string.IsNullOrEmpty(selectedPhotoFile))
                {
                    MessageBox.Show("Добавьте фотографию", "Внимание");
                    return;
                }

                newEquipment.Photo = ImageClass.ImageToByte(selectedPhotoFile);

                if (TypeofEquipment.SelectedItem == null)
                {
                    MessageBox.Show("Выберите тип оборудования", "Внимание");
                    return;
                }

                if (Con.SelectedItem == null)
                {
                    MessageBox.Show("Выберите состояние оборудования", "Внимание");
                    return;
                }

                newEquipment.TypeofEquipmentID = ((TypeofEquipment)TypeofEquipment.SelectedItem).TypeofEquipmentID;
                newEquipment.ConditionID = ((Condition)Con.SelectedItem).ConditionID;
                newEquipment.Description = Op.Text;
                newEquipment.DescriptionOP = DESOp.Text;
                newEquipment.Istooc = false;

                // Присваиваем UserID авторизованного пользователя
                int userId = SessionInfo.CustomersID;
                //newEquipment.UserID = userId;
                newEquipment.CustomersID = userId;


                // Устанавливаем текущую дату захвата
                newEquipment.DateOfCapture = DateTime.Now;

                // Добавляем оборудование в контекст и сохраняем изменения
                DBEntities.getContex().Equipment.Add(newEquipment);
                DBEntities.getContex().SaveChanges();

                MessageBox.Show("Оборудование добавлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files (.png.jpeg)|*.png;*.jpeg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            if (openFileDialog.ShowDialog() == true)
            {
                PhotoOB.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                selectedPhotoFile = openFileDialog.FileName;
            }
        }
    }
}
