using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player player = new Player(new Vector2(100, 100));
        //private Insect insect = new Insect(new Vector2(100, 600));
        private Location testArea;
        private int screenGridWidth, screenGridHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            screenGridWidth = (int)(graphics.PreferredBackBufferWidth / 50) + 1;
            screenGridHeight = (int)(graphics.PreferredBackBufferHeight / 50) + 1;

            testArea = new TestArea(player, screenGridWidth, screenGridHeight, Content);





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

            player.SetTexture(Content.Load<Texture2D>("player-facing-right"), Content.Load<Texture2D>("player-facing-left"),
                Content.Load<Texture2D>("player-facing-right"), Content.Load<Texture2D>("sword"), Content.Load<Texture2D>("blue-ball"));

            testArea.SetTextures(Content.Load<Texture2D>("brick-wall"), Content.Load<Texture2D>("insect-guy-facing-right"), 
                Content.Load<Texture2D>("insect-guy-facing-left"), Content.Load<Texture2D>("door"), Content.Load<Texture2D>("slime-facing-left"), 
                Content.Load<Texture2D>("slime-facing-right"));

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
            testArea.Update(state);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //insect.Draw(spriteBatch);
            //player.Draw(spriteBatch);
            testArea.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}