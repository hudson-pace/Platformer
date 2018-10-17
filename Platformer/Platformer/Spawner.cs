using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Spawner
    {
        private List<Enemy> enemyList;
        private Vector2 location;
        private int spawnInterval;
        private Location currentLocation;

        public Spawner(Vector2 location, List<Enemy> enemyList, Location currentLocation)
        {
            this.location = location;
            this.enemyList = enemyList;
            this.currentLocation = currentLocation;
            spawnInterval = 0;
        }
        public void Update()
        {
            if (spawnInterval < 200)
            {
                spawnInterval++;
            }
            else
            {
                spawnInterval = 0;
                foreach(Enemy enemy in enemyList)
                {
                    currentLocation.AddEnemy(enemy.Create(location, currentLocation));
                }
            }
        }
    }
}
