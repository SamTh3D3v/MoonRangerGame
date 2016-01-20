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
    public class MenuScreen : GameStateBase
    {
        #region Field
        Texture2D backgroundImage;
        SpriteFont font;
        Texture2D texImageMenuItem;
        List<MenuItem> Menu;
        int TotalMenuItems = 4;
        int index = 0;
        int currentIndex = -1;
        #endregion

        public MenuScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            // TODO: Construct any child components here
            Menu = new List<MenuItem>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        protected override void LoadContent()
        {

            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Assets\Bg\MenuScreen");
            texImageMenuItem = Content.Load<Texture2D>(@"Assets\Menu\MenuItem0");
            font = Content.Load<SpriteFont>(@"Assets\Font\BaseFont");

            int X = GameRef.GraphicsDevice.Viewport.Width/2 ;
            int Y = 140;
            for (int i = 0; i < TotalMenuItems; i++) {  
                MenuItem item = new MenuItem(GameRef.SpriteBatch ,
                    new Vector2(
                        X, Y + i * (texImageMenuItem.Height + 20)), Content.Load<Texture2D>(@"Assets\Menu\MenuItem"+i));
                item.Index = index++; 
                Menu.Add(item);
            }

            base.LoadContent();

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            currentIndex = -1;
            if (InputHundler.KeyPressed(Keys.Enter))
            {
                StateManager.PushState(GameRef.mainScreen);
            }
            Vector2 tapPosition = new Vector2();


            tapPosition.X = InputHundler.MouseState.X;
            tapPosition.Y = InputHundler.MouseState.Y;
                    
            foreach (MenuItem item in Menu)  
            {       
                item.Update(gameTime, tapPosition);
              if (item.Tap)       
              {          
                  currentIndex = item.Index;    
              }   
            }
            if (InputHundler.MouseState.LeftButton ==
                ButtonState.Pressed)
            {
               
                switch (currentIndex)
                {
                    case 0:
                        //Main Screen
                        StateManager.PushState(GameRef.mainScreen);
                        //StateManager.PushState(GameRef.mainScreen);
                        break;
                    case 1:
                        //Setting Screen 
                        StateManager.PushState(GameRef.settingScreen);
                        break;
                    case 2:
                        //The Credit Screen
                        StateManager.PushState(GameRef.aboutScreen);
                        break;
                    case 3:
                        //Must Unload The Dust :p
                        GameRef.Exit();
                        break;
                }
            }
          
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(backgroundImage,
                              GameRef.ScreenRectangle, Color.White);
            // Draw the Menu
            foreach (MenuItem item in Menu) { 
                item.Draw(gameTime); }
            // Draw the current index on the top-left of screen
            GameRef.SpriteBatch.DrawString(font, "Current Index: " +   
                currentIndex.ToString(), new Vector2(0, 0), Color.White);

            GameRef.SpriteBatch.End();
        }
    }
}
