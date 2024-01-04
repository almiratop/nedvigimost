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
    /// Логика взаимодействия для Deals.xaml
    /// </summary>
    public partial class Deals : Window
    {
        Deal Deal;
        int Flag;
        EntityModelContainer db = new EntityModelContainer();
        public Offer SelectedOffer { get; set; }
        public Need SelectedNeed { get; set; }
        public Deals(Deal deal, int flag)
        {
            InitializeComponent();
            Deal = deal;
            Flag = flag;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (var need in db.NeedSet)
            {
                bool exists = db.DealSet.Any(d => d.IdNeed == need.Id);
                if (!exists)
                {
                    NeedComBox.Items.Add(need);
                }
            }
            foreach (var offer in db.OfferSet)
            {
                bool exists = db.DealSet.Any(d => d.IdOffer == offer.Id);
                if (!exists)
                {
                    OfferComBox.Items.Add(offer);
                }
            }
            if (Flag == 1)
            {
                var needs = db.NeedSet.ToList();
                var offers = db.OfferSet.ToList();
                NeedComBox.Items.Add(needs.FirstOrDefault(c => c.Id == SelectedNeed.Id));
                OfferComBox.Items.Add(offers.FirstOrDefault(a => a.Id == SelectedOffer.Id));

                NeedComBox.SelectedItem = needs.FirstOrDefault(c => c.Id == SelectedNeed.Id);
                OfferComBox.SelectedItem = offers.FirstOrDefault(a => a.Id == SelectedOffer.Id);
            }
        }
    }
}
