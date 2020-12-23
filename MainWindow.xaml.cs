using System.Windows;
using Game15.View;
using Game15.Models;
using System.ComponentModel;

namespace Game15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Size;

        public MainWindow()
        {
            InitializeComponent();
            ReadSize();
        }

        //сделать мигающее окно при победе (тени)
        private void ReadSize()
        {
            var changedSize = new ChangeSize();
            changedSize.PropertyChanged += Game;
            this.UserView.Content = changedSize;
        }

        private void Game(object sender, PropertyChangedEventArgs e)
        {
            this.Width = 700;
            this.Height = 550;
            UserView.Content = new GameMap((Position)sender);
        }
    }
}
