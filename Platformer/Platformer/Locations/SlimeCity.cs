using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Locations
{
    class SlimeCity : Location
    {

        public SlimeCity(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice)
        {
            height = 30;
            width = 50;
            offsetX = 0;
            offsetY = 0;
            AddBorder();

            int i = height - 2;
            for (int j = 1; j < width - 1; j++) {
                tiles[j][i] = new Tiles.SlimeBlock(j, i, 10);
            }

            tiles[16][i] = new Tiles.SlimeBlock(16, i, 12);
            tiles[17][i] = new Tiles.SlimeBlock(17, i, 12);
            tiles[16][i - 1] = new Tiles.SlimeBlock(16, i - 1, 11);
            tiles[17][i - 1] = new Tiles.SlimeBlock(17, i - 1, 9);
            tiles[16][i - 2] = new Tiles.SlimeBlock(16, i - 2, 4);
            tiles[17][i - 2] = new Tiles.SlimeBlock(17, i - 2, 5);

            tiles[21][i - 3] = new Tiles.SlimeBlock(21, i - 3, 6);
            tiles[22][i - 3] = new Tiles.SlimeBlock(22, i - 3, 7);
            tiles[21][i - 4] = new Tiles.SlimeBlock(21, i - 4, 4);
            tiles[22][i - 4] = new Tiles.SlimeBlock(22, i - 4, 5);

            tiles[26][i - 5] = new Tiles.SlimeBlock(26, i - 5, 6);
            tiles[27][i - 5] = new Tiles.SlimeBlock(27, i - 5, 7);
            tiles[26][i - 6] = new Tiles.SlimeBlock(26, i - 6, 4);
            tiles[27][i - 6] = new Tiles.SlimeBlock(27, i - 6, 5);

            tiles[31][i] = new Tiles.SlimeBlock(31, i, 8);
            tiles[32][i] = new Tiles.SlimeBlock(32, i, 8);
            tiles[33][i] = new Tiles.SlimeBlock(33, i, 8);
            tiles[31][i - 1] = new Tiles.SlimeBlock(31, i - 1, 11);
            tiles[32][i - 1] = new Tiles.SlimeBlock(32, i - 1, 12);
            tiles[33][i - 1] = new Tiles.SlimeBlock(33, i - 1, 9);
            tiles[31][i - 2] = new Tiles.SlimeBlock(31, i - 2, 11);
            tiles[32][i - 2] = new Tiles.SlimeBlock(32, i - 2, 12);
            tiles[33][i - 2] = new Tiles.SlimeBlock(33, i - 2, 9);
            tiles[31][i - 3] = new Tiles.SlimeBlock(31, i - 3, 11);
            tiles[32][i - 3] = new Tiles.SlimeBlock(32, i - 3, 12);
            tiles[33][i - 3] = new Tiles.SlimeBlock(33, i - 3, 9);
            tiles[31][i - 4] = new Tiles.SlimeBlock(31, i - 4, 11);
            tiles[32][i - 4] = new Tiles.SlimeBlock(32, i - 4, 12);
            tiles[33][i - 4] = new Tiles.SlimeBlock(33, i - 4, 9);
            tiles[31][i - 5] = new Tiles.SlimeBlock(31, i - 5, 11);
            tiles[32][i - 5] = new Tiles.SlimeBlock(32, i - 5, 12);
            tiles[33][i - 5] = new Tiles.SlimeBlock(33, i - 5, 9);
            tiles[31][i - 6] = new Tiles.SlimeBlock(31, i - 6, 11);
            tiles[32][i - 6] = new Tiles.SlimeBlock(32, i - 6, 12);
            tiles[33][i - 6] = new Tiles.SlimeBlock(33, i - 6, 9);
            tiles[31][i - 7] = new Tiles.SlimeBlock(31, i - 7, 11);
            tiles[32][i - 7] = new Tiles.SlimeBlock(32, i - 7, 12);
            tiles[33][i - 7] = new Tiles.SlimeBlock(33, i - 7, 9);
            tiles[31][i - 8] = new Tiles.SlimeBlock(31, i - 8, 4);
            tiles[32][i - 8] = new Tiles.SlimeBlock(32, i - 8, 10);
            tiles[33][i - 8] = new Tiles.SlimeBlock(33, i - 8, 5);

            tiles[6][i] = new Tiles.SlimeBlock(6, i, 12);
            tiles[7][i] = new Tiles.SlimeBlock(7, i, 12);
            tiles[6][i - 1] = new Tiles.SlimeBlock(6, i - 1, 11);
            tiles[7][i - 1] = new Tiles.SlimeBlock(7, i - 1, 9);
            tiles[6][i - 2] = new Tiles.SlimeBlock(6, i - 2, 4);
            tiles[7][i - 2] = new Tiles.SlimeBlock(7, i - 2, 5);

            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemies.SlimeDrip(new Vector2(0, 0), player, this, null, 5));
            spawners.Add(new Spawner(new Vector2(450, (i - 1) * 50), enemyList, this));
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(new Vector2(100, 1250), Game1.testArea, new Vector2(1300, 900)));
        }
        override public void LoadTextures(ContentManager content)
        {
            DialogBox.LoadTextures(content);
            Tiles.SlimeBlock.LoadTextures(content);
        }
    }
}
