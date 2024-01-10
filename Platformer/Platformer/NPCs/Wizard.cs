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
        private string menuDialog;
        private string[][] menuChoices;
        private Shop shop;


        public Wizard(Vector2 location, Location currentLocation, int screenWidth, int screenHeight, Player player)
            :base()
        {
            this.location = location;
            this.currentLocation = currentLocation;
            newLocation = location;
            shop = new Shop(screenWidth, screenHeight, player.GetInventory());
            shop.AddToInventory(new Items.HealthPotion(1), true, 5);
            height = 100;
            width = 100;
            greetingDialog = "Greetings, mortal.";
            greetingChoices = new string[][] { new string[] { "Hello there!", "positive" }, new string[] { "Nice getup, you nerd.", "negative" }, new string[] { "See ya.", "exit" } };
            menuDialog = "How can I help you?";
            menuChoices = new string[][] { new string[] { "Can I see what you have for sale?", "shop" }, new string[] { "I'm looking for work.", "job" }, new string[] { "Goodbye.", "exit" } };
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), Color.White);
        }

        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("wizard");
        }
        public override void Update(KeyboardState state, Location l, MouseState mouseState)
        {
            if (isFalling)
            {
                newLocation.Y++;
                Collisions.CollideWithTiles(l, this);
                location = newLocation;
            }
        }


        public override void ChooseOption(string option)
        {
            switch(option)
            {
                case "exit":
                    CloseDialog();
                    return;
                case "shop":
                    ToggleShop();
                    break;
                default:
                    dialogBox.SetText(menuDialog, menuChoices);
                    break;
            }
        }
        public void ToggleShop()
        {
            shop.Toggle();
        }
    }
}
