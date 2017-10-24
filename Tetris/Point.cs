using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Point : Colored
    {
        // Координаты точки
        int x;
        int y;

        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }

        // Символ точки
        public char Ch { get; set; }

        public Point()
        {

        }

        public Point(int x, int y, char ch)
        {
            X = x;
            Y = y;
            Ch = ch;
        }

        // Клонирование точки
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Ch = p.Ch;
            bgColor = p.bgColor;
            fgColor = p.fgColor;
        }

        // Рисование точки
        public void Draw()
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;

            Console.SetCursorPosition(X, Y);
            Console.Write(Ch);

            Console.ResetColor();
        }

        // Стирание точки
        public void Undraw()
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;

            Console.SetCursorPosition(X, Y);
            Console.Write(' ');

            Console.ResetColor();
        }

        // Смещение точки на offset пунктов в заданном направлении
        public void Move(int xOffset, int yOffset)
        {
            X += xOffset;
            Y += yOffset;
        }

        // Проверка столкновений с точкой
        public bool IsHit(Point p)
        {
            return p.X == X && p.Y == Y;
        }

        // Перегрузка операторов
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Ch) { bgColor = p1.bgColor, fgColor = p1.fgColor };
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Ch) { bgColor = p1.bgColor, fgColor = p1.fgColor };
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Ch;
        }
    }
}