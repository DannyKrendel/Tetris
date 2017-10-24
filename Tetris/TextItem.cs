using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class TextItem : Colored
    {
        int x;
        int y;
        string str;

        public TextItem(int x, int y, string str)
        {
            this.x = x;
            this.y = y;
            this.str = str;
        }

        // Показ текста
        public void Show()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.Write(str + "\r");
            Console.ResetColor();
        }

        // Мигание текста
        public void Flashing(int n, int speed)
        {
            for (int i = 0; i < n; i++)
            {
                Show();
                ReverseColors();

                Thread.Sleep(1000 / speed);
            }
            Console.Clear();
        }

        // Меняет местами цвета
        public void ReverseColors()
        {
            ConsoleColor temp = bgColor;
            bgColor = fgColor;
            fgColor = temp;
        }
    }
}