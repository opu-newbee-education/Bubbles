using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Bubbles
{
    class Item : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _d;
        private int _value;
        private Color _color;


        public double X { get { return _x; } set { _x = value; OnPropertyChanged(nameof(X)); } }
        public double Y { get { return _y; } set { _y = value; OnPropertyChanged(nameof(Y)); } }
        public int Value { get { return _value; } set { _value = value; OnPropertyChanged(nameof(Value)); } }
        public double D { get { return _d; } set { _d = value; OnPropertyChanged(nameof(D)); } }
        public Color Color { get { return _color; } set { _color = value; OnPropertyChanged(nameof(ColorBrush)); } }


        public SolidColorBrush ColorBrush
        {
            get { return new SolidColorBrush(Color); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
