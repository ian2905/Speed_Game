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
    public class Standing : PlayerState
    {
        public void Update(Player p, GameTime gameTime, BoundingRectangle[] platforms)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.A))
            {
                p.state = (PlayerState)Player.walkState;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                p.state = Player.jumpState;
                p.velocity.X -= Player.JUMP_ACCELERATION;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                p.sliding = true;
            }
            if (p.sliding == true && !keyboardState.IsKeyDown(Keys.S))
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

            if (p.velocity.X > 0)
            {
                p.velocity.X -= Player.FRICTION;
            }
            if (p.velocity.X < 0)
            {
                p.velocity.X += Player.FRICTION;
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
                p.frames[3].Draw(spriteBatch, p.walkingDraw, Color.White, 0, p.velocity, s, 0);
            }
            else
            {
                p.frames[0].Draw(spriteBatch, p.walkingDraw, Color.White, 0, p.velocity, s, 0);
            }

        }

        private void ManageCollisions(Player p, BoundingRectangle[] platforms)
        {
            foreach (BoundingRectangle plat in platforms)
            {
                if (p.bounds.CollidesWith(plat))
                {
                    p.bounds.Y = plat.Y - p.bounds.Height;
                }
            }
        }
    }
}
