using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class Player : Entity
    {
        private static Texture2D swordTexture, projectileTexture, megamanTexture;
        private Location currentLocation;
        private KeyboardState previousKeyboardState;
        public bool swordIsActive = false, scytheIsActive = false;
        private bool swinging = false;
        public Rectangle swordHitBox;
        private string facing = "right";
        public string swingFacing = "right";
        private int projectileCooldown = 0, textureChangeCounter = 0, currentTextureState = 1, swordTextureChangeCounter = 5, currentSwordTextureState = 0, swordOffset;
        private PlayerInventory inventory;
        private EquipmentMenu equipmentMenu;
        private int health, maxHealth, mana, maxMana, xp, xpToLevel;
        private int invulnerableTimer = 0;
        public bool invulnerable = false;
        private PlayerInfoBar playerInfoBar;
        private int screenWidth, screenHeight, manaRegenCooldown;

        public Player(Vector2 location, int screenWidth, int screenHeight)
        {
            this.location = location;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            inventory = new PlayerInventory(this, screenWidth, screenHeight);
            equipmentMenu = new EquipmentMenu(this);
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
            playerInfoBar = new PlayerInfoBar(this, screenWidth, screenHeight);

            swordOffset = 30;
            height = 100;
            width = 100;
            maxHealth = 100;
            health = maxHealth;
            maxMana = 50;
            mana = maxMana;
            xpToLevel = 100;
            xp = 0;
        }

        public static void LoadTextures(ContentManager content, GraphicsDevice graphicsDevice)
        {
            swordTexture = content.Load<Texture2D>("sword");
            projectileTexture = content.Load<Texture2D>("blue-ball");
            megamanTexture = content.Load<Texture2D>("megaman");
            Inventory.LoadTextures(content);
            Inventory.CreateTextures(graphicsDevice);
            EquipmentMenu.CreateTextures(graphicsDevice);
            PlayerInfoBar.CreateTextures(graphicsDevice);
            PlayerInfoBar.LoadTextures(content);
            InfoBox.CreateTextures(graphicsDevice);
        }

        public void AddToInventory(Item item)
        {
            inventory.AddToInventory(item);
        }

        public void Travel(Location destination, Vector2 position)
        {
            if (this.currentLocation != destination)
            {
                // TODO: unload content from previous location, and load from new one.
                this.currentLocation = destination;
            }
            this.location = position;
            isFalling = true;
        }

        public EquipmentMenu GetEquipmentMenu()
        {
            return equipmentMenu;
        }
        public int GetMaxHealth()
        {
            return maxHealth;
        }
        public int GetHealth()
        {
            return health;
        }
        public int GetMaxMana()
        {
            return maxMana;
        }
        public int GetMana()
        {
            return mana;
        }
        public int GetXp()
        {
            return xp;
        }
        public int GetXpToLevel()
        {
            return xpToLevel;
        }
        public Location GetCurrentLocation()
        {
            return currentLocation;
        }
        public void Update(KeyboardState keyboardState, Tile[][] tiles, MouseState mouseState)
        {
            newLocation = location;

            if (invulnerableTimer > 0)
            {
                invulnerableTimer--;
                if (this.state == "hurt" && invulnerableTimer < 80)
                {
                    this.state = "invulnerable";
                }
                if (invulnerableTimer == 0)
                {
                    invulnerable = false;
                    this.state = "normal";
                }
            }

            if (!previousKeyboardState.IsKeyDown(Keys.OemComma) && keyboardState.IsKeyDown(Keys.OemComma))
            {
                if (inventory.RemoveFromInventory(new InventoryItem(new Items.HealthPotion(1), new Vector2(0, 0))))
                {
                    health += 50;
                    if (health > maxHealth)
                    {
                        health = maxHealth;
                    }
                }
            }
            if (!previousKeyboardState.IsKeyDown(Keys.OemPeriod) && keyboardState.IsKeyDown(Keys.OemPeriod))
            {
                if (inventory.RemoveFromInventory(new InventoryItem(new Items.ManaPotion(1), new Vector2(0, 0))))
                {
                    mana = maxMana;
                }
            }
            if (!previousKeyboardState.IsKeyDown(Keys.I) && keyboardState.IsKeyDown(Keys.I))
            {
                Game1.ToggleMenu(inventory);
            }
            if (!previousKeyboardState.IsKeyDown(Keys.O) && keyboardState.IsKeyDown(Keys.O))
            {
                Game1.ToggleMenu(equipmentMenu);
            }
            if (!(keyboardState.IsKeyDown(Keys.A) ^ keyboardState.IsKeyDown(Keys.D))) // if both or neither are pressed
            {
                textureChangeCounter = 5;
                currentTextureState = 1;
            }
            else
            {
                if (textureChangeCounter <= 0)
                {
                    currentTextureState++;
                    if (currentTextureState > 2)
                    {
                        currentTextureState = 0;
                    }
                    textureChangeCounter = 5;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (previousKeyboardState.IsKeyDown(Keys.A) && !isFalling)
                    {
                        textureChangeCounter--;
                    }
                    newLocation.X -= 4;
                    facing = "left";
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (previousKeyboardState.IsKeyDown(Keys.D) && !isFalling)
                    {
                        textureChangeCounter--;
                    }
                    newLocation.X += 4;
                    facing = "right";
                }
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl))
            {
                Console.WriteLine(location.X + ", " + location.Y);
            }
            if (keyboardState.IsKeyDown(Keys.Space) && !isFalling)
            {
                isFalling = true;
                verticalVelocity = -20;
            }

            if (projectileCooldown > 0)
            {
                projectileCooldown--;
            }
            if (keyboardState.IsKeyDown(Keys.J) && projectileCooldown == 0 && mana >= 25)
            {
                if (swingFacing == "right")
                {
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, 10, currentLocation));
                }
                else if (swingFacing == "left")
                {
                    currentLocation.AddProjectile(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)), projectileTexture, -10, currentLocation));
                }
                projectileCooldown = 60;
                mana -= 25;
            }
            if (keyboardState.IsKeyDown(Keys.F) && !previousKeyboardState.IsKeyDown(Keys.F) && equipmentMenu.GetEquippedItem() != null && swinging == false)
            {
                if (equipmentMenu.GetEquippedItem().GetItem().GetType() == typeof(Items.SwordItem))
                {
                    swinging = true;
                    swingFacing = facing;
                    swordIsActive = true;
                }
                else if (equipmentMenu.GetEquippedItem().GetItem().GetType() == typeof(Items.ScytheItem))
                {
                    swinging = true;
                    swingFacing = facing;
                    scytheIsActive = true;
                }
            }
            

            if (!swinging)
            {
                swingFacing = facing;
            }
            if (swinging)
            {
                swordTextureChangeCounter--;
                if (swordTextureChangeCounter < 0)
                {
                    swordTextureChangeCounter = 5;
                    currentSwordTextureState++;
                    if (currentSwordTextureState >= 2)
                    {
                        swinging = false;
                        scytheIsActive = false;
                        swordIsActive = false;
                        currentSwordTextureState = 0;
                    }
                }
            }

            if (isFalling)
            {
                newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }

            newHitBox = new Rectangle((int)newLocation.X, (int)newLocation.Y, width, height);
            Collisions.CollideWithTiles(tiles, this);
            location = newLocation;
            hitBox = newHitBox;
            if (facing == "left")
            {
                swordHitBox = new Rectangle((int)location.X - swordOffset, (int)location.Y, swordOffset, height);
            }
            else if (facing == "right")
            {
                swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
            }

            if (keyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E))
            {
                foreach (Portal portal in currentLocation.GetPortals())
                {
                    if (Collisions.EntityCollisions(hitBox, portal.hitBox))
                    {
                        Travel(portal.GetDestination(), portal.GetPositionDestination());
                    }
                }
            }

            if (manaRegenCooldown > 0)
            {
                manaRegenCooldown--;
            }
            if (manaRegenCooldown == 0 && (mana < maxMana))
            {
                mana++;
                manaRegenCooldown = 10;
            }

            playerInfoBar.Update();

            previousKeyboardState = keyboardState;
        }
        override public void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Rectangle sourceRectangle;
            if (swinging)
            {
                int textureRow = 0;
                int textureOffset = 0;
                if (swingFacing == "left")
                {
                    textureRow = 1;
                    textureOffset = swordOffset;
                }
                sourceRectangle = new Rectangle(currentSwordTextureState * 130, textureRow * 100, 130, 100);
                spriteBatch.Draw(swordTexture, new Vector2(location.X - offsetX - textureOffset, location.Y - offsetY), sourceRectangle, Color.White);
            }

            int sourceY = 0;
            if (state == "hurt")
            {
                sourceY += 200;
            }
            else if (state == "invulnerable")
            {
                sourceY += 400;
            }
            if (swingFacing == "right")
            {
                sourceY += 100;
            }

            sourceRectangle = new Rectangle(currentTextureState * 100, sourceY, 100, 100);

            spriteBatch.Draw(megamanTexture, new Vector2(location.X - offsetX, location.Y - offsetY), sourceRectangle, Color.White);

            playerInfoBar.Draw(spriteBatch);

        }

        public void GetHit(String direction, int damage)
        {
            invulnerableTimer = 100;
            invulnerable = true;
            health -= damage;
            if (health <= 0)
            {
                /* die(); */
                location = currentLocation.GetSpawnPoint();
                health = maxHealth;
                mana = maxMana;
                xp = 0;
                Game1.ToggleMenu(new InfoBox("dead."));
            }

            state = "hurt";
            hurtCounter = 20;

            if (direction == "left")
            {
                horizontalVelocity -= 5;
            }
            else
            {
                horizontalVelocity += 5;
            }
            if (!isFalling)
            {
                verticalVelocity = -7;
            }
            else
            {
                verticalVelocity -= 3;
            }
            isFalling = true;
        }

        public void AddXp(int xpToAdd)
        {
            xp += xpToAdd;
            if (xp >= xpToLevel)
            {
                /* LevelUp(); */
                xp -= xpToLevel;
            }
        }
    }
}
