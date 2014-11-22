using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FinalSoundProject.Screens
{
    public class GameStateBase : GameState
    {
        #region Fields
        protected Game1 GameRef;
        #endregion
        #region Properties
        #endregion

        public GameStateBase(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (Game1)game;  
        }

    }
}

