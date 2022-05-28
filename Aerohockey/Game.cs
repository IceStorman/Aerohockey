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

            Puck puck = new Puck();

            SFML.System.Vector2f player1StartPos = new SFML.System.Vector2f(0, 0);
            SFML.System.Vector2f player2StartPos = new SFML.System.Vector2f(window.Size.X - player2.playerSprite.Radius * 2, window.Size.Y - player2.playerSprite.Radius * 2);

            player1.SetStandartParameters(Color.Blue, player1StartPos, Keyboard.Key.W, Keyboard.Key.S);
            player2.SetStandartParameters(Color.Red, player2StartPos, Keyboard.Key.Up, Keyboard.Key.Down);

            puck.SetPuckParameters(window);

            window.Closed += WindowClosed;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Cyan);

                SFML.System.Vector2f newPlayer1Pos = MoveLogic(player1);
                SFML.System.Vector2f newPlayer2Pos = MoveLogic(player2);

                CheckMove(window, player1, newPlayer1Pos);
                CheckMove(window, player2, newPlayer2Pos);

                puck.PuckMoving(window);

                TouchPlayer(puck, player1, player2);

                window.Draw(player1.playerSprite);
                window.Draw(player2.playerSprite);

                window.Draw(puck.puckSprite);

                SpawnCoin(window);

                window.Display();
            }
        }

        private void TouchPlayer(Puck puck, Player player1, Player player2)
        {
            if(puck.puckSprite.Position.X <= player1.playerSprite.Position.X + puck.puckSprite.Radius * 2
                || puck.puckSprite.Position.X >= player2.playerSprite.Position.X - puck.puckSprite.Radius * 2)
            {
                if((puck.puckSprite.Position.Y <= player1.playerSprite.Position.Y + puck.puckSprite.Radius * 2
                    && puck.puckSprite.Position.Y >= player1.playerSprite.Position.Y - puck.puckSprite.Radius * 2)
                    || (puck.puckSprite.Position.Y <= player2.playerSprite.Position.Y + puck.puckSprite.Radius * 2
                    && puck.puckSprite.Position.Y >= player2.playerSprite.Position.Y - puck.puckSprite.Radius * 2))
                {
                    puck.direction *= -1;
                }
            }
        }

        private void SpawnCoin(RenderWindow win)
        {
            Random rnd = new Random();
            Coin coin = new Coin();

            if (coin.isCoinTaken)
            {
                SFML.System.Vector2f newCoinPos = new SFML.System.Vector2f(rnd.Next(0, (int)win.Size.X), rnd.Next(0, (int)win.Size.Y));
                coin.coinSprite.Position = newCoinPos;
                win.Draw(coin.coinSprite);
                coin.isCoinTaken = false;
            }
        }

        private SFML.System.Vector2f GetInput(Player player)
        {
            SFML.System.Vector2f dir = new SFML.System.Vector2f();

            if (Keyboard.IsKeyPressed(player.upKey))
            {
                dir.Y = -1;
            }
            else if (Keyboard.IsKeyPressed(player.downKey))
            {
                dir.Y = 1;
            }

            return dir;
        }

        private SFML.System.Vector2f MoveLogic(Player player)
        {
            SFML.System.Vector2f newPos = player.playerSprite.Position + (GetInput(player) * player.speed);

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
            if (newPos.Y > win.Size.Y - player.playerSprite.Radius * 2 || newPos.Y < 0)
            {
                return false;
            }

            return true;
        }

        private void Move(Player player, SFML.System.Vector2f newPos)
        {
            player.playerSprite.Position = newPos;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
