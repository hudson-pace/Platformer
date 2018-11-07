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
        private string[] choices;
        private int screenWidth, screenHeight;
        private Texture2D containerTexture, textFieldTexture, optionsTexture;
        private static SpriteFont font;
        private Rectangle container, textField, options;
        private Color color;
        private Rectangle[] choiceContainers;
        private Texture2D[] choiceTextures;

        public DialogBox(string text, string[] choices, int screenWidth, int screenHeight)
        {
            this.text = text;
            this.choices = choices;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            color = new Color(55, 220, 225, 240);
            container = new Rectangle((int)(screenWidth * .15), (int)(screenHeight * .6), (int)(screenWidth * .7), (int)(screenHeight * .3));
            textField = new Rectangle((int)(container.X + (container.Width * .02)), (int)(container.Y + (container.Height * .1)), (int)(container.Width * .96), (int)(container.Height * .2));
            options = new Rectangle((int)(container.X + (container.Width * .02)), (int)(container.Y + (container.Height * .4)), (int)(container.Width * .96), (int)(container.Height * .5));

            choiceContainers = new Rectangle[choices.Length];
            choiceTextures = new Texture2D[choices.Length];

            for (int i = 0; i < choiceContainers.Length; i++)
            {
                choiceContainers[i] = new Rectangle(options.X + 1, options.Y + 1 + (30 * i), options.Width - 2, 30);
            }
        }
        public void Update()
        {

        }
        public void CreateTextures(GraphicsDevice graphicsDevice)
        {
            containerTexture = new Texture2D(graphicsDevice, 1, 1);
            containerTexture.SetData(new[] { color });
            textFieldTexture = new Texture2D(graphicsDevice, textField.Width, textField.Height);
            Color[] data = new Color[textField.Width * textField.Height];
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

            

            data = new Color[options.Width * options.Height];
            optionsTexture = new Texture2D(graphicsDevice, options.Width, options.Height);
            for (int i = 0; i < optionsTexture.Height; i++)
            {
                for (int j = 0; j < optionsTexture.Width; j++)
                {
                    data[(i * options.Width) + j] = Color.White;
                    if (i == 0 || i == options.Height - 1 || j == 0 || j == options.Width - 1)
                    {
                        data[(i * options.Width) + j] = Color.Black;
                    }
                }
            }
            optionsTexture.SetData(data);


            for (int i = 0; i < choiceContainers.Length; i++)
            {
                data = new Color[choiceContainers[i].Width * choiceContainers[i].Height];
                choiceTextures[i] = new Texture2D(graphicsDevice, choiceContainers[i].Width, choiceContainers[i].Height);
                for (int j = 0; j < choiceContainers[i].Height; j++)
                {
                    for (int k = 0; k < choiceContainers[i].Width; k++)
                    {
                        data[(j * choiceContainers[i].Width) + k] = Color.Yellow;
                    }
                    
                }
                choiceTextures[i].SetData(data);
            }

        }
        public void Close()
        {
            containerTexture.Dispose();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(containerTexture, container, color);
            spriteBatch.Draw(textFieldTexture, textField, color);
            spriteBatch.Draw(optionsTexture, options, color);
            for (int i = 0; i < choiceContainers.Length; i++)
            {
                spriteBatch.Draw(choiceTextures[i], choiceContainers[i], color);
                spriteBatch.DrawString(font, choices[i], new Vector2(choiceContainers[i].X, choiceContainers[i].Y), Color.Black);
            }
            spriteBatch.DrawString(font, text, new Vector2(textField.X, textField.Y), Color.Black);
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("NPCText");
        }
    }
}
