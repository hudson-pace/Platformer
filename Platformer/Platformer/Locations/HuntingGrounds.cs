using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Platformer.Locations
{
    class HuntingGrounds : Location
    {
        public HuntingGrounds(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, ContentManager content) 
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, content)
        {
            height = 30;
            width = 60;
            offsetX = 0;
            offsetY = 0;
            AddBorder();
        }
    }
}
