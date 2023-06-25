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

namespace demkaTUT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <param name="table">Список данных из БД</param>
        List<Table_2> table;
        private int previousIndex = 0;
        private demoEntities DB = new demoEntities();
        public MainWindow()
        {
            InitializeComponent();
            table = DB.Table_2.OrderBy(Name => Name.name).ToList();
            ListProd.ItemsSource = table;
        }
        private void Procent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = Procent.SelectedIndex;

            switch (selectedIndex)
            {
                case 0:
                    {
                        table = DB.Table_2.OrderBy(name => name.name).ToList();
                        break;
                    }
                case 1:
                    {
                        table = DB.Table_2.Where(name => name.discount >= 0 && name.discount < 10).OrderBy(name => name.discount).ToList();
                        break;
                    }
                case 2:
                    {
                        table = DB.Table_2.Where(name => name.discount >= 10 && name.discount < 15).OrderBy(name => name.discount).ToList();
                        break;
                    }
                case 3:
                    {
                        table = DB.Table_2.Where(name => name.discount >= 15).OrderBy(name => name.discount).ToList();
                        break;
                    }

            }
            ListProd.ItemsSource = table;
            previousIndex = selectedIndex;


        }
        private void vozr_Click(object sender, RoutedEventArgs e)
        {
            table = table.OrderBy(table => table.price - (table.price * table.discount / 100)).ToList();
            ListProd.ItemsSource = table;
        }

        /// <summary>
        ///  Метод кнопки сброса фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbros_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Сбросить сортировку?", "Внимание", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                table = table.OrderBy(name => name.name).ToList();
                ListProd.ItemsSource = table;
            }

        }

        /// <summary>
        /// Метод нажатия кнопки фильтрации по убыванию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ubv_Click(object sender, RoutedEventArgs e)
        {
            table = table.OrderByDescending(table => table.price - (table.price * table.discount / 100)).ToList();
            ListProd.ItemsSource = table;
        }
    }
}

