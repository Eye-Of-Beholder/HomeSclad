using Pennant.ClassFolder;
using Pennant.DataFolder;
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

namespace Pennant.WindowFolder.CabInfo
{
    /// <summary>
    /// Логика взаимодействия для CustInfo.xaml
    /// </summary>
    public partial class CustInfo : Page, IValueConverter
    {
        public CustInfo(int userId)
        {
            InitializeComponent();
            LoadCustomerData(userId);
            LoadPhotoFromDatabase();
        }

        private void LoadCustomerData(int userId)
        {

            var user = DBEntities.getContex().Customers
                                 .Where(e => e.UserID == userId)
                                 .FirstOrDefault();

            if (user != null)
            {
                DataContext = user;
            }
            else
            {
                // Отладочная информация
                var allUserIds = DBEntities.getContex().Customers.Select(c => c.UserID).ToList();
                MessageBox.Show($"Пользователь не найден! Доступные userIds: {string.Join(", ", allUserIds)}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    


    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray)
            {
                using (var stream = new MemoryStream(byteArray))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            return null;
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}