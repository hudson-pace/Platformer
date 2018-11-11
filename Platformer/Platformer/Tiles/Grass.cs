using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Tiles
{
    class Grass : UpdatableTile
    {
        private static Random random = new Random();
        private static Texture2D texture;
        private int baseGrowthTime, growthTime, growthTimer;
        private bool growing;
        public Grass(int x, int y, Location currentLocation) : base(x, y, currentLocation, true, true, true)
        {
            name = "grass";
            baseGrowthTime = 200;
            growthTime = random.Next(10, 16) * baseGrowthTime;
            growthTimer = 0;
            updatable = true;
            growing = true;
            isBreakable = false;
        }
        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("grass");
        }
        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
        override public void Update(Player player)
        {
            if (growing)
            {
                growthTimer++;
                if ((growthTimer > growthTime) && (random.Next(1, 10000) == 1))
                {
                    if (currentLocation.tiles[x][y - 1].GetName() == "empty")
                    {
                        currentLocation.tiles[x][y - 1] = new Plant(x, y - 1, currentLocation);
                    }
                    growing = false;
                }
            }
        }
    }
}
