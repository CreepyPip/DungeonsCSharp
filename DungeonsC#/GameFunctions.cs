using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dungeons
{
    public static class GameFunctions
    {
        public static string RandomItems()
        {
            int item = Program.Rnd.Next(0, 100);
            if (item >= 0 && item <= 15) return "Ржавый меч";
            if (item >= 16 && item <= 35) return "Ржавая броня";
            if (item >= 36 && item <= 46) return "Маленький мешок с монетами";
            if (item >= 47 && item <= 52) return "Средний мешок с монетами";
            if (item >= 53 && item <= 57) return "Большой мешок с монетами";
            if (item >= 58 && item <= 70) return "Чьи-то кости";
            if (item >= 71 && item <= 75) return "Почти новый меч";
            if (item >= 76 && item <= 79) return "Почти новая броня";
            if (item >= 80 && item <= 94) return "Мусор";
            if (item == 95) return "Золото";
            return "Пустой";
        }

        public static void FreeFile() => File.WriteAllText("PlayerChest.sosal", "");

        public static void InFile(List<string> arr)
        {
            List<string> arra = new List<string>(arr) { "" };
            string text = string.Join("\n", arra);
            File.AppendAllText("PlayerChest.sosal", text);
        }

        public static List<string> FromFile()
        {
            if (File.Exists("PlayerChest.sosal"))
                return new List<string>(File.ReadAllText("PlayerChest.sosal").Split('\n'));
            return new List<string>();
        }

        public static void View(string[][] A, int x, int y)
        {
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
                for (int j = yl; j < yr; j++) Console.Write(A[i][j] + " ");
                Console.WriteLine();
            }
        }
    }
}