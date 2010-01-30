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

namespace ggj2010
{
    class Sprite
    {
//        public Texture2D m_texture;
        public Vector2 m_position = new Vector2(0, 0);
        public Vector2 m_velocity = new Vector2(0, 0);
        public Rectangle m_rect;
        public float m_scale = 1.0f;
        public float m_angle = 0.0f;

        private AnimatedTexture SpriteTexture;
        private const float Rotation = 0;
        private const float Scale = 2.0f;
        private const float Depth = 0.5f;

        private Vector2 shipPos;
        private const int Frames = 4;
        private const int FramesPerSec = 2;

//        public int offset;

        public Sprite()
	    {
            SpriteTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);

            //offset = squareWidthHeight;
	        //this->SetImage(*RS::GetImage(fileName));
	        //sf::Vector2f center(int(offset/2), int(offset/2));
	        //this->SetCenter(center);
	        //sf::IntRect subRect(0, 0, offset, offset);
	        //this->SetSubRect(subRect);
	        //dyingAnimationIsDone = false;
        //	PlayAnimation(IDLING); // start in "idling animation" state
	    }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager theContent, string assetName)
        {
            //m_texture = theContent.Load<Texture2D>(assetName);
            //m_rect = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
            SpriteTexture.Load(theContent, assetName, Frames, FramesPerSec);
            shipPos = new Vector2(300, 300);
            SpriteTexture.Play();

        }
        public void Update(GameTime gameTime)
        {
            m_position += m_velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Pauses and plays the animation.
            if (GamePad.GetState(PlayerIndex.One).Buttons.A ==
                ButtonState.Pressed)
            {
                if (SpriteTexture.IsPaused)
                    SpriteTexture.Play();
                else
                    SpriteTexture.Pause();
            }
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteTexture.UpdateFrame(elapsed);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
 //           spriteBatch.Draw(m_texture, m_position, m_rect, Color.White, m_angle, Vector2.Zero, m_scale, SpriteEffects.None, 0);
            SpriteTexture.DrawFrame(spriteBatch, shipPos);
            //spriteBatch.Draw(m_tiles[m_map[i, j]].m_texture, pos, Color.White);
        }
        /*
                public void SetAnimations(int idleFrames, float idleRate, int movingFrames, float movingRate,
                        int shootingFrames, float shootingRate, int spawningFrames, float spawningRate,
                        int dyingFrames, float dyingRate)
                {
                    AddAnimation(IDLING, idleFrames, idleRate, true);
                    AddAnimation(MOVING, movingFrames, movingRate, true);
                    AddAnimation(SHOOTING, shootingFrames, shootingRate, false);
                    AddAnimation(SPAWNING, spawningFrames, spawningRate, false);
                    AddAnimation(DYING, dyingFrames, dyingRate, false);
                }


                    // update currently running animation
                    totalTime += fTime;
                    currentFrame = Animations.at(currentAnimation).UpdateAnimation(fTime);
                    int X1 = (currentFrame-1)*offset;
                    int Y1 = currentAnimation*offset;
                    int X2 = currentFrame*offset;
                    int Y2 = (currentAnimation+1)*offset;
                    //sf::IntRect subRectFrame(X1, Y1, X2, Y2);
                    this->SetSubRect(subRectFrame);

                    // go back to idle animation after SHOOTING animation is done
                    if (currentAnimation == SHOOTING && Animations.at(SHOOTING).GetCurrentFrame() == 0) //|| currentFrame >= Animations.at(SHOOTING).GetLastFrame()
                        {PlayAnimation(IDLING);}

                    // go back to idle animation after SPAWNING animation is done
                    if (currentAnimation == SPAWNING && Animations.at(SPAWNING).GetCurrentFrame() == 0)
                        {PlayAnimation(IDLING);}

                    // if dying animation completed, trip flag
                    if (currentAnimation == DYING && currentFrame == 0) {dyingAnimationIsDone = true;}



        public void AddAnimation(AnimationType Animation, int numFrames, float numSecondsPerFrame, bool isLooping)
        {
	        //TAnimation newAnimation(numFrames, numSecondsPerFrame, isLooping);
	        //Animations.push_back(newAnimation);
        }

        public void PlayAnimation(AnimationType Animation)
        {
            // if character is dying, can't change to new animation
            // don't restart animation if it is currently playing
            if (currentAnimation != DYING && currentAnimation != Animation)
            {
                // reset all animations to frame 0 prior to starting new animation
                for (itAnim = Animations.begin(); itAnim != Animations.end(); itAnim++)
                {
                    itAnim->ResetAnimation();
                }
                // trigger animation to play, by specifying enum AnimationType
                currentAnimation = Animation;
                Animations.at(Animation).PlayAnimation();
            }
        }
        public bool IsDyingAnimationDone()
        {
            return dyingAnimationIsDone;
        }
        public bool IsAnimationLooping()
        {
            return Animations.at(currentAnimation).IsAnimationLooping();
        }
        */
    }
}

