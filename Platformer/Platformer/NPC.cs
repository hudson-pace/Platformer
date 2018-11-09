using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    abstract class NPC : Entity
    {
        public string greeting;
        public string[][] options;
        protected DialogBox dialogBox;

        abstract public void Update(KeyboardState state, Tile[][] tiles, MouseState mouseState);

        abstract public void ChooseOption(string optionType);

        public bool HasOpenDialog()
        {
            if (dialogBox != null)
            {
                return true;
            }
            return false;
        }
        public void CreateDialog(int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            dialogBox = new DialogBox(greeting, options, screenWidth, screenHeight, this);
            dialogBox.CreateTextures(graphicsDevice);
        }
        public void CloseDialog()
        {
            if (dialogBox != null)
            {
                dialogBox.Close();
                dialogBox = null;
            }
        }
        abstract public void DrawDialog(SpriteBatch spriteBatch);
    }
}
