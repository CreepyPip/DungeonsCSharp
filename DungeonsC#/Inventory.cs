using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons
{
    public class Inventory
    {
        private List<string> Bag = new List<string>();
        private List<string> Chest = new List<string>();
        private List<string> Belt = new List<string>();
        private Double Money = 0.0;

        public void InBag(string item) => Bag.Add(item);
        public List<string> OutBag() => new List<string>(Bag);
        public void FreeBag() => Bag.Clear();
        public void FreeChest() => Chest.Clear();
        public void InChest(string item) => Chest.Add(item);
        public void FreeBelt() => Belt.Clear();
        public void InBelt(string item) => Belt.Add(item);
        public List<string> OutChest() => new List<string>(Chest);
        public List<string> OutBelt() => new List<string>(Belt);
        public string FromBelt(int index) => Belt[index];
        public string FromChest(int index) => Chest[index];
        public void DeleteFromBelt(int index) => Belt.RemoveAt(index);
        public void DeleteFromChest(int index) => Chest.RemoveAt(index);
        public void AddMoney(Double New_Money) => Money += New_Money;
        public void PutMoney(Double Put_Money) => Money -= Put_Money;
        public Double GetMoney() => Money;
    }
}
