using Pennant.ClassFolder;
using Pennant.DataFolder;
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

namespace Pennant.ListFolder
{
    /// <summary>
    /// Логика взаимодействия для AddWarehouses.xaml
    /// </summary>
    public partial class AddWarehouses : Page
    {
        public AddWarehouses()
        {
            InitializeComponent();

            EquipmentList.ItemsSource = DBEntities.getContex().Equipment.Where(u => u.Istooc == false).ToList();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Equipment equipment = ListWarehouses.ItemsSource as Equipment;

            //if(ListWarehouses.SelectedItem == null) 
            //{
            //    MBClass.ErrorMB("Выберите оборудование");
            //}
            //else
            //{
            //    if (MBClass.QuestionMB("Взять это оборудование?"))
            //    {
            //        DBEntities.getContex().Equipment.Remove
            //            (ListWarehouses.SelectedItem as Equipment);
            //        DBEntities.getContex().SaveChanges();
            //        MBClass.InfoMB("Оборудование взято");
            //        ListWarehouses.ItemsSource = DBEntities.getContex().Equipment.ToList().OrderBy(u => u.EquipmentID);
            //    }
            //}
        }

        private void EquipmentList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EquipmentList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void GrillChessInfoMI_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = EquipmentList.SelectedItem as Equipment;
            if (selectedUser != null)
            {
                int equipment = selectedUser.EquipmentID;
                var window = new EquipmentInfoWindow(equipment);
                window.Show();
            }
        }

        private void GrillChessRemoveMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selecteditem = EquipmentList.SelectedItem as Equipment;
                if (selecteditem != null)
                {
                    CustomersResponsible customersResponsible = new CustomersResponsible();
                    customersResponsible.CustomersID = SessionInfo.CustomersID;
                    customersResponsible.EquipmentID = selecteditem.EquipmentID;
                    customersResponsible.DateOfCapture = DateTime.Now;
                    DBEntities.getContex().CustomersResponsible.Add(customersResponsible);
                    selecteditem.Istooc = true;
                    DBEntities.getContex().SaveChanges();
                    EquipmentList.ItemsSource = DBEntities.getContex().Equipment.Where(u => u.Istooc == false).ToList();
                }      
            }
            catch (Exception ex)
            {
                throw;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VZt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
