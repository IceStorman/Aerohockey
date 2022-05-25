using SFML.Graphics;

namespace Aerohockey
{
    public class Puck
    {
        public CircleShape puckSprite = new CircleShape(64);
        public Color puckColor = Color.Yellow;
        public float speed = 0.5f;
        public SFML.System.Vector2f direction = new SFML.System.Vector2f(1, 1);
    }
}
