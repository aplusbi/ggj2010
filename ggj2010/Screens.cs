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
        public enum ScreenType { NONE, INTRO, RULES, TEAMSCORES, PLAYERSCORES };
        public ScreenType screenState = ScreenType.INTRO;
        SpriteFont Font1;

        public void LoadContent(ContentManager Content)
        {
            Font1 = Content.Load<SpriteFont>("Arial");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (screenState)
            {
                case ScreenType.NONE: break;
                case ScreenType.INTRO: DrawIntro(spriteBatch); break;
                case ScreenType.RULES: DrawRules(spriteBatch); break;
                case ScreenType.TEAMSCORES: DrawTeamScores(spriteBatch); break;
                case ScreenType.PLAYERSCORES: DrawPlayerScores(spriteBatch); break;
                default: break;
            }
        }

        public void DrawIntro(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(512, 384);
            string output = "Hello World";
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            Color fontColor = new Color(79, 111, 130);
            //spriteBatch.Draw(spriteSheet, FontPos, Color.White);
            spriteBatch.DrawString(Font1, output, FontPos, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public void DrawRules(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(512, 384);
            string output = "Hello World";
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            Color fontColor = new Color(79, 111, 130);
            //spriteBatch.Draw(spriteSheet, FontPos, Color.White);
            spriteBatch.DrawString(Font1, output, FontPos, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public void DrawTeamScores(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(512, 384);
            string output = "Hello World";
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            Color fontColor = new Color(79, 111, 130);
            //spriteBatch.Draw(spriteSheet, FontPos, Color.White);
            spriteBatch.DrawString(Font1, output, FontPos, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public void DrawPlayerScores(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(512, 384);
            string output = "Hello World";
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            Color fontColor = new Color(79, 111, 130);
            //spriteBatch.Draw(spriteSheet, FontPos, Color.White);
            spriteBatch.DrawString(Font1, output, FontPos, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
