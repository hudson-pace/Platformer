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
        public InvisibleBarrier(int x, int y) : base(x, y, true, false, true)
        {

        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
        }
    }
}
