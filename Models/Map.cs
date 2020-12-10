using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Linq;
using System.Windows;

namespace Game15
{
    class Map : INotifyPropertyChanged
    {
        public int Size { get; private set; }

        private List<Button> _buttons;

        public Map(int size, int[,] buttons = null)
        {
            if(size <= 0 || buttons == null) return;
            Size = size;
            int k = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _buttons.Add(new Button { Content = k, Name = $"button{k}"});
                    k++;
                }
            } 
        }
        
        public List<Button> Buttons 
        { 
            get => _buttons; 
            set 
            { 
                if (value != null) _buttons = Buttons; NotifyPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
