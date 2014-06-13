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
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace MovieManager.Dialogs {
    /// <summary>
    /// Interaktionslogik für AddGenre.xaml
    /// </summary>
    public partial class EditGenre : Window {
        public EditGenre(Window owner, Genre genre) {
            InitializeComponent();
            this.Owner = owner;
            this.bild.Text = genre.Bild;
            this.name.Text = genre.Name;
            this.genre = genre;
        }
        MovieEntities ent = new MovieEntities();

        #region RoutedEvents
        public static readonly RoutedEvent ChangeEvent = EventManager.RegisterRoutedEvent("Change", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditGenre));
        public static readonly RoutedEvent CancelEvent = EventManager.RegisterRoutedEvent("Cancel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(EditGenre));

        public event RoutedEventHandler Change {
            add { AddHandler(ChangeEvent, value); }
            remove { RemoveHandler(ChangeEvent, value); }
        }
        public event RoutedEventHandler Cancel {
            add { AddHandler(CancelEvent, value); }
            remove { RemoveHandler(CancelEvent, value); }
        }

        protected virtual void OnChange() {
            RaiseEvent(new RoutedEventArgs(ChangeEvent, this));
        }
        protected virtual void OnCancel() {
            RaiseEvent(new RoutedEventArgs(CancelEvent, this));
        }
        #endregion

        Genre genre;

        private void edit_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            genre.Name = name.Text;
            try {
                if (bild.Text != genre.Bild) {
                    string folder = System.IO.Path.Combine(System.Threading.Thread.GetDomain().BaseDirectory, "pic");
                    string file = System.IO.Path.Combine(folder, new FileInfo(bild.Text).Name + DateTime.Now.ToFileTime());
                    Debug.WriteLine(file);
                    DirectoryInfo di = new DirectoryInfo(folder);
                    di.Create();
                    di.Attributes = FileAttributes.Hidden;
                    File.Copy(bild.Text, file);
                    File.SetAttributes(file, FileAttributes.Hidden);
                    genre.Bild = file;
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            try {
                ent.changeGenre(genre.ID, genre);
            } catch (Exception ex) {
                Debug.WriteLine(ex.InnerException.Message);
            }
            OnChange();
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            OnCancel();
            this.Close();
        }

        private void bildSearch_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Cover wählen...";
            ofd.Filter = "Alle Bildformate(*.jpg, *.jpeg, *.png, *.gif, *.tiff, *.tif)|*.jpg;*.jpeg;*.png;*.gif;*.tiff;*.tif";
            if (ofd.ShowDialog(this) == true) {
                bild.Text = ofd.FileName;
            }
        }
    }
}
