using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Locations
{
    class HuntingGrounds : Location
    {
        public HuntingGrounds(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice) 
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice)
        {
            height = 30;
            width = 60;
            offsetX = 0;
            offsetY = 0;
            AddBorder();

            Console.WriteLine(tiles.Length + ", " + tiles[0].Length);

            for (int i = 1; i < 58; i++)
            {
                tiles[i][28] = new Tiles.BrickWall(i, 28, this);
            }
            for (int i = 22; i > 5; i-= 6)
            {
                for (int j = 5; j < 26; j++)
                {
                    tiles[j][i] = new Tiles.BrickWall(j, i, this);
                }
                tiles[2][i + 3] = new Tiles.BrickWall(2, i + 3, this);
            }
            spawnPoint = new Vector2(60, 60);
        }

        public override void AddPortals()
        {
            throw new NotImplementedException();
        }
        override public void LoadTextures(ContentManager content)
        {
            DialogBox.LoadTextures(content);
            Tiles.BrickWall.LoadTextures(content);
        }
    }
}
