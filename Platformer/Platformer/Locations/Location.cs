﻿using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System.Diagnostics;

namespace Platformer
{
    abstract class Location
    {
        protected List<Enemy> enemies = new List<Enemy>();
        protected List<Spawner> spawners = new List<Spawner>();
        protected List<EntitySpawner> entitySpawners = new List<EntitySpawner>();
        protected List<NPC> NPCList = new List<NPC>();
        protected List<Item> items = new List<Item>();
        protected List<Portal> portals = new List<Portal>();
        protected Vector2 spawnPoint;
        public Player player;
        public int height, width;
        public int screenGridWidth, screenGridHeight, screenWidth, screenHeight;
        protected GraphicsDevice graphicsDevice;
        private bool previousQPressed = false;
        protected TiledMapTileLayer collisionTileLayer;
        protected TiledMapTileLayer mainTileLayer;
        private TiledMapObjectLayer portalLayer;
        protected TiledMap tiledMap;
		protected TiledMapRenderer tiledMapRenderer;
        private string contentPath;


		public Location(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice, string contentPath)
        {
            this.player = player;
            this.screenGridWidth = screenGridWidth;
            this.screenGridHeight = screenGridHeight;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.graphicsDevice = graphicsDevice;
            this.contentPath = contentPath;
		}
        public abstract void AddPortals();
        
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
        public virtual void Draw(SpriteBatch spriteBatch, OrthographicCamera camera)
        {
            portals.ForEach(portal => portal.Draw(spriteBatch));
            NPCList.ForEach(npc => npc.Draw(spriteBatch));
            enemies.ForEach(enemy => enemy.Draw(spriteBatch));
            items.ForEach(item => item.Draw(spriteBatch));
			tiledMapRenderer.Draw(camera.GetViewMatrix());
            entitySpawners.ForEach(spawner => spawner.Draw(spriteBatch));
		}
		public void LoadTextures(ContentManager content)
		{
			tiledMap = content.Load<TiledMap>(contentPath);
			tiledMapRenderer = new TiledMapRenderer(graphicsDevice, tiledMap);
			collisionTileLayer = tiledMap.GetLayer<TiledMapTileLayer>("collision");
            mainTileLayer = tiledMap.GetLayer<TiledMapTileLayer>("tiles");
            portalLayer = tiledMap.GetLayer<TiledMapObjectLayer>("portals");
            foreach (TiledMapObject obj in portalLayer.Objects)
            {
				Portal portal = new Portal(obj.Position, obj.Size, obj.Identifier, int.Parse(obj.Properties.GetValueOrDefault("destination")), this);
                portals.Add(portal);
            }
        }

		public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }
        public bool IsObstacleAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) // Out of bounds
            {
                return true;
            }
			TiledMapTile t = collisionTileLayer.GetTile((ushort)x, (ushort)y);
            return t.GlobalIdentifier == 22 || t.GlobalIdentifier == 1282;
		}
        public bool IsEnemyObstacleAt(int x, int y)
        {
			if (x < 0 || y < 0 || x >= width || y >= height) // Out of bounds
			{
				return true;
			}
			TiledMapTile t = collisionTileLayer.GetTile((ushort)x, (ushort)y);
            return t.GlobalIdentifier == 21 || t.GlobalIdentifier == 22 || t.GlobalIdentifier == 1281 || t.GlobalIdentifier == 1282;
        }
        public virtual void Update(KeyboardState state, MouseState mouseState, OrthographicCamera camera, GameTime gameTime)
        {
			portals.ForEach(portal => portal.Update());
			player.Update(state, this, mouseState);
            for (int i = enemies.Count - 1; i >= 0; i--) // updating may cause enemy to be removed, so iterate backwards.
            {
                enemies[i].Update(player, this);
            }
            spawners.ForEach(spawner => spawner.Update(enemies));
            items.ForEach(item => item.Update(state, this));
            NPCList.ForEach(npc => npc.Update(state, this, mouseState));
			entitySpawners.ForEach(spawner => spawner.Update(player));

            

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

            if (camera.Position.X < 0)
            {
                camera.Move(new Vector2(-1 * camera.Position.X, 0));
            }
            else if (camera.Position.X > (width * Globals.tileSize) - camera.BoundingRectangle.Width)
            {
                camera.Move(new Vector2(((width * Globals.tileSize) - camera.BoundingRectangle.Width) - camera.Position.X, 0));
            }

            if (camera.Position.Y < 0)
            {
                camera.Move(new Vector2(0, -1 * camera.Position.Y));
            }
            else if (camera.Position.Y > (height * Globals.tileSize) - camera.BoundingRectangle.Height)
            {
                camera.Move(new Vector2(0, ((height * Globals.tileSize) - camera.BoundingRectangle.Height) - camera.Position.Y));
            }
        }
    }
}
