using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Пойдёт на замену MazeGenerator

namespace Dungeons
{
    public static class Generator
    {
        public static char[,] GenerateMaze(int width, int height, int density)
        {
            char[,] A = new char[height, width];
            for (int i = 0; i < height; i++) 
            {
                for (int j = 0; j < width; j++)
                {
                    A[i] = "#";
                }
                
            }

            int checkpoints = ((width * height) / 50) + 1;
            int count = 1;
            int[,] points = new int[checkpoints + 2, 2];

            int b = (Program.Rnd.Next(width - 2)) + 1;
            A[0][b] = "E";
            points[0] = b + width;

            b = height - (Program.Rnd.Next(width - 2));
            A[height][b] = "S";

            double chance = ((double)checkpoints / ((double)width * (double)height)) * 100;

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (A[i - 1][j] == "E" || A[i + 1][j] == "S") A[i][j] = " ";
                    if (checkpoints != 0 && Program.Rnd.Next(100) < chance && A[i][j] != ' ' && i != 1 && i != height - 1)
                    {
                        A[i][j] = ' ';
                        points[count][0] = i;
                        points[count][1] = i;
                        count++;
                        checkpoints--;
                        break;
                    }
                }
            }
            points[count][0] = [height-1];
            points[count][1] = [b];

            for (int c = 0; c < count; c++)
            {
                int there = points[c];
                int minus = points[c];
                int larger = points[c + 1];

                while (there != points[c + 1])
                {
                    while (minus[0] - 1 > 0) minus[0] -= 1;
                    while (larger[0] - 1 > 0) larger[0] -= 1;
                    while (minus[1] - 1 > 0) minus[1] -= 1;
                    while (larger[1] - 1 > 0) larger[1] -= 1;

                    if (larger[0] - minus[0] == 0)
                    {
                        if (there[0] < points[c + 1][0]) { A[there[0] + 1] = " "; there[0] += 1; }
                        if (there[0] > points[c + 1][0]) { A[there[0] - 1] = " "; there[0] -= 1; }
                    }
                    if (larger[1] - minus[1] < 0) { A[there[1] - 1] = " "; there[1]--; minus[1]--; }
                    if (larger[1] - minus[1] > 0) { A[there[1] + 1] = " "; there[1]++; minus[1]++; }
                }
            }



            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (((GetSafe(A, i - 1, j) == " " || GetSafe(A, i - 2, j) == " " || GetSafe(A, i, j - 1) == " " ||
                          GetSafe(A, i - 1, j - 1) == " " || GetSafe(A, i - 1, j - 2) == " " || GetSafe(A, i + 1, j) == " " ||
                          GetSafe(A, i + 2, j) == " " || GetSafe(A, i + 1, j + 2) == " " || GetSafe(A, i + 1, j + 1) == " ") && 
                          Program.Rnd.Next(100) < density) ||
                         Program.Rnd.Next(100) < density / 1.5)
                    {
                        A[i][j] = " ";
                    }
                }
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if ((GetSafe(A, i - 1, j) == " " || GetSafe(A, i, j) == " " || GetSafe(A, i, j - 1) == " " ||
                         GetSafe(A, i - 1, j - 1) == " " || GetSafe(A, i - 1, j - 2) == " " || GetSafe(A, i + 1, j) == " " ||
                         GetSafe(A, i + 1, j + 2) == " " || GetSafe(A, i + 1, j + 1) == " ") && 
                         Program.Rnd.Next(300) < 1)
                    {
                        A[i][j] = "?";
                    }
                }
            }
            return A;
        }

        private static char GetSafe(char[,] A, int i, int j, int height, int width)
        {
            if (i >= 0 && i < height && j >= 0 && j < width)
            {
                return A[i, j];
            }
            return '\0';
        }
    }
}