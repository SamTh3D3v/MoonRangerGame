using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.TileEngine
{
    public class TileSet
    {
        #region Fields
        Texture2D image;
        int tileWidth; //The Hight Is The Same As The WiDth 
        int tilesWide;
        int tilesHigh;
        Rectangle[] sourceRectangles;
        #endregion
        #region Property        
        public Texture2D Texture
        {
            get { return image; }
            set { image = value; }
        }
        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }
        public int TilesWide
        { get { return tilesWide; } private set { tilesWide = value; } }
        public int TilesHigh
        { get { return tilesHigh; } private set { tilesHigh = value; } }
        public Rectangle[] SourceRectangles
        { get { return (Rectangle[])sourceRectangles.Clone(); } }
        #endregion
        #region Method Region
        public TileSet(Texture2D image, int tilesWide, int tilesHigh, int tileWidth)    // TileWidth In Pixel Same as Hight 
        {
            Texture = image; 
            TileWidth = tileWidth;
            TilesWide = tilesWide; 
            TilesHigh = tilesHigh;
            int tiles = tilesWide * tilesHigh;
            sourceRectangles = new Rectangle[tiles];
            int tile = 0;
            for (int y = 0; y < tilesHigh; y++)
                for (int x = 0; x < tilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                        x * tileWidth, y * tileWidth, tileWidth, tileWidth); tile++;
                }
        }
        
        
        #endregion

    }
}
