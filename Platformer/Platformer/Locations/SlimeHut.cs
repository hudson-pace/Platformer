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
                    if (i == 1 || i == 2 || i == width - 2 || i == width - 3 || j == 1 || j == 2)
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 12, this);
                    }
                    else if ((i == 3 && j == height - 2) || (i == 3 && j == 3) || (i == width - 4 && j == height - 2) || (i == width - 4 && j == 3))
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 12, this);
                    }
                    else if (i == 3)
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 9, this);
                    }
                    else if (i == width - 4)
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 11, this);
                    }
                    else if (j == 3)
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 8, this);
                    }
                    else if (j == height - 2)
                    {
                        tiles[i][j] = new Tiles.SlimeBlock(i, j, 10, this);
                    }
                }
            }

            tiles[15][height - 2] = new Tiles.SlimeBlock(15, height - 2, 12, this);
            tiles[15][height - 3] = new Tiles.SlimeBlock(15, height - 3, 4, this);

            tiles[16][height - 2] = new Tiles.SlimeBlock(16, height - 2, 12, this);
            tiles[16][height - 3] = new Tiles.SlimeBlock(16, height - 3, 5, this);

            NPCList.Add(new NPCs.Wizard(new Vector2(750, 600), this, screenWidth, screenHeight));
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
            NPCs.Wizard.LoadTextures(content);
        }
    }
}
