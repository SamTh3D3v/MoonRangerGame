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
    public class MenuItem
    {
        SpriteBatch spriteBatch;

        Texture2D texture;
        public bool Tap;
        public Vector2 Position;
        public Vector2 Origin;

        float timer = 0;

        const float MinScale = 0.8f;
        const float MaxScale = 1;
        float scale = 0.8f;
        public int Index = 0;
        public Rectangle Bound
        {
            get
            { return new Rectangle((int)(Position.X - Origin.X * scale),
                (int)(Position.Y - Origin.Y * scale), 
                (int)(texture.Width * scale), 
                (int)(texture.Height * scale)); }
        }
        public MenuItem(SpriteBatch _spritrBatch,Vector2 Location,Texture2D Texture)
        {
            Position = Location; 
            texture = Texture; 
            Origin = new Vector2(texture.Width / 2, texture.Height / 2); 
            spriteBatch = _spritrBatch;
        }
        public void Update(GameTime gameTime, Vector2 tapPosition)
        {    // if the tapped position within the text menu item bound, 
            // set Tap to true and trigger the OnTap event  
            Tap = Bound.Contains((int)tapPosition.X,      
                (int)tapPosition.Y);   
            // Accumulate the game elapsed time  
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        } 
        public void Draw(GameTime gameTime) {       
            // Draw the text menu item   
            if (Tap)    {             
                // if tap gesture is valid, gradually scale to    
                // MaxScale in        
                if (scale <= MaxScale && timer > 200)      
                {          
                    scale += 0.1f;    
                }     
                spriteBatch.Draw(texture, Position, null, Color.Red,     
                    0f, Origin, scale, SpriteEffects.None, 0f);
                }   
            else  
                {        
                // If no valid tap, gradually restore scale to       
                // MinScale in every frame      
                if (scale > MinScale && timer > 200)     
                {           
                    scale -= 0.1f;   
                }
                spriteBatch.Draw(texture, Position, null, Color.White,   
                    0f, Origin, scale, SpriteEffects.None, 0f); 
            } }
    }
}
