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
        private Location destination;
        private Vector2 positionDestination;

        public Portal(Vector2 position, Location destination, Vector2 positionDestination)
        {
            this.hitBox = new Rectangle((int)position.X, (int)position.Y, 100, 150);
            this.destination = destination;
            this.positionDestination = positionDestination;
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
