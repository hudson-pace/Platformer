using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer.Enemies
{
    class Snail : Enemy
    {
        private static Texture2D textureRegular, textureHurt, textureRegularRight, textureHurtRight;
        private string facing;
        private int checkFacingCounter;
        public Snail(Vector2 location, Player player, Location currentLocation, Spawner spawner)
        {
            this.location = location;
            newLocation = location;
            this.player = player;
            this.currentLocation = currentLocation;
            width = 65;
            height = 50;
            hitBox = new Rectangle((int)location.X, (int)location.Y, width, height);
            health = 80;
            drops.Add(new Items.SnailGoop(70));
            drops.Add(new Items.ShellFragment(100));
            facing = "left";
            checkFacingCounter = 40;
            this.spawner = spawner;
            name = "snail";
        }


        override public void Update(KeyboardState state, Tile[][] tiles)
        {
            if (isFalling)
            {
                this.newLocation.Y += verticalVelocity;
                verticalVelocity++;
            }
            else
            {
                if (checkFacingCounter > 0)
                {
                    checkFacingCounter--;
                }
                if (checkFacingCounter == 0)
                {
                    checkFacingCounter = 40;
                    if (player.location.X < location.X)
                    {
                        facing = "left";
                        horizontalVelocity = -1;
                    }
                    else
                    {
                        facing = "right";
                        horizontalVelocity = 1;
                    }
                }
            }

            newLocation.X += horizontalVelocity;

            base.Update(state, tiles);
        }

        public override Enemy Create(Vector2 location, Location currentLocation, Spawner spawner)
        {
            return new Snail(location, player, currentLocation, spawner);
        }

        public override void Draw(SpriteBatch spriteBatch, int offsetX, int offsetY)
        {
            Texture2D texture = textureRegular;
            if (state != "hurt" && facing == "right")
            {
                texture = textureRegularRight;
            }
            if (state == "hurt")
            {
                if (facing == "left")
                {
                    texture = textureHurt;
                }
                else
                {
                    texture = textureHurtRight;
                }
            }
            spriteBatch.Draw(texture, new Vector2(location.X - offsetX, location.Y - offsetY), Color.White);
        }
        public static void LoadTextures(ContentManager content)
        {
            textureRegular = content.Load<Texture2D>("snail");
            textureHurt = content.Load<Texture2D>("snail-hurt");
            textureRegularRight = content.Load<Texture2D>("snail-right");
            textureHurtRight = content.Load<Texture2D>("snail-hurt-right");
            Items.SnailGoop.LoadTextures(content);
            Items.ShellFragment.LoadTextures(content);
        }
    }
}
