using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            MenuSelect();
        }

        // Выбор в меню
        static void MenuSelect()
        {
            Console.Clear();

            // Установка размера окна меню
            int width = 25;
            int height = 5;
            Console.SetWindowSize(width, height);

            // Название игры и пункты меню
            TextItem title = new TextItem(1, 1, "******T E T R I S******");

            // Передача пунктов в меню
            Menu menu = new Menu(new List<TextItem>
            {
                new TextItem(1, 2, "      НАЧАТЬ ИГРУ      "),
                new TextItem(1, 3, "         ВЫХОД         ")
            });

            // Цвета
            title.SetColor(ConsoleColor.DarkBlue, ConsoleColor.Green);
            menu.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);

            // Рамка вокруг меню
            FrameDisplay(0, 0, width, height, '#');

            // Показ названия и меню
            title.Show();
            menu.Show();

            // Индекс выбранного пункта
            int selected = menu.SelectItem();

            switch (selected)
            {
                case 0:
                    Game();
                    break;
                case 1:
                    Exit();
                    break;
            }
        }

        static void Game()
        {
            // Размеры игрового поля
            int width = 16;
            int height = 25;

            // Установка размера окна игры
            Console.SetWindowSize(width + 21, height);

            // Обнуление статистики
            Stats.Initialize();

            // Символ рамки
            char wallCh = '#';

            // Игровая рамка
            Walls walls = new Walls(width, height, wallCh);

            Tetris tetris = new Tetris();

            Stopwatch stopwatch = new Stopwatch();

            // Отображение игровой рамки
            walls.Draw();
            // Отображение рамки статистики
            FrameDisplay(width, 0, 21, height, wallCh);
            // Отображение рамки следующей фигуры
            FrameDisplay(width + 5, 4, 11, 6, wallCh);

            TextItem text = new TextItem(width + 2, 2, "СЛЕДУЮЩАЯ ФИГУРА:");
            text.SetColor(ConsoleColor.Black, ConsoleColor.White);
            text.Show();

            stopwatch.Start();

            long timeAtPreviousFrame = stopwatch.ElapsedMilliseconds;

            while (true)
            {
                int deltaTimeMS = (int)(stopwatch.ElapsedMilliseconds - timeAtPreviousFrame);
                timeAtPreviousFrame = stopwatch.ElapsedMilliseconds;

                // Отображение следующей фигуры
                tetris.DisplayNextFigure(26, 5);

                // Отображение статистики
                Stats.Display();

                // Если вернёт true игра закончится
                if (tetris.Update(deltaTimeMS))
                    GameOver();

                stopwatch.Stop();
                tetris.RemoveLine();
                stopwatch.Start();

                // Выбор направления движения
                tetris.HandleKey();
            }
        }

        // Функция для показа рамки
        static void FrameDisplay(int x, int y, int width, int height, char ch, ConsoleColor bg = ConsoleColor.Black, ConsoleColor fg = ConsoleColor.Gray)
        {
            for (int i = y; i < y + height; i++)
            {
                Console.SetCursorPosition(x, i);
                if (i == y || i == y + height - 1)
                    Console.Write(new string(ch, width));
                else
                    Console.Write(ch + new string(' ', width - 2) + ch);
            }
        }

        static void GameOver()
        {
            // Game Over текст
            List<TextItem> text = new List<TextItem>
            {
                new TextItem(4, 5, "============================"),
                new TextItem(4, 6, " И Г Р А    О К О Н Ч Е Н А "),
                new TextItem(4, 7, "  Попробовать ещё раз? Y/N  "),
                new TextItem(4, 8, "============================")
            };

            Menu gameOver = new Menu(text);
            gameOver.SetColor(ConsoleColor.DarkRed, ConsoleColor.Yellow);
            gameOver.Show();

            ConsoleKeyInfo cki;

            do
            {
                cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    Game();
                }
                else if (cki.Key == ConsoleKey.N)
                {
                    MenuSelect();
                }
            } while (cki.Key != ConsoleKey.Y && cki.Key != ConsoleKey.N);
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
