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
        public static int BLOCK_SIZE = 21;

        Sprite frames;

        public BoundingRectangle bounds;
        Orentation orentation;
        Vector2 velocity;
        int blockCount;

        public Platform(Orentation orentation, int blockCount, Vector2 origin)
        {
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
            frames = spriteSheet[123];
            frames.sourceOffset(new Vector2(1, 1));
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if VISUAL_DEBUG
            VisualDebugging.DrawRectangle(spriteBatch, bounds, Color.Red);
#endif


            //Console.WriteLine($"{bounds.X} {bounds.Y}");

            Vector2 temp = new Vector2(bounds.X, bounds.Y);
            if (orentation == Orentation.Flat)
            {
                for (int i = 0; i < blockCount; i++)
                {
                    frames.Draw(spriteBatch, temp, Color.White);
                    temp.X += BLOCK_SIZE;
                }
            }
            else
            {
                for(int i = 0; i < blockCount; i++)
                {
                    frames.Draw(spriteBatch, temp, Color.White);
                    temp.Y += BLOCK_SIZE;
                }
            }
        }
    }
}
