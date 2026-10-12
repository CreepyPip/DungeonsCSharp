using System;
using System.Collections.Generic;

namespace Dungeons
{
    public class InfinityMode
    {
        private int Level = -1;

        public void LevelPlus()
        {
            Level += 1;
        } 

        public int GetLevel()
        {
            return Level;
        }

        public double Score(List<string> Items)
        {
            double ItemsScore = 0.0;

            for (int i = 0; i < Items.Count; i++)
            {
                ItemsScore += GameValues.ItemsScore[Items[i]];
            }

            return Math.Round(ItemsScore, 4);
        }
    }
}