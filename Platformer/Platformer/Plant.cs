using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
	internal class Plant : Entity
	{
		private static Texture2D texture;
		private Location currentMap;
		private bool isAlive;
		public Plant(int x, int y, Location currentMap)
		{
			isAlive = true;
			height = Globals.tileSize;
			width = Globals.tileSize;
			location = new Vector2(x * Globals.tileSize, y * Globals.tileSize);
			hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
			this.currentMap = currentMap;
		}
		public bool GetIsAlive()
		{
			return isAlive;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, new Vector2(location.X, location.Y), Color.White);
		}
		public static void LoadTextures(ContentManager content)
		{
			texture = content.Load<Texture2D>("plant");
		}
		public void Update(Player player)
		{
			if (player.scytheIsActive && Collisions.EntityCollisions(player.swordHitBox, hitBox))
			{
				Item drop = new Item("plantFibers", 2);
				drop.ResetPickUpCounter();
				drop.SetLocation(new Vector2(hitBox.Left, hitBox.Top));
				currentMap.AddItem(drop);
				// currentMap.ReplaceTile(x, y, new Tiles.Empty(x, y, currentMap));
				isAlive = false;
			}
		}
	}
}