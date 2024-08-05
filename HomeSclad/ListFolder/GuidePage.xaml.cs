using Pennant.ClassFolder;
using Pennant.DataFolder;
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
    /// Логика взаимодействия для GuidePage.xaml
    /// </summary>
    public partial class GuidePage : Page
    {

        public GuidePage()
        {
            InitializeComponent();

        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool dataEntered = false; // Флаг, указывающий, были ли введены данные

                if (!string.IsNullOrEmpty(PostTextBox.Text))
                {
                    Post newPost = new Post
                    {
                        PostName = PostTextBox.Text
                    };

                    DBEntities.getContex().Post.Add(newPost);
                    MBClass.InfoMB("Должность добавлена");

                    dataEntered = true;
                }

                if (!string.IsNullOrEmpty(CabTextBox.Text))
                {
                    NumCab newCab = new NumCab
                    {
                        NumCabName = CabTextBox.Text
                    };

                    DBEntities.getContex().NumCab.Add(newCab);
                    MBClass.InfoMB("Кабинет добавлен");

                    dataEntered = true;
                }

                if (!dataEntered)
                {
                    MessageBox.Show("Пожалуйста, введите данные.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DBEntities.getContex().SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }

        }
        }
}


