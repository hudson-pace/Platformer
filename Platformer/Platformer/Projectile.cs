using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Projectile : Entity
    {
        public Texture2D texture;

        public Projectile(Vector2 location, Texture2D texture, int horizontalVelocity)
        {
            this.location = location;
            this.horizontalVelocity = horizontalVelocity;
            this.texture = texture;
            height = 30;
            width = 30;
            verticalVelocity = 0;
            isFalling = false;
            canFall = false;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public bool Update(Tile[][] tiles)
        {
            newLocation = location;
            newLocation.X += horizontalVelocity;
            if (Collisions.CollideWithTiles(tiles, this))
            {
                return true;
            }
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
