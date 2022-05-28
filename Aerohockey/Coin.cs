using SFML.Graphics;
using SFML.Window;

namespace Aerohockey
{
    public class Coin
    {
        public CircleShape coinSprite = new CircleShape(32);
        public Color coinColor = Color.Yellow;
        public bool isCoinTaken = false;

        public void SetCoinParameters()
        {
            coinSprite.FillColor = coinColor;
        }
    }
}
