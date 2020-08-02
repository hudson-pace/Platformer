using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    abstract class Entity
    {
        public Vector2 location, newLocation;
        public int width, height, verticalVelocity, horizontalVelocity;
        public bool isFalling = true, isEnemy = false;
        public bool canFall = true;
        public bool active = true;
        public string state = "normal";
        public int hurtCounter = 0;
        public Rectangle hitBox, newHitBox;
        abstract public void Draw(SpriteBatch spriteBatch);
    }
}
