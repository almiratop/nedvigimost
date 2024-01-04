using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для OfferPage.xaml
    /// </summary>
    public partial class OfferPage : Page
    {
        EntityModelContainer db = new EntityModelContainer();
        int offerid = 0;
        public OfferPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            var query = from offer in db.OfferSet
                        join client in db.ClientSet on offer.IdClient equals client.Id
                        join agent in db.AgentSet on offer.IdAgent equals agent.Id
                        join estate in db.EstateSet on offer.IdEstate equals estate.Id
                        select new
                        {
                            OfferId = offer.Id,
                            ClientId = client.Id,
                            ClientName = client.Surname + " " + client.Name + " " + client.LastName,
                            AgentId = agent.Id,
                            AgentName = agent.Surname + " " + agent.Name + " " + agent.LastName,
                            EstateId = estate.Id,
                            EstateType = estate.Type,
                            EstateAddress = estate.Adress,
                            Price = offer.Price
                        };

            dataGrid.ItemsSource = query.ToList();
        }
        private void AddOffer_Click(object sender, RoutedEventArgs e)
        {
            Offers form = new Offers(new Offer(), 0);
            if (form.ShowDialog() == true)
            {
                int intValue;
                if (int.TryParse(form.PriceTextBox.Text, out intValue) && form.combox.SelectedItem != null && form.combox2.SelectedItem != null && form.combox3.SelectedItem != null)
                {
                    var selectedClient = (Client)form.combox.SelectedItem;
                    var selectedAgent = (Agent)form.combox2.SelectedItem;
                    var selectedEstate = (Estate)form.combox3.SelectedItem;
                    Offer offer = new Offer();
                    int maxId = db.OfferSet.Max(of => of.Id) + 1;
                    offer.Id = maxId;
                    offer.IdClient = selectedClient.Id;
                    offer.IdAgent = selectedAgent.Id;
                    offer.IdEstate = selectedEstate.Id;
                    offer.Price = Convert.ToInt32(form.PriceTextBox.Text);
                    db.OfferSet.Add(offer);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Новое предложение добавлено");
                }
                else
                {
                    MessageBox.Show("Все поля обязательные. Цена - это целое число");
                }
            }
        }

        private void EditOffer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as dynamic;

                Offer offer = db.OfferSet.Find(selectedRow.OfferId);

                Client client = db.ClientSet.Find(selectedRow.ClientId);
                Agent agent = db.AgentSet.Find(selectedRow.AgentId);
                Estate estate = db.EstateSet.Find(selectedRow.EstateId);

                Offers form = new Offers(offer,1);
                form.SelectedClient = client;
                form.SelectedAgent = agent;
                form.SelectedEstate = estate;
                form.PriceTextBox.Text = Convert.ToString(selectedRow.Price);


                if (form.ShowDialog() == true)
                {
                    int intValue;                   
                    if (int.TryParse(form.PriceTextBox.Text, out intValue) && form.combox.SelectedItem != null && form.combox2.SelectedItem != null && form.combox3.SelectedItem != null)
                    {
                        var selectedClient = (Client)form.combox.SelectedItem;
                        var selectedAgent = (Agent)form.combox2.SelectedItem;
                        var selectedEstate = (Estate)form.combox3.SelectedItem;
                        offer.IdClient = selectedClient.Id;
                        offer.IdAgent = selectedAgent.Id;
                        offer.IdEstate = selectedEstate.Id;
                        offer.Price = Convert.ToInt32(form.PriceTextBox.Text);
                        db.SaveChanges();

                        UpdateDataGrid();
                        MessageBox.Show("Успешно изменено");
                    }
                    else { MessageBox.Show("Все поля обязательные. Цена - это целое число"); }
                }
            }
        }

        private void DeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid.SelectedItem);
                text = text.Replace("OfferId", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("ClientId", "");
                text = text.Replace("ClientName", "");
                text = text.Replace("AgentId", "");
                text = text.Replace("AgentName", "");
                text = text.Replace("EstateId", "");
                text = text.Replace("EstateType", "");
                text = text.Replace("Price", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });


                int offerId = Convert.ToInt32(word[0]);

                bool hasDeals = db.DealSet.Any(deal => deal.IdOffer == offerId);

                if (hasDeals)
                {
                    MessageBox.Show("Нельзя удалить предложение, связанного со сделкой.");
                }
                else
                {
                    Offer offer = db.OfferSet.Find(offerId);
                    db.OfferSet.Remove(offer);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Успешно удалено");
                }

            }
        }

        private void CreateDeal_Click(object sender, RoutedEventArgs e)
        {
            var selectedNeed = (Need)NeedComBox.SelectedItem;
            Deal deal = new Deal();
            int maxId = db.DealSet.Max(of => of.Id) + 1;
            deal.Id = maxId;
            deal.IdOffer = offerid;
            deal.IdNeed = selectedNeed.Id;
            db.DealSet.Add(deal);
            db.SaveChanges();
            MessageBox.Show("Сделка создана успешно!");
            NeedComBox.Items.Clear();
            NeedComBox.SelectedItem = null;
            CreateDeal.IsEnabled = false;
            dataGrid.UnselectAll();

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                NeedComBox.Items.Clear();
                string text = Convert.ToString(dataGrid.SelectedItem);
                text = text.Replace("OfferId", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("ClientId", "");
                text = text.Replace("ClientName", "");
                text = text.Replace("AgentId", "");
                text = text.Replace("AgentName", "");
                text = text.Replace("EstateId", "");
                text = text.Replace("EstateType", "");
                text = text.Replace("Price", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Offer offer = db.OfferSet.Find(Convert.ToInt32(word[0]));
                offerid = offer.Id;

                Estate estate = db.EstateSet.Find(Convert.ToInt32(word[5]));

                string type = word[6];
                int price = Convert.ToInt32(word[word.Length - 1]);

                bool exists = db.DealSet.Any(d => d.IdOffer == offer.Id);

                if (!exists)
                {
                    foreach (var need in db.NeedSet)
                    {
                        if (need.Type == type)
                        {
                            if (type == "Apartment")
                            {
                                foreach (var apart in db.ApartmentNeedSet)
                                {
                                    if (apart.IdNeed == need.Id)
                                    {
                                        Apartment apartm = db.ApartmentSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                        int? room = null;
                                        int? floor = null;
                                        double? area = null;
                                        if (apartm.Room != null) { room = (int)apartm.Room; }
                                        if (apartm.Floor != null) { floor = (int)apartm.Floor; }
                                        if (apartm.Area != null) { area = (double)apartm.Area; }

                                        bool minPrice = need.MinPrice == null || need.MinPrice <= price;
                                        bool maxPrice = need.MaxPrice == null || need.MaxPrice >= price;
                                        bool minRoom = apart.MinRoom == null || (room != null && apart.MinRoom <= room);
                                        bool maxRoom = apart.MaxRoom == null || (room != null && apart.MaxRoom >= room);
                                        bool minFloor = apart.MinFloor == null || (floor != null && apart.MinFloor <= floor);
                                        bool maxFloor = apart.MaxFloor == null || (floor != null && apart.MaxFloor >= floor);
                                        bool minArea = apart.MinArea == null || (area != null && apart.MinArea <= area);
                                        bool maxArea = apart.MaxArea == null || (area != null && apart.MaxArea >= area);

                                        bool exists2 = db.DealSet.Any(d => d.IdNeed == need.Id);

                                        if (minPrice && maxPrice && minRoom && maxRoom && minFloor && maxFloor && minArea && maxArea && !exists2)
                                        {
                                            NeedComBox.Items.Add(need);
                                        }
                                    }
                                }
                            }
                            if(type == "Land")
                            {
                                foreach (var land in db.LandNeedSet)
                                {
                                    if (land.IdNeed == need.Id)
                                    {
                                        Land lan = db.LandSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                        double? area = null;
                                        if (lan.Area != null) { area = (double)lan.Area; }

                                        bool minPrice = need.MinPrice == null || need.MinPrice <= price;
                                        bool maxPrice = need.MaxPrice == null || need.MaxPrice >= price;
                                        bool minArea = land.MinArea == null || (area != null && land.MinArea <= area);
                                        bool maxArea = land.MaxArea == null || (area != null && land.MaxArea >= area);

                                        bool exists2 = db.DealSet.Any(d => d.IdNeed == need.Id);

                                        if (minPrice && maxPrice && minArea && maxArea && !exists2)
                                        {
                                            NeedComBox.Items.Add(need);
                                        }
                                    }
                                }
                            }
                            if (type == "House")
                            {
                                foreach (var house in db.HouseNeedSet)
                                {
                                    if (house.IdNeed == need.Id)
                                    {
                                        House hous = db.HouseSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                        int? room = null;
                                        int? floor = null;
                                        double? area = null;
                                        if (hous.Room != null) { room = (int)hous.Room; }
                                        if (hous.Floor != null) { floor = (int)hous.Floor; }
                                        if (hous.Area != null) { area = (double)hous.Area; }

                                        bool minPrice = need.MinPrice == null || need.MinPrice <= price;
                                        bool maxPrice = need.MaxPrice == null || need.MaxPrice >= price;
                                        bool minRoom = house.MinRoom == null || (room != null && house.MinRoom <= room);
                                        bool maxRoom = house.MaxRoom == null || (room != null && house.MaxRoom >= room);
                                        bool minFloor = house.MinFloor == null || (floor != null && house.MinFloor <= floor);
                                        bool maxFloor = house.MaxFloor == null || (floor != null && house.MaxFloor >= floor);
                                        bool minArea = house.MinArea == null || (area != null && house.MinArea <= area);
                                        bool maxArea = house.MaxArea == null || (area != null && house.MaxArea >= area);

                                        bool exists2 = db.DealSet.Any(d => d.IdNeed == need.Id);

                                        if (minPrice && maxPrice && minRoom && maxRoom && minFloor && maxFloor && minArea && maxArea && !exists2)
                                        {
                                            NeedComBox.Items.Add(need);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Need_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateDeal.IsEnabled = true;
        }
    }
}
