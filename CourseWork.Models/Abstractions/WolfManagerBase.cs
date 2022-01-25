using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Models.Models;

namespace CourseWork.Models.Abstractions
{
    public abstract class WolfManagerBase : PlayerManagerBase
    {
        protected const double SpentEnergyPerMove = 0.1;
        protected const double ReproduceEnergy = 1;
        public abstract void Hunt(GameCell[,] gameCells);

        public abstract void RemoveIfNotAlive(GameCell[,] gameCells);

        protected bool Reproduce(GameCell[,] gameCells, GameCell[,] gameCellsNew, Coordinate coordinate)
        {
            var coordinates = GetDataForScan(gameCells, coordinate);
            var rand = new Random();

            for (int k = coordinates.First.I; k <= coordinates.Second.I; k++)
            {
                for (int l = coordinates.First.J; l <= coordinates.Second.J; l++)
                {
                    if (gameCells[k, l].Wolves.Any() && gameCells[k, l].SheWolves.Any())
                    {
                        foreach (var wolf in gameCells[k, l].Wolves)
                        {
                            var sex = rand.Next(1, 2) % 2;
                            if (sex % 2 == 0)
                            {
                                gameCellsNew[k, l].Wolves.Add(new Wolf());
                            }
                            else
                            {
                                gameCellsNew[k, l].SheWolves.Add(new SheWolf());
                            }
                        }
                        return true;
                    }
                }
            }

            return false;
        }
        protected bool Hunt(GameCell[,] gameCells, Coordinate coordinate)
        {
            var coordinates = GetDataForScan(gameCells, coordinate);

            for (int k = coordinates.First.I; k <= coordinates.Second.I; k++)
            {
                for (int l = coordinates.First.J; l <= coordinates.Second.J; l++)
                {
                    if (gameCells[k, l].Rabbits.Any())
                    {
                        gameCells[k, l].Rabbits.RemoveAt(0);
                        return true;
                    }
                }
            }

            return false;
        }
        protected static Coordinat2D GetDataForScan(GameCell[,] gameCells, Coordinate currentCoordinates)
        {
            var firstCoordinate = new Coordinate();
            var lastCoordinate = new Coordinate();
            if (currentCoordinates.I == 0 && currentCoordinates.J == 0)
            {
                firstCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J + 1);
            }

            else if (currentCoordinates.I != 0 && currentCoordinates.I != gameCells.GetLength(0) - 1 && currentCoordinates.J == 0)
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J + 1);
            }

            else if (currentCoordinates.I == gameCells.GetLength(0) - 1 && currentCoordinates.J == 0)
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J);
                lastCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J + 1);
            }

            //4
            else if (currentCoordinates.I == gameCells.GetLength(0) - 1 && currentCoordinates.J != gameCells.GetLength(1) - 1)
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J + 1);
            }
            // 5 
            // TODO review if we need to add '(' ')' in below operation, add it in all if's
            else if ((currentCoordinates.I == gameCells.GetLength(0) - 1) && (currentCoordinates.J == gameCells.GetLength(1) - 1))
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J);
            }

            // 6

            else if (currentCoordinates.I != 0 && (currentCoordinates.J == gameCells.GetLength(1) - 1))
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J);
            }
            // 7

            else if (currentCoordinates.I == 0 && (currentCoordinates.J == gameCells.GetLength(1) - 1))
            {
                firstCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J);
            }

            // 8

            else if (currentCoordinates.I == 0 && (currentCoordinates.J != gameCells.GetLength(1) - 1))
            {
                firstCoordinate.SetValue(currentCoordinates.I, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J + 1);
            }
            // 9
            else
            {
                firstCoordinate.SetValue(currentCoordinates.I - 1, currentCoordinates.J - 1);
                lastCoordinate.SetValue(currentCoordinates.I + 1, currentCoordinates.J + 1);
            }

            var result = new Coordinat2D()
            {
                First = firstCoordinate,
                Second = lastCoordinate
            };

            return result;
        }

    }
}
