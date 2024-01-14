using Microsoft.Xna.Framework.Graphics;
using Platformer.Enemies;
using Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer.Locations
{
	internal class NewMain : Location
	{
		private static string contentPath = "tiled-maps/new-main";

		public NewMain(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
			: base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice, contentPath)
		{
			height = 24;
			width = 70;

			List<Enemy> enemyList = new List<Enemy>();
			enemyList.Add(new SlimeRegular(new Vector2(0, 0), this, null, 1));
			spawners.Add(new Spawner(new Vector2(4 * Globals.tileSize, 6 * Globals.tileSize), enemyList, this, 100));
			spawnPoint = new Vector2(60, 60);
		}

		public override void AddPortals()
		{

		}
	}
}