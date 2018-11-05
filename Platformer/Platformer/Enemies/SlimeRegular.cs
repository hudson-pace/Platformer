using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Enemies
{
    class SlimeRegular : Slime
    {
        
        private static Texture2D leftFacingTexture, rightFacingTexture, leftFacingHurtTexture, rightFacingHurtTexture;

        public SlimeRegular(Vector2 location, Player player, Location currentLocation, Spawner spawner)
        {
            this.location = location;
            newLocation = location;
            this.currentLocation = currentLocation;
            this.player = player;
            jumpCooldown = 80;
            width = 100;
            height = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 100;
            drops.Add(new Items.SlimeItem(1, 80));
            drops.Add(new Items.SlimeItem(1, 20));
            drops.Add(new Items.SlimeTail(1, 40));
            jumpHeight = 1f;
            this.spawner = spawner;
            name = "slimeRegular";
        }

        public override Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new SlimeRegular(location, player, currentLocation, spawner);
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
            Items.SlimeTail.LoadTextures(content);
        }

        
    }
}
