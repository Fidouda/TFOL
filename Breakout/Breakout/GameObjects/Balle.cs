﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

using System;


namespace Breakout
{
    class Balle
    {
        private Texture2D sprite;
        private Rectangle screenBound;
        private Vector2 direction;
        private float speed;
        private float maxSpeed = 400;
        private Vector2 position;
        private bool enable;
        private bool inCollision;

        public Balle(Texture2D sprite, Rectangle screenBound, Vector2 position)
        {
            this.position = position;
            this.sprite = sprite;
            this.screenBound = screenBound;
            direction = new Vector2(0, 1);
            speed = 150;
            enable = false;
            inCollision = false;
        }

        public float getPositionY()
        {
            return position.Y;
        }

        public float getPositionX()
        {
            return position.X;
        }

        public void setPositionX(float positionX)
        {
            position.X = positionX;
        }

        public bool getEnable()
        {
            return enable;
        }

        public void setEnable(bool enableballe)
        {
            enable = enableballe;
        }

        public void Update(KeyboardState state, GameTime gameTime, SoundEffectInstance soundEngineInstance)
        {
            
            if (!enable && state.IsKeyDown(Keys.Space))
                enable = true;

            if (enable) 
            {
                if (checkWallCollision())
                {
                    // La balle émet le son approprié
                    soundEngineInstance.Volume = 0.75f;
                    soundEngineInstance.Play();
                }
                position += direction * (float)(speed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            
        }

        private bool checkWallCollision()
        {
            bool hasCollided = false;

            if (position.X < 0 || (position.X + sprite.Width) > screenBound.Width)
            {
                direction.X *= -1;
                hasCollided = true;
            }

            if (position.Y < 0)
            {
                direction.Y *= -1;
                hasCollided = true;
            }

            return hasCollided;
        }

        public bool checkBrickCollision(Rectangle brick)
        {
            Rectangle ballLocation = new Rectangle((int)position.X, (int)position.Y,
               sprite.Width, sprite.Height);

            if (brick.Intersects(ballLocation) && !inCollision)
            {
                direction.Y *= -1;
                if (speed < maxSpeed)
                {
                    speed += 5;
                }
                return true;
            }
            return false;
        }

        public void checkPaddleCollision(Rectangle paddle, SoundEffectInstance soundEngineInstance)
        {
            Rectangle ballLocation = new Rectangle((int)position.X, (int)position.Y,
                sprite.Width, sprite.Height);

            if (paddle.Intersects(ballLocation) && !inCollision)
            {
                inCollision = true;

                // La balle émet le son approprié
                soundEngineInstance.Volume = 0.75f;
                soundEngineInstance.Play();

                //Collision avec le top de la palette
                if (ballLocation.Intersects(new Rectangle(paddle.X, paddle.Y, paddle.Width, 0)))
                {
                    direction.Y *= -1;
                    position.Y = paddle.Y - sprite.Height;
                    
                    float paddleCenter = paddle.X + (paddle.Width/2);
                    float ballCenter = position.X + sprite.Width/2;
                    direction.X = (ballCenter - paddleCenter) / (paddle.Width / 2);
                    direction = Vector2.Normalize(direction);
                }

            }
            else if(!paddle.Intersects(ballLocation))
                inCollision = false;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }
    }
}
