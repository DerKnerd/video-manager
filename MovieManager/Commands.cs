using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MovieManager {
    public static class Commands {
        static Commands() {
            edit = new RoutedUICommand("Bearbeiten", "Edit", typeof(Commands));
            edit.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            add = new RoutedUICommand("Hinzufügen", "Change", typeof(Commands));
            add.InputGestures.Add(new KeyGesture(Key.H | Key.A, ModifierKeys.Control));
        }

        private static RoutedUICommand edit;
        private static RoutedUICommand add;

        public static RoutedUICommand Edit {
            get { return edit; }
        }
        public static RoutedUICommand Add {
            get { return add; }
        }
    }
}
