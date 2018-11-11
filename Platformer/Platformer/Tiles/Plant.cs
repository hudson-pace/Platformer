﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Tiles
{
    class Plant : UpdatableTile
    {
        private static Texture2D texture;

        public Plant(int x, int y, Location currentLocation) : base(x, y, currentLocation, false, true, false)
        {
            name = "plant";
            updatable = true;
            isBreakable = true;
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("plant");
        }
        public override void Update(Player player)
        {
        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
