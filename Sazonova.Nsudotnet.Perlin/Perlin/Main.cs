
using System;
using System.IO;
using System.Drawing;

namespace Sazonova.DotNetCourse.Perlin
{
	class MainClass
	{
		static string Usage = "Use: Perlin.exe Size FileName";

		public static void Main (string[] args)
		{
			if (args.Length != 2) {
				throw new Exception (Usage);
			}
            int size = Convert.ToInt32(args[0], 10);
            float[] partition = new float[4];
            partition[0] = partition[1] = partition[2] = partition[3] = 0.25f;
            PerlinNoise noise = new PerlinNoise(size, partition);

            noise.Save(args[1]);
		}
	}
}
