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
    class Wizard : NPC
    {
        private static Texture2D texture;
        private Location currentLocation;
        private string greetingDialog, shopDialog;
        private string[][] greetingChoices, shopChoices;


        public Wizard(Vector2 location, Location currentLocation)
        {
            this.location = location;
            this.currentLocation = currentLocation;
            newLocation = location;
            height = 100;
            width = 100;
            greetingDialog = "Greetings, mortal.";
            greetingChoices = new string[][] { new string[] { "Hello there!", "positive" }, new string[] { "Nice getup, you nerd.", "negative" }, new string[] { "See ya.", "exit" } };
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
            texture = content.Load<Texture2D>("wizard");
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
            else
            {
                dialogBox.SetText("Would you like to see my store?", new string[][]{ new string[]{ "Yes, please!", "positive" }, new string[]{ "I think I'm ok.", "negative" },
                    new string[]{ "Goodbye.", "exit" } });
            }
        }
    }
}
