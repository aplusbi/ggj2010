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
/*
    public class Test : SimpleCollidable
    {
        Texture2D m_texture;
        Vector2 m_vel;
        Vector2 m_dir = new Vector2(0, 0);
        Vector2 m_pos;
        Vector2 m_gravity = new Vector2(0.0f, 120.0f);
        double m_speed;
        public void LoadContent(ContentManager theContent, string assetName)
        {
            m_texture = theContent.Load<Texture2D>(assetName);
            m_pos = new Vector2(50, 5);
            m_rect = new floatRectangle();
            m_rect.X = 0;
            m_rect.Y = 0;
            m_rect.Width = m_texture.Width;
            m_rect.Height = m_texture.Height;
            m_speed = 120.0;        }
        public void Update(GameTime gameTime, TileMap map)
        {
            m_rect.X = m_pos.X;
            m_rect.Y = m_pos.Y;
            int ladder_x = 0, ladder_y = 0;

            bool onLadder = map.OnLadder(this, out ladder_x, out ladder_y);

            this.m_dir.X = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            if (onLadder)
            {
                this.m_dir.Y = -GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                if (Math.Abs(m_dir.Y) > 0.2f)
                    m_pos.X = (float)ladder_x;
            }
            else
                m_dir.Y = 0;
            if(m_dir.LengthSquared() > 0)
                m_dir.Normalize();
            m_vec.X = m_dir.X * (float)(m_speed * gameTime.ElapsedGameTime.TotalSeconds);
            m_vec.Y = m_dir.Y * (float)(m_speed * gameTime.ElapsedGameTime.TotalSeconds);
            if(!onLadder)
            {
                m_vec += m_gravity * (float)(gameTime.ElapsedGameTime.TotalSeconds);
            }
            m_vec = map.Collide(this);

            if (m_vec.Y > 0)
                m_vec.X = 0;
            m_pos.X += m_vec.X;
            m_pos.Y += m_vec.Y;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_pos, Color.Green);
        }
    }
 */
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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                players[i] = new Player();
            }
//            players[0] = new Player();
//            players[1] = new Player();
//            players[2] = new Player();
//            players[3] = new Player();
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

            base.Initialize();
            ContentManager contentManager = new ContentManager(this.Services, @"Content\");
            soundEffect = contentManager.Load<SoundEffect>(soundName);
            //soundEffect.Play();
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
            map.LoadContent(Content, @"Content\map1.txt");

            players[0].LoadContent(Content, "character_frames", 64);
            players[1].LoadContent(Content, "character_frames", 64);
            players[2].LoadContent(Content, "character_frames", 64);
            players[3].LoadContent(Content, "character_frames", 64);

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


            // TODO: Add your update logic here
            players[0].Update(gameTime, this.map);
            //player.Update(gameTime, this.map);
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
            players[0].Draw(spriteBatch);
            //player.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
