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

        public bool Fighting()
        {
            int hpBot = 30;
            bool blow = false;

            while (hpPlayer > 0 && hpBot > 0)
            {
                Console.Clear();
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
                Console.WriteLine($"Здоровье игрока {hpPlayer}\nЗдоровье бота {hpBot}\n");

                if (!blow)
                {
                    Console.WriteLine("1. Быстрый удар\n2. Усиленный удар\n3. Бежать");
                    string input = Console.ReadLine();
                    while (input != "1" && input != "2" && input != "3") input = Console.ReadLine();

                    if (input == "1")
                    {
                        int damage = rnd.Next(0, 16);
                        hpBot -= damage;
                    }
                    if (input == "2") blow = true;
                    if (input == "3")
                    {
                        int escape = rnd.Next(0, 30);
                        if (escape > 20) return true;
                        else Console.WriteLine("Не удалось");
                    }
                }
                else
                {
                    int damage = rnd.Next(10, 36);
                    hpBot -= damage;
                    blow = false;
                }

                if (hpBot <= 0) break;
                hpPlayer -= rnd.Next(0, 15);
            }

            Console.Clear();
            Console.WriteLine($"Здоровье игрока {hpPlayer}\nЗдоровье бота {hpBot}\n\nНажмите Enter");
            Console.ReadLine();
            return hpPlayer > 0;
        }
    }
}