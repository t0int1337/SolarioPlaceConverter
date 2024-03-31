using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SolarioPlaceConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String path;
        public MainWindow()
        {

            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // file dialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".rbxlx";
            dlg.Filter = "Roblox XML Place Files (.rbxlx)|*.rbxlx";
            // show dialog
            Nullable<bool> result = dlg.ShowDialog();
            // process dialog result
            if (result == true)
            {
                // Open document
                path = dlg.FileName;
                // set text box to file path
                textBox.Text = path;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // if path is not rbxlx file
            if (path == null || !path.EndsWith(".rbxlx"))
            {
                MessageBox.Show("Please select a .rbxlx file");
                return;
            }
            // replace all rbxassetid:// with http://www.solario.lol/asset/?id=
            string text = System.IO.File.ReadAllText(path);
            text = text.Replace("rbxassetid://", "http://www.solario.lol/asset/?id=");
            text = text.Replace("http://www.roblox.com/asset/?id=", "http://www.solario.lol/asset/?id=");
            text = text.Replace("assetdelivery.roblox.com", "assetdelivery.solario.lol");
            text = text.Replace("api.roblox.com", "api.solario.lol");
            text = text.Replace("assetgame.roblox.com", "assetgame.solario.lol");
            if (enablehttp.IsChecked == true)
            {
                text = text.Replace("<bool name=\"HttpEnabled\">false</bool>", "<bool name=\"HttpEnabled\">true</bool>");
            }
            // write to file (filename: placename_converted.rbxlx)
            System.IO.File.WriteAllText(path.Replace(".rbxlx", "_converted.rbxlx"), text);
            MessageBox.Show("Conversion complete! Saved as " + path.Replace(".rbxlx", "_converted.rbxlx"));
            // launch explorer to show file
            System.Diagnostics.Process.Start("explorer.exe", "/select," + path.Replace(".rbxlx", "_converted.rbxlx"));
        }
    }
}
