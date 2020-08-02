using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Items
{
    class ShellFragment : Item
    {
        private static Texture2D texture;
        private static int itemId;


        public ShellFragment(int count) : base(count)
        {
            itemName = "shellFragment";
            id = itemId;
        }
        public ShellFragment(int[] count, int[] probability) : base(count, probability)
        {
            itemName = "shellFragment";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("shell-fragment");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
