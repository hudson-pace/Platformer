﻿using System;
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
        private string shopDialog;
        private string[][] shopChoices;
        private Inventory inventory;


        public Wizard(Vector2 location, Location currentLocation, int screenWidth, int screenHeight)
        {
            this.location = location;
            this.currentLocation = currentLocation;
            newLocation = location;
            inventory = new Inventory(screenWidth, screenHeight);
            height = 100;
            width = 100;
            greetingDialog = "Greetings, mortal.";
            greetingChoices = new string[][] { new string[] { "Hello there!", "positive" }, new string[] { "Nice getup, you nerd.", "negative" }, new string[] { "See ya.", "exit" } };
            shopDialog = "Would you like to see my store?";
            shopChoices = new string[][] { new string[] { "Yes, please!", "positive" }, new string[] { "I think I'm ok.", "negative" }, new string[] { "Goodbye.", "exit" } };
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            if (inventory.GetIsActive())
            {
                inventory.Draw(spriteBatch);
            }

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
                return;
            }
            if (dialogBox.GetCurrentDialog() == greetingDialog)
            {
                dialogBox.SetText(shopDialog, shopChoices);
                return;
            }
            if (dialogBox.GetCurrentDialog() == shopDialog)
            {
                if (option == "positive")
                {
                    OpenShop();
                }
                return;
            }
        }
        public void OpenShop()
        {
            inventory.Toggle();
        }
    }
}