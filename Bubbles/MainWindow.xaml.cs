using System;
using System.Collections;
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

namespace Bubbles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<int> indexes = Enumerable.Range(1, 5);
        ObservableCollection<Item> collection;
        Color[] colors = new Color[]
        {
                Colors.Red,
                Colors.GreenYellow,
                Colors.AliceBlue,
                Colors.Pink,
                Colors.MediumPurple,
                Colors.Yellow,
                Colors.RosyBrown,
                Colors.Orange,
        };
        Random rand = new Random(Environment.TickCount);
        const int Size = 40;
        const int Offset = Size + 8;
        Item selectedItem;
        Point lastCoords;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var itemsCollection = new List<Item>();

            for (var i = 0; i < 5; i++)
            {
                var items = indexes
                    .Select(x => (x + i) * Offset)
                    .Select(x => new Item()
                    {
                        X = x,
                        Y = i * Offset,
                        D = Size,
                        Value = 0,
                        Color = GetRandomColor()
                    });

                itemsCollection.AddRange(items);
            }

            collection = new ObservableCollection<Item>(itemsCollection);

            canvas.DataContext = collection;
        }

        private Color GetRandomColor(IEnumerable<Color> colorsRange = null)
        {
            if (colorsRange == null)
            {
                colorsRange = colors;
            }
            return colorsRange.ElementAt(rand.Next(colorsRange.Count()));
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(canvas);
            var item = collection.FirstOrDefault(x => IsPointInsideItemShape(point, x));
            if (item == null)
            {
                return;
            }
            selectedItem = item;
            lastCoords = point;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedItem == null)
            {
                return;
            }
            var point = e.GetPosition(canvas);
            var diff = lastCoords - point;

            selectedItem.X -= diff.X;
            selectedItem.Y -= diff.Y;

            lastCoords = point;
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedItem == null)
            {
                return;
            }
            var otherColors = colors.Where(x => x != selectedItem.Color);
            selectedItem.Color = GetRandomColor(otherColors);
            selectedItem.Value += 1;
            selectedItem = null;
        }

        private bool IsPointInsideItemShape(Point point, Item item)
        {
            var radius = item.D / 2;
            var radiusSquared = Math.Pow(radius, 2);
            var center = new Point(item.X + radius, item.Y + radius);

            var diff = point - center;
            return diff.LengthSquared <= radiusSquared;
        }
    }
}
