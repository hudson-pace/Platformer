using System;
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
        private static Color color = new Color(55, 220, 225, 240);
        private bool isActive = false, dragging = false, draggingItem = false;
        private InventoryItem selectedItem;
        private MouseState previousMouseState;
        private Player player;
        private int screenWidth, screenHeight;

        public Inventory(Player player, int screenWidth, int screenHeight)
        {
            this.player = player;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            container = new Rectangle(500, 200, 270, 400);
        }

        public void Toggle()
        {
            isActive = !isActive;
            if (container.X < 0)
            {
                container.X = 0;
            }
            else if (container.X > (screenWidth - container.Width))
            {
                container.X = screenWidth - container.Width;
            }
            if (container.Y < 0)
            {
                container.Y = 0;
            }
            else if (container.Y > (screenHeight - container.Height))
            {
                container.Y = screenHeight - container.Height;
            }
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryItems[i].SetLocation(new Vector2(container.X + 10 + ((i % 5) * 50), container.Y + 40 + ((i / 5) * 50)));
            }
        }

        public bool GetIsActive()
        {
            return isActive;
        }
        public void AddToInventory(Item item)
        {
            foreach (InventoryItem i in inventoryItems)
            {
                if (i.GetItem().itemName == item.itemName) {
                    i.GetItem().SetCount(i.GetItem().GetCount() + item.GetCount());
                    return;
                }
            }
            inventoryItems.Add(new InventoryItem(item, new Vector2(container.X + 10 + ((inventoryItems.Count % 5) * 50), container.Y + 40 + ((inventoryItems.Count / 5) * 50))));
            ReSort();
        }
        public void ReSort()
        {
            inventoryItems.Sort();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryItems[i].SetLocation(new Vector2(container.X + 10 + ((i % 5) * 50), container.Y + 40 + ((i / 5) * 50)));
            }
        }
        public bool RemoveFromInventory(InventoryItem item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].GetItem().GetId() == item.GetItem().GetId())
                {
                    if (inventoryItems[i].GetItem().GetCount() < item.GetItem().GetCount())
                    {
                        return false;
                    }
                    else if (inventoryItems[i].GetItem().GetCount() == item.GetItem().GetCount())
                    {
                        inventoryItems.Remove(inventoryItems[i]);
                        ReSort();
                        return true;
                    }
                    else
                    {
                        inventoryItems[i].GetItem().SetCount(inventoryItems[i].GetItem().GetCount() - item.GetItem().GetCount());
                        return true;
                    }
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            


            spriteBatch.Draw(containerTexture, container, color);

            int itemCount = inventoryItems.Count;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Texture2D texture = itemSlotTexture;
                    if (((i * 5) + j) < itemCount)
                    {
                        if (inventoryItems[(i * 5) + j].GetHovering())
                        {
                            texture = selectedItemSlotTexture;
                        }
                    }
                    spriteBatch.Draw(texture, new Vector2(10 + (container.X + (j * 50)), 40 + (container.Y + (i * 50))), color);
                }
            }

            foreach (InventoryItem item in inventoryItems)
            {
                item.Draw(spriteBatch);
                spriteBatch.DrawString(font, item.GetItem().GetCount() + "", new Vector2(item.GetHitBox().X + item.GetItem().width + 10, item.GetHitBox().Y + item.GetItem().height + 6), Color.Black);
            }
            if (selectedItem != null)
            {
                selectedItem.Draw(spriteBatch);
            }

            foreach (InventoryItem item in inventoryItems)
            {
                if (item.GetHovering())
                {
                    item.DrawPopupText(spriteBatch, font);
                    break;
                }
            }
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("ItemText");
        }
        public static void CreateTextures(GraphicsDevice graphicsDevice)
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

            InventoryItem.CreateTextures(graphicsDevice);
        }

        public void Update(MouseState mouseState)
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
                    inventoryItems[i].SetLocation(new Vector2(container.X + 10 + ((i % 5) * 50), container.Y + 40 + ((i / 5) * 50)));
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
                    }
                }
            }
            if (select)
            {
                RemoveFromInventory(selectedItem);
            }

            if (draggingItem)
            {
                selectedItem.SetLocation(new Vector2(mouseState.Position.X - 25, mouseState.Position.Y - 25));
                if (mouseState.LeftButton != ButtonState.Pressed)
                {
                    if (container.Contains(mouseState.Position))
                    {
                        AddToInventory(selectedItem.GetItem());
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
