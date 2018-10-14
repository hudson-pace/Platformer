using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Collisions
    {
        public static bool CollideWithTiles(Tile[][] tiles, Entity entity)
        {
            bool collided = false;

            int oldLeftGridX = (int)(entity.location.X / 50);
            int newLeftGridX = (int)(entity.newLocation.X / 50);
            int oldRightGridX = (int)((entity.location.X + entity.width - 1) / 50);
            int newRightGridX = (int)((entity.newLocation.X + entity.width - 1) / 50);
            int oldTopGridY = (int)(entity.location.Y / 50);
            int newTopGridY = (int)(entity.newLocation.Y / 50);
            int oldBottomGridY = (int)((entity.location.Y + entity.height - 1) / 50);
            int newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / 50);

            if (newLeftGridX < oldLeftGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (tiles[newLeftGridX][i].isBarrier || (entity.isEnemy && tiles[newLeftGridX][i].isEnemyBarrier))
                    {
                        entity.newLocation.X = oldLeftGridX * 50;
                        collided = true;
                        break;
                    }
                }
            }

            else if (newRightGridX > oldRightGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (tiles[newRightGridX][i].isBarrier || (entity.isEnemy && tiles[newRightGridX][i].isEnemyBarrier))
                    {
                        entity.newLocation.X = (newRightGridX * 50) - entity.width;
                        collided = true;
                        break;
                    }
                }
            }
            newLeftGridX = (int)(entity.newLocation.X / 50);
            newRightGridX = (int)((entity.newLocation.X + entity.width - 1) / 50);

            if (newTopGridY < oldTopGridY)
            {
                for (int i = newLeftGridX; i <= newRightGridX; i++)
                {
                    if (tiles[i][newTopGridY].isBarrier || (entity.isEnemy && tiles[i][newTopGridY].isEnemyBarrier))
                    {
                        entity.newLocation.Y = ((newTopGridY + 1) * 50);
                        entity.verticalVelocity = 0;
                        collided = true;
                        break;
                    }
                }
            }

            else if (newBottomGridY > oldBottomGridY)
            {
                for (int i = newLeftGridX; i <= newRightGridX; i++)
                {
                    if (tiles[i][newBottomGridY].isBarrier || (entity.isEnemy && tiles[i][newBottomGridY].isEnemyBarrier))
                    {
                        entity.newLocation.Y = (newBottomGridY * 50) - entity.height;
                        entity.isFalling = false;
                        entity.state = "squished";
                        entity.squishCounter = 8;
                        collided = true;
                        break;
                    }
                }
            }
            newTopGridY = (int)(entity.newLocation.Y / 50);
            newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / 50);

            if (!entity.isFalling && entity.canFall && ((newLeftGridX != oldLeftGridX) || (newRightGridX != oldRightGridX)))
            {
                if (!tiles[newLeftGridX][newBottomGridY + 1].isBarrier && !tiles[newRightGridX][newBottomGridY + 1].isBarrier)
                {
                    entity.isFalling = true;
                    entity.verticalVelocity = 0;
                }
            }

            return collided;
        }
    }
}
