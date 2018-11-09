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
    class HealthPotion : Item
    {
        private static Texture2D texture;

        public HealthPotion(int count) : base(count)
        {
            itemName = "healthPotion";
            id = 6;
        }
        public HealthPotion(int count, int probability) : base(count, probability)
        {
            itemName = "healthPotion";
            id = 6;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("health-potion");
        }
    }
}
