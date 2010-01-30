using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using TilePair = ggj2010.Pair<string, ggj2010.TileMap.Tile.TileType>;


namespace ggj2010
{
    class TileMap
    {
        public class Tile
        {
            public enum TileType { SOLID, LADDER, NONE };
            public TileType m_type;
            public Texture2D m_texture;
            public Tile(ContentManager theContent, string assetName, TileType type)
            {
                m_texture = theContent.Load<Texture2D>(assetName);
                m_type = type;
            }
        }
        int[,] m_map;
        int m_width, m_height;
        int m_tileWidth, m_tileHeight;
        Tile[] m_tiles;
        public void LoadContent(ContentManager theContent, string assetName)
        {
            FileInfo mapFile = new FileInfo(assetName);
            StreamReader stream = mapFile.OpenText();

            string text;
            char[] separator = { ' ', ',' };
            text = stream.ReadLine();
            if (text != null)
            {
                string[] dimensions = text.Split(separator);
                m_width = Convert.ToInt32(dimensions[0]);
                m_height = Convert.ToInt32(dimensions[1]);
                m_map = new int[m_width, m_height];
            }
            int i = 0, j = 0;
            while ((text = stream.ReadLine()) != null)
            {
                i = 0;
                foreach (string number in text.Split(separator))
                {
                    m_map[i,j] = Convert.ToInt32(number, 10);
                    ++i;
                }
                ++j;
            }
            stream.Close();
        }
        public void LoadTiles(ContentManager theContent, TilePair[] tileNames)
        {
            int i = 0;
            m_tiles = new Tile[tileNames.Count()];
            foreach (TilePair file in tileNames)
            {
                m_tiles[i] = new Tile(theContent, file.first, file.second);
                ++i;
            }
            m_tileWidth = m_tiles[0].m_texture.Width;
            m_tileHeight = m_tiles[0].m_texture.Height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < m_height; ++j)
            {
                for (int i = 0; i < m_width; ++i)
                {
                    Vector2 pos = new Vector2(i*m_tileWidth, j * m_tileHeight);
                    spriteBatch.Draw(m_tiles[m_map[i,j]].m_texture, pos, Color.White);
                }
            }
        }
        public Vector2 Collide(ICollidable obj)
        {
            Rectangle objInitRect = obj.GetBoundingBox();
            Vector2 objVel = obj.GetVelocityVector();
            Rectangle objRect = new Rectangle(objInitRect.X + (int)objVel.X,
                objInitRect.Y + (int)objVel.Y, objInitRect.Width, objInitRect.Height);

            Rectangle tileRect = new Rectangle();
            tileRect.X = objRect.X / m_tileWidth;
            tileRect.Y = objRect.Y / m_tileHeight;
            tileRect.Width = objRect.Width / m_tileWidth;
            tileRect.Height = objRect.Height / m_tileHeight;

            for(int j=tileRect.Top; j<=tileRect.Bottom; ++j)
            {
                for (int i = tileRect.Left; i <= tileRect.Right; ++i)
                {
                    Rectangle currRect = new Rectangle(i * m_tileWidth, j * m_tileHeight,
                        m_tileWidth, m_tileHeight);
                    SimpleCollidable tileCollidable = new SimpleCollidable(currRect, Vector2.Zero);
                    SimpleCollidable objX = new SimpleCollidable(objInitRect, new Vector2(objVel.X, 0));
                    SimpleCollidable objY = new SimpleCollidable(objInitRect, new Vector2(0, objVel.Y));

                    if (m_tiles[m_map[i, j]].m_type == Tile.TileType.NONE)
                        continue;
                    if(objX.WillCollide(tileCollidable))
                    {
                        if (objVel.X > 0)
                            objVel.X = currRect.Left - objInitRect.Right;
                        else
                            objVel.X = currRect.Right - objInitRect.Left;
                    }
                    if(objY.WillCollide(tileCollidable))
                    {
                        if (objVel.Y > 0)
                            objVel.Y = currRect.Top - objInitRect.Bottom;
                        else
                            objVel.Y = currRect.Bottom - objInitRect.Top;
                    }
                }
            }
            return objVel;
        }
    }
}
