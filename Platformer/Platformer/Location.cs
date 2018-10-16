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
    abstract class Location
    {
        public Tile[][] tiles;
        protected List<Enemy> enemies = new List<Enemy>();
        protected List<Projectile> projectiles = new List<Projectile>();
        protected List<Entity> entities = new List<Entity>();
        public Player player;
        public int height, width;
        public int offsetX, offsetY, screenGridWidth, screenGridHeight, screenWidth, screenHeight;
        private DialogBox dialogBox;
        private GraphicsDevice graphicsDevice;

        public Location(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            player.SetLocation(this);
            this.graphicsDevice = graphicsDevice;
        }
        public void CreateDialog(string text)
        {
            dialogBox = new DialogBox("hello", screenWidth, screenHeight);
            dialogBox.CreateTextures(graphicsDevice);
        }
        public void CloseDialog()
        {
            if (dialogBox != null)
            {
                dialogBox.Close();
                dialogBox = null;
            }
        }
        public bool HasOpenDialog()
        {
            if (dialogBox != null)
            {
                return true;
            }
            return false;
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
            entities.ForEach(entity => entity.Draw(spriteBatch, offsetX, offsetY));
            enemies.ForEach(enemy => enemy.Draw(spriteBatch, offsetX, offsetY));
            projectiles.ForEach(projectile => projectile.Draw(spriteBatch, offsetX, offsetY));
            player.Draw(spriteBatch, offsetX, offsetY);
            if (dialogBox != null)
            {
                dialogBox.Draw(spriteBatch);
            }
        }

        abstract public void LoadTextures(ContentManager content);
        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }
        public void Update(KeyboardState state)
        {
            player.Update(state, tiles);
            enemies.ForEach(enemy => enemy.Update(state, tiles));
            projectiles.ForEach(projectile => projectile.Update(state, tiles, enemies));
            entities.ForEach(entity => entity.Update(state, tiles));

            Enemy[] enemyTempList = new Enemy[enemies.Count];
            enemies.CopyTo(enemyTempList);
            enemies.Clear();
            foreach(Enemy enemy in enemyTempList)
            {
                if (enemy.active)
                {
                    enemies.Add(enemy);
                }
            }
            Projectile[] projectileTempList = new Projectile[projectiles.Count];
            projectiles.CopyTo(projectileTempList);
            projectiles.Clear();
            foreach(Projectile projectile in projectileTempList)
            {
                if (projectile.active)
                {
                    projectiles.Add(projectile);
                }
            };

            if (player.swordIsActive)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (Collisions.EntityCollisions(player.swordHitBox, enemy.hitBox))
                    {
                        enemy.GetHit(player.swingFacing);
                        break;
                    }
                }
                player.swordIsActive = false;
            }


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

            if ((player.location.Y - offsetY) > screenHeight - 300)
            {
                offsetY += (int)(player.location.Y - offsetY - (screenHeight - 300));
                if (offsetY > ((tiles[0].Length - 1) * 50) - screenHeight)
                {
                    offsetY = ((tiles[0].Length - 1) * 50) - screenHeight;
                }
            }
            else if ((player.location.Y - offsetY) < 300)
            {
                offsetY -= (int)(300 - player.location.Y + offsetY);
                if (offsetY < 50)
                {
                    offsetY = 50;
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
                    if ((j == height - 1) || (j == 0) || (i == 0) || (i == width - 1))
                    {
                        tiles[i][j] = new Tiles.InvisibleBarrier(i, j);
                    }
                    else
                    {
                        tiles[i][j] = new Tiles.Empty(i, j);
                    }
                }
            }
        }
    }
}
