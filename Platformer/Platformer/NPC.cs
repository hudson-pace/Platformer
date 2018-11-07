﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    abstract class NPC : Entity
    {
        public string greeting;
        public string[] options;

        abstract public void Update(KeyboardState state, Tile[][] tiles);
    }
}
