﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game15.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game15.View
{
    public partial class ChangeSize : UserControl, INotifyPropertyChanged
    {
        private Position _changedSize;
        public Position ChangedSize 
        { 
            get 
            {
                return _changedSize;
            }
            private set 
            {
                if (value.X < 0 && value.Y > 0) return;
                _changedSize = value; 
                NotifyPropertyChanged();
            } 
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(_changedSize, new PropertyChangedEventArgs("Size"));
        }

        private const int _Size = 5;

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void ShowGridMapOnLoad()
        {
            GridMap.Children.Clear();
            for (int i = 0; i < _Size; i++)
            {
                for (int j = 0; j < _Size; j++)
                {
                    Border border = new Border() { Background = Brushes.Transparent, BorderBrush = Brushes.White, BorderThickness = new Thickness(1)};
                    border.MouseEnter += gridMapEnter;
                    border.MouseLeftButtonDown += gridMapClick;
                    border.Margin = new Thickness(1);
                    border.Name = $"C{j + 1}R{i + 1}";
                    GridMap.Children.Add(border);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                }
            }
        }

        private void UpdateGridMap(Position position)
        {
            for (int i = 0; i < _Size; i++)
            {
                for (int j = 0; j < _Size; j++)
                {
                    int number = i * _Size + j;
                    var border = ((Border)GridMap.Children[number]);
                    if (j < position.X && i < position.Y)
                    {
                        border.Background = Brushes.Azure;
                        border.BorderBrush = Brushes.SlateGray;
                    }
                    else
                    {
                        border.Background = Brushes.Transparent;
                        border.BorderBrush = Brushes.White;
                    }
                }
            }
        }

        private void gridMapClick(object sender, RoutedEventArgs e)
        {
            ChangedSize = getElemntNumber((Border)sender);
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
