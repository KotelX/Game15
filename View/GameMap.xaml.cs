using Game15.Models;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Game15.View
{
    /// <summary>
    /// Логика взаимодействия для GameMap.xaml
    /// </summary>
    public partial class GameMap : UserControl
    {
        private Map Map;

        public GameMap(Position _size)
        {
            InitializeComponent();
            Map = new Map(_size);
            Map.ButtonClickEvent += MousClick;
            StartGame();
            Map.CropImage(new BitmapImage(new Uri("pack://application:,,,/View/Desert.jpg")));
            Map.RandomExchange(100);
            MainGrid.Background.Opacity = 0.8;
            //MainGrid.Background = new ImageBrush(new BitmapImage(new Uri("bear.jpg")));
        }

        private void StartGame()
        {
            MainGrid.Children.Add(Map.View());
        }
        
        private void MousClick(Border border)
        {
            Map.IsWin();
            Map.Exchange(border);
        }
    }
}
