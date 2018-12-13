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
        protected string greetingDialog;
        protected string[][] greetingChoices;

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
            dialogBox = new DialogBox(greetingDialog, greetingChoices, screenWidth, screenHeight, this);
            dialogBox.CreateTextures(graphicsDevice);
            Game1.AddToMenuList(dialogBox);
        }
        public void CloseDialog()
        {
            if (dialogBox != null)
            {
                Game1.RemoveFromMenuList(dialogBox);
                dialogBox.Close();
                dialogBox = null;
            }
        }
    }
}
