using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class InfoBox : Menu
    {
        private string text;
        private static Texture2D boxTexture;
        private Rectangle box;
        private Rectangle button;
        private bool buttonSelected;
        public InfoBox(string text)
        {
            this.text = text;
            buttonSelected = false;
            box = new Rectangle(100, 100, 200, 100);
            button = new Rectangle((int)(box.X + box.Width * .15), (int)(box.Y + box.Height * .6), (int)(box.Width * .7), (int)(box.Height * .3));
        }
        public static void CreateTextures(GraphicsDevice graphicsDevice)
        {
            boxTexture = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = new Color[] { Color.Blue };
            boxTexture.SetData(data);
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boxTexture, box, Color.White);
            if (buttonSelected)
            {
                spriteBatch.Draw(boxTexture, button, Color.Purple);
            }
            else
            {
                spriteBatch.Draw(boxTexture, button, Color.Violet);
            }
        }
        public override void Update(MouseState mouseState)
        {
            buttonSelected = false;
            if (button.Contains(mouseState.Position))
            {
                buttonSelected = true;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Close();
                }
            }
        }

        public void Close()
        {
            Game1.ToggleMenu(this);
        }
    }
}
