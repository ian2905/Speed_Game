using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public enum BoxSideHit
    {
        Top,
        Right,
        Bottom,
        Left,
        Null
        
    }
    public struct BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public BoxSideHit CollidesWith(BoundingRectangle other)
        {
            if(!(this.X > other.X + other.Width
                  || this.X + this.Width < other.X
                  || this.Y > other.Y + other.Height
                  || this.Y + this.Height < other.Y))
            {
                float dTop = Math.Abs((this.Y + this.Height) - other.Y);
                float dRight = Math.Abs(this.X - (other.X + other.Width));
                float dBottom = Math.Abs(this.Y - (other.Y + other.Height));
                float dLeft = Math.Abs((this.X + this.Width) - other.X);

                if(dTop < Math.Min(dRight, Math.Min(dBottom, dLeft)))
                {
                    return BoxSideHit.Top;
                }
                else if(dRight < Math.Min(dBottom, dLeft))
                {
                    return BoxSideHit.Right;
                }
                else if (dBottom < dLeft)
                {
                    return BoxSideHit.Bottom;
                }
                else
                {
                    return BoxSideHit.Left;
                }
            }
            else
            {
                return BoxSideHit.Null;
            }
            /*
            return !(this.X > other.X + other.Width
                  || this.X + this.Width < other.X
                  || this.Y > other.Y + other.Height
                  || this.Y + this.Height < other.Y);
                  */
        }

        public bool CollidesWith(BoundingCircle other)
        {
            float nearestX = Math.Max(this.X, Math.Min(other.Center.X, this.X + this.Width));
            float nearestY = Math.Max(this.Y, Math.Min(other.Center.Y, this.Y + this.Height));
            return Math.Pow((other.Center.X - nearestX), 2) + Math.Pow((other.Center.Y - nearestY), 2) < Math.Pow(other.Radius, 2);
        }

        public static implicit operator Rectangle(BoundingRectangle br)
        {
            return new Rectangle(
                (int)br.X,
                (int)br.Y,
                (int)br.Width,
                (int)br.Height);
        }
    }
}
