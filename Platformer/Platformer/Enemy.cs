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
        private static Random random = new Random();
        public int howMany;
        protected int damage, xp;

        public Enemy()
        {
            isEnemy = true;
        }
        abstract public Enemy Create(Vector2 location, Location currentLocation, Spawner spawner);

        virtual public void Update(KeyboardState state, Tile[][] tiles)
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
        public int GetDamage()
        {
            return damage;
        }
        public void GetHit(String direction)
        {
            health -= 15;
            if (health <= 0)
            {
                active = false;
                currentLocation.RemoveEnemy(this);
                drops.ForEach(drop =>
                {
                    if (drop.GetCount() > 0)
                    {
                        drop.SetLocation(new Vector2(location.X + (width / 2), location.Y));
                        currentLocation.AddItem(drop);
                    }
                });
                player.AddXp(xp);
                return;
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
