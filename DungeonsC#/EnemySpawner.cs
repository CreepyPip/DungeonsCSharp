using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    public static class EnemySpawner
    {
        public static char[] GenerateDungeon(int width, int height, int density)
        {
            char[] A = MazeGenerator.GenerateMaze(width, height, density);
            double enemyChance = (50.0 / 20000.0) * 100.0;

            for (int i = 1; i < height; i++)
            {
                for (int j = 1; j < width; j++)
                {
                    int index = i * width + j;
                    if (A[index] != '#' && A[index] != '?' && A[index] != 'E' && A[index] != 'S' && Program.Rnd.Next(100) < enemyChance)
                    {
                        A[index] = '&';
                    }
                }
            }
            return A;
        }
    }
}