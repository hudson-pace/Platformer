using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Items
{
    class SlimeItem : Item
    {
        private static Texture2D texture;
        private static int itemId;

        public SlimeItem(int count) : base(count)
        {
            itemName = "slime";
            id = itemId;
        }
        public SlimeItem(int[] count, int[] probability) : base(count, probability)
        {
            itemName = "slime";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime-ball");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
