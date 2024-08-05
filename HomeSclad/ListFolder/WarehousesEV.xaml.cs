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
    /// Логика взаимодействия для WarehousesEV.xaml
    /// </summary>
    public partial class WarehousesEV : Page
    {
        public WarehousesEV()
        {
            InitializeComponent();
            EquipmentList.ItemsSource = DBEntities.getContex().Equipment.Where(u => u.Istooc == true).ToList();
        }

        private void EquipmentList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = EquipmentList.SelectedItem as Equipment;
            if (selectedUser != null)
            {
                int equipment = selectedUser.EquipmentID;
                var window = new InfoCustResEQ(equipment);
                window.Show();
            }
        }

        private void EquipmentList_MouseRightButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void EquipmentList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
