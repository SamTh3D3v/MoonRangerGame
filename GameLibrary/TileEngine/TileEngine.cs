using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameLibrary.TileEngine
{
    public class TileEngine
    {
        #region Field
        static int _tileWidth;   //Width And Hight
        #endregion
        #region Property
        public static int TileWidth
        {
            get { return _tileWidth; }
        }

        #endregion
        #region Methods
        public TileEngine(int tileWidth)
        {
            TileEngine._tileWidth = tileWidth;
        }

        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / _tileWidth, (int)position.Y / _tileWidth);
        }
        #endregion
    }
}
