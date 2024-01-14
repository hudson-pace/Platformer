using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;

namespace Platformer.Locations
{
    class TestArea : Location
    {
        private static string contentPath = "tiled-maps/test-area";
        public TestArea(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice) 
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice, contentPath)
        {
            //enemies.Add(new Enemies.Snail(new Vector2(600, 300), player, this, null, 0));
            NPCList.Add(new NPCs.BusinessMan(new Vector2(300, 200), this));
            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemies.SlimeDrip(new Vector2(51, 51), this, null, 1));
            enemyList.Add(new Enemies.SlimeRegular(new Vector2(0, 0), this, null, 1));
            enemyList.Add(new Enemies.Snail(new Vector2(50, 50), this, null, 1));
            spawners.Add(new Spawner(new Vector2(600, 300), enemyList, this));
            height = 22;
            width = 30;
            spawnPoint = new Vector2(60, 60);

            entitySpawners.Add(new EntitySpawner(this, 6, 19));
			entitySpawners.Add(new EntitySpawner(this, 7, 19));
			entitySpawners.Add(new EntitySpawner(this, 8, 19));
		}

        public override void AddPortals()
        {
            portals.Add(new Portal(27, 18, 2, 3, Game1.slimeCity, 2, 26));

            portals.Add(new Portal(3, 17, 2, 3, this, 3, 8));
            portals.Add(new Portal(3, 8, 2, 3, this, 3, 17));
        }
    }
}
