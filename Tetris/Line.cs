using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Line : Figure
    {
        // Символ, которым рисуется линия
        char lineCh;
        // Направление линии
        Direction direction;

        // Конструктор, создающий линию в указанных координатах,
        // определенной длины и в определенном направлении
        public Line(int x, int y, int length, char ch, Direction dir)
        {
            pList = new List<Point>();
            lineCh = ch;
            direction = dir;

            switch (direction)
            {
                case Direction.Left:
                    for (int i = x; i > x - length; i--)
                    {
                        Point p = new Point(i, y, lineCh);
                        pList.Add(p);
                    }
                    break;
                case Direction.Right:
                    for (int i = x; i < x + length; i++)
                    {
                        Point p = new Point(i, y, lineCh);
                        pList.Add(p);
                    }
                    break;
                case Direction.Up:
                    for (int i = y; i > y - length; i--)
                    {
                        Point p = new Point(x, i, lineCh);
                        pList.Add(p);
                    }
                    break;
                case Direction.Down:
                    for (int i = y; i < y + length; i++)
                    {
                        Point p = new Point(x, i, lineCh);
                        pList.Add(p);
                    }
                    break;
            }
        }
    }
}
