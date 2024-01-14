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
        public Rectangle HitBox { get; private set; }
        public Location Destination { get; private set; }
        public Vector2 PositionDestination { get; private set; }

        // x, y, width, and height are all measured via tile. ie tile location and number of tiles in width.
        public Portal(int x, int y, int width, int height, Location destination, int destX, int destY)
        {
            HitBox = new Rectangle(x * Globals.tileSize, y * Globals.tileSize, width * Globals.tileSize, height * Globals.tileSize);
            Destination = destination;
            PositionDestination = new Vector2(destX * Globals.tileSize, destY * Globals.tileSize);
        }
    }
}
