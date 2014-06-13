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

namespace MovieManager.Dialogs {
    /// <summary>
    /// Interaktionslogik für ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window {
        public ConfirmDialog(Window owner) {
            InitializeComponent();
            this.Owner = owner;
        }
        public ConfirmDialog(Window owner, string title, string content)
            : this(owner) {
            Binding titleBinding = new Binding();
            titleBinding.ElementName = this.Name;
            titleBinding.Path = new PropertyPath("Title");
            this.title.SetBinding(TextBlock.TextProperty, titleBinding);
            Binding contentBinding = new Binding();
            contentBinding.ElementName = this.Name;
            contentBinding.Path = new PropertyPath("Content");
            this.content.SetBinding(TextBlock.TextProperty, contentBinding);
            this.Title = title;
            this.Content = content;
        }

        #region DependencyProperties
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string Content {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(ConfirmDialog), new UIPropertyMetadata(""));
        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ConfirmDialog), new UIPropertyMetadata(""));
        #endregion
        #region RoutedEvents
        public static readonly RoutedEvent ConfirmYesEvent = EventManager.RegisterRoutedEvent("ConfirmYes", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ConfirmDialog));
        public static readonly RoutedEvent ConfirmNoEvent = EventManager.RegisterRoutedEvent("ConfirmNo", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ConfirmDialog));

        public event RoutedEventHandler ConfirmYes {
            add { AddHandler(ConfirmYesEvent, value); }
            remove { RemoveHandler(ConfirmYesEvent, value); }
        }
        public event RoutedEventHandler ConfirmNo {
            add { AddHandler(ConfirmNoEvent, value); }
            remove { RemoveHandler(ConfirmNoEvent, value); }
        }

        protected virtual void OnConfirmYes() {
            RaiseEvent(new RoutedEventArgs(ConfirmYesEvent, this));
        }
        protected virtual void OnConfirmNo() {
            RaiseEvent(new RoutedEventArgs(ConfirmNoEvent, this));
        }

        private void no_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            try {
                this.OnConfirmNo();
            } catch (Exception) {
            }
            this.Close();
        }
        private void yes_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            try {
                OnConfirmYes();
            } catch (Exception ex) {
            }
            this.Close();
        }
        #endregion
    }
}
