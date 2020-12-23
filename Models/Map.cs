using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Windows;
using Game15.Models;
using System.Windows.Media;
using System.Windows.Input;

namespace Game15
{
    class Map
    {
        Grid ResultGrid = new Grid();

        public delegate void ButtonClick(Border sender);

        public event ButtonClick ButtonClickEvent;

        public Position Size { get; private set; }

        private List<Border> _buttons = new List<Border>();

        public Map(Position size, MouseButtonEventHandler mouseButtinEventHandler = null)
        {
            if (size == null) return;
            if (mouseButtinEventHandler == null) mouseButtinEventHandler = LeftMousButtonClick;
            Size = size;
            int k = 0;
            for (int i = 0; i < size.Y; i++)
            {
                for (int j = 0; j < size.X; j++)
                { 
                    _buttons.Add(new Border() { Background = Brushes.White, BorderBrush = Brushes.Azure, BorderThickness = new Thickness(1), Name = $"B{k}", Margin = new Thickness(1), CornerRadius = new CornerRadius(1)});
                    _buttons[i * Size.X + j].MouseLeftButtonDown += mouseButtinEventHandler;
                    _buttons[i * Size.X + j].Child = new TextBlock() { Text = $"{k + 1}", Foreground = Brushes.White, FontSize =  40, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center};
                    k++;
                }
            }
            _buttons[size.Count - 1].Opacity = 0;
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        public void Exchange(Border a = null, Border b = null)
        {
            if (b == null) b = _buttons.First(x => x.Opacity == 0);
            var positionB = getElemntPosition(b);
            if (a == null)
            { 
                var rnd = new Random();
                int r = rnd.Next(0, 4);
                if (positionB.X == 0 && r == 0) r++;
                if (positionB.Y == 0 && r == 1) r++;
                switch(r)
                {
                    case 0:
                        {
                            var posA = new Position() { X = positionB.X - 1, Y = positionB.Y };
                            if (posA.X >= Size.X || posA.Y >= Size.Y) { r++; return; }
                            a = getElement(posA);
                        }
                        break;

                    case 1:
                        {
                            var posA = new Position() { X = positionB.X, Y = positionB.Y - 1 };
                            if (posA.X >= Size.X || posA.Y >= Size.Y) { r++; return; }
                            a = getElement(posA);
                        }
                        break;

                    case 2:
                        {
                            var posA = new Position() { X = positionB.X + 1, Y = positionB.Y };
                            if (posA.X >= Size.X || posA.Y >= Size.Y) { r++; return; }
                            a = getElement(posA);
                        }
                        break;



                    case 3:
                        {
                            var posA = new Position() { X = positionB.X, Y = positionB.Y + 1 };
                            if (posA.X >= Size.X || posA.Y >= Size.Y) { r++; return; }
                            a = getElement(posA);
                        }
                        break;

                    default: return;
                }
            }
            var positionA = getElemntPosition(a);
            if (Math.Abs(positionA.X - positionB.X) >= 1 || Math.Abs(positionA.Y - positionB.Y) >= 1) return;
            Grid.SetColumn(_buttons[_buttons.IndexOf(a)], positionB.X);
            Grid.SetRow(_buttons[_buttons.IndexOf(a)], positionB.Y);
            Grid.SetColumn(_buttons[_buttons.IndexOf(b)],positionA.X);
            Grid.SetRow(_buttons[_buttons.IndexOf(b)], positionA.Y);
        }

        private List<Border> Buttons 
        { 
            get => _buttons; 
            set 
            { 
                if (value != null) _buttons = Buttons;
            } 
        }

        public Grid View()
        {
            for (int i = 0; i < Size.Y; i++)
            {
                ResultGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < Size.X; i++)
            {
                ResultGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < Size.Y; i++)
            {
                for (int j = 0; j < Size.X; j++)
                {
                    ResultGrid.Children.Add((Border)_buttons[i * Size.X + j]);
                    Grid.SetColumn(_buttons[i * Size.X + j], j);
                    Grid.SetRow(_buttons[i * Size.X + j], i);
                }
            }
            return ResultGrid;
        }

        private void LeftMousButtonClick(object sender, EventArgs e)
        {
            ButtonClickEvent?.Invoke((Border)sender);
        }

        private Position getElemntPosition(Border border)
        {
            if (border == null) return null;
            var position = new Position();
            position.X = Grid.GetColumn(border);
            position.Y = Grid.GetRow(border);
            return position;
        }

        public void IsWin()
        {
            for (int i = 0; i < Size.Count; i++)
            {
                var index = int.Parse(_buttons[i].Name.Split('B', StringSplitOptions.RemoveEmptyEntries)[0]);
                var butPosition = getElemntPosition(_buttons[i]);
                if (index != butPosition.X + butPosition.Y * Size.X) return;
            }
            MessageBox.Show("You win!!!");
        }

        public void CropImage(ImageSource img)
        {
            if (img == null) throw new Exception("img = null");
            var drawingVisual = new DrawingVisual();
            var width = img.Width / Size.X;
            var height = img.Height / Size.Y;
            for (int i = 0; i < Size.Y; i++)
            {
                for (int j = 0; j < Size.X; j++)
                {
                    using (var drawingContext = drawingVisual.RenderOpen())
                    {
                        var cropRect = new Rect(
                            x: width * j ,
                            y: height * i,
                            width: width,
                            height: height);
                        var brush = new ImageBrush(img)
                        {
                            ViewboxUnits = BrushMappingMode.Absolute,
                            Viewbox = cropRect
                        };
                        drawingContext.DrawRectangle(
                            brush, pen: new Pen(Brushes.Red, 2), cropRect);
                        _buttons[i * Size.X + j].Background = brush;
                    }
                }
            }
        }

        private Border getElement(Position position)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (Grid.GetColumn(_buttons[i]) == position.X && Grid.GetRow(_buttons[i]) == position.Y) return (_buttons[i]);
            }
            return null;
        }

        public void RandomExchange(int level)
        {
            for (int i = 0; i < level; i++)
            {
                Exchange();
            }
        }
    }
}
