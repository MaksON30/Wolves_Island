using System;
using CourseWork.Models.Models;

namespace CourseWork.Models.Abstractions
{
    public abstract class PlayerManagerBase
    {
        public abstract void Reproduce(GameCell[,] gameCellsOld, GameCell[,] gameCellsNew);
        protected virtual (int, int) RandomWay(int i, int j)
        {
            Random random = new Random();
            var moveWay = random.Next(1, 9);
            switch (moveWay)
            {
                case 1:
                    i--;
                    j--;

                    break;

                case 2:
                    i--;
                    break;

                case 3:
                    i--;
                    j++;
                    break;

                case 4:
                    j--;
                    break;

                case 5:
                    //залишаємось на місці
                    break;

                case 6:
                    j++;
                    break;

                case 7:
                    i++;
                    j--;
                    break;

                case 8:
                    i++;
                    break;

                case 9:
                    i++;
                    j++;
                    break;
            }
            return (i, j);
        }
    }
}
