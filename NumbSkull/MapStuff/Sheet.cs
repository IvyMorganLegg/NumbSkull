using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbSkull
{
    class Sheet
    {

        public TileType type;
        public Rectangle rect;
        public int tiles_wide, tiles_high;
        public Vector2 offset;

        public Sheet(int x, int y, int x2, int y2, TileType Type, int Tiles_wide, int Tiles_high, float top_left_corner_x, float top_left_corner_y)
        {
            int width = x2 - x + 1;
            int height = y2 - y + 1;
            rect = new Rectangle(x, y, width, height);
            type = Type;
            tiles_wide = Tiles_wide;
            tiles_high = Tiles_high;
            offset = new Vector2(rect.X, rect.Y) - new Vector2(top_left_corner_x, top_left_corner_y);
        }
    }

    class SheetManager
    {
        int num_sheet_parts;

        public SheetManager() { num_sheet_parts = 0; }

        public void Setup_Sheet_Level_1(ref Sheet[] sheet)
        {
            num_sheet_parts = 0;
            int n = 0;
            sheet[n] = new Sheet(0, 0, 1, 1, TileType.empty, 1, 1, 0f, 0f); n++; //nothing
        }
    }
}
