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
            tiles[3][28] = new Tile(3, 28, true, true);
            tiles[4][25] = new Tile(4, 25, true, true);
            tiles[5][22] = new Tile(5, 22, true, true);
            tiles[6][19] = new Tile(6, 19, true, true);
            tiles[7][16] = new Tile(7, 16, true, true);
            tiles[8][13] = new Tile(8, 13, true, true);
            tiles[9][10] = new Tile(9, 10, true, true);
            tiles[10][7] = new Tile(10, 7, true, true);
            tiles[11][4] = new Tile(11, 4, true, true);
        }
    }
}
