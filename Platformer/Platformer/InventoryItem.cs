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
        private Rectangle hitBox;
        public InventoryItem(Item item, int count, Vector2 location)
        {
            this.item = item;
            this.count = count;
            this.hitBox = new Rectangle((int)location.X, (int)location.Y, 50, 50);
            item.SetLocation(location);
        }
        public Rectangle GetHitBox()
        {
            return hitBox;
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
        public void setLocation(Vector2 location)
        {
            hitBox.X = (int)location.X;
            hitBox.Y = (int)location.Y;
            item.SetLocation(location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, 0, 0);
        }
    }
}
