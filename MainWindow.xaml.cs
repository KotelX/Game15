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
        Map MainMap;

        public MainWindow()
        {
            InitializeComponent();
            ReadSize();
        }

        //сделать мигающее окно при победе (тени)
        private void ReadSize()
        {
            var changedSize = new ChangeSize();
            changedSize.PropertyChanged += ViewMap;
            this.UserView.Content = changedSize;
        }

        void ViewMap(object sender, PropertyChangedEventArgs e)
        {
            MessageBox.Show(((Position)sender).ToString());
        }
    }
}
