using CourseWork.Models.Models;

namespace CourseWork.Core.Core
{
    public static class Extensions
    {
        public static bool IsOutsideOfBounds(this GameCell[,] array, (int i, int j) index)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            return !(index.i >= 0
                       && index.i < width
                       && index.j >= 0
                       && index.j < height);
        }
    }
}