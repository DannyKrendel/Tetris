using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Tetris
    {
        Random rand = new Random();

        // Список стандартных фигур
        List<Figure> figureList;
        // Список старых фигур
        List<Point> oldPoints;

        // Текущая фигура
        Figure curFigure;
        // Следующая фигура
        Figure nextFigure;

        // Маркеры отвечающие за отображение статистики
        public static bool isFigureChanged;
        public static bool isScoreChanged;
        public static bool isHighScoreChanged;
        public static bool isLevelChanged;
        public static bool isLinesChanged;

        public Tetris()
        {
            isFigureChanged = true;
            isScoreChanged = true;
            isHighScoreChanged = true;
            isLevelChanged = true;
            isLinesChanged = true;

            figureList = new List<Figure>()
            {
                new Figure(1, "■",
                              "■",
                              "■",
                              "■"),
                new Figure(-1, "■■",
                               "■■"),
                new Figure(1, "■",
                              "■",
                              "■■"),
                new Figure(1, " ■",
                              " ■",
                              "■■"),
                new Figure(2, " ■ ",
                              "■■■"),
                new Figure(0, " ■■",
                              "■■"),
                new Figure(1, "■■",
                              " ■■")
            };

            figureList[0].SetColor(ConsoleColor.Black, ConsoleColor.Cyan);
            figureList[1].SetColor(ConsoleColor.Black, ConsoleColor.Yellow);
            figureList[2].SetColor(ConsoleColor.Black, ConsoleColor.Gray);
            figureList[3].SetColor(ConsoleColor.Black, ConsoleColor.Magenta);
            figureList[4].SetColor(ConsoleColor.Black, ConsoleColor.Blue);
            figureList[5].SetColor(ConsoleColor.Black, ConsoleColor.Green);
            figureList[6].SetColor(ConsoleColor.Black, ConsoleColor.Red);

            oldPoints = new List<Point>();

            GetNextFigure();
            SpawnFigure();
        }

        // Начальная скорость фигур
        int speed = 200;

        // Множитель скорости
        int multiplier = 100;

        // Время прошедшее с прошлого движения
        int timeSinceLastMove = 0;

        // Основное движение фигуры вниз
        public bool Update(int deltaTimeMS)
        {
            // Увеличение скорости каждый уровень
            int timeBetweenMoves = speed - Stats.Level * 10;

            timeSinceLastMove += deltaTimeMS;

            if (timeSinceLastMove < timeBetweenMoves)
                return false;

            timeSinceLastMove -= timeBetweenMoves;

            // Стирание фигуры
            curFigure.Undraw();
            // Перемещение фигуры
            curFigure.Move(Direction.Down);

            // Если фигуры столкнулась с полом или другой фигурой
            if (curFigure.CollidingWithFloor() || curFigure.CollidingWith(oldPoints))
            {
                // Перемещение в начальную позицию
                curFigure.Move(Direction.Up);
                // Показ фигуры
                curFigure.Draw();

                // Добавление точек фигуры в список прошлых фигур
                foreach (Point p in curFigure.pList)
                {
                    oldPoints.Add(p);
                }

                // Получение следующей фигуры
                GetNextFigure();
                // Спавн фигуры
                SpawnFigure();

                // Прибавление счета
                Stats.Score += 10;

                // Повышение уровня сложности каждые 100 очков
                if (Stats.Score >= multiplier)
                {
                    multiplier += 100;
                    Stats.Level++;
                    isLevelChanged = true;
                }

                isFigureChanged = true;
                isScoreChanged = true;

                // Если счет больше рекорда - рекорд обновляется
                if (Stats.Score >= Stats.HighScore)
                {
                    Stats.HighScore = Stats.Score;
                    isHighScoreChanged = true;
                }

                // Если фигура зашла за рамку - конец игры
                if (curFigure.CollidingWith(oldPoints))
                    if (curFigure.pList.Any(p => p.Y < 1))
                        return true;
            }

            // Показ фигуры
            curFigure.Draw();

            // Возвращает false если всё нормально
            return false;
        }

        // Обработка нажатых клавиш
        public void HandleKey()
        {
            if (Console.KeyAvailable)
            {
                // Стирание фигуры
                curFigure.Undraw();

                // Начальное положение фигуры
                Figure tempFigure = new Figure(curFigure);

                ConsoleKeyInfo cki = Console.ReadKey(true);
                // Обработка движений влево, вправо, поворота фигуры
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        curFigure.Rotate();
                        if (curFigure.CollidingWith(oldPoints))
                            curFigure = new Figure(tempFigure);
                        while (curFigure.CollidingWithLeftWall())
                        {
                            curFigure.Move(Direction.Right);
                        }
                        while (curFigure.CollidingWithRightWall())
                        {
                            curFigure.Move(Direction.Left);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        curFigure.Move(Direction.Left);
                        if (curFigure.CollidingWithLeftWall() || curFigure.CollidingWith(oldPoints))
                            curFigure.Move(Direction.Right);
                        break;
                    case ConsoleKey.RightArrow:
                        curFigure.Move(Direction.Right);
                        if (curFigure.CollidingWithRightWall() || curFigure.CollidingWith(oldPoints))
                            curFigure.Move(Direction.Left);
                        break;
                }
                // Показ фигуры
                curFigure.Draw();
            }
        }

        // Перенос фигур в точку спавна
        public void SpawnFigure()
        {
            curFigure.SetCoordinates(Walls.Width / 2 - 1, -3);
            nextFigure.SetCoordinates(Walls.Width / 2 - 1, -3);
        }

        // Получение следующей случайной фигуры
        public void GetNextFigure()
        {
            if (nextFigure == null)
            {
                curFigure = new Figure(figureList[rand.Next(figureList.Count)]);
            }
            else
            {
                nextFigure.Undraw();
                curFigure = new Figure(nextFigure);
            }

            nextFigure = new Figure(figureList[rand.Next(figureList.Count)]);
        }

        // Вывод следующей фигуры
        public void DisplayNextFigure(int x, int y)
        {
            if (isFigureChanged)
            {
                nextFigure.SetCoordinates(x, y);
                nextFigure.Draw();

                isFigureChanged = false;
            }
        }

        // Стирание выстроенной линии
        public void RemoveLine()
        {
            // Если нет точек - функция не выполняется
            if (oldPoints.Count == 0)
                return;

            for (int i = oldPoints.Min(p => p.Y); i < Walls.Height - 1; i++)
            {
                int count = oldPoints.Count(p => p.Y == i);

                if (count == Walls.Width - 2)
                {
                    var above = oldPoints.Where(p => p.Y < i).ToList();
                    var middle = oldPoints.Where(p => p.Y == i).ToList();
                    var below = oldPoints.Where(p => p.Y > i).ToList();

                    Flashing(middle, 4, 6);

                    foreach (Point p in oldPoints)
                    {
                        p.Undraw();
                    }

                    foreach (Point pnt in above)
                    {
                        pnt.Move(0, 1);
                    }

                    oldPoints = below.Concat(above).ToList();

                    foreach (Point p in oldPoints)
                    {
                        p.Draw();
                    }

                    Stats.Lines++;
                    Stats.Score += 50;
                    isLinesChanged = true;
                }
            }
        }

        // Мигание списка точек
        public void Flashing(List<Point> pList, int n, int speed)
        {
            for (int i = 0; i < n; i++)
            {
                foreach (Point p in pList)
                {
                    p.Undraw();
                }
                Thread.Sleep(1000 / speed);

                foreach (Point p in pList)
                {
                    p.Draw();
                }
                Thread.Sleep(1000 / speed);
            }
            foreach (Point p in pList)
            {
                p.Undraw();
            }
        }
    }
}