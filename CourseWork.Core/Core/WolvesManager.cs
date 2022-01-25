using System;
using System.Linq;
using CourseWork.Models.Abstractions;
using CourseWork.Models.Models;

namespace CourseWork.Core.Core
{
    public class WolvesManager : WolfManagerBase

    {
        public void Move(GameCell[,] _gameCellsOld, GameCell[,] _gameCellsNew)
        {
            for (int i = 0; i < _gameCellsOld.GetLength(0); i++)
            {
                for (int j = 0; j < _gameCellsOld.GetLength(1); j++)
                {
                    if (_gameCellsOld[i, j].Wolves.Any())
                    {
                        foreach (var wolves in _gameCellsOld[i, j].Wolves)
                        {
                            var newCoordinate = RandomWay(i, j);
                            // GameCellsNew[newCoordinate.Item1, newCoordinate.Item2].Wolves.Add(wolves);

                            wolves.SpentEnergy(SpentEnergyPerMove);
                            if (!_gameCellsNew.IsOutsideOfBounds(newCoordinate))
                            {
                                _gameCellsNew[newCoordinate.Item1, newCoordinate.Item2].Wolves.Add(wolves);
                                continue;
                            }
                            _gameCellsNew[i, j].Wolves.Add(wolves);
                        }
                    }
                }
            }
        }

     
        public override void Reproduce(GameCell[,] gameCells, GameCell[,] gameCellsNew)
        {
            for (int i = 0; i < gameCells.GetLength(0); i++)
            {
                for (int j = 0; j < gameCells.GetLength(1); j++)
                {
                    if (gameCells[i, j].Wolves.Any())
                    {
                        foreach (var wolves in gameCells[i, j].Wolves)
                        {
                            if (Reproduce(gameCells, gameCellsNew, new Coordinate(i, j)))
                            {
                                wolves.ReproduceEnergy(ReproduceEnergy);
                            }
                        }
                    }
                }
            }
        }

        public override void Hunt(GameCell[,] gameCells)
        {

            for (int i = 0; i < gameCells.GetLength(0); i++)
            {
                for (int j = 0; j < gameCells.GetLength(1); j++)
                {
                    if (gameCells[i, j].Wolves.Any())
                    {
                        foreach (var wolves in gameCells[i, j].Wolves)
                        {
                            if (Hunt(gameCells, new Coordinate(i, j)))
                            {
                                wolves.ReproduceEnergy(ReproduceEnergy);
                            }
                        }
                    }
                }
            }

        }
        //public override void RemoveIfNotAlive(GameCell[,] gameCells)
        //{
        //    for (int i = 0; i < gameCells.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < gameCells.GetLength(1); j++)
        //        {
        //            if (!gameCells[i, j].Wolves.Any()) continue;

        //            for (int k = 0; k < gameCells[i, j].Wolves.Count; k++)
        //            {
        //                if (!gameCells[i, j].Wolves[k].IsAlive)
        //                {
        //                    gameCells[i, j].Wolves.Remove(gameCells[i, j].Wolves[k]);
        //                }
        //            }
        //        }
        //    }
        //}

        public override void RemoveIfNotAlive(GameCell[,] gameCells)
        {
            for (int i = 0; i < gameCells.GetLength(0); i++)
            {
                for (int j = 0; j < gameCells.GetLength(1); j++)
                {
                    if (!gameCells[i, j].Wolves.Any()) continue;

                    for (int k = 0; k < gameCells[i, j].Wolves.Count; k++)
                    {
                        if (!gameCells[i, j].Wolves[k].IsAlive)
                        {
                            gameCells[i, j].Wolves.Remove(gameCells[i, j].Wolves[k]);
                        }
                    }
                }
            }
        }
    }
}

