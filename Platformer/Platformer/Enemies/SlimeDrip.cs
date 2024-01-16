using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Enemies
{
    class SlimeDrip : Slime
    {
        private static Texture2D regularTexture, hurtTexture;

        public SlimeDrip(Vector2 location, Location currentLocation, Spawner spawner, int howMany)
        {
            this.location = location;
            newLocation = location;
            this.currentLocation = currentLocation;
            jumpCooldown = 20;
            width = 40;
            height = 40;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 40;
			maxHealth = health;
			drops.Add(new Item("slimeItem", new int[] { 1 }, new int[] { 100 }));
            drops.Add(new Item("copperCoin", new int[] { 2, 1 }, new int[] { 100, 60 }));
            jumpHeight = .5f;
            Spawner = spawner;
            name = "slimeDrip";
            this.howMany = howMany;
            damage = 10;
            xp = 5;
        }
        override public Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new SlimeDrip(location, currentLocation, spawner, 0);
        }
        public static void LoadTextures(ContentManager content)
        {
            regularTexture = content.Load<Texture2D>("drip");
            hurtTexture = content.Load<Texture2D>("drip-hurt");
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(currentFrame * 40, 0, 40, 40);
            Texture2D texture = regularTexture;
            if (this.state == "hurt")
            {
                texture = hurtTexture;
            }
            spriteBatch.Draw(texture, new Vector2(location.X, location.Y), sourceRectangle, Color.White);
			base.Draw(spriteBatch);
		}
    }
}
