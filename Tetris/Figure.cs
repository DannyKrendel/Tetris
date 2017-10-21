using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Figure
    {
        // Список точек фигуры
        public List<Point> pList;
        int center;

        // Цвет фигуры
        public ConsoleColor fg;

        public Figure()
        {

        }

        // Копирование фигуры
        public Figure(Figure figure)
        {
            fg = figure.fg;
            center = figure.center;

            pList = new List<Point>();

            foreach (Point p in figure.pList)
            {
                pList.Add(new Point(p.X, p.Y, p.Ch, fg));
            }
        }

        // Создание фигуры из строки, с центром поворота
        public Figure(int center, ConsoleColor fg = ConsoleColor.Gray, params string[] strArr)
        {
            this.center = center;
            this.fg = fg;
            pList = new List<Point>();
            for (int i = 0; i < strArr.Length; i++)
            {
                for (int j = 0; j < strArr[i].Length; j++)
                {
                    if (strArr[i][j] != ' ')
                    {
                        char ch = strArr[i][j];
                        pList.Add(new Point(j, i, ch, fg));
                    }
                }
            }
        }

        // Рисование точек фигуры
        public void Draw()
        {
            foreach (Point p in pList)
            {
                if (p.X > 0 && p.Y > 0)
                    p.Draw();
            }
        }

        // Стирание точек фигуры
        public void Undraw()
        {
            foreach (Point p in pList)
            {
                if (p.X > 0 && p.Y > 0)
                    p.Undraw();
            }
        }

        // Вращение фигуры вокруг центра на 90 градусов
        public void Rotate()
        {
            if (center == -1)
                return;
            for (int i = 0; i < pList.Count; i++)
            {
                int x = 0 - (pList[i].Y - pList[center].Y) + pList[center].X;
                int y = pList[i].X - pList[center].X + pList[center].Y;
                pList[i] = new Point(x, y, pList[i].Ch, fg);
            }
        }

        // Движение фигуры в направлении
        public void Move(Direction dir)
        {
            foreach (Point p in pList)
            {
                switch (dir)
                {
                    case Direction.Left:
                        p.Move(-1, 0);
                        break;
                    case Direction.Right:
                        p.Move(1, 0);
                        break;
                    case Direction.Up:
                        p.Move(0, -1);
                        break;
                    case Direction.Down:
                        p.Move(0, 1);
                        break;
                }
            }
        }

        // Назначить координаты фигуры
        public void SetCoordinates(int x, int y)
        {
            int xOffset = x - pList.First().X;
            int yOffset = y - pList.First().Y;
            foreach (Point p in pList)
            {
                p.Move(xOffset, yOffset);
            }
        }

        // Проверка столкновения фигуры со стенами
        internal bool CollidingWithLeftWall()
        {
            foreach (var p in pList)
            {
                if (p.X == 0)
                    return true;
            }
            return false;
        }

        internal bool CollidingWithRightWall()
        {
            foreach (var p in pList)
            {
                if (p.X == Walls.Width - 1)
                    return true;
            }
            return false;
        }

        // Проверка столкновения фигуры с полом
        internal bool CollidingWithFloor()
        {
            foreach (var p in pList)
            {
                if (p.Y == Walls.Height - 1)
                    return true;
            }
            return false;
        }

        // Проверка столкновения фигуры с точками
        internal bool CollidingWith(List<Point> _pList)
        {
            if (_pList.Count > 0)
            {
                foreach (Point p in _pList)
                {
                    if (CollidingWith(p))
                        return true;
                }
            }
            return false;
        }

        // Проверка столкновения фигуры с фигурой
        internal bool CollidingWith(Figure figure)
        {
            foreach (var p in pList)
            {
                if (figure.CollidingWith(p))
                    return true;
            }
            return false;
        }

        // Проверка столкновения фигуры с точкой
        internal bool CollidingWith(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}