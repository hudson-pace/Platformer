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
        public Enemy[] enemies = new Enemy[3];
        public int height, width;
        public int offsetX, offsetY, screenGridWidth, screenGridHeight;
        public Player player;
        private ContentManager content;

        public Location(Player player, int screenGridWidth, int screenGridHeight, ContentManager content)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
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
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch, offsetX, offsetY);
            }
            player.Draw(spriteBatch, offsetX, offsetY);
        }
        public void SetTextures(Texture2D brickWall, Texture2D insectRightFacing, Texture2D insectLeftFacing, Texture2D door, Texture2D slimeLeftFacing, Texture2D slimeRightFacing)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].isTextured)
                    {
                        tiles[i][j].SetTexture(brickWall);
                    }
                }
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.LoadTextures(content);
            }
        }
        public void Update(KeyboardState state)
        {
            player.Update(state, tiles);
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(tiles);
            }
            if ((player.location.X - offsetX) > (screenGridWidth * 50) - 500)
            {
                if (offsetX + (screenGridWidth * 50) < tiles.Length * 50)
                {
                    offsetX += (int)(player.location.X - offsetX - ((screenGridWidth * 50) - 500));
                }
            }
            else if ((player.location.X - offsetX) < 500)
            {
                offsetX -= (int)(500 - player.location.X + offsetX);
                if (offsetX < 0)
                {
                    offsetX = 0;
                }
            }
        }
    }
}
