using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System.Reflection.Metadata;
using Platformer.Enemies;

namespace Platformer.Locations
{
    class SlimeCity : Location
    {
		private static string contentPath = "tiled-maps/slime-city";
		public SlimeCity(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice, contentPath)
        {
            height = 30;
            width = 50;

			List<Enemy> enemyList = new List<Enemy>();
            // enemyList.Add(new Enemies.SlimeDrip(new Vector2(0, 0), this, null, 5));
            enemyList.Add(new SlimeRegular(new Vector2(0, 0), this, null, 1));
            spawners.Add(new Spawner(new Vector2(450, 27 * Globals.tileSize), enemyList, this, 100));
            spawnPoint = new Vector2(60, 60);
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(2, 26, 2, 3, Game1.testArea, 27, 18));

            portals.Add(new Portal(15, 24, 2, 3, this, 6, 24));
            portals.Add(new Portal(6, 24, 2, 3, this, 15, 24));
            portals.Add(new Portal(25, 26, 2, 3, Game1.slimeHut, 1, 13));

            portals.Add(new Portal(45, 26, 2, 3, Game1.testTileLocation, 14, 3));
		}
    }
}