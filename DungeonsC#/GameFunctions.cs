using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dungeons
{
    public static class GameValues
    {
        public static Dictionary<string, double> ItemsScore = new Dictionary<string, double>
        {
            {"Ржавый меч", 1.0},
            {"Ржавая броня", 2.0},
            {"Маленький мешок с монетами", 3.0},
            {"Средний мешок с монетами", 5.0},
            {"Большой мешок с монетами", 8.0},
            {"Почти новый меч", 10.0},
            {"Почти новая броня", 13.0},
            {"Золото", 20.0},
            {"Чьи-то кости", 0.1},
            {"Мусор", 0.2}
        };


        public static List<string> Poisons = ["Зелье скрытности", "Зелье концентрации", "Зелье здоровья", "Зелье защиты", "Зелье силы"];
        public static List<int> PoisonsPrice = [15, 2, 8, 5, 5];

    }

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
            if (!File.Exists("PlayerChest.sosal"))
            {
                using (File.Create("PlayerChest.sosal")) { }
            }
            File.AppendAllText("PlayerChest.sosal", text);
        }

        public static List<string> FromFile()
        {
            if (File.Exists("PlayerChest.sosal"))
            {
                string fullText = File.ReadAllText("PlayerChest.sosal");

                List<string> arr1 = new List<string>(
                    fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
                return arr1;
            } else
            {
                using (File.Create("PlayerChest.sosal")) { }
            }

            return new List<string>();
        }

        public static void BeltInFile(List<string> arr)
        {
            string text = string.Join("\n", arr);
            File.WriteAllText("PlayerBelt.sosal", text);
        }

        public static List<string> BeltFromFile()
        {
            if (File.Exists("PlayerBelt.sosal"))
            {
                string fullText = File.ReadAllText("PlayerBelt.sosal");
                return new List<string>(
                    fullText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                );
            }
            return new List<string>();
        }

        public static void MoneyInFile(double money)
        {
            string text = $"{money*10}";
            File.WriteAllText("PlayerMoney.sosal", text);
        }

        public static double MoneyFromFile()
        {
            if (File.Exists("PlayerMoney.sosal"))
            {
                string text = File.ReadAllText("PlayerMoney.sosal");
                double money = (GameFunctions.IntOrNull(text) ?? 0.0)/10.0;
                return money;
            }
            return 0.0;
        }

        public static List<string> FromRecordsFile()
        {
            if (File.Exists("PlayerRecords.sosal"))
                return new List<string>(File.ReadAllLines("PlayerRecords.sosal"));
            return new List<string>();
        }

        public static void InRecordsFile(double Record, string Name)
        {
            string RecordText = Record + " " + Name;
            List<string> arra = FromRecordsFile();
            arra.Add(RecordText);
            string text = string.Join("\n", arra);
            File.WriteAllText("PlayerRecords.sosal", text);
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
                ReturnPoisons.Add(GameValues.Poisons[Program.Rnd.Next(0, 5)]);
            }

            return ReturnPoisons;
        }

        public static int? IntOrNull(string input)
        {
            return int.TryParse(input, out int result) ? result : null;
        }
    }
}