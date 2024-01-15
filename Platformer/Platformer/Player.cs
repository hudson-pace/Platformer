using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using System.Diagnostics;

namespace Platformer
{
    class Player : Entity
    {
        private class Attack
        {
            private int range;
            private int duration;
            private int delay;
            private string texturePath;
            private Texture2D texture;
            private int frames;
            public Attack(int range, int delay, string texturePath, int textureWidth)
            {
                this.range = range;
                this.texturePath = texturePath;
                this.delay = delay;
            }

            public int GetAttackDuration()
            {
                return 1;
            }

            public void LoadTextures(ContentManager content)
            {
                texture = content.Load<Texture2D>(texturePath);
                frames = texture.Width / textureWidth;
				duration = delay * frames;
			}
            public Texture2D GetTexture()
            {
                return texture;
            }
            public int GetDuration()
            {
                return duration;
			}
            public int GetDelay()
            {
                return delay;
            }
            public int GetFrames()
            {
                return frames;
            }
        }

		private bool showHitboxes = false;


		private static Texture2D projectileTexture;
        private Location currentLocation;
        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;
        public bool swordIsActive = false, scytheIsActive = false;
        private bool swinging = false;
        private bool running = false;
        public Rectangle swordHitBox;
        public string facing = "right";
        private int projectileCooldown = 0, swordOffset;
        private PlayerInventory inventory;
        private EquipmentMenu equipmentMenu;
        private int health, maxHealth, mana, maxMana, xp, xpToLevel;
        private int invulnerableTimer = 0;
        public bool invulnerable = false;
        private int manaRegenCooldown;

        /*
        private static int textureWidth = 120;
        private static int textureHeight = 80;
        private int textureScale = 2;
        private int rightOffset = -15;
        private int leftOffset = 15;
        */
		private static int textureWidth = 100;
		private static int textureHeight = 64;
		private int textureScale = 1;
		private int rightOffset = 0;
		private int leftOffset = 0;

		enum AnimationType
        {
            Running,
            Idle,
            Jumping,
            Attacking,
        }
        private static Dictionary<AnimationType, int> animationFrames = new Dictionary<AnimationType, int>();
        private static Dictionary<AnimationType, string> animationTextureSources = new Dictionary<AnimationType, string>()
        {
            /*
            { AnimationType.Running, "knight/Run" },
            { AnimationType.Idle, "knight/Idle" },
            { AnimationType.Jumping, "knight/Jump" },
            { AnimationType.Attacking, "knight/Attack" },
            */
            { AnimationType.Running, "knight2/Walking_KG_1" },
			{ AnimationType.Idle, "knight2/Idle_KG_1" },
			{ AnimationType.Jumping, "knight2/Jump_KG_1" },
		};
        private static Dictionary<AnimationType, int> animationDelays = new Dictionary<AnimationType, int>()
        {
            { AnimationType.Running, 5 },
            { AnimationType.Idle, 10 },
            { AnimationType.Jumping, 5 },
        };
        private static Dictionary<AnimationType, Texture2D> animationTextures = new Dictionary<AnimationType, Texture2D>();

        private AnimationType currentAnimation = AnimationType.Idle;
        private int currentAnimationFrame = 0;
        private int animationDelayCounter;
        private int swingCounter;

        private Attack currentAttack;

        private Attack swordAttack;
        private Attack shieldAttack;

        private List<Attack> attacks = new List<Attack>();

		public List<Projectile> Projectiles { get; } = new List<Projectile>();

        public Player(Vector2 location, int screenWidth, int screenHeight)
        {
            this.location = location;

            // height = 40;
            // width = 20;
            height = 64;
            width = 45;
			height *= textureScale;
			width *= textureScale;

			inventory = new PlayerInventory(this, screenWidth, screenHeight);
            equipmentMenu = new EquipmentMenu(this);
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            swordHitBox = new Rectangle((int)location.X + width, (int)location.Y, swordOffset, height);
            

            swordOffset = 40;
            maxHealth = 100;
            health = maxHealth;
            maxMana = 50;
            mana = maxMana;
            xpToLevel = 100;
            xp = 0;

            swordAttack = new Attack(40, 5, "knight2/Attack_KG_1", 120);
            shieldAttack = new Attack(40, 5, "knight2/Shield_Bash_KG", 120);

            attacks.Add(swordAttack);
            attacks.Add(shieldAttack);

            currentAttack = swordAttack;

            animationDelayCounter = animationDelays[currentAnimation];
        }

        public static void LoadTextures(ContentManager content, GraphicsDevice graphicsDevice)
        {
            projectileTexture = content.Load<Texture2D>("blue-ball");
            Inventory.LoadTextures(content);
            Inventory.CreateTextures(graphicsDevice);
            EquipmentMenu.CreateTextures(graphicsDevice);
            PlayerInfoBar.CreateTextures(graphicsDevice);
            PlayerInfoBar.LoadTextures(content);
            InfoBox.CreateTextures(graphicsDevice);

            foreach (KeyValuePair<AnimationType, string> entry in animationTextureSources)
            {
                Texture2D texture = content.Load<Texture2D>(entry.Value);
                animationTextures.Add(entry.Key, texture);
                animationFrames.Add(entry.Key, texture.Width / textureWidth);
            }
        }
        public void LoadAttackTextures(ContentManager content)
        {
            attacks.ForEach(attack => attack.LoadTextures(content));
        }

        public void AddToInventory(Item item)
        {
            inventory.AddToInventory(item, false, 0);
        }
        public bool Pay(int price)
        {
            return inventory.Pay(price);
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
        public Inventory GetInventory()
        {
            return inventory;
        }
        public void Update(KeyboardState keyboardState, Location l, MouseState mouseState)
        {
            animationDelayCounter--;
			if (animationDelayCounter <= 0)
			{
				currentAnimationFrame++;
				if (currentAnimationFrame >= GetAnimationFrames())
				{
					currentAnimationFrame = 0;
				}
				animationDelayCounter = GetAnimationDelay();
			}
			newLocation = location;

            if (invulnerableTimer > 0)
            {
                invulnerableTimer--;
                if (state == "hurt" && invulnerableTimer < 80)
                {
                    state = "invulnerable";
                }
                if (invulnerableTimer == 0)
                {
                    invulnerable = false;
                    state = "normal";
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
            if (keyboardState.IsKeyDown(Keys.A) ^ keyboardState.IsKeyDown(Keys.D)) // XOR. Either, but not both, pressed.
            {
                running = true;
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    newLocation.X -= 6;
                    facing = "left";
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    newLocation.X += 6;
                    facing = "right";
                }
            } else
            {
                running = false;
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
                if (facing == "right")
                {
                    Projectiles.Add(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)),
                        projectileTexture, 10, currentLocation, this));
                }
                else if (facing == "left")
                {
                    Projectiles.Add(new Projectile(new Vector2(location.X + (width / 2) - (30 / 2), location.Y + (height / 2) - (30 / 2)),
                        projectileTexture, -10, currentLocation, this));
                }
                projectileCooldown = 60;
                mana -= 25;
            }
            if (swinging == false)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && equipmentMenu.GetEquippedItem() != null)
                {
                    currentAttack = swordAttack;
                    swingCounter = 3;
                    swinging = true;
                    if (equipmentMenu.GetEquippedItem().GetItem().GetType() == typeof(Items.SwordItem))
                    {
                        swordIsActive = true;
                    }
                    else if (equipmentMenu.GetEquippedItem().GetItem().GetType() == typeof(Items.ScytheItem))
                    {
                        scytheIsActive = true;
                    }
                } else if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                {
                    swordIsActive = true;
                    currentAttack = shieldAttack;
                    swingCounter = 0;
                    swinging = true;
                }
            }
            
            if (swinging)
            {
                swingCounter++;
                if (swingCounter >= currentAttack.GetDuration()) // Only play animation once.
                {
					swinging = false;
					scytheIsActive = false;
					swordIsActive = false;
				}
            }

            if (isFalling)
            {
				newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }

            if (swinging)
            {
                StartAnimation(AnimationType.Attacking);
            } else if (isFalling)
            {
                StartAnimation(AnimationType.Jumping);
            } else if (running)
            {
                StartAnimation(AnimationType.Running);
            } else
            {
                StartAnimation(AnimationType.Idle);
            }

            newHitBox = new Rectangle((int)newLocation.X, (int)newLocation.Y, width, height);
            Collisions.CollideWithTiles(l, this);
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
                    if (Collisions.EntityCollisions(hitBox, portal.GetHitbox()))
                    {
						Portal destination = portal.GetDestination();
                        Travel(destination.Location, new Vector2(destination.Position.X, destination.Position.Y - (height - portal.Size.Y)));
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

            for (int i = Projectiles.Count- 1; i >= 0; i--) // some elements may be removed, so iterate backwards.
            {
                Projectiles[i].Update(l);
            }

            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;
        }
        override public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            Rectangle sourceRectangle;

			Color tintColor = Color.White;
			if (state == "hurt")
            {
                tintColor = Color.Red;
            }
            else if (state == "invulnerable")
            {
                tintColor = Color.Gray;
            }

            sourceRectangle = new Rectangle(currentAnimationFrame * textureWidth, 0, textureWidth, textureHeight);

            Texture2D texture = GetAnimationTexture();
            
            if (showHitboxes)
            {
				spriteBatch.DrawRectangle(location.X, location.Y, width, height, Color.White);
			}

			SpriteEffects effect = SpriteEffects.None;
            int offset = rightOffset;
            if (facing == "left")
            {
                effect = SpriteEffects.FlipHorizontally;
                offset = leftOffset;
            }
            int widthDiff = offset + textureWidth - (width / textureScale);
            int widthOffset = (widthDiff * textureScale) / 2;
            int heightDiff = textureHeight - (height / textureScale);
            int heightOffset = (heightDiff * textureScale);
            spriteBatch.Draw(texture, new Vector2(location.X - widthOffset, location.Y - heightOffset), sourceRectangle, tintColor, 0, new Vector2(0, 0), /*new Vector2((textureWidth / 2) + offset, textureHeight / 2),*/ textureScale, effect, 1);
			if (showHitboxes && swinging)
			{
				spriteBatch.DrawRectangle(swordHitBox, Color.Red);
			}
		}

        public void GetHit(string direction, int damage)
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

        public void RemoveProjectile(Projectile projectile)
        {
            Projectiles.Remove(projectile);
        }

        private void StartAnimation(AnimationType animationType)
        {
            if (currentAnimation != animationType)
            {
                currentAnimation = animationType;
                currentAnimationFrame = 0;
            }
        }

        private Texture2D GetAnimationTexture()
        {
			if (currentAnimation == AnimationType.Attacking)
			{
				return currentAttack.GetTexture();
			}
			else
			{
				return animationTextures[currentAnimation];
			}
		}
        private int GetAnimationDelay()
        {
			if (currentAnimation == AnimationType.Attacking)
			{
				return currentAttack.GetDelay();
			}
			else
			{
				return animationDelays[currentAnimation];
			}
		}
        private int GetAnimationFrames()
        {
            if (currentAnimation == AnimationType.Attacking)
            {
                return currentAttack.GetFrames();
            }
            else
            {
                return animationFrames[currentAnimation];
            }
        }
    }
}
