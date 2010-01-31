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
    class Screens
    {
        enum ScreenType { INTRO, RULES, GAME, SCORE, TITLE };
        ScreenType screenState = ScreenType.INTRO;
        SpriteFont Font1;
        Texture2D introTexture, rulesTexture, scoreTexture, titleTexture;
        Vector2 origin;

        public void LoadContent(ContentManager Content)
        {
            Font1 = Content.Load<SpriteFont>("Arial");
            titleTexture = Content.Load<Texture2D>("screen_title");
            introTexture = Content.Load<Texture2D>("screen_intro");
            rulesTexture = Content.Load<Texture2D>("screen_vibrate");
            scoreTexture = Content.Load<Texture2D>("screen_individual_score");
            origin = new Vector2(0.0f, 0.0f);
        }

        public void Update(GameTime gameTime, Game1.State state)
        {
            screenState = (ScreenType)state;
        }

        public void Draw(SpriteBatch spriteBatch, int[] m_score)
        {
            switch (screenState)
            {
                case ScreenType.INTRO: DrawIntro(spriteBatch); break;
                case ScreenType.RULES: DrawRules(spriteBatch); break;
                case ScreenType.GAME: break;
                case ScreenType.SCORE: DrawScores(spriteBatch, m_score); break;
                case ScreenType.TITLE: DrawTitle(spriteBatch); break;
                default: break;
            }
        }

        public void DrawIntro(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(introTexture, origin, Color.White);
       }

        public void DrawRules(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rulesTexture, origin, Color.White);
        }

        public void DrawTitle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(titleTexture, origin, Color.White);
        }

        public void DrawScores(SpriteBatch spriteBatch, int[] m_score)
        {
            Color fontColor = new Color(79, 111, 130);
            Vector2 FontPos = new Vector2(512, 384);
            string output1 = m_score[1].ToString();
            string output2 = m_score[2].ToString();
            string output3 = m_score[3].ToString();
            string output4 = m_score[0].ToString();
            Vector2 outputPos1 = new Vector2(830, 130);
            Vector2 outputPos2 = new Vector2(830, 280);
            Vector2 outputPos3 = new Vector2(830, 440);
            Vector2 outputPos4 = new Vector2(830, 590);
            Vector2 FontOrigin = new Vector2(0, 0);
            spriteBatch.Draw(scoreTexture, origin, Color.White);
            spriteBatch.DrawString(Font1, output1, outputPos1, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, output2, outputPos2, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, output3, outputPos3, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, output4, outputPos4, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, float size)
        {
            Color fontColor = new Color(79, 111, 130);
            Vector2 FontOrigin = new Vector2(0, 0);
            spriteBatch.DrawString(Font1, text, position, fontColor, 0, FontOrigin, size, SpriteEffects.None, 0.5f);
        }
    }
}
