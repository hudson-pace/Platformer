using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class InventoryItem : IComparable
    {
        private Item item;
        private Rectangle hitBox;
        private bool hovering = false;
        public InventoryItem(Item item, Vector2 location)
        {
            this.item = item;
            this.hitBox = new Rectangle((int)location.X, (int)location.Y, 50, 50);
            item.SetLocation(location);
        }
        public Rectangle GetHitBox()
        {
            return hitBox;
        }
        public Item GetItem()
        {
            return item;
        }
        public bool GetHovering()
        {
            return hovering;
        }
        public void SetHovering(bool hovering)
        {
            this.hovering = hovering;
        }
        public void SetLocation(Vector2 location)
        {
            hitBox.X = (int)location.X;
            hitBox.Y = (int)location.Y;
            item.SetLocation(location);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, -10, -10);
        }

        public int CompareTo(object obj)
        {
            InventoryItem item = obj as InventoryItem;
            if (GetItem().GetId() < item.GetItem().GetId())
            {
                return -1;
            }
            return 1;
        }
    }
}
