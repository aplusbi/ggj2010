using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ggj2010
{
    public class floatRectangle
    {
        public float X, Y;
        public float Width, Height;
        public floatRectangle()
        {
            X = Y = Width = Height = 0;
        }
        public floatRectangle(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        public float Top() { return Y; }
        public float Left() { return X; }
        public float Bottom() { return Y + Height; }
        public float Right() { return X + Width; }
    }
    public interface ICollidable
    {
        floatRectangle GetBoundingBox();
        Vector2 GetVelocityVector();
        bool Colliding(ICollidable other);
        bool WillCollide(ICollidable other);
    }
    public class SimpleCollidable: ICollidable
    {
        protected floatRectangle m_rect;
        protected Vector2 m_vec;
        public SimpleCollidable()
        {
        }
        public SimpleCollidable(floatRectangle rect, Vector2 vec)
        {
            m_rect = rect;
            m_vec = vec;
        }
        public floatRectangle GetBoundingBox()
        {
            return m_rect;
        }
        public Vector2 GetVelocityVector()
        {
            return m_vec;

        }
        public bool Colliding(ICollidable other)
        {
            floatRectangle theirs = other.GetBoundingBox();
            if (m_rect.Right() > theirs.Left() && m_rect.Left() < theirs.Right()
                && m_rect.Bottom() > theirs.Top() && m_rect.Top() < theirs.Bottom())
            {
                return true;
            }
            return false;
        }
        public bool WillCollide(ICollidable other)
        {
            floatRectangle theirs = other.GetBoundingBox();
            Vector2 vel = other.GetVelocityVector();

            floatRectangle mine = new floatRectangle(m_rect.X + m_vec.X,
                m_rect.Y + m_vec.X, m_rect.Width, m_rect.Height);

            theirs.X += vel.X;
            theirs.Y += vel.Y;

            if (mine.Right() > theirs.Left() && mine.Left() < theirs.Right()
                && mine.Bottom() > theirs.Top() && mine.Top() < theirs.Bottom())
            {
                return true;
            }
            return false;
        }
    }
}
