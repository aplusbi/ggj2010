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
    public class TileMap
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
        public bool OnLadder(ICollidable obj, out int x, out int y)
        {
            floatRectangle objRect = obj.GetBoundingBox();
            Vector2 objVel = obj.GetVelocityVector();

            x = 0;
            y = 0;

            if (objRect.Top() > m_tileHeight * m_height || objRect.Bottom() < 0
                || objRect.Left() > m_tileWidth * m_width || objRect.Right() < 0)
                return false;

            Rectangle tileRect = new Rectangle();
            tileRect.X = (int)objRect.X / m_tileWidth;
            tileRect.Y = (int)objRect.Y / m_tileHeight;
            tileRect.Width = (int)objRect.Width / m_tileWidth;
            tileRect.Height = (int)objRect.Height / m_tileHeight;

            if (tileRect.X < 0) tileRect.X = 0;
            if (tileRect.Y < 0) tileRect.Y = 0;
            if (tileRect.Right >= m_width)
                tileRect.Width = m_width - tileRect.X - 1;
            if (tileRect.Bottom >= m_height)
                tileRect.Height = m_height - tileRect.Y - 1;

            for (int j = tileRect.Top; j <= tileRect.Bottom; ++j)
            {
                for (int i = tileRect.Left; i <= tileRect.Right; ++i)
                {
                    if (m_tiles[m_map[i, j]].m_type == Tile.TileType.LADDER)
                    {
                        x = i * m_tileWidth;
                        y = i * m_tileHeight;
                        return true;
                    }
                }
            }
            return false;
        }
        public Vector2 Collide(ICollidable obj)
        {
            floatRectangle objInitRect = obj.GetBoundingBox();
            Vector2 objVel = obj.GetVelocityVector();
            floatRectangle objRect = new floatRectangle(objInitRect.X + objVel.X,
                objInitRect.Y + objVel.Y, objInitRect.Width, objInitRect.Height);

            if (objRect.Top() > m_tileHeight * m_height || objRect.Bottom() < 0
                || objRect.Left() > m_tileWidth * m_width || objRect.Right() < 0)
                return objVel;

            Rectangle tileRect = new Rectangle();
            tileRect.X = (int)objRect.X / m_tileWidth;
            tileRect.Y = (int)objRect.Y / m_tileHeight;
            tileRect.Width = (int)objRect.Width / m_tileWidth;
            tileRect.Height = (int)objRect.Height / m_tileHeight;

            if (tileRect.X < 0) tileRect.X = 0;
            if (tileRect.Y < 0) tileRect.Y = 0;
            if (tileRect.Right >= m_width)
                tileRect.Width = m_width - tileRect.X - 1;
            if (tileRect.Bottom >= m_height)
                tileRect.Height = m_height - tileRect.Y - 1;

            for (int j = tileRect.Top; j <= tileRect.Bottom; ++j)
            {
                for (int i = tileRect.Left; i <= tileRect.Right; ++i)
                {
                    if (m_tiles[m_map[i, j]].m_type == Tile.TileType.NONE
                        || m_tiles[m_map[i, j]].m_type == Tile.TileType.LADDER)
                        continue;

                    floatRectangle currRect = new floatRectangle(i * m_tileWidth, j * m_tileHeight,
                        m_tileWidth, m_tileHeight);
                    floatRectangle objRectX = new floatRectangle(objInitRect.X + objVel.X, objInitRect.Y+1,
                        objInitRect.Width, objInitRect.Height-2);
                    floatRectangle objRectY = new floatRectangle(objInitRect.X+1, objInitRect.Y + objVel.Y,
                        objInitRect.Width-2, objInitRect.Height);

                    if (objVel.X > 0)
                    {
                        if (objRectX.Right() > currRect.Left()
                            && objRectX.Top() < currRect.Bottom() && objRectX.Bottom() > currRect.Top())
                        {
                            objVel.X = currRect.Left() - objInitRect.Right();
                            if (objVel.X < 0)
                                objVel.X = 0;
                        }
                    }
                    else
                    {
                        if (objRectX.Left() < currRect.Right()
                            && objRectX.Top() < currRect.Bottom() && objRectX.Bottom() > currRect.Top())
                        {
                            objVel.X = currRect.Right() - objInitRect.Left();
                            if (objVel.X > 0)
                                objVel.X = 0;
                        }
                    }
                    if (objVel.Y > 0)
                    {
                        if (objRectY.Bottom() > currRect.Top()
                            && objRectY.Left() < currRect.Right() && objRectY.Right() > currRect.Left())
                        {
                            objVel.Y = currRect.Top() - objInitRect.Bottom();
                            if (objVel.Y < 0)
                                objVel.Y = 0;
                        }
                    }
                    //else
                    //{
                    //    if (objRectY.Top() < currRect.Bottom()
                    //        && objRectY.Left() < currRect.Right() && objRectY.Right() > currRect.Left())
                    //    {
                    //        objVel.Y = currRect.Bottom() - objInitRect.Top();
                    //        if (objVel.Y > 0)
                    //            objVel.Y = 0;
                    //    }
                    //}
                }
            }
            return objVel;
        }
    }
}
