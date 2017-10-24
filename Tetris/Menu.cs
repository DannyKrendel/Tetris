using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tetris
{
    class Menu : Colored
    {
        // Список пунктов меню
        List<TextItem> items;

        // Выбранный пункт
        int selectedItem = 0;

        public int SelectedItem
        {
            get => selectedItem;
            set
            {
                // Предотвращение обращения к несуществующему пункту
                if (value >= 0 && value < items.Count)
                {
                    selectedItem = value;
                }
            }
        }

        // Помещение пунктов в список
        public Menu(List<TextItem> items)
        {
            this.items = new List<TextItem>(items);
        }

        // Вывод меню
        public void Show()
        {
            foreach (TextItem item in items)
            {
                item.SetColor(bgColor, fgColor);
                item.Show();
            }
        }

        // Выбор пункта
        public int SelectItem()
        {
            items[SelectedItem].ReverseColors();
            items[SelectedItem].Show();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    items[SelectedItem].ReverseColors();
                    items[SelectedItem].Show();
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.DownArrow:
                            SelectedItem++;
                            break;
                        case ConsoleKey.UpArrow:
                            SelectedItem--;
                            break;
                        case ConsoleKey.Enter:
                            items[SelectedItem].Flashing(6, 5);
                            return SelectedItem;
                    }
                    items[SelectedItem].ReverseColors();
                    items[SelectedItem].Show();
                }
            }
        }
    }
}