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
    /// Логика взаимодействия для NeedPage.xaml
    /// </summary>
    public partial class NeedPage : Page
    {
        EntityModelContainer db = new EntityModelContainer();
        int needid = 0;
        public NeedPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }
        public void UpdateDataGrid()
        {
            var query = from need in db.NeedSet
                        join apartmentneed in db.ApartmentNeedSet on need.Id equals apartmentneed.IdNeed
                        select new { need.Id, need.Type, need.Adress, need.MaxPrice, need.MinPrice, need.IdClient, need.IdAgent, apartmentneed.MaxRoom, apartmentneed.MinRoom, apartmentneed.MaxFloor, apartmentneed.MinFloor, apartmentneed.MaxArea, apartmentneed.MinArea };
            dataGrid.ItemsSource = query.ToList();

            var query2 = from need in db.NeedSet
                         join landneed in db.LandNeedSet on need.Id equals landneed.IdNeed
                         select new { need.Id, need.Type, need.Adress, need.MaxPrice, need.MinPrice, need.IdClient, need.IdAgent, landneed.MaxArea, landneed.MinArea };
            dataGrid2.ItemsSource = query2.ToList();

            var query3 = from need in db.NeedSet
                         join houseneed in db.HouseNeedSet on need.Id equals houseneed.IdNeed
                         select new { need.Id, need.Type, need.Adress, need.MaxPrice, need.MinPrice, need.IdClient, need.IdAgent, houseneed.MaxRoom, houseneed.MinRoom, houseneed.MaxFloor, houseneed.MinFloor, houseneed.MaxArea, houseneed.MinArea };
            dataGrid3.ItemsSource = query3.ToList();
        }
        
        private void AddNeed_Click(object sender, RoutedEventArgs e)
        {
            Needs form = new Needs(new Need(), 0);
            if (form.ShowDialog() == true)
            {
                if (form.TypeObject.SelectedItem != null && form.Clients.SelectedItem != null && form.Rieltors.SelectedItem != null)
                {
                    ComboBoxItem cbi = (ComboBoxItem)form.TypeObject.SelectedItem;
                    string str = cbi.Content.ToString();
                    var selectedClient = (Client)form.Clients.SelectedItem;
                    var selectedAgent = (Agent)form.Rieltors.SelectedItem;
                    if (str == "Квартира")
                    {
                        int intValue;
                        if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinRoomTextBox.Text == "" || int.TryParse(form.MinRoomTextBox.Text, out intValue)) && (form.MaxRoomTextBox.Text == "" || int.TryParse(form.MaxRoomTextBox.Text, out intValue)) && (form.MinFloorTextBox.Text == "" || int.TryParse(form.MinFloorTextBox.Text, out intValue)) && (form.MaxFloorTextBox.Text == "" || int.TryParse(form.MaxFloorTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                        {
                            Need need = new Need();
                            int needId = db.NeedSet.Max(est1 => est1.Id) + 1;
                            need.Id = needId;
                            need.Type = "Apartment";
                            need.Adress = form.CityTextBox.Text;
                            if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                            else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                            if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                            else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                            need.IdClient = selectedClient.Id;
                            need.IdAgent = selectedAgent.Id;
                            db.NeedSet.Add(need);

                            ApartmentNeed apart = new ApartmentNeed();
                            int apartId = db.ApartmentNeedSet.Max(ap => ap.Id) + 1;
                            apart.Id = apartId;
                            if (form.MaxRoomTextBox.Text == "") { apart.MaxRoom = null; }
                            else { apart.MaxRoom = Convert.ToInt32(form.MaxRoomTextBox.Text); }
                            if (form.MinRoomTextBox.Text == "") { apart.MinRoom = null; }
                            else { apart.MinRoom = Convert.ToInt32(form.MinRoomTextBox.Text); }

                            if (form.MaxFloorTextBox.Text == "") { apart.MaxFloor = null; }
                            else { apart.MaxFloor = Convert.ToInt32(form.MaxFloorTextBox.Text); }
                            if (form.MinFloorTextBox.Text == "") { apart.MinFloor = null; }
                            else { apart.MinFloor = Convert.ToInt32(form.MinFloorTextBox.Text); }

                            if (form.MaxSquareTextBox.Text == "") { apart.MaxArea = null; }
                            else { apart.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                            if (form.MinSquareTextBox.Text == "") { apart.MinArea = null; }
                            else { apart.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }

                            apart.IdNeed = needId;
                            db.ApartmentNeedSet.Add(apart);
                            db.SaveChanges();
                            MessageBox.Show("Новая потребность добавлена");
                        }
                        else
                        {
                            MessageBox.Show("Все минимальные и максимальные значения - это целое число");
                        }
                    }
                    else if (str == "Земля")
                    {
                        int intValue;
                        if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                        {
                            Need need = new Need();
                            int needId = db.NeedSet.Max(est1 => est1.Id) + 1;
                            need.Id = needId;
                            need.Type = "Land";
                            need.Adress = form.CityTextBox.Text;
                            if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                            else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                            if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                            else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                            need.IdClient = selectedClient.Id;
                            need.IdAgent = selectedAgent.Id;
                            db.NeedSet.Add(need);

                            LandNeed land = new LandNeed();
                            int landId = db.LandNeedSet.Max(ap => ap.Id) + 1;
                            land.Id = landId;

                            if (form.MaxSquareTextBox.Text == "") { land.MaxArea = null; }
                            else { land.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                            if (form.MinSquareTextBox.Text == "") { land.MinArea = null; }
                            else { land.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }
                            land.IdNeed = needId;
                            db.LandNeedSet.Add(land);
                            db.SaveChanges();
                            MessageBox.Show("Новая потребность добавлена");
                        }
                        else
                        {
                            MessageBox.Show("Все минимальные и максимальные значения - это целое число");
                        }
                    }
                    else if (str == "Дом")
                    {
                        int intValue;
                        if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinRoomTextBox.Text == "" || int.TryParse(form.MinRoomTextBox.Text, out intValue)) && (form.MaxRoomTextBox.Text == "" || int.TryParse(form.MaxRoomTextBox.Text, out intValue)) && (form.MinFloorTextBox.Text == "" || int.TryParse(form.MinFloorTextBox.Text, out intValue)) && (form.MaxFloorTextBox.Text == "" || int.TryParse(form.MaxFloorTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                        {
                            Need need = new Need();
                            int needId = db.NeedSet.Max(est1 => est1.Id) + 1;
                            need.Id = needId;
                            need.Type = "House";
                            need.Adress = form.CityTextBox.Text;
                            if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                            else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                            if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                            else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                            need.IdClient = selectedClient.Id;
                            need.IdAgent = selectedAgent.Id;
                            db.NeedSet.Add(need);

                            HouseNeed house = new HouseNeed();
                            int houseId = db.HouseNeedSet.Max(ap => ap.Id) + 1;
                            house.Id = houseId;
                            if (form.MaxRoomTextBox.Text == "") { house.MaxRoom = null; }
                            else { house.MaxRoom = Convert.ToInt32(form.MaxRoomTextBox.Text); }
                            if (form.MinRoomTextBox.Text == "") { house.MinRoom = null; }
                            else { house.MinRoom = Convert.ToInt32(form.MinRoomTextBox.Text); }

                            if (form.MaxFloorTextBox.Text == "") { house.MaxFloor = null; }
                            else { house.MaxFloor = Convert.ToInt32(form.MaxFloorTextBox.Text); }
                            if (form.MinFloorTextBox.Text == "") { house.MinFloor = null; }
                            else { house.MinFloor = Convert.ToInt32(form.MinFloorTextBox.Text); }

                            if (form.MaxSquareTextBox.Text == "") { house.MaxArea = null; }
                            else { house.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                            if (form.MinSquareTextBox.Text == "") { house.MinArea = null; }
                            else { house.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }
                            house.IdNeed = needId;
                            db.HouseNeedSet.Add(house);
                            db.SaveChanges();
                            MessageBox.Show("Новая потребность добавлена");
                        }
                        else
                        {
                            MessageBox.Show("Все минимальные и максимальные значения - это целое число");
                        }
                    }
                    UpdateDataGrid();
                }
                else { MessageBox.Show("Выберите тип объекта, клиента, риэлтора"); }
            }
        }

        private void EditNeed_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxRoom", "");
                text = text.Replace("MinRoom", "");
                text = text.Replace("MaxFloor", "");
                text = text.Replace("MinFloor", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));
                Client client = db.ClientSet.Find(Convert.ToInt32(word[5]));
                Agent agent = db.AgentSet.Find(Convert.ToInt32(word[6]));

                Needs form = new Needs(need, 1);
                form.TypeObject.IsEnabled = false;
                form.TypeObject.SelectedItem = form.ComboBoxApartment;
                form.SelectedClient = client;
                form.SelectedAgent = agent;

                form.MaxPriceTextBox.Text = word[3];
                form.MinPriceTextBox.Text = word[4];
                form.MaxRoomTextBox.Text = word[7];
                form.MinRoomTextBox.Text = word[8];
                form.MaxFloorTextBox.Text = word[9];
                form.MinFloorTextBox.Text = word[10];
                form.MaxSquareTextBox.Text = word[11];
                form.MinSquareTextBox.Text = word[12];

                if (form.ShowDialog() == true)
                {
                    int intValue;
                    var selectedClient = (Client)form.Clients.SelectedItem;
                    var selectedAgent = (Agent)form.Rieltors.SelectedItem;
                    if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinRoomTextBox.Text == "" || int.TryParse(form.MinRoomTextBox.Text, out intValue)) && (form.MaxRoomTextBox.Text == "" || int.TryParse(form.MaxRoomTextBox.Text, out intValue)) && (form.MinFloorTextBox.Text == "" || int.TryParse(form.MinFloorTextBox.Text, out intValue)) && (form.MaxFloorTextBox.Text == "" || int.TryParse(form.MaxFloorTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                    {
                        need.Adress = form.CityTextBox.Text;
                        if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                        else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                        if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                        else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                        need.IdClient = selectedClient.Id;
                        need.IdAgent = selectedAgent.Id;

                        ApartmentNeed apart = db.ApartmentNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);
                        if (form.MaxRoomTextBox.Text == "") { apart.MaxRoom = null; }
                        else { apart.MaxRoom = Convert.ToInt32(form.MaxRoomTextBox.Text); }
                        if (form.MinRoomTextBox.Text == "") { apart.MinRoom = null; }
                        else { apart.MinRoom = Convert.ToInt32(form.MinRoomTextBox.Text); }

                        if (form.MaxFloorTextBox.Text == "") { apart.MaxFloor = null; }
                        else { apart.MaxFloor = Convert.ToInt32(form.MaxFloorTextBox.Text); }
                        if (form.MinFloorTextBox.Text == "") { apart.MinFloor = null; }
                        else { apart.MinFloor = Convert.ToInt32(form.MinFloorTextBox.Text); }

                        if (form.MaxSquareTextBox.Text == "") { apart.MaxArea = null; }
                        else { apart.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                        if (form.MinSquareTextBox.Text == "") { apart.MinArea = null; }
                        else { apart.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }
                        db.SaveChanges();

                        UpdateDataGrid();
                        MessageBox.Show("Успешно изменено");
                    }
                    else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                }
                dataGrid.UnselectAll();
            }

            if (dataGrid2.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid2.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });
                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));
                Client client = db.ClientSet.Find(Convert.ToInt32(word[5]));
                Agent agent = db.AgentSet.Find(Convert.ToInt32(word[6]));

                Needs form = new Needs(need, 1);
                form.TypeObject.IsEnabled = false;
                form.TypeObject.SelectedItem = form.ComboBoxLand;
                form.SelectedClient = client;
                form.SelectedAgent = agent;

                form.MaxPriceTextBox.Text = word[3];
                form.MinPriceTextBox.Text = word[4];
                form.MaxSquareTextBox.Text = word[7];
                form.MinSquareTextBox.Text = word[8];

                if (form.ShowDialog() == true)
                {
                    int intValue;
                    var selectedClient = (Client)form.Clients.SelectedItem;
                    var selectedAgent = (Agent)form.Rieltors.SelectedItem;
                    if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinRoomTextBox.Text == "" || int.TryParse(form.MinRoomTextBox.Text, out intValue)) && (form.MaxRoomTextBox.Text == "" || int.TryParse(form.MaxRoomTextBox.Text, out intValue)) && (form.MinFloorTextBox.Text == "" || int.TryParse(form.MinFloorTextBox.Text, out intValue)) && (form.MaxFloorTextBox.Text == "" || int.TryParse(form.MaxFloorTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                    {
                        need.Adress = form.CityTextBox.Text;
                        if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                        else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                        if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                        else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                        need.IdClient = selectedClient.Id;
                        need.IdAgent = selectedAgent.Id;

                        LandNeed apart = db.LandNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);
                        if (form.MaxSquareTextBox.Text == "") { apart.MaxArea = null; }
                        else { apart.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                        if (form.MinSquareTextBox.Text == "") { apart.MinArea = null; }
                        else { apart.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }
                        db.SaveChanges();

                        UpdateDataGrid();
                        MessageBox.Show("Успешно изменено");
                    }
                    else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                }
                dataGrid2.UnselectAll();
            }

            if (dataGrid3.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid3.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxRoom", "");
                text = text.Replace("MinRoom", "");
                text = text.Replace("MaxFloor", "");
                text = text.Replace("MinFloor", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));
                Client client = db.ClientSet.Find(Convert.ToInt32(word[5]));
                Agent agent = db.AgentSet.Find(Convert.ToInt32(word[6]));

                Needs form = new Needs(need, 1);
                form.TypeObject.IsEnabled = false;
                form.TypeObject.SelectedItem = form.ComboBoxHouse;
                form.SelectedClient = client;
                form.SelectedAgent = agent;

                form.MaxPriceTextBox.Text = word[3];
                form.MinPriceTextBox.Text = word[4];
                form.MaxRoomTextBox.Text = word[7];
                form.MinRoomTextBox.Text = word[8];
                form.MaxFloorTextBox.Text = word[9];
                form.MinFloorTextBox.Text = word[10];
                form.MaxSquareTextBox.Text = word[11];
                form.MinSquareTextBox.Text = word[12];

                if (form.ShowDialog() == true)
                {
                    int intValue;
                    var selectedClient = (Client)form.Clients.SelectedItem;
                    var selectedAgent = (Agent)form.Rieltors.SelectedItem;
                    if (form.MinPriceTextBox.Text == "" || int.TryParse(form.MinPriceTextBox.Text, out intValue) && (form.MaxPriceTextBox.Text == "" || int.TryParse(form.MaxPriceTextBox.Text, out intValue)) && (form.MinRoomTextBox.Text == "" || int.TryParse(form.MinRoomTextBox.Text, out intValue)) && (form.MaxRoomTextBox.Text == "" || int.TryParse(form.MaxRoomTextBox.Text, out intValue)) && (form.MinFloorTextBox.Text == "" || int.TryParse(form.MinFloorTextBox.Text, out intValue)) && (form.MaxFloorTextBox.Text == "" || int.TryParse(form.MaxFloorTextBox.Text, out intValue)) && (form.MinSquareTextBox.Text == "" || int.TryParse(form.MinSquareTextBox.Text, out intValue)) && (form.MaxSquareTextBox.Text == "" || int.TryParse(form.MaxSquareTextBox.Text, out intValue)))
                    {
                        need.Adress = form.CityTextBox.Text;
                        if (form.MaxPriceTextBox.Text == "") { need.MaxPrice = null; }
                        else { need.MaxPrice = Convert.ToInt32(form.MaxPriceTextBox.Text); }
                        if (form.MinPriceTextBox.Text == "") { need.MinPrice = null; }
                        else { need.MinPrice = Convert.ToInt32(form.MinPriceTextBox.Text); }
                        need.IdClient = selectedClient.Id;
                        need.IdAgent = selectedAgent.Id;

                        HouseNeed house = db.HouseNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);
                        if (form.MaxRoomTextBox.Text == "") { house.MaxRoom = null; }
                        else { house.MaxRoom = Convert.ToInt32(form.MaxRoomTextBox.Text); }
                        if (form.MinRoomTextBox.Text == "") { house.MinRoom = null; }
                        else { house.MinRoom = Convert.ToInt32(form.MinRoomTextBox.Text); }

                        if (form.MaxFloorTextBox.Text == "") { house.MaxFloor = null; }
                        else { house.MaxFloor = Convert.ToInt32(form.MaxFloorTextBox.Text); }
                        if (form.MinFloorTextBox.Text == "") { house.MinFloor = null; }
                        else { house.MinFloor = Convert.ToInt32(form.MinFloorTextBox.Text); }

                        if (form.MaxSquareTextBox.Text == "") { house.MaxArea = null; }
                        else { house.MaxArea = Convert.ToInt32(form.MaxSquareTextBox.Text); }
                        if (form.MinSquareTextBox.Text == "") { house.MinArea = null; }
                        else { house.MinArea = Convert.ToInt32(form.MinSquareTextBox.Text); }
                        db.SaveChanges();

                        UpdateDataGrid();
                        MessageBox.Show("Успешно изменено");
                    }
                    else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                }
                dataGrid3.UnselectAll();
            }
        }

        private void DeleteNeed_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxRoom", "");
                text = text.Replace("MinRoom", "");
                text = text.Replace("MaxFloor", "");
                text = text.Replace("MinFloor", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));

                ApartmentNeed apart = db.ApartmentNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);
                if (hasDeals)
                {
                    MessageBox.Show("Нельзя удалить потребность, связанный со сделкой.");
                }
                else
                {
                    db.NeedSet.Remove(need);
                    db.ApartmentNeedSet.Remove(apart);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Успешно удалено");
                }
            }
            if (dataGrid2.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid2.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxRoom", "");
                text = text.Replace("MinRoom", "");
                text = text.Replace("MaxFloor", "");
                text = text.Replace("MinFloor", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));
                LandNeed land = db.LandNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);
                if (hasDeals)
                {
                    MessageBox.Show("Нельзя удалить потребность, связанный со сделкой.");
                }
                else
                {
                    db.NeedSet.Remove(need);
                    db.LandNeedSet.Remove(land);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Успешно удалено");
                }
            }
            if (dataGrid3.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid3.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("MinPrice", "");
                text = text.Replace("MaxPrice", "");
                text = text.Replace("Client", "");
                text = text.Replace("Agent", "");
                text = text.Replace("MaxRoom", "");
                text = text.Replace("MinRoom", "");
                text = text.Replace("MaxFloor", "");
                text = text.Replace("MinFloor", "");
                text = text.Replace("MaxArea", "");
                text = text.Replace("MinArea", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Need need = db.NeedSet.Find(Convert.ToInt32(word[0]));

                HouseNeed house = db.HouseNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);
                if (hasDeals)
                {
                    MessageBox.Show("Нельзя удалить потребность, связанный со сделкой.");
                }
                else
                {
                    db.NeedSet.Remove(need);
                    db.HouseNeedSet.Remove(house);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Успешно удалено");
                }
            }
        }

        private void CreateDeal_Click(object sender, RoutedEventArgs e)
        {
            var selectedOffer = (Offer)OfferComBox.SelectedItem;
            Deal deal = new Deal();
            int maxId = db.DealSet.Max(of => of.Id) + 1;
            deal.Id = maxId;
            deal.IdNeed = needid;
            deal.IdOffer = selectedOffer.Id;
            db.DealSet.Add(deal);
            db.SaveChanges();
            MessageBox.Show("Сделка создана успешно!");
            OfferComBox.Items.Clear();
            OfferComBox.SelectedItem = null;
            CreateDeal.IsEnabled = false;
            dataGrid.UnselectAll();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                OfferComBox.Items.Clear();
                dynamic selectedRow = dataGrid.SelectedItem;

                if (selectedRow != null)
                {
                    int id = selectedRow.Id;
                    Need need = db.NeedSet.Find(id);
                    needid = need.Id;

                    string type = selectedRow.Type;
                    int? minprice, maxprice, minroom, maxroom, minfloor, maxfloor;
                    double? minarea, maxarea;

                    if (selectedRow.MinPrice == null) { minprice = null; }
                    else { minprice = Convert.ToInt32(selectedRow.MinPrice); }
                    if (selectedRow.MaxPrice == null) { maxprice = null; }
                    else { maxprice = Convert.ToInt32(selectedRow.MaxPrice); }

                    if (selectedRow.MinRoom == null) { minroom = null; }
                    else { minroom = Convert.ToInt32(selectedRow.MinRoom); }
                    if (selectedRow.MaxRoom == null) { maxroom = null; }
                    else { maxroom = Convert.ToInt32(selectedRow.MaxRoom); }

                    if (selectedRow.MinFloor == null) { minfloor = null; }
                    else { minfloor = Convert.ToInt32(selectedRow.MinFloor); }
                    if (selectedRow.MaxFloor == null) { maxfloor = null; }
                    else { maxfloor = Convert.ToInt32(selectedRow.MaxFloor); }

                    if (selectedRow.MinArea == null) { minarea = null; }
                    else { minarea = Convert.ToDouble(selectedRow.MinArea); }
                    if (selectedRow.MaxArea == null) { maxarea = null; }
                    else { maxarea = Convert.ToDouble(selectedRow.MaxArea); }

                    ApartmentNeed apart = db.ApartmentNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                    bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);

                    if (!hasDeals)
                    {
                        foreach (var offer in db.OfferSet)
                        {
                            foreach (var estate in db.EstateSet)
                            {
                                if (estate.Id == offer.IdEstate && estate.Type == type)
                                {
                                    Apartment apartm = db.ApartmentSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                    int price = offer.Price;
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

                                    bool exists2 = db.DealSet.Any(d => d.IdOffer == offer.Id);

                                    if (minPrice && maxPrice && minRoom && maxRoom && minFloor && maxFloor && minArea && maxArea && !exists2)
                                    {
                                        OfferComBox.Items.Add(offer);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid2.SelectedItems.Count == 1)
            {
                OfferComBox.Items.Clear();
                dynamic selectedRow = dataGrid2.SelectedItem;

                if (selectedRow != null)
                {
                    int id = selectedRow.Id;
                    Need need = db.NeedSet.Find(id);
                    needid = need.Id;

                    string type = selectedRow.Type;
                    int? minprice, maxprice, minroom, maxroom, minfloor, maxfloor;
                    double? minarea, maxarea;

                    if (selectedRow.MinPrice == null) { minprice = null; }
                    else { minprice = Convert.ToInt32(selectedRow.MinPrice); }
                    if (selectedRow.MaxPrice == null) { maxprice = null; }
                    else { maxprice = Convert.ToInt32(selectedRow.MaxPrice); }

                    if (selectedRow.MinArea == null) { minarea = null; }
                    else { minarea = Convert.ToDouble(selectedRow.MinArea); }
                    if (selectedRow.MaxArea == null) { maxarea = null; }
                    else { maxarea = Convert.ToDouble(selectedRow.MaxArea); }

                    LandNeed land = db.LandNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                    bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);

                    if (!hasDeals)
                    {
                        foreach (var offer in db.OfferSet)
                        {
                            foreach (var estate in db.EstateSet)
                            {
                                if (estate.Id == offer.IdEstate && estate.Type == type)
                                {
                                    Land lan = db.LandSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                    int price = offer.Price;
                                    double? area = null;
                                    if (lan.Area != null) { area = (double)lan.Area; }

                                    bool minPrice = need.MinPrice == null || need.MinPrice <= price;
                                    bool maxPrice = need.MaxPrice == null || need.MaxPrice >= price;
                                    bool minArea = land.MinArea == null || (area != null && land.MinArea <= area);
                                    bool maxArea = land.MaxArea == null || (area != null && land.MaxArea >= area);

                                    bool exists2 = db.DealSet.Any(d => d.IdNeed == need.Id);

                                    if (minPrice && maxPrice && minArea && maxArea && !exists2)
                                    {
                                        OfferComBox.Items.Add(offer);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid3.SelectedItems.Count == 1)
            {
                OfferComBox.Items.Clear();
                dynamic selectedRow = dataGrid3.SelectedItem;

                if (selectedRow != null)
                {
                    int id = selectedRow.Id;
                    Need need = db.NeedSet.Find(id);
                    needid = need.Id;

                    string type = selectedRow.Type;
                    int? minprice, maxprice, minroom, maxroom, minfloor, maxfloor;
                    double? minarea, maxarea;

                    if (selectedRow.MinPrice == null) { minprice = null; }
                    else { minprice = Convert.ToInt32(selectedRow.MinPrice); }
                    if (selectedRow.MaxPrice == null) { maxprice = null; }
                    else { maxprice = Convert.ToInt32(selectedRow.MaxPrice); }

                    if (selectedRow.MinRoom == null) { minroom = null; }
                    else { minroom = Convert.ToInt32(selectedRow.MinRoom); }
                    if (selectedRow.MaxRoom == null) { maxroom = null; }
                    else { maxroom = Convert.ToInt32(selectedRow.MaxRoom); }

                    if (selectedRow.MinFloor == null) { minfloor = null; }
                    else { minfloor = Convert.ToInt32(selectedRow.MinFloor); }
                    if (selectedRow.MaxFloor == null) { maxfloor = null; }
                    else { maxfloor = Convert.ToInt32(selectedRow.MaxFloor); }

                    if (selectedRow.MinArea == null) { minarea = null; }
                    else { minarea = Convert.ToDouble(selectedRow.MinArea); }
                    if (selectedRow.MaxArea == null) { maxarea = null; }
                    else { maxarea = Convert.ToDouble(selectedRow.MaxArea); }

                    HouseNeed house = db.HouseNeedSet.FirstOrDefault(a => a.IdNeed == need.Id);

                    bool hasDeals = db.DealSet.Any(deal => deal.IdNeed == need.Id);

                    if (!hasDeals)
                    {
                        foreach (var offer in db.OfferSet)
                        {
                            foreach (var estate in db.EstateSet)
                            {
                                if (estate.Id == offer.IdEstate && estate.Type == type)
                                {
                                    House hous = db.HouseSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                    int price = offer.Price;
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
                                        OfferComBox.Items.Add(offer);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Offer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateDeal.IsEnabled = true;
        }
    }
}
