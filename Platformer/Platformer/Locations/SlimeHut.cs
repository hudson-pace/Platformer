using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;

namespace Platformer.Locations
{
    class SlimeHut : Location
    {
		private static string contentPath = "tiled-maps/slime-hut";
		public SlimeHut(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice, contentPath)
        {
            height = 17;
            width = 28;
            NPCList.Add(new NPCs.Wizard(new Vector2(750, 600), this, screenWidth, screenHeight, player));
            spawnPoint = new Vector2(60, 60);
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(1, 13, Game1.slimeCity, 25, 26));
        }
    }
}
