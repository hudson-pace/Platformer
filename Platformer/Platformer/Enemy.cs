using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Platformer
{
    abstract class Enemy : Entity
    {
        public int health;
        public Location currentLocation;
        public List<Item> drops = new List<Item>();
        public Enemy(Location currentLocation)
        {
            this.currentLocation = currentLocation;
            isEnemy = true;
        }
        abstract public Enemy Create(Vector2 location, Location currentLocation);
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
