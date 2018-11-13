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
        private static Random random = new Random();
        protected int currentYOffset = -3;
        private int yOffsetCounter = 0;
        private int yOffsetDirection = 1;
        public bool canBePickedUp;
        private int pickUpCounter;
        private int probability;
        private int count { get; set; }
        protected int id;
        public Item(int count)
        {
            if (count != 0)
            {
                this.count = count;
            }
            verticalVelocity = -3;
            height = 30;
            width = 30;
            canBePickedUp = false;
            pickUpCounter = 80;
            horizontalVelocity = random.Next(-3, 3);
        }
        public Item(int[] count, int[] probability) : this(0)
        {
            int sum = 0;
            for (int i = 0; i < count.Length; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    if (random.Next(0, 100) < probability[i])
                    {
                        sum++;
                    }
                }
            }
            this.count = sum;
        }
        public void SetLocation(Vector2 location)
        {
            this.location = location;
            this.hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            newLocation = location;
        }
        public void SetCount(int count)
        {
            this.count = count;
        }
        public int GetCount()
        {
            return count;
        }
        public Vector2 GetLocation()
        {
            return location;
        }
        public void SetProbability(int probability)
        {
            this.probability = probability;
        }
        public int GetProbability()
        {
            return probability;
        }
        public int GetId()
        {
            return id;
        }
        public void ResetPickUpCounter()
        {
            pickUpCounter = 80;
            canBePickedUp = false;
            isFalling = true;
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
                newLocation.X += horizontalVelocity;
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
