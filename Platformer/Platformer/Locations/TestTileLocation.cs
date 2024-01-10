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

namespace Platformer.Locations
{
	internal class TestTileLocation : Location
	{
		TiledMap tiledMap;
		TiledMapRenderer tiledMapRenderer;
		public TestTileLocation(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
			: base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice)
		{
			height = 20;
			width = 30;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			/*
			for (int i = 0; i < tiles.Length; i++)
			{
				for (int j = 0; j < tiles[i].Length; j++)
				{
					if (tiles[i][j].isTextured)
					{
						tiles[i][j].Draw(spriteBatch);
					}
				}
			}
			portals.ForEach(portal => portal.Draw(spriteBatch));
			NPCList.ForEach(npc => npc.Draw(spriteBatch));
			enemies.ForEach(enemy => enemy.Draw(spriteBatch));
			items.ForEach(item => item.Draw(spriteBatch));
			*/
			tiledMapRenderer.Draw();
			player.Draw(spriteBatch);
		}
		public override void AddPortals()
		{
		}
		override public void LoadTextures(ContentManager content)
		{
			tiledMap = content.Load<TiledMap>("tiled-maps/tiled-map-test");
			tiledMapRenderer = new TiledMapRenderer(graphicsDevice, tiledMap);
		}
		public override void Update(KeyboardState state, MouseState mouseState, OrthographicCamera camera, GameTime gameTime)
		{
			tiledMapRenderer.Update(gameTime);
		}
	}
}
