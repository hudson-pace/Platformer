using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class Location
    {
        public Tile[][] tiles;
        protected List<Enemy> enemies = new List<Enemy>();
        public int height, width;
        public int offsetX, offsetY, screenGridWidth, screenGridHeight, screenWidth, screenHeight;
        public Player player;
        private ContentManager content;

        public Location(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, ContentManager content)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.content = content;
            player.SetLocation(this);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = (int)(offsetX / 50); i < tiles.Length && i < screenGridWidth + (int)(offsetX / 50) + 1; i++)
            {
                for (int j = (int)(offsetY / 50); j < tiles[i].Length && j < screenGridHeight + (int)(offsetY / 50) + 1; j++)
                {
                    if (tiles[i][j].isTextured)
                    {
                        tiles[i][j].Draw(spriteBatch, offsetX, offsetY);
                    }
                }
            }
            enemies.ForEach(enemy => enemy.Draw(spriteBatch, offsetX, offsetY));
            player.Draw(spriteBatch, offsetX, offsetY);
        }
        public void SetTextures()
        {
            foreach(Tile[] tileRow in tiles)
            {
                foreach(Tile tile in tileRow)
                {
                    tile.LoadTextures(content);
                }
            }
            enemies.ForEach(enemy => enemy.LoadTextures(content));
        }
        public void Update(KeyboardState state)
        {
            player.Update(state, tiles);
            enemies.ForEach(enemy => enemy.Update(tiles));

            if ((player.location.X - offsetX) > screenWidth - 500)
            {
                offsetX += (int)(player.location.X - offsetX - (screenWidth - 500));
                if (offsetX > ((tiles.Length - 1) * 50) - screenWidth) {
                    offsetX = ((tiles.Length - 1) * 50) - screenWidth;
                }
            }
            else if ((player.location.X - offsetX) < 500)
            {
                offsetX -= (int)(500 - player.location.X + offsetX);
                if (offsetX < 50)
                {
                    offsetX = 50;
                }
            }
        }

        public void AddBorder()
        {
            tiles = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                tiles[i] = new Tile[height];
                for (int j = 0; j < height; j++)
                {
                    if (j == height - 1)
                    {
                        tiles[i][j] = new Tile(i, j, true, true);
                    }
                    else if (j == 0)
                    {
                        tiles[i][j] = new Tile(i, j, true, false);
                    }
                    else if (i == 0 || i == width - 1)
                    {
                        tiles[i][j] = new Tile(i, j, true, false);
                    }
                    else
                    {
                        tiles[i][j] = new Tile(i, j, false, false);
                    }
                }
            }
        }
    }
}
