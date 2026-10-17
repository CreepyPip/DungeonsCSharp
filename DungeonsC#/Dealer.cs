using System.Reflection.Metadata.Ecma335;

namespace Dungeons
{
    public static class Dealer
    {
        public static void Trade(Inventory inv)
        {
            List<string> Chest = inv.OutChest();
            List<string> Belt = inv.OutBelt();
            double Money = inv.GetMoney();

            Console.WriteLine($"У вас на счету {inv.GetMoney()}");

            Console.WriteLine("1.Купить");
            Console.WriteLine("2.Продать");
            Console.WriteLine("3.Выйти");
            const string Buy = "1";
            const string Sell = "2";
            const string Exit = "3";

            string? Input = Console.ReadLine();
            while (Input != Sell && Input != Buy && Input!= Exit)
            {
                Input = Console.ReadLine();
            }

            switch (Input)
            {
                case (Sell):
                    while (true)
                    {
                        ConsoleUtils.ClearScreen();
                        for (int i = 0; i < Chest.Count; i++)
                        {
                            Console.WriteLine($"{i+1}. {Chest[i]}");
                        }
                        Console.WriteLine($"{Chest.Count+1}. Выйти");

                        Input = Console.ReadLine();
                        int? parsedInput = GameFunctions.IntOrNull(Input);
                        int IntInput = parsedInput ?? 0;
                        if (IntInput >= 1 && IntInput <= Chest.Count)
                        {
                            int index = IntInput - 1;
                            string itemToSell = Chest[index];
                            double price = GameValues.ItemsScore[itemToSell];

                            GameFunctions.FreeFile();
                            inv.DeleteFromChest(index);
                            inv.AddMoney(price);

                            Chest.RemoveAt(index);
                            Money += price;

                            GameFunctions.InFile(Chest);
                            GameFunctions.BeltInFile(Belt);
                            GameFunctions.MoneyInFile(Money);
                        } else return;
                    }
                case (Buy):
                    while (true)
                    {
                        ConsoleUtils.ClearScreen();
                        for (int i = 0; i < GameValues.Poisons.Count; i++)
                        {
                            Console.WriteLine($"{i+1}.{GameValues.Poisons[i]}: {GameValues.PoisonsPrice[i]}");
                        }
                        Console.WriteLine($"{GameValues.Poisons.Count + 1}. Выйти");

                        Input = Console.ReadLine();
                        int IntInput = GameFunctions.IntOrNull(Input) ?? 0;

                        if (IntInput >= 1 && IntInput <= GameValues.Poisons.Count)
                        {
                            if (Money >= GameValues.PoisonsPrice[IntInput-1])
                            {
                                GameFunctions.FreeFile();
                                inv.PutMoney(GameValues.PoisonsPrice[IntInput - 1]);
                                inv.InBelt(GameValues.Poisons[IntInput - 1]);

                                Belt.Add(GameValues.Poisons[IntInput - 1]);
                                Money -= GameValues.PoisonsPrice[IntInput - 1];

                                GameFunctions.InFile(Chest);
                                GameFunctions.BeltInFile(Belt);
                                GameFunctions.MoneyInFile(Money);
                            }
                        }
                        else return;
                    }
                default:
                    return;
            }
        }
    }
}