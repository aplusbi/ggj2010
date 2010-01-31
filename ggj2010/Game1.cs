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
        SoundEffect soundEffect;
        string soundName = "kaboom";
        TileMap map = new TileMap();
        float vibration = 0.0f;
        //Test player = new Test();
        Player[] players;

        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                players[i] = new Player();
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            viewport = GraphicsDevice.Viewport;

            audioEngine = new AudioEngine(@"Content/Audio/GGJ_2010.xgs");
            waveBank = new WaveBank(audioEngine, @"Content/Audio/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content/Audio/Sound Bank.xsb");

            base.Initialize();

            ContentManager contentManager = new ContentManager(this.Services, @"Content\");
            soundEffect = contentManager.Load<SoundEffect>(soundName);
            //soundEffect.Play();
            soundBank.PlayCue("test_cue"); 
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
            //player.LoadContent(Content, "whitesquare16x16");

            TilePair[] tiles = {
                new TilePair("blacksquare16x16", TileMap.Tile.TileType.NONE),
                new TilePair("whitesquare16x16", TileMap.Tile.TileType.SOLID),
                new TilePair("laddersquare16x16", TileMap.Tile.TileType.LADDER), };
            map.LoadTiles(Content, tiles);
            map.LoadContent(Content, @"Content\map2.txt");

            for (int i = 0; i < 4; i++)
            {
                players[i].LoadContent(Content, "character_frames", 64);
            }
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released)
            //    vibration += 0.1f;
            //if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Released)
            //    vibration -= 0.1f;
            //if (vibration > 1.0f)
            //    vibration = 1.0f;
            //if (vibration < 0.0f)
            //    vibration = 0.0f;
            //GamePad.SetVibration(PlayerIndex.One, 0.0f, vibration);

            for (int i = 0; i < 4; i++)
            {
                players[i].Update(gameTime, this.map, i);
            }
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
            for (int i = 0; i < 4; i++)
            {
                players[i].Draw(spriteBatch);
            }
            //player.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
