using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter
{
    public enum Orentation
    {
        Flat,
        Tall
    }

    class Platform
    {
        static int BLOCK_SIZE = 21;

        Game game;
        Sprite[] frames = new Sprite[5];

        public BoundingRectangle bounds;
        Orentation orentation;
        Vector2 velocity;
        int blockCount;

        public Platform(Game game, Orentation orentation, int blockCount, Vector2 origin)
        {
            this.game = game;
            this.orentation = orentation;
            this.blockCount = blockCount;
            if(orentation == Orentation.Flat)
            {
                bounds = new BoundingRectangle(origin.X, origin.Y, BLOCK_SIZE * blockCount, BLOCK_SIZE);
            }
            else
            {
                bounds = new BoundingRectangle(origin.X, origin.Y, BLOCK_SIZE, BLOCK_SIZE * blockCount);
            }
        }

        public void LoadContent(SpriteSheet spriteSheet)
        {
            for (int i = 120; i <= 123; i++)
            {
                frames[i - 120] = spriteSheet[i];
                if(i == 123)
                {
                    frames[i - 119] = spriteSheet[i + 28];
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if VISUAL_DEBUG
            VisualDebugging.DrawRectangle(spriteBatch, bounds, Color.Red);
#endif
            


            if (orentation == Orentation.Flat)
            {
                BoundingRectangle temp = new BoundingRectangle(bounds.X, bounds.Y, bounds.Width - ((blockCount - 1) * BLOCK_SIZE), bounds.Height);

                if (blockCount == 1)
                {
                    frames[0].Draw(spriteBatch, temp, Color.White);
                }
                else
                {
                    frames[1].Draw(spriteBatch, temp, Color.White);
                    for(int i = blockCount - 2; i >= 0; i--)
                    {
                        temp.X += BLOCK_SIZE - 1;
                        frames[2].Draw(spriteBatch, temp, Color.White);
                    }
                    frames[3].Draw(spriteBatch, temp, Color.White);
                }
            }
            else
            {
                BoundingRectangle temp = new BoundingRectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height - ((blockCount - 1) * BLOCK_SIZE));
                if (blockCount == 1)
                {
                    frames[0].Draw(spriteBatch, temp, Color.White);
                }
                else
                {
                    frames[1].Draw(spriteBatch, temp, Color.White);
                    for (int i = blockCount - 2; i >= 0; i--)
                    {
                        temp.Y += BLOCK_SIZE - 1;
                        frames[4].Draw(spriteBatch, temp, Color.White);
                    }
                    frames[4].Draw(spriteBatch, temp, Color.White);
                }
            }


        }



    }
}
