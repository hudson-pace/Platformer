using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Tiles
{
    class InvisibleBarrier : Tile
    {
        public InvisibleBarrier(int x, int y, Location currentLocation) : base(x, y, currentLocation, true, false, true)
        {
            name = "invisibleBarrier";
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
