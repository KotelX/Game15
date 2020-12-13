using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Game15.Models;
using System.Linq;
using System.Collections.Generic;

namespace Game15.View
{
    public partial class ChangeSize : UserControl
    {
        private const int _Size = 5;
        private List<int> borderIndex = new List<int>(_Size);
        public ChangeSize()
        {
            InitializeComponent();
            TextBoxForMap.Text = new Position().ToString();
            GridMap.Width = _Size * 35;
            GridMap.Height = _Size * 35;
            for (int i = 0; i < _Size; i++)
            {
                GridMap.RowDefinitions.Add(new RowDefinition());
                GridMap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            ShowGridMapOnLoad();
        }
        ///GOTO:разобратся с постоянным вызовом сборщика мусора
        private void ShowGridMapOnLoad(Position position = null)
        {
            if (position == null) position = new Position();
            GridMap.Children.Clear(); ;
            for (int i = 0; i < _Size; i++)
            {
                for (int j = 0; j < _Size; j++)
                {
                    Border border;
                    if (j < position.X && i < position.Y) 
                        border = new Border() { Background = Brushes.Azure, BorderBrush = Brushes.SlateGray, BorderThickness = new Thickness(1), OpacityMask = Brushes.Red};
                    else
                        border = new Border() { Background = Brushes.Transparent, BorderBrush = Brushes.White, BorderThickness = new Thickness(1)};
                    border.MouseEnter += gridMapEnter;
                    border.MouseLeftButtonDown += gridMapClick;
                    border.Margin = new Thickness(1);
                    border.Name = $"C{j + 1}R{i + 1}";
                    borderIndex.Add(GridMap.Children.Add(border));
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                }
            }
        }
        private void UpdateGridMap(Position position)
        {
            for (int i = 0; i < GridMap.Children.Count; i++)
            {
                if (i % _Size < position.X)
                    GridMap.Children.RemoveAt(borderIndex[i]);
            }
        }
        private void gridMapClick(object sender, RoutedEventArgs e)
        {
            var position = getElemntNumber((Border)sender);
            //MessageBox.Show(position.ToString());
        }

        private void gridMapEnter(object sender, RoutedEventArgs e)
        {
            var position = getElemntNumber((Border)sender);
            TextBoxForMap.Text = $"{position.X} x {position.Y}";
            UpdateGridMap(position);
        }
        private Position getElemntNumber(Border border)
        {
            if (border == null) return null;
            var position = new Position();
            var s = ((Border)border).Name.ToString().Split('C', StringSplitOptions.RemoveEmptyEntries)[0].Split('R', StringSplitOptions.RemoveEmptyEntries);
            position.X = int.Parse(s[0]);
            position.Y = int.Parse(s[1]);
            return position;
        }
    }
}
