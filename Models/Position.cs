using System;

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
            get => _maxX;
            private set
            {
                if (value >= 0) _maxX = value;
            }
        }

        public int MaxY
        {
            get => _maxY;
            private set
            {
                if (value >= 0) _maxY = value; else return;
            }
        }

        public int X
        {
            get => _x;
            set
            {
                _x = value >= 0 && value <= MaxX ? value : throw new Exception("Значение X вышло за допустимые границы");
            }
        }

        public int Y
        {
            get => _y;
            set => _y = value >= 0 && value <= MaxY ? value : throw new Exception("Значение Y вышло за допустимые границы");
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }

        public int Count => X * Y;
    }
}
