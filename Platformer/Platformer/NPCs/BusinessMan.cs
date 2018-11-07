using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer.NPCs
{
    class BusinessMan : NPC
    {
        private static Texture2D texture;

        public BusinessMan(Vector2 location)
        {
            this.location = location;
            newLocation = location;
            height = 100;
            width = 100;
            greeting = "Hello there, friend!";
            options = new string[]{ "hey!", "hi", "hello" };
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
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