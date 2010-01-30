using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using TilePair = ggj2010.Pair<string, ggj2010.TileMap.Tile.TileType>;

namespace ggj2010
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Viewport viewport;
        SpriteBatch spriteBatch;
        Texture2D spriteTexture;
        Texture2D spriteTexture2;
        Vector2 spritePosition;
        Vector2 spritePosition2;
        SoundEffect soundEffect;
        string soundName = "kaboom";
        TileMap map = new TileMap();
        float vibration = 0.0f;
        float vibDelta = 0.1f;

        public Game1()
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
            viewport = GraphicsDevice.Viewport;
            spritePosition = new Vector2(viewport.Width / 2, viewport.Height / 2);
            spritePosition2 = new Vector2(viewport.Width / 2, viewport.Height / 2);
            spritePosition.X -= 64;
            spritePosition2.X += 64;
            base.Initialize();
            ContentManager contentManager = new ContentManager(this.Services, @"Content\");
            soundEffect = contentManager.Load<SoundEffect>(soundName);
            soundEffect.Play();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteTexture = Content.Load<Texture2D>("blacksquare64x64");
            spriteTexture2 = Content.Load<Texture2D>("whitesquare64x64");

            TilePair[] tiles = {
                new TilePair("blacksquare64x64", TileMap.Tile.TileType.NONE),
                new TilePair("whitesquare64x64", TileMap.Tile.TileType.SOLID) };
            map.LoadTiles(Content, tiles);
            map.LoadContent(Content, @"Content\map.txt");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            vibration -= GamePad.GetState(PlayerIndex.One).Triggers.Left;
            vibration += GamePad.GetState(PlayerIndex.One).Triggers.Right;
            if (vibration > 1.0f)
                vibration = 1.0f;
            if (vibration < 0.0f)
                vibration = 0.0f;
            GamePad.SetVibration(PlayerIndex.One, 1.0f - vibration, vibration);


            // TODO: Add your update logic here
            spritePosition.X += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            spritePosition.Y -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;

            spritePosition2.X += GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
            spritePosition2.Y -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.Draw(spriteTexture, spritePosition, Color.White);
            spriteBatch.Draw(spriteTexture2, spritePosition2, Color.Red);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
