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
    public class Bullet: SimpleCollidable
    {
        private Texture2D m_sprite;
        private bool m_valid = true;
        public PlayerIndex m_player;
        private Vector2 m_vel;
        private int m_team;
        public Bullet(ContentManager theContent, floatRectangle r, Vector2 v, PlayerIndex player, int team)
        {
            m_player = player;
            m_rect = r;
            m_vel = v;
            m_team = team;
            m_vec = new Vector2();
            m_sprite = theContent.Load<Texture2D>("bullet");
        }
        public int GetTeam() { return m_team; }
        public void Update(GameTime gameTime, TileMap map)
        {
            bool hit;
            m_vec.X = m_vel.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 vel = map.Collide(this, out hit);
            if (hit || m_rect.X < 0 || m_rect.X > 1024)
            {
                m_valid = false;
            }
            else
            {
                m_rect.X += m_vel.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public bool IsValid()
        {
            return m_valid;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_sprite, new Vector2(m_rect.X, m_rect.Y), Color.Red);
        }
        public void Remove()
        {
            m_valid = false;
        }
    }
}
