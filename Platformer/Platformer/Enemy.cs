using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Platformer
{
    abstract class Enemy : Entity
    {
        public int health;
        public Location currentLocation;
        public List<Item> drops = new List<Item>();
        public Spawner Spawner { get; set; } = null;
        public string name;
        private static Random random = new Random();
        public int howMany;
        protected int damage, xp;

        public Enemy()
        {
            isEnemy = true;
        }
        abstract public Enemy Create(Vector2 location, Location currentLocation, Spawner spawner);

        virtual public void Update(Player player, Tile[][] tiles)
        {
            Collisions.CollideWithTiles(tiles, this);
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);

            if (state == "hurt")
            {
                hurtCounter--;
                if (hurtCounter <= 0)
                {
                    hurtCounter = 0;
                    state = "normal";
                }
            }
            else if (player.swordIsActive && Collisions.EntityCollisions(player.swordHitBox, hitBox))
            {
                GetHit(player, player.swingFacing);
                player.swordIsActive = false;
            }
            else
            {
                foreach (Projectile projectile in player.Projectiles)
                {
                    if (Collisions.EntityCollisions(projectile.hitBox, hitBox))
                    {
                        string direction = "left";
                        if (projectile.horizontalVelocity > 0)
                        {
                            direction = "right";
                        }
                        GetHit(player, direction);
                        player.RemoveProjectile(projectile);
                        break;
                    }
                }
            }

            if (!player.invulnerable && Collisions.EntityCollisions(player.hitBox, hitBox))
            {
                player.GetHit("left", GetDamage());
            }
        }
        public int GetDamage()
        {
            return damage;
        }
        public void GetHit(Player player, string direction)
        {
            health -= 15;
            if (health <= 0)
            {
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
