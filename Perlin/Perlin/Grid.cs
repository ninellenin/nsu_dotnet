using System;

namespace Sazonova.DotNetCourse.Perlin
{
	public class Grid
	{
        private int _size;
        private int _gridSize;
		private int _step;
        private byte [ , ] _values;
		private byte [ , ] _grid;
        private const int InterpolateOrder = 3;

		public Grid (int size, int step)
		{
			_step = step;
			_size = size;
            _gridSize = _size / step;
			_grid = new byte[_gridSize + 3, _gridSize + 3];
            _values = new byte[_size, _size];

			FillRandomly();
            BilinearInterpolation();
		}

		private void FillRandomly ()
		{
			Random random = new Random();
			
			// fill the grid with borders
			for (int i = 0; i < _gridSize + 3; ++i) 
			{
				for (int j = 0; j < _gridSize + 3; ++j) 
				{
					_grid[i, j] = (byte)random.Next(50, 255);
				}
			}
		}
		public void BilinearInterpolation ()
		{
			for (int i = 0; i < _gridSize; ++i) 
			{
				for (int j = 0; j < _gridSize; ++j)
				{
                    // iterpolate other values 
                    float [ , ] c = new float [InterpolateOrder + 1, InterpolateOrder + 1];
					c[0, 0] = _grid[i + 1, j + 1];
					c[0, 1] = (- 0.5f) * _grid[i + 1, j]
						+ 0.5f * _grid[i + 1, j + 2];
					c[0, 2] = _grid[i + 1, j]
						+ (- 2.5f) * _grid[i + 1, j + 1]
						+ 2f * _grid[i + 1, j + 2]
						+ (- 0.5f) * _grid[i + 1, j + 3];
					c[0, 3] = (-0.5f) * _grid[i + 1, j]
						+ 1.5f * _grid[i + 1, j + 1]
						+ (- 1.5f) * _grid[i + 1, j + 2] 
						+ 0.5f * _grid[i + 1, j + 3];	
					c[1, 0] = (-0.5f) * _grid[i, j + 1]
						+ 0.5f * _grid[i + 2, j + 1];
					c[1, 1] = 0.25f * _grid[i, j]
						+ (- 0.25f) * _grid[i, j + 2] 
						+ (- 0.25f) * _grid[i + 2, j] 
						+ 0.25f * _grid[i + 2, j + 2];
					c[1, 2] = (- 0.5f) * _grid[i, j]
						+ 1.25f * _grid[i, j + 1] 
						- _grid[i, j + 2] 
						+ 0.25f * _grid[i, j + 3]
						+ 0.5f * _grid[i + 2, j] 
						+ (- 1.25f) * _grid[i + 2, j + 1]
						+ _grid[i + 2, j + 2] 
						+ (- 0.25f) * _grid[i + 2, j + 3];
					c[1, 2] = (- 0.5f) * _grid[i, j]
						+ 1.25f * _grid[i, j + 1] 
						- _grid[i, j + 2] 
						+ 0.25f * _grid[i, j + 3]
						+ 0.5f * _grid[i + 2, j] 
						+ (- 1.25f) * _grid[i + 2, j + 1]
						+ _grid[i + 2, j + 2] 
						+ (- 0.25f) * _grid[i + 2, j + 3];
					c[1, 3] = 0.25f * _grid[i, j]
						+ (- 0.75f) * _grid[i, j + 1] 
						+  0.75f * _grid[i, j + 2] 
						+ (-0.25f) * _grid[i, j + 3] 
						+ (-0.25f) * _grid[i + 2, j] 
						+ 0.75f * _grid[i + 2, j + 1]
						+ (- 0.75f) * _grid[i + 2, j + 2]
						+ 0.25f * _grid[i + 2, j + 3];
					c[2, 0] = _grid[i, j + 1]
						+ (- 2.5f) * _grid[i + 1, j + 1] 
						+  2f * _grid[i + 2, j + 1] 
						+ (- 0.5f) * _grid[i + 3, j + 1];
					c[2, 1] = (- 0.5f) * _grid[i, j]
						+ 0.5f * _grid[i, j + 2] 
						+  1.25f * _grid[i + 1, j] 
						+ (-1.25f) * _grid[i + 1, j + 2] 
						- _grid[i + 2, j] 
						+ _grid[i + 2, j + 2]
						+ 0.25f * _grid[i + 3, j]
						+ (- 0.25f) * _grid[i + 3, j + 2];
					c[2, 2] = _grid[i, j]
						+ (- 2.5f) * _grid[i, j + 1] 
						+  2f * _grid[i, j + 2] 
						+ (-0.5f) * _grid[i, j + 3] 
						+ (- 2.5f) * _grid[i + 1, j] 
						+  6.25f * _grid[i + 1, j + 1] 
						+  (- 5f) * _grid[i + 1, j + 2] 
						+ 1.25f * _grid[i + 1, j + 3] 
						+  2f * _grid[i + 2, j] 
						+  (-5f) * _grid[i + 2, j + 1] 
						+ 4f * _grid[i + 2, j + 2]
						- _grid[i + 2, j + 3]
						+ (- 0.5f) * _grid[i + 3, j]
						+  1.25f * _grid[i + 3, j + 1] 
						- _grid[i + 3, j + 2] 
						+ 0.25f * _grid[i + 3, j + 3];
					c[2, 3] = (- 0.5f) * _grid[i, j]
						+ 1.5f * _grid[i, j + 1] 
						+  (-1.5f) * _grid[i, j + 2] 
						+ 0.5f * _grid[i, j + 3] 
						+ 1.25f * _grid[i + 1, j] 
						+  (-3.75f) * _grid[i + 1, j + 1] 
						+  3.75f * _grid[i + 1, j + 2] 
						+ (- 1.25f) * _grid[i + 1, j + 3] 
						- _grid[i + 2, j] 
						+  3f * _grid[i + 2, j + 1] 
						+ (-3f) * _grid[i + 2, j + 2]
						+ _grid[i + 2, j + 3]
						+  0.25f * _grid[i + 3, j]
						+  (- 0.75f) * _grid[i + 3, j + 1] 
						+ 0.75f * _grid[i + 3, j + 2] 
						+ (- 0.25f) * _grid[i + 3, j + 3];
					c[3, 0] = (- 0.5f) * _grid[i, j + 1]
						+ 1.5f * _grid[i + 1, j + 1]
						+  (- 1.5f) * _grid[i + 2, j + 1] 
						+ 0.5f * _grid[i + 3, j + 1];
					c[3, 1] = 0.25f * _grid[i, j]
						+ (- 0.25f) * _grid[i, j + 2] 
						+ (- 0.75f) * _grid[i + 1, j]
						+  0.75f * _grid[i + 1, j + 2] 
						+  0.75f * _grid[i + 2, j]
						+ (- 0.75f) * _grid[i + 2, j + 2]
						+  (- 0.25f) * _grid[i + 3, j]
						+ 0.25f * _grid[i + 3, j + 2];
					c[3, 2] = (- 0.5f) * _grid[i, j]
						+ 1.25f * _grid[i, j + 1] 
						- _grid[i, j + 2]
						+ 0.25f * _grid[i, j + 3] 
						+ 1.5f * _grid[i + 1, j]
						+  (-3.75f) * _grid[i + 1, j + 1] 
						+  3f * _grid[i + 1, j + 2]
						+ (- 0.75f) * _grid[i + 1, j + 3] 
						+ (- 1.5f) * _grid[i + 2, j]
						+  3.75f * _grid[i + 2, j + 1] 
						+ (-3f) * _grid[i + 2, j + 2]
						+ 0.75f * _grid[i + 2, j + 3]
						+  0.5f * _grid[i + 3, j]
						+  (- 1.25f) * _grid[i + 3, j + 1] 
						+ _grid[i + 3, j + 2]
						+ (- 0.25f) * _grid[i + 3, j + 3];
					c[3, 3] = 0.25f * _grid[i, j]
						+ (- 0.75f) * _grid[i, j + 1] 
						+  0.75f * _grid[i, j + 2]
						+ (-0.25f) * _grid[i, j + 3]
						+ (- 0.75f) * _grid[i + 1, j]
						+  2.25f * _grid[i + 1, j + 1]
						+  (- 2.25f) * _grid[i + 1, j + 2] 
						+ 0.75f * _grid[i + 1, j + 3] 
						+  0.75f * _grid[i + 2, j]
						+  (-2.25f) * _grid[i + 2, j + 1] 
						+ 2.25f * _grid[i + 2, j + 2]
						+ (- 0.75f) * _grid[i + 2, j + 3]
						+ (- 0.25f) * _grid[i + 3, j]
						+  0.75f * _grid[i + 3, j + 1] 
						+ (- 0.75f) * _grid[i + 3, j + 2] 
						+ 0.25f * _grid[i + 3, j + 3];

                    for (int k = 0; k < _step; ++k) 
                    {
                        float x = (float)k / _step;
                        for (int l = 0; l < _step; ++ l) 
                        {
                            float y = (float)l / _step;
                            double result = 0;

                            for (int idx1 = 0; idx1 < InterpolateOrder + 1; ++idx1) 
                            {
                                for (int idx2 = 0; idx2 < InterpolateOrder + 1; ++idx2) 
                                {
                                    result += c[idx1, idx2] * Math.Pow(x, idx1) * Math.Pow(y, idx2);
                                }
                            }
                            _values[i * _step + k, j * _step + l] = (byte)Round(result);
                           
                        }
                    }
				}
			}
		}

		public float GetColor(int x, int y)
		{
			return (float)(_values[x, y]);
		}

        public static double Round(double number)
        {
            if (number > 255)
            {
                return 255;
            }
            if (number < 0)
            {
                return 0;
            }

            return number;
        }
        
	}
}

