using System;
using System.Collections.Generic;
using System.Text;

namespace Game15.Models
{
    public class Position
    {
        public Position(int maxX = int.MaxValue - 1, int maxY = int.MaxValue - 1)
        {
            //if (maxX < 0 || maxY < 0) return;
            MaxX = maxX;
            MaxY = maxY;
            X = 0;
            Y = 0;
        }

        private int _maxX;

        private int _maxY;

        private int _x;

        private int _y;

        public int MaxX 
        { 
            get 
            { 
                return _maxX; 
            } 
            private set 
            { 
                if (value >= 0) _maxX = value; else return; 
            } 
        }

        public int MaxY 
        { 
            get 
            { 
                return _maxY; 
            } 
            private set 
            { 
                if (value >= 0) _maxY = value; else return; 
            } 
        }

        public int X 
        { 
            get 
            { 
                return _x; 
            } 
            set 
            { 
                if (value >= 0 && value <= MaxX) _x = value; 
                else 
                    throw new Exception("Значение X вышло за допустимые границы"); 
            } 
        }

        public int Y 
        { 
            get 
            { 
                return _y; 
            } 
            set 
            { 
                if (value >= 0 && value <= MaxY) _y = value; 
                else 
                    throw new Exception("Значение Y вышло за допустимые границы"); 
            } 
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}
