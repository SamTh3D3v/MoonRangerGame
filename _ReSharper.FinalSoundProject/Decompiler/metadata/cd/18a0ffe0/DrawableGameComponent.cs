// Type: Microsoft.Xna.Framework.DrawableGameComponent
// Assembly: Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553
// Assembly location: C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.Game.dll

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Xna.Framework
{
  /// <summary>
  /// A game component that is notified when it needs to draw itself.
  /// </summary>
  public class DrawableGameComponent : GameComponent, IDrawable
  {
    /// <summary>
    /// Creates a new instance of DrawableGameComponent.
    /// </summary>
    /// <param name="game">The Game that the game component should be attached to.</param>
    public DrawableGameComponent(Game game);
    /// <summary>
    /// Initializes the component. Override this method to load any non-graphics resources and query for any required services.
    /// </summary>
    public override void Initialize();
    /// <summary>
    /// Releases the unmanaged resources used by the DrawableGameComponent and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing);
    /// <summary>
    /// Called when the DrawableGameComponent needs to be drawn. Override this method with component-specific drawing code. Reference page contains links to related conceptual articles.
    /// </summary>
    /// <param name="gameTime">Time passed since the last call to Draw.</param>
    public virtual void Draw(GameTime gameTime);
    /// <summary>
    /// Called when graphics resources need to be loaded. Override this method to load any component-specific graphics resources.
    /// </summary>
    protected virtual void LoadContent();
    /// <summary>
    /// Called when graphics resources need to be unloaded. Override this method to unload any component-specific graphics resources.
    /// </summary>
    protected virtual void UnloadContent();
    /// <summary>
    /// Called when the DrawOrder property changes. Raises the DrawOrderChanged event.
    /// </summary>
    /// <param name="sender">The DrawableGameComponent.</param><param name="args">Arguments to the DrawOrderChanged event.</param>
    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers")]
    protected virtual void OnDrawOrderChanged(object sender, EventArgs args);
    /// <summary>
    /// Called when the Visible property changes. Raises the VisibleChanged event.
    /// </summary>
    /// <param name="sender">The DrawableGameComponent.</param><param name="args">Arguments to the VisibleChanged event.</param>
    [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers")]
    protected virtual void OnVisibleChanged(object sender, EventArgs args);
    /// <summary>
    /// Indicates whether Draw should be called.
    /// </summary>
    public bool Visible { get; set; }
    /// <summary>
    /// Order in which the component should be drawn, relative to other components that are in the same GameComponentCollection.  Reference page contains code sample.
    /// </summary>
    public int DrawOrder { get; set; }
    /// <summary>
    /// The GraphicsDevice the DrawableGameComponent is associated with.
    /// </summary>
    public GraphicsDevice GraphicsDevice { get; }
    /// <summary>
    /// Raised when the Visible property changes.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> VisibleChanged;
    /// <summary>
    /// Raised when the DrawOrder property changes.
    /// </summary>
    /// <param name=""/>
    public event EventHandler<EventArgs> DrawOrderChanged;
  }
}
