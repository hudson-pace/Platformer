using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
	internal class Portal
	{
		private static int animationDelay = 5;
		private static int textureWidth = 64;
		private static int textureHeight = 64;
		private static int animationFrameCount;
		private static Texture2D texture;

		private int animationFrame = 0;
		private int animationCounter = 0;

		public Vector2 Position { get; private set; }
		public Vector2 Size { get; private set; }
		public int ID { get; private set; }

		private int DestinationID { get; set; }
		public Location Location { get; private set; }

		public Portal(Vector2 position, Vector2 size, int id, int destinationID, Location location)
		{
			Position = position;
			Size = size;
			ID = id;
			DestinationID = destinationID;
			Location = location;

			Globals.portals.Add(id, this);
		}
		public void Update()
		{
			animationCounter++;
			if (animationCounter > animationDelay)
			{
				animationFrame++;
				animationCounter = 0;
				if (animationFrame >= animationFrameCount)
				{
					animationFrame = 0;
				}
			}
		}

		public Rectangle GetHitbox()
		{
			return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
		}

		public Portal GetDestination()
		{
			return Globals.portals.GetValueOrDefault(DestinationID);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle dest = new Rectangle((int)(Position.X - ((textureWidth * 2 - Size.X) / 2)), (int)(Position.Y - (textureHeight * 2 - Size.Y)), textureWidth * 2, textureHeight * 2);
			Rectangle source = new Rectangle(textureWidth * animationFrame, 0, textureWidth, textureHeight);
			spriteBatch.Draw(texture, dest, source, Color.White);
		}

		public static void LoadTextures(ContentManager content)
		{
			
			texture = content.Load<Texture2D>("purple-portal");
			animationFrameCount = texture.Width / textureWidth;
		}
	}
}
