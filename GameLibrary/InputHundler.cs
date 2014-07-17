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


namespace GameLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputHundler : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields
         static KeyboardState keyboardState;
         static KeyboardState lastKeyboardState;

         static MouseState mouseState;
         static MouseState lastMouseState;

        #endregion
        #region Properties
         public static KeyboardState KeyboardState { get { return keyboardState; } }
         public static KeyboardState LastKeyboardState { get { return lastKeyboardState; } }

         public static MouseState MouseState { get { return mouseState; } }
         public static MouseState LastMouseState { get { return lastMouseState; } } 

        #endregion
        public InputHundler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState(); 
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }
        #region General Methods
        public  void Flush() { lastKeyboardState = keyboardState; }
        #endregion

        #region Keyboard Methods
        public static bool KeyReleased(Keys key) { return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key); }
        public static bool KeyPressed(Keys key) { return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key); }
        public static bool KeyDown(Keys key) { return keyboardState.IsKeyDown(key); }
        #endregion
        #region Mouse Methods
        public static bool LeftMouseReleased() { return mouseState.LeftButton==ButtonState.Released && lastMouseState.LeftButton==ButtonState.Pressed; }
        public static bool LeftMousePressed() { return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released; }
        public static bool LeftMouseDown() { return mouseState.LeftButton == ButtonState.Pressed; }
        public static bool RightMouseDown() { return mouseState.RightButton == ButtonState.Pressed; }
        #endregion
    }
}
