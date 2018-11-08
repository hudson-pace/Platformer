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
    class PlantFibers : Item
    {
        private static Texture2D texture;

        public PlantFibers(int count) : base(count)
        {
            itemName = "plantFibers";
            id = 5;
        }
        public PlantFibers(int count, int probability) : base(count, probability)
        {
            itemName = "plantFibers";
            id = 5;
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("plant-fibers");
        }
    }
}
