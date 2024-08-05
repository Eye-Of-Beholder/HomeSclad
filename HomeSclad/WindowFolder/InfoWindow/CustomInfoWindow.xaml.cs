using Pennant.ClassFolder;
using Pennant.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace Pennant.WindowFolder.InfoWindow
{
    /// <summary>
    /// Логика взаимодействия для CustomInfoWindow.xaml
    /// </summary>
    public partial class CustomInfoWindow : Window
    {
        public CustomInfoWindow(int userId)
        {
            InitializeComponent();
            LoadCustomerData(userId);
        }

        private void LoadCustomerData(int userId)
        {
            using (var context = new DBEntities())
            {
                var customer = context.Customers
                                      .Include(c => c.Gender)
                                      .Include(c => c.Post)
                                      .Include(c => c.NumCab)
                                      .Where(c => c.UserID == userId)
                                      .Select(c => new
                                      {
                                          c.SurnameCust,
                                          c.NameCust,
                                          c.PatronomycCust,
                                          c.EmailCust,
                                          c.PhoneCust,
                                          GenderInfo = c.Gender.GenderName,
                                          PostInfo = c.Post.PostName,
                                          NumCabInfo = c.NumCab.NumCabName,
                                          c.Photo
                                      })
                                      .FirstOrDefault();

                if (customer != null)
                {
                    DataContext = customer;
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }

}
