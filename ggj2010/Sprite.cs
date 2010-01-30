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
	    public Texture2D m_texture;
	    public Vector2 m_position = new Vector2(0, 0);
	    public Vector2 m_velocity = new Vector2(0, 0);
	    public Rectangle m_rect;
	    public float m_scale = 1.0f;
	    public float m_angle = 0.0f;

	    public void Initialize()
	    {
	    }
	    public void LoadContent(ContentManager theContent, string assetName)
	    {
		    m_texture = theContent.Load<Texture2D>(assetName);
		    m_rect = new Rectangle(0, 0, m_texture.Width, m_texture.Height);
	    }
	    public void Update(GameTime gameTime)
	    {
		    m_position += m_velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
	    }
	    public void Draw(SpriteBatch spriteBatch)
	    {
            spriteBatch.Draw(m_texture, m_position, m_rect, Color.White, m_angle, Vector2.Zero, m_scale, SpriteEffects.None, 0);
	    }
    }
}

