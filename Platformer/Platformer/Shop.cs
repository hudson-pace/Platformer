using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Shop : Inventory
    {
        private MouseState previousState;
        public Shop(int screenWidth, int screenHeight)
            :base(screenWidth, screenHeight)
        {
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

    }
}
