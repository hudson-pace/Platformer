using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Platformer
{
    class Character
    {
        public Vector2 location, newLocation;
        public int width, height, verticalVelocity;
        public int squishCounter = 0;
        public bool isFalling = true;
        public string state = "normal";

        public bool[] Collide(Tile[][] tiles)
        {
            int oldLeftGridX = (int)(location.X / 50);
            int newLeftGridX = (int)(newLocation.X / 50);
            int oldRightGridX = (int)((location.X + width - 1) / 50);
            int newRightGridX = (int)((newLocation.X + width - 1) / 50);
            int oldTopGridY = (int)(location.Y / 50);
            int newTopGridY = (int)(newLocation.Y / 50);
            int oldBottomGridY = (int)((location.Y + height - 1) / 50);
            int newBottomGridY = (int)((newLocation.Y + height - 1) / 50);

            bool[] collisions = new bool[2];
            collisions[0] = false;
            collisions[1] = false;

            if (newLeftGridX < oldLeftGridX)
            {
                if (tiles[newLeftGridX][newTopGridY].isBarrier || tiles[newLeftGridX][newBottomGridY].isBarrier)
                {
                    newLocation.X = oldLeftGridX * 50;
                    collisions[0] = true;
                }
            }

            else if (newRightGridX > oldRightGridX)
            {
                if (tiles[newRightGridX][newTopGridY].isBarrier || tiles[newRightGridX][newBottomGridY].isBarrier)
                {
                    newLocation.X = newLeftGridX * 50;
                    collisions[0] = true;
                }
            }

            newLeftGridX = (int)(newLocation.X / 50);
            newRightGridX = (int)((newLocation.X + width - 1) / 50);

            if (newTopGridY < oldTopGridY)
            {
                if (tiles[newLeftGridX][newTopGridY].isBarrier || tiles[newRightGridX][newTopGridY].isBarrier)
                {
                    newLocation.Y = oldTopGridY * 50;
                    verticalVelocity = 0;
                    collisions[1] = true;
                }
            }

            else if (newBottomGridY > oldBottomGridY)
            {
                if (tiles[newLeftGridX][newBottomGridY].isBarrier || tiles[newRightGridX][newBottomGridY].isBarrier)
                {
                    newLocation.Y = (int)((newLocation.Y) / 50) * 50;
                    isFalling = false;
                    state = "squished";
                    squishCounter = 8;
                    collisions[1] = true;
                }
            }

            newTopGridY = (int)(newLocation.Y / 50);
            newBottomGridY = (int)((newLocation.Y + height - 1) / 50);

            if (isFalling == false && ((newLeftGridX != oldLeftGridX) || (newRightGridX != oldRightGridX)))
            {
                if (!tiles[newLeftGridX][newBottomGridY + 1].isBarrier && !tiles[newRightGridX][newBottomGridY + 1].isBarrier)
                {
                    isFalling = true;
                    verticalVelocity = 0;
                }
            }

            return collisions;

        }
    }
}
