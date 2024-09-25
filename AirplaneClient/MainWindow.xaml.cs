using Airplane_Data_Access;
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

namespace AirplaneClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AirplaneDbContext dbContext = new AirplaneDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbContext.Clients.ToList();  
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbContext.Airplanes.ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbContext.Flights.ToList();
        }
    }
}
