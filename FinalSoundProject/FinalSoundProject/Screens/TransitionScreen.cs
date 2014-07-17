using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalSoundProject.Screens
{
    public class TransitionScreen : GameStateBase
    {

        #region Properties
        public int Transition { get; set; }
        #endregion
        #region Fields
        private SpriteFont _font;
        float timer = 0f;
        float interval = 4000;
        //Transitions        
        private Texture2D _transitionSprite;
        private Texture2D _gameOverSprite;
        private Texture2D _winningSprite;
        Song bgSound;
        #endregion

        public TransitionScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #region Method
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            _font = Content.Load<SpriteFont>(@"Assets\Font\SpriteFont1");
            _transitionSprite = Content.Load<Texture2D>(@"Assets\Bg\Transition\TransitionSprite");
            _gameOverSprite = Content.Load<Texture2D>(@"Assets\Bg\Transition\GameOverSprite");
            _winningSprite = Content.Load<Texture2D>(@"Assets\Bg\Transition\WinningSprite");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;        
                    if (timer > interval)
                    {
                        timer = 0f;                        
                        StateManager.PopState();
                        if (GameRef.mainScreen.Stage == 0 || GameRef.mainScreen.Stage == 5)
                        {
                            GameRef.mainScreen = new MainScreen(GameRef, StateManager);
                            StateManager.PopState();  //In Order To Go To The Main Menu Screen 
                        }                                                    
                    }            
            //if (InputHundler.KeyReleased(Keys.Enter))
            //{
            //    player.Stop();
            //}
            //if (player.State == MediaState.Stopped)
            //{
            //    StateManager.ChangeState(GameRef.menuScreen);
            //}
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            switch (GameRef.mainScreen.Stage)
            {
                case 0:
                    //The GameOver Sprite 
                    GameRef.SpriteBatch.Draw(_gameOverSprite,GameRef.ScreenRectangle,Color.White);
                    GameRef.SpriteBatch.DrawString(_font, "Game Over.. Shmuck :{", new Vector2(GameRef.ScreenRectangle.Width / 2 - 200, GameRef.ScreenRectangle.Height / 2), Color.White);                          
                    break;
                case 5:
                    //The Winning Sprite 
                    GameRef.SpriteBatch.Draw(_winningSprite, GameRef.ScreenRectangle, Color.White);
                     GameRef.SpriteBatch.DrawString(_font, "Congratulation Champ You Made It :}", new Vector2(GameRef.ScreenRectangle.Width/2-200,GameRef.ScreenRectangle.Height/2),Color.White);                          
                    break;
                default:
                     GameRef.SpriteBatch.Draw(_transitionSprite,GameRef.ScreenRectangle,Color.White);
                     GameRef.SpriteBatch.DrawString(_font, "Stage : " + GameRef.mainScreen.Stage, new Vector2(GameRef.ScreenRectangle.Width/2,GameRef.ScreenRectangle.Height/2),Color.White);                          

                    break;

            }
                       GameRef.SpriteBatch.End();
        }
        #endregion    }
    }
}
