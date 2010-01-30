using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ggj2010
{
    public interface ICollidable
    {
        Rectangle GetBoundingBox();
        Vector2 GetVelocityVector();
        bool Colliding(ICollidable other);
        bool WillCollide(ICollidable other);
    }
    public class SimpleCollidable: ICollidable
    {
        private Rectangle m_rect;
        private Vector2 m_vec;
        public SimpleCollidable(Rectangle rect, Vector2 vec)
        {
            m_rect = rect;
            m_vec = vec;
        }
        public Rectangle GetBoundingBox()
        {
            return m_rect;
        }
        public Vector2 GetVelocityVector()
        {
            return m_vec;
        }
        public bool Colliding(ICollidable other)
        {
            Rectangle theirs = other.GetBoundingBox();
            if (m_rect.Right > theirs.Left && m_rect.Left < theirs.Right
                && m_rect.Bottom > theirs.Top && m_rect.Top < theirs.Bottom)
            {
                return true;
            }
            return false;
        }
        public bool WillCollide(ICollidable other)
        {
            Rectangle theirs = other.GetBoundingBox();
            Vector2 vel = other.GetVelocityVector();

            Rectangle mine = new Rectangle(m_rect.X + (int)m_vec.X,
                m_rect.Y + (int)m_vec.X, m_rect.Width, m_rect.Height);

            theirs.X += (int)vel.X;
            theirs.Y += (int)vel.Y;

            if (mine.Right > theirs.Left && mine.Left < theirs.Right
                && mine.Bottom > theirs.Top && mine.Top < theirs.Bottom)
            {
                return true;
            }
            return false;
        }
    }
}
