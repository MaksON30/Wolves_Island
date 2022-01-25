using System.Linq;
using CourseWork.Models.Abstractions;
using CourseWork.Models.Models;

namespace CourseWork.Core.Core
{
    public class SheWolvesManager : WolfManagerBase
    {
        public void Move(GameCell[,] _gameCellsOld, GameCell[,] _gameCellsNew)
        {
            for (int i = 0; i < _gameCellsOld.GetLength(0); i++)
            {
                for (int j = 0; j < _gameCellsOld.GetLength(1); j++)
                {
                    if (_gameCellsOld[i, j].SheWolves.Any())
                    {
                        foreach (var sheWolves in _gameCellsOld[i, j].SheWolves)
                        {
                            var newCoordinate = RandomWay(i, j);
                            sheWolves.SpentEnergy(SpentEnergyPerMove);

                            if (!_gameCellsNew.IsOutsideOfBounds(newCoordinate))
                            {
                                _gameCellsNew[newCoordinate.Item1, newCoordinate.Item2].SheWolves.Add(sheWolves);
                                continue;
                            }

                            _gameCellsNew[i, j].SheWolves.Add(sheWolves);
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
                    if (gameCells[i, j].SheWolves.Any())
                    {
                        foreach (var wolves in gameCells[i, j].SheWolves)
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
        public override void RemoveIfNotAlive(GameCell[,] gameCells)
        {
            for (int i = 0; i < gameCells.GetLength(0); i++)
            {
                for (int j = 0; j < gameCells.GetLength(1); j++)
                {
                    if (!gameCells[i, j].SheWolves.Any()) continue;

                    for (int k = 0; k < gameCells[i, j].SheWolves.Count; k++)
                    {
                        if (!gameCells[i, j].SheWolves[k].IsAlive)
                        {
                            gameCells[i, j].SheWolves.Remove(gameCells[i, j].SheWolves[k]);
                        }
                    }
                }
            }
        }

        public override void Reproduce(GameCell[,] gameCellsOld, GameCell[,] gameCellsNew)
        {
            throw new System.NotImplementedException();
        }
    }
}
