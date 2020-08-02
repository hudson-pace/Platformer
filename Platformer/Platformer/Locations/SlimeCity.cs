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
            AddBorder();

            int i = height - 2;
            for (int j = 1; j < width - 1; j++) {
                tiles[j][i] = new Tiles.SlimeBlock(j, i, 10, this);
                //tiles[j][i] = new Tiles.Wall(j, i, this, Tiles.Wall.WallType.SlimeTop);
            }

            tiles[6][i] = new Tiles.SlimeBlock(6, i, 12, this);
            tiles[6][i - 1] = new Tiles.SlimeBlock(6, i - 1, 11, this);
            tiles[6][i - 2] = new Tiles.SlimeBlock(6, i - 2, 4, this);

            tiles[7][i] = new Tiles.SlimeBlock(7, i, 12, this);
            tiles[7][i - 1] = new Tiles.SlimeBlock(7, i - 1, 9, this);
            tiles[7][i - 2] = new Tiles.SlimeBlock(7, i - 2, 5, this);

            tiles[16][i] = new Tiles.SlimeBlock(16, i, 12, this);
            tiles[16][i - 1] = new Tiles.SlimeBlock(16, i - 1, 11, this);
            tiles[16][i - 2] = new Tiles.SlimeBlock(16, i - 2, 4, this);

            tiles[17][i] = new Tiles.SlimeBlock(17, i, 12, this);
            tiles[17][i - 1] = new Tiles.SlimeBlock(17, i - 1, 9, this);
            tiles[17][i - 2] = new Tiles.SlimeBlock(17, i - 2, 5, this);

            tiles[21][i - 3] = new Tiles.SlimeBlock(21, i - 3, 6, this);
            tiles[21][i - 4] = new Tiles.SlimeBlock(21, i - 4, 4, this);

            tiles[22][i - 3] = new Tiles.SlimeBlock(22, i - 3, 7, this);
            tiles[22][i - 4] = new Tiles.SlimeBlock(22, i - 4, 5, this);

            tiles[26][i - 4] = new Tiles.SlimeBlock(26, i - 4, 6, this);
            tiles[26][i - 5] = new Tiles.SlimeBlock(26, i - 5, 4, this);

            tiles[27][i - 4] = new Tiles.SlimeBlock(27, i - 4, 8, this);
            tiles[27][i - 5] = new Tiles.SlimeBlock(27, i - 5, 10, this);

            tiles[28][i] = new Tiles.SlimeBlock(28, i, 12, this);
            tiles[28][i - 1] = new Tiles.SlimeBlock(28, i - 1, 11, this);
            tiles[28][i - 2] = new Tiles.SlimeBlock(28, i - 2, 11, this);
            tiles[28][i - 3] = new Tiles.SlimeBlock(28, i - 3, 11, this);
            tiles[28][i - 4] = new Tiles.SlimeBlock(28, i - 4, 12, this);
            tiles[28][i - 5] = new Tiles.SlimeBlock(28, i - 5, 10, this);

            tiles[29][i] = new Tiles.SlimeBlock(29, i, 12, this);
            tiles[29][i - 1] = new Tiles.SlimeBlock(29, i - 1, 12, this);
            tiles[29][i - 2] = new Tiles.SlimeBlock(29, i - 2, 12, this);
            tiles[29][i - 3] = new Tiles.SlimeBlock(29, i - 3, 12, this);
            tiles[29][i - 4] = new Tiles.SlimeBlock(29, i - 4, 12, this);
            tiles[29][i - 5] = new Tiles.SlimeBlock(29, i - 5, 12, this);
            tiles[29][i - 6] = new Tiles.SlimeBlock(29, i - 6, 4, this);

            tiles[30][i] = new Tiles.SlimeBlock(30, i, 12, this);
            tiles[30][i - 1] = new Tiles.SlimeBlock(30, i - 1, 12, this);
            tiles[30][i - 2] = new Tiles.SlimeBlock(30, i - 2, 12, this);
            tiles[30][i - 3] = new Tiles.SlimeBlock(30, i - 3, 12, this);
            tiles[30][i - 4] = new Tiles.SlimeBlock(30, i - 4, 12, this);
            tiles[30][i - 5] = new Tiles.SlimeBlock(30, i - 5, 12, this);
            tiles[30][i - 6] = new Tiles.SlimeBlock(30, i - 6, 10, this);

            tiles[31][i] = new Tiles.SlimeBlock(31, i, 12, this);
            tiles[31][i - 1] = new Tiles.SlimeBlock(31, i - 1, 12, this);
            tiles[31][i - 2] = new Tiles.SlimeBlock(31, i - 2, 12, this);
            tiles[31][i - 3] = new Tiles.SlimeBlock(31, i - 3, 12, this);
            tiles[31][i - 4] = new Tiles.SlimeBlock(31, i - 4, 12, this);
            tiles[31][i - 5] = new Tiles.SlimeBlock(31, i - 5, 12, this);
            tiles[31][i - 6] = new Tiles.SlimeBlock(31, i - 6, 12, this);
            tiles[31][i - 7] = new Tiles.SlimeBlock(31, i - 7, 11, this);
            tiles[31][i - 8] = new Tiles.SlimeBlock(31, i - 8, 4, this);

            tiles[32][i] = new Tiles.SlimeBlock(32, i, 12, this);
            tiles[32][i - 1] = new Tiles.SlimeBlock(32, i - 1, 12, this);
            tiles[32][i - 2] = new Tiles.SlimeBlock(32, i - 2, 12, this);
            tiles[32][i - 3] = new Tiles.SlimeBlock(32, i - 3, 12, this);
            tiles[32][i - 4] = new Tiles.SlimeBlock(32, i - 4, 12, this);
            tiles[32][i - 5] = new Tiles.SlimeBlock(32, i - 5, 12, this);
            tiles[32][i - 6] = new Tiles.SlimeBlock(32, i - 6, 12, this);
            tiles[32][i - 7] = new Tiles.SlimeBlock(32, i - 7, 12, this);
            tiles[32][i - 8] = new Tiles.SlimeBlock(32, i - 8, 10, this);

            tiles[33][i] = new Tiles.SlimeBlock(33, i, 12, this);
            tiles[33][i - 1] = new Tiles.SlimeBlock(33, i - 1, 9, this);
            tiles[33][i - 2] = new Tiles.SlimeBlock(33, i - 2, 9, this);
            tiles[33][i - 3] = new Tiles.SlimeBlock(33, i - 3, 9, this);
            tiles[33][i - 4] = new Tiles.SlimeBlock(33, i - 4, 9, this);
            tiles[33][i - 5] = new Tiles.SlimeBlock(33, i - 5, 9, this);
            tiles[33][i - 6] = new Tiles.SlimeBlock(33, i - 6, 9, this);
            tiles[33][i - 7] = new Tiles.SlimeBlock(33, i - 7, 9, this);
            tiles[33][i - 8] = new Tiles.SlimeBlock(33, i - 8, 5, this);

            

            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemies.SlimeDrip(new Vector2(0, 0), this, null, 5));
            spawners.Add(new Spawner(new Vector2(450, (i - 1) * 50), enemyList, this));
            spawnPoint = new Vector2(60, 60);
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(new Vector2(100, 1250), Game1.testArea, new Vector2(1300, 900), this));

            portals.Add(new Portal(new Vector2(800, 1150), this, new Vector2(300, 1200), this));
            portals.Add(new Portal(new Vector2(300, 1150), this, new Vector2(800, 1200), this));
            portals.Add(new Portal(new Vector2(1300, 1250), Game1.slimeHut, new Vector2(100, 650), this));
        }
        override public void LoadTextures(ContentManager content)
        {
            Portal.LoadContent(content);
            DialogBox.LoadTextures(content);
            Tiles.SlimeBlock.LoadTextures(content);
            Enemies.SlimeDrip.LoadTextures(content);
        }
    }
}