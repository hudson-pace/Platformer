using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Shop : Inventory
    {
        private MouseState previousState;
        private Inventory playerInventory;
        public Shop(int screenWidth, int screenHeight, Inventory playerInventory)
            :base(screenWidth, screenHeight)
        {
            this.playerInventory = playerInventory;
        }

        override public void Update(MouseState mouseState)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                item.SetHovering(false, mouseState);
                if (item.GetHitBox().Contains(mouseState.Position))
                {
                    item.SetHovering(true, mouseState);
                    if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
                    {
                        if (Game1.GetPlayer().Pay(item.GetPrice()))
                        {
                            Game1.GetPlayer().AddToInventory(item.GetItem());
                        }
                    }
                }
            }
            previousState = mouseState;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            playerInventory.Draw(spriteBatch);
        }
    }
}
