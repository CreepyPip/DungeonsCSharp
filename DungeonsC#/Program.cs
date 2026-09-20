using System;
using System.Collections.Generic;

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

            while (game)
            {
                Inventory inv = new Inventory();

                Console.WriteLine("1. Продолжить");
                Console.WriteLine("2. Начать игру");
                Console.WriteLine("3. Закрыть игру");
                Console.WriteLine("4. Открыть свой сундук");

                string answer = Console.ReadLine();

                if (answer == "2") { GameFunctions.FreeFile(); game2 = true; }
                if (answer == "1")
                {
                    inv.FreeChest();
                    List<string> arrFromFile = GameFunctions.FromFile();
                    for (int i = 0; i < arrFromFile.Count; i++)
                    {
                        inv.InChest(arrFromFile[i]);
                    }
                    game2 = true;
                }
                if (answer == "4")
                {
                    List<string> arrFromFile = GameFunctions.FromFile();
                    Console.WriteLine("У вас в сундуке:");
                    for (int i = 0; i < arrFromFile.Count; i++)
                    {
                        Console.WriteLine(arrFromFile[i]);
                    }
                }
                if (answer != "1" && answer != "2" && answer != "4") { game = false; break; }

                // Для проверки нахождения выхода
                bool exit = false;

                if (game2)
                {
                    char[] dungeon = EnemySpawner.GenerateDungeon(width, height, 30);
                    Combat fight = new Combat();

                    string[][] A = new string[height][];
                    int x = height - 2;
                    int y = (width / 2) - 1;

                    for (int i = 0; i < height; i++)
                    {
                        A[i] = new string[width];
                        for (int j = 0; j < width; j++)
                        {
                            char pretrans = dungeon[i * width + j];
                            A[i][j] = pretrans.ToString();
                            if (pretrans == 'S') y = j;
                        }
                    }

                    A[x][y] = "@";
                    bool inGame = true;
                    List<string> outputItems = new List<string>();
                    int itemsCount = 0;

                    while (inGame)
                    {
                        Console.Clear();
                        GameFunctions.View(A, x, y);

                        if (outputItems.Count > 0)
                        {
                            for (int i = 0; i < outputItems.Count; i++)
                            {
                                Console.WriteLine(outputItems[i]);
                            }
                            itemsCount++;
                        }

                        if (itemsCount == 3)
                        {
                            for (int i = 0; i < outputItems.Count; i++)
                            {
                                if (outputItems[i] != "Пустой")
                                {
                                    inv.InBag(outputItems[i]);
                                }
                            }
                            itemsCount = 0;
                            outputItems.Clear();
                        }

                        char input = GetAction();

                        if ((input == 'w' && A[x - 1][y] == "&") || (input == 'a' && A[x][y - 1] == "&") ||
                            (input == 's' && A[x + 1][y] == "&") || (input == 'd' && A[x][y + 1] == "&"))
                        {
                            bool ff = fight.Fighting();
                            if (!ff) inGame = false;
                            else { A[x + 1][y] = " "; A[x - 1][y] = " "; A[x][y + 1] = " "; A[x][y - 1] = " "; GameFunctions.View(A, x, y); }
                        }

                        if (input == 'w' && A[x - 1][y] == "E") { exit = true; inGame = false; }
                        if (input == 'w' && A[x - 1][y] == " ") { A[x][y] = " "; x--; A[x][y] = "@"; }
                        if (input == 'a' && A[x][y - 1] == " ") { A[x][y] = " "; y--; A[x][y] = "@"; }
                        if (input == 's' && A[x + 1][y] == " ") { A[x][y] = " "; x++; A[x][y] = "@"; }
                        if (input == 'd' && A[x][y + 1] == " ") { A[x][y] = " "; y++; A[x][y] = "@"; }

                        if (input == 'e' && (A[x][y + 1] == "?" || A[x][y - 1] == "?" || A[x + 1][y] == "?" || A[x - 1][y] == "?"))
                        {
                            if (A[x][y + 1] == "?") { A[x][y + 1] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                            if (A[x][y - 1] == "?") { A[x][y - 1] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                            if (A[x + 1][y] == "?") { A[x + 1][y] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                            if (A[x - 1][y] == "?") { A[x - 1][y] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                            itemsCount = 0;
                        }

                        string[][] iE = EnemyLogic.IfEnemy(A, x, y);
                        if (iE.Length > 0 && iE[0].Length > 0 && iE[0][0] == "fight")
                        {
                            bool fif = fight.Fighting();
                            if (!fif) inGame = false;
                            else 
                            {
                                if (x + 1 != height) { A[x + 1][y] = " "; }
                                if (x - 1 != 0) { A[x - 1][y] = " "; }
                                if (y + 1 != width) { A[x][y + 1] = " "; }
                                if (y - 1 != 0) { A[x][y - 1] = " "; }
                                GameFunctions.View(A, x, y); 
                            }
                        }
                        else { A = iE; }
                    }

                    Console.Clear();
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
                    }
                    inv.FreeBag();
                    game2 = false;
                }
            }
        }

        static char GetAction()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == 'w' || key.KeyChar == 'a' || key.KeyChar == 's' || key.KeyChar == 'd' || key.KeyChar == 'e')
                    return key.KeyChar;
            }
        }
    }
}