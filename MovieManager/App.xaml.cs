using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;
using System.Security.AccessControl;

namespace MovieManager {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application {
        public App() {
            this.Exit += new ExitEventHandler(App_Exit);
            this.Startup += new StartupEventHandler(App_Startup);
        }

        void App_Startup(object sender, StartupEventArgs e) {
            try {
                Directory.Delete(appDataPath, true);
            } catch {
            }
            Directory.CreateDirectory(appDataPath);
        }

        void App_Exit(object sender, ExitEventArgs e) {
            try {
                Directory.Delete(appDataPath, true);
            } catch {
            }
        }
        public readonly static string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MovieDatabase");
    }
}
