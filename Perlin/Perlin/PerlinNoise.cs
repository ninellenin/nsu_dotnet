using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sazonova.DotNetCourse.Perlin
{
	public class PerlinNoise
	{
        private int _size;
        private ColoredGrid[] _grids;
        private Color[,] _colors;
        private Bitmap _bitmap;

		public PerlinNoise (int size, float []partition)
		{
            int length = partition.Length;
            Init(size, length);
                 
            for (int i = 0, step =  size / 2; (step > 0) && (i < length); ++i, step /= 2)
            {
                _grids[i] = new ColoredGrid(size, step);

                for (int j = 0; j < size; ++j) 
                {
                    for (int k = 0; k < size; ++k)
                    {
                        _colors[j, k] = SumColors(_colors[j, k], _grids[i].GetColor(j, k), 1, partition[i]);
                    }
                }
            }
		}

        public PerlinNoise(int size, float[] partition, Color turnColor)
        {
            int length = partition.Length;
            Init(size, length);

            for (int i = 0, step = size / 2; (step > 0) && (i < length); ++i, step /= 2)
            {
                _grids[i] = new ColoredGrid(size, step);
                _grids[i].Turn(turnColor);

                for (int j = 0; j < size; ++j)
                {
                    for (int k = 0; k < size; ++k)
                    {
                        _colors[j, k] = SumColors(_colors[j, k], _grids[i].GetColor(j, k), 1, partition[i]);
                    }
                }
            }
        }

        private void Init(int size, int length)
        {
            _size = size;

            _grids = new ColoredGrid[length];
            _colors = new Color[size, size];
            _bitmap = new Bitmap(size, size);

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    _colors[i, j] = new Color();
                }
            }

        }

        public void Save(string name)
        {
            for (int i = 0; i < _size; ++i)
            {
                for (int j = 0; j < _size; ++j)
                {
                    _bitmap.SetPixel(i, j, _colors[i, j]);
                }
            }
            _bitmap.Save(name, ImageFormat.Bmp);
        }

        private Color SumColors(Color x, Color y, float alpha, float beta)
        {
            return Color.FromArgb((int)(x.R * alpha + y.R * beta),
                (int)(x.G * alpha + y.G * beta),
                (int)(x.B * alpha + y.B * beta));
        }

        private int Round(int number)
        {
            if (number > 255)
            {
                return 255;
            }
            if (number < 0)
            {
                return 0;
            }

            return (byte)number;
        }
	}
}

