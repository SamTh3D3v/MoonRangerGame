using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.TileEngine
{
    public class MapLayer
    {
        #region Field
        Tile[,] map;
        #endregion
        #region Property
        public int Width
        {
            get { return map.GetLength(1); }
        }
        public int Height
        {
            get { return map.GetLength(0); }
        }
        #endregion
        #region Methods
        public MapLayer(Tile[,] map)
        {
            this.map = (Tile[,])map.Clone();
        }
        public MapLayer(int width, int height)
        {
            map = new Tile[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[y, x] = new Tile(8, 1);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return map[y, x];
        }
        public void SetTile(int x, int y, Tile tile)
        {
            map[y, x] = tile;
        }
        public void SetTile(int x, int y, int tileIndex, int tileset)
        {
            map[y, x] = new Tile(tileIndex, tileset);
        }
        public void Draw(SpriteBatch spriteBatch, Camera camera, List<TileSet> tilesets)
        {
            //Undo The Zoom Firsrt 
            Point cameraPoint = TileEngine.VectorToCell(camera.Position * (1 / camera.Zoom));            
            Point viewPoint = TileEngine.VectorToCell(
                new Vector2(
                    (camera.Position.X + camera.ViewportRectangle.Width) * (1 / camera.Zoom),
                    (camera.Position.Y + camera.ViewportRectangle.Height) * (1 / camera.Zoom)));

            Point min = new Point();
            Point max = new Point();

            min.X = Math.Max(0, cameraPoint.X - 1);
            min.Y = Math.Max(0, cameraPoint.Y - 1);
            max.X = Math.Min(viewPoint.X + 1, Width);
            max.Y = Math.Min(viewPoint.Y + 1, Height);

            Rectangle destination = new Rectangle(0, 0, TileEngine.TileWidth, TileEngine.TileWidth);
            Tile tile;

            for (int y = min.Y; y < max.Y; y++)
            {
                destination.Y = y * TileEngine.TileWidth;

                for (int x = min.X; x < max.X; x++)
                {
                    tile = GetTile(x, y);

                    if (tile.TileIndex == -1 || tile.Tileset == -1)
                        continue;

                    destination.X = x * TileEngine.TileWidth;

                    spriteBatch.Draw(
                        tilesets[tile.Tileset].Texture,
                        destination,
                        tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                        Color.White);
                    
                }
            }
        }

        #endregion
    }
}
