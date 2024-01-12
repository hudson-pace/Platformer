using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Enemies
{
    class Snail : Enemy
    {
        private static Texture2D textureRegular, textureHurt, textureRegularRight, textureHurtRight;
        private string facing;
        private int checkFacingCounter;
        public Snail(Vector2 location, Location currentLocation, Spawner spawner, int howMany)
        {
            this.location = location;
            newLocation = location;
            this.currentLocation = currentLocation;
            width = 65;
            height = 50;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 80;
			maxHealth = health;
			drops.Add(new Items.SnailGoop(new int[] { 1 }, new int[] { 70 }));
            drops.Add(new Items.ShellFragment(new int[] { 1 }, new int[] { 100 }));
            drops.Add(new Items.CopperCoin(new int[] { 1, 2 }, new int[] { 100, 60 }));
            facing = "left";
            checkFacingCounter = 40;
            Spawner = spawner;
            name = "snail";
            this.howMany = howMany;
            damage = 15;
            xp = 10;
        }


        override public void Update(Player player, Location l)
        {
            if (isFalling)
            {
                this.newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }
            else
            {
                if (checkFacingCounter > 0)
                {
                    checkFacingCounter--;
                }
                if (checkFacingCounter == 0)
                {
                    checkFacingCounter = 40;
                    if (player.location.X < location.X)
                    {
                        facing = "left";
                        horizontalVelocity = -1;
                    }
                    else
                    {
                        facing = "right";
                        horizontalVelocity = 1;
                    }
                }
            }

            newLocation.X += horizontalVelocity;

            base.Update(player, l);
        }

        public override Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new Snail(location, currentLocation, spawner, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = textureRegular;
            if (state != "hurt" && facing == "right")
            {
                texture = textureRegularRight;
            }
            if (state == "hurt")
            {
                if (facing == "left")
                {
                    texture = textureHurt;
                }
                else
                {
                    texture = textureHurtRight;
                }
            }
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), Color.White);
			base.Draw(spriteBatch);
		}
        public static void LoadTextures(ContentManager content)
        {
            textureRegular = content.Load<Texture2D>("snail");
            textureHurt = content.Load<Texture2D>("snail-hurt");
            textureRegularRight = content.Load<Texture2D>("snail-right");
            textureHurtRight = content.Load<Texture2D>("snail-hurt-right");
        }
    }
}
