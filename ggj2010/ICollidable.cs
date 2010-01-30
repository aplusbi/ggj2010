using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ggj2010
{
    interface ICollidable
    {
        Rectangle GetBoundingBox();
        Vector2 GetVelocityVector();
        bool Colliding(ICollidable other);
    }
}
