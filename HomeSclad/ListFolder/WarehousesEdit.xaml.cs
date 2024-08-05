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

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для WarehousesEdit.xaml
    /// </summary>
    public partial class WarehousesEdit : Page
    {
        Equipment equipment;
        private string selectedPhotoFile;
        private string selectedPhoto;

        public WarehousesEdit(Equipment equipment)
        {
            InitializeComponent();
            this.equipment = equipment;
            TypeofEquipmentEd.ItemsSource = DBEntities.getContex().TypeofEquipment.ToList();
            Con.ItemsSource = DBEntities.getContex().Condition.ToList();
            try
            {
                TypeofEquipmentEd.SelectedValue = equipment.TypeofEquipmentID;
                Con.SelectedValue = equipment.ConditionID;
                Op.Text = equipment.Description.ToString();
                DATE.SelectedDate = equipment.DateOfCapture;
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
                PhotoOB.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                selectedPhotoFile = openFileDialog.FileName;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            { equipment = DBEntities.getContex().Equipment.FirstOrDefault(u => u.EquipmentID == equipment.EquipmentID);

                equipment.TypeofEquipmentID = Convert.ToInt32(TypeofEquipmentEd.SelectedValue);
                equipment.ConditionID = Convert.ToInt32(Con.SelectedValue);
                equipment.Description = Op.Text;
                equipment.DateOfCapture = DATE.SelectedDate;
                if (selectedPhoto != "Картинка кароче есть")
                    equipment.Photo = ImageClass.ImageToByte(selectedPhotoFile);

                DBEntities.getContex().SaveChanges();

                MBClass.InfoMB("Данные успешно отредактированы");

                NavigationService.Navigate(new WarehousesRes());
            }
            catch (Exception ex)
            {
                
                MBClass.ErrorMB(ex);
            }
        }
    }
}
