﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class InventoryItem : IComparable
    {
        private Item item;
        private Rectangle hitBox, textRect, priceRect;
        private static Texture2D popUpTextTexture;
        private static SpriteFont font;
        private bool hovering;
        private bool storeItem;
        private int price;
        public InventoryItem(Item item, Vector2 location)
        {
            hovering = false;
            storeItem = false;
            this.item = item;
            this.hitBox = new Rectangle((int)location.X, (int)location.Y, 50, 50);
            item.SetLocation(location);
        }
        public InventoryItem(Item item, Vector2 location, int price)
            : this(item, location)
        {
            storeItem = true;
            this.price = price;
        }
        public Rectangle GetHitBox()
        {
            return hitBox;
        }
        public Item GetItem()
        {
            return item;
        }
        public int GetPrice()
        {
            return price;
        }
        public bool GetHovering()
        {
            return hovering;
        }
        public void SetHovering(bool hovering, MouseState mouseState)
        {
            this.hovering = hovering;
            if (hovering && !storeItem)
            {
                textRect = new Rectangle(mouseState.X, mouseState.Y - 15, 80, 15);
            }
            else if (hovering && storeItem)
            {
                textRect = new Rectangle(mouseState.X, mouseState.Y - 30, 80, 15);
                priceRect = new Rectangle(mouseState.X, mouseState.Y - 15, 80, 15);
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
            item.Draw(spriteBatch);
        }
        public void DrawPopupText(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(popUpTextTexture, textRect, Color.White);
            spriteBatch.DrawString(font, item.itemName + " " + item.GetId(), new Vector2(textRect.X, textRect.Y), Color.White);
            if (storeItem)
            {
                spriteBatch.Draw(popUpTextTexture, priceRect, Color.White);
                spriteBatch.DrawString(font, price + " copper", new Vector2(priceRect.X, priceRect.Y), Color.White);
            }
        }

        public static void CreateTextures(GraphicsDevice graphics)
        {
            popUpTextTexture = new Texture2D(graphics, 1, 1);
            Color[] data = new Color[] { Color.Gray };
            popUpTextTexture.SetData(data);
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("ItemText");
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
