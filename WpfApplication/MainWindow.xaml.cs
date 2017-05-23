using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication.Data;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public ObservableCollection<Files> FileCollection { get; set; }
        int defaultCounter = 1;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            FileCollection = new ObservableCollection<Files>();
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                FileCollection.Clear();

                IEnumerable<string> files = (IEnumerable<string>)e.Data.GetData(DataFormats.FileDrop);
                foreach (var item in files)
                {
                    FileCollection.Add(new Files(item));
                }

                var col = (listView.View as GridView).Columns[0];
                if (double.IsNaN(col.Width))
                    col.Width = col.ActualWidth;
                col.Width = double.NaN;
                updateNewName();
            }
        }

        private void tb_newName_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateNewName();
        }

        void updateNewName()
        {
            int counter = defaultCounter;
            for (int i = 0; i < FileCollection.Count; i++)
            {
                string newName = tb_newName.Text;
                newName = Regex.Replace(newName, "[*]", counter.ToString().PadLeft(FileCollection.Count.ToString().Length, '0'));
                FileCollection[i].NewName = newName;

                counter++;
            }
            listView.Items.Refresh();

            var col = (listView.View as GridView).Columns[1];
            if (double.IsNaN(col.Width))
                col.Width = col.ActualWidth;
            col.Width = double.NaN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in FileCollection)
            {
                string newPath = System.IO.Path.Combine(item.DirectoryPath, item.NewName + item.Extension);
                File.Move(item.FilePath, newPath);
            }
            FileCollection.Clear();
        }
    }
}
