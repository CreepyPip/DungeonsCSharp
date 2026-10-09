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
                switch (Items[i])
                {
                    case "Ржавый меч":
                        ItemsScore += 1.0;
                        break;

                    case "Ржавая броня":
                        ItemsScore += 2.0;
                        break;

                    case "Маленький мешок с монетами":
                        ItemsScore += 3.0;
                        break;

                    case "Средний мешок с монетами":
                        ItemsScore += 5.0;
                        break;

                    case "Большой мешок с монетами":
                        ItemsScore += 8.0;
                        break;

                    case "Почти новый меч":
                        ItemsScore += 10.0;
                        break;

                    case "Почти новая броня":
                        ItemsScore += 13.0;
                        break;

                    case "Золото":
                        ItemsScore += 20.0;
                        break;

                    case "Чьи-то кости":
                        ItemsScore += 0.1;
                        break;

                    default:
                        ItemsScore += 0.2;
                        break;
                }
            }

            return ItemsScore;
        }
    }
}