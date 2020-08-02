using System;
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
        private Rectangle hitBox;

        public Plant(int x, int y, Location currentLocation) : base(x, y, currentLocation, false, true, false)
        {
            name = "plant";
            isBreakable = true;
            this.hitBox = new Rectangle(x * 50, y * 50, 50, 50);
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("plant");
        }
        public override void Update(Player player)
        {
            if (player.scytheIsActive && Collisions.EntityCollisions(player.swordHitBox, hitBox))
            {
                Item drop = new Items.PlantFibers(2);
                drop.ResetPickUpCounter();
                drop.SetLocation(new Vector2(hitBox.Left, hitBox.Top));
                currentLocation.AddItem(drop);
                currentLocation.ReplaceTile(x, y, new Tiles.Empty(x, y, currentLocation));
            }
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), Color.White);
        }
    }
}
