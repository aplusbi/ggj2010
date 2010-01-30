using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;


namespace ggj2010
{
    class TileMap
    {
        int[,] m_map;
        int m_width, m_height;
        int m_tileWidth, m_tileHeight;
        Texture2D[] m_tiles;
        public void LoadContent(ContentManager theContent, string assetName)
        {
            FileInfo mapFile = new FileInfo(assetName);
            StreamReader stream = mapFile.OpenText();

            string text;
            char[] separator = { ' ' };
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
        public void LoadTiles(ContentManager theContent, string[] tileNames)
        {
            int i = 0;
            m_tiles = new Texture2D[tileNames.Count()];
            foreach (string file in tileNames)
            {
                m_tiles[i] = theContent.Load<Texture2D>(file);
                ++i;
            }
            m_tileWidth = m_tiles[0].Width;
            m_tileHeight = m_tiles[0].Height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < m_height; ++j)
            {
                for (int i = 0; i < m_width; ++i)
                {
                    Vector2 pos = new Vector2(i*m_tileWidth, j * m_tileHeight);
                    spriteBatch.Draw(m_tiles[m_map[i,j]], pos, Color.White);
                }
            }
        }
    }
}
