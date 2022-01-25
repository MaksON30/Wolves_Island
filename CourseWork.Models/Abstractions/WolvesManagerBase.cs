using CourseWork.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.Abstractions
{
  public abstract class WolvesManagerBase : PlayerManagerBase
    {
        public static Coordinat2D GetDataForScan(GameCell[,] gameCells, Coordinate currentCoordinates) 
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
