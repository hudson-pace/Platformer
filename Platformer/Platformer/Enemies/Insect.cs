using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Enemies
{
    class Insect : Enemy
    {
        private string facing = "right";
        private static Texture2D rightFacing, leftFacing;

        public Insect(Vector2 location, Location currentLocation, Spawner spawner, int howMany)
        {
            this.location = location;
            this.currentLocation = currentLocation;
            width = 60;
            height = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 50;
            this.spawner = spawner;
            name = "insect";
            this.howMany = howMany;
        }

        public override Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new Insect(location, currentLocation, spawner, 0);
        }

        override public void Update(Player player, Tile[][] tiles)
        {
            newLocation = location;

            if (facing == "right")
            {
                newLocation.X += 2;
            }
            else if (facing == "left")
            {
                newLocation.X -= 2;
            }
            if (Collisions.CollideWithTiles(tiles, this))
            {
                if (facing == "right")
                {
                    facing = "left";
                }
                else if (facing == "left")
                {
                    facing = "right";
                }
            }
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            base.Update(player, tiles);
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            if (facing == "right")
            {
                spriteBatch.Draw(rightFacing, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
            else if (facing == "left")
            {
                spriteBatch.Draw(leftFacing, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
        }
        public static void LoadTextures(ContentManager content)
        {
            rightFacing = content.Load<Texture2D>("insect-guy-facing-right");
            leftFacing = content.Load<Texture2D>("insect-guy-facing-left");
        }
    }
}
