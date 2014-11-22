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
    public class AboutScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        #endregion
        public AboutScreen(Game game,GameStateManager manager)
            : base(game,manager)
        {
           
        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\AboutScreen");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHundler.KeyPressed(Keys.Escape))
            {
                StateManager.PopState();
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(backgroundImage,
                              GameRef.ScreenRectangle, Color.White);
            GameRef.SpriteBatch.End();
        }
    }
}
