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

namespace Tema1.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomersModelContainer _customersContext;

        public MainWindow()
        {
            InitializeComponent();
            _customersContext = new CustomersModelContainer();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                    
        }

        private void customerEntity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox) sender;

            switch (combo.SelectedIndex)
            {
                case 0:
                    entitiesBox.ItemsSource = _customersContext.CustomerTypes.Local.Select(x => x.Description);
                    break;
                case 1:
                    entitiesBox.ItemsSource = _customersContext.Customers.Local.Select(x => x.Name);
                    break;
                case 2:
                    entitiesBox.ItemsSource = _customersContext.CustomerEmails.Local.Select(x => x.Email);
                    break;
            }

        }
    }
}
