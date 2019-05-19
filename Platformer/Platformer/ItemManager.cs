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
            int i = 0;
            Items.CopperCoin.Register(i++);
            Items.SilverCoin.Register(i++);
            Items.GoldCoin.Register(i++);
            Items.HealthPotion.Register(i++);
            Items.ManaPotion.Register(i++);
            Items.PlantFibers.Register(i++);
            Items.ScytheItem.Register(i++);
            Items.ShellFragment.Register(i++);
            Items.SlimeItem.Register(i++);
            Items.SlimeTail.Register(i++);
            Items.SnailGoop.Register(i++);
            Items.SwordItem.Register(i++);
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