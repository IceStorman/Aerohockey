using SFML.Window;
using SFML.Graphics;
using System;

namespace Aerohockey
{
    public class Game
    {
        public void Play()
        {
            RenderWindow window = new RenderWindow(new VideoMode(1600, 900), "Game window");

            Player player1 = new Player();
            Player player2 = new Player();

            player1.playerModel.FillColor = Color.Blue;
            player2.playerModel.FillColor = Color.Red;

            window.Closed += WindowClosed;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Cyan);
                window.Draw(player1.playerModel);

                SFML.System.Vector2f newPlayer1Pos = MoveLogic(player1);

                CheckMove(window, player1, newPlayer1Pos);

                window.Display();
            }
        }

        private SFML.System.Vector2f GetInput()
        {
            SFML.System.Vector2f dir = new SFML.System.Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                dir.Y = -1;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                dir.Y = 1;
            }

            return dir;
        }

        private SFML.System.Vector2f MoveLogic(Player player)
        {
            SFML.System.Vector2f newPos = player.playerModel.Position + GetInput();

            return newPos;
        }

        private void CheckMove(RenderWindow win, Player player, SFML.System.Vector2f newPos)
        {
            if (CanMove(win, player, newPos))
            {
                Move(player, newPos);
            }
        }

        private bool CanMove(RenderWindow win, Player player, SFML.System.Vector2f newPos)
        {

            if (newPos.Y > win.Size.Y - player.playerModel.Radius * 2 || newPos.Y < 0)
            {
                return false;
            }

            return true;
        }

        private void Move(Player player, SFML.System.Vector2f newPos)
        {
            player.playerModel.Position = newPos;
        }

        void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
