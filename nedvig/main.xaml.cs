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
using static nedvig.MainWindow;
using System.Windows.Media.Animation;

namespace nedvig
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Page
    {
        private readonly double squareSize = 40;
        private int currentShapeIndex = 0;
        private readonly SolidColorBrush[] colors = new SolidColorBrush[]
        {
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#be1e2d")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ed1c24")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f15a29")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f7941e")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fbb040")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff200")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d7df23")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8dc63f")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00a651")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006838")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00a79d")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#27aae1")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1c75bc")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b3990")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#262262")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#662d91")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#92278f")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9e1f63")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#da1c5c")),
    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ee2a7b"))
        };

        public main()
        {
            InitializeComponent();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGrid();
        }
        private void DrawGrid()
        {
            MainCanvas.Children.Clear();

            int rows = (int)(MainCanvas.ActualHeight / squareSize);
            int columns = (int)(MainCanvas.ActualWidth / squareSize);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    DrawSquare(j * squareSize, i * squareSize);
                }
            }
        }
        private void DrawSquare(double left, double top)
        {
            var triangle1 = CreateTriangle(left, top, new Point(left + squareSize, top), new Point(left, top + squareSize));
            var triangle2 = CreateTriangle(left + squareSize, top, new Point(left + squareSize, top + squareSize), new Point(left, top + squareSize));

            MainCanvas.Children.Add(triangle1);
            MainCanvas.Children.Add(triangle2);
        }
        private Polygon CreateTriangle(double x1, double y1, Point p2, Point p3)
        {

            Polygon triangle = new Polygon
            {
                Fill = Brushes.Transparent, // Изначально прозрачный
                Points = new PointCollection { new Point(x1, y1), p2, p3 }
            };

            // Обработчики событий для наведения мыши
            triangle.MouseEnter += Triangle_MouseEnter;

            return triangle;
        }

        private void Triangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Polygon triangle = sender as Polygon;
            if (triangle != null)
            {
                SolidColorBrush newColor = colors[currentShapeIndex % colors.Length];

                SolidColorBrush animatedBrush = new SolidColorBrush();
                animatedBrush.Color = ((SolidColorBrush)triangle.Fill).Color;
                triangle.Fill = animatedBrush;

                var colorAnimation = new ColorAnimation
                {
                    To = newColor.Color,
                    Duration = TimeSpan.FromMilliseconds(300) // Время изменения цвета
                };

                triangle.Fill.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
                currentShapeIndex++;

            }
        }

        
    }
}
