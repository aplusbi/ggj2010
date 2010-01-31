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
        Vector2 m_bulletDir = new Vector2(1.0f, 0);
        double m_bulletWait = 0.0f;
        Vector2 m_pos;
        Vector2[] m_origin = {
            new Vector2(80, 686),
            new Vector2(80, 16),
            new Vector2(944, 686),
            new Vector2(944, 16) };
        Vector2 m_gravity = new Vector2(0.0f, 120.0f);
        double m_xspeed;
        double m_yspeed;
        LinkedList<Bullet> m_bullets = new LinkedList<Bullet>();
        ContentManager m_content;
        bool flipped = false;
        Animation.AnimationType m_atype = Animation.AnimationType.IDLING;
        public PlayerIndex m_index;
        private int m_team;
        private int m_health;
        public double m_vibrate = 1.0f;

        public Player(Random rng)
        {
            m_sprite = new Sprite();
            m_sprite.AddAnimation(Animation.AnimationType.DYING, 1, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.RUNNING, 5, 0.05f, true);
            m_sprite.AddAnimation(Animation.AnimationType.CLIMBING, 8, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.IDLING, 1, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.PANTING, 4, 0.3f, true);
            m_sprite.AddAnimation(Animation.AnimationType.SHOOTING, 2, 0.1f, false);
            m_sprite.AddAnimation(Animation.AnimationType.SPAWNING, 2, 0.1f, true);
            m_team = rng.Next(2);
            m_health = 3;
        }
        public int GetTeam()
        {
            return m_team;
        }

        public void LoadContent(ContentManager theContent, string assetName, int squareSize, int pnum, PlayerIndex index)
        {
            m_content = theContent;
            m_sprite.LoadContent(theContent, assetName, squareSize);
            m_texture = theContent.Load<Texture2D>(assetName);
            m_rect = new floatRectangle();
            m_rect.X = 0;
            m_rect.Y = 0;
            m_rect.Width = 16;
            m_rect.Height = 64;
            m_xspeed = 240.0f;
            m_yspeed = 120.0f;
            m_index = index;
            m_pos =  m_origin[pnum];

            if(m_team == 0)
                GamePad.SetVibration(m_index, 0.5f, 0.0f);
        }
        public void Respawn()
        {
            m_atype = Animation.AnimationType.IDLING;
            m_pos = m_origin[new Random().Next(4)];
        }

        public void Update(GameTime gameTime, TileMap map)
        {
            m_vibrate -= gameTime.ElapsedGameTime.TotalSeconds;
            if (m_vibrate < 0)
                GamePad.SetVibration(m_index, 0, 0);
            m_rect.X = m_pos.X;
            m_rect.Y = m_pos.Y;
            int ladder_x = 0, ladder_y = 0;

            bool onLadder = map.OnLadder(this, out ladder_x, out ladder_y);

            this.m_dir.X = GamePad.GetState(m_index).ThumbSticks.Left.X;
            if (onLadder)
            {
                this.m_dir.Y = -GamePad.GetState(m_index).ThumbSticks.Left.Y;
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
                m_vec.X = m_dir.X * (float)(m_xspeed * gameTime.ElapsedGameTime.TotalSeconds);
            m_vec.Y = m_dir.Y * (float)(m_yspeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (!onLadder)
            {
                m_vec += m_gravity * (float)(gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                m_atype = Animation.AnimationType.CLIMBING;
            }
            m_vec = map.Collide(this);

            m_pos.X += m_vec.X;
            m_pos.Y += m_vec.Y;

            if (m_vec.X > 0)
            {
                m_bulletDir.X = 2.0f;
                flipped = false;
            }
            else if (m_vec.X < 0)
            {
                m_bulletDir.X = -2.0f;
                flipped = true;
            }

            if (m_atype != Animation.AnimationType.SHOOTING && m_atype != Animation.AnimationType.DYING)
            {
                if (Math.Abs(m_vec.X) > 0.20f && Math.Abs(m_vec.Y) < 0.20f)
                {
                    m_atype = Animation.AnimationType.RUNNING;
                }
                if (Math.Abs(m_vec.Y) > 0.20f && Math.Abs(m_vec.X) < 0.20f && onLadder == true)
                {
                    m_atype = Animation.AnimationType.CLIMBING;
                }
                if (Math.Abs(m_vec.X) < 0.20f && !onLadder)
                {
                    m_atype = Animation.AnimationType.IDLING;
                }
            }

            // go back to idle animation after SHOOTING animation is done
            if (m_atype == Animation.AnimationType.SHOOTING && m_sprite.IsCurrentAnimationDone() == true)
            {
                m_atype = Animation.AnimationType.IDLING;
            }

            // go back to idle animation after SPAWNING animation is done
            if (m_atype == Animation.AnimationType.SPAWNING && m_sprite.IsCurrentAnimationDone() == true)
            { 
                m_atype = Animation.AnimationType.IDLING; 
            }

            // if dying animation completed, trip flag
            if (m_atype == Animation.AnimationType.DYING && m_sprite.IsCurrentAnimationDone() == true)
            {
                //dyingAnimationIsDone = true;
            }

            m_sprite.Update(gameTime, m_pos +  new Vector2(8,0));
            //m_sprite.PlayAnimation(Animation.AnimationType.RUNNING); // start in "idling animation" state

            // bullets!
            m_bulletWait -= gameTime.ElapsedGameTime.TotalSeconds;
            if (m_bulletWait < 0) m_bulletWait = 0;
            if(GamePad.GetState(m_index).Buttons.A == ButtonState.Pressed
                && m_bullets.Count() < 5 && m_bulletWait <= 0)
            {
                m_atype = Animation.AnimationType.SHOOTING;
                m_bulletWait = 0.5f;
                 m_bullets.AddLast(new Bullet(m_content,
                    new floatRectangle(m_pos.X + 8, m_pos.Y + 32, 8, 8), 
                    m_bulletDir * 320.0f, 
                    m_index, m_team));
            }
            LinkedList<Bullet> garbage = new LinkedList<Bullet>();
            foreach (Bullet b in m_bullets)
            {
                if (!b.IsValid())
                    garbage.AddLast(b);
                else
                    b.Update(gameTime, map);
            }
            foreach (Bullet b in garbage)
            {
                m_bullets.Remove(b);
            }

            switch (m_atype)
            {
                case Animation.AnimationType.DYING: break;
                case Animation.AnimationType.RUNNING: m_sprite.PlayAnimation(Animation.AnimationType.RUNNING); break;
                case Animation.AnimationType.CLIMBING: m_sprite.PlayAnimation(Animation.AnimationType.CLIMBING); break;
                case Animation.AnimationType.IDLING: m_sprite.PlayAnimation(Animation.AnimationType.IDLING); break;
                case Animation.AnimationType.PANTING: m_sprite.PlayAnimation(Animation.AnimationType.PANTING); break;
                case Animation.AnimationType.SHOOTING: m_sprite.PlayAnimation(Animation.AnimationType.SHOOTING); break;
                case Animation.AnimationType.SPAWNING: break;
                default: break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            m_sprite.Draw(spriteBatch, flipped);
            foreach (Bullet b in m_bullets)
            {
                b.Draw(spriteBatch);
            }
        }
        public LinkedList<Bullet> GetBullets()
        {
            return m_bullets;
        }
        public bool Shot()
        {
            if (--m_health == 0)
            {
                Die();
                return true;
            }
            return false;
        }
        public void Die()
        {
            Respawn();
            m_health = 3;
            //m_atype = Animation.AnimationType.DYING;
        }
    }
}
