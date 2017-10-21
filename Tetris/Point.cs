using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Point
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

        // Цвет точки
        public ConsoleColor fg;

        public Point()
        {

        }

        public Point(int x, int y, char ch, ConsoleColor fg = ConsoleColor.Gray)
        {
            X = x;
            Y = y;
            Ch = ch;
            this.fg = fg;
        }

        // Клонирование точки
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Ch = p.Ch;
        }

        // Рисование точки
        public void Draw()
        {
            Console.ForegroundColor = fg;
            Console.SetCursorPosition(X, Y);
            Console.Write(Ch);
            Console.ResetColor();
        }

        // Стирание точки
        public void Undraw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
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
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Ch);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Ch);
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Ch;
        }
    }
}