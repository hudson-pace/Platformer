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
        private static Dictionary<int, Item> itemDict = new Dictionary<int, Item>();
        public static void RegisterItems()
        {
            int i = 0;
            Items.CopperCoin.Register(i++);
            itemDict[i++] = new Items.CopperCoin(0);
            Items.SilverCoin.Register(i++);
            itemDict[i++] = new Items.SilverCoin(0);
            Items.GoldCoin.Register(i++);
            itemDict[i++] = new Items.GoldCoin(0);
            Items.HealthPotion.Register(i++);
            itemDict[i++] = new Items.HealthPotion(0);
            Items.ManaPotion.Register(i++);
            itemDict[i++] = new Items.ManaPotion(0);
            Items.PlantFibers.Register(i++);
            itemDict[i++] = new Items.PlantFibers(0);
            Items.ScytheItem.Register(i++);
            itemDict[i++] = new Items.ScytheItem(0);
            Items.ShellFragment.Register(i++);
            itemDict[i++] = new Items.ShellFragment(0);
            Items.SlimeItem.Register(i++);
            itemDict[i++] = new Items.SlimeItem(0);
            Items.SlimeTail.Register(i++);
            itemDict[i++] = new Items.SlimeTail(0);
            Items.SnailGoop.Register(i++);
            itemDict[i++] = new Items.SnailGoop(0);
            Items.SwordItem.Register(i++);
            itemDict[i++] = new Items.SwordItem(0);
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
        public static Item GetItemFromId(int id)
        {
            return itemDict[id];
        }
    }
}