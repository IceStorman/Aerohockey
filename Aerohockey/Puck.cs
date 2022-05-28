using SFML.Graphics;

namespace Aerohockey
{
    public class Puck
    {
        public CircleShape puckSprite = new CircleShape(64);
        public Color puckColor = Color.Green;
        public float speed = 0.5f;
        public SFML.System.Vector2f direction = new SFML.System.Vector2f(1, 1);

        public void SetPuckParameters(RenderWindow win)
        {
            puckSprite.Position = new SFML.System.Vector2f(win.Size.X / 2, win.Size.Y / 2);
            puckSprite.FillColor = puckColor;
        }

        public void PuckMoving(RenderWindow win)
        {
            puckSprite.Position += direction * speed;
            ChangePuckDirection(win);
        }

        public void ChangePuckDirection(RenderWindow win)
        {
            if (puckSprite.Position.X >= win.Size.X - puckSprite.Radius * 2
                || puckSprite.Position.X <= 0)
            {
                direction.X *= -1;
                ChangePuckSpeed();
            }
            else if (puckSprite.Position.Y >= win.Size.Y - puckSprite.Radius * 2
                || puckSprite.Position.Y <= 0)
            {
                direction.Y *= -1;
                ChangePuckSpeed();
            }
        }

        public void ChangePuckSpeed()
        {
            if (speed <= 3f)
            {
                speed *= 1.125f;
            }
        }
    }
}
