using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Portal
    {
        public Rectangle hitBox;
        private static Texture2D texture;
        private Location destination, currentLocation;
        private Vector2 positionDestination;

        public Portal(Vector2 position, Location destination, Vector2 positionDestination, Location currentLocation)
        {
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, 100, 150);
            this.destination = destination;
            this.positionDestination = positionDestination;
            this.currentLocation = currentLocation;

            Tile[][] tiles = currentLocation.tiles;
            int x = (int)(position.X / 50);
            int y = (int)(position.Y / 50);

            tiles[x][y] = new Tiles.PortalTile(x, y, currentLocation);
            tiles[x][y + 1] = new Tiles.PortalTile(x, y + 1, currentLocation);
            tiles[x][y + 2] = new Tiles.PortalTile(x, y + 2, currentLocation);
            tiles[x + 1][y] = new Tiles.PortalTile(x + 1, y, currentLocation);
            tiles[x + 1][y + 1] = new Tiles.PortalTile(x + 1, y + 1, currentLocation);
            tiles[x + 1][y + 2] = new Tiles.PortalTile(x + 1, y + 2, currentLocation);
        }
        public Location GetDestination()
        {
            return destination;
        }
        public Vector2 GetPositionDestination()
        {
            return positionDestination;
        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(hitBox.Left - offsetX, hitBox.Top - offsetY), Color.White);
        }
        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("portal");
        }
    }
}
