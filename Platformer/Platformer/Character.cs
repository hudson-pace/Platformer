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
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (tiles[newLeftGridX][i].isBarrier)
                    {
                        newLocation.X = oldLeftGridX * 50;
                        collisions[0] = true;
                        break;
                    }
                }
            }

            else if (newRightGridX > oldRightGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++) {
                    if (tiles[newRightGridX][i].isBarrier)
                    {
                        newLocation.X = (newRightGridX * 50) - width;
                        collisions[0] = true;
                        break;
                    }
                }
            }

            newLeftGridX = (int)(newLocation.X / 50);
            newRightGridX = (int)((newLocation.X + width - 1) / 50);

            if (newTopGridY < oldTopGridY)
            {
                for (int i = newLeftGridX; i <= newRightGridX; i++)
                {
                    if (tiles[i][newTopGridY].isBarrier)
                    {
                        newLocation.Y = ((newTopGridY + 1) * 50);
                        verticalVelocity = 0;
                        collisions[1] = true;
                        break;
                    }
                }
            }

            else if (newBottomGridY > oldBottomGridY)
            {
                for (int i = newLeftGridX; i <= newRightGridX; i++)
                {
                    if (tiles[i][newBottomGridY].isBarrier)
                    {
                        newLocation.Y = (newBottomGridY * 50) - height;
                        isFalling = false;
                        state = "squished";
                        squishCounter = 8;
                        collisions[1] = true;
                        break;
                    }
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
