using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class DialogBox : Menu
    {
        private string text;
        private string[][] choices;
        private int screenWidth, screenHeight;
        private Texture2D containerTexture, textFieldTexture, optionsTexture, yellowTexture, blueTexture;
        private static SpriteFont font;
        private Rectangle container, textField, options;
        private Color color;
        private Rectangle[] choiceContainers;
        private Texture2D[] choiceTextures;
        private NPC npc;
        private MouseState previousState;
        private bool isActive;

        public DialogBox(string text, string[][] choices, int screenWidth, int screenHeight, NPC npc)
        {
            this.text = text;
            this.choices = choices;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.npc = npc;
            color = new Color(55, 220, 225, 240);
            container = new Rectangle((int)(screenWidth * .15), (int)(screenHeight * .6), (int)(screenWidth * .7), (int)(screenHeight * .3));
            textField = new Rectangle((int)(container.X + (container.Width * .02)), (int)(container.Y + (container.Height * .1)), (int)(container.Width * .96), (int)(container.Height * .2));
            options = new Rectangle((int)(container.X + (container.Width * .02)), (int)(container.Y + (container.Height * .4)), (int)(container.Width * .96), (int)(container.Height * .5));

            choiceContainers = new Rectangle[choices.Length];
            choiceTextures = new Texture2D[choices.Length];
            isActive = true;

            for (int i = 0; i < choiceContainers.Length; i++)
            {
                choiceContainers[i] = new Rectangle(options.X + 1, options.Y + 1 + (30 * i), options.Width - 2, 30);
            }
        }

        public bool GetIsActive()
        {
            return isActive;
        }
        public void SetText(string text, string[][] choices)
        {
            this.text = text;
            this.choices = choices;
            choiceContainers = new Rectangle[choices.Length];
            for (int i = 0; i < choiceContainers.Length; i++)
            {
                choiceContainers[i] = new Rectangle(options.X + 1, options.Y + 1 + (30 * i), options.Width - 2, 30);
            }
        }
        override public void Update(MouseState state)
        {
            for (int i = 0; i < choiceContainers.Length; i++)
            {
                if (choiceContainers[i].Contains(state.Position))
                {
                    choiceTextures[i] = blueTexture;
                    if ((state.LeftButton == ButtonState.Pressed) && !(previousState.LeftButton == ButtonState.Pressed))
                    {
                        npc.ChooseOption(choices[i][1]);
                    }
                }
                else
                {
                    choiceTextures[i] = yellowTexture;
                }
            }
            previousState = state;
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

            yellowTexture = new Texture2D(graphicsDevice, 1, 1);
            data = new Color[] { Color.Yellow };
            yellowTexture.SetData(data);
            blueTexture = new Texture2D(graphicsDevice, 1, 1);
            data = new Color[] { Color.Blue };
            blueTexture.SetData(data);

            for (int i = 0; i < choiceTextures.Length; i++)
            {
                choiceTextures[i] = yellowTexture;
            }

        }
        public void Close()
        {
            isActive = false;
            containerTexture.Dispose();
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(containerTexture, container, color);
            spriteBatch.Draw(textFieldTexture, textField, color);
            spriteBatch.Draw(optionsTexture, options, color);
            for (int i = 0; i < choiceContainers.Length; i++)
            {
                spriteBatch.Draw(choiceTextures[i], choiceContainers[i], color);
                spriteBatch.DrawString(font, choices[i][0], new Vector2(choiceContainers[i].X, choiceContainers[i].Y), Color.Black);
            }
            spriteBatch.DrawString(font, text, new Vector2(textField.X, textField.Y), Color.Black);
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("NPCText");
        }

        public string GetCurrentDialog()
        {
            return text;
        }
    }
}
