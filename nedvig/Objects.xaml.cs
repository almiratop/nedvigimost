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
    /// Логика взаимодействия для Objects.xaml
    /// </summary>
    public partial class Objects : Window
    {
        Estate estate;
        public Objects(Estate estate)
        {
            InitializeComponent();
            this.estate = estate;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)combox.SelectedItem;
            string str = cbi.Content.ToString();
            if (str == "Дом" || str == "Квартира")
            {
                floorlabel.Visibility = Visibility.Visible;
                FloorTextBox.Visibility = Visibility.Visible;
                roomlabel.Visibility = Visibility.Visible;
                RoomTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                floorlabel.Visibility = Visibility.Hidden;
                FloorTextBox.Visibility = Visibility.Hidden;
                roomlabel.Visibility = Visibility.Hidden;
                RoomTextBox.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
