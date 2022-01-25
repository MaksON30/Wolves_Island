using CourseWork.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Models.Abstractions
{
   public interface IReprodusable
    {
       abstract public void Reproduce(GameCell[,] gameCellsOld, GameCell[,] gameCellsNew);
    }
}
