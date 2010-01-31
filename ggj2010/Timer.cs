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
    class Timer
    {
        double m_time;
        SpriteFont m_font;
        public Timer(double time)
        {
            m_time = time;
        }
        public void LoadContent(ContentManager theContent, string font)
        {
            m_font = theContent.Load<SpriteFont>(font);
        }
        public void Update(GameTime gameTime)
        {
            m_time -= gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(512, 32);
            int seconds = (int)m_time;
            string output = seconds.ToString();
            Vector2 FontOrigin = m_font.MeasureString(output) / 2;
            Color fontColor = new Color(79, 111, 130);
            //spriteBatch.Draw(spriteSheet, FontPos, Color.White);
            spriteBatch.DrawString(m_font, output, FontPos, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
        public double GetTime()
        {
            return m_time;
        }
    }
}
