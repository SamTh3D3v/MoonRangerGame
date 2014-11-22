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
using FinalSoundProject.Screens;



namespace FinalSoundProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public SpriteBatch SpriteBatch;
        
        
        #region Game State Region

        public GameStateManager stateManager;
        public LogoScreen logoScreen;
        public MainScreen mainScreen;
        public TransitionScreen transitionScreen;
        public MenuScreen menuScreen;
        public AboutScreen aboutScreen;
        public SettingScreen settingScreen;
        #endregion
        #region Screen Field Region
        public const int screenWidth = 800;
        public const int screenHeight = 600;
        public readonly Rectangle ScreenRectangle;
        #endregion       
        public InputHundler inputeManager;
        public GraphicsDeviceManager Graphics;
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this); 
            Graphics.PreferredBackBufferWidth = screenWidth;
            Graphics.PreferredBackBufferHeight = screenHeight;
            //Graphics.IsFullScreen = true;
            ScreenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            Content.RootDirectory = "Content";
            inputeManager = new InputHundler(this);
            stateManager = new GameStateManager(this);
            logoScreen = new LogoScreen(this, stateManager);
            mainScreen = new MainScreen(this, stateManager);
            transitionScreen = new TransitionScreen(this, stateManager);
            menuScreen = new MenuScreen(this, stateManager);
            aboutScreen = new AboutScreen(this, stateManager);
            settingScreen=new SettingScreen(this, stateManager);
            Components.Add(inputeManager);  
            Components.Add(stateManager);
            stateManager.ChangeState(logoScreen);
            IsMouseVisible = true;

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
           
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
         

            base.Draw(gameTime);
        }
    }
}
