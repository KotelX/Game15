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

namespace Game15
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.UserView.Content = (object)(new ChangeSize());
        }

        //сделать мигающее окно при победе (тени)
        private void ReadSize()
        {
            
            //MessageBox.Show(new ChangeSize().ToString());
        }
        bool ViewMap(Map map = null)
        {
            return true;
        }
    }
}
