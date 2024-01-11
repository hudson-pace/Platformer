using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
	// NOTE: There is already a "Spawner" class, used to spawn enemies. I think eventually they'll be combined,
	// with an enemy spawner being a special case of EntitySpawner.

	// This will replace the old grass-growing mechanic for now.
	// Right now, that's all it does.
	
	// TODO: FINISH
	internal class EntitySpawner
	{
		private Location currentMap;
		private int x;
		private int y;
		private Plant plant;
        private int minGrowthTime, maxGrowthTime, actualGrowthTime, growthTimer;
        private static Random random;
		public EntitySpawner(Location currentMap, int x, int y) //, Entity entityToSpawn)
		{
			this.currentMap = currentMap;
			this.x = x;
			this.y = y;

			minGrowthTime = 2000;
			maxGrowthTime = 5000;

            random = new Random();
            RestartGrowthTimer();
		}
        private void RestartGrowthTimer()
        {
			actualGrowthTime = random.Next(minGrowthTime, maxGrowthTime);
            growthTimer = 0;
		}

		public void Update(Player player)
		{
            if (plant != null)
            {
                plant.Update(player);
                if (!plant.GetIsAlive())
                {
                    plant = null;
                    RestartGrowthTimer();
                }
            }
            else
            {
                growthTimer++;
                if (growthTimer > actualGrowthTime)
				{
                    plant = new Plant(x, y, currentMap);
                }
            }
		}
		public void Draw(SpriteBatch spriteBatch)
		{
            if (plant != null)
            {
                plant.Draw(spriteBatch);
            }
		}
	}
}