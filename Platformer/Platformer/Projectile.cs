using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class Projectile
    {
        public Vector2 location;
        public Texture2D texture;
        public int velocity;
        public static int HEIGHT = 30;
        public static int WIDTH = 30;

        public Projectile(Vector2 location, Texture2D texture, int velocity)
        {
            this.location = location;
            this.velocity = velocity;
            this.texture = texture;
        }

        public void Update()
        {
            this.location.X += this.velocity;
        }

        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
    }
}
