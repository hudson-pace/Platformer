using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class Inventory
    {
        private List<InventoryItem> inventoryItems = new List<InventoryItem>();
        private static Texture2D containerTexture, itemSlotTexture;
        private static SpriteFont font;
        private Rectangle container;
        private Color color = new Color(55, 220, 225, 240);
        private bool isActive = true;

        public Inventory()
        {
            container = new Rectangle(0, 0, 300, 500);
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
            foreach (InventoryItem item in inventoryItems)
            {
                item.Draw(spriteBatch);
                spriteBatch.DrawString(font, item.getCount() + "", new Vector2(item.location.X + item.getItem().width, item.location.Y + item.getItem().height), Color.Black);
            }


            spriteBatch.Draw(containerTexture, container, color);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    spriteBatch.Draw(itemSlotTexture, new Vector2(20 + (container.X + i * 60), 20 + (container.Y + j * 60)), color);
                }
            }
            //spriteBatch.Draw(textFieldTexture, textField, color);
            //spriteBatch.DrawString(font, text, new Vector2(textField.X, textField.Y), Color.Black);

            int itemsInInventory = inventoryItems.Count;
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
            Color[] data = new Color[itemSlotTexture.Width * itemSlotTexture.Height];
            for (int i = 0; i < itemSlotTexture.Height; i++)
            {
                for (int j = 0; j < itemSlotTexture.Width; j++)
                {
                    data[(i * itemSlotTexture.Width) + j] = Color.White;
                    if (i == 0 || i == itemSlotTexture.Height - 1 || j == 0 || j == itemSlotTexture.Width - 1)
                    {
                        data[(i * itemSlotTexture.Width) + j] = Color.Black;
                    }
                }
            }
            itemSlotTexture.SetData(data);

            


        }
    }
}
