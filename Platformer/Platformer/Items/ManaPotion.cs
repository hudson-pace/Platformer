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
    class ManaPotion : Item
    {
        private static Texture2D texture;
        private static int itemId;


        public ManaPotion(int count) : base(count)
        {
            itemName = "manaPotion";
            id = itemId;
        }
        public ManaPotion(int[] count, int[] probability) : base(count, probability)
        {
            itemName = "manaPotion";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("mana-potion");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
