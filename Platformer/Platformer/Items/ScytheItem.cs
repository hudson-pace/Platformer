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
    class ScytheItem : Item
    {
        private static Texture2D texture;
        private static int itemId;


        public ScytheItem(int count) : base(count)
        {
            itemName = "scythe";
            id = itemId;
        }
        public ScytheItem(int count, int probability) : base(count, probability)
        {
            itemName = "scythe";
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
            texture = content.Load<Texture2D>("scythe-item");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
