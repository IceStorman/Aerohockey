using SFML.Graphics;
using SFML.Window;

namespace Aerohockey
{
    public class Player
    {
        public CircleShape playerSprite = new CircleShape(64);
        public float speed = 1.5f;
        public Keyboard.Key upKey;
        public Keyboard.Key downKey;
    }
}
