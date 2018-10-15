﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Player : Character
    {
        private Texture2D normalFacingRightTexture, normalFacingLeftTexture, squishedTexture, swordTexture, projectileTexture, runningLeft, runningRight;
        private Location currentLocation;
        private bool previousFPressed = false, previousAPressed = false, previousDPressed = false;
        public bool swordIsActive = false;
        private bool swingingSword = false;
        public Rectangle swordHitBox;
        private string facing = "right";
        public string swingFacing = "right";
        private int projectileCooldown = 0, textureChangeCounter = 0, currentTextureState = 1, swordTextureChangeCounter = 5, currentSwordTextureState = 0, swordOffset;



        public Player(Vector2 location)
        {
            this.location = location;
            isEnemy = false;
            height = 100;
            width = 100;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
        }

        public void SetTexture(Texture2D normalFacingRightTexture, Texture2D normalFacingLeftTexture, Texture2D squishedTexture, Texture2D swordTexture, 
            Texture2D projectileTexture, Texture2D runningLeft, Texture2D runningRight)
        {
            this.normalFacingRightTexture = normalFacingRightTexture;
            this.normalFacingLeftTexture = normalFacingLeftTexture;
            this.squishedTexture = squishedTexture;
            this.swordTexture = swordTexture;
            this.projectileTexture = projectileTexture;
            this.runningLeft = runningLeft;
            this.runningRight = runningRight;
            this.swordOffset = 30;
        }

        public void SetLocation(Location currentLocation)
        {
            this.currentLocation = currentLocation;
        }


        public void Update(KeyboardState state, Tile[][] tiles)
        {
            newLocation = location;

            if (squishCounter > 0)
            {
                squishCounter--;
                if (squishCounter == 0)
                {
                    this.state = "normal";
                }
            }

            if (!previousAPressed && !previousDPressed)
            {
                textureChangeCounter = 5;
                currentTextureState = 1;
            }
            if (!(state.IsKeyDown(Keys.A) && state.IsKeyDown(Keys.D)))
            {
                if (state.IsKeyDown(Keys.A))
                {
                    if (previousAPressed)
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
                    if (previousDPressed)
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
            



            if (state.IsKeyDown(Keys.Space) && !isFalling && this.state == "normal")
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
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, 10));
                }
                else if (swingFacing == "left")
                {
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, -10));
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

        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Rectangle sourceRectangle;
            Texture2D texture;

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
                texture = swordTexture;
                spriteBatch.Draw(texture, new Vector2(location.X - offsetX - textureOffset, location.Y - offsetY), sourceRectangle, Color.White);
            }

            sourceRectangle = new Rectangle(currentTextureState * 100, 0, 100, 100);
            texture = runningLeft;
            if (swingFacing == "right")
            {
                texture = runningRight;
            }

            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);
        }
    }
}
