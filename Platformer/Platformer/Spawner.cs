﻿using System;
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
            this.enemyList = new List<Enemy>();
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
                enemy.active = false;
            }
        }
        public void Update(List<Enemy> enemies)
        {
            if (spawnInterval < 600)
            {
                spawnInterval++;
            }
            else
            {
                spawnInterval = 0;
                List<Enemy> enemiesToSpawn = new List<Enemy>(enemyList);
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].Spawner == this)
                    {
                        for (int j = 0; j < enemiesToSpawn.Count; j++)
                        {
                            if (enemies[i].name.Equals(enemiesToSpawn[j].name))
                            {
                                enemiesToSpawn.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
                for (int i = 0; i < enemiesToSpawn.Count; i++)
                {
                    enemiesToSpawn[i] = enemiesToSpawn[i].Create(location, currentLocation, this);
                    currentLocation.AddEnemy(enemiesToSpawn[i]);
                }
            }
        }
    }
}
