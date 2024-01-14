using Microsoft.Xna.Framework.Graphics;
using Platformer.Enemies;
using Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}

		public override void AddPortals()
		{

		}
	}
}