using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Items
{
    class SnailGoop : Item
    {
        private static Texture2D texture;
        private static int itemId;

        public SnailGoop(int count) : base(count)
        {
            itemName = "snailGoop";
            id = itemId;
        }
        public SnailGoop(int[] count, int[] probability) : base(count, probability)
        {
            itemName = "snailGoop";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("sticky-goop");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}