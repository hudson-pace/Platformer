using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    abstract class Item : Entity
    {
        public string itemName;
        protected int currentYOffset = -3;
        private int yOffsetCounter = 0;
        private int yOffsetDirection = 1;
        public bool canBePickedUp;
        private int pickUpCounter;
        public Item()
        {
            verticalVelocity = -3;
            height = 30;
            width = 30;
            canBePickedUp = false;
            pickUpCounter = 80;
        }
        public void SetLocation(Vector2 location)
        {
            this.location = location;
            this.hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            newLocation = location;
        }
        public Vector2 GetLocation()
        {
            return location;
        }
        public void Update(KeyboardState state, Tile[][] tiles)
        {
            if (pickUpCounter > 0)
            {
                pickUpCounter--;
                if (pickUpCounter == 0)
                {
                    canBePickedUp = true;
                }
            }
            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
                Collisions.CollideWithTiles(tiles, this);
                location = newLocation;
                hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            }
            else
            {
                yOffsetCounter++;
                if (yOffsetCounter > 5)
                {
                    yOffsetCounter = 0;
                    currentYOffset += yOffsetDirection;
                    if (currentYOffset > 1 || currentYOffset < -5)
                    {
                        yOffsetDirection *= -1;
                    }
                }
            }
        }

        

        
    }
}
