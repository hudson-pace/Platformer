using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Platformer
{
    class EquipmentMenu : Menu
    {
        private static Texture2D containerTexture, itemSlotTexture;
        private Rectangle container, itemSlot;
        private InventoryItem equippedItem;
        private Player player;
        private bool isActive, dragging;
        private MouseState previousMouseState;

        public EquipmentMenu(Player player)
        {
            this.player = player;
            container = new Rectangle(100, 100, 100, 100);
            itemSlot = new Rectangle(110, 130, 50, 50);
        }
        public static void CreateTextures(GraphicsDevice graphicsDevice)
        {
            containerTexture = new Texture2D(graphicsDevice, 1, 1);
            containerTexture.SetData(new Color[] { Color.Turquoise });
            itemSlotTexture = new Texture2D(graphicsDevice, 1, 1);
            itemSlotTexture.SetData(new Color[] { Color.LightGray });
        }

        public bool GetIsActive()
        {
            return isActive;
        }
        public InventoryItem GetEquippedItem()
        {
            return equippedItem;
        }
        public void Toggle()
        {
            if (isActive)
            {
                Game1.RemoveFromMenuList(this);
            }
            else
            {
                Game1.AddToMenuList(this);
            }
            isActive = !isActive;
        }
        override public void Update(MouseState mouseState)
        {
            if (container.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed
                && mouseState.Position.Y < container.Y + 30)
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
                    container.X += mouseState.Position.X - previousMouseState.Position.X;
                    container.Y += mouseState.Position.Y - previousMouseState.Position.Y;
                }
            }

            if (itemSlot.Contains(mouseState.Position) && mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed)
            {
                UnEquip();
            }


            itemSlot = new Rectangle(container.X + 10, container.Y + 30, 50, 50);
            if (equippedItem != null)
            {
                equippedItem.SetLocation(new Vector2(itemSlot.X, itemSlot.Y));
            }

            previousMouseState = mouseState;
        }

        public void Equip(InventoryItem item)
        {
            UnEquip();
            equippedItem = item;
            equippedItem.SetLocation(new Vector2(itemSlot.X, itemSlot.Y));
        }
        public void UnEquip()
        {
            if (equippedItem != null)
            {
                player.AddToInventory(equippedItem.GetItem());
                equippedItem = null;
            }
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(containerTexture, container, Color.White);
            spriteBatch.Draw(itemSlotTexture, itemSlot, Color.White);
            if (equippedItem != null)
            {
                equippedItem.Draw(spriteBatch);
            }
        }
    }
}
