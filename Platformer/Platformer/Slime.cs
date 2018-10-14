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
        private Texture2D leftFacingTexture, rightFacingTexture;
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
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * 100, 0, 100, 100);
            if (facing == "left")
            {
                spriteBatch.Draw(leftFacingTexture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);
            }
            else if (facing == "right")
            {
                spriteBatch.Draw(rightFacingTexture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);
            }
        }

        public override void LoadTextures(ContentManager content)
        {
            this.leftFacingTexture = content.Load<Texture2D>("slime-facing-left");
            this.rightFacingTexture = content.Load<Texture2D>("slime-facing-right");
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
