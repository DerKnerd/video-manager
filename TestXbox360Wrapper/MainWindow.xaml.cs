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
using XNAWrapper;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace TestXbox360Wrapper {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            xbox360controller.AButtonPressed += new Action(xbox360controller_AButtonPressed);
            //xbox360controller.AButtonReleased += new Action(xbox360controller_AButtonReleased);
        }

        void xbox360controller_AButtonReleased() {
            addInfo("A-Button Released");
        }

        void xbox360controller_AButtonPressed() {
            addInfo("A-Button Pressed");
        }

        private void addInfo(string whatInfo) {
            Info i = new Info() {
                what = whatInfo,
                when = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()
            };
            listBox1.Dispatcher.Invoke(new Action<object>(listBox1.Items.Add), i}));
        }
        ObservableCollection<Info> infos = new ObservableCollection<Info>();
        private XBox360Controller xbox360controller = new XBox360Controller();

        private void Button_Click(object sender, RoutedEventArgs e) {
            App.DoEvents();
            infos.Clear();
        }
    }
}
