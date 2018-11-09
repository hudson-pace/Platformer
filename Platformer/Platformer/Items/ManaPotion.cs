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

        public ManaPotion(int count) : base(count)
        {
            itemName = "manaPotion";
            id = 7;
        }
        public ManaPotion(int count, int probability) : base(count, probability)
        {
            itemName = "manaPotion";
            id = 7;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("mana-potion");
        }
    }
}
