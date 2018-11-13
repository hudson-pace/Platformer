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
            Items.CopperCoin.Register(1);
            Items.SilverCoin.Register(2);
            Items.GoldCoin.Register(3);
            Items.HealthPotion.Register(4);
            Items.ManaPotion.Register(5);
            Items.PlantFibers.Register(6);
            Items.ScytheItem.Register(7);
            Items.ShellFragment.Register(8);
            Items.SlimeItem.Register(9);
            Items.SlimeTail.Register(10);
            Items.SnailGoop.Register(11);
            Items.SwordItem.Register(12);
        }
        public static void LoadTextures(ContentManager content)
        {
            Items.CopperCoin.LoadTextures(content);
            Items.SilverCoin.LoadTextures(content);
            Items.GoldCoin.LoadTextures(content);
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
