using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Tiles
{
    abstract class UpdatableTile : Tile
    {
        public UpdatableTile(int x, int y, Location currentLocation, bool isBarrier, bool isTextured, bool isEnemyBarrier) : base(x, y, currentLocation, isBarrier, isTextured, isEnemyBarrier)
        {

        }
        abstract public void Update();
    }
}
