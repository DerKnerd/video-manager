using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using Shell32;
using System.Security.AccessControl;
using System.Windows.Shell;

namespace MovieManager.Dialogs {
    /// <summary>
    /// Interaktionslogik für AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window {
        public AddMovie(Window owner) {
            InitializeComponent();
            this.Owner = owner;
        }
        public AddMovie(Window owner, Genre genre)
            : this(owner) {
            this.genre = genre;
        }
        MovieEntities ent = new MovieEntities();
        MediaPlayer mp = new MediaPlayer();
        Genre genre;

        #region RoutedEvents
        public static readonly RoutedEvent AddEvent = EventManager.RegisterRoutedEvent("Change", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AddMovie));
        public static readonly RoutedEvent CancelEvent = EventManager.RegisterRoutedEvent("Cancel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AddMovie));

        public event RoutedEventHandler Add {
            add { AddHandler(AddEvent, value); }
            remove { RemoveHandler(AddEvent, value); }
        }
        public event RoutedEventHandler Cancel {
            add { AddHandler(CancelEvent, value); }
            remove { RemoveHandler(CancelEvent, value); }
        }

        protected virtual void OnAdd() {
            RaiseEvent(new RoutedEventArgs(AddEvent, this));
        }
        protected virtual void OnCancel() {
            RaiseEvent(new RoutedEventArgs(CancelEvent, this));
        }
        #endregion

        private void add_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Movie movie = new Movie();
            if (genre != null)
                movie.Genre = ent.getGenreById(this.genre.ID);
            movie.Grose = Convert.ToInt64(grose.Text);
            movie.Inhalt = inhalt.Text;
            movie.Lange = lange.Text;
            movie.Pfad = pfad.Text;
            movie.Stichworte = stichworte.Text;
            movie.Titel = titel.Text;
            movie.Breite = Convert.ToInt32(this.breite.Text);
            movie.Hohe = Convert.ToInt32(this.hohe.Text);
            try {
                string folder = System.IO.Path.Combine(System.Threading.Thread.GetDomain().BaseDirectory, "pic");
                string file = System.IO.Path.Combine(folder, new FileInfo(image.Text).Name + DateTime.Now.ToFileTime());
                Debug.WriteLine(file);
                DirectoryInfo di = new DirectoryInfo(folder);
                di.Create();
                di.Attributes = FileAttributes.Hidden;
                File.Copy(image.Text, file);
                File.SetAttributes(file, FileAttributes.Hidden);
                movie.Bild = file;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            try {
                ent.addMovie(movie);
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            this.OnAdd();
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            this.OnCancel();
            this.Close();
        }

        private void inhalt_TextChanged(object sender, TextChangedEventArgs e) {
            inhalt.ScrollToEnd();
        }

        private void pfadSearch_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Film wählen...";
            string alleFormate = "Alle unterstützten Formate(*.mkv, *.mka, *.wmv, *.asf, *.avi, *.mpeg, *.mpg, *.mp4)|*.mkv;*.mka;*.wmv;*.avi;*.asf;*.mpeg;*.mpg;*.mp4";
            string matroskaFormate = "Matroska Videoformat(*.mkv, *.mka)|*.mkv;*.mka";
            string windowsFormate = "WMV und AVI Formate(*.wmv, *.asf, *.avi)|*.wmv;*.asf;*.avi";
            string mpegFormate = "MPEG Videoformate(*.mpeg, *.mpg, *.mp4)|*.mpeg;*.mpg;*.mp4";
            ofd.Filter = alleFormate + "|" + matroskaFormate + "|" + windowsFormate + "|" + mpegFormate;
            if (ofd.ShowDialog(this) == true) {
                if (this.Owner.TaskbarItemInfo == null)
                    this.Owner.TaskbarItemInfo = new TaskbarItemInfo();
                this.Owner.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
                pfad.Text = ofd.FileName;
                Stream s = ofd.OpenFile();
                grose.Text = s.Length.ToString();
                mp = new MediaPlayer();
                mp.MediaOpened += new EventHandler(mp_MediaOpened);
                dataScroll.Opacity = 0.5;
                dataScroll.IsEnabled = false;
                add.IsEnabled = false;
                cancel.IsEnabled = false;
                infoRead.Visibility = Visibility.Visible;
                mp.Close();
                mp.Open(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                if (titel.Text == null || titel.Text == "") {
                    try {
                        Shell sh = new Shell32.Shell();
                        Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(ofd.FileName));
                        FolderItem item = dir.ParseName(System.IO.Path.GetFileName(ofd.FileName));
                        string det = dir.GetDetailsOf(item, 21);
                        titel.Text = det;
                    } catch {
                    }
                }
            }
        }

        void mp_MediaOpened(object sender, EventArgs e) {
            lange.Text = mp.NaturalDuration.ToString();
            hohe.Text = mp.NaturalVideoHeight.ToString();
            breite.Text = mp.NaturalVideoWidth.ToString();
            dataScroll.Opacity = 1;
            dataScroll.IsEnabled = true;
            add.IsEnabled = true;
            cancel.IsEnabled = true;
            infoRead.Visibility = Visibility.Collapsed;
            this.Owner.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
        }

        private void imageSearch_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Cover wählen...";
            ofd.Filter = "Alle Bildformate(*.jpg, *.jpeg, *.png, *.gif, *.tiff, *.tif)|*.jpg;*.jpeg;*.png;*.gif;*.tiff;*.tif";
            if (ofd.ShowDialog(this) == true) {
                image.Text = ofd.FileName;
            }
        }
    }
}
