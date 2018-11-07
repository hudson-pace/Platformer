using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Tiles
{
    class EnemyBarrier : Tile
    {
        public EnemyBarrier(int x, int y, Location currentLocation) : base(x, y, currentLocation, false, false, true)
        {
            name = "enemyBarrier";
            updatable = false;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
        }
    }
}
