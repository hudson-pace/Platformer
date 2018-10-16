﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Tiles
{
    class EnemyBarrier : Tile
    {
        public EnemyBarrier(int x, int y) : base(x, y, false, false, true)
        {

        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
        }
    }
}
