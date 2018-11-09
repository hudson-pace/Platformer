using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class PlayerInfoBar
    {
        private int screenWidth, screenHeight;
        private static Texture2D redTexture, blackTexture, blueTexture, greenTexture;
        private static SpriteFont font;
        private Rectangle healthBar, manaBar, xpBar, healthBarFill, manaBarFill, xpBarFill;
        private Player player;

        public PlayerInfoBar(Player player, int screenWidth, int screenHeight)
        {
            this.player = player;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            healthBar = new Rectangle((int)(screenWidth * .05), (int)(screenHeight * .95), (int)(screenWidth * .2), (int)(screenHeight * .025));
            manaBar = new Rectangle((int)(screenWidth * .3), (int)(screenHeight * .95), (int)(screenWidth * .2), (int)(screenHeight * .025));
            xpBar = new Rectangle((int)(screenWidth * .55), (int)(screenHeight * .95), (int)(screenWidth * .2), (int)(screenHeight * .025));
            healthBarFill = healthBar;
            manaBarFill = manaBar;
            xpBarFill = xpBar;
        }
        public void Update()
        {
            healthBarFill.Width = (int)(healthBar.Width * ((float)player.GetHealth() / player.GetMaxHealth()));
            manaBarFill.Width = (int)(manaBar.Width * ((float)player.GetMana() / player.GetMaxMana()));
            xpBarFill.Width = (int)(xpBar.Width * ((float)player.GetXp() / player.GetXpToLevel()));
        }
        public static void CreateTextures(GraphicsDevice graphicsDevice)
        {
            redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new[] { Color.Red });
            blackTexture = new Texture2D(graphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });
            blueTexture = new Texture2D(graphicsDevice, 1, 1);
            blueTexture.SetData(new[] { Color.Blue });
            greenTexture = new Texture2D(graphicsDevice, 1, 1);
            greenTexture.SetData(new[] { Color.Green });
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blackTexture, healthBar, Color.White);
            spriteBatch.Draw(redTexture, healthBarFill, Color.White);
            spriteBatch.DrawString(font, player.GetHealth() + " / " + player.GetMaxHealth(), new Vector2(healthBar.X, healthBar.Y), Color.White);

            spriteBatch.Draw(blackTexture, manaBar, Color.White);
            spriteBatch.Draw(blueTexture, manaBarFill, Color.White);
            spriteBatch.DrawString(font, player.GetMana() + " / " + player.GetMaxMana(), new Vector2(manaBar.X, manaBar.Y), Color.White);

            spriteBatch.Draw(blackTexture, xpBar, Color.White);
            spriteBatch.Draw(greenTexture, xpBarFill, Color.White);
            spriteBatch.DrawString(font, player.GetXp() + " / " + player.GetXpToLevel(), new Vector2(xpBar.X, xpBar.Y), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            font = content.Load<SpriteFont>("NPCText");
        }
    }
}
