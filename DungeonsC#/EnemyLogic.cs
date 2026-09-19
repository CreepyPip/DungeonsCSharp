using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    public static class EnemyLogic
    {
        public static int[] EnemyMove(string[][] A, int x, int y)
        {
            if ((A[x + 1][y] == "@" || A[x - 1][y] == "@" || A[x][y + 1] == "@" || A[x][y - 1] == "@") && Program.Rnd.Next(0, 2) == 1)
                return new int[] { 11111 };
            if (A[x + 1][y] == " " && Program.Rnd.Next(0, 5) < 1) return new int[] { x + 1, y };
            if (A[x - 1][y] == " " && Program.Rnd.Next(0, 5) < 1) return new int[] { x - 1, y };
            if (A[x][y + 1] == " " && Program.Rnd.Next(0, 5) < 1) return new int[] { x, y + 1 };
            if (A[x][y - 1] == " " && Program.Rnd.Next(0, 5) < 1) return new int[] { x, y - 1 };
            return new int[0];
        }

        public static string[][] IfEnemy(string[][] A, int x, int y)
        {
            string[][] B = new string[A.Length][];
            for (int i = 0; i < A.Length; i++) B[i] = (string[])A[i].Clone();

            int xh = x - 10;
            int xl = x + 10;
            int yl = y - 10;
            int yr = y + 10;

            while (xh < 0) xh++;
            while (xl > 200) xl--;
            while (yl < 0) yl++;
            while (yr > 100) yr--;

            for (int i = xh; i < xl; i++)
            {
                for (int j = yl; j < yr; j++)
                {
                    if (A[i][j] == "&")
                    {
                        int[] em = EnemyMove(A, i, j);
                        if (em.Length > 0)
                        {
                            if (em[0] == 11111) return new string[][] { new string[] { "fight" } };
                            else { B[i][j] = " "; B[em[0]][em[1]] = "&"; }
                        }
                    }
                }
            }
            return B;
        }
    }
}