using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer
{
    abstract class Item : Entity
    {
        public Item()
        {
            verticalVelocity = -3;
        }
        abstract public void SetLocation(Vector2 location);
    }
}
