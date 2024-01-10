using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace Platformer
{
    abstract class Location
    {
        public Tile[][] tiles;
        protected List<Enemy> enemies = new List<Enemy>();
        protected List<Spawner> spawners = new List<Spawner>();
        protected List<NPC> NPCList = new List<NPC>();
        protected List<Item> items = new List<Item>();
        protected List<Portal> portals = new List<Portal>();
        protected Vector2 spawnPoint;
        public Player player;
        public int height, width;
        public int screenGridWidth, screenGridHeight, screenWidth, screenHeight;
        protected GraphicsDevice graphicsDevice;
        private bool previousQPressed = false;

        public Location(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].isTextured)
                    {
                        tiles[i][j].Draw(spriteBatch);
                    }
                }
            }
            portals.ForEach(portal => portal.Draw(spriteBatch));
            NPCList.ForEach(npc => npc.Draw(spriteBatch));
            enemies.ForEach(enemy => enemy.Draw(spriteBatch));
            items.ForEach(item => item.Draw(spriteBatch));
            player.Draw(spriteBatch);
        }

        abstract public void LoadTextures(ContentManager content);
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public virtual void Update(KeyboardState state, MouseState mouseState, OrthographicCamera camera, GameTime gameTime)
        {
            player.Update(state, tiles, mouseState);
            for (int i = enemies.Count - 1; i >= 0; i--) // updating may cause enemy to be removed, so iterate backwards.
            {
                enemies[i].Update(player, tiles);
            }
            spawners.ForEach(spawner => spawner.Update(enemies));
            items.ForEach(item => item.Update(state, tiles));
            NPCList.ForEach(npc => npc.Update(state, tiles, mouseState));
            
            foreach (Tile[] row in tiles)
            {
                foreach (Tile tile in row)
                {
                    if (tile is Tiles.UpdatableTile)
                    {
                        ((Tiles.UpdatableTile)tile).Update(player);
                    }
                }
            }


            if (state.IsKeyDown(Keys.Q) && !previousQPressed)
            {
                foreach(NPC character in NPCList)
                {
                    if ((character.GetDialogBox() != null && character.GetDialogBox().GetIsActive()) || Collisions.EntityCollisions(player.hitBox, character.hitBox))
                    {
                        character.ToggleDialog(screenWidth, screenHeight, graphicsDevice);
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
                if (Collisions.EntityCollisions(item.hitBox, player.hitBox) && item.CanBePickedUp)
                {
                    player.AddToInventory(item);
                }
                else
                {
                    items.Add(item);
                }
            }

            Vector2 playerScreenLocation = camera.WorldToScreen(player.location.X, player.location.Y);
            if (playerScreenLocation.X > screenWidth - 500)
            {
                camera.Move(new Vector2(playerScreenLocation.X - (screenWidth - 500), 0));
            }
            else if (playerScreenLocation.X < 500)
            {
                camera.Move(new Vector2(-1 * (500 - playerScreenLocation.X), 0));
            }

            if (playerScreenLocation.Y > screenHeight - 300)
            {
                camera.Move(new Vector2(0, playerScreenLocation.Y - (screenHeight - 300)));
            }
            else if (playerScreenLocation.Y < 300)
            {
                camera.Move(new Vector2(0, (-1 * (300 - playerScreenLocation.Y))));
            }

            if (camera.Position.X < 50)
            {
                camera.Move(new Vector2((-1 * camera.Position.X) + 50, 0));
            }
            else if (camera.Position.X > ((width - 1) * 50) - camera.BoundingRectangle.Width)
            {
                camera.Move(new Vector2((((width - 1) * 50) - camera.BoundingRectangle.Width) - camera.Position.X, 0));
            }

            if (camera.Position.Y < 50)
            {
                camera.Move(new Vector2(0, (-1 * camera.Position.Y) + 50));
            }
            else if (camera.Position.Y > ((height - 1) * 50) - camera.BoundingRectangle.Height)
            {
                camera.Move(new Vector2(0, (((height - 1) * 50) - camera.BoundingRectangle.Height) - camera.Position.Y));
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

        public void ReplaceTile(int x, int y, Tile newTile)
        {
            tiles[x][y] = newTile;
        }
    }
}
