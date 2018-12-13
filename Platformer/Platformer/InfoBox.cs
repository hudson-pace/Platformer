using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class InfoBox
    {
        private string text;
        private Texture2D boxTexture;
        private Rectangle box;
        public InfoBox(string text)
        {
            this.text = text;
            box = new Rectangle(100, 100, 200, 100);
        }
        public void CreateTextures(GraphicsDevice graphicsDevice)
        {
            boxTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[] { Color.Blue };
            boxTexture.SetData(data);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boxTexture, box, Color.White);
        }
    }
}
