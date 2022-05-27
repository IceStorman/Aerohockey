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

        public void SetStandartParameters(Color color, SFML.System.Vector2f startPos, Keyboard.Key upKey, Keyboard.Key downKey)
        {
            playerSprite.Position = startPos;
            playerSprite.FillColor = color;
            this.upKey = upKey;
            this.downKey = downKey;
        }
    }
}
