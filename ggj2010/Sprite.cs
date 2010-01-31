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

//        Hashtable AnimationsTable;

        //std::vector<TAnimation> Animations;
        List<Animation> AnimationsList = new List<Animation>();
        Animation.AnimationType currentAnimation;
	    int currentFrame;
	    float totalTime;
	    bool dyingAnimationIsDone;

        public Sprite()
	    {
            SpriteTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
 //           Hashtable AnimationsTable = new Hashtable();

            //offset = squareWidthHeight;
	        //this->SetImage(*RS::GetImage(fileName));
	        //sf::Vector2f center(int(offset/2), int(offset/2));
	        //this->SetCenter(center);
	        //sf::IntRect subRect(0, 0, offset, offset);
	        //this->SetSubRect(subRect);
	        dyingAnimationIsDone = false;
//            currentAnimation = Animation.AnimationType.RUNNING;
	    }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager theContent, string assetName, int squareSize)
        {
            //m_texture = theContent.Load<Texture2D>(assetName);
            //m_rect = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
            offset = squareSize;
            SpriteTexture.Load(theContent, assetName, Frames, FramesPerSec);
            shipPos = new Vector2(300, 300);
            SpriteTexture.Play();
            spriteSheet = theContent.Load<Texture2D>(assetName);
        }

        public void Update(GameTime gameTime, Vector2 m_pos)
        {
            //m_position += m_velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            shipPos = m_pos;

            // update currently running animation
            //totalTime += fTime;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteTexture.UpdateFrame(elapsed);

            //currentFrame = Animations.at(currentAnimation).UpdateAnimation(fTime);
            currentFrame = AnimationsList[(int)currentAnimation].UpdateAnimation(elapsed);
            int X1 = (currentFrame-1)*offset;
            int Y1 = (int)currentAnimation*offset;
//            int X2 = currentFrame*offset;
//            int Y2 = (int)(currentAnimation+1)*offset;
//            this->SetSubRect(subRectFrame);
//            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0, FrameWidth, myTexture.Height);
            subRectFrame = new Rectangle(X1, Y1, offset, offset);
//            subRectFrame = new Rectangle(0, 0, 64, 64);

            // go back to idle animation after SHOOTING animation is done
            //if (currentAnimation == Animation.AnimationType.SHOOTING
            //    && AnimationsList[(int)Animation.AnimationType.SHOOTING].GetCurrentFrame() == 0)
            //    { 
            //        PlayAnimation(Animation.AnimationType.IDLING); 
            //    }

            //// go back to idle animation after SPAWNING animation is done
            //if (currentAnimation == Animation.AnimationType.SPAWNING
            //    && AnimationsList[(int)Animation.AnimationType.SPAWNING].GetCurrentFrame() == 0)
            //    { PlayAnimation(Animation.AnimationType.IDLING); }

            //// if dying animation completed, trip flag
            //if (currentAnimation == Animation.AnimationType.DYING && currentFrame == 0)
            //    { 
            //        dyingAnimationIsDone = true; 
            //    }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
 //           spriteBatch.Draw(m_texture, m_position, m_rect, Color.White, m_angle, Vector2.Zero, m_scale, SpriteEffects.None, 0);
            //spriteBatch.Draw(m_tiles[m_map[i, j]].m_texture, pos, Color.White);

            //spriteBatch.Draw(myTexture, shipPos, subRectFrame, Color.White,
            //    Rotation, Origin, Scale, SpriteEffects.None, Depth);

            //           SpriteTexture.DrawFrame(spriteBatch, shipPos);
            origin = new Vector2(0.0f, 0.0f);
            //spriteBatch.Draw(spriteSheet, shipPos, subRectFrame, Color.White);
            spriteBatch.Draw(spriteSheet, shipPos, subRectFrame, Color.White, 0.0f, origin, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
        }

        public void AddAnimation(Animation.AnimationType animationName, int numFrames, float numSecondsPerFrame, bool isLooping)
        {
            Animation newAnimation = new Animation(numFrames, numSecondsPerFrame, isLooping);
            AnimationsList.Add(newAnimation);

            //            AnimationsTable.Add(animationName, newAnimation);

            //            TAnimation newAnimation(numFrames, numSecondsPerFrame, isLooping);
            //            Animations.push_back(newAnimation);
        }

//        public void SetAnimations(int idleFrames, float idleRate, int runningFrames, int runningRate,
//                int climbingFrames, int climbingRate, int shootingFrames, float shootingRate,
//                int spawningFrames, float spawningRate, int dyingFrames, float dyingRate)
//        {
//            AddAnimation(Animation.AnimationType.IDLING, idleFrames, idleRate, true);
//            AddAnimation(Animation.AnimationType.RUNNING, runningFrames, runningRate, true);
//            AddAnimation(Animation.AnimationType.CLIMBING, climbingFrames, climbingRate, true);
////            AddAnimation(Animation.AnimationType.SHOOTING, shootingFrames, shootingRate, false);
////            AddAnimation(Animation.AnimationType.DYING, dyingFrames, dyingRate, false);
////            AddAnimation(Animation.AnimationType.SPAWNING, spawningFrames, spawningRate, false);
//        }

        public void PlayAnimation(Animation.AnimationType animationToPlay)
        {
            // if character is dying, can't change to new animation
            // don't restart animation if it is currently playing
//            if (currentAnimation != Animation.AnimationType.DYING && currentAnimation != animationToPlay)
            if (currentAnimation != animationToPlay)
            {
                // reset all animations to frame 0 prior to starting new animation
                //for (itAnim = Animations.begin(); itAnim != Animations.end(); itAnim++)
                //{
                //    itAnim->ResetAnimation();
                //}
                foreach (Animation anim in AnimationsList)
                {
                    anim.ResetAnimation();
                }

                // trigger animation to play, by specifying enum AnimationType
                currentAnimation = animationToPlay;
                //Animations.at(Animation).PlayAnimation();
                AnimationsList[(int)currentAnimation].PlayAnimation();
            }
        }

        //public bool IsDyingAnimationDone()
        //{
        //    return dyingAnimationIsDone;
        //}

        //public bool IsAnimationLooping()
        //{
        //    return AnimationsList[(int)currentAnimation].IsAnimationLooping();
        //}
    }
}

