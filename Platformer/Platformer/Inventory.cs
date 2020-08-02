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
    class Inventory : Menu
    {
        protected List<InventoryItem> inventoryItems = new List<InventoryItem>();
        protected static Texture2D containerTexture, itemSlotTexture, selectedItemSlotTexture;
        protected static SpriteFont font;
        protected Rectangle container;
        protected static Color color = new Color(55, 220, 225, 240);
        protected bool isActive = false, dragging = false, draggingItem = false;
        protected InventoryItem selectedItem;
        protected MouseState previousMouseState;
        protected int screenWidth, screenHeight;
        protected int money;

        public Inventory(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            container = new Rectangle(500, 200, 270, 400);
        }

        override public void Update(MouseState mouseState)
        {

        }

        public void Toggle()
        {
            if (!isActive)
            {
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
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    inventoryItems[i].SetLocation(new Vector2(container.X + 20 + ((i % 5) * 50), container.Y + 50 + ((i / 5) * 50)));
                }
            }
            Game1.ToggleMenu(this);
            isActive = !isActive;
        }

        public bool GetIsActive()
        {
            return isActive;
        }
        public void AddToInventory(Item item, bool storeItem, int price)
        {
            bool found = false;
            foreach (InventoryItem i in inventoryItems)
            {
                if (i.GetItem().itemName == item.itemName)
                {
                    i.GetItem().Count = i.GetItem().Count + item.Count;
                    found = true;
                    break;
                }
            }

            if (item.itemName == "copperCoin")
            {
                money += item.Count;
            }
            else if (item.itemName == "silverCoin")
            {
                money += (item.Count * 100);
            }
            else if (item.itemName == "goldCoin")
            {
                money += (item.Count * 10000);
            }

            if (found && item.itemName == "copperCoin")
            {
                item.Count = 100;
                if (RemoveFromInventory(new InventoryItem(item, new Vector2(0, 0))))
                {
                    AddToInventory(new Items.SilverCoin(1), false, 0);
                }
            }
            else if (found && item.itemName == "silverCoin")
            {
                item.Count = 100;
                if (RemoveFromInventory(new InventoryItem(item, new Vector2(0, 0))))
                {
                    AddToInventory(new Items.GoldCoin(1), false, 0);
                }
            }
            
            if (found)
            {
                return;
            }
            if (storeItem)
            {
                inventoryItems.Add(new InventoryItem(item, new Vector2(container.X + 10 + ((inventoryItems.Count % 5) * 50), container.Y + 40 + ((inventoryItems.Count / 5) * 50)), price));
            }
            else
            {
                inventoryItems.Add(new InventoryItem(item, new Vector2(container.X + 10 + ((inventoryItems.Count % 5) * 50), container.Y + 40 + ((inventoryItems.Count / 5) * 50))));
            }
            
            ReSort();
        }
        public void ReSort()
        {
            inventoryItems.Sort();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryItems[i].SetLocation(new Vector2(container.X + 20 + ((i % 5) * 50), container.Y + 50 + ((i / 5) * 50)));
            }
        }
        public bool RemoveFromInventory(InventoryItem item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].GetItem().GetId() == item.GetItem().GetId())
                {
                    if (inventoryItems[i].GetItem().Count < item.GetItem().Count)
                    {
                        return false;
                    }
                    else if (inventoryItems[i].GetItem().Count == item.GetItem().Count)
                    {
                        inventoryItems.Remove(inventoryItems[i]);
                        ReSort();
                        return true;
                    }
                    else
                    {
                        inventoryItems[i].GetItem().Count = inventoryItems[i].GetItem().Count - item.GetItem().Count;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Pay(int price)
        {
            if (price > money)
            {
                return false;
            }

            money -= price;
            RemoveFromInventory(new InventoryItem(new Items.GoldCoin(price / 10000), new Vector2(0, 0)));
            price = price % 100;
            RemoveFromInventory(new InventoryItem(new Items.SilverCoin(price / 100), new Vector2(0, 0)));
            price = price % 100;
            RemoveFromInventory(new InventoryItem(new Items.CopperCoin(price / 1), new Vector2(0, 0)));

            return true;
        }
        override public void Draw(SpriteBatch spriteBatch)
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
                spriteBatch.DrawString(font, item.GetItem().Count + "", new Vector2(item.GetHitBox().X + item.GetItem().width, item.GetHitBox().Y + item.GetItem().height - 4), Color.Black);
            }
            if (selectedItem != null)
            {
                selectedItem.Draw(spriteBatch);
            }
            foreach (InventoryItem item in inventoryItems)
            {
                if (item.GetHovering())
                {
                    item.DrawPopupText(spriteBatch);
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
    }
}
