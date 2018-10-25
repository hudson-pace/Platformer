using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class InventoryItem
    {
        private int count;
        private Item item;
        public Vector2 location;
        public InventoryItem(Item item, int count, Vector2 location)
        {
            this.item = item;
            this.count = count;
            this.location = location;
            item.SetLocation(location);
        }
        public Item getItem()
        {
            return item;
        }
        public int getCount()
        {
            return count;
        }
        public void setCount(int count)
        {
            this.count = count;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, 0, 0);
        }
    }
}
