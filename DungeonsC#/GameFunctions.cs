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

        public static List<string> Poisons = ["Зелье скрытности", "Зелье концентрации", "Зелье здоровья", "Зелье защиты", "Зелье силы"];
        public static List<int> PoisonsPrice = [15, 2, 8, 5, 5];

        public static void FreeFile()
        {
            File.WriteAllText("PlayerChest.sosal", "");
            File.WriteAllText("PlayerBelt.sosal", "");
            File.WriteAllText("PlayerMoney.sosal", "");
        } 

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

        public static void BeltInFile(List<string> arr, Double money)
        {
            List<string> arra = new List<string>(arr) { "" };
            string text = string.Join("\n", arra);
            File.AppendAllText("PlayerBelt.sosal", text);
        }

        public static List<string> BeltFromFile()
        {
            if (File.Exists("PlayerBelt.sosal"))
                return new List<string>(File.ReadAllText("PlayerBelt.sosal").Split('\n'));
            return new List<string>();
        }

        public static List<string> FromRecordsFile()
        {
            if (File.Exists("PlayerRecords.sosal"))
                return new List<string>(File.ReadAllText("PlayerRecords.sosal").Split('\n'));
            return new List<string>();
        }

        public static void InRecordsFile(double Record, string Name)
        {
            string RecordText = Record + " " + Name;
            List<string> arra = FromRecordsFile();
            arra.Append(RecordText);
            string text = string.Join("\n", arra);
            File.AppendAllText("PlayerRecords.sosal", text);
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

        public static List<string> RandomItemInfMode()
        {
            List<string> ReturnPoisons = new List<string>();

            int Count = Program.Rnd.Next(1, 4);
            for (int i = 0; i < Count; i++)
            {
                ReturnPoisons.Add(Poisons[Program.Rnd.Next(0, 5)]);
            }

            return ReturnPoisons;
        }

        public static int? IntOrNull(string input)
        {
            return int.TryParse(input, out int result) ? result : null;
        }
    }
}