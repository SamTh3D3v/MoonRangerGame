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
    public class PauseScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        #endregion
        public PauseScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\PauseScreen");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHundler.KeyPressed(Keys.Escape) && InputHundler.KeyPressed(Keys.Space) && InputHundler.KeyPressed(Keys.Enter))
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
