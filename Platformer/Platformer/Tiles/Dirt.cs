using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Tiles
{
    class Dirt : Tile
    {
        private static Texture2D texture;
        public Dirt(int x, int y) : base(x, y, true, true, true)
        {

        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("dirt");
        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
