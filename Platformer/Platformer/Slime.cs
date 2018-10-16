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
    class Slime : Enemy
    {
        private string facing = "left";
        private static Texture2D leftFacingTexture, rightFacingTexture, leftFacingHurtTexture, rightFacingHurtTexture;
        private Player player;
        private int jumpCooldown;
        private int currentFrame = 0;
        private int frameCounter = 0;
        private static Random random = new Random();

        public Slime(Vector2 location, Player player)
        {
            this.location = location;
            this.player = player;
            jumpCooldown = 50;
            width = 100;
            height = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 100;
            drops.Add(new Items.SlimeItem());
        }

        public override void Update(KeyboardState state, Tile[][] tiles)
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

            if (this.state == "hurt")
            {
                hurtCounter--;
                if (hurtCounter <= 0)
                {
                    hurtCounter = 0;
                    this.state = "normal";
                }
            }
            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
                newLocation.X += horizontalVelocity;
            }
            if (!isFalling && jumpCooldown == 0)
            {
                Jump();
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
            Items.SlimeItem.LoadTextures(content);
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
            if (facing == "left")
            {
                horizontalVelocity *= -1;
            }
            jumpCooldown = random.Next(7, 13) * 10;
        }
    }
}
