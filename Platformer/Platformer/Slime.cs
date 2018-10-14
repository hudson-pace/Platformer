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
    class Slime : Enemy
    {
        private string facing = "left";
        private static Texture2D leftFacingTexture, rightFacingTexture, leftFacingHurtTexture, rightFacingHurtTexture;
        private Player player;
        private int jumpCooldown;
        private int currentFrame = 0;
        private int frameCounter = 0;

        public Slime(Vector2 location, Player player)
        {
            this.location = location;
            this.player = player;
            jumpCooldown = 50;
            width = 100;
            height = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 100;
        }

        public override void Update(Tile[][] tiles)
        {
            newLocation = location;

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

            if (state == "hurt")
            {
                hurtCounter--;
                if (hurtCounter <= 0)
                {
                    hurtCounter = 0;
                    state = "normal";
                }
            }
            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
                if (facing == "left")
                {
                    newLocation.X -= 5;
                }
                else if (facing == "right")
                {
                    newLocation.X += 5;
                }
            }
            if (!isFalling && jumpCooldown == 0)
            {
                Jump();
                jumpCooldown = 100;
            }
            else if (!isFalling && jumpCooldown > 0)
            {
                jumpCooldown--;
            }


            Collisions.CollideWithTiles(tiles, this);
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * 100, 0, 100, 100);
            Texture2D texture;
            if (facing == "left")
            {
                if (state == "hurt")
                {
                    texture = leftFacingHurtTexture;
                }
                else
                {
                    texture = leftFacingTexture;
                }
            }
            else if (facing == "right")
            {
                if (state == "hurt")
                {
                    texture = rightFacingHurtTexture;
                }
                else
                {
                    texture = rightFacingTexture;
                }
            }
            else
            {
                texture = leftFacingTexture;
            }

            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);
        }

        public static void LoadTextures(ContentManager content)
        {
            leftFacingTexture = content.Load<Texture2D>("slime-facing-left");
            rightFacingTexture = content.Load<Texture2D>("slime-facing-right");
            leftFacingHurtTexture = content.Load<Texture2D>("slime-facing-left-hurt");
            rightFacingHurtTexture = content.Load<Texture2D>("slime-facing-right-hurt");

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
        }
    }
}
