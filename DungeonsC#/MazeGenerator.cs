using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    public static class MazeGenerator
    {
        public static char[] GenerateMaze(int width, int height, int density)
        {
            char[] A = new char[width * height];
            for (int i = 0; i < height * width; i++) A[i] = '#';

            int checkpoints = ((width * height) / 50) + 1;
            int count = 1;
            int[] points = new int[checkpoints + 2];

            int b = (Program.Rnd.Next(width - 2)) + 1;
            A[b] = 'E';
            points[0] = b + width;

            b = width * height - (Program.Rnd.Next(width - 2));
            A[b] = 'S';

            double chance = ((double)checkpoints / ((double)width * (double)height)) * 100;

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    int index = i * width + j;
                    if (A[index - width] == 'E' || A[index + width] == 'S') A[index] = ' ';
                    if (checkpoints != 0 && Program.Rnd.Next(100) < chance && A[index] != ' ' && i != 1 && i != height - 1)
                    {
                        A[index] = ' ';
                        points[count] = index;
                        count++;
                        checkpoints--;
                        break;
                    }
                }
            }
            points[count] = b - width;

            for (int c = 0; c < count; c++)
            {
                int there = points[c];
                int minus = points[c];
                int larger = points[c + 1];

                while (there != points[c + 1])
                {
                    while (minus - width > 0) minus -= width;
                    while (larger - width > 0) larger -= width;

                    if (larger - minus == 0)
                    {
                        if (there < points[c + 1]) { A[there + width] = ' '; there += width; }
                        if (there > points[c + 1]) { A[there - width] = ' '; there -= width; }
                    }
                    if (larger - minus < 0) { A[there - 1] = ' '; there--; minus--; }
                    if (larger - minus > 0) { A[there + 1] = ' '; there++; minus++; }
                }
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    int index = i * width + j;
                    if (((GetSafe(A, index - width) == ' ' || GetSafe(A, index - 2 * width) == ' ' || GetSafe(A, index - 1) == ' ' ||
                          GetSafe(A, index - (width - 1)) == ' ' || GetSafe(A, index - (width - 2)) == ' ' || GetSafe(A, index + width) == ' ' ||
                          GetSafe(A, index + 2 * width) == ' ' || GetSafe(A, index + width + 2) == ' ' || GetSafe(A, index + width + 1) == ' ') && Program.Rnd.Next(100) < density) ||
                         Program.Rnd.Next(100) < density / 1.5)
                    {
                        A[index] = ' ';
                    }
                }
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    int index = i * width + j;
                    if ((GetSafe(A, index - width) == ' ' || GetSafe(A, index) == ' ' || GetSafe(A, index - 1) == ' ' ||
                         GetSafe(A, index - (width - 1)) == ' ' || GetSafe(A, index - (width - 2)) == ' ' || GetSafe(A, index + width) == ' ' ||
                         GetSafe(A, index + width + 2) == ' ' || GetSafe(A, index + width + 1) == ' ') && Program.Rnd.Next(300) < 1)
                    {
                        A[index] = '?';
                    }
                }
            }
            return A;
        }

        private static char GetSafe(char[] A, int index) => (index >= 0 && index < A.Length) ? A[index] : '\0';
    }
}