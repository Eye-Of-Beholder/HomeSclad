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
    /// Логика взаимодействия для WarehousesRes.xaml
    /// </summary>
    public partial class WarehousesRes : Page
    {
        public WarehousesRes()
        {
            InitializeComponent();
            EquipmentList.ItemsSource = DBEntities.getContex().Equipment.Where(u => u.Istooc == false).ToList();
          
        }

        private void GrillChessRemoveMI_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehousesEdit(EquipmentList.SelectedItem as Equipment));
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

        private void EquipmentList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EquipmentList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ob_vz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Warehouse_V_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WA_VZ_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
