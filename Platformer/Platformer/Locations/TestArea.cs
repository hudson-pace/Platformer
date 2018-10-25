using System;
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
        public TestArea(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice) 
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice)
        {
            enemies.Add(new Enemies.SlimeRegular(new Vector2(1100, 400), player, this));
            enemies.Add(new Enemies.SlimeRegular(new Vector2(1000, 300), player, this));
            enemies.Add(new Enemies.SlimeDrip(new Vector2(600, 300), player, this));
            NPCList.Add(new NPCs.BusinessMan(new Vector2(300, 200)));
            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemies.SlimeDrip(new Vector2(51, 51), player, this));
            enemyList.Add(new Enemies.SlimeRegular(new Vector2(0, 0), player, this));
            spawners.Add(new Spawner(new Vector2(600, 300), enemyList, this));
            height = 22;
            width = 30;
            offsetX = 0;
            offsetY = 0;
            AddBorder();


            tiles[3][3] = new Tiles.BrickWall(3, 3);
            tiles[6][6] = new Tiles.BrickWall(6, 6);
            tiles[8][8] = new Tiles.BrickWall(8, 8);

            tiles[0][10] = new Tiles.BrickWall(0, 10);
            tiles[1][10] = new Tiles.BrickWall(1, 10);
            tiles[2][10] = new Tiles.BrickWall(2, 10);
            tiles[3][10] = new Tiles.BrickWall(3, 10);
            tiles[4][10] = new Tiles.BrickWall(4, 10);
            tiles[5][10] = new Tiles.BrickWall(5, 10);
            tiles[6][10] = new Tiles.BrickWall(6, 10);
            tiles[7][10] = new Tiles.BrickWall(7, 10);
            tiles[8][10] = new Tiles.BrickWall(8, 10);
            tiles[9][10] = new Tiles.BrickWall(9, 10);
            tiles[10][10] = new Tiles.BrickWall(10, 10);

            tiles[15][15] = new Tiles.EnemyBarrier(15, 15);
            tiles[15][14] = new Tiles.EnemyBarrier(15, 14);
            tiles[15][13] = new Tiles.EnemyBarrier(15, 13);
            tiles[16][15] = new Tiles.BrickWall(16, 15);
            tiles[17][15] = new Tiles.BrickWall(17, 15);
            tiles[18][15] = new Tiles.BrickWall(18, 15);
            tiles[19][15] = new Tiles.BrickWall(19, 15);
            tiles[20][15] = new Tiles.BrickWall(20, 15);
            tiles[21][15] = new Tiles.BrickWall(21, 15);
            tiles[22][15] = new Tiles.BrickWall(22, 15);
            tiles[23][15] = new Tiles.BrickWall(23, 15);
            tiles[24][15] = new Tiles.BrickWall(24, 15);
            tiles[25][15] = new Tiles.BrickWall(25, 15);
            tiles[26][15] = new Tiles.BrickWall(26, 15);
            tiles[27][15] = new Tiles.BrickWall(27, 15);
            tiles[28][15] = new Tiles.BrickWall(28, 15);
            tiles[13][17] = new Tiles.BrickWall(13, 17);

            for (int i = 1; i < 10; i++)
            {
                tiles[i][height - 2] = new Tiles.Dirt(i, height - 2);
                tiles[i][height - 3] = new Tiles.Grass(i, height - 3);
            }
            for (int i = 10; i < width - 1; i++)
            {
                tiles[i][height - 2] = new Tiles.Grass(i, height - 2);
            }

        }

        override public void LoadTextures(ContentManager content)
        {
            DialogBox.LoadTextures(content);
            Tiles.BrickWall.LoadTextures(content);
            Tiles.Grass.LoadTextures(content);
            Tiles.Dirt.LoadTextures(content);
            Enemies.SlimeRegular.LoadTextures(content);
            Enemies.SlimeDrip.LoadTextures(content);
            NPCs.BusinessMan.LoadTextures(content);
        }
    }
}
