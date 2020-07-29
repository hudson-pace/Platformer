using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Enemies
{
    class SlimeRegular : Slime
    {
        
        private static Texture2D leftFacingTexture, rightFacingTexture, leftFacingHurtTexture, rightFacingHurtTexture;

        public SlimeRegular(Vector2 location, Location currentLocation, Spawner spawner, int howMany)
        {
            this.location = location;
            newLocation = location;
            this.currentLocation = currentLocation;
            jumpCooldown = 80;
            width = 100;
            height = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 100;
            drops.Add(new Items.SlimeItem(new int[] { 1, 1 }, new int[] { 80, 20 }));
            drops.Add(new Items.SlimeTail(new int[] { 1 }, new int[] { 40 }));
            drops.Add(new Items.CopperCoin(new int[] { 8, 2, 1 }, new int[] { 100, 80, 50 }));
            jumpHeight = 1f;
            Spawner = spawner;
            name = "slimeRegular";
            this.howMany = howMany;
            damage = 25;
            xp = 20;
        }

        public override Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new SlimeRegular(location, currentLocation, spawner, 0);
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

        
    }
}
