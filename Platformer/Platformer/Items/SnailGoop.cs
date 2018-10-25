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

        public SnailGoop()
        {
            itemName = "snailGoop";
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("sticky-goop");
        }
    }
}