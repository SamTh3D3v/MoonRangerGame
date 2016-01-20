using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.TileEngine
{
    public class TileMap
    {
        #region Fields
        List<TileSet> tilesets;
        List<MapLayer> mapLayers;
        static int mapWidth;
        static int mapHeight;

        #endregion
        #region Property
        public static int WidthInPixels { get { return mapWidth * TileEngine.TileWidth; } }
        public static int HeightInPixels { get { return mapHeight * TileEngine.TileWidth; } }

        #endregion
        #region Methods
        public TileMap(List<TileSet> tilesets, List<MapLayer> layers)
        {

            this.tilesets = tilesets; 
            this.mapLayers = layers;
            mapWidth = mapLayers[0].Width;
            mapHeight = mapLayers[0].Height;
        }
        public TileMap(TileSet tileset, MapLayer layer)
        {
            tilesets = new List<TileSet>();
            tilesets.Add(tileset);
            mapLayers = new List<MapLayer>();
            mapLayers.Add(layer);
            mapWidth = mapLayers[0].Width; 
            mapHeight = mapLayers[0].Height;
        }
        public void AddLayer(MapLayer layer)
        {           
            mapLayers.Add(layer);
        }
        public void RemoveLayer(int index)
        {
            mapLayers.RemoveAt(index);
        }

        public void UpdateLayer(MapLayer maplayer,int index)
        {
            if (index > mapLayers.Count || index <0)
                return;
            mapLayers[index] = maplayer;
        }
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {                      
            foreach (MapLayer layer in mapLayers)
            {
                layer.Draw(spriteBatch, camera, tilesets);
            }
        }
        #endregion
    }
}
