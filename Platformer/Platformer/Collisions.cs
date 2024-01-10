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
        public static bool CollideWithTiles(Location location, Entity entity)
        {
            int oldLeftGridX = (int)(entity.location.X / 50);
            int newLeftGridX = (int)(entity.newLocation.X / 50);
            int oldRightGridX = (int)((entity.location.X + entity.width - 1) / 50);
            int newRightGridX = (int)((entity.newLocation.X + entity.width - 1) / 50);
            int oldTopGridY = (int)(entity.location.Y / 50);
            int newTopGridY = (int)(entity.newLocation.Y / 50);
            int oldBottomGridY = (int)((entity.location.Y + entity.height - 1) / 50);
            int newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / 50);


            bool collided = false;          
            
            if (newLeftGridX < oldLeftGridX)
            {
                for (int i = newTopGridY; i <= newBottomGridY; i++)
                {
                    if (location.IsObstacleAt(newLeftGridX, i) || (entity.isEnemy && location.IsEnemyObstacleAt(newLeftGridX, i)))
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
                    if (location.IsObstacleAt(newRightGridX, i) || (entity.isEnemy && location.IsEnemyObstacleAt(newRightGridX, i)))
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
                    if (location.IsObstacleAt(i, newTopGridY) || (entity.isEnemy && location.IsEnemyObstacleAt(i, newTopGridY)))
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
                    if (location.IsObstacleAt(i, newBottomGridY) || (entity.isEnemy && location.IsEnemyObstacleAt(i, newBottomGridY)))
					{
                        entity.newLocation.Y = (newBottomGridY * 50) - entity.height;
                        entity.isFalling = false;
                        entity.verticalVelocity = 0;
                        entity.horizontalVelocity = 0;
                        collided = true;
                        break;
                    }
                }
            }
            newTopGridY = (int)(entity.newLocation.Y / 50);
            newBottomGridY = (int)((entity.newLocation.Y + entity.height - 1) / 50);

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
