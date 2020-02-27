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
    public class Walking : PlayerState
    {
        public void Entry(GameTime gameTime)
        {

        }
        public void Update(Player p, GameTime gameTime, BoundingRectangle[] platforms)
        {
            var keyboardState = Keyboard.GetState();
            
            if (p.velocity.X < Math.Abs(Player.SPEEDCAP) && !p.sliding)
            {
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    p.velocity.X += Player.ACCELERATION * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    p.velocity.X -= Player.ACCELERATION * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                p.state = Player.jumpState;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = true;
            }
            if (p.sliding == true && !keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = false;
            }


            if (keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = true;
            }
            if(p.sliding == true && !keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = false;
            }

            //Player Restrictions
            if (p.bounds.X < 0)
            {
                p.velocity.X = 0;
                p.bounds.X = 0;
            }
            if (p.bounds.X > p.game.GraphicsDevice.Viewport.Width - p.bounds.Width)
            {
                p.velocity.X = 0;
                p.bounds.X = p.game.GraphicsDevice.Viewport.Width - p.bounds.Width;
            }
            if (p.bounds.Y < 0)
            {
                p.velocity.Y = 0;
                p.bounds.Y = 0;
            }
            if (p.bounds.Y > p.game.GraphicsDevice.Viewport.Height - p.bounds.Height)
            {
                p.velocity.Y = 0;
                p.bounds.Y = p.game.GraphicsDevice.Viewport.Height - p.bounds.Height;
            }

            p.velocity.Y += Player.GRAVITY;
            if (p.velocity.X > 0)
            {
                p.velocity.X -= Player.FRICTION;
            }
            if (p.velocity.X < 0)
            {
                p.velocity.X += Player.FRICTION;
            }

            while (p.animationTimer.TotalMilliseconds > Player.ANIMATION_FRAME_RATE)
            {
                //Console.WriteLine(frame);
                // increase by one frame
                p.frame++;
                // reduce the timer by one frame duration
                p.animationTimer -= new TimeSpan(0, 0, 0, 0, Player.ANIMATION_FRAME_RATE);
            }
            p.frame %= 1;
            if (Math.Abs(p.velocity.X) > 1 || Math.Abs(p.velocity.Y) > 1)
            {
                p.animationTimer += gameTime.ElapsedGameTime;
            }

            if (Math.Abs(p.velocity.X) > Math.Abs(p.velocity.Y))
            {
                if (p.velocity.X >= 0)
                {
                    p.orentation = Facing.Right;
                }
                else
                {
                    p.orentation = Facing.Left;
                }
            }

            if (p.sliding)
            {
                p.bounds.Width = Player.SLIDING_SIZE.X;
                p.bounds.Height = Player.SLIDING_SIZE.Y;
            }
            else
            {
                p.bounds.Width = Player.WALKING_SIZE.X;
                p.bounds.Height = Player.WALKING_SIZE.Y;
            }
            p.bounds.Y += (int)p.velocity.Y;
            p.bounds.X += (int)p.velocity.X;

            ManageCollisions(p, platforms);

        }

        public void Draw(Player p, SpriteBatch spriteBatch)
        {
            SpriteEffects s;
            if (p.orentation == Facing.Right)
            {
                s = SpriteEffects.None;
            }
            else
            {
                s = SpriteEffects.FlipHorizontally;
            }
            if (p.sliding)
            {
                p.frames[3].Draw(spriteBatch, p.walkingDraw, Color.White, 0, new Vector2(p.bounds.X, p.bounds.Y), s, 0);
            }
            else
            {
                p.frames[10 - p.frame].Draw(spriteBatch, p.walkingDraw, Color.White, 0, new Vector2(p.bounds.X, p.bounds.Y), s, 0);
            }
        }

        private void ManageCollisions(Player p, BoundingRectangle[] platforms)
        {
            foreach (BoundingRectangle plat in platforms)
            {
                if (p.bounds.CollidesWith(plat))
                {
                    p.bounds.Y = plat.Y - p.bounds.Height;
                    p.velocity.Y = 0;
                }
            }
        }
    }
}
