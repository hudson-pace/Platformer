using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    abstract class Enemy : Entity
    {
        public int health;
        public Location currentLocation;
        public List<Item> drops = new List<Item>();
        protected Player player;
        protected Spawner spawner = null;
        public string name;

        public Enemy()
        {
            isEnemy = true;
        }
        abstract public Enemy Create(Vector2 location, Location currentLocation, Spawner spawner);

        override public void Update(KeyboardState state, Tile[][] tiles)
        {
            if (this.state == "hurt")
            {
                hurtCounter--;
                if (hurtCounter <= 0)
                {
                    hurtCounter = 0;
                    this.state = "normal";
                }
            }

            Collisions.CollideWithTiles(tiles, this);
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }
        public void GetHit(String direction)
        {
            health -= 15;
            if (health <= 0)
            {
                active = false;
                drops.ForEach(drop =>
                {
                    drop.SetLocation(location);
                    currentLocation.AddItem(drop);
                });
                if (spawner != null)
                {
                    spawner.RemoveEnemy(this);
                }
            }

            state = "hurt";
            hurtCounter = 20;

            if (direction == "left")
            {
                horizontalVelocity -= 5;
            }
            else
            {
                horizontalVelocity += 5;
            }
            if (!isFalling)
            {
                verticalVelocity = -7;
            }
            else
            {
                verticalVelocity -= 3;
            }
            isFalling = true;
        }
    }
}
