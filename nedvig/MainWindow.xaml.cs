using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Shapes;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            MainFrame.Navigate(new main());
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += RotateIcon_Tick;
            timer.Start();
        }
       
        private void RotateIcon_Tick(object sender, EventArgs e)
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1)
            };

            RotateIcon.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }
        private void InitializeDatabase()
        {
            using (var context = new EntityModelContainer())
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                }

                if (!context.AgentSet.Any())
                {
                    context.AgentSet.Add(new Agent { Id = 1, Surname = "Фахрутдинов", Name = "Роман", LastName = "Рубинович", Comission = 20 });
                    context.AgentSet.Add(new Agent { Id = 4, Surname = "Устинов", Name = "Максим", LastName = "Алексеевич", Comission = 40 });
                    context.AgentSet.Add(new Agent { Id = 7, Surname = "Сысоева", Name = "Людмила", LastName = "Валентиновна", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 9, Surname = "Додонов", Name = "Илья", LastName = "Геннадьевич", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 11, Surname = "Мухтаруллин", Name = "Руслан", LastName = "Расыхович", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 13, Surname = "Мосеева", Name = "Любовь", LastName = "Александровна", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 15, Surname = "Киселев", Name = "Алексей", LastName = "Геннадьевич", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 19, Surname = "Клюйков", Name = "Евгений", LastName = "Николаевич", Comission = 50 });
                    context.AgentSet.Add(new Agent { Id = 24, Surname = "Жданова", Name = "Галина", LastName = "Николаевна", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 34, Surname = "Басырова", Name = "Елена", LastName = "Азатовна", Comission = 45 });
                    context.AgentSet.Add(new Agent { Id = 37, Surname = "Швецов", Name = "Виталий", LastName = "Олегович", Comission = 45 });

                    context.ClientSet.Add(new Client { Id = 2, Surname = "Семенов", Name = "Евгений", LastName = "Николаевич", Number = "32-25-55", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 3, Surname = "Денисова", Name = "Олеся", LastName = "Леонидовна", Number = "", Mail = "dummy@email.ru" });
                    context.ClientSet.Add(new Client { Id = 5, Surname = "Сафронов", Name = "Алексей", LastName = "Вячеславович", Number = "", Mail = "client@esoft.tech" });
                    context.ClientSet.Add(new Client { Id = 6, Surname = "Кудряшов", Name = "Александр", LastName = "Витальевич", Number = "551988", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 8, Surname = "Фёдоров", Name = "Алексей", LastName = "Николаевич", Number = "", Mail = "fedorov@mail.ru" });
                    context.ClientSet.Add(new Client { Id = 10, Surname = "Пелымская", Name = "Светлана", LastName = "Александровна", Number = "83452112233", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 12, Surname = "Коновальчик", Name = "Татьяна", LastName = "Геннадьевна", Number = "", Mail = "dummy@email.ru" });
                    context.ClientSet.Add(new Client { Id = 14, Surname = "Молоковская", Name = "Светлана", LastName = "Михайловна", Number = "898489848", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 16, Surname = "Моторина", Name = "Анастасия", LastName = "Сергеевна", Number = "895159848", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 17, Surname = "Поспелова", Name = "Ольга", LastName = "Александровна", Number = "", Mail = "angel@mail.ru" });
                    context.ClientSet.Add(new Client { Id = 18, Surname = "Жиляков", Name = "Владимир", LastName = "Владимирович", Number = "445588", Mail = "445588@email.ru" });
                    context.ClientSet.Add(new Client { Id = 20, Surname = "Ефремов", Name = "Владислав", LastName = "Николаевич", Number = "", Mail = "parampampam@mail.ru" });
                    context.ClientSet.Add(new Client { Id = 21, Surname = "Баль", Name = "Валентина", LastName = "Сергеевна", Number = "+7998888444", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 22, Surname = "Стрелков", Name = "Артем", LastName = "Николаевич", Number = "", Mail = "test@test.test" });
                    context.ClientSet.Add(new Client { Id = 23, Surname = "Луканин", Name = "Павел", LastName = "Валерьевич", Number = "", Mail = "foo@bar.ru" });
                    context.ClientSet.Add(new Client { Id = 25, Surname = "Шарипова", Name = "Эльвира", LastName = "Закирчановна", Number = "12345678910", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 26, Surname = "Фомина", Name = "Маргарита", LastName = "Николаевна", Number = "", Mail = "fomina@email.ru" });
                    context.ClientSet.Add(new Client { Id = 27, Surname = "Кремлев", Name = "Владислав", LastName = "Юрьевич", Number = "777", Mail = "kremlevvu@gmail.ru" });
                    context.ClientSet.Add(new Client { Id = 28, Surname = "Пономарева", Name = "Елена", LastName = "Сергеевна", Number = "", Mail = "ponomareva@gmail.ru" });
                    context.ClientSet.Add(new Client { Id = 29, Surname = "Шелест", Name = "Тамара", LastName = "Васильевна", Number = "112", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 30, Surname = "Шарипов", Name = "Рустам", LastName = "Владимирович", Number = "", Mail = "sharipov@yandex.ru" });
                    context.ClientSet.Add(new Client { Id = 31, Surname = "Романов", Name = "Сергей", LastName = "Федорович", Number = "02", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 32, Surname = "Кручинин", Name = "Иван", LastName = "Андреевич", Number = "", Mail = "kruch@list.ru" });
                    context.ClientSet.Add(new Client { Id = 33, Surname = "Алферов", Name = "Алексей", LastName = "Николаевич", Number = "+688899444", Mail = "" });
                    context.ClientSet.Add(new Client { Id = 35, Surname = "Попов", Name = "Алексей", LastName = "Николаевич", Number = "+0489848565", Mail = "popovan@bik.ru" });
                    context.ClientSet.Add(new Client { Id = 36, Surname = "Неезжала", Name = "Наталья", LastName = "Леонидовна", Number = "", Mail = "neez@mail.ru" });

                    context.EstateSet.Add(new Estate { Id = 1, Type = "Apartment", Adress = "Тюмень,Энергостроителей,25,12", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 2, Type = "Apartment", Adress = "Тюмень,Елизарова,8,44", Coordinate = "0,0"});
                    context.EstateSet.Add(new Estate { Id = 3, Type = "Apartment", Adress = "Тюмень,Московский тракт,139,6", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 4, Type = "Apartment", Adress = "Тюмень,Широтная,189,5", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 5, Type = "Apartment", Adress = "Тюмень,Пролетарская,110,99", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 6, Type = "Apartment", Adress = "Тюмень,Тараскульская,189,1", Coordinate = "0,0"});
                    context.EstateSet.Add(new Estate { Id = 7, Type = "Apartment", Adress = "Тюмень,Парфенова,22,1", Coordinate = "0,0"});
                    context.EstateSet.Add(new Estate { Id = 8, Type = "Apartment", Adress = "Тюмень,Республики,144,16", Coordinate = "0,0" });

                    context.EstateSet.Add(new Estate { Id = 9, Type = "House", Adress = "Тюмень, 1-й Заречный,25,", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 10, Type = "House", Adress = "Тюмень,Ялyтopoвcкий тpaкт,,", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 11, Type = "House", Adress = "Тюмень,Березняковский п,,", Coordinate = "0,0" });

                    context.EstateSet.Add(new Estate { Id = 12, Type = "Land", Adress = "Тюмень,Луговое,,", Coordinate = "0,0" });
                    context.EstateSet.Add(new Estate { Id = 13, Type = "Land", Adress = "Тюмень,Алексеевский хутор,,", Coordinate = "0,0"});
                    context.EstateSet.Add(new Estate { Id = 14, Type = "Land", Adress = "Тюмень,Суходольский мкр,,", Coordinate = "0,0"});

                    context.ApartmentSet.Add(new Apartment { Id = 1, Room = 1, Floor = 3, Area = 41.7, IdEstate = 1 });
                    context.ApartmentSet.Add(new Apartment { Id = 2, Room = 3, Floor = 5, Area = 105, IdEstate = 2 });
                    context.ApartmentSet.Add(new Apartment { Id = 3, Room = 3, Floor = 2, Area = 62, IdEstate = 3 });
                    context.ApartmentSet.Add(new Apartment { Id = 4, Room = 2, Floor = 7, Area = 50, IdEstate = 4 });
                    context.ApartmentSet.Add(new Apartment { Id = 5, Room = 2, Floor = 2, Area = 51.7, IdEstate = 5 });
                    context.ApartmentSet.Add(new Apartment { Id = 6, Room = 2, Floor = 1, Area = 44, IdEstate = 6 });
                    context.ApartmentSet.Add(new Apartment { Id = 7, Room = 1, Floor = 5, Area = 43.1, IdEstate = 7 });
                    context.ApartmentSet.Add(new Apartment { Id = 8, Room = 3, Floor = 1, Area = 92, IdEstate = 8 });

                    context.HouseSet.Add(new House { Id = 9, Room = null, Floor = 2, Area = 84.4, IdEstate = 9 });
                    context.HouseSet.Add(new House { Id = 10, Room = null, Floor = 3, Area = 130, IdEstate = 10 });
                    context.HouseSet.Add(new House { Id = 11, Room = null, Floor = 1, Area = 120, IdEstate = 11 });

                    context.LandSet.Add(new Land { Id = 12, Area = 20.3, IdEstate = 12 });
                    context.LandSet.Add(new Land { Id = 13, Area = 12.45, IdEstate = 13 });
                    context.LandSet.Add(new Land { Id = 14, Area = 12, IdEstate = 14 });

                    context.NeedSet.Add(new Need { Id = 1, Type = "Apartment", Adress = "", MinPrice = null, MaxPrice = null, IdClient = 23, IdAgent = 4 });
                    context.NeedSet.Add(new Need { Id = 6, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = 3100000, IdClient = 5, IdAgent = 7 });
                    context.NeedSet.Add(new Need { Id = 7, Type = "Apartment", Adress = "Тюмень Широтная", MinPrice = null, MaxPrice = null, IdClient = 25, IdAgent = 24 });
                    context.NeedSet.Add(new Need { Id = 8, Type = "Apartment", Adress = "Тюмень Пролетарская", MinPrice = null, MaxPrice = null, IdClient = 26, IdAgent = 1 });
                    context.NeedSet.Add(new Need { Id = 9, Type = "Apartment", Adress = "Тюмень Тараскульская", MinPrice = null, MaxPrice = null, IdClient = 27, IdAgent = 15 });
                    context.NeedSet.Add(new Need { Id = 10, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 28, IdAgent = 19 });
                    context.NeedSet.Add(new Need { Id = 11, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 29, IdAgent = 4 });
                    context.NeedSet.Add(new Need { Id = 12, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 30, IdAgent = 7 });
                    context.NeedSet.Add(new Need { Id = 13, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 31, IdAgent = 9 });
                    context.NeedSet.Add(new Need { Id = 14, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 32, IdAgent = 11 });
                    context.NeedSet.Add(new Need { Id = 15, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 33, IdAgent = 13 });
                    context.NeedSet.Add(new Need { Id = 16, Type = "Apartment", Adress = "Тюмень", MinPrice = null, MaxPrice = null, IdClient = 35, IdAgent = 34 });
                    context.NeedSet.Add(new Need { Id = 2, Type = "House", Adress = "", MinPrice = null, MaxPrice = null, IdClient = 16, IdAgent = 4 });
                    context.NeedSet.Add(new Need { Id = 3, Type = "House", Adress = "", MinPrice = null, MaxPrice = null, IdClient = 10, IdAgent = 19 });
                    context.NeedSet.Add(new Need { Id = 4, Type = "Land", Adress = "", MinPrice = null, MaxPrice = null, IdClient = 12, IdAgent = 15 });
                    context.NeedSet.Add(new Need { Id = 5, Type = "Land", Adress = "", MinPrice = null, MaxPrice = null, IdClient = 14, IdAgent = 1 });

                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 1, IdNeed = 1 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 6, MinArea = 5, MaxArea = 20, MaxFloor = 4, IdNeed = 6 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 7, IdNeed = 7 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 8, IdNeed = 8 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 9, IdNeed = 9 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 10, MinArea = 60, IdNeed = 10 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 11, MaxFloor = 4, IdNeed = 11 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 12, MaxFloor = 5, IdNeed = 12 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 13, MaxRoom = 2, IdNeed = 13 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 14, MaxRoom = 3, IdNeed = 14 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 15, MinArea = 30, MaxFloor = 4, IdNeed = 15 });
                    context.ApartmentNeedSet.Add(new ApartmentNeed { Id = 16, IdNeed = 16 });

                    context.HouseNeedSet.Add(new HouseNeed { Id = 2, IdNeed = 2 });
                    context.HouseNeedSet.Add(new HouseNeed { Id = 3, IdNeed = 3 });

                    context.LandNeedSet.Add(new LandNeed { Id = 4, IdNeed = 4 });
                    context.LandNeedSet.Add(new LandNeed { Id = 5, IdNeed = 5 });


                    context.OfferSet.Add(new Offer { Id = 1, IdClient = 2, IdAgent = 1, IdEstate = 1, Price = 2500000 });
                    context.OfferSet.Add(new Offer { Id = 2, IdClient = 8, IdAgent = 7, IdEstate = 3, Price = 5000000 });
                    context.OfferSet.Add(new Offer { Id = 3, IdClient = 12, IdAgent = 11, IdEstate = 3, Price = 1300000 });
                    context.OfferSet.Add(new Offer { Id = 4, IdClient = 16, IdAgent = 15, IdEstate = 4, Price = 5000000 });
                    context.OfferSet.Add(new Offer { Id = 5, IdClient = 3, IdAgent = 1, IdEstate = 2, Price = 4700000 });
                    context.OfferSet.Add(new Offer { Id = 6, IdClient = 5, IdAgent = 4, IdEstate = 3, Price = 3750000 });
                    context.OfferSet.Add(new Offer { Id = 7, IdClient = 6, IdAgent = 4, IdEstate = 3, Price = 1900000 });
                    context.OfferSet.Add(new Offer { Id = 8, IdClient = 10, IdAgent = 9, IdEstate = 3, Price = 4300000 });
                    context.OfferSet.Add(new Offer { Id = 9, IdClient = 14, IdAgent = 13, IdEstate = 3, Price = 1750000 });
                    context.OfferSet.Add(new Offer { Id = 10, IdClient = 17, IdAgent = 15, IdEstate = 5, Price = 5850000 });
                    context.OfferSet.Add(new Offer { Id = 11, IdClient = 18, IdAgent = 15, IdEstate = 6, Price = 6800000 });
                    context.OfferSet.Add(new Offer { Id = 12, IdClient = 20, IdAgent = 19, IdEstate = 7, Price = 950000 });
                    context.OfferSet.Add(new Offer { Id = 13, IdClient = 21, IdAgent = 19, IdEstate = 8, Price = 700000 });
                    context.OfferSet.Add(new Offer { Id = 14, IdClient = 22, IdAgent = 19, IdEstate = 9, Price = 600000 });

                    context.DealSet.Add(new Deal { Id = 1, IdNeed = 1, IdOffer = 1 });
                    context.DealSet.Add(new Deal { Id = 2, IdNeed = 3, IdOffer = 2 });
                    context.DealSet.Add(new Deal { Id = 3, IdNeed = 5, IdOffer = 3 });
                    context.DealSet.Add(new Deal { Id = 4, IdNeed = 7, IdOffer = 4 });

                    context.SaveChanges();
                }
            }
        }
        private void OpenClientsWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления клиентами
            MainFrame.Navigate(new ClientPage());

            client.Style = (Style)Resources["press"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["nopress"];
        }

        private void OpenRealtorsWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления риэлторами
            MainFrame.Navigate(new RieltorPage());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["press"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["nopress"];
        }

        private void OpenRealEstateWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления объектами недвижимости
            MainFrame.Navigate(new ObjectPage());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["press"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["nopress"];
        }

        private void OpenNeedsWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления потребностями
            MainFrame.Navigate(new NeedPage());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["press"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["nopress"];
        }

        private void OpenDealsWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления сделками
            MainFrame.Navigate(new DealPage());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["press"];
        }

        private void OpenOffersWindow(object sender, RoutedEventArgs e)
        {
            // Открыть окно управления предложениями
            MainFrame.Navigate(new OfferPage());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["press"];
            deal.Style = (Style)Resources["nopress"];
        }

        private void Main(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new main());
            client.Style = (Style)Resources["nopress"];
            rieltor.Style = (Style)Resources["nopress"];
            obgect.Style = (Style)Resources["nopress"];
            need.Style = (Style)Resources["nopress"];
            offer.Style = (Style)Resources["nopress"];
            deal.Style = (Style)Resources["nopress"];
        }
    }
}
