using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Projectile : Entity
    {
        public Texture2D texture;
        private Location currentLocation;
        private Player owner;

        public Projectile(Vector2 location, Texture2D texture, int horizontalVelocity, Location currentLocation, Player player)
        {
            this.location = location;
            this.horizontalVelocity = horizontalVelocity;
            this.texture = texture;
            this.currentLocation = currentLocation;
            this.owner = player;
            height = 30;
            width = 30;
            verticalVelocity = 0;
            isFalling = false;
            canFall = false;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public void Update(Tile[][] tiles)
        {
            newLocation = location;
            newLocation.X += horizontalVelocity;
            if (Collisions.CollideWithTiles(tiles, this))
            {
                owner.RemoveProjectile(this);
            }
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }
        

        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), Color.White);
        }
    }
}
