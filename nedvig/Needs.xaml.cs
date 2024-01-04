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
using System.Windows.Shapes;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для Needs.xaml
    /// </summary>
    public partial class Needs : Window
    {
        Need Need;
        int Flag;
        EntityModelContainer db = new EntityModelContainer();
        public Client SelectedClient { get; set; }
        public Agent SelectedAgent { get; set; }
        public Needs(Need need, int flag)
        {
            InitializeComponent();
            Need = need;
            Flag = flag;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)TypeObject.SelectedItem;
            string str = cbi.Content.ToString();
            if (str == "Дом" || str == "Квартира")
            {
                MinRoomLabel.Visibility = Visibility.Visible;
                MaxRoomLabel.Visibility = Visibility.Visible;
                MinFloorLabel.Visibility = Visibility.Visible;
                MaxFloorLabel.Visibility = Visibility.Visible;
                MinRoomTextBox.Visibility = Visibility.Visible;
                MaxRoomTextBox.Visibility = Visibility.Visible;
                MinFloorTextBox.Visibility = Visibility.Visible;
                MaxFloorTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                MinRoomLabel.Visibility = Visibility.Hidden;
                MaxRoomLabel.Visibility = Visibility.Hidden;
                MinFloorLabel.Visibility = Visibility.Hidden;
                MaxFloorLabel.Visibility = Visibility.Hidden;
                MinRoomTextBox.Visibility = Visibility.Hidden;
                MaxRoomTextBox.Visibility = Visibility.Hidden;
                MinFloorTextBox.Visibility = Visibility.Hidden;
                MaxFloorTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void combox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void combox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clients = db.ClientSet.ToList();
            Clients.ItemsSource = clients;

            var agents = db.AgentSet.ToList();
            Rieltors.ItemsSource = agents;

            if (Flag == 1)
            {
                Clients.SelectedItem = clients.FirstOrDefault(c => c.Id == SelectedClient.Id);
                Rieltors.SelectedItem = agents.FirstOrDefault(a => a.Id == SelectedAgent.Id);
            }
        }
    }
}
