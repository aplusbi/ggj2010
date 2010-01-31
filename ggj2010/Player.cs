using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ggj2010
{
    class Player : SimpleCollidable
    {
        private Sprite m_sprite;

        Texture2D m_texture;

        Vector2 m_vel;
        Vector2 m_dir = new Vector2(0, 0);
        Vector2 m_pos;
        Vector2 m_gravity = new Vector2(0.0f, 120.0f);
        double m_speed;

        public Player()
        {
            m_sprite = new Sprite();
            m_sprite.AddAnimation(Animation.AnimationType.DYING, 1, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.RUNNING, 5, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.CLIMBING, 8, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.IDLING, 1, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.PANTING, 4, 0.3f, true);
            m_sprite.AddAnimation(Animation.AnimationType.SHOOTING, 2, 0.1f, true);

            //m_sprite.AddAnimation(Animation.AnimationType.IDLING, idleFrames, idleRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.RUNNING, movingFrames, movingRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.CLIMBING, movingFrames, movingRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.SHOOTING, shootingFrames, shootingRate, false);
            //m_sprite.AddAnimation(Animation.AnimationType.SPAWNING, spawningFrames, spawningRate, false);
            //m_sprite.AddAnimation(Animation.AnimationType.DYING, dyingFrames, dyingRate, false);
        }

        public void LoadContent(ContentManager theContent, string assetName, int squareSize)
        {
            m_sprite.LoadContent(theContent, assetName, squareSize);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
            m_texture = theContent.Load<Texture2D>(assetName);
            m_pos = new Vector2(5, 650);
            m_rect = new floatRectangle();
            m_rect.X = 0;
            m_rect.Y = 0;
            m_rect.Width = 16;
            m_rect.Height = 64;
            m_speed = 240.0f;
        }

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
                if (Math.Abs(m_dir.Y) > Math.Abs(m_dir.X))
                    m_pos.X = (float)ladder_x;
            }
            else
                m_dir.Y = 0;
            if (m_dir.LengthSquared() > 0)
                m_dir.Normalize();

            // stuck wall hack
            bool unstuck = false;
            if (!onLadder)
            {
                float grav = m_gravity.Y * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                m_vec = m_gravity * (float)(gameTime.ElapsedGameTime.TotalSeconds);
                m_vec = map.Collide(this);
                if (m_vec.Y == grav)
                    unstuck = true;
            }
            if(!unstuck)
                m_vec.X = m_dir.X * (float)(m_speed * gameTime.ElapsedGameTime.TotalSeconds);
            m_vec.Y = m_dir.Y * (float)(m_speed * gameTime.ElapsedGameTime.TotalSeconds);
            if (!onLadder)
            {
                m_vec += m_gravity * (float)(gameTime.ElapsedGameTime.TotalSeconds);
            }
            m_vec = map.Collide(this);

            m_pos.X += m_vec.X;
            m_pos.Y += m_vec.Y;

            m_sprite.Update(gameTime, m_pos +  new Vector2(8,0));

            m_sprite.PlayAnimation(Animation.AnimationType.RUNNING); // start in "idling animation" state

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            m_sprite.Draw(spriteBatch);
        }

//            idle = new Animation(new Vector2(0, 116), new Vector2(21, 24), 16, 9, 0, 0, 0);
//            up = new Animation(new Vector2(31,146), new Vector2(19,22), 0, 11, 0, 0, 2);
//            down = new Animation(new Vector2(31,116), new Vector2(19, 22), 0, 11, 0, 0, 2);
//            right = new Animation(new Vector2(180, 146), new Vector2(21, 22), 0, 11, 0, 0, 2);
//            left = new Animation(new Vector2(210, 116), new Vector2(22, 22), 0, 11, 0, 0, 2);

    }
}
