using System;
using System.Collections.Generic;
using System.Linq;
using GameLibrary.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary
{
    public enum Moves
    {
        Right, Left, ForWard, BackWard, Shoot
    }
    public class SpriteSheet
    {
        Texture2D sprite;
        float timer = 0f;
        float interval = 100;
        int FrameCount = 2;
        int AnimationCount = 1;
        public int currentFrame;
        public int spriteWidth;
        public int spriteHeight;
        Rectangle sourceRect;
        public Vector2 origin;
        public float Rotation;
        int row;
        private Rectangle boundingRect;
        private int offset;
        public Dictionary<Moves, Vector3> moves;
        //A Move Start at moves[actualMove].X and Ends at moves[actualMove].Y
        //The moves[actualMove].Z Indicate The Line Where the The Move In The Sprite 
        //Is Located 
        //It Is Important -For The Moment At Least- That Each Move Is a Single Line ..
        public Moves actualMove;
        public Camera camera;

        #region Propertiese
        public Rectangle BoundingRect
        {
            get { return boundingRect; }
        }
        #endregion
        public Vector2 Position;


        public SpriteSheet(Texture2D _texture, Vector2 pos, Point size, Dictionary<Moves, Vector3> _movs, Moves iniMov, int _offset, Camera _camera)
        {
            offset = _offset;
            spriteWidth = size.X;
            spriteHeight = size.Y;
            camera = _camera;
            sprite = _texture;
            Position = pos;
            boundingRect = new Rectangle((int)Position.X, (int)Position.Y, spriteWidth, spriteHeight);
            moves = _movs;
            actualMove = iniMov;
            currentFrame = (int)moves[actualMove].X;
            row = (int)moves[actualMove].Z;
            Rotation = 0f;
        }

        // return a Rectangle Of The Bound Of The Player (By Removing The Offset And Shit)
        public Rectangle GetRectPosition()
        {
            return new Rectangle((int)(Position.X - (spriteWidth / 2) + offset * camera.Zoom - camera.Position.X), (int)(Position.Y - spriteWidth / 2 + offset * camera.Zoom - camera.Position.Y), (int)(spriteWidth - 2 * offset * camera.Zoom), (int)(spriteHeight - 2 * offset * camera.Zoom));
        }
        public Rectangle GetRectPositionWithCam()
        {
            return new Rectangle((int)(Position.X - (spriteWidth / 2) + offset * camera.Zoom ), (int)(Position.Y - spriteWidth / 2 + offset * camera.Zoom ), (int)(spriteWidth - 2 * offset * camera.Zoom), (int)(spriteHeight - 2 * offset * camera.Zoom));
        }
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }
            if (currentFrame == (int)moves[actualMove].Y + 1)
            {
                currentFrame = (int)moves[actualMove].X;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, (int)moves[actualMove].Z * spriteHeight, spriteWidth, spriteHeight);
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(sprite, Position - camera.Position, sourceRect, Color.White, Rotation, origin, camera.Zoom, SpriteEffects.None, 0);
            _spriteBatch.End();
        }



    }
}
