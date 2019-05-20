using System;

namespace Fluid
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            player.Tick();
            Console.ReadKey();
        }
    }
}
