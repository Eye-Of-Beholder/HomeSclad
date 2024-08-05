using Pennant.ClassFolder;
using Pennant.DataFolder;
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
using System.Windows.Shapes;

namespace Pennant.WindowFolder.InfoWindow
{
    /// <summary>
    /// Логика взаимодействия для EquipmentInfoWindow.xaml
    /// </summary>
    public partial class EquipmentInfoWindow : Window
    {
        public EquipmentInfoWindow(int Equipmentid)
        {
            InitializeComponent();
            LoadPhotoFromDatabase();
            LoadEquipmentData(Equipmentid);
        }

        private void LoadEquipmentData(int equipmentId)
        {
                var equipment =DBEntities.getContex().Equipment
                                       .Where(e => e.EquipmentID == equipmentId)                                
                                       .FirstOrDefault();
            
                if (equipment != null)
                {
                    DataContext = equipment;
                }
                else
                {
                    MessageBox.Show("Оборудование не найдено");
                }
            
        }

        private void LoadPhotoFromDatabase()
        {
            using (var context = new DBEntities())
            {
                var customer = context.Customers.FirstOrDefault(c => c.CustomersID == 47);
                if (customer != null)
                {
                    this.DataContext = customer;
                }
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray)
            {
                return ImageClass.BytesToImage(byteArray);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
