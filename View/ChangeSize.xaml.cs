using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Game15.View
{
    public partial class ChangeSize : UserControl
    {
        private int _Size = 5;
        public ChangeSize()
        {
            InitializeComponent();
            GridMap.Width = _Size * 35;
            GridMap.Height = _Size * 35;
            for (int i = 0; i < _Size; i++)
            {
                GridMap.RowDefinitions.Add(new RowDefinition());
                GridMap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ShowGridMap();
        }

        public void ShowGridMap(int x = 0, int y = 0)
        {
            GridMap.Children.Clear();
            for (int i = 0; i <= _Size; i++)
            {
                for (int j = 0; j <= _Size; j++)
                {
                    Border border;
                    if (j < x && i < y) 
                        border = new Border() { Background = Brushes.Azure, BorderBrush = Brushes.SlateGray, BorderThickness = new Thickness(1), OpacityMask = Brushes.Red};
                    else
                        border = new Border() { Background = Brushes.Transparent, BorderBrush = Brushes.White, BorderThickness = new Thickness(1) };
                    border.MouseEnter += gridMapClick;
                    border.Name = $"C{j + 1}R{i + 1}";
                    this.GridMap.Children.Add(border);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                }
            }
        }

        private void gridMapClick(object sender, RoutedEventArgs e)
        {
            var s = ((Border)sender).Name.ToString().Split('C',StringSplitOptions.RemoveEmptyEntries)[0].Split('R',StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(s[0]);
            var y = int.Parse(s[1]);
            ShowGridMap(x,y);
        }
    }
}
