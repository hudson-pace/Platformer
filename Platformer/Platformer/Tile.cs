using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Tile
    {
        public bool isBarrier, isTextured;

        private Texture2D texture;
        private Vector2 location;

        public Tile(int x, int y, bool isBarrier, bool isTextured)
        {
            this.isBarrier = isBarrier;
            this.isTextured = isTextured;
            location = new Vector2(x * 50, y * 50);
        }
        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
