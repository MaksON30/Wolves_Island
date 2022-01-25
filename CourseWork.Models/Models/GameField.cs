using System.Text;

namespace CourseWork.Models.Models
{
    public class GameField
    {
        private GameCell[,] _gameCells;

        public int Width => _gameCells.GetLength(1);
        public int Height => _gameCells.GetLength(0);
        public GameCell[,] GameCells => _gameCells;

        public GameField(GameCell[,] gameCells)
        {
            // _gameCells = new GameCell[width, height];
            _gameCells = gameCells;
        }

        public void ReplaceGameCells(GameCell[,] newGameCells)
        {
            _gameCells = newGameCells;
        }
        //public GameField(GameCell[,] gameCells)
        //{
        //    _gameCells = gameCells;
        //}
       

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            for (int i = 0; i < _gameCells.GetLength(0); i++)
            {
                strBuilder.Append("\n");
                for (int j = 0; j < _gameCells.GetLength(1); j++)
                {
                    strBuilder.Append(_gameCells[i, j] + "\t");
                }
            }
            return strBuilder.ToString();
        }
    }
}
