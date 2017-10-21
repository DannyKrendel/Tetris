using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class Stats
    {
        public static int Score;
        public static int HighScore;
        public static int Level;
        public static int Lines;

        public static void Initialize()
        {
            Score = 0;
            Level = 1;
            Lines = 0;
        }

        public static void Display()
        {
            if (Tetris.isScoreChanged)
            {
                TextItem text = new TextItem(18, 12, "ВАШ СЧЕТ: " + Score);
                text.Show();

                Tetris.isScoreChanged = false;
            }
            if (Tetris.isHighScoreChanged)
            {
                TextItem text = new TextItem(18, 15, "ВАШ РЕКОРД: " + HighScore);
                text.Show();

                Tetris.isHighScoreChanged = false;
            }
            if (Tetris.isLevelChanged)
            {
                TextItem text = new TextItem(18, 18, "ВАШ УРОВЕНЬ: " + Level);
                text.Show();

                Tetris.isLevelChanged = false;
            }
            if (Tetris.isLinesChanged)
            {
                TextItem text = new TextItem(18, 21, "ЛИНИЙ СОБРАНО: " + Lines);
                text.Show();

                Tetris.isLinesChanged = false;
            }
        }
    }
}
