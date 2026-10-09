namespace Dungeons
{
    public static class Dealer
    {
        public static void Trade(Inventory inv)
        {
            List<string> Chest = inv.OutChest();
            List<string> Belt = inv.OutBelt();


            Console.WriteLine($"У вас на счету {inv.GetMoney()}");

            Console.WriteLine("1.Купить");
            Console.WriteLine("2.Продать");
            Console.WriteLine("3.Выйти");
            const string Sell = "1";
            const string Buy = "2";
            const string Exit = "3";

            string? Input = Console.ReadLine();
            while (Input != Sell && Input != Buy && Input!= Exit)
            {
                Input = Console.ReadLine();
            }

            switch (Input)
            {
                case (Sell):

                    break;
                case (Buy):

                    break;
                default:

                    break;
            }
        }
    }
}