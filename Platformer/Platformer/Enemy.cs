using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    abstract class Enemy : Character
    {
        public int health;
        public Enemy()
        {
            isEnemy = true;
        }
        abstract public void Update(Tile[][] tiles);
        abstract public void Draw(SpriteBatch spritebatch, int offsetX, int offsetY);
        public bool GetHit(String direction)
        {
            health -= 15;
            if (health <= 0)
            {
                return true;
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


            return false;
        }
    }
}
