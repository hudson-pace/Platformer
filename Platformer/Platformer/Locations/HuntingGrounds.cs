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

            Console.WriteLine(tiles.Length + ", " + tiles[0].Length);

            for (int i = 1; i < 58; i++)
            {
                tiles[i][28] = new Tile(i, 28, true, true, true);
            }
            for (int i = 22; i > 5; i-= 6)
            {
                for (int j = 5; j < 26; j++)
                {
                    tiles[j][i] = new Tile(j, i, true, true, true);
                }
                tiles[2][i + 3] = new Tile(2, i + 3, true, true, true);
            }
        }
    }
}
