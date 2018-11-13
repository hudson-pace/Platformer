using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Platformer.Items
{
    class GoldCoin : Item
    {
        private static Texture2D texture;
        private static int itemId;

        public GoldCoin(int count) : base(count)
        {
            itemName = "goldCoin";
            id = itemId;
        }
        public GoldCoin(int count, int probability) : base(count, probability)
        {
            itemName = "goldCoin";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("gold-coin");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
