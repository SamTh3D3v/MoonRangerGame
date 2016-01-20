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
    public class GameOverScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        private float timer;
        private float gameovertimer = 4000;
        #endregion
        public GameOverScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {

        }
        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\GameOverScreen");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHundler.KeyPressed(Keys.Escape) && InputHundler.KeyPressed(Keys.Space) && InputHundler.KeyPressed(Keys.Enter))
            {
                StateManager.PopState();
            }
            if (timer > gameovertimer)
            {
                timer = 0f;
                StateManager.PopState();
                StateManager.PopState();
                GameRef.mainScreen = new MainScreen(GameRef, GameRef.stateManager); //Dirty yes I know ...Get A Life Yu Sucker, Barka ma t3ess
                StateManager.PushState(GameRef.mainScreen);
                StateManager.PopState();
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
