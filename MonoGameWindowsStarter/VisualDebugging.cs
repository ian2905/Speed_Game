using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


#if VISUAL_DEBUG
namespace MonoGameWindowsStarter
{
    public static class VisualDebugging
    {
        static Texture2D pixel;

        /// <summary>
        /// Loads the necessary content for visual debugging
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public static void LoadContent(ContentManager content) 
        {
            pixel = content.Load<Texture2D>("OnePixel");
        }

        /// <summary>
        /// Draws a visual debugging rectangle in the specified color
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use</param>
        /// <param name="rectangle">The rectangle's bounds</param>
        /// <param name="color">The rectangle's color</param>
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(pixel, rectangle, color);
        }
    }
}
#endif