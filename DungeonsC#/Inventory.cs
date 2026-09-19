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

        public void InBag(string item) => Bag.Add(item);
        public List<string> OutBag() => new List<string>(Bag);
        public void FreeBag() => Bag.Clear();
        public void FreeChest() => Chest.Clear();
        public void InChest(string item) => Chest.Add(item);
        public List<string> OutChest() => new List<string>(Chest);
    }
}
