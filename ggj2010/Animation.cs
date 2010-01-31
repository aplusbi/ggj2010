using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ggj2010
{
    class Animation
    {
        private float secondsPerFrame;
        private float currentTime;
        private int currentFrame;
        private int lastFrame;
        private bool loopingType;

        public enum AnimationType { DYING, RUNNING, CLIMBING, IDLING, PANTING, SHOOTING, SPAWNING}; 

        public Animation(int numFrames, float numSecondsPerFrame, bool isLooping)
        {
            lastFrame = numFrames;
            secondsPerFrame = numSecondsPerFrame;
            loopingType = isLooping;
            currentTime = 0.0f;
            currentFrame = 0; // when frame is 0, animation is stopped, otherwise running (>0)
        }

        public int UpdateAnimation(float fTime)
        {
            if (currentFrame != 0)
            {
                currentTime += fTime;
                currentFrame = (int)Math.Ceiling(currentTime / secondsPerFrame);
                if (currentFrame > lastFrame)
                {
                    //(loopingType == false) ? (currentFrame = 0) : (currentFrame = 1);
                    if (loopingType == false)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = 1;
                    }
                    currentTime = 0.0f;
                }
            }
            return currentFrame;
        }

        public void PlayAnimation()
        {
            currentFrame = 1;
            currentTime = 0.0f;
        }

        public void ResetAnimation()
        {
            currentFrame = 0;
            currentTime = 0.0f;
        }

        public int GetCurrentFrame()
        {
            return currentFrame;
        }
    }
}
