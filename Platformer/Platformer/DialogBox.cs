using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class DialogBox
    {
        private string text;
        private int screenWidth, screenHeight;
        private Texture2D containerTexture, textFieldTexture;
        private static SpriteFont font;
        private Rectangle container, textField;
        private Color color;
        
        public DialogBox(string text, int screenWidth, int screenHeight)
        {
            this.text = text;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            color = new Color(55, 220, 225, 240);
            container = new Rectangle((int)(screenWidth * .15), (int)(screenHeight * .6), (int)(screenWidth * .7), (int)(screenHeight * .3));
            textField = new Rectangle((int)(container.X + (container.Width * .02)), (int)(container.Y + (container.Height * .1)), (int)(container.Width * .96), (int)(container.Height * .5));
            
        }
        public void CreateTextures(GraphicsDevice graphicsDevice)
        {
            containerTexture = new Texture2D(graphicsDevice, 1, 1);
            containerTexture.SetData(new[] { color });
            textFieldTexture = new Texture2D(graphicsDevice, textField.Width, textField.Height);
            Color[] data = new Color[textField.Width * textField.Height];
            /*for (int i = 0; i < textField.Height; i++)
            {
                for (int j = 0; j < textField.Width; j++)
                {
                    if (i == 0)//j == 0 || j == textField.Height - 1 || i == 0 || i == textField.Width - 1)
                    {
                        data[(i * textField.Height) + j] = Color.Black;
                    }
                    else
                    {
                        data[(i * textField.Height) + j] = Color.White;
                    }
                }
            }
            textFieldTexture.SetData(data);*/
            //for (int i = 0; i < data.Length; i++)
            //{
            //    data[i] = Color.White;
            //}
            for (int i = 0; i < textFieldTexture.Height; i++)
            {
                for (int j = 0; j < textFieldTexture.Width; j++)
                {
                    data[(i * textFieldTexture.Width) + j] = Color.White;
                    if (i == 0 || i == textFieldTexture.Height - 1 || j == 0 || j == textFieldTexture.Width - 1)
                    {
                        data[(i * textFieldTexture.Width) + j] = Color.Black;
                    }
                }
            }
            textFieldTexture.SetData(data);
        }
        public void Close()
        {
            containerTexture.Dispose();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(containerTexture, container, color);
            spriteBatch.Draw(textFieldTexture, textField, color);
            spriteBatch.DrawString(font, text, new Vector2(textField.X, textField.Y), Color.Black);
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("NPCText");
        }
    }
}
