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
    /// Логика взаимодействия для MyEquipment.xaml
    /// </summary>
    public partial class MyEquipment : Page
    {
        public MyEquipment()
        {
            InitializeComponent();
            EquipmentListMY.ItemsSource = DBEntities.getContex().CustomersResponsible.Where(u => u.CustomersID == SessionInfo.CustomersID).ToList();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = EquipmentListMY.SelectedItem as CustomersResponsible;
            if (selectedUser != null)
            {
                int equipment = selectedUser.EquipmentID;
                var window = new InfoCustResEQ(equipment);
                window.Show();
            }
        }

        private void EquipmentList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EquipmentList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
