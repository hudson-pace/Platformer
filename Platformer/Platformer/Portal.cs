using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Portal
    {
        public Rectangle hitBox;
        private static Texture2D texture;
        private Location destination;
        private Vector2 positionDestination;

        public Portal(int x, int y, Location destination, int destX, int destY)
        {
            this.hitBox = new Rectangle(x * 50, y * 50, 100, 150);
            this.destination = destination;
            this.positionDestination = new Vector2(destX * 50, destY * 50);
        }
        public Location GetDestination()
        {
            return destination;
        }
        public Vector2 GetPositionDestination()
        {
            return positionDestination;
        }
    }
}
