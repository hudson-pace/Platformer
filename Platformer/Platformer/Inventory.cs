﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Inventory
    {
        private List<InventoryItem> inventoryItems = new List<InventoryItem>();
        private static Texture2D containerTexture, itemSlotTexture, selectedItemSlotTexture;
        private static SpriteFont font;
        private Rectangle container;
        private Color color = new Color(55, 220, 225, 240);
        private bool isActive = false, dragging = false;
        private MouseState previousMouseState;

        public Inventory()
        {
            container = new Rectangle(0, 0, 270, 400);
        }

        public void Toggle()
        {
            isActive = !isActive;
            container.X = 0;
            container.Y = 0;
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryItems[i].setLocation(new Vector2(container.X + 10 + 10 + ((i % 5) * 50), container.Y + 40 + 10 + ((i / 5) * 50)));
            }
        }

        public bool GetIsActive()
        {
            return isActive;
        }
        public void AddToInventory(Item item, int count)
        {
            foreach (InventoryItem i in inventoryItems)
            {
                if (i.getItem().itemName == item.itemName) {
                    i.setCount(i.getCount() + count);
                    return;
                }
            }
            inventoryItems.Add(new InventoryItem(item, count, new Vector2((inventoryItems.Count * 60) + 10, 10)));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            


            spriteBatch.Draw(containerTexture, container, color);

            int itemCount = inventoryItems.Count;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    spriteBatch.Draw(itemSlotTexture, new Vector2(10 + (container.X + (i * 50)), 40 + (container.Y + (j * 50))), color);
                }
            }

            foreach (InventoryItem item in inventoryItems)
            {
                item.Draw(spriteBatch);
                spriteBatch.DrawString(font, item.getCount() + "", new Vector2(item.GetHitBox().X + item.getItem().width, item.GetHitBox().Y + item.getItem().height), Color.Black);
            }
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("ItemText");
        }
        public void CreateTextures(GraphicsDevice graphicsDevice)
        {
            containerTexture = new Texture2D(graphicsDevice, 1, 1);
            
            containerTexture.SetData(new[] { color });
            itemSlotTexture = new Texture2D(graphicsDevice, 50, 50);
            selectedItemSlotTexture = new Texture2D(graphicsDevice, 50, 50);
            Color[] data = new Color[itemSlotTexture.Width * itemSlotTexture.Height];
            Color[] selectedData = new Color[selectedItemSlotTexture.Width * selectedItemSlotTexture.Height];
            for (int i = 0; i < itemSlotTexture.Height; i++)
            {
                for (int j = 0; j < itemSlotTexture.Width; j++)
                {
                    data[(i * itemSlotTexture.Width) + j] = Color.White;
                    selectedData[(i * itemSlotTexture.Width) + j] = Color.Blue;
                    if (i == 0 || i == itemSlotTexture.Height - 1 || j == 0 || j == itemSlotTexture.Width - 1)
                    {
                        data[(i * itemSlotTexture.Width) + j] = Color.Black;
                        selectedData[(i * selectedItemSlotTexture.Width) + j] = Color.Black;
                    }
                }
            }
            itemSlotTexture.SetData(data);
            selectedItemSlotTexture.SetData(selectedData);

        }

        public void Update(MouseState mouseState)
        {
            Rectangle topMenu = new Rectangle(container.X, container.Y, container.Width, 40);
            if (mouseState.LeftButton == ButtonState.Pressed && topMenu.Contains(mouseState.Position))
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
                    inventoryItems[i].setLocation(new Vector2(container.X + 10 + 10 + ((i % 5) * 50), container.Y + 40 + 10 + ((i / 5) * 50)));
                }
            }

            previousMouseState = mouseState;
        }
    }
}
