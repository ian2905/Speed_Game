using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    public enum State
    {
        Jumping,
        Walking,
        Standing
    }

    public enum Facing
    {
        Right,
        Left
    }

    class Player
    {
        static float FRICTION = (float).5;
        static float GRAVITY = (float)1;
        static float ACCELERATION = (float).05;
        static float JUMP_ACCELERATION = (float)2;
        static int SPEEDCAP = 40;
        static int ANIMATION_FRAME_RATE = 124;
        static Vector2 WALKING_SIZE = new Vector2(17, 22);
        static Vector2 SLIDING_SIZE = new Vector2(18, 16);

        Game game;
        Sprite[] frames = new Sprite[11];

        BoundingRectangle bounds;
        Vector2 origin;
        Vector2 velocity = new Vector2(0,0);

        State movementState = State.Standing;
        Facing orentation = Facing.Right;

        public TimeSpan animationTimer = new TimeSpan();
        public int frame = 0;

        bool sliding = false;

        public Player(Game game, Vector2 position)
        {
            this.game = game;
            bounds = new BoundingRectangle(position.X, position.Y, WALKING_SIZE.X, WALKING_SIZE.Y);
            origin = position;
        }

        public void LoadContent(SpriteSheet spriteSheet)
        {
            for (int i = 49; i <= 59; i++)
            {
                frames[i - 49] = spriteSheet[i];
            }
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            
            if (velocity.X < Math.Abs(SPEEDCAP))
            {
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    velocity.X += ACCELERATION * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    velocity.X -= ACCELERATION * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }

            if (keyboardState.IsKeyDown(Keys.W) && movementState != State.Jumping)
            {
                velocity.Y -= ACCELERATION * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                sliding = true;
                bounds.Y += 5;
            }


            //Player Restrictions
            if (bounds.X < 0)
            {
                velocity.X = 0;
                bounds.X = 0;
            }
            if (bounds.X > game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                velocity.X = 0;
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }
            if (bounds.Y < 0)
            {
                velocity.Y = 0;
                bounds.Y = 0;
            }
            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                velocity.Y = 0;
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }

            //Physics
            if(movementState != State.Jumping)
            {
                if (velocity.X > 0)
                {
                    velocity.X -= FRICTION;
                }
                if (velocity.X < 0)
                {
                    velocity.X += FRICTION;
                }
            }
            else
            {
                velocity.Y += GRAVITY;
            }

            while (animationTimer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                //Console.WriteLine(frame);
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                animationTimer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }
            frame %= 1;
            if (Math.Abs(velocity.X) > 1 || Math.Abs(velocity.Y) > 1)
            {
                animationTimer += gameTime.ElapsedGameTime;
            }

            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
            {
                if (velocity.X >= 0)
                {
                    orentation = Facing.Right;
                }
                else
                {
                    orentation = Facing.Left;
                }
            }
            if (velocity.Y == 0)
            {
                if(velocity.X > 0)
                {
                    movementState = State.Walking;
                }
                else
                {
                    movementState = State.Standing;
                }
            }



            //Final Update
            bounds.Y += (int)velocity.Y;
            bounds.X += (int)velocity.X;
            if (sliding)
            {
                bounds.Width = SLIDING_SIZE.X;
                bounds.Height = SLIDING_SIZE.Y;
            }
            else
            {
                bounds.Width = WALKING_SIZE.X;
                bounds.Height = WALKING_SIZE.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Console.WriteLine(frames[0].Height);
            SpriteEffects s;
            if(orentation == Facing.Right)
            {
                s = SpriteEffects.None;
            }
            else
            {
                s = SpriteEffects.FlipHorizontally;
            }

            if (sliding)
            {
                frames[3].Draw(spriteBatch, new Rectangle((int)origin.X, (int)origin.Y, (int)WALKING_SIZE.X, (int)WALKING_SIZE.Y), Color.White, 0, velocity, s, 0);
            }
            else if (movementState == State.Walking)
            {
                frames[10 - frame].Draw(spriteBatch, new Rectangle((int)origin.X, (int)origin.Y, (int)WALKING_SIZE.X, (int)WALKING_SIZE.Y), Color.White, 0, velocity, s, 0);
            }
            else if (movementState == State.Standing)
            {
                frames[0].Draw(spriteBatch, new Rectangle((int)origin.X, (int)origin.Y, (int)WALKING_SIZE.X, (int)WALKING_SIZE.Y), Color.White, 0, velocity, s, 0);
            }
            else
            {
                if (velocity.Y < 0)
                {
                    frames[7].Draw(spriteBatch, new Rectangle((int)origin.X, (int)origin.Y, (int)WALKING_SIZE.X, (int)WALKING_SIZE.Y), Color.White, 0, velocity, s, 0);
                }
                else
                {
                    frames[4].Draw(spriteBatch, new Rectangle((int)origin.X, (int)origin.Y, (int)WALKING_SIZE.X, (int)WALKING_SIZE.Y), Color.White, 0, velocity, s, 0);
                }
            }
        }
    }
}
