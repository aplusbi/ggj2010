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
    class Player
    {
        private Sprite m_sprite;

        public Player()
        {
            m_sprite = new Sprite();
            m_sprite.AddAnimation(Animation.AnimationType.IDLING, 4, 0.3f, true);
            m_sprite.AddAnimation(Animation.AnimationType.RUNNING, 5, 0.1f, true);
            m_sprite.AddAnimation(Animation.AnimationType.CLIMBING, 8, 0.1f, true);
            //m_sprite.AddAnimation(Animation.AnimationType.IDLING, idleFrames, idleRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.RUNNING, movingFrames, movingRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.CLIMBING, movingFrames, movingRate, true);
            //m_sprite.AddAnimation(Animation.AnimationType.SHOOTING, shootingFrames, shootingRate, false);
            //m_sprite.AddAnimation(Animation.AnimationType.DYING, dyingFrames, dyingRate, false);
//            m_sprite.AddAnimation(Animation.AnimationType.SPAWNING, spawningFrames, spawningRate, false);
        }

        public void LoadContent(ContentManager theContent, string assetName, int squareSize)
        {
            m_sprite.LoadContent(theContent, assetName, squareSize);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
//            m_sprite.AddAnimation("idle", 0);
        }

        public void Update(GameTime gameTime)
        {
            m_sprite.Update(gameTime);
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
