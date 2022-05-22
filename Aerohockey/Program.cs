using SFML.Window;
using SFML.Graphics;
using System;

namespace Aerohockey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Play();
        }
    }
}