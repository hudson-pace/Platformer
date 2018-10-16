using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class BusinessMan : Entity
    {
        private static Texture2D texture;

        public BusinessMan(Vector2 location)
        {
            this.location = location;
            newLocation = location;
            height = 100;
            width = 100;
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }

        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("business-man");
        }
        public override void Update(KeyboardState state, Tile[][] tiles)
        {
            if (isFalling)
            {
                newLocation.Y++;
                Collisions.CollideWithTiles(tiles, this);
                location = newLocation;
            }
        }
    }
}