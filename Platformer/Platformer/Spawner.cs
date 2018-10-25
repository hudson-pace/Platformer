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
        private List<Enemy> currentAliveList;
        private Vector2 location;
        private int spawnInterval;
        private Location currentLocation;

        public Spawner(Vector2 location, List<Enemy> enemyList, Location currentLocation)
        {
            this.location = location;
            this.enemyList = enemyList;
            this.currentLocation = currentLocation;
            spawnInterval = 0;
            currentAliveList = new List<Enemy>();
        }
        public void RemoveEnemy(Enemy enemy)
        {
            foreach (Enemy e in currentAliveList)
            {
                if (enemy.name == e.name)
                {
                    enemy = e;
                    break;
                }
            }
            currentAliveList.Remove(enemy);
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
                foreach(Enemy enemy in enemyList)
                {
                    bool found = false;
                    Enemy temp = null;
                    foreach(Enemy enemy2 in currentAliveList)
                    {
                        if (enemy == enemy2)
                        {
                            found = true;
                            temp = enemy2;
                            break;
                        }
                    }
                    if (found)
                    {
                        currentAliveList.Remove(temp);
                    }
                    if (!found)
                    {
                        currentLocation.AddEnemy(enemy.Create(location, currentLocation, this));
                    }
                }
                currentAliveList = new List<Enemy>(enemyList);
            }
        }
    }
}
