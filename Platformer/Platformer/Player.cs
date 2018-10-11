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
        private Texture2D normalTexture, squishedTexture, swordTexture;
        private Location currentLocation;
        private bool previousFPressed = false;
        private bool swingingSword = false;
        private float angle = 0;
        private Vector2 swordLocation;
        private Rectangle swordSource;
        private string facing = "right";
        private string swingFacing = "right";



        public Player(Vector2 location)
        {
            this.location = location;
            height = 50;
            width = 50;
        }

        public void SetTexture(Texture2D normalTexture, Texture2D squishedTexture, Texture2D swordTexture)
        {
            this.normalTexture = normalTexture;
            this.squishedTexture = squishedTexture;
            this.swordTexture = swordTexture;
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

            if (previousFPressed == false && state.IsKeyDown(Keys.F) && swingingSword == false)
            {
                Console.WriteLine("swingin");
                swingingSword = true;
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

            Collide(tiles);
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
            if (state == "normal")
            {
                spriteBatch.Draw(normalTexture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
            else if (state == "squished")
            {
                spriteBatch.Draw(squishedTexture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }

        }
    }
}
