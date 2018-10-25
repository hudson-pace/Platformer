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
        private static SpriteFont font;
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
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("ItemText");
        }
    }
}
