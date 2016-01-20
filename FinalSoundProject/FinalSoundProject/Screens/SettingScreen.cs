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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SettingScreen : GameStateBase
    {
        

        #region Field
        Texture2D backgroundImage;
        
        #endregion
        public SettingScreen(Game game,GameStateManager manager)
            : base(game,manager)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: Add your update code here
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\SettingScreen");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
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
