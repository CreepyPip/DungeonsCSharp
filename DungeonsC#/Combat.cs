using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    public class Combat
    {
        private static readonly Random rnd = new Random();
        private int hpPlayer = 100;

        private void Battlefield()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i == 0 || i == 8 || j == 0 || j == 8) Console.Write("# ");
                    else if (i == 3 && j == 4) Console.Write($"{Types.EnemyType} ");
                    else if (i == 5 && j == 4) Console.Write("@ ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        public string[][] StartFight(string[][] A, int x, int y, Inventory Inv, bool type)
        {
            bool ff = Fighting(Inv, type);
            if (!ff) return [["false"]];
            else
            {
                if (x + 1 != A.Length - 1) { A[x + 1][y] = " "; }
                if (x - 1 != 0) { A[x - 1][y] = " "; }
                if (y + 1 != A[0].Length - 1) { A[x][y + 1] = " "; }
                if (y - 1 != 0) { A[x][y - 1] = " "; }
                GameFunctions.View(A, x, y);
            }
            return A;
        }

        private string InventoryInFight(Inventory Inv)
        {
            string ReturnText = "";

            List<string> Belt = Inv.OutBelt();
            if (Belt.Count != 0)
            {
                for (int i = 0; i < Belt.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {Belt[i]}");
                }
            } else return "";
            Console.WriteLine($"{Belt.Count + 1}. Выход из инвентаря");

            string? Input = Console.ReadLine();

            while (GameFunctions.IntOrNull(Input) == null && 
            GameFunctions.IntOrNull(Input) <= 0 && GameFunctions.IntOrNull(Input) <= Belt.Count)
            {
                Input = Console.ReadLine();
            }

            int IntInput = GameFunctions.IntOrNull(Input) ?? Belt.Count + 1;
            if (IntInput != Belt.Count+1)
            {
                ReturnText = Belt[IntInput - 1];
                Inv.DeleteFromBelt(IntInput - 1);
            }

            return ReturnText;
        }

        public bool Fighting(Inventory Inv,bool type)
        {
            int hpBot = 30;
            bool blow = false;
            int minDamageFromBot = 0;
            int maxDamageFromBot = 15;
            int minDamage = 0;
            int maxDamage = 15;

            while (hpPlayer > 0 && hpBot > 0)
            {
                Console.Clear();
                Battlefield();
                Console.WriteLine($"Здоровье игрока {hpPlayer}\nЗдоровье бота {hpBot}\n");

                if (!blow)
                {
                    Console.WriteLine("1. Быстрый удар\n2. Усиленный удар\n3. Инвентарь \n4. Бежать");
                    string? input = Console.ReadLine();
                    while (input != "1" && input != "2" && input != "3" && input != "4") input = Console.ReadLine();

                    if (input == "1")
                    {
                        int damage = rnd.Next(0, 16);
                        hpBot -= damage;
                        Console.WriteLine($"Нанесено урона тобой {damage}");
                        Thread.Sleep(1000);
                    }
                    if (input == "2") blow = true;
                    if (input == "3")
                    {
                        ConsoleUtils.ClearScreen();
                        string ItemFromBelt = InventoryInFight(Inv);
                        ConsoleUtils.ClearScreen();

                        if (ItemFromBelt == GameValues.Poisons[0])
                        {
                            Console.WriteLine("Побег");
                            if (type)
                            {
                                
                            }
                            Thread.Sleep(1500);
                            return true;
                        } else
                        if (ItemFromBelt == GameValues.Poisons[1])
                        {
                                Console.WriteLine("Ты сконцентрировался");
                                Thread.Sleep(1000);
                                blow = true;
                                continue;
                            } else
                        if (ItemFromBelt == GameValues.Poisons[2])
                        {
                            Console.WriteLine("Лечение");
                            Thread.Sleep(1000); 
                            hpPlayer += rnd.Next(15, 41);
                        } else
                        if (ItemFromBelt == GameValues.Poisons[3])
                        {
                            Console.WriteLine("Повышение защиты");
                            Thread.Sleep(1000);
                            maxDamageFromBot -= 8;
                        } else 
                        if (ItemFromBelt == GameValues.Poisons[4])
                        {
                            Console.WriteLine("Повышение защиты");
                            Thread.Sleep(1000);
                            minDamage += 5;
                            maxDamage += 10;
                        } else continue;
                    }
                    if (input == "4")
                    {
                        int escape = rnd.Next(0, 30);
                        if (escape > 20) return true;
                        else Console.WriteLine("Не удалось сбежать");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                    int damage = rnd.Next(minDamage + 10, maxDamage + 20);
                    hpBot -= damage;
                    blow = false;
                    Console.WriteLine($"Нанесено урона тобой {damage}");
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
                if (hpBot <= 0) break;
                int damageP = rnd.Next(minDamageFromBot, maxDamageFromBot);
                hpPlayer -= damageP;
                Console.WriteLine($"Нанесено урона тебе {damageP}");
                Thread.Sleep(1000);
            }

            Console.Clear();
            Console.WriteLine($"Здоровье игрока {hpPlayer}\nЗдоровье бота {hpBot}\n\nНажмите Enter");
            Console.ReadLine();
            return hpPlayer > 0;
        }
    }
}