using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace Dungeons
{
    class Program
    {
        public static readonly Random Rnd = new Random();

        static void Main()
        {
            int height = 200;
            int width = 100;
            bool game = true;
            bool game2 = false;

            if (OperatingSystem.IsWindows())
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
                ConsoleUtils.ConsoleColor();
            }

            while (game)
            {
                Inventory inv = new Inventory();
                List<string> Chest = GameFunctions.FromFile();
                List<string> Belt = GameFunctions.BeltFromFile();
                double Money = GameFunctions.MoneyFromFile();
                int floor = GameFunctions.FloorFromFile();
                for (int i = 0; i < Chest.Count; i++)
                {
                    inv.InChest(Chest[i]);
                }
                for (int i = 0; i < Belt.Count; i++)
                {
                    inv.InBelt(Belt[i]);
                }
                inv.AddMoney(Money);

                if (floor > 7) {floor = 1;}

                Console.WriteLine("1. Продолжить");
                Console.WriteLine("2. Начать игру");
                Console.WriteLine("3. Бесконечный режим");
                Console.WriteLine("4. Открыть свой сундук");
                Console.WriteLine("5. Торговец");
                Console.WriteLine("6. Посмотреть рекорды в бесконечном режиме");
                Console.WriteLine("7. Закрыть игру");

                string? answer = Console.ReadLine();
                const string continueGame = "1";
                const string newGame = "2";
                const string noEndGame = "3";
                const string openChest = "4";
                const string dealerInput = "5";
                const string records = "6";
                bool type = true;
                InfinityMode inf = new InfinityMode();

                switch (answer)
                {
                    case continueGame:
                        inv.FreeChest();
                        List<string> arrFromFile = GameFunctions.FromFile();
                        for (int i = 0; i < arrFromFile.Count; i++)
                        {
                            inv.InChest(arrFromFile[i]);
                        }
                        game2 = true;
                        break;
                    case newGame:
                        GameFunctions.FreeFile(); 
                        inv.FreeBelt();
                        inv.FreeChest();
                        inv.PutMoney(inv.GetMoney());
                        floor = 1;
                        game2 = true;
                        break;
                    case openChest:
                        List<string> arr1FromFile = GameFunctions.FromFile();
                        Console.WriteLine("У вас в сундуке:");
                        for (int i = 0; i < arr1FromFile.Count; i++)
                        {
                            Console.WriteLine(arr1FromFile[i]);
                        }
                        break;
                    case noEndGame:
                        type = false;
                        game2 = true;
                        break;
                    case dealerInput:
                        Dealer.Trade(inv);
                        break;
                    case records:
                        List<string> RecordsOut = GameFunctions.FromRecordsFile();
                        for (int i = 0; i < RecordsOut.Count; i++)
                        {
                            Console.WriteLine(RecordsOut[i]);
                        }
                        break;
                    default:
                        game = false; 
                        break;
                }

                // Для проверки нахождения выхода
                bool exit = false;
                Combat fight = new Combat();
                Inventory InvIG = new Inventory();

                if (game2)
                {
                    if (type)
                    {
                        ConsoleUtils.ClearScreen();
                        Console.WriteLine($"Этаж {floor}");
                        Thread.Sleep(1500);
                        height = (int)(0.25 * (floor - 1) * height) + height;
                        width = (int)(0.25 * (floor - 1) * width) + width;
                        exit = Gameplay.PlayingField(width, height, inv, fight, type);
                    } else
                    {
                        bool infExit = true;

                        while (infExit)
                        {
                            ConsoleUtils.ClearScreen();
                            List<string> Items = GameFunctions.RandomItemInfMode();
                            Console.WriteLine("Вы получили:");
                            for (int i = 0; i < Items.Count; i++)
                            {
                                Console.WriteLine(Items[i]);
                                InvIG.InBelt(Items[i]);
                            }
                            Thread.Sleep(2000);
                            infExit = Gameplay.PlayingField(width, height, InvIG, fight, type);
                            inf.LevelPlus();
                        }
                    }

                    Console.Clear();
                    if (!type)
                    {
                        int L = inf.GetLevel();
                        double LS = L * 10.0;
                        double S = inf.Score(InvIG.OutBag());
                        double Scores = LS + S;
                        Console.WriteLine($"Вы получили {S} очков");
                        Console.WriteLine($"И прошли {L} этажей, и получили {LS} очков");
                        Console.WriteLine($"В целом {Scores} очков");
                        Console.WriteLine("/nВведите имя: ");
                        answer = Console.ReadLine();
                        if (answer == null) 
                        {
                            answer = "noname";
                        }
                        GameFunctions.InRecordsFile(Scores, answer);
                    }

                    if (exit)
                    {
                        Console.WriteLine("Вы дошли до конца");
                        Console.WriteLine("Вы собрали за забег:");
                        List<string> arrBag = inv.OutBag();
                        GameFunctions.InFile(arrBag);
                        if (arrBag.Count > 0)
                        {
                            for (int i = 0; i < arrBag.Count; i++)
                            {
                                inv.InChest(arrBag[i]);
                                Console.WriteLine(arrBag[i]);
                            }
                        }
                        Thread.Sleep(1500);
                        if (floor == 7)
                        {
                            Console.WriteLine("Вы прошли подземелье");
                            Thread.Sleep(2500);
                            floor = 1;
                            break;
                        }
                        floor += 1;
                    } else floor = 1;
                    GameFunctions.FloorInFile(floor);
                    inv.FreeBag();
                    game2 = false;
                }
            }
        }

    }
}