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
            //enemies.Add(new Enemies.Snail(new Vector2(600, 300), player, this, null, 0));
            NPCList.Add(new NPCs.BusinessMan(new Vector2(300, 200), this));
            List<Enemy> enemyList = new List<Enemy>();
            enemyList.Add(new Enemies.SlimeDrip(new Vector2(51, 51), this, null, 1));
            enemyList.Add(new Enemies.SlimeRegular(new Vector2(0, 0), this, null, 1));
            enemyList.Add(new Enemies.Snail(new Vector2(50, 50), this, null, 1));
            spawners.Add(new Spawner(new Vector2(600, 300), enemyList, this));
            height = 22;
            width = 30;
            AddBorder();


            tiles[3][3] = new Tiles.BrickWall(3, 3, this);
            tiles[6][6] = new Tiles.BrickWall(6, 6, this);
            tiles[8][8] = new Tiles.BrickWall(8, 8, this);

            tiles[0][10] = new Tiles.BrickWall(0, 10, this);
            tiles[1][10] = new Tiles.BrickWall(1, 10, this);
            tiles[2][10] = new Tiles.BrickWall(2, 10, this);
            tiles[3][10] = new Tiles.BrickWall(3, 10, this);
            tiles[4][10] = new Tiles.BrickWall(4, 10, this);
            tiles[5][10] = new Tiles.BrickWall(5, 10, this);
            tiles[6][10] = new Tiles.BrickWall(6, 10, this);
            tiles[7][10] = new Tiles.BrickWall(7, 10, this);
            tiles[8][10] = new Tiles.BrickWall(8, 10, this);
            tiles[9][10] = new Tiles.BrickWall(9, 10, this);
            tiles[10][10] = new Tiles.BrickWall(10, 10, this);

            tiles[15][15] = new Tiles.EnemyBarrier(15, 15, this);
            tiles[15][14] = new Tiles.EnemyBarrier(15, 14, this);
            tiles[15][13] = new Tiles.EnemyBarrier(15, 13, this);
            tiles[16][15] = new Tiles.BrickWall(16, 15, this);
            tiles[17][15] = new Tiles.BrickWall(17, 15, this);
            tiles[18][15] = new Tiles.BrickWall(18, 15, this);
            tiles[19][15] = new Tiles.BrickWall(19, 15, this);
            tiles[20][15] = new Tiles.BrickWall(20, 15, this);
            tiles[21][15] = new Tiles.BrickWall(21, 15, this);
            tiles[22][15] = new Tiles.BrickWall(22, 15, this);
            tiles[23][15] = new Tiles.BrickWall(23, 15, this);
            tiles[24][15] = new Tiles.BrickWall(24, 15, this);
            tiles[25][15] = new Tiles.BrickWall(25, 15, this);
            tiles[26][15] = new Tiles.BrickWall(26, 15, this);
            tiles[27][15] = new Tiles.BrickWall(27, 15, this);
            tiles[28][15] = new Tiles.BrickWall(28, 15, this);
            tiles[13][17] = new Tiles.BrickWall(13, 17, this);

            for (int i = 1; i < 10; i++)
            {
                tiles[i][height - 2] = new Tiles.Dirt(i, height - 2, this);
                tiles[i][height - 3] = new Tiles.Grass(i, height - 3, this);
            }
            for (int i = 10; i < width - 1; i++)
            {
                tiles[i][height - 2] = new Tiles.Grass(i, height - 2, this);
            }
            spawnPoint = new Vector2(60, 60);
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(new Vector2(1300, 850), Game1.slimeCity, new Vector2(100, 1300), this));

            portals.Add(new Portal(new Vector2(100, 800), this, new Vector2(100, 400), this));
            portals.Add(new Portal(new Vector2(100, 350), this, new Vector2(100, 850), this));
        }
        override public void LoadTextures(ContentManager content)
        {
            Portal.LoadContent(content);
            DialogBox.LoadTextures(content);
            Tiles.BrickWall.LoadTextures(content);
            Tiles.Grass.LoadTextures(content);
            Tiles.Plant.LoadTextures(content);
            Tiles.Dirt.LoadTextures(content);
            Enemies.SlimeRegular.LoadTextures(content);
            Enemies.SlimeDrip.LoadTextures(content);
            NPCs.BusinessMan.LoadTextures(content);
            Enemies.Snail.LoadTextures(content);
        }
    }
}
