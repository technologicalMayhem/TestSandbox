using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppUtil
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; private set; }
        public string Header { get; set; } = null;

        public Menu(IEnumerable<MenuItem> menuItems)
        {
            MenuItems = new List<MenuItem>(menuItems);
        }

        public void ShowMenu()
        {
            if (Header != null) Console.WriteLine(Header);

            var num = 0;
            foreach (var item in MenuItems)
            {
                Console.WriteLine($"{++num}. {item.Descriptor}");
            }

            Console.WriteLine();
            Console.Write($"Please make your choice [1 - {MenuItems.Count}]: ");
            var choice = 0;
            while (true)
            {
                var text = Console.ReadLine();
                if (text.ToCharArray().All(x => char.IsNumber(x)))
                {
                    choice = int.Parse(text);
                    if (choice > 0 && choice <= MenuItems.Count)
                    {
                        break;
                    }
                }
                Console.Write($"Please enter a valid number [1 - {MenuItems.Count}]: ");
            }
            MenuItems[choice - 1].Action.Invoke();
        }
    }

    public struct MenuItem
    {
        public string Descriptor { get; set; }
        public Action Action { get; set; }
        public bool Disabled { get; set; }

        public MenuItem(string descriptor, Action action) : this()
        {
            Descriptor = descriptor;
            Action = action;
        }

        public MenuItem(string descriptor, Action action, bool disabled) : this()
        {
            Descriptor = descriptor;
            Action = action;
            Disabled = disabled;
        }
    }
}
