using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer;
using System.Reflection.Metadata;

namespace Platformer
{
    class Item : Entity
    {
        public string itemName;
        private static Random random = new Random();
        protected int currentYOffset = -3;
        private int yOffsetCounter = 0;
        private int yOffsetDirection = 1;
        public bool CanBePickedUp { get; private set; }
        private int pickUpCounter;
        public int Count { get; set; }
        protected int id;

        private static Texture2D texture;
        private static Dictionary<string, int> itemDict = new Dictionary<string, int>();

        private static string[] itemNames =
        {
            "copperCoin", "silverCoin", "goldCoin", "healthPotion", "manaPotion", "plantFibers", "scytheItem", "shellFragment", "slimeItem", "slimeTail",
            "snailGoop", "swordItem",
        };
        public Item(string itemName, int count)
        {
            Count = count;
            verticalVelocity = -3;
            height = 30;
            width = 30;
            CanBePickedUp = false;
            pickUpCounter = 80;
            horizontalVelocity = random.Next(-3, 3);
            this.itemName = itemName;
            id = itemDict.GetValueOrDefault(itemName);
            if (id == 0)
            {
                this.itemName = "notFound";
            }
        }
        public Item(string itemName, int[] count, int[] probability) : this(itemName, calculateSumFromProbability(count, probability))
        {
        }

        private static int calculateSumFromProbability(int[] count, int[] probability)
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
            return sum;
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
        public int GetId()
        {
            return id;
        }
        public void ResetPickUpCounter()
        {
            pickUpCounter = 80;
            CanBePickedUp = false;
            isFalling = true;
        }
        public void Update(KeyboardState state, Location l)
        {
            if (pickUpCounter > 0)
            {
                pickUpCounter--;
                if (pickUpCounter == 0)
                {
                    CanBePickedUp = true;
                }
            }
            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                newLocation.X += horizontalVelocity;
                verticalVelocity++;
                Collisions.CollideWithTiles(l, this);
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

        public static void LoadTextures(ContentManager content)
        {
            texture = content.Load<Texture2D>("items");
        }

		public override void Draw(SpriteBatch spriteBatch)
		{
            Rectangle source = new Rectangle(30 * id, 0, 30, 30);
            Rectangle destination = new Rectangle((int)location.X, (int)location.Y, 30, 30);
            spriteBatch.Draw(texture, destination, source, Color.White);
		}

        public static void RegisterItems()
        {
            for (int i = 0; i < itemNames.Length; i++)
            {
                itemDict.Add(itemNames[i], i + 1);
            }
		}
	}
}