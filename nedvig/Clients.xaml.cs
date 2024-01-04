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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        Client Client;
        public Clients(Client client)
        {
            InitializeComponent();
            Client = client;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
