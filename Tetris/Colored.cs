using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // Класс, который наследуют все цветные объекты
    class Colored
    {
        protected ConsoleColor bgColor = ConsoleColor.Black;
        protected ConsoleColor fgColor = ConsoleColor.White;

        // Метод, устанавливающий цвета объекта
        public void SetColor(ConsoleColor bgColor, ConsoleColor fgColor)
        {
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }
    }
}