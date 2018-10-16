using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Items
{
    class SlimeItem : Item
    {
        private static Texture2D texture;
        private int currentYOffset = -3;
        public SlimeItem()
        {
            height = 30;
            width = 30;
        }
        override public void SetLocation(Vector2 location)
        {
            this.location = location;
            newLocation = location;
        }
        override public void Update(KeyboardState state, Tile[][] tiles)
        {
            if (isFalling)
            {
                newLocation.Y += horizontalVelocity;
                horizontalVelocity++;
                Collisions.CollideWithTiles(tiles, this);
                location = newLocation;
            }
            else
            {
                currentYOffset++;
                if (currentYOffset > 2)
                {
                    currentYOffset = -3;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime-ball");
        }
    }
}
