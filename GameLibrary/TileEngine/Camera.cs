using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.TileEngine
{
    public class Camera
    {
        public enum  CameraMode
        {
           Manual,Follow 
        }
        #region Fields
        CameraMode camMod;
         Vector2 _position;       
        float speed;
        float zoom;
        Rectangle viewportRectangle;
        

        #endregion
        #region Property
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (_position==value)
                return;
                _position = value;
                
            }
        }
        public CameraMode CamMod
        {
            get { return camMod; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = (float)MathHelper.Clamp(speed, 1f, 16f); }
        }
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        public Matrix Transformation
        {
            get { return Matrix.CreateScale(zoom) * Matrix.CreateTranslation(new Vector3(-Position, 0f)); }
        }
        public Rectangle ViewportRectangle
        {
            get { return new Rectangle(viewportRectangle.X, viewportRectangle.Y, viewportRectangle.Width, viewportRectangle.Height); }
        }
        #endregion
        #region Methods 
        public Camera(Rectangle viewportRect)
        {
            
            speed = 4f;
            zoom = 1f; 
            viewportRectangle = viewportRect;
            camMod = CameraMode.Follow;
        }
        public Camera(Rectangle viewportRect, Vector2 position)
        {
            speed = 4f; zoom = 1f; 
            viewportRectangle = viewportRect;
            Position = position;
            camMod = CameraMode.Follow
                ;
        }
        public void Update(GameTime gameTime)
        {
            Vector2 motion = Vector2.Zero;            
            //if (InputHundler.KeyReleased(Keys.M))
            //{
            //    if(camMod==CameraMode.Manual)camMod=CameraMode.Follow;
            //    else camMod=CameraMode.Manual;
            //}
            if (InputHundler.KeyReleased(Keys.PageUp)) 
                ZoomIn();
            else if (InputHundler.KeyReleased(Keys.PageDown))
                ZoomOut();
            if (camMod==CameraMode.Manual)
            {
                if (InputHundler.KeyDown(Keys.Left)) motion.X = -speed; else if (InputHundler.KeyDown(Keys.Right)) motion.X = speed;
                if (InputHundler.KeyDown(Keys.Up)) motion.Y = -speed; else if (InputHundler.KeyDown(Keys.Down)) motion.Y = speed;
                if (motion != Vector2.Zero)
                {
                    motion.Normalize();
                    _position += motion * speed;
                    AdjastCamera();
                }
            }
                  
        }
        public void CenterPlayer(SpriteSheet player)
        {
            _position.X =( player.Position.X)*zoom - (viewportRectangle.Width / 2);
            _position.Y = (player.Position.Y)*zoom  - (viewportRectangle.Height / 2);
            AdjastCamera();
        }


       

        public void ZoomIn()
        {
            zoom += .25f;
            if (zoom > 2.5f) zoom = 2.5f;
            Vector2 newPosition = Position * zoom; 
            ResetPosition(newPosition);
        }
        public void ZoomOut()
        {
            zoom -= .25f;
            if (zoom < .5f) zoom = .5f;
            Vector2 newPosition = Position * zoom; 
            ResetPosition(newPosition);
        }
        private void ResetPosition(Vector2 newPosition)
        {
            _position.X = newPosition.X - viewportRectangle.Width / 2;
            _position.Y = newPosition.Y - viewportRectangle.Height / 2; 
            AdjastCamera();
        }

        private void AdjastCamera()
        {
            _position.X = MathHelper.Clamp(_position.X, 0, TileMap.WidthInPixels * zoom - viewportRectangle.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, TileMap.HeightInPixels * zoom - viewportRectangle.Height);
        }
        #endregion
    }
}
