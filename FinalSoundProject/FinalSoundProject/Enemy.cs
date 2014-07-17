using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using FinalSoundProject.Screens;
using GameLibrary;
using GameLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalSoundProject
{
    public class Enemy : Player
    {
        #region Fields

        private bool left = true, up = false, down = true, right=false; //I Know That Right is !Left -> So Just Don't Shit Your Pants Fu*ker -_-
        Random rnd = new Random();
        #endregion
        #region Properties
        public Rectangle ActiveArea { get; set; }
        #endregion
        public Enemy(Game game, Texture2D PlyerText, Point size, Vector2 position, Camera cam, Dictionary<GameLibrary.Moves, Vector3> _movs, GameLibrary.Moves initMv, int offset, float speed, Rectangle activeArea, float energy)
            : base(game, PlyerText, size, position, cam, _movs, initMv, offset, speed, energy)
        {
            ActiveArea = activeArea;
        }

        private void RangerInActiveArea()
        {
            // if (_gameRef.mainScreen.mainPlayer.SprtSheet.GetRectPosition().Intersects(ActiveArea))
            {
                //The Position IS GENERATED Using AI
                //}
                //else
                //{
                Rectangle rectTmp = SprtSheet.GetRectPositionWithCam();
                if (!SprtSheet.GetRectPositionWithCam().Intersects(ActiveArea))
                {
                    
                    left = rnd.Next(2) == 0 ? true:false;
                    up = rnd.Next(2) == 0 ? true : false;
                    right = rnd.Next(2) == 0 ? true : false;
                    down = rnd.Next(2) == 0 ? true : false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 oldPos = SprtSheet.Position; //Update The Position If Posible  

            //If the enemy is in the active area 
            //If the ranger entre its active are -> fllow ranger 
            //otherwise -> keep going until the enemy is out the active area 
            //          -> In This Case regenerate a new direction (random direction)
            RangerInActiveArea();
            if (InputHundler.LeftMouseDown())
            {
                //When Intersection  Suck Energy
                EnergyLight.Range += 1f;
            }
            //Change The KeyBord Input With The AI Mchanics 
            if (left)
            {
                SprtSheet.actualMove = Moves.Left;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.X -= Speed;


            }
            if(right)
            {
                SprtSheet.actualMove = Moves.Right;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.X += Speed;


            }
            if (up)
            {
                SprtSheet.actualMove = Moves.ForWard;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.Y -= Speed;

            }
            if(down)
            {
                SprtSheet.actualMove = Moves.BackWard;
                SprtSheet.currentFrame = (int)SprtSheet.moves[SprtSheet.actualMove].X;
                SprtSheet.Position.Y += Speed;
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
                        Rectangle wallsRect = new Rectangle((int)(i * TileEngine.TileWidth - _gameRef.mainScreen.camera.Position.X), (int)(j * TileEngine.TileWidth - _gameRef.mainScreen.camera.Position.Y), TileEngine.TileWidth, TileEngine.TileWidth);
                        //  ZoomRect(ref wallsRect, camera.Zoom);
                        //Check The Collision 
                        if (wallsRect.Intersects(PlayerPos))
                        {
                            SprtSheet.Position = oldPos;
                            left = rnd.Next(2) == 0 ? true : false;
                            up = rnd.Next(2) == 0 ? true : false;
                            right = rnd.Next(2) == 0 ? true : false;
                            down = rnd.Next(2) == 0 ? true : false;
                            break;       // This Is 4 Optimisation :p
                            break;
                        }
                    }

                }

            }

        }


    }
}
