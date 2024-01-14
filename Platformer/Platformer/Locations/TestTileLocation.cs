using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Diagnostics;

namespace Platformer.Locations
{
	internal class TestTileLocation : Location
	{
		private static string contentPath = "tiled-maps/tiled-map-test";
		public TestTileLocation(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
			: base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice, contentPath)
		{
			height = 20;
			width = 30;
			NPCList.Add(new NPCs.Wizard(new Vector2(300, 100), this, screenWidth, screenHeight, player));
		}
		public override void AddPortals()
		{
			portals.Add(new Portal(4, 5, 2, 3, this, 25, 5));
			portals.Add(new Portal(25, 5, 2, 3, this, 4, 5));

			portals.Add(new Portal(14, 3, 2, 3, Game1.slimeCity, 45, 26));
		}
		public override void Update(KeyboardState state, MouseState mouseState, OrthographicCamera camera, GameTime gameTime)
		{
			tiledMapRenderer.Update(gameTime);
			base.Update(state, mouseState, camera, gameTime);
		}
	}
}
