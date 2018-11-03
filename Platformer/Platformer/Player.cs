using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class Player : Entity
    {
        private static Texture2D swordTexture, projectileTexture, megamanTexture;
        private Location currentLocation;
        private bool previousFPressed = false, previousAPressed = false, previousDPressed = false, previousIPressed = false;
        public bool swordIsActive = false;
        private bool swingingSword = false;
        public Rectangle swordHitBox;
        private string facing = "right";
        public string swingFacing = "right";
        private int projectileCooldown = 0, textureChangeCounter = 0, currentTextureState = 1, swordTextureChangeCounter = 5, currentSwordTextureState = 0, swordOffset;
        private Inventory inventory = new Inventory();
        private int health;
        private int invulnerableTimer = 0;
        public bool invulnerable = false;



        public Player(Vector2 location, GraphicsDevice graphicsDevice)
        {
            swordOffset = 30;
            this.location = location;
            isEnemy = false;
            height = 100;
            width = 100;
            health = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
            inventory.CreateTextures(graphicsDevice);
        }
        public static void LoadTextures(ContentManager content)
        {
            swordTexture = content.Load<Texture2D>("sword");
            projectileTexture = content.Load<Texture2D>("blue-ball");
            megamanTexture = content.Load<Texture2D>("megaman");
            Inventory.LoadTextures(content);
        }

        public void AddToInventory(Item item, int count)
        {
            inventory.AddToInventory(item, count);
        }

        public void SetLocation(Location currentLocation)
        {
            this.currentLocation = currentLocation;
        }


        public void Update(KeyboardState state, Tile[][] tiles, MouseState mouseState)
        {
            newLocation = location;
            if (invulnerableTimer > 0)
            {
                invulnerableTimer--;
                if (this.state == "hurt" && invulnerableTimer < 80)
                {
                    this.state = "invulnerable";
                }
                if (invulnerableTimer == 0)
                {
                    invulnerable = false;
                    this.state = "normal";
                }
            }

            if (!previousIPressed && state.IsKeyDown(Keys.I))
            {
                inventory.Toggle();
            }
            previousIPressed = state.IsKeyDown(Keys.I);
            if (!previousAPressed && !previousDPressed)
            {
                textureChangeCounter = 5;
                currentTextureState = 1;
            }
            if (!(state.IsKeyDown(Keys.A) && state.IsKeyDown(Keys.D)))
            {
                if (state.IsKeyDown(Keys.A))
                {
                    if (previousAPressed && !isFalling)
                    {
                        textureChangeCounter--;
                    }
                    previousAPressed = true;
                    previousDPressed = false;
                    newLocation.X -= 4;
                    facing = "left";
                }
                if (state.IsKeyDown(Keys.D))
                {
                    if (previousDPressed && !isFalling)
                    {
                        textureChangeCounter--;
                    }
                    previousAPressed = false;
                    previousDPressed = true;
                    newLocation.X += 4;
                    facing = "right";
                }
                if (textureChangeCounter <= 0)
                {
                    currentTextureState++;
                    if (currentTextureState > 2)
                    {
                        currentTextureState = 0;
                    }
                    textureChangeCounter = 5;
                }
                if (!state.IsKeyDown(Keys.A) && !state.IsKeyDown(Keys.D))
                {
                    textureChangeCounter = 5;
                    currentTextureState = 1;
                }
            }
            



            if (state.IsKeyDown(Keys.Space) && !isFalling)
            {
                isFalling = true;
                verticalVelocity = -20;
            }

            if (projectileCooldown > 0)
            {
                projectileCooldown--;
            }
            if (state.IsKeyDown(Keys.J) && projectileCooldown == 0)
            {
                if (swingFacing == "right")
                {
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, 10, currentLocation));
                }
                else if (swingFacing == "left")
                {
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, -10, currentLocation));
                }
                projectileCooldown = 60;
            }
            if (previousFPressed == false && state.IsKeyDown(Keys.F) && swingingSword == false)
            {
                swingingSword = true;
                swingFacing = facing;
            }
            if (!swingingSword)
            {
                swingFacing = facing;
            }
            if (swingingSword)
            {
                swordTextureChangeCounter--;
                if (swordTextureChangeCounter < 0)
                {
                    swordTextureChangeCounter = 5;
                    currentSwordTextureState++;
                    if (currentSwordTextureState == 1)
                    {
                        swordIsActive = true;
                    }
                    if (currentSwordTextureState >= 2)
                    {
                        swingingSword = false;
                        currentSwordTextureState = 0;
                    }
                }
            }

            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }

            Collisions.CollideWithTiles(tiles, this);
            location = newLocation;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            if (facing == "left")
            {
                swordHitBox = new Rectangle((int)location.X - swordOffset, (int)location.Y, swordOffset, height);
            }
            else if (facing == "right")
            {
                swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
            }
            previousFPressed = state.IsKeyDown(Keys.F);

            if (inventory.GetIsActive())
            {
                inventory.Update(mouseState);
            }
        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Rectangle sourceRectangle;
            if (swingingSword)
            {
                int textureRow = 0;
                int textureOffset = 0;
                if (swingFacing == "left")
                {
                    textureRow = 1;
                    textureOffset = swordOffset;
                }
                sourceRectangle = new Rectangle(currentSwordTextureState * 130, textureRow * 100, 130, 100);
                spriteBatch.Draw(swordTexture, new Vector2(location.X - offsetX - textureOffset, location.Y - offsetY), sourceRectangle, Color.White);
            }

            int sourceY = 0;
            if (state == "hurt")
            {
                sourceY += 200;
            }
            else if (state == "invulnerable")
            {
                sourceY += 400;
            }
            if (swingFacing == "right")
            {
                sourceY += 100;
            }

            sourceRectangle = new Rectangle(currentTextureState * 100, sourceY, 100, 100);

            spriteBatch.Draw(megamanTexture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);
            
            if (inventory.GetIsActive())
            {
                inventory.Draw(spriteBatch);
            }
        }

        public void GetHit(String direction, int damage)
        {
            invulnerableTimer = 100;
            invulnerable = true;
            health -= damage;
            if (health <= 0)
            {
            }

            state = "hurt";
            hurtCounter = 20;

            if (direction == "left")
            {
                horizontalVelocity -= 5;
            }
            else
            {
                horizontalVelocity += 5;
            }
            if (!isFalling)
            {
                verticalVelocity = -7;
            }
            else
            {
                verticalVelocity -= 3;
            }
            isFalling = true;
        }
    }
}
