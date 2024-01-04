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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для RieltorPage.xaml
    /// </summary>
    public partial class RieltorPage : Page
    {
        EntityModelContainer db = new EntityModelContainer();
        public RieltorPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = db.AgentSet.ToList();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Agent; // Замените YourDataType на тип данных, который вы используете в DataGrid
                if (selectedRow != null)
                {
                    int id = selectedRow.Id;
                    var matchingNeeds = db.NeedSet.Where(need => need.IdAgent == id).ToList();
                    dataGrid2.ItemsSource = matchingNeeds;
                    var matchingOffers = db.OfferSet.Where(offer => offer.IdAgent == id).ToList();
                    dataGrid3.ItemsSource = matchingOffers;
                }
            }
        }
        private void AddRieltor_Click(object sender, RoutedEventArgs e)
        {
            Rieltors form = new Rieltors(new Agent());
            if (form.ShowDialog() == true)
            {
                if (form.SurnameTextBox.Text != "" && form.NameTextBox.Text != "" && form.PatronymicTextBox.Text != "" && form.KomTextBox.Text != "")
                {
                    int intValue;
                    if(int.TryParse(form.KomTextBox.Text, out intValue))
                    {
                        Agent agent = new Agent();
                        int maxId = db.AgentSet.Max(clientt => clientt.Id) + 1;
                        agent.Id = maxId;
                        agent.Surname = form.SurnameTextBox.Text;
                        agent.Name = form.NameTextBox.Text;
                        agent.LastName = form.PatronymicTextBox.Text;
                        agent.Comission = Convert.ToInt32(form.KomTextBox.Text);
                        db.AgentSet.Add(agent);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.AgentSet.ToList();
                        MessageBox.Show("Новый риэлтор добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Комиссия - это целое число");
                    }
                    
                }
                else { MessageBox.Show("Все поля обязательны к заполнению"); }
            }
        }

        private void EditRieltor_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Agent;
                if (selectedRow != null)
                {
                    Agent agent = db.AgentSet.Find(selectedRow.Id);
                    Rieltors form = new Rieltors(agent);
                    form.SurnameTextBox.Text = selectedRow.Surname;
                    form.NameTextBox.Text = selectedRow.Name;
                    form.PatronymicTextBox.Text = selectedRow.LastName;
                    form.KomTextBox.Text = Convert.ToString(selectedRow.Comission);
                    if (form.ShowDialog() == true)
                    {
                        if (form.SurnameTextBox.Text != "" && form.NameTextBox.Text != "" && form.PatronymicTextBox.Text != "" && form.KomTextBox.Text != "")
                        {
                            int intValue;
                            if (int.TryParse(form.KomTextBox.Text, out intValue))
                            {
                                agent.Surname = form.SurnameTextBox.Text;
                                agent.Name = form.NameTextBox.Text;
                                agent.LastName = form.PatronymicTextBox.Text;
                                agent.Comission = Convert.ToInt32(form.KomTextBox.Text);
                                db.SaveChanges();
                                dataGrid.ItemsSource = db.AgentSet.ToList();
                                MessageBox.Show("Успешно изменено");
                            }
                            else
                            {
                                MessageBox.Show("Комиссия - это целое число");
                            }
                        }
                        else { MessageBox.Show("Все поля обязательны к заполнению"); }
                    }
                }
            }
        }

        private void DeleteRieltor_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = dataGrid.SelectedItem as Agent;
                if (selectedRow != null)
                {
                    int agentId = selectedRow.Id;

                    bool hasNeeds = db.NeedSet.Any(need => need.IdAgent == agentId);
                    bool hasOffers = db.OfferSet.Any(offer => offer.IdAgent == agentId);

                    if (hasNeeds || hasOffers)
                    {
                        MessageBox.Show("Нельзя удалить риэлтора, связанного с потребностью или предложением.");
                    }
                    else
                    {
                        Agent agent = db.AgentSet.Find(selectedRow.Id);
                        db.AgentSet.Remove(agent);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.AgentSet.ToList();
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
                var allAgents = db.AgentSet.ToList();
                var matchingAgents = allAgents.Where(agent => LevenshteinDistance(agent.Surname + " " + agent.Name + " " + agent.LastName, searchTerm) <= 3).ToList();
                dataGrid.ItemsSource = matchingAgents;
            }
            else
            {
                dataGrid.ItemsSource = db.AgentSet.ToList();
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
    }
}
