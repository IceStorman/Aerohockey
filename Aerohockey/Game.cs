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

            SetStandartParameters(player1, Color.Blue, player1StartPos, Keyboard.Key.W, Keyboard.Key.S);
            SetStandartParameters(player2, Color.Red, player2StartPos, Keyboard.Key.Up, Keyboard.Key.Down);

            SetPuckParameters(window, puck);

            window.Closed += WindowClosed;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Cyan);

                window.Draw(player1.playerSprite);
                window.Draw(player2.playerSprite);

                window.Draw(puck.puckSprite);

                SFML.System.Vector2f newPlayer1Pos = MoveLogic(player1);
                SFML.System.Vector2f newPlayer2Pos = MoveLogic(player2);

                CheckMove(window, player1, newPlayer1Pos);
                CheckMove(window, player2, newPlayer2Pos);

                PuckMoving(window, puck);

                window.Display();
            }
        }

        private void SetStandartParameters(Player player, Color color, SFML.System.Vector2f startPos, Keyboard.Key upKey, Keyboard.Key downKey)
        {
            player.playerSprite.Position = startPos;
            player.playerSprite.FillColor = color;
            player.upKey = upKey;
            player.downKey = downKey;
        }

        private void SetPuckParameters(RenderWindow win, Puck puck)
        {
            puck.puckSprite.Position = new SFML.System.Vector2f(win.Size.X / 2, win.Size.Y / 2);
            puck.puckSprite.FillColor = puck.puckColor;
        }

        private void PuckMoving(RenderWindow win, Puck puck)
        {
            puck.puckSprite.Position += puck.direction * puck.speed;
            ChangePuckDirection(win, puck);
        }

        private void ChangePuckDirection(RenderWindow win, Puck puck)
        {
            if(puck.puckSprite.Position.X >= win.Size.X - puck.puckSprite.Radius * 2
                || puck.puckSprite.Position.X <= 0)
            {
                puck.direction.X *= -1;
                ChangePuckSpeed(puck);
            }
            else if(puck.puckSprite.Position.Y >= win.Size.Y - puck.puckSprite.Radius * 2
                || puck.puckSprite.Position.Y <= 0)
            {
                puck.direction.Y *= -1;
                ChangePuckSpeed(puck);
            }
        }

        private void ChangePuckSpeed(Puck puck)
        {
            if (puck.speed <= 3f)
            {
                puck.speed *= 1.125f;
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

        void WindowClosed(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }
    }
}
