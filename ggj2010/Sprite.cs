using System;
using System.Collections;
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
//using Anim = ggj2010.Pair<string, ggj2010.Animation.AnimationType>;

namespace ggj2010
{
    class Sprite
    {
//        public Texture2D m_texture;
        Texture2D spriteSheet;
        public Vector2 m_position = new Vector2(0, 0);
        public Vector2 m_velocity = new Vector2(0, 0);
        public Rectangle m_rect;
        public float m_scale = 1.0f;
        public float m_angle = 0.0f;

        private AnimatedTexture SpriteTexture;
        private const float Rotation = 0;
        private const float Scale = 1.00f;
        private const float Depth = 0.5f;

        private Animation[] Animations;
        private Vector2 shipPos;
        private const int Frames = 4;
        private const int FramesPerSec = 2;
        private Vector2 origin;
        public int offset = 16;
        Rectangle subRectFrame;
        List<Animation> AnimationsList = new List<Animation>();
        Animation.AnimationType currentAnimation;
	    int currentFrame;
	    float totalTime;
        bool dyingAnimationIsDone;
        public Color m_color;
        ContentManager Content;
        SoundEffect soundEffect;

        public Sprite()
	    {
            SpriteTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
	        dyingAnimationIsDone = false;
            currentAnimation = Animation.AnimationType.NONE;
	    }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager theContent, string assetName, int squareSize)
        {
            offset = squareSize;
            SpriteTexture.Load(theContent, assetName, Frames, FramesPerSec);
            shipPos = new Vector2(300, 300);
            SpriteTexture.Play();
            spriteSheet = theContent.Load<Texture2D>(assetName);
            origin = new Vector2(squareSize / 2, 0);
            soundEffect = theContent.Load<SoundEffect>("kaboom");
        }

        public void Update(GameTime gameTime, Vector2 m_pos, ContentManager Content2)
        {
            Content = Content2;
            if (currentAnimation == Animation.AnimationType.NONE)
                return;
            //m_position += m_velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            shipPos = m_pos;

            // update currently running animation
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteTexture.UpdateFrame(elapsed);

            currentFrame = AnimationsList[(int)currentAnimation].UpdateAnimation(elapsed);
            int X1 = (currentFrame-1)*offset;
            int Y1 = (int)currentAnimation*offset;
            subRectFrame = new Rectangle(X1, Y1, offset, offset);
        }

        public void Draw(SpriteBatch spriteBatch, bool flip)
        {
            if (flip)
            {
                spriteBatch.Draw(spriteSheet, shipPos, subRectFrame, m_color, 0.0f, origin,
                    1.0f, SpriteEffects.FlipHorizontally, 0.0f);
            }
            else
            {
                spriteBatch.Draw(spriteSheet, shipPos, subRectFrame, m_color, 0.0f, origin,
                    1.0f, 0, 0.0f);
            }
        }

        public void AddAnimation(Animation.AnimationType animationName, int numFrames, float numSecondsPerFrame, bool isLooping)
        {
            Animation newAnimation = new Animation(numFrames, numSecondsPerFrame, isLooping);
            AnimationsList.Add(newAnimation);
        }

        public void PlayAnimation(Animation.AnimationType animationToPlay)
        {
            // if character is dying, can't change to new animation; don't restart animation if currently playing
            if (currentAnimation != Animation.AnimationType.DYING && currentAnimation != animationToPlay)
            {
                // reset all animations to frame 0 prior to starting new animation
                foreach (Animation anim in AnimationsList)
                {
                    anim.ResetAnimation();
                }

                // trigger animation to play, by specifying enum AnimationType
                currentAnimation = animationToPlay;
                AnimationsList[(int)currentAnimation].PlayAnimation();

                switch (currentAnimation)
                {
                    case Animation.AnimationType.DYING: break;
                    case Animation.AnimationType.RUNNING: soundEffect = Content.Load<SoundEffect>("kaboom2"); break;
                    case Animation.AnimationType.CLIMBING: soundEffect = Content.Load<SoundEffect>("kaboom2"); break;
                    case Animation.AnimationType.IDLING: soundEffect = Content.Load<SoundEffect>("kaboom2"); break;
                    case Animation.AnimationType.PANTING: soundEffect = Content.Load<SoundEffect>("kaboom2"); break;
                    case Animation.AnimationType.SHOOTING: soundEffect = Content.Load<SoundEffect>("gun_shoot_02"); break;
                    case Animation.AnimationType.SPAWNING: break;
                    default: break;
                }
                soundEffect.Play();
            }
        }

        public bool IsCurrentAnimationDone()
        {
            if (AnimationsList[(int)currentAnimation].GetCurrentFrame() == 0)
            {
                return true;
            }
            else return false;
            //return dyingAnimationIsDone;
        }

        //public bool IsAnimationLooping()
        //{
        //    return AnimationsList[(int)currentAnimation].IsAnimationLooping();
        //}
    }
}

