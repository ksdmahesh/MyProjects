using MiniPro.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MiniPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region public variables

        public static string TitleName = "Cloud Calculator", HelpIconPath = @"Cloud Calculator.png", WindowIconPath = "Source\\Cloud Calculator.png";

        #endregion

        #region public methods

        public static void HelpMe()
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "Help.pdf");
            Regex reg = new Regex(@"\\");
            path = "file:///" + reg.Replace(path, "/");
            Process.Start(path);
        }

        #endregion

        #region private methods

        private void Helper_Click(object sender, RoutedEventArgs e)
        {
            HelpMe();
        }

        #endregion
    }
}
