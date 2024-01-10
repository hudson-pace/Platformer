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

        public NPC()
        {
            dialogBox = null;
        }

        abstract public void Update(KeyboardState state, Location l, MouseState mouseState);

        abstract public void ChooseOption(string optionType);

        public DialogBox GetDialogBox()
        {
            return dialogBox;
        }


        public void ToggleDialog(int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            if (dialogBox != null && dialogBox.GetIsActive())
            {
                dialogBox.Close();
            }
            else
            {
                dialogBox = new DialogBox(greetingDialog, greetingChoices, screenWidth, screenHeight, this);
                dialogBox.CreateTextures(graphicsDevice);
            }
            Game1.ToggleMenu(dialogBox);
        }
        public void CloseDialog()
        {
            dialogBox.Close();
            Game1.ToggleMenu(dialogBox);
        }
    }
}
