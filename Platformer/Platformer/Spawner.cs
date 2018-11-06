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
        private List<Enemy> deadEnemies;

        public Spawner(Vector2 location, List<Enemy> enemyList, Location currentLocation)
        {
            this.location = location;
            this.enemyList = new List<Enemy>();
            deadEnemies = new List<Enemy>();
            MakeEnemyList(enemyList);
            this.currentLocation = currentLocation;
            spawnInterval = 0;
            
        }
        public void MakeEnemyList(List<Enemy> enemiesToAdd)
        {
            foreach (Enemy enemy in enemiesToAdd)
            {
                for (int i = 0; i < enemy.howMany; i++)
                {
                    enemyList.Add(enemy.Create(location, currentLocation, this));
                }
            }
            foreach (Enemy enemy in enemyList)
            {
                deadEnemies.Add(enemy);
            }
        }
        public void RemoveEnemy(Enemy enemy)
        {
            deadEnemies.Add(enemy);
        }
        public void Update()
        {
            if (spawnInterval < 600)
            {
                spawnInterval++;
            }
            else
            {
                spawnInterval = 0;
                foreach(Enemy enemy in deadEnemies)
                {
                    currentLocation.AddEnemy(enemy.Create(location, currentLocation, this));
                }
                deadEnemies.Clear();
            }
        }
    }
}
