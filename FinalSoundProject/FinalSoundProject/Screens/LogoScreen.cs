using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameLibrary;


namespace FinalSoundProject.Screens
{
    public class LogoScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        private Video startVideo;
        private VideoPlayer player;
        Song bgSound;
        #endregion

        public LogoScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #region Method
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            player = new VideoPlayer();
            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\StartScreen");
            startVideo = Content.Load<Video>(@"Assets\Videos\Intro");
            player.Play(startVideo);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHundler.KeyReleased(Keys.Enter))
            {
                player.Stop();
            }
            if (player.State == MediaState.Stopped)
            {
                StateManager.ChangeState(GameRef.menuScreen);
            }


            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            if (player.State != MediaState.Stopped)
            {
                Texture2D texture = player.GetTexture();
                if (texture != null)
                {
                    GameRef.SpriteBatch.Draw(texture, new Rectangle(0, 0, GameRef.GraphicsDevice.Viewport.Width, GameRef.GraphicsDevice.Viewport.Height),
                        Color.White);
                }
            }
            //GameRef.SpriteBatch.Draw(backgroundImage,
            //                  GameRef.ScreenRectangle, Color.White);
            GameRef.SpriteBatch.End();
        }
        #endregion    }
    }
}
