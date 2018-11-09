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
        private Location currentLocation;
        

        public BusinessMan(Vector2 location, Location currentLocation)
        {
            this.location = location;
            this.currentLocation = currentLocation;
            newLocation = location;
            height = 100;
            width = 100;
            greeting = "Hello there, friend!";
            options = new string[][]{ new string[] { "Hey!", "positive" }, new string[] { "Please don't speak to me.", "negative" }, new string[] { "I've got to go.", "exit" } };
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            
            
        }
        public override void DrawDialog(SpriteBatch spriteBatch)
        {
            if (dialogBox != null)
            {
                dialogBox.Draw(spriteBatch);
            }
        }

        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("business-man");
        }
        public override void Update(KeyboardState state, Tile[][] tiles, MouseState mouseState)
        {
            if (isFalling)
            {
                newLocation.Y++;
                Collisions.CollideWithTiles(tiles, this);
                location = newLocation;
            }

            if (dialogBox != null)
            {
                dialogBox.Update(mouseState);
            }
        }
        

        public override void ChooseOption(string option)
        {
            if (option == "exit")
            {
                CloseDialog();
            }
        }
    }
}