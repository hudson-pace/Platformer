using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Tiles
{
    class Wall : Tile
    {
        public enum WallType
        {
            SlimeTopLeftBottomRight,
            SlimeTopLeftBottom,
            SlimeTopBottom,
            SlimeTopRightBottom,
            Brick,
            SlimeTopLeft,
            SlimeTop,
            SlimeTopRight,
            SlimeTopLeftRight,
            Dirt,
            SlimeLeft,
            Slime,
            SlimeRight,
            SlimeLeftRight,
            Grass,
            SlimeLeftBottom,
            SlimeBottom,
            SlimeBottomRight,
            SlimeLeftBottomRight,
            Portal
            
        }
        private static Texture2D texture;
        private WallType type;
        public Wall(int x, int y, Location currentLocation, WallType type) : base(x, y, currentLocation, true, true, true)
        {
            this.type = type;
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("Tiles/basic_tiles");
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle textureSource = new Rectangle((((int)type % 5) * 50), (((int)type / 5) * 50), 50, 50);
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), textureSource, Color.White);
        }
    }
}
