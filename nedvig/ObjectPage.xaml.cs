using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для ObjectPage.xaml
    /// </summary>
    public partial class ObjectPage : Page
    {
        EntityModelContainer db = new EntityModelContainer();
        public ObjectPage()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            var query = from estate in db.EstateSet
                        join apartment in db.ApartmentSet on estate.Id equals apartment.IdEstate
                        select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, apartment.Room, apartment.Floor, apartment.Area };
            dataGrid.ItemsSource = query.ToList();

            var query2 = from estate in db.EstateSet
                         join land in db.LandSet on estate.Id equals land.IdEstate
                         select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, land.Area };
            dataGrid2.ItemsSource = query2.ToList();

            var query3 = from estate in db.EstateSet
                         join house in db.HouseSet on estate.Id equals house.IdEstate
                         select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, house.Room, house.Floor, house.Area };
            dataGrid3.ItemsSource = query3.ToList();
        }

        private void AddObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Objects form = new Objects(new Estate());
                if (form.ShowDialog() == true)
                {
                    if (form.combox.SelectedItem != null)
                    {
                        ComboBoxItem cbi = (ComboBoxItem)form.combox.SelectedItem;
                        string str = cbi.Content.ToString();
                        if (str == "Квартира")
                        {
                            int intValue;
                            double value;
                            if (form.RoomTextBox.Text == "" || int.TryParse(form.RoomTextBox.Text, out intValue) && (form.FloorTextBox.Text == "" || int.TryParse(form.FloorTextBox.Text, out intValue)) && form.SquareTextBox.Text == "" || double.TryParse(form.SquareTextBox.Text, out value))
                            {
                                Estate est = new Estate();
                                int estId = db.EstateSet.Max(est1 => est1.Id) + 1;
                                est.Id = estId;
                                est.Type = "Apartment";
                                est.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                                est.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;
                                db.EstateSet.Add(est);

                                Apartment apart = new Apartment();
                                int apartId = db.ApartmentSet.Max(ap => ap.Id) + 1;
                                apart.Id = apartId;
                                if (form.RoomTextBox.Text == "") { apart.Room = null; }
                                else { apart.Room = Convert.ToInt32(form.RoomTextBox.Text); }
                                if (form.FloorTextBox.Text == "") { apart.Floor = null; }
                                else { apart.Floor = Convert.ToInt32(form.FloorTextBox.Text); }
                                if (form.SquareTextBox.Text == "") { apart.Area = null; }
                                else { apart.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                                apart.IdEstate = estId;
                                db.ApartmentSet.Add(apart);
                                db.SaveChanges();
                                MessageBox.Show("Новая квартира добавлена");
                            }
                            else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                        }
                        else if (str == "Земля")
                        {
                            double value;
                            if (form.SquareTextBox.Text == "" || double.TryParse(form.SquareTextBox.Text, out value))
                            {
                                Estate est = new Estate();
                                int estId = db.EstateSet.Max(est2 => est2.Id) + 1;
                                est.Id = estId;
                                est.Type = "Land";
                                est.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                                est.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;
                                db.EstateSet.Add(est);

                                Land land = new Land();
                                int landId = db.LandSet.Max(lan => lan.Id) + 1;
                                land.Id = landId;
                                if (form.SquareTextBox.Text == "") { land.Area = null; }
                                else { land.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                                land.IdEstate = estId;
                                db.LandSet.Add(land);
                                db.SaveChanges();
                                MessageBox.Show("Новая земля добавлена");
                            }
                        }
                        else if (str == "Дом")
                        {
                            int intValue;
                            double value;
                            if (form.RoomTextBox.Text == "" || int.TryParse(form.RoomTextBox.Text, out intValue) && (form.FloorTextBox.Text == "" || int.TryParse(form.FloorTextBox.Text, out intValue)) && form.SquareTextBox.Text == "" || double.TryParse(form.SquareTextBox.Text, out value))
                            {
                                Estate est = new Estate();
                                int estId = db.EstateSet.Max(est3 => est3.Id) + 1;
                                est.Id = estId;
                                est.Type = "House";
                                est.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                                est.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;
                                db.EstateSet.Add(est);
                                db.SaveChanges();

                                House house = new House();
                                int houseId = db.HouseSet.Max(hous => hous.Id) + 1;
                                house.Id = houseId;
                                if (form.RoomTextBox.Text == "") { house.Room = null; }
                                else { house.Room = Convert.ToInt32(form.RoomTextBox.Text); }
                                if (form.FloorTextBox.Text == "") { house.Floor = null; }
                                else { house.Floor = Convert.ToInt32(form.FloorTextBox.Text); }
                                if (form.SquareTextBox.Text == "") { house.Area = null; }
                                else { house.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                                house.IdEstate = estId;
                                db.HouseSet.Add(house);
                                MessageBox.Show("Новый дом добавлен");
                                db.SaveChanges();
                            }
                            else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                        }
                        UpdateDataGrid();
                    }
                    else { MessageBox.Show("Выберите тип объекта"); }
                }
            }
            catch
            {
                MessageBox.Show("Площадь вводите в формате 20,5. Целая и дробная часть отделяется ЗАПЯТОЙ!");
            }
            
        }

        private void EditObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItems.Count == 1)
                {
                    var selectedRow = dataGrid.SelectedItem as dynamic;

                    if (selectedRow != null)
                    {
                        var id = selectedRow.Id;
                        var type = selectedRow.Type;
                        var adress = selectedRow.Adress;
                        var coordinate = selectedRow.Coordinate;
                        var room = selectedRow.Room;
                        var floor = selectedRow.Floor;
                        var area = selectedRow.Area;

                        string[] adr = adress.Split(new char[] { ',' });
                        string[] cor = coordinate.Split(new char[] { ',' });

                        Estate estate = db.EstateSet.Find(id);

                        Objects form = new Objects(estate);
                        form.combox.IsEnabled = false;
                        form.combox.SelectedItem = form.ComboBoxApartment;
                        form.CityTextBox.Text = adr[0];
                        form.StreetNumberTextBox.Text = adr[1];
                        form.HouseNumberTextBox.Text = adr[2];
                        form.ApartmentNumberTextBox.Text = adr[3];
                        form.LongitudeTextBox.Text = cor[0];
                        form.LatitudeTextBox.Text = cor[1];
                        if (room == null) { form.RoomTextBox.Text = ""; }
                        else { form.RoomTextBox.Text = Convert.ToString(room); }
                        if (floor == null) { form.FloorTextBox.Text = ""; }
                        else { form.FloorTextBox.Text = Convert.ToString(floor); }
                        if (area == null) { form.SquareTextBox.Text = ""; }
                        else { form.SquareTextBox.Text = Convert.ToString(area); }

                        if (form.ShowDialog() == true)
                        {
                            int intValue;
                            double value;
                            if (form.RoomTextBox.Text == "" || int.TryParse(form.RoomTextBox.Text, out intValue) && form.FloorTextBox.Text == "" || int.TryParse(form.FloorTextBox.Text, out intValue) && form.SquareTextBox.Text == "" || double.TryParse(form.SquareTextBox.Text, out value))
                            {
                                estate.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                                estate.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;

                                Apartment apart = db.ApartmentSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                if (form.RoomTextBox.Text == "") { apart.Room = null; }
                                else { apart.Room = Convert.ToInt32(form.RoomTextBox.Text); }
                                if (form.FloorTextBox.Text == "") { apart.Floor = null; }
                                else { apart.Floor = Convert.ToInt32(form.FloorTextBox.Text); }
                                if (form.SquareTextBox.Text == "") { apart.Area = null; }
                                else { apart.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                                db.SaveChanges();

                                UpdateDataGrid();
                                MessageBox.Show("Успешно изменено");
                            }
                            else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                        }
                    }

                    dataGrid.UnselectAll();
                }

                if (dataGrid2.SelectedItems.Count == 1)
                {
                    var selectedRow = dataGrid2.SelectedItem as dynamic;

                    if (selectedRow != null)
                    {
                        var id = selectedRow.Id;
                        var type = selectedRow.Type;
                        var adress = selectedRow.Adress;
                        var coordinate = selectedRow.Coordinate;
                        var area = selectedRow.Area;

                        string[] adr = adress.Split(new char[] { ',' });
                        string[] cor = coordinate.Split(new char[] { ',' });

                        Estate estate = db.EstateSet.Find(id);

                        Objects form = new Objects(estate);
                        form.combox.IsEnabled = false;
                        form.combox.SelectedItem = form.ComboBoxLand;
                        form.CityTextBox.Text = adr[0];
                        form.StreetNumberTextBox.Text = adr[1];
                        form.HouseNumberTextBox.Text = adr[2];
                        form.ApartmentNumberTextBox.Text = adr[3];
                        form.LongitudeTextBox.Text = cor[0];
                        form.LatitudeTextBox.Text = cor[1];
                        if (area == null) { form.SquareTextBox.Text = ""; }
                        else { form.SquareTextBox.Text = Convert.ToString(area); }

                        if (form.ShowDialog() == true)
                        {
                            estate.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                            estate.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;

                            Land land = db.LandSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                            if (form.SquareTextBox.Text == "") { land.Area = null; }
                            else { land.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                            db.SaveChanges();
                            UpdateDataGrid();
                            MessageBox.Show("Успешно изменено");
                        }
                        dataGrid2.UnselectAll();
                    }
                }

                if (dataGrid3.SelectedItems.Count == 1)
                {
                    var selectedRow = dataGrid3.SelectedItem as dynamic;

                    if (selectedRow != null)
                    {
                        var id = selectedRow.Id;
                        var type = selectedRow.Type;
                        var adress = selectedRow.Adress;
                        var coordinate = selectedRow.Coordinate;
                        var room = selectedRow.Room;
                        var floor = selectedRow.Floor;
                        var area = selectedRow.Area;

                        string[] adr = adress.Split(new char[] { ',' });
                        string[] cor = coordinate.Split(new char[] { ',' });

                        Estate estate = db.EstateSet.Find(id);

                        Objects form = new Objects(estate);
                        form.combox.IsEnabled = false;
                        form.combox.SelectedItem = form.ComboBoxHouse;
                        form.CityTextBox.Text = adr[0];
                        form.StreetNumberTextBox.Text = adr[1];
                        form.HouseNumberTextBox.Text = adr[2];
                        form.ApartmentNumberTextBox.Text = adr[3];
                        form.LongitudeTextBox.Text = cor[0];
                        form.LatitudeTextBox.Text = cor[1];
                        if (room == null) { form.RoomTextBox.Text = ""; }
                        else { form.RoomTextBox.Text = Convert.ToString(room); }
                        if (floor == null) { form.FloorTextBox.Text = ""; }
                        else { form.FloorTextBox.Text = Convert.ToString(floor); }
                        if (area == null) { form.SquareTextBox.Text = ""; }
                        else { form.SquareTextBox.Text = Convert.ToString(area); }

                        if (form.ShowDialog() == true)
                        {
                            int intValue;
                            double value;
                            if (form.RoomTextBox.Text == "" || int.TryParse(form.RoomTextBox.Text, out intValue) && form.FloorTextBox.Text == "" || int.TryParse(form.FloorTextBox.Text, out intValue) && form.SquareTextBox.Text == "" || double.TryParse(form.SquareTextBox.Text, out value))
                            {
                                estate.Adress = form.CityTextBox.Text + "," + form.StreetNumberTextBox.Text + "," + form.HouseNumberTextBox.Text + "," + form.ApartmentNumberTextBox.Text;
                                estate.Coordinate = form.LatitudeTextBox.Text + "," + form.LongitudeTextBox.Text;

                                House house = db.HouseSet.FirstOrDefault(a => a.IdEstate == estate.Id);
                                if (form.RoomTextBox.Text == "") { house.Room = null; }
                                else { house.Room = Convert.ToInt32(form.RoomTextBox.Text); }
                                if (form.FloorTextBox.Text == "") { house.Floor = null; }
                                else { house.Floor = Convert.ToInt32(form.FloorTextBox.Text); }
                                if (form.SquareTextBox.Text == "") { house.Area = null; }
                                else { house.Area = Convert.ToDouble(form.SquareTextBox.Text); }
                                db.SaveChanges();
                                UpdateDataGrid();
                                MessageBox.Show("Успешно изменено");
                            }
                            else { MessageBox.Show("Этаж, кол-во комнат, площадь - это целое число"); }
                        }
                        dataGrid3.UnselectAll();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Площадь вводите в формате 20,5. Целая и дробная часть отделяется ЗАПЯТОЙ!");
            }

        }
        private void DeleteObject_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                string text = Convert.ToString(dataGrid.SelectedItem);
                text = text.Replace("Id", "");
                text = text.Replace("=", "");
                text = text.Replace(" ", "");
                text = text.Replace("Type", "");
                text = text.Replace("Adress", "");
                text = text.Replace("Coordinate", "");
                text = text.Replace("Room", "");
                text = text.Replace("Area", "");
                text = text.Replace("Floor", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Estate estate = db.EstateSet.Find(Convert.ToInt32(word[0]));
                Apartment apart = db.ApartmentSet.FirstOrDefault(a => a.IdEstate == estate.Id);

                bool hasOffers = db.OfferSet.Any(offer => offer.IdEstate == estate.Id);
                if (hasOffers)
                {
                    MessageBox.Show("Нельзя удалить объект, связанный с предложением.");
                }
                else
                {
                    db.EstateSet.Remove(estate);
                    db.ApartmentSet.Remove(apart);
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
                text = text.Replace("Coordinate", "");
                text = text.Replace("Area", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Estate estate = db.EstateSet.Find(Convert.ToInt32(word[0]));
                Land land = db.LandSet.FirstOrDefault(a => a.IdEstate == estate.Id);

                bool hasOffers = db.OfferSet.Any(offer => offer.IdEstate == estate.Id);
                if (hasOffers)
                {
                    MessageBox.Show("Нельзя удалить объект, связанный с предложением.");
                }
                else
                {
                    db.EstateSet.Remove(estate);
                    db.LandSet.Remove(land);
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
                text = text.Replace("Coordinate", "");
                text = text.Replace("Room", "");
                text = text.Replace("Area", "");
                text = text.Replace("Floor", "");
                text = text.Replace("{", "");
                text = text.Replace("}", "");
                string[] word = text.Split(new char[] { ',' });

                Estate estate = db.EstateSet.Find(Convert.ToInt32(word[0]));
                House house = db.HouseSet.FirstOrDefault(a => a.IdEstate == estate.Id);

                bool hasOffers = db.OfferSet.Any(offer => offer.IdEstate == estate.Id);
                if (hasOffers)
                {
                    MessageBox.Show("Нельзя удалить объект, связанный с предложением.");
                }
                else
                {
                    db.EstateSet.Remove(estate);
                    db.HouseSet.Remove(house);
                    db.SaveChanges();
                    UpdateDataGrid();
                    MessageBox.Show("Успешно удалено");
                }
            }
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();           // Получаем введенный город
            string street = StreetNumberTextBox.Text.Trim();       // Получаем введенную улицу
            string houseNumber = HouseNumberTextBox.Text.Trim(); // Получаем введенный номер дома
            string apartmentNumber = ApartmentNumberTextBox.Text.Trim(); // Получаем введенный номер квартиры

            int maxLevenshteinDistance = 3;

            var allEstates = db.EstateSet.ToList();
            var searchResults = new List<Estate>();

            foreach (var estate in allEstates)
            {
                string[] addressParts = estate.Adress.Split(',').Select(part => part.Trim()).ToArray();

                if (addressParts.Length >= 4 &&
                    LevenshteinDistance(addressParts[0], city) <= maxLevenshteinDistance &&
                    LevenshteinDistance(addressParts[1], street) <= maxLevenshteinDistance &&
                    Math.Abs(LevenshteinDistance(addressParts[2], houseNumber) - LevenshteinDistance(addressParts[3], apartmentNumber)) <= 1)
                {
                    searchResults.Add(estate);

                }
            }
            var apartmentResults = new List<object>();
            var landResults = new List<object>();
            var houseResults = new List<object>();
            foreach (var est in searchResults)
            {
                if(est.Type == "Apartment")
                {
                    var query = from estate in db.EstateSet
                                join apartment in db.ApartmentSet on estate.Id equals apartment.IdEstate
                                where estate.Id == est.Id
                                select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, apartment.Room, apartment.Floor, apartment.Area };
                    apartmentResults.AddRange(query);
                }
                if (est.Type == "Land")
                {
                    var query = from estate in db.EstateSet
                                 join land in db.LandSet on estate.Id equals land.IdEstate
                                where estate.Id == est.Id
                                select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, land.Area };
                    landResults.AddRange(query);
                }
                if (est.Type == "House")
                {
                    var query = from estate in db.EstateSet
                                 join house in db.HouseSet on estate.Id equals house.IdEstate
                                where estate.Id == est.Id
                                select new { estate.Id, estate.Type, estate.Adress, estate.Coordinate, house.Room, house.Floor, house.Area };
                    houseResults.AddRange(query);
                }
            }
            dataGrid.ItemsSource = apartmentResults;
            dataGrid2.ItemsSource = landResults;
            dataGrid3.ItemsSource = houseResults;
        }

        public static int LevenshteinDistance(string s, string t)
        {
            if (s == null || t == null)
                throw new ArgumentNullException();

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        private void break_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            CityTextBox.Text = "";
            StreetNumberTextBox.Text = "";
            HouseNumberTextBox.Text = "";
            ApartmentNumberTextBox.Text = "";
        }
    }
}
