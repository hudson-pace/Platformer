using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Tiles
{
    class EnemyBarrier : Tile
    {
        public EnemyBarrier(int x, int y) 
            : base(x, y, false, false, true)
        {

        }
    }
}
