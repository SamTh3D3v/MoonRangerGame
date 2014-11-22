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
    public class GameState : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields and Properties
        List<GameComponent> childComponents;
        
        GameState tag;
        protected GameStateManager StateManager;

        public List<GameComponent> Components 
        {
            get { return childComponents; }
        }

        public GameState Tag
        {
            get { return tag; } 
        }

        #endregion


        public GameState(Game game, GameStateManager manager)
            : base(game)
        {
            StateManager = manager;
            childComponents = new List<GameComponent>();
            tag = this;
            
        }


        #region  Methods
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
        #endregion
        #region GameState Method
        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag)
                Show();
            else
                Hide();
        }
        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }
        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
        #endregion
    }
}
