using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FinalSoundProject.Screens;
using GameLibrary;
using GameLibrary.TileEngine;
using Krypton;
using Krypton.Lights;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalSoundProject
{
    public class Player:DrawableGameComponent
    {
        #region Consts
        private const float DefaultEnergyValue = 100f;
        #endregion
        #region Properties
        public Point Size { get; set; }
        public SpriteSheet SprtSheet { get; set; }
        public Light2D EnergyLight;
        public float Energy
        {
            get
            {
                return _energy;
            }
            set
            {
                if (_energy == value)
                    return;               
                _energy = value;
                EnergyLight.Range = _energy;
                if (_energy == 0)
                {
                    _gameRef.mainScreen.Stage = _gameRef.mainScreen.Stage; //EviiilCaniival Technique 3:^)>
                    //A Screen Transition -->Start Over
                }
               
            }
        }

        public float Speed { get; set; }
        #endregion
        #region Fields 
        private readonly Camera _camera;
        protected readonly Game1 _gameRef;
        private float _energy;     
        #endregion
        #region Methods
        public Player(Game game, Texture2D PlyerText, Point size, Vector2 position, Camera cam, Dictionary<Moves, Vector3> _movs, Moves initMov, int offset, float speed,float energy=DefaultEnergyValue) : base(game)
        {
            _gameRef = (Game1)game;
            _camera = cam;
            Size = size;
            Speed = speed;
            _energy = energy;
            SprtSheet = new SpriteSheet(PlyerText, position, size, _movs, initMov, offset, _camera);
            CreateLight();
        }
        public override void Update(GameTime gameTime)
        {
            bool left = false, right = false, up = false, down = false; //I am Too Lazy To Think Of a More Sofisticated Solution :p
            SprtSheet.Update(gameTime); //Update The Move
            if (this.GetType() == typeof(Player))  //Due To Laziness :p
            {
            Vector2 oldPos = SprtSheet.Position; //Update The Position If Posible  
            if (InputHundler.LeftMouseDown())
            {
                Energy += 1f;
            }
            if (InputHundler.RightMouseDown())
            {
                Energy -= 1f;
            }
            if (InputHundler.KeyDown(Keys.Left))
            {
                SprtSheet.actualMove = Moves.Left;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.X -= Speed;
                left = true;

            }
            else if (InputHundler.KeyDown(Keys.Right))
            {
                SprtSheet.actualMove = Moves.Right;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.X += Speed;
                right = true;

            }
            if (InputHundler.KeyDown(Keys.Up))
            {
                SprtSheet.actualMove = Moves.ForWard;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.Y -= Speed;
                up = true;
            }
            else if
                (InputHundler.KeyDown(Keys.Down))
            {
                SprtSheet.actualMove = Moves.BackWard;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.Y += Speed;
                down = true;
            }
            if (left && down || right && up) SprtSheet.Rotation = MathHelper.PiOver4;
            else if (left && up || right && down) SprtSheet.Rotation = -MathHelper.PiOver4;           
            else SprtSheet.Rotation = 0f;
            //collision with walls detection                         
            Rectangle PlayerPos = SprtSheet.GetRectPosition();
            //ZoomRect(ref PlayerPos, camera.Zoom);           
            for (int i = 0; i < _gameRef.mainScreen.MapWidthInTiles; i++)
            {
                for (int j = 0; j < _gameRef.mainScreen.MapHightInTiles; j++)
                {
                    if (MainScreen.wallsPlan[j, i] != 8)
                    {
                        Rectangle wallsRect = new Rectangle((int)(i * TileEngine.TileWidth - _camera.Position.X), (int)(j * TileEngine.TileWidth - _camera.Position.Y), TileEngine.TileWidth, TileEngine.TileWidth);
                        //  ZoomRect(ref wallsRect, camera.Zoom);
                        //Check The Collision 
                        if (wallsRect.Intersects(PlayerPos))
                        {
                            SprtSheet.Position = oldPos;
                            break;       // This Is 4 Optimisation :p
                            break;
                        }
                    }

                }

            }
            Vector2 camOldPOs = _camera.Position;            
                if (_camera.CamMod == Camera.CameraMode.Follow)
                {
                    _camera.CenterPlayer(this.SprtSheet);
                }

                _camera.Update(gameTime);

                if (camOldPOs != _camera.Position)
                {
                    for (int i = 0; i < _gameRef.mainScreen.Krypton.Hulls.Count; i++)
                    {
                        _gameRef.mainScreen.Krypton.Hulls[i].Position.X -= (_camera.Position.X - camOldPOs.X);
                        _gameRef.mainScreen.Krypton.Hulls[i].Position.Y += (_camera.Position.Y - camOldPOs.Y);
                    }
                }
            }
            EnergyLight.Position = new Vector2(SprtSheet.Position.X - _gameRef.mainScreen.camera.Position.X, -SprtSheet.Position.Y + _gameRef.mainScreen.camera.Position.Y);
        }
        public void CreateLight()
        {            
            EnergyLight = new Light2D()
            {
                Texture = LightTextureBuilder.CreatePointLight(_gameRef.GraphicsDevice, 512),
                Range = _energy,
                Color = (this.GetType() == typeof(Player)) ? new Color(255, 255, 255) : new Color(255, 0, 0),
                //Intensity = (float)(this.mRandom.NextDouble() * 0.25 + 0.75),
                Intensity = 40f,
                Angle = 0f,
                X = SprtSheet.Position.X,
                Y = SprtSheet.Position.Y,
            };
            EnergyLight.Fov = MathHelper.TwoPi;
            _gameRef.mainScreen.Krypton.Lights.Add(EnergyLight);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SprtSheet.Draw(spriteBatch);

        }
        #endregion
    }
}
