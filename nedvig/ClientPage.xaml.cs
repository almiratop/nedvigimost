using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        
        EntityModelContainer db = new EntityModelContainer();

        public ClientPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = db.ClientSet.ToList();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Client; // Замените YourDataType на тип данных, который вы используете в DataGrid
                if (selectedRow != null)
                {
                    int id = selectedRow.Id;
                    var matchingNeeds = db.NeedSet.Where(need => need.IdClient == id).ToList();
                    dataGrid2.ItemsSource = matchingNeeds;
                    var matchingOffers = db.OfferSet.Where(offer => offer.IdClient == id).ToList();
                    dataGrid3.ItemsSource = matchingOffers;
                }
            }
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
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            Clients form = new Clients(new Client());
            if (form.ShowDialog() == true)
            {
                if (form.PhoneNumberTextBox.Text != "" || form.EmailTextBox.Text != "")
                {
                    Client client = new Client();
                    int maxId = db.ClientSet.Max(clientt => clientt.Id) + 1;
                    client.Id = maxId;
                    client.Surname = form.SurnameTextBox.Text;
                    client.Name = form.NameTextBox.Text;
                    client.LastName = form.PatronymicTextBox.Text;
                    client.Number = form.PhoneNumberTextBox.Text;
                    client.Mail = form.EmailTextBox.Text;
                    db.ClientSet.Add(client);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.ClientSet.ToList();
                    MessageBox.Show("Новый клиент добавлен");
                }
                else { MessageBox.Show("Номер телефона или почта - обязательно к заполнению"); }
            }
        }

        private void EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Client;
                if (selectedRow != null)
                {
                    Client client = db.ClientSet.Find(selectedRow.Id);
                    Clients form = new Clients(client);
                    form.SurnameTextBox.Text = selectedRow.Surname;
                    form.NameTextBox.Text = selectedRow.Name;
                    form.PatronymicTextBox.Text = selectedRow.LastName;
                    form.PhoneNumberTextBox.Text = selectedRow.Number;
                    form.EmailTextBox.Text = selectedRow.Mail;
                    if (form.ShowDialog() == true)
                    {
                        if (form.PhoneNumberTextBox.Text != "" || form.EmailTextBox.Text != "")
                        {
                            client.Surname = form.SurnameTextBox.Text;
                            client.Name = form.NameTextBox.Text;
                            client.LastName = form.PatronymicTextBox.Text;
                            client.Number = form.PhoneNumberTextBox.Text;
                            client.Mail = form.EmailTextBox.Text;
                            db.SaveChanges();
                            dataGrid.ItemsSource = db.ClientSet.ToList();
                            MessageBox.Show("Успешно изменено");
                        }
                        else { MessageBox.Show("Номер телефона или почта - обязательно к заполнению"); }
                    }
                }
            }
        }

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Client;
                if (selectedRow != null)
                {
                    int clientId = selectedRow.Id;

                    bool hasNeeds = db.NeedSet.Any(need => need.IdClient == clientId);
                    bool hasOffers = db.OfferSet.Any(offer => offer.IdClient == clientId);

                    if (hasNeeds || hasOffers)
                    {
                        MessageBox.Show("Нельзя удалить клиента, связанного с потребностью или предложением.");
                    }
                    else
                    {
                        Client client = db.ClientSet.Find(clientId);
                        db.ClientSet.Remove(client);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.ClientSet.ToList();
                        MessageBox.Show("Успешно удалено");
                    }
                }
            }
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = FindTextBox.Text;
            if (searchTerm != "")
            {
                var allClients = db.ClientSet.ToList();
                var matchingClients = allClients.Where(client => LevenshteinDistance(client.Surname + " " + client.Name + " " + client.LastName, searchTerm) <= 3).ToList();
                dataGrid.ItemsSource = matchingClients;
            }
            else
            {
                dataGrid.ItemsSource = db.ClientSet.ToList();
            }
        }
    }
}
