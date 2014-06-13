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
using MovieManager.Dialogs;
using System.Windows.Media.Animation;

namespace MovieManager {
    /// <summary>
    /// Interaktionslogik für AlleFilme.xaml
    /// </summary>
    public partial class Overview : Page {
        private enum CurrentItem {
            Movies,
            Genres
        }
        public Overview(MainWindow parent)
            : this() {
            this.parent = parent;
        }

        private Overview() {
            InitializeComponent();
            this.genres.ItemsSource = ent.getAllGenres();
            this.movies.ItemsSource = ent.getAllMovies();
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, delete_Executed, delete_CanExecute));
            this.CommandBindings.Add(new CommandBinding(Commands.Edit, edit_Executed, edit_CanExecute));
            this.CommandBindings.Add(new CommandBinding(Commands.Add, add_Executed));
            currentItem = CurrentItem.Genres;
        }

        MainWindow parent;
        MovieEntities ent = new MovieEntities();
        ConfirmDialog delConfirm;
        CurrentItem currentItem;

        #region Commands
        private void delete_Executed(object sender, ExecutedRoutedEventArgs e) {
            string title = "Wirklich löschen?";
            string content = "";
            if (currentItem == CurrentItem.Genres) {
                content = "Sind Sie sicher das Sie das Genre " + ((Genre)genres.SelectedItem).Name + " löschen wollen?";
            } else if (currentItem == CurrentItem.Movies) {
                content = "Sind Sie sicher das Sie den Film " + ((Movie)movies.SelectedItem).Titel + " löschen wollen?";
            }
            this.delConfirm = new ConfirmDialog(parent, title, content);
            this.delConfirm.ConfirmYes += new RoutedEventHandler(delConfirm_ConfirmYes);
            this.delConfirm.ConfirmNo += new RoutedEventHandler(delConfirm_ConfirmNo);
            this.parent.OpacityMask = new SolidColorBrush(Colors.White);
            this.parent.Opacity = 0.5;
            this.delConfirm.ShowDialog();
        }

        private void delete_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (currentItem == CurrentItem.Genres) {
                if ((Genre)genres.SelectedItem != null) {
                    e.CanExecute = true;
                }
            } else if (currentItem == CurrentItem.Movies) {
                if ((Movie)movies.SelectedItem != null) {
                    e.CanExecute = true;
                }
            }
        }

        private void delConfirm_ConfirmYes(object sender, RoutedEventArgs e) {
            if (currentItem == CurrentItem.Genres) {
                Genre genre = (Genre)genres.SelectedItem;
                ent.deleteGenre(genre);
            } else if (currentItem == CurrentItem.Movies) {
                Movie movie = (Movie)genres.SelectedItem;
                ent.deleteMovie(movie);
            }
            this.genres.ItemsSource = ent.getAllGenres();
            this.movies.ItemsSource = ent.getAllMovies();
            this.parent.Opacity = 1;
        }
        private void delConfirm_ConfirmNo(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }

        private void edit_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (currentItem == CurrentItem.Genres) {
                EditGenre editGenre = new EditGenre(parent, genres.SelectedItem as Genre);
                this.parent.OpacityMask = new SolidColorBrush(Colors.White);
                this.parent.Opacity = 0.5;
                editGenre.Change += new RoutedEventHandler(editGenre_Change);
                editGenre.Cancel += new RoutedEventHandler(ag_Cancel);
                editGenre.ShowDialog();
            } else if (currentItem == CurrentItem.Movies) {
                EditMovie editMovie = new EditMovie(parent, movies.SelectedItem as Movie);
                this.parent.OpacityMask = new SolidColorBrush(Colors.White);
                this.parent.Opacity = 0.5;
                editMovie.Change += new RoutedEventHandler(editMovie_Change);
                editMovie.Cancel += new RoutedEventHandler(ag_Cancel);
                editMovie.ShowDialog();
            }
        }

        private void editMovie_Change(object sender, RoutedEventArgs e) {
            this.genres.ItemsSource = ent.getAllGenres();
            this.movies.ItemsSource = ent.getAllMovies();
            this.parent.Opacity = 1;
        }

        private void edit_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            if (currentItem == CurrentItem.Genres) {
                if ((Genre)genres.SelectedItem != null) {
                    e.CanExecute = true;
                }
            } else if (currentItem == CurrentItem.Movies) {
                if ((Movie)movies.SelectedItem != null) {
                    e.CanExecute = true;
                }
            }
        }

        private void editGenre_Change(object sender, RoutedEventArgs e) {
            this.genres.ItemsSource = ent.getAllGenres();
            this.movies.ItemsSource = ent.getAllMovies();
            this.parent.Opacity = 1;
        }

        private void add_Executed(object sender, ExecutedRoutedEventArgs e) {
            this.parent.OpacityMask = new SolidColorBrush(Colors.White);
            this.parent.Opacity = 0.5;
            if (currentItem == CurrentItem.Genres) {
                AddGenre ag = new AddGenre(parent);
                ag.Add += new RoutedEventHandler(ag_Add);
                ag.Cancel += new RoutedEventHandler(ag_Cancel);
                ag.ShowDialog();
            } else if (currentItem == CurrentItem.Movies) {
                AddMovie am = new AddMovie(parent);
                am.Add += new RoutedEventHandler(am_Add);
                am.Cancel += new RoutedEventHandler(ag_Cancel);
                am.ShowDialog();
            }
        }

        private void ag_Add(object sender, RoutedEventArgs e) {
            genres.ItemsSource = ent.getAllGenres();
            this.parent.Opacity = 1;
        }
        private void am_Add(object sender, RoutedEventArgs e) {
            movies.ItemsSource = ent.getAllMovies();
            this.parent.Opacity = 1;
        }

        void ag_Cancel(object sender, RoutedEventArgs e) {
            this.parent.Opacity = 1;
        }
        #endregion
        private void genres_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (genres.SelectedItem != null)
                parent.mainFrame.Content = new GenreDetail(parent, genres.SelectedItem as Genre);
        }
        private void movies_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (movies.SelectedItem != null)
                parent.mainFrame.Content = new MovieDetail(parent, movies.SelectedItem as Movie);
        }

        private void type_Changed(object sender, SelectionChangedEventArgs e) {
            if ((sender as TabControl).SelectedItem == movieItem) {
                currentItem = CurrentItem.Movies;
            } else if ((sender as TabControl).SelectedItem == genreItem) {
                currentItem = CurrentItem.Genres;
            }
        }

        private void search_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (search.Text.ToLower().Equals("suche")) {
                search.Text = "";
                search.FontStyle = FontStyles.Normal;
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e) {
            if (!search.Text.ToLower().Equals("suche")) {
                switch (currentItem) {
                    case CurrentItem.Movies:
                        movies.ItemsSource = ent.getMoviesByKeyword(search.Text);
                        break;
                    case CurrentItem.Genres:
                        genres.ItemsSource = ent.getGenresByKeyword(search.Text);
                        break;
                }
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
