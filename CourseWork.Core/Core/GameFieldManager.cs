using System;
using System.Threading.Tasks;
using CourseWork.Models.Models;

namespace CourseWork.Core.Core
{
    public class GameFieldManager
    {
        private const int MinAnimalCount = 1;
        private readonly WolvesManager _wolvesManager;
        private readonly SheWolvesManager _sheWolvesManager;
        public readonly GameField GameField;
        public int Height => GameField.Height;
        public int Width => GameField.Width;
        private readonly RabbitManager _rabbitManager;


        public GameFieldManager(int width, int height, RabbitManager rabbitManager, WolvesManager wolvesManager, SheWolvesManager sheWolvesManager)
        {
            _rabbitManager = rabbitManager;
            _wolvesManager = wolvesManager;
            _sheWolvesManager = sheWolvesManager;
            GameField = new GameField(GenerateRandomField(width, height, 20, 10, 15));
        }
        public GameFieldManager(int width, int height, int rabbitsCount, int wolvesCount, int shewolvesCount, RabbitManager rabbitManager, WolvesManager wolvesManager, SheWolvesManager sheWolvesManager) 
        {
            _rabbitManager = rabbitManager;
            _wolvesManager = wolvesManager;
            _sheWolvesManager = sheWolvesManager;
            GameField = new GameField(GenerateRandomField(width, height, wolvesCount, shewolvesCount, rabbitsCount));
        }

        public async Task StartSimulation() 
        {
            while (true)
            {
              
                // creating new game field
                var newGameCells = GenerateEmptyField(GameField.Width, GameField.Height);

                // printing new game field
                PrintGameField();


                _wolvesManager.Hunt(GameField.GameCells);

                // reproducing game units
                _rabbitManager.Reproduce(GameField.GameCells, newGameCells);

                // moving game units
                _rabbitManager.Move(GameField.GameCells, newGameCells);
                _wolvesManager.Move(GameField.GameCells, newGameCells);

                GameField.ReplaceGameCells(newGameCells);
                await Task.Delay(5000);
            }
        }
        public async Task StartSimulationWF() //ми беремо один і той же поток для двох зчитувань
        {
            while (true)
            {
                // creating new game field
                var newGameCells = GenerateEmptyField(GameField.Width, GameField.Height);

                // printing new game field
                PrintGameField();


                _wolvesManager.Hunt(GameField.GameCells);

                // reproducing game units
                _rabbitManager.Reproduce(GameField.GameCells, newGameCells);

                // moving game units
                _rabbitManager.Move(GameField.GameCells, newGameCells); 
                _wolvesManager.Move(GameField.GameCells, newGameCells);

                GameField.ReplaceGameCells(newGameCells);
                await Task.Delay(3000);
            }
        }
        private GameCell[,] GenerateRandomField(int width, int height, int maxWolvesCount, int maxSheWolvesCount, int maxRabbitsCount)
        {
            var gameCells = GenerateEmptyField(width, height);

            var random = new Random();

            //  gameCells[5, 5].Rabbits.Add(new Rabbit());

            // todo check for out bounds of array (game field)
            var wolvesCount = random.Next(MinAnimalCount, maxWolvesCount);
            var sheWolvesCount = random.Next(MinAnimalCount, maxSheWolvesCount);
            var rabbitCount = random.Next(MinAnimalCount, maxRabbitsCount);

            for (int i = 0; i < wolvesCount; i++)
            {
                var cellIndex = GetRandomCellIndex(width, height, random);
                gameCells[cellIndex.Item1, cellIndex.Item2].Wolves.Add(new Wolf());
            }

            for (int i = 0; i < sheWolvesCount; i++)
            {
                var cellIndex = GetRandomCellIndex(width, height, random);
                gameCells[cellIndex.Item1, cellIndex.Item2].SheWolves.Add(new SheWolf());
            }

            for (int i = 0; i < rabbitCount; i++)
            {
                var cellIndex = GetRandomCellIndex(width, height, random);
                gameCells[cellIndex.Item1, cellIndex.Item2].Rabbits.Add(new Rabbit());
            }

            return gameCells;
        }
        public static GameCell[,] GenerateEmptyField(int width, int height)
        {
            var gameCells = new GameCell[width, height];

            for (int i = 0; i < gameCells.GetLength(0); i++)
            {
                for (int j = 0; j < gameCells.GetLength(1); j++)
                {
                    gameCells[i, j] = new GameCell();
                }
            }

            return gameCells;
        }

        private (int, int) GetRandomCellIndex(int width, int height, Random random) => (random.Next(0, height), random.Next(0, width));

        private void PrintGameField()
        {
            Console.WriteLine(GameField.ToString());
        }
        private void PrintGameFieldWF()
        {
            Console.WriteLine(GameField.ToString());
        }
      //  private bool IsOutOfFieldRange()

    }
}