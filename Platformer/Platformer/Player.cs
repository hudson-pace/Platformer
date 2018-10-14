using System;
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
        private Texture2D normalFacingRightTexture, normalFacingLeftTexture, squishedTexture, swordTexture, projectileTexture;
        private Location currentLocation;
        private bool previousFPressed = false;
        private bool swingingSword = false;
        private float angle = 0;
        private Vector2 swordLocation;
        private Rectangle swordSource;
        private string facing = "right";
        private string swingFacing = "right";
        private int projectileCooldown = 0;



        public Player(Vector2 location)
        {
            this.location = location;
            isEnemy = false;
            height = 80;
            width = 55;
        }

        public void SetTexture(Texture2D normalFacingRightTexture, Texture2D normalFacingLeftTexture, Texture2D squishedTexture, Texture2D swordTexture, Texture2D projectileTexture)
        {
            this.normalFacingRightTexture = normalFacingRightTexture;
            this.normalFacingLeftTexture = normalFacingLeftTexture;
            this.squishedTexture = squishedTexture;
            this.swordTexture = swordTexture;
            this.projectileTexture = projectileTexture;
            swordSource = new Rectangle(0, 0, swordTexture.Width, swordTexture.Height);
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

            if (state.IsKeyDown(Keys.A))
            {
                newLocation.X -= 4;
                facing = "left";
            }
            if (state.IsKeyDown(Keys.D))
            {
                newLocation.X += 4;
                facing = "right";
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
                swordLocation = new Vector2(location.X + (width / 2), location.Y + (height / 2));
                if (swingFacing == "right")
                {
                    angle += .11f;
                }
                else if (swingFacing == "left")
                {
                    angle -= .11f;
                }
                if (angle >= 2 || angle <= -2)
                {
                    angle = 0;
                    swingingSword = false;
                }
            }

            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }

            Collisions.CollideWithTiles(tiles, this);
            location.X = newLocation.X;
            location.Y = newLocation.Y;
            previousFPressed = state.IsKeyDown(Keys.F);

        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            if (swingingSword)
            {
                spriteBatch.Draw(swordTexture, new Vector2(swordLocation.X - offsetX, swordLocation.Y - offsetY), swordSource, Color.White, angle, new Vector2(0, swordTexture.Height), 1.0f, SpriteEffects.None, 1);
            }
            if (swingFacing == "right")
            {
                spriteBatch.Draw(normalFacingRightTexture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
            else if (swingFacing == "left")
            {
                spriteBatch.Draw(normalFacingLeftTexture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
        }
    }
}
