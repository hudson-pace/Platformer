using System;

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

        public override void Update(Player player, Location l)
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
                Jump(player.location.X);
            }
            else if (!isFalling && cooldownCounter > 0)
            {
                cooldownCounter--;
            }

            base.Update(player, l);

        }

        public void Jump(float playerLocation)
        {
            if (playerLocation < location.X)
            {
                facing = "left";
            }
            else if (playerLocation > location.X)
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
