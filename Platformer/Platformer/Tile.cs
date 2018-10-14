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
    class Tile
    {
        public bool isBarrier, isTextured, isEnemyBarrier;

        private Texture2D texture;
        private Vector2 location;

        public Tile(int x, int y, bool isBarrier, bool isTextured, bool isEnemyBarrier)
        {
            this.isBarrier = isBarrier;
            this.isTextured = isTextured;
            this.isEnemyBarrier = isEnemyBarrier;
            location = new Vector2(x * 50, y * 50);
        }
        public void LoadTextures(ContentManager content)
        {
            if (isTextured)
            {
                texture = content.Load<Texture2D>("brick-wall");
            }
        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
