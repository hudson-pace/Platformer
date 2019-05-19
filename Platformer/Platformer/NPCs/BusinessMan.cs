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
        private string sadDialog, welcomeDialog;
        private string[][] sadChoices, welcomeChoices;
        
        

        public BusinessMan(Vector2 location, Location currentLocation)
            :base()
        {
            this.location = location;
            this.currentLocation = currentLocation;
            newLocation = location;
            height = 100;
            width = 100;
            greetingDialog = "Hello there, friend!";
            greetingChoices = new string[][]{ new string[] { "Hey!", "positive" }, new string[] { "Please don't speak to me.", "negative" }, new string[] { "I've got to go.", "exit" } };
            sadDialog = ":(";
            sadChoices = new string[][] { new string[] { "Goodbye.", "exit" } };
            welcomeDialog = "Welcome to the Test Area! Slime city is to the right.";
            welcomeChoices = new string[][] { new string[] { "Thanks for the tip!", "exit" }, new string[] { "Obviously. Thanks for nothing, dirtbag.", "negative" } };
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            
            
        }
        /*public override void DrawDialog(SpriteBatch spriteBatch)
        {
            if (dialogBox != null)
            {
                dialogBox.Draw(spriteBatch);
            }
        }*/

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

            /*if (dialogBox != null)
            {
                dialogBox.Update(mouseState);
            }*/
        }
        

        public override void ChooseOption(string option)
        {
            if (option == "exit")
            {
                CloseDialog();
                return;
            }
            if (option == "negative")
            {
                dialogBox.SetText(sadDialog, sadChoices);
            }
            if (dialogBox.GetCurrentDialog() == greetingDialog)
            {
                dialogBox.SetText(welcomeDialog, welcomeChoices);
            }
        }
    }
}