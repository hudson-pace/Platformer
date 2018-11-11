using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Items
{
    class SwordItem : Item
    {
        private static Texture2D texture;
        private static int itemId = 8;

        public SwordItem(int count) : base(count)
        {
            itemName = "sword";
            id = itemId;
        }
        public SwordItem(int count, int probability) : base(count, probability)
        {
            itemName = "sword";
            id = itemId;
        }
        public static int GetItemId()
        {
            return itemId;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("sword-item");
        }
    }
}
