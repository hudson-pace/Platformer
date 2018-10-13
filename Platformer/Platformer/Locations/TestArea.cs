﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Locations
{
    class TestArea : Location
    {
        public TestArea(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, ContentManager content) 
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, content)
        {
            enemies.Add(new Insect(new Vector2(100, 600)));
            enemies.Add(new Slime(new Vector2(700, 400), player));
            enemies.Add(new Slime(new Vector2(1000, 300), player));
            height = 15;
            width = 30;
            offsetX = 0;
            offsetY = 0;
            AddBorder();
            

            tiles[3][3] = new Tile(3, 3, true, true);
            tiles[6][6] = new Tile(6, 6, true, true);
            tiles[8][8] = new Tile(8, 8, true, true);

            tiles[0][10] = new Tile(0, 10, true, true);
            tiles[1][10] = new Tile(1, 10, true, true);
            tiles[2][10] = new Tile(2, 10, true, true);
            tiles[3][10] = new Tile(3, 10, true, true);
            tiles[4][10] = new Tile(4, 10, true, true);
            tiles[5][10] = new Tile(5, 10, true, true);
            tiles[6][10] = new Tile(6, 10, true, true);
            tiles[7][10] = new Tile(7, 10, true, true);
            tiles[8][10] = new Tile(8, 10, true, true);
            tiles[9][10] = new Tile(9, 10, true, true);
            tiles[10][10] = new Tile(10, 10, true, true);
        }


    }
}