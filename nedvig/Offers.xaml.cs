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
using static System.Windows.Forms.AxHost;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для Offers.xaml
    /// </summary>
    public partial class Offers : Window
    {
        EntityModelContainer db = new EntityModelContainer();
        Offer Offer;
        public Client SelectedClient { get; set; }
        public Agent SelectedAgent { get; set; }
        public Estate SelectedEstate { get; set; }
        int Flag;

        public Offers(Offer offer, int flag)
        {
            InitializeComponent();
            Offer = offer;
            Flag = flag;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clients = db.ClientSet.ToList();
            combox.ItemsSource = clients;

            var agents = db.AgentSet.ToList();
            combox2.ItemsSource = agents;

            foreach (var estate in db.EstateSet)
            {
                bool exists = db.OfferSet.Any(d => d.IdEstate == estate.Id);
                if (!exists)
                {
                    combox3.Items.Add(estate);
                }
            }
            if (Flag == 1)
            {
                var estates = db.EstateSet.ToList();
                combox3.Items.Add(estates.FirstOrDefault(s => s.Id == SelectedEstate.Id));
                combox.SelectedItem = clients.FirstOrDefault(c => c.Id == SelectedClient.Id);
                combox2.SelectedItem = agents.FirstOrDefault(a => a.Id == SelectedAgent.Id);
                combox3.SelectedItem = estates.FirstOrDefault(s => s.Id == SelectedEstate.Id);
            }
        }
    }
}
