using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Entity
    {
        public Vector2 location, newLocation;
        public int width, height, verticalVelocity, horizontalVelocity;
        public int squishCounter = 0;
        public bool isFalling = true, isEnemy;
        public bool canFall = true;
        public string state = "normal";
        public int hurtCounter = 0;
        public Rectangle hitBox;
    }
}
