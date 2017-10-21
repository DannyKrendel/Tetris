using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Walls
    {
        // Длина и высота стен
        public static int Width { get; set; }
        public static int Height { get; set; }

        // Символ стен
        char wallCh;

        List<Point> wallList;

        public Walls(int fieldWidth, int fieldHeight, char wallCh)
        {
            Width = fieldWidth;
            Height = fieldHeight;
            this.wallCh = wallCh;

            wallList = new List<Point>();

            // Создание стен
            for (int i = 0; i < Height; i++)
            {
                for (int j = 1; j < Width - 1; j++)
                {
                    if (i == 0 || i == Height - 1)
                    {
                        wallList.Add(new Point(j, i, wallCh));
                    }
                }
                wallList.Add(new Point(0, i, wallCh));
                wallList.Add(new Point(Width - 1, i, wallCh));
            }
        }

        // Рисовка стен
        public void Draw()
        {
            foreach (Point p in wallList)
            {
                p.Draw();
            }
        }
        
        public void Draw(int x, int y)
        {
            foreach (Point p in wallList)
            {
                p.Move(x, y);
                p.Draw();
            }
        }
    }
}