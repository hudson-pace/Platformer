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
    class InventoryItem : IComparable
    {
        private Item item;
        private Rectangle hitBox, textRect;
        private static Texture2D popUpTextTexture;
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
        public void SetHovering(bool hovering, MouseState mouseState)
        {
            this.hovering = hovering;
            if (hovering)
            {
                textRect = new Rectangle(mouseState.X, mouseState.Y - 15, 80, 15);
            }
            
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
        public void DrawPopupText(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(popUpTextTexture, textRect, Color.White);
            spriteBatch.DrawString(font, item.itemName, new Vector2(textRect.X, textRect.Y), Color.White);
        }

        public static void CreateTextures(GraphicsDevice graphics)
        {
            popUpTextTexture = new Texture2D(graphics, 1, 1);
            Color[] data = new Color[] { Color.Gray };
            popUpTextTexture.SetData(data);
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
