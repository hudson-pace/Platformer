using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Location slimeCity, testArea;

        private Player player;
        private int screenGridWidth, screenGridHeight, screenWidth, screenHeight, time;
        private SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            screenWidth = (int)(graphics.PreferredBackBufferWidth);
            screenHeight = (int)(graphics.PreferredBackBufferHeight);
            screenGridWidth = (int)(graphics.PreferredBackBufferWidth / 50) + 1;
            screenGridHeight = (int)(graphics.PreferredBackBufferHeight / 50) + 1;

            player = new Player(new Vector2(100, 100));
            testArea = new Locations.TestArea(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, GraphicsDevice);
            slimeCity = new Locations.SlimeCity(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, GraphicsDevice);
            testArea.AddPortals();
            slimeCity.AddPortals();
            player.Travel(testArea, new Vector2(100, 100));

            IsMouseVisible = true;





            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            /*player.AddToInventory(new Items.ShellFragment(2));
            player.AddToInventory(new Items.SlimeItem(2));
            player.AddToInventory(new Items.SlimeTail(2));
            player.AddToInventory(new Items.SnailGoop(2));*/

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Player.LoadTextures(Content, GraphicsDevice);

            player.GetCurrentLocation().LoadTextures(Content);
            slimeCity.LoadTextures(Content);

            font = Content.Load<SpriteFont>("NPCText");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //insect.Update(testArea.tiles);
            //player.Update(state, testArea.tiles);
            player.GetCurrentLocation().Update(state, Mouse.GetState());


            time++;
            if (time > 2000)
            {
                time = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (time < 1600)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                GraphicsDevice.Clear(Color.DarkBlue);
            }

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            player.GetCurrentLocation().Draw(spriteBatch);


            spriteBatch.DrawString(font, "time: " + time, new Vector2(10, 10), Color.White);
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}