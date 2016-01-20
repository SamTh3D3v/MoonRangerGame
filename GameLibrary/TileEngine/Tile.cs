using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.TileEngine
{
    public class Tile
    {
        #region Field
        int tileIndex;
        int tileset;
        #endregion
        #region Property
        public int TileIndex
        {
            get { return tileIndex; }
            private set { tileIndex = value; }
        }
        public int Tileset
        {
            get { return tileset; }
            private set { tileset = value; }
        }
        #endregion
        #region Methods
        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex; 
            Tileset = tileset;
        }
        #endregion
    }
}
