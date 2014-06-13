using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MovieManager.Dialogs;
using System.Windows.Data;
using System;
using System.Diagnostics;

namespace MovieManager {
    /// <summary>
    /// Interaktionslogik für GenreDetail.xaml
    /// </summary>
    public partial class GenreDetail : Page {
        public GenreDetail(MainWindow parent) {
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, delete_Executed, delete_CanExecute));
            this.CommandBindings.Add(new CommandBinding(Commands.Edit, edit_Executed, edit_CanExecute));
            this.CommandBindings.Add(new CommandBinding(Commands.Add, add_Executed));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Play, play_Executed, play_CanExecute));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Pause, pause_Executed, pause_CanExecute));
            this.CommandBindings.Add(new CommandBinding(MediaCommands.Stop, stop_Executed));
            this.parent = parent;
        }
        public GenreDetail(MainWindow parent, Genre genre)
            : this(parent) {
            this.Title = genre.Name;
            this.filme.ItemsSource = genre.Movies;
            this.genre = genre;
        }

        void delConfirm_ConfirmYes(object sender, RoutedEventArgs e) {
            ent.deleteMovie(((Movie)filme.SelectedItem).ID);
            this.filme.ItemsSource = ent.getMoviesByGenre(this.genre);
            this.parent.Opacity = 1;
        }
        void delConfirm_ConfirmNo(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }
        ConfirmDialog delConfirm;
        AddMovie addMovie;
        EditMovie editMovie;
        MainWindow parent;
        MovieEntities ent = new MovieEntities();
        Genre genre = new Genre();
        bool loaded = false;
        bool paused = false;

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

        private void delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.delConfirm = new ConfirmDialog(parent, "Wirklich löschen?", "Sind sie sicher das sie den Film " + ((Movie)filme.SelectedItem).Titel + " löschen wollen?");
            this.delConfirm.ConfirmYes += new RoutedEventHandler(delConfirm_ConfirmYes);
            this.delConfirm.ConfirmNo += new RoutedEventHandler(delConfirm_ConfirmNo);
            this.parent.OpacityMask = new SolidColorBrush(Colors.White);
            this.parent.Opacity = 0.5;
            this.delConfirm.ShowDialog();
        }
        private void delete_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if ((Movie)filme.SelectedItem != null) {
                e.CanExecute = true;
            }
        }

        private void edit_Executed(object sender, ExecutedRoutedEventArgs e) {
            editMovie = new EditMovie(parent, filme.SelectedItem as Movie);
            this.parent.OpacityMask = new SolidColorBrush(Colors.White);
            this.parent.Opacity = 0.5;
            editMovie.Change += new RoutedEventHandler(editMovie_Change);
            editMovie.Cancel += new RoutedEventHandler(editMovie_Cancel);
            this.editMovie.ShowDialog();
        }

        void editMovie_Cancel(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }
        private void edit_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if ((Movie)filme.SelectedItem != null) {
                e.CanExecute = true;
            }
        }

        private void add_Executed(object sender, ExecutedRoutedEventArgs e) {
            addMovie = new AddMovie(parent, genre);
            addMovie.Add += new RoutedEventHandler(addMovie_Add);
            addMovie.Cancel += new RoutedEventHandler(addMovie_Cancel);
            this.parent.OpacityMask = new SolidColorBrush(Colors.White);
            this.parent.Opacity = 0.5;
            addMovie.ShowDialog();
        }
        #endregion
        void editMovie_Change(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }

        void addMovie_Cancel(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }

        void addMovie_Add(object sender, RoutedEventArgs e) {
            filme.ItemsSource = ent.getMoviesByGenre(genre.ID);
            this.parent.Opacity = 1;
        }

        private void moviePlayer_MediaOpened(object sender, RoutedEventArgs e) {
            moviePlayer.Stop();
            loaded = true;
            paused = true;
        }

        private void filme_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            loaded = false;
            paused = false;
        }

        private void search_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (search.Text.ToLower().Equals("suche")) {
                search.Text = "";
                search.FontStyle = FontStyles.Normal;
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e) {
            if (!search.Text.ToLower().Equals("suche")) {
                filme.ItemsSource = ent.getMoviesByKeyword(search.Text);
            }
        }

        private void search_LostFocus(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(search.Text)) {
                search.Text = "Suche";
                search.FontStyle = FontStyles.Italic;
            }
        }
    }
}
