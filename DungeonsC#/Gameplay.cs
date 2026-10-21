using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Dungeons
{
    public static class Types
    {
        public const string Reset = "\x1b[0m";
        public const string Red = "\x1b[31m";
        public const string Scarlet = "\x1b[91m";
        public const string Green = "\x1b[32m";

        public static string EnemyType = $"{Red}&{Reset}";
        public static string BossType = $"{Scarlet}&{Reset}";
        public static string ChestType = $"{Green}?{Reset}";
    }



    public static class Gameplay
    {
        public static bool PlayingField(int width, int height, Inventory inv, Combat fight, bool type)
        {
            char[] dungeon = EnemySpawner.GenerateDungeon(width, height, 30);

            string[][] A = new string[height][];
            int x = height - 2;
            int y = (width / 2) - 1;

            for (int i = 0; i < height; i++)
            {
                A[i] = new string[width];
                for (int j = 0; j < width; j++)
                {

                    if (dungeon[i * width + j] == '$')
                    {
                        A[i][j] = Types.BossType;
                    }
                    else if(dungeon[i * width + j] == '&')
                    {
                        A[i][j] = Types.EnemyType;
                    } 
                    else if (dungeon[i * width + j] == '?')
                    {
                        A[i][j] = Types.ChestType;
                    }
                    else
                    {
                        char pretrans = dungeon[i * width + j];
                        A[i][j] = pretrans.ToString();
                        if (pretrans == 'S') y = j;
                    }
                }
            }

            A[x][y] = "@";
            bool inGame = true;
            List<string> outputItems = new List<string>();
            int itemsCount = 0;

            while (inGame)
            {
                Console.Clear();
                GameFunctions.View(A, x, y, width, height);

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
                const char wKey = 'w';
                const char aKey = 'a';
                const char sKey = 's';
                const char dKey = 'd';
                const char eKey = 'e';

                if ((input == wKey && A[x - 1][y] == Types.BossType) || (input == aKey && A[x][y - 1] == Types.BossType) ||
                    (input == sKey && A[x + 1][y] == Types.BossType) || (input == dKey && A[x][y + 1] == Types.BossType))
                {
                    string[][] AA = fight.StartFight(A, x, y, inv, type, width, height, true);
                    if (AA[0][0] != "false")
                    {
                        A = AA;
                    }
                    else
                    {
                        inGame = false;
                        break;
                    }
                }
                else
                if ((input == wKey && A[x - 1][y] == Types.EnemyType) || (input == aKey && A[x][y - 1] == Types.EnemyType) ||
                    (input == sKey && A[x + 1][y] == Types.EnemyType) || (input == dKey && A[x][y + 1] == Types.EnemyType))
                {
                    string[][] AA = fight.StartFight(A, x, y, inv, type, width, height, false);
                    if (AA[0][0] != "false")
                    {
                        A = AA;
                    }
                    else
                    {
                        inGame = false;
                        break;
                    }
                }

                if (input == wKey && A[x - 1][y] == "E") { return true; }
                if (input == wKey && A[x - 1][y] == " ") { A[x][y] = " "; x--; A[x][y] = "@"; }
                if (input == aKey && A[x][y - 1] == " ") { A[x][y] = " "; y--; A[x][y] = "@"; }
                if (input == sKey && A[x + 1][y] == " ") { A[x][y] = " "; x++; A[x][y] = "@"; }
                if (input == dKey && A[x][y + 1] == " ") { A[x][y] = " "; y++; A[x][y] = "@"; }

                if (input == eKey && (A[x][y + 1] == Types.ChestType ||
                A[x][y - 1] == Types.ChestType || A[x + 1][y] == Types.ChestType ||
                A[x - 1][y] == Types.ChestType))
                {
                    if (A[x][y + 1] == Types.ChestType) { A[x][y + 1] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                    if (A[x][y - 1] == Types.ChestType) { A[x][y - 1] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                    if (A[x + 1][y] == Types.ChestType) { A[x + 1][y] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                    if (A[x - 1][y] == Types.ChestType) { A[x - 1][y] = " "; outputItems.Add(GameFunctions.RandomItems()); }
                    itemsCount = 0;
                }

                string[][] iE = EnemyLogic.IfEnemy(A, x, y, width, height);
                if (iE.Length > 0 && iE[0].Length > 0 && iE[0][0] == "fight")
                {
                    string[][] AA = fight.StartFight(A, x, y, inv, type, width, height, false);
                    if (AA[0][0] != "false")
                    {
                        A = AA;
                    }
                    else
                    {
                        inGame = false;
                        break;
                    }
                }
                else { A = iE; }


            }
            return false;
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