using System;
using System.Linq;
using CourseWork.Models.Abstractions;
using CourseWork.Models.Models;

namespace CourseWork.Core.Core
{
    public class RabbitManager : PlayerManagerBase
    {

        private const int LowerNumberToReproduce = 1;
        private const int UpperNumberToReproduce = 5;
        private const int LuckyNumberToReproduce = 3;

        public override void Reproduce(GameCell[,] gameCellsOld, GameCell[,] gameCellsNew)
        {
            var random = new Random();
            for (int i = 0; i < gameCellsOld.GetLength(0); i++)
            {
                for (int j = 0; j < gameCellsOld.GetLength(1); j++)
                {
                    if (gameCellsOld[i, j].Rabbits.Any())
                    {
                        foreach (var rabbit in gameCellsOld[i, j].Rabbits)
                        {
                            var numberToReproduce = random.Next(LowerNumberToReproduce, UpperNumberToReproduce);

                            if (numberToReproduce == LuckyNumberToReproduce)
                            {
                                gameCellsNew[i, j].Rabbits.Add(rabbit);
                            }
                        }
                    }
                }
            }
        }

        public void Move(GameCell[,] gameCellsOld, GameCell[,] gameCellsNew)
        {

            for (int i = 0; i < gameCellsOld.GetLength(0); i++)
            {
                for (int j = 0; j < gameCellsOld.GetLength(1); j++)
                {
                    if (gameCellsOld[i, j].Rabbits.Any())
                    {
                        foreach (var rabbit in gameCellsOld[i, j].Rabbits)
                        {
                            var newCoordinate = RandomWay(i, j);

                            if (!gameCellsNew.IsOutsideOfBounds(newCoordinate))
                            {
                                gameCellsNew[newCoordinate.Item1, newCoordinate.Item2].Rabbits.Add(rabbit);
                                continue;
                            }

                            gameCellsNew[i, j].Rabbits.Add(rabbit);
                        }
                    }
                }
            }
        }
    }
}
