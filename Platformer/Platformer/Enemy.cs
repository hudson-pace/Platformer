using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    abstract class Enemy : Character
    {
        public Enemy()
        {
            isEnemy = true;
        }
        abstract public void Update(Tile[][] tiles);
        abstract public void Draw(SpriteBatch spritebatch, int offsetX, int offsetY);
        abstract public void LoadTextures(ContentManager content);
    }
}
