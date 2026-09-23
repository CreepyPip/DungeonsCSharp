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
                    else if (i == 3 && j == 4) Console.Write("& ");
                    else if (i == 5 && j == 4) Console.Write("@ ");
                    else Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        public string[][] StartFight(string[][] A, int x, int y)
        {
            bool ff = Fighting();
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

        public bool Fighting()
        {
            int hpBot = 30;
            bool blow = false;

            while (hpPlayer > 0 && hpBot > 0)
            {
                Console.Clear();
                Battlefield();
                Console.WriteLine($"Здоровье игрока {hpPlayer}\nЗдоровье бота {hpBot}\n");

                if (!blow)
                {
                    Console.WriteLine("1. Быстрый удар\n2. Усиленный удар\n3. Бежать");
                    string? input = Console.ReadLine();
                    while (input != "1" && input != "2" && input != "3") input = Console.ReadLine();

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
                        int escape = rnd.Next(0, 30);
                        if (escape > 20) return true;
                        else Console.WriteLine("Не удалось сбежать");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                    int damage = rnd.Next(10, 36);
                    hpBot -= damage;
                    blow = false;
                    Console.WriteLine($"Нанесено урона тобой {damage}");
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
                if (hpBot <= 0) break;
                int damageP = rnd.Next(0, 15);
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