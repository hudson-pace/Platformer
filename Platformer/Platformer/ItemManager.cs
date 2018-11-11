using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;

namespace Platformer
{
    class ItemManager
    {
        public static void RegisterItems()
        {
            Items.HealthPotion.Register(1);
            Items.ManaPotion.Register(2);
            Items.PlantFibers.Register(3);
            Items.ScytheItem.Register(4);
            Items.ShellFragment.Register(5);
            Items.SlimeItem.Register(6);
            Items.SlimeTail.Register(7);
            Items.SnailGoop.Register(8);
            Items.SwordItem.Register(9);
        }
        public static void LoadTextures(ContentManager content)
        {
            Items.HealthPotion.LoadTextures(content);
            Items.ManaPotion.LoadTextures(content);
            Items.PlantFibers.LoadTextures(content);
            Items.ScytheItem.LoadTextures(content);
            Items.ShellFragment.LoadTextures(content);
            Items.SlimeItem.LoadTextures(content);
            Items.SlimeTail.LoadTextures(content);
            Items.SnailGoop.LoadTextures(content);
            Items.SwordItem.LoadTextures(content);
        }
    }
}
