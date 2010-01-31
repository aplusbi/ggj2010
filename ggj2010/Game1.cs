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
        public enum State { INTRO, RULES, GAME, SCORE, TITLE };
        State m_state = State.TITLE;
        GraphicsDeviceManager graphics;
        Viewport viewport;
        SpriteBatch spriteBatch;
        SoundEffect soundEffect;
        string soundName = "kaboom2";
        TileMap map;
        //Test player = new Test();
        Player[] players;
        Screens screens;
        int[] m_score = {0, 0, 0, 0};
        string[] m_levels = {@"Content\map1.txt", @"Content\map2.txt", @"Content\map3.txt", @"Content\map4.txt"};
        int m_currentlevel = 0;
        Random rng = new Random();
        bool spacebar = false;
        Timer m_leveltime;
        double m_delaytime;

        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screens = new Screens();
            m_delaytime = 0.0;
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

            //audioEngine = new AudioEngine(@"Content/Audio/Win/GGJ_2010.xgs");
            //waveBank = new WaveBank(audioEngine, @"Content/Audio/Win/Wave Bank.xwb");
            //soundBank = new SoundBank(audioEngine, @"Content/Audio/Win/Sound Bank.xsb");

            base.Initialize();

            ContentManager contentManager = new ContentManager(this.Services, @"Content\");
            soundEffect = contentManager.Load<SoundEffect>(soundName);
            //soundEffect.Play();
            //soundBank.PlayCue("test_cue"); 
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screens.LoadContent(Content);
            //LoadLevel(m_currentlevel);
            // TODO: use this.Content to load your game content here
        }
        public void LoadLevel(int level)
        {
            players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                players[i] = new Player(rng);
            }
            TilePair[] tiles = {
                new TilePair("blacksquare16x16", TileMap.Tile.TileType.NONE),
                new TilePair("whitesquare16x16", TileMap.Tile.TileType.SOLID),
                new TilePair("laddersquare16x16", TileMap.Tile.TileType.LADDER), };
            map = new TileMap();
            map.LoadTiles(Content, tiles);
            map.LoadContent(Content, m_levels[level]);

            PlayerIndex[] indices = { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };
            Shuffle(indices, 4);
            for (int i = 0; i < 4; i++)
            {
                players[i].LoadContent(Content, "character_frames", 64, i, indices[i]);
            }
            m_leveltime = new Timer(60);
            m_leveltime.LoadContent(Content, "Arial");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Three).Buttons.Back == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Four).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            else if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Three).Buttons.Start == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Four).Buttons.Start == ButtonState.Pressed)
            {
                m_delaytime = 0;
                m_state = State.TITLE;
            }
            switch(m_state)
            {
                case State.TITLE:
                    if ((m_delaytime > 1) && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed))
                    {
                        m_state = State.INTRO;
                        m_delaytime = 0.0;
                    }
                    m_delaytime += gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case State.INTRO:
                    if ((m_delaytime > 1) && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed))
                    {
                        m_state = State.RULES;
                        m_delaytime = 0.0;
                    }
                    m_delaytime += gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case State.RULES:
                    if ((m_delaytime > 3) && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed))
                    {
                        //string output = "Press A to continue";
                        //Vector2 outputPos = new Vector2(330, 630);
                        //screens.DrawText(spriteBatch, output, outputPos, 1.0f);
                        m_state = State.GAME;
                        m_delaytime = 0.0;
                        LoadLevel(m_currentlevel);
                    }
                    m_delaytime += gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case State.SCORE:
                    if ((m_delaytime > 5) && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Three).Buttons.A == ButtonState.Pressed
                        || GamePad.GetState(PlayerIndex.Four).Buttons.A == ButtonState.Pressed))
                    {
                        m_state = State.INTRO;
                        m_delaytime = 0.0;
                    }
                    m_delaytime += gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case State.GAME:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        spacebar = true;
                    if (spacebar && Keyboard.GetState().IsKeyUp(Keys.Space) || m_leveltime.GetTime() < 0)
                    {
                        spacebar = false;
                        if (++m_currentlevel >= m_levels.Count())
                            m_currentlevel = 0;
                        m_state = State.SCORE;
                        //LoadLevel(m_currentlevel); 
                    }
                    m_leveltime.Update(gameTime);

                    for (int i = 0; i < 4; i++)
                    {
                        players[i].Update(gameTime, this.map, Content);

                        // bullet collisions
                        LinkedList<Bullet> bullets = players[i].GetBullets();
                        foreach (Bullet b in bullets)
                        {
                            foreach (Player p in players)
                            {
                                if (b.m_player != p.m_index && b.Colliding(p))
                                {
                                    if (p.Shot())
                                    {
                                        //m_score[(int)p.m_index]--;
                                        if (b.GetTeam() == p.GetTeam())
                                            m_score[(int)b.m_player]--;
                                        else
                                            m_score[(int)b.m_player]++;
                                    }
                                    b.Remove();
                                }
                            }
                        }
                    }
                    break;
            }
            screens.Update(gameTime, m_state);
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
            if (m_state == State.GAME)
            {
                map.Draw(spriteBatch);
                m_leveltime.Draw(spriteBatch);
                for (int i = 0; i < 4; i++)
                {
                    players[i].Draw(spriteBatch);
                }
            }
            //player.Draw(spriteBatch);
            screens.Draw(spriteBatch, m_score);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public void Shuffle<T>(T[] array, int n)
        {
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
