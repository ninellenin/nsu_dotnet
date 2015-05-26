using System;
using System.Drawing;

namespace Sazonova.DotNetCourse.Perlin
{
    class ColoredGrid
    {
        private int _size;
        private Grid _redColor;
        private Grid _greenColor;
        private Grid _blueColor;

        public ColoredGrid(int size, int step)
        {
            _size = size;
            _redColor = new Grid(size, step);
            _greenColor = new Grid(size, step);
            _blueColor = new Grid(size, step);
        }

        public Color [ , ] Turn(Color turnColor)
        {
            Color[,] result = new Color[_size, _size];

            for (int i = 0; i < _size; ++i)
            {
                for (int j = 0; j < _size; ++j)
                {
                    result[i, j] = MultiplyColors(GetColor(i, j), turnColor);
                }
            }

            return result;
        }

        private Color MultiplyColors(Color color, Color turnColor)
        { 
            return Color.FromArgb((int)(Grid.Round((double)color.R * turnColor.R)),
                (int)(Grid.Round((double)color.G * turnColor.G)),
                (int)(Grid.Round((double)color.B * turnColor.B)));
        }

        public Color GetColor(int x, int y)
        {
            return Color.FromArgb((int)_redColor.GetColor(x, y), 
                (int)_greenColor.GetColor(x, y),
                (int)_blueColor.GetColor(x, y));
        }
    }
}
