using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class PlayerInventory : Inventory
    {
        private Player player;

        public PlayerInventory(Player player, int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
        {
            this.player = player;
        }

        override public void Update(MouseState mouseState)
        {
            Rectangle topMenu = new Rectangle(container.X, container.Y, container.Width, 40);
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed && topMenu.Contains(mouseState.Position))
            {
                dragging = true;
            }

            if (dragging)
            {
                if (mouseState.LeftButton != ButtonState.Pressed)
                {
                    dragging = false;
                }
                else
                {
                    container.X += mouseState.X - previousMouseState.X;
                    container.Y += mouseState.Y - previousMouseState.Y;
                }
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    inventoryItems[i].SetLocation(new Vector2(container.X + 20 + ((i % 5) * 50), container.Y + 50 + ((i / 5) * 50)));
                }
            }

            if (!draggingItem)
            {
                selectedItem = null;
            }

            bool select = false;
            foreach (InventoryItem item in inventoryItems)
            {
                item.SetHovering(false, mouseState);
                if (item.GetHitBox().Contains(mouseState.Position))
                {
                    item.SetHovering(true, mouseState);
                    if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
                    {
                        selectedItem = item;
                        draggingItem = true;
                        select = true;
                        break;
                    }
                    if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed)
                    {
                        selectedItem = item;
                        select = true;
                        player.GetEquipmentMenu().Equip(item);
                        break;
                    }
                }
            }
            if (select)
            {
                RemoveFromInventory(selectedItem);
                if (!draggingItem)
                {
                    selectedItem = null;
                }
            }

            if (draggingItem)
            {
                selectedItem.SetLocation(new Vector2(mouseState.Position.X - 15, mouseState.Position.Y - 15));
                if (mouseState.LeftButton != ButtonState.Pressed)
                {
                    if (container.Contains(mouseState.Position))
                    {
                        AddToInventory(selectedItem.GetItem(), false, 0);
                    }
                    else
                    {
                        Item drop = selectedItem.GetItem();
                        drop.ResetPickUpCounter();
                        drop.SetLocation(player.location);
                        player.GetCurrentLocation().AddItem(drop);
                    }


                    selectedItem = null;
                    draggingItem = false;
                }
            }
            previousMouseState = mouseState;
        }
    }
}
