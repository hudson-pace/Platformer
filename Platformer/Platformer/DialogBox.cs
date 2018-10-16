using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class DialogBox
    {
        private string text;
        private int screenWidth, screenHeight;
        private Texture2D customTexture;
        
        public DialogBox(string text, int screenWidth, int screenHeight)
        {
            this.text = text;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }
        public void CreateTextures(GraphicsDevice graphicsDevice)
        {
            customTexture = new Texture2D(graphicsDevice, 1, 1);
            customTexture.SetData(new[] { Color.White });
        }
        public void Close()
        {
            customTexture.Dispose();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(customTexture, new Rectangle((int)(screenWidth * .05), (int)(screenHeight * .7), (int)(screenWidth * .9), (int)(screenHeight * .2)), Color.White);
        }
    }
}
