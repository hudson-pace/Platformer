using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Collisions
    {
        public static bool CollideWithTiles(Location location, Entity entity)
        {
            int oldLeftGridX = (int)(entity.location.X / Globals.tileSize);
            int newLeftGridX = (int)(entity.newLocation.X / Globals.tileSize);
            // Round down. Won't work if negative, so check.
            if (newLeftGridX == 0 && entity.newLocation.X < 0)
            {
                newLeftGridX -= 1;
            }
            int oldRightGridX = (int)((entity.location.X + entity.width - 1) / Globals.tileSize);
            int newRightGridX = (int)((entity.newLocation.X + entity.width - 1) / Globals.tileSize);
            int oldTopGridY = (int)(entity.location.Y / Globals.tileSize);
            int newTopGridY = (int)(entity.newLocation.Y / Globals.tileSize);
            // Round down again.
            if (newTopGridY == 0 && entity.newLocation.Y < 0)
            {
                newTopGridY -= 1;
            }
            int oldBottomGridY = (int)((entity.location.Y + entity.height - 1) / Globals.tileSize);
            int newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / Globals.tileSize);


            bool collided = false;

            if (newLeftGridX < oldLeftGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (location.IsObstacleAt(newLeftGridX, i) || (entity.isEnemy && location.IsEnemyObstacleAt(newLeftGridX, i)))
					{
                        entity.newLocation.X = oldLeftGridX * Globals.tileSize;
                        collided = true;
                        break;
                    }
                }
            }

            else if (newRightGridX > oldRightGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (location.IsObstacleAt(newRightGridX, i) || (entity.isEnemy && location.IsEnemyObstacleAt(newRightGridX, i)))
                    {
                        entity.newLocation.X = (newRightGridX * Globals.tileSize) - entity.width;
                        collided = true;
                        break;
                    }
                }
            }
            newLeftGridX = (int)(entity.newLocation.X / Globals.tileSize);
            newRightGridX = (int)((entity.newLocation.X + entity.width - 1) / Globals.tileSize);

            if (newTopGridY < oldTopGridY)
            {
                for (int i = newLeftGridX; i <= newRightGridX; i++)
                {
                    if (location.IsObstacleAt(i, newTopGridY) || (entity.isEnemy && location.IsEnemyObstacleAt(i, newTopGridY)))
					{
                        entity.newLocation.Y = ((newTopGridY + 1) * Globals.tileSize);
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
                    if (location.IsObstacleAt(i, newBottomGridY) || (entity.isEnemy && location.IsEnemyObstacleAt(i, newBottomGridY)))
					{
                        entity.newLocation.Y = (newBottomGridY * Globals.tileSize) - entity.height;
                        entity.isFalling = false;
                        entity.verticalVelocity = 0;
                        entity.horizontalVelocity = 0;
                        collided = true;
                        break;
                    }
                }
            }
            newTopGridY = (int)(entity.newLocation.Y / Globals.tileSize);
            newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / Globals.tileSize);

            if (!entity.isFalling && entity.canFall && ((newLeftGridX != oldLeftGridX) || (newRightGridX != oldRightGridX)))
            {
                if (!location.IsObstacleAt(newLeftGridX, newBottomGridY + 1) && !location.IsObstacleAt(newRightGridX, newBottomGridY + 1))
                {
                    entity.isFalling = true;
                    entity.verticalVelocity = 0;
                }
            }

            return collided;
        }

        public static bool EntityCollisions(Rectangle entity1HitBox, Rectangle entity2HitBox)
        {
            if (entity1HitBox.Intersects(entity2HitBox))
            {
                return true;
            }
            return false;
        }
    }
}
