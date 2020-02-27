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
    public enum Facing
    {
        Right,
        Left
    }

    public class Player
    {
        public static float FRICTION = (float).5;
        public static float GRAVITY = (float)1;
        public static float ACCELERATION = (float).05;
        public static float JUMP_ACCELERATION = (float).5;
        public static int SPEEDCAP = 40;
        public static int ANIMATION_FRAME_RATE = 124;
        public static Vector2 WALKING_SIZE = new Vector2(36, 44); //18, 22
        public static Vector2 SLIDING_SIZE = new Vector2(36, 32); //18, 16

        public static Standing standState = new Standing();
        public static Walking walkState = new Walking();
        public static Jumping jumpState = new Jumping();

        public Game game;
        public Sprite[] frames = new Sprite[11];


        public BoundingRectangle bounds;
        public Vector2 positionVector = new Vector2(0, 6);
        public Vector2 velocity = new Vector2(0,0);

        public PlayerState state = new Standing();
        public Facing orentation = Facing.Right;

        public TimeSpan animationTimer = new TimeSpan();
        public int frame = 0;

        public bool sliding = false;

        public Rectangle walkingDraw => new Rectangle((int)bounds.X, (int)bounds.Y, (int)(WALKING_SIZE.X), (int)(WALKING_SIZE.Y));
        public Rectangle slidingDraw => new Rectangle((int)bounds.X, (int)bounds.Y, (int)(SLIDING_SIZE.X), (int)(SLIDING_SIZE.Y));

        public Player(Game game, Vector2 position)
        {
            this.game = game;
            bounds = new BoundingRectangle(position.X, position.Y, WALKING_SIZE.X, WALKING_SIZE.Y);
        }

        public void LoadContent(SpriteSheet spriteSheet)
        {
            for (int i = 79; i <= 89; i++)
            {
                frames[i - 79] = spriteSheet[i];
            }
        }

        public void Update(GameTime gameTime, BoundingRectangle[] platforms)
        {
            PlayerState temp = state;
            state.Update(this, gameTime, platforms);
            if(state != temp)
            {
                state.Entry(GameTime gameTime);
            }
            
            //Console.WriteLine(state);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if VISUAL_DEBUG
            VisualDebugging.DrawRectangle(spriteBatch, bounds, Color.Red);
#endif
            state.Draw(this, spriteBatch);
        }
    }
}
