using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game15.View
{
    public partial class ChangeSize : UserControl
    {
        public ChangeSize()
        {
            InitializeComponent();
            ShowGridMap();
        }

        public void ShowGridMap()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var border = new Button() { BorderBrush = new SolidColorBrush(new Color() { R = 30, G = 30, B = 0 }), BorderThickness = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Background = new SolidColorBrush(new Color() { R = 0, G = 0, B = 0, A = 0})};
                    border.Style = Style.ButtonForGrid;
                    this.GridMap.Children.Add(border);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    
                }
            }
        }
    }
}
