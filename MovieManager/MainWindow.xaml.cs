using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieManager {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
            this.mainFrame.Content = new Overview(this);
        }

        Page backTarget;

        private void mainFrame_Navigated(object sender, NavigationEventArgs e) {
            Type mainFrameContent = this.mainFrame.Content.GetType();
            if (mainFrameContent == typeof(Overview)) {
                backButton.IsEnabled = false;
            } else {
                backButton.IsEnabled = true;
                backTarget = new Overview(this);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            mainFrame.Content = backTarget;
        }

        private void close_Click(object sender, RoutedEventArgs e) {
            App.Current.Shutdown();
        }

        private void minimize_Click(object sender, RoutedEventArgs e) {
            this.WindowState = WindowState.Minimized;
        }
    }
}
