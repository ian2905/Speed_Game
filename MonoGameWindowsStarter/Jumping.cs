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
    public class Jumping : PlayerState
    {
        public void Update(Player p, GameTime gameTime, List<Platform> platforms)
        {
            var keyboardState = Keyboard.GetState();

            if (p.velocity.X < Math.Abs(Player.SPEEDCAP))
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

            if (keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = true;
            }
            if (p.sliding == true && !keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = false;
            }

            p.velocity.Y += Player.GRAVITY;

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
            ManageCollisions(p, platforms);

        }

        private void Draw(Player p, SpriteBatch spriteBatch)
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
                p.frames[3].Draw(spriteBatch, p.slidingDraw, Color.White, 0, p.velocity, s, 0);
            }
            else if (p.velocity.Y < 0)
            {
                p.frames[7].Draw(spriteBatch, p.walkingDraw, Color.White, 0, p.velocity, s, 0);
            }
            else
            {
                p.frames[4].Draw(spriteBatch, p.walkingDraw, Color.White, 0, p.velocity, s, 0);
            }
        }

        private void ManageCollisions(Player p, List<Platform> platforms)
        {
            foreach (Platform plat in platforms)
            {
                if (p.bounds.CollidesWith(plat.bounds))
                {
                    p.bounds.Y = plat.bounds.Y - p.bounds.Height;
                    p.state = (PlayerState)Player.walkState;
                }
            }
        }
    }
}
