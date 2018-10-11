using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Insect : Character
    {
        private string facing = "right";
        private Texture2D rightFacing, leftFacing;

        public Insect(Vector2 location)
        {
            this.location = location;
            width = 60;
            height = 100;
        }

        public void SetTexture(Texture2D rightFacing, Texture2D leftFacing)
        {
            this.rightFacing = rightFacing;
            this.leftFacing = leftFacing;
        }

        public void Update(Tile[][] tiles)
        {
            newLocation = location;

            if (facing == "right")
            {
                newLocation.X += 2;
            }
            else if (facing == "left")
            {
                newLocation.X -= 2;
            }
            bool[] collisions = Collide(tiles);
            if (collisions[0])
            {
                if (facing == "right")
                {
                    facing = "left";
                }
                else if (facing == "left")
                {
                    facing = "right";
                }
            }

            location = newLocation;

        }
        public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            if (facing == "right")
            {
                spriteBatch.Draw(rightFacing, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
            else if (facing == "left")
            {
                spriteBatch.Draw(leftFacing, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
            }
        }
    }
}
