using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Enemies
{
    abstract class Slime : Enemy
    {
        protected string facing = "left";
        protected int jumpCooldown, cooldownCounter;
        protected int currentFrame = 0;
        protected int frameCounter = 0;
        protected static Random random = new Random();
        protected float jumpHeight;

        public override void Update(KeyboardState state, Tile[][] tiles)
        {
            

            frameCounter++;
            if (frameCounter > 8)
            {
                currentFrame++;
                frameCounter = 0;
            }
            if (currentFrame > 3)
            {
                currentFrame = 0;
            }

            
            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
                newLocation.X += horizontalVelocity;
            }
            if (!isFalling && cooldownCounter == 0)
            {
                Jump();
            }
            else if (!isFalling && cooldownCounter > 0)
            {
                cooldownCounter--;
            }

            base.Update(state, tiles);

        }

        public void Jump()
        {
            if (player.location.X < location.X)
            {
                facing = "left";
            }
            else if (player.location.X > location.X)
            {
                facing = "right";
            }
            isFalling = true;
            verticalVelocity = -10;
            verticalVelocity = random.Next(-13, -9);
            horizontalVelocity = random.Next(4, 6);
            verticalVelocity = (int)(verticalVelocity * jumpHeight);
            if (facing == "left")
            {
                horizontalVelocity *= -1;
            }
            cooldownCounter = (int)((random.Next(7, 13) * jumpCooldown) / 10);
        }
    }
}
