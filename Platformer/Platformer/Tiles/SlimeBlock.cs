using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Tiles
{
    class SlimeBlock : Tile
    {
        private static Texture2D texture;
        private Rectangle textureSource;
        public SlimeBlock(int x, int y, int textureNumber, Location currentLocation) : base(x, y, currentLocation, true, true, true)
        {
            textureSource = new Rectangle(((textureNumber % 4) * 50), ((textureNumber / 4) * 50), 50, 50);
            name = "slimeBlock";
            updatable = false;
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("Tiles/slime-block");
        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), textureSource, Color.White);
        }
    }
}
