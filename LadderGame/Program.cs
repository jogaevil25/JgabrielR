using System;

namespace SnakeLadderGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tamaño del tablero?");
            int tamanotablero = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Numero de jugadores?");
            int jugadores = Convert.ToInt32(Console.ReadLine());
            Game g = new Game(tamanotablero, jugadores);
            g.Play();
        }
    }
}
