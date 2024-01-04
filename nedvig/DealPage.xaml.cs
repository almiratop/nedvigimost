using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для DealPage.xaml
    /// </summary>
    public partial class DealPage : Page
    {
        EntityModelContainer db = new EntityModelContainer();
        public DealPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            var query = from deal in db.DealSet
                        join offer in db.OfferSet on deal.IdOffer equals offer.Id
                        join need in db.NeedSet on deal.IdNeed equals need.Id
                        join clientoffer in db.ClientSet on offer.IdClient equals clientoffer.Id
                        join agentoffer in db.AgentSet on offer.IdAgent equals agentoffer.Id
                        join clientneed in db.ClientSet on need.IdClient equals clientneed.Id
                        join agentneed in db.AgentSet on need.IdAgent equals agentneed.Id
                        join estate in db.EstateSet on offer.IdEstate equals estate.Id
                        select new
                        {
                            DealId = deal.Id,
                            NeedId = need.Id,
                            ClientNeedId = clientneed.Id,
                            ClientNeedName = clientneed.Surname + " " + clientneed.Name + " " + clientneed.LastName,
                            AgentNeedId = agentneed.Id,
                            AgentNeedName = agentneed.Surname + " " + agentneed.Name + " " + agentneed.LastName,
                            AgentNeedComission = agentneed.Comission,
                            NeedMinPrice = need.MinPrice,
                            NeedMaxPrice = need.MaxPrice,
                            NeedAdress = need.Adress,
                            OfferId = offer.Id,
                            ClientOfferId = clientoffer.Id,
                            ClientOfferName = clientoffer.Surname + " " + clientoffer.Name + " " + clientoffer.LastName,
                            AgentOfferId = agentoffer.Id,
                            AgentOfferName = agentoffer.Surname + " " + agentoffer.Name + " " + agentoffer.LastName,
                            AgentOfferComission = agentoffer.Comission,
                            Price = offer.Price,
                            EstateId = estate.Id,
                            EstateType = estate.Type,
                            EstateAddress = estate.Adress
                        };

            dataGrid.ItemsSource = query.ToList();
        }

        private void AddDeal_Click(object sender, RoutedEventArgs e)
        {
            Deals form = new Deals(new Deal(), 0);
            if (form.ShowDialog() == true)
            {
                int intValue;
                if (form.NeedComBox.SelectedItem != null && form.OfferComBox.SelectedItem != null)
                {
                    var selectedNeed = (Need)form.NeedComBox.SelectedItem;
                    var selectedOffer = (Offer)form.OfferComBox.SelectedItem;

                    Deal deal = new Deal();
                    int maxId = db.DealSet.Max(of => of.Id) + 1;
                    deal.Id = maxId;
                    deal.IdNeed = selectedNeed.Id;
                    deal.IdOffer = selectedOffer.Id;
                    
                    db.DealSet.Add(deal);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Новая сделка создана");
                }
                else
                {
                    MessageBox.Show("Выберите потребность и предложение");
                }
            }
        }

        private void EditDeal_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as dynamic;

                Deal deal = db.DealSet.Find(selectedRow.DealId);
                Need need = db.NeedSet.Find(selectedRow.NeedId);
                Offer offer = db.OfferSet.Find(selectedRow.OfferId);


                Deals form = new Deals(deal, 1);
                form.SelectedNeed = need;
                form.SelectedOffer = offer;


                if (form.ShowDialog() == true)
                {
                    int intValue;
                    if (form.NeedComBox.SelectedItem != null && form.OfferComBox.SelectedItem != null)
                    {
                        var selectedNeed = (Client)form.NeedComBox.SelectedItem;
                        var selectedOffer = (Agent)form.OfferComBox.SelectedItem;
                        db.SaveChanges();
                        UpdateDataGrid();
                        MessageBox.Show("Успешно изменено");
                    }
                    else { MessageBox.Show("Все поля обязательные."); }
                }
            }
        }

        private void DeleteDeal_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as dynamic;
                Deal deal = db.DealSet.Find(selectedRow.DealId);

                db.DealSet.Remove(deal);
                db.SaveChanges();
                UpdateDataGrid();
                MessageBox.Show("Успешно удалено");


            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedRow = dataGrid.SelectedItem as dynamic;
            if (selectedRow!= null)
            {
                string propertyType = selectedRow.EstateType;
                int estateprice = selectedRow.Price;

                double sellercompanyCommission = 0;

                if (propertyType == "Apartment")
                {
                    sellercompanyCommission = 36000 + (0.01 * estateprice);
                }
                else if (propertyType == "Land")
                {
                    sellercompanyCommission = 30000 + (0.02 * estateprice);
                }
                else if (propertyType == "House")
                {
                    sellercompanyCommission = 30000 + (0.01 * estateprice);
                }

                double buyercompanyCommission = 0.03 * estateprice;
                double sellerRealtorShare;
                double buyerRealtorShare;
                if (selectedRow.AgentOfferComission != null) { sellerRealtorShare = selectedRow.AgentOfferComission * estateprice / 100; }
                else { sellerRealtorShare = 0.45 * estateprice; }

                if (selectedRow.AgentNeedComission != null) { buyerRealtorShare = selectedRow.AgentNeedComission * estateprice / 100; }
                else { buyerRealtorShare = 0.45 * estateprice; }

                double companyCommission = sellercompanyCommission + buyercompanyCommission;

                resultTextBlock.Text = $"Стоимость услуг для клиента-продавца: {sellercompanyCommission:C}\n" +
                                       $"Стоимость услуг для клиента-покупателя: {buyercompanyCommission:C}\n" +
                                       $"Размер отчислений риэлтору клиента-продавца: {sellerRealtorShare:C}\n" +
                                       $"Размер отчислений риэлтору клиента-покупателя: {buyerRealtorShare:C}\n" +
                                       $"Размер отчислений компании: {companyCommission:C}";

            }
        }
    }
}
