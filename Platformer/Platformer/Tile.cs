using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    abstract class Tile
    {
        public bool isBarrier, isTextured, isEnemyBarrier;

        protected Vector2 location;

        public Tile(int x, int y, bool isBarrier, bool isTextured, bool isEnemyBarrier)
        {
            this.isBarrier = isBarrier;
            this.isTextured = isTextured;
            this.isEnemyBarrier = isEnemyBarrier;
            location = new Vector2(x * 50, y * 50);
        }
        abstract public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY);
    }
}
