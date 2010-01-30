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
        }

        public void LoadContent(ContentManager theContent, string assetName)
        {
            m_sprite.LoadContent(theContent, assetName);
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
