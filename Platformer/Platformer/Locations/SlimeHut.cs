using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Locations
{
    class SlimeHut : Location
    {
        public SlimeHut(Player player, int screenGridWidth, int screenGridHeight, int screenWidth, int screenHeight, GraphicsDevice graphicsDevice)
            : base(player, screenGridWidth, screenGridHeight, screenWidth, screenHeight, graphicsDevice)
        {
            height = 17;
            width = 28;
            offsetX = 0;
            offsetY = 0;
            AddBorder();

            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if (i == 1 || i == width - 2 || j == 1 || j == height - 2)
                    {
                        tiles[i][j] = new Tiles.BrickWall(i, j, this);
                    }
                }
            }

            
        }

        public override void AddPortals()
        {
            portals.Add(new Portal(new Vector2(100, 600), Game1.slimeCity, new Vector2(1300, 1300), this));
        }
        override public void LoadTextures(ContentManager content)
        {
            Portal.LoadContent(content);
            DialogBox.LoadTextures(content);
            Tiles.SlimeBlock.LoadTextures(content);
            Tiles.BrickWall.LoadTextures(content);
        }
    }
}
