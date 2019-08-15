using Microsoft.Win32;
using MiniPro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiniPro.Dialogs
{
    /// <summary>
    /// Interaction logic for Helper.xaml
    /// </summary>
    public partial class Helper : Window, INotifyPropertyChanged
    {
        private string _styler;

        public string Styler
        {
            get
            {
                return _styler;
            }
            set
            {
                _styler = value;
                OnProp("Styler");
            }
        }

        #region public constructor

        public Helper()
        {
            InitializeComponent();
            DataContext = this;
            Width = SystemParameters.PrimaryScreenWidth / 2;
            Height = SystemParameters.WorkArea.Height;
            Left = SystemParameters.WorkArea.Right - Width;
            Top = 0;
            Styler = App.WindowIconPath;
            webBrowser.Navigate((System.IO.Path.Combine(Environment.CurrentDirectory, "Help.pdf")));
        }

        #endregion


        private void OnProp(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
