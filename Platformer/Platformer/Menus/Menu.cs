using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    abstract class Menu
    {
        abstract public void Draw(SpriteBatch spriteBatch);
        abstract public void Update(MouseState mouseState);
    }
}
