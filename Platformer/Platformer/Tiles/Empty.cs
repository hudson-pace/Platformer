using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Tiles
{
    class Empty : Tile
    {
        public Empty(int x, int y, Location currentLocation) : base(x, y, currentLocation, false, false, false)
        {
            name = "empty";
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
