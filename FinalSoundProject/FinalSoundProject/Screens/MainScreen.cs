using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Krypton;
using Krypton.Lights;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Nine;
using Nine.Components;
using Nine.Graphics;
using Nine.Graphics.Materials;
using Nine.Graphics.ParticleEffects;
using Nine.Graphics.PostEffects;
using Nine.Graphics.Primitives;
using Nine.Physics;
using GameLibrary;
using GameLibrary.TileEngine;



namespace FinalSoundProject.Screens
{
    public enum Light
    { ON, OFF }
    public class MainScreen : GameStateBase
    {
        #region Properties
        public int Stage
        {
            get
            {
                return _stage;
            }
            set
            {
                _stage = value;
                NewLevel();
            }
        }
        #endregion
        #region Fields
        public int MapWidthInTiles = 40;
        public int MapHightInTiles = 40;
        private Vector2 cameraPos = Vector2.Zero;
        public int TileSize;
        TileEngine engine;
        Texture2D backgroundImage;
        TileSet tilesetFloor;
        TileSet tilesetWalls;
        TileMap map;
        public static int[,] wallsPlan;
        Player player;
        private Enemy enemy;
        private List<Enemy> enemies; 
        public Player mainPlayer;
        SpriteFont font;
        public GameLibrary.TileEngine.Camera camera;
        //Basic Spote Of Lighting Stuff
        public Light light = Light.ON;
        private Texture2D blackSquare;
        RenderTarget2D mainScene;
        RenderTarget2D lightMask;
        public Texture2D lightMskTex;
        Effect lightingEffect;
        //end Of Light Stuff
        //Krypton engine Lighting Stuff
        public KryptonEngine Krypton;
        public Texture2D mLightTexture;
        private float mVerticalUnits = 50;
        //Krypton End Stuff
        private int _stage = 1;
        //Stages Properties 
        public List<List<Vector2>> StartEndPositions;
        #endregion
        public MainScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            Stage = 1;
            MapWidthInTiles = 40;
            MapHightInTiles = 40;
            TileSize = 66;
            engine = new TileEngine(TileSize);
            camera = new GameLibrary.TileEngine.Camera(GameRef.ScreenRectangle);
            SetUpStartingEndPositions();
           
        }
        private void SetUpStartingEndPositions()
        {

            StartEndPositions = new List<List<Vector2>>()
            {
                new List<Vector2>()
                {
                    new Vector2(2.5f,2.5f),
                    new Vector2(39.5f,38f)
                },
                
                new List<Vector2>()
                {
                    new Vector2(0.5f,38.5f),
                    new Vector2(39.5f,1f)
                },
                
                new List<Vector2>()
                {
                    new Vector2(0.5f,1.5f),
                    new Vector2(39.5f,25f)
                },
                
                new List<Vector2>()
                {
                    new Vector2(0.5f,25.5f),
                    new Vector2(38.5f,38f)
                }

            };
        }
        protected override void LoadContent()
        {

            ContentManager Content = GameRef.Content;
            font = Content.Load<SpriteFont>(@"Assets\Font\SpriteFont1");
            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\MainScreen");
            Krypton = new KryptonEngine(GameRef, @"Effects\KryptonEffect");
            Krypton.Initialize();
            Dictionary<Moves, Vector3> _mvs = new Dictionary<Moves, Vector3>()
                                                {
                                                   {Moves.Left, new Vector3(0,1,1)},
                                                   {Moves.Right, new Vector3(2,3,1)},
                                                   {Moves.ForWard, new Vector3(2,3,0)},
                                                   {Moves.BackWard, new Vector3(0,1,0)}
                                                };

            mainPlayer = new Player(GameRef, Content.Load<Texture2D>(@"Assets\Images\SmallPlayer"), new Point(50, 50), StartEndPositions.First().First() * TileSize, camera, _mvs, Moves.Left, 15, 5);
            Components.Add(mainPlayer);
            Dictionary<Moves, Vector3> _mvsEnemy = new Dictionary<Moves, Vector3>()
                                                {
                                                   {Moves.Left, new Vector3(0,2,2)},
                                                   {Moves.Right, new Vector3(0,2,1)},
                                                   {Moves.ForWard, new Vector3(0,2,0)},
                                                   {Moves.BackWard, new Vector3(0,2,3)}
                                                };
            enemy = new Enemy(GameRef, Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30), new Vector2(400, 250), camera, _mvsEnemy, Moves.Left, 5, 8,new Rectangle(2*TileSize,2*TileSize,20*TileSize,20*TileSize), 20);
            Components.Add(enemy);
            SetUpEnemies();
            // 8 rect * 66 px
            Texture2D floorTilesetTexture = Game.Content.Load<Texture2D>(@"Assets\Tiles\Floor");
            tilesetFloor = new TileSet(floorTilesetTexture, 8, 8, 66);

            Texture2D wallsTileSetTexture = Game.Content.Load<Texture2D>(@"Assets\Tiles\Walls");
            tilesetWalls = new TileSet(wallsTileSetTexture, 8, 2, 66);
            List<TileSet> listTileSets = new List<TileSet>();
            listTileSets.Add(tilesetFloor);
            listTileSets.Add(tilesetWalls);
            List<MapLayer> mapLayers = new List<MapLayer>();
            MapLayer layer = new MapLayer(MapWidthInTiles, MapHightInTiles);
            Random rand = new Random();
            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    // The RanDom UnForTunantly Sucks -->
                    Tile tile = new Tile(rand.Next(0, 64), 0);
                    layer.SetTile(x, y, tile);
                }
            }
            //Krypton
           
            // Components.Add(Krypton);


            //this.mLightTexture = LightTextureBuilder.CreatePointLight(GameRef.GraphicsDevice, 512);
            //CreateLight(mLightTexture,mainPlayer.SprtSheet.Position);
            mapLayers.Add(layer);
            MapLayer wallsLayer = new MapLayer(MapWidthInTiles, MapHightInTiles);

            for (int i = 0; i < wallsLayer.Height; i++)
            {
                for (int j = 0; j < wallsLayer.Width; j++)
                {

                    int index = rand.Next(0, 16);
                    Tile tile = new Tile(wallsPlan[j, i], 1);
                    wallsLayer.SetTile(i, j, tile);

                    if (wallsPlan[j, i] != 8)
                    {
                        var hull = ShadowHull.CreateRectangle(new Vector2(TileSize, TileSize));
                        hull.Position.X = i * TileSize + TileSize / 2;
                        hull.Position.Y = -(j * TileSize + TileSize / 2);
                        Krypton.Hulls.Add(hull);
                    }
                }
            }
            mapLayers.Add(wallsLayer);
            map = new TileMap(listTileSets, mapLayers);
            //Light 
            lightMskTex = Content.Load<Texture2D>(@"Assets\Light\lightmask");
            blackSquare = Content.Load<Texture2D>(@"Assets\Light\blacksquare");
            lightingEffect = Content.Load<Effect>(@"Effects\Light");
            var pp = GraphicsDevice.PresentationParameters;
            mainScene = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            lightMask = new RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight);
            base.LoadContent();

        }

        private void SetUpEnemies()
        {
            enemies = new List<Enemy>();
             Dictionary<Moves, Vector3> _mvsEnemy = new Dictionary<Moves, Vector3>()
                                                {
                                                   {Moves.Left, new Vector3(0,2,2)},
                                                   {Moves.Right, new Vector3(0,2,1)},
                                                   {Moves.ForWard, new Vector3(0,2,0)},
                                                   {Moves.BackWard, new Vector3(0,2,3)}
                                                };
            switch (Stage)
            {
                case 1:
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 10) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(10 * TileSize, 8 * TileSize, 5 * TileSize, 7 * TileSize), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 18) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(10 * TileSize, 8 * TileSize, 14 * TileSize, 12 * TileSize), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 13) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(16 * TileSize, 8 * TileSize, 10 * TileSize, 10 * TileSize), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 9) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(10 * TileSize, 5 * TileSize, 10 * TileSize, 5 * TileSize), 20f));                       
                    break;
                case 2:
                    //new Type Of Enemies
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 10) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 18) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 13) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 9) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f)); 
                    break;
                case 3:
                    //new Type Of Enemies
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 10) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 18) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 13) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 9) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f)); 
                    break;
                case 4:
                    //new Type Of Enemies
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 10) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(12, 18) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 13) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f));
                    enemies.Add(new Enemy(GameRef, GameRef.Content.Load<Texture2D>(@"Assets\Images\Enemy"), new Point(30, 30),
                            new Vector2(18, 9) * TileSize, camera, _mvsEnemy, Moves.Left, 5, 8, new Rectangle(), 20f)); 
                    break;

            }
            foreach (var item in enemies)
            {
                Components.Add(item);
            }           
            
        }

        public void NewLevel()
        {
            if (_stage < 1 || _stage > 4)
                return;
            switch (_stage)
            {
                case 1:
                    wallsPlan = new int[,]
                         {
                            {0,4,4,4,0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                            {1,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,0,4,4,4,0,8,8,0,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,0,8,8,8,8,8,8,8,8,8,0,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,4,0,4,4,0,8,1},
                            {1,8,8,8,8,8,8,0,8,8,8,8,8,8,0,4,4,4,4,4,0,8,0,4,4,4,4,0,8,8,0,8,8,8,1,8,8,8,8,1},
                            {0,4,4,4,0,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,1},
                            {1,1,8,8,8,8,8,0,4,4,0,8,8,8,1,8,8,8,0,8,8,8,8,8,8,8,8,0,8,0,8,8,8,8,1,8,8,8,8,1},
                            {1,1,8,8,8,8,8,1,8,8,1,8,8,8,1,8,8,8,1,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,1,8,8,8,8,1},
                            {1,1,8,8,8,8,8,1,8,8,1,8,8,8,1,8,8,0,0,4,1,8,8,8,8,8,8,0,4,4,4,4,4,4,0,4,0,8,8,1},
                            {1,0,4,4,4,4,4,0,8,8,0,8,8,8,1,8,8,8,8,8,1,4,4,0,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,0,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,1,8,8,8,0,4,4,4,4,4,4,4,4,4,4,4,0,4,4,4,0,4,4,4,4,0,8,8,8,1},
                            {1,8,8,8,8,0,4,4,0,8,1,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,1,8,8,8,1,8,8,8,8,1,8,8,8,1},
                            {1,8,8,8,8,1,8,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1,8,8,8,1,8,0,8,8,1,8,8,8,1},
                            {1,8,8,8,8,1,8,8,8,8,1,8,8,0,8,8,8,0,8,8,8,8,8,8,8,8,1,8,8,8,1,8,0,8,8,0,4,0,8,1},
                            {1,8,0,8,8,1,8,8,8,8,1,8,8,8,8,8,8,8,8,8,8,0,4,4,4,4,0,8,0,8,1,8,8,8,8,1,8,8,8,1},
                            {1,8,1,8,8,0,4,4,4,4,0,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,1,8,8,8,0,8,8,8,8,1,8,8,8,1},
                            {1,8,1,8,8,1,8,8,8,8,1,8,8,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,0,0,4,0,8,8,8,1},
                            {1,8,1,8,8,1,8,8,8,8,1,8,8,8,8,0,4,4,4,0,4,4,4,0,8,8,1,8,0,8,8,8,8,1,8,8,8,8,8,1},
                            {1,8,1,8,8,0,8,8,8,8,1,8,0,8,8,1,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,1},
                            {1,8,1,8,8,8,8,8,8,8,1,8,8,8,8,1,8,8,8,1,8,8,0,8,8,8,1,8,8,8,0,4,4,0,8,8,0,8,8,1},
                            {1,8,1,8,8,8,8,0,8,8,1,8,8,8,8,1,8,0,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,1,8,8,0,8,8,8,8,1,8,0,8,8,0,8,8,8,0,4,4,4,4,4,4,0,4,0,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,1,8,8,1,8,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,0,8,8,0,4,4,4,4,0,8,8,8,0,4,4,4,4,4,4,4,4,4,4,4,0,8,8,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,4,4,0,4,4,0,8,8,1},
                            {1,8,8,8,8,8,8,8,8,0,8,0,0,8,8,8,8,0,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,1,8,8,1,8,8,1},
                            {1,0,0,0,8,8,0,8,8,8,8,0,4,4,0,8,8,8,8,8,8,8,1,8,8,8,0,8,8,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,1,8,8,8,1,8,8,8,8,8,8,8,1,8,8,8,8,0,4,0,4,0,8,8,8,8,0,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,0,8,8,8,0,4,4,4,4,4,4,4,0,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,0,8,8,1,8,8,1},
                            {1,8,8,8,8,8,1,8,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,0,8,8,8,8,8,1,8,8,1},
                            {1,8,8,8,8,8,1,8,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,1,8,8,8,8,8,8,8,1,8,8,1},
                            {1,8,0,4,4,4,0,8,8,0,4,0,8,8,0,4,4,4,4,4,4,4,4,4,4,4,0,8,1,8,8,8,8,8,8,8,1,8,8,1},
                            {1,8,8,1,8,8,1,8,8,8,1,8,8,8,1,8,8,8,8,8,8,8,8,8,8,8,8,8,1,8,8,0,4,0,8,8,1,8,8,1},
                            {1,8,8,1,8,8,1,8,8,8,1,8,8,8,1,8,8,8,8,8,8,8,8,8,0,8,8,8,1,8,8,8,8,1,8,8,1,8,8,1},
                            {1,8,8,1,8,8,1,8,8,8,1,8,8,8,1,8,8,0,4,4,4,0,8,8,1,8,8,8,1,8,8,8,8,0,4,4,0,8,8,1},
                            {1,8,8,0,8,8,1,8,8,8,1,8,8,8,1,8,8,8,8,1,8,8,8,8,1,8,8,8,0,4,4,4,4,4,0,8,8,8,8,1},
                            {1,8,8,8,8,8,1,8,8,8,1,8,8,8,0,8,8,8,8,1,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,15},
                            {1,0,4,0,8,8,0,8,8,0,0,0,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,0,8,8,0,8,8,8,0,8,8,8,8,8},
                            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,15},
                         };
                    break;
                case 2:
                    wallsPlan = new int[,]
                         {
                            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,15},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,15},
                            {1,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,0,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {15,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {15,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                         };
                    break;
                case 3:
                    wallsPlan = new int[,]
                        {
                            {15,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {15,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,0,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,15},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,15},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,0,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                         };
                    break;
                case 4:
                    wallsPlan = new int[,]
                        {
                            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {15,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {15,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {1,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,1},
                            {0,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,0},
                         };
                    break;
            }
            //Update The Env -->Lots Of Ugly Code..Iknow ...Dont Shit Yur Pants Nd Dare Open Yur Fudjing Mouth :^<
            if (map != null)
            {
                if (Krypton != null)
                    Krypton.Hulls.RemoveRange(0, Krypton.Hulls.Count);
                MapLayer wallsLayer = new MapLayer(MapWidthInTiles, MapHightInTiles);

                for (int i = 0; i < wallsLayer.Height; i++)
                {
                    for (int j = 0; j < wallsLayer.Width; j++)
                    {
                        Tile tile = new Tile(wallsPlan[j, i], 1);
                        wallsLayer.SetTile(i, j, tile);

                        if (Krypton != null)
                        {
                            if (wallsPlan[j, i] != 8)
                            {
                                var hull = ShadowHull.CreateRectangle(new Vector2(TileSize, TileSize));
                                hull.Position.X = i * TileSize + TileSize / 2 - camera.Position.X;
                                hull.Position.Y = -(j * TileSize + TileSize / 2) + camera.Position.Y;
                                Krypton.Hulls.Add(hull);
                            }
                        }
                    }
                }
                map.UpdateLayer(wallsLayer, 1);
                //The Player New Position -> Related To The Current Stage 
                mainPlayer.SprtSheet.Position = StartEndPositions[Stage - 1][0] * TileSize;
                //Reset and Insert a New Kinds Of Enemies 
                SetUpEnemies();
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            if (InputHundler.KeyPressed(Keys.Escape))
            {
                StateManager.PopState();
            }
            if (InputHundler.KeyPressed(Keys.N))
            {
                Stage++;
            }
            if (InputHundler.KeyReleased(Keys.L))
            {
                if (light == Light.OFF)
                {
                    light = Light.ON;
                }
                else
                {
                    light = Light.OFF;
                }
            }
           
            if (mainPlayer.SprtSheet.Position.X > StartEndPositions[Stage - 1][1].X * TileSize && mainPlayer.SprtSheet.Position.Y > StartEndPositions[Stage - 1][1].Y * TileSize)
            {
                //Do The Transition                
                Stage++;
                StateManager.PushState(GameRef.transitionScreen);

            }
            //Update The MainPlayer Light Position
            (Krypton.Lights.First() as Light2D).Position = new Vector2(mainPlayer.SprtSheet.Position.X - camera.Position.X, -mainPlayer.SprtSheet.Position.Y + camera.Position.Y);
            //cameraPos = camera.Position;

            base.Update(gameTime);
        }
        private void drawMain(GameTime gameTime)
        {

            // Create a world view projection matrix to use with krypton
            Matrix world = Matrix.Identity;
            Matrix view = Matrix.CreateTranslation(new Vector3(GameRef.Graphics.PreferredBackBufferWidth / 2, -GameRef.Graphics.PreferredBackBufferHeight / 2, 0) * -1f);
            Matrix projection = Matrix.CreateOrthographic(GameRef.Graphics.PreferredBackBufferWidth, GameRef.Graphics.PreferredBackBufferHeight, 0, 1);

            //  (Krypton.Lights.First() as Light2D).Position = new Vector2(mainPlayer.sprtSheet.Position.X-camera.Position.X, -mainPlayer.sprtSheet.Position.Y+camera.Position.Y);
            // Assign the matrix and pre-render the lightmap.
            // Make sure not to change the position of any lights or shadow hulls after this call, as it won't take effect till the next frame!
            Krypton.Matrix = world * view * projection;
            Krypton.Bluriness = 0;
            Krypton.LightMapPrepare();

            GraphicsDevice.SetRenderTarget(mainScene);
            GraphicsDevice.Clear(Color.Black);
            GameRef.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transformation);
            map.Draw(GameRef.SpriteBatch, camera);
            GameRef.SpriteBatch.End();
            mainPlayer.Draw(gameTime, GameRef.SpriteBatch);
            enemy.Draw(gameTime, GameRef.SpriteBatch);
            foreach (var item in enemies)
            {
                item.Draw(gameTime,GameRef.SpriteBatch);
            }
            //player.Draw(gameTime, GameRef.SpriteBatch);           
            Krypton.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(null);
        }
        private void drawLightMask(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(lightMask);
            GraphicsDevice.Clear(Color.Black);
            GameRef.SpriteBatch.Begin();
            GameRef.SpriteBatch.Draw(blackSquare, new Vector2(0, 0), GameRef.ScreenRectangle, Color.White);
            GameRef.SpriteBatch.End();
            GameRef.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            GameRef.SpriteBatch.Draw(lightMskTex, mainPlayer.SprtSheet.Position - new Vector2(lightMskTex.Height / 2, lightMskTex.Height / 2) - camera.Position, Color.White);
            GameRef.SpriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
        }
        public override void Draw(GameTime gameTime)
        {
            drawMain(gameTime);
            drawLightMask(gameTime);
            if (light == Light.ON)
            {
                GameRef.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                GameRef.SpriteBatch.Draw(mainScene, new Vector2(0, 0), Color.White);
                GameRef.SpriteBatch.DrawString(font, "Camera pos : " + camera.Position.ToString(), new Vector2(0, 0), Color.White);
                GameRef.SpriteBatch.DrawString(font, "Player pos : " + mainPlayer.SprtSheet.Position.ToString(), new Vector2(0, 30), Color.White);
                GameRef.SpriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                GameRef.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                lightingEffect.Parameters["lightMask"].SetValue(lightMask);
                lightingEffect.CurrentTechnique.Passes[0].Apply();
                GameRef.SpriteBatch.Draw(mainScene, new Vector2(0, 0), Color.White);
                GameRef.SpriteBatch.End();
            }
            base.Draw(gameTime);

        }
    }
}
