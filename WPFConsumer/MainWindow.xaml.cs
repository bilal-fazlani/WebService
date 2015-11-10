using System;
using System.Collections;
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
using Client;
using Models;

namespace WPFConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly CarsClient Client = new CarsClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void window_loaded(object sender, RoutedEventArgs e)
        {
            await Rebind();
        }

        private async Task Rebind()
        {
            dataGrid.ItemsSource = await Client.GetCarsAsync();
        }

        private async void AddClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ModelTxt.Text)
                && string.IsNullOrWhiteSpace(NumberTxt.Text))
            {
                return;
            }

            await Client.AddCarAsync(new Car
            {
                Model = ModelTxt.Text,
                Number = NumberTxt.Text
            });

            NumberTxt.Clear();
            ModelTxt.Clear();

            await Rebind();
        }

        private async void DeleteClicked(object sender, RoutedEventArgs e)
        {

            var allCars = dataGrid.SelectedItems.Cast<Car>().ToList();

            foreach (Car car in allCars)
            {
                await Client.DeleteCarAsync(car.Id);
                await Rebind();
            }
        }

        private async void RefreshbuttonClick(object sender, RoutedEventArgs e)
        {
            await Rebind();
        }
    }
}
