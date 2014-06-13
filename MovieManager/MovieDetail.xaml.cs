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
    /// Interaktionslogik für MovieDetail.xaml
    /// </summary>
    public partial class MovieDetail : Page {
        public MovieDetail(MainWindow parent, Movie movie) {
            InitializeComponent();
            this.parent = parent;
            this.content.DataContext = movie;
            this.Title = movie.Titel;
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, play_Executed, play_CanExecute));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, pause_Executed, pause_CanExecute));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, stop_Executed));
        }
        MainWindow parent;
        bool loaded = false;
        bool paused = false;

        private void moviePlayer_MediaOpened(object sender, RoutedEventArgs e) {
            paused = true;
            loaded = true;
        }
        #region Commands
        private void play_Executed(object sender, ExecutedRoutedEventArgs e) {
            moviePlayer.Play();
            paused = false;
        }
        private void play_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = loaded && paused;
        }

        private void pause_Executed(object sender, ExecutedRoutedEventArgs e) {
            moviePlayer.Pause();
            paused = true;
        }
        private void pause_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = loaded && !paused;
        }

        private void stop_Executed(object sender, ExecutedRoutedEventArgs e) {
            moviePlayer.Stop();
            paused = true;
        }
        #endregion
    }
}
