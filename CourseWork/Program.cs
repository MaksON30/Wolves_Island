using System;
using System.Threading.Tasks;
using CourseWork.Core;
using CourseWork.Core.Core;
using CourseWork.Models;

namespace CourseWork
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GameFieldManager gameFieldManager = new GameFieldManager(20, 20, new RabbitManager(),new WolvesManager(), new SheWolvesManager());
            await gameFieldManager.StartSimulation();
        }
    }
}
