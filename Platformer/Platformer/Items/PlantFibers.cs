﻿using System;
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
        private static int itemId;


        public PlantFibers(int count) : base(count)
        {
            itemName = "plantFibers";
            id = itemId;
        }
        public PlantFibers(int[] count, int[] probability) : base(count, probability)
        {
            itemName = "plantFibers";
            id = itemId;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y + currentYOffset), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("plant-fibers");
        }
        public static void Register(int newId)
        {
            itemId = newId;
        }
    }
}
