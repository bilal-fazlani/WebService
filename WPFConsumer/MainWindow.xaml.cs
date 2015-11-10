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
using PortableClient;
using PortableModels;

namespace WPFConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiClient<Car> _client = new ApiClient<Car>
            ("http://BILALMUSTAF3107/webservice/api/mycars/");

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
            dataGrid.ItemsSource = await _client.GetAllAsync();
        }

        private async void AddClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ModelTxt.Text)
                && string.IsNullOrWhiteSpace(NumberTxt.Text))
            {
                return;
            }

            await _client.AddAsync(new Car
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
                await _client.DeleteAsync(car.Id);
                await Rebind();
            }
        }

        private async void RefreshbuttonClick(object sender, RoutedEventArgs e)
        {
            await Rebind();
        }
    }
}
