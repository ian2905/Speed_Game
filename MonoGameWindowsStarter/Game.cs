using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Random random;

        SpriteSheet spriteSheet;
        List<Platform> platforms;
        Player player;
        KeyboardState oldKeyboardState;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();


            random = new Random();
            platforms = new List<Platform>();

            base.Initialize();


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
#if VISUAL_DEBUG
            VisualDebugging.LoadContent(Content);
#endif
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            spriteFont = Content.Load<SpriteFont>("Font");
            spriteSheet = new SpriteSheet(Content.Load<Texture2D>("spritesheet"), 21, 21, 2, 2);
            
            player = new Player(graphics, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight/2 + 21));
            player.LoadContent(spriteSheet);

            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(150, 100)));
            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(300, 200)));
            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(450, 300)));
            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(600, 400)));
            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(750, 500)));
            platforms.Add(new Platform(Orentation.Flat, 5, new Vector2(900, 600)));
            platforms.Add(new Platform(Orentation.Flat, (graphics.PreferredBackBufferWidth/Platform.BLOCK_SIZE) + Platform.BLOCK_SIZE, new Vector2(0, graphics.PreferredBackBufferHeight - Platform.BLOCK_SIZE)));
            foreach (Platform platform in platforms)
            {
                platform.LoadContent(spriteSheet);
            }






            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            BoundingRectangle[] temp = new BoundingRectangle[platforms.Count];
            for(int i = 0; i < platforms.Count; i++)
            {
                temp[i] = platforms[i].bounds;
            }
            player.Update(gameTime, temp);

            // TODO: Add your update logic here

            oldKeyboardState = newKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            
            player.Draw(spriteBatch);
            foreach(Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //public List<Platform> InitilizePlatforms(SpriteSheet spriteSheet)
        //{
            //return [new Platform]
        //}
    }
}
