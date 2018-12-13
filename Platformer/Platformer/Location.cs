using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    abstract class Location
    {
        public Tile[][] tiles;
        protected List<Enemy> enemies = new List<Enemy>();
        protected List<Projectile> projectiles = new List<Projectile>();
        protected List<Spawner> spawners = new List<Spawner>();
        protected List<NPC> NPCList = new List<NPC>();
        protected List<Item> items = new List<Item>();
        protected List<Portal> portals = new List<Portal>();
        protected Vector2 spawnPoint;
        public Player player;
        public int height, width;
        public int offsetX, offsetY, screenGridWidth, screenGridHeight, screenWidth, screenHeight;
        private GraphicsDevice graphicsDevice;
        private bool previousQPressed = false;

        public Location(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            //player.SetLocation(this);
            this.graphicsDevice = graphicsDevice;
        }
        public abstract void AddPortals();
        
        public void AddTile(Tile tile, int x, int y)
        {
            if (tiles[x][y].GetName() == "empty")
            {
                tiles[x][y] = tile;
            }
        }
        public Vector2 GetSpawnPoint()
        {
            return spawnPoint;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public List<Portal> GetPortals()
        {
            return portals;
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
            portals.ForEach(portal => portal.Draw(spriteBatch, offsetX, offsetY));
            NPCList.ForEach(npc => npc.Draw(spriteBatch, offsetX, offsetY));
            enemies.ForEach(enemy => enemy.Draw(spriteBatch, offsetX, offsetY));
            projectiles.ForEach(projectile => projectile.Draw(spriteBatch, offsetX, offsetY));
            items.ForEach(item => item.Draw(spriteBatch, offsetX, offsetY));
            player.Draw(spriteBatch, offsetX, offsetY);
            //NPCList.ForEach(npc => npc.DrawDialog(spriteBatch));
        }

        abstract public void LoadTextures(ContentManager content);
        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public void Update(KeyboardState state, MouseState mouseState)
        {
            player.Update(state, tiles, mouseState);
            enemies.ForEach(enemy => enemy.Update(state, tiles));
            projectiles.ForEach(projectile => projectile.Update(state, tiles, enemies));
            spawners.ForEach(spawner => spawner.Update());
            items.ForEach(item => item.Update(state, tiles));
            NPCList.ForEach(npc => npc.Update(state, tiles, mouseState));
            

            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].updatable)
                    {
                        ((Tiles.UpdatableTile)(tiles[i][j])).Update(player);
                        if (((Tiles.UpdatableTile)(tiles[i][j])).isBreakable && Collisions.EntityCollisions(player.swordHitBox, new Rectangle((int)tiles[i][j].location.X, (int)tiles[i][j].location.Y, 50, 50)) && player.scytheIsActive)
                        {
                            Item drop = new Items.PlantFibers(2);
                            drop.ResetPickUpCounter();
                            drop.SetLocation(new Vector2(i * 50, j * 50));
                            AddItem(drop);
                            tiles[i][j] = new Tiles.Empty(i, j, this);
                            break;
                        }
                    }
                }
            }


            if (state.IsKeyDown(Keys.Q) && !previousQPressed)
            {
                foreach(NPC character in NPCList)
                {
                    if (character.HasOpenDialog())
                    {
                        character.CloseDialog();
                    }
                    else if (Collisions.EntityCollisions(player.hitBox, character.hitBox))
                    {
                        character.CreateDialog(screenWidth, screenHeight, graphicsDevice);
                        break;
                    }
                }
            }
            previousQPressed = state.IsKeyDown(Keys.Q);

            Item[] itemTempList = new Item[items.Count];
            items.CopyTo(itemTempList);
            items.Clear();
            foreach (Item item in itemTempList)
            {
                if (Collisions.EntityCollisions(item.hitBox, player.hitBox) && item.canBePickedUp)
                {
                    player.AddToInventory(item);
                }
                else
                {
                    items.Add(item);
                }
            }

            Enemy[] enemyTempList = new Enemy[enemies.Count];
            enemies.CopyTo(enemyTempList);
            enemies.Clear();
            foreach(Enemy enemy in enemyTempList)
            {
                if (enemy.active)
                {
                    enemies.Add(enemy);
                    if (!player.invulnerable)
                    {
                        if (Collisions.EntityCollisions(player.hitBox, enemy.hitBox))
                        {
                            player.GetHit("left", enemy.GetDamage());
                        }
                    }
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
                    if (Collisions.EntityCollisions(player.swordHitBox, enemy.hitBox) && player.swordIsActive)
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
                        tiles[i][j] = new Tiles.InvisibleBarrier(i, j, this);
                    }
                    else
                    {
                        tiles[i][j] = new Tiles.Empty(i, j, this);
                    }
                }
            }
        }
    }
}
