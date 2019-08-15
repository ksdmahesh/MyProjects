using MiniPro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace MiniPro.Dialogs
{
    /// <summary>
    /// Interaction logic for Expanded.xaml
    /// </summary>
    public partial class Expanded : Window, INotifyPropertyChanged
    {
        #region private variables

        private string _drager;

        private double[,] _collections = new double[0, 0];

        private Styler _styler = new Styler();

        #endregion

        #region public properties

        public Styler Styler
        {
            get
            {
                return _styler;
            }
            set
            {
                _styler = value;
                OnProperty("Styler");
            }
        }

        public string Drager
        {
            get
            {
                return _drager;
            }
            set
            {
                _drager = value;
                OnProperty("Drager");
            }
        }

        public double[,] CollectionList
        {
            get
            {
                return _collections;
            }
            set
            {
                _collections = value;
                OnProperty("CollectionList");
            }
        }

        #endregion

        #region public constructors

        public Expanded()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Expanded(double[,] collection, Styler styler)
            : this()
        {
            Styler = styler;
            CollectionList = collection;
            createRowCol(collection);
        }

        #endregion

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private methods

        private void createRowCol(double[,] list)
        {
            int row = 0, col = 1;
            if (list.Length > 0)
            {
                for (int i = 0; i <= list.GetLength(0); i++)
                {
                    Grid1.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                }
                for (int i = 0; i < list.GetLength(1) + 2; i++)
                {
                    if (i == 0 || i == list.GetLength(1) + 1)
                    {
                        Grid1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
                    }
                    else
                    {
                        Grid1.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    }
                }
                foreach (double item in list)
                {
                    if (col == list.GetLength(1) + 1)
                    {
                        if (row == list.GetLength(0))
                        {
                            break;
                        }
                        row++;
                        col = 1;
                    }
                    TextBlock textBlock = new TextBlock() { Text = item.ToString(), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Padding = new Thickness(10) };
                    Grid.SetRow(textBlock, row);
                    Grid.SetColumn(textBlock, col);
                    Grid1.Children.Add(textBlock);
                    col++;
                }
            }
            else
            {
                Grid1.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid1.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
                TextBlock textBloack = new TextBlock() { Text = "0", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                Grid.SetColumn(textBloack, 0);
                Grid.SetRow(textBloack, 0);
                Grid1.Children.Add(textBloack);
            }
            StackPanel panel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            Button OK = new Button() { Content = "  Ok  ", Margin = new Thickness(0, 20, 5, 0), HorizontalAlignment = HorizontalAlignment.Center };
            Button Copy = new Button() { Content = " Copy ", Margin = new Thickness(5, 20, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };
            OK.Click += OK_Click;
            Copy.Click += Copy_Click;
            panel.Children.Add(OK);
            panel.Children.Add(Copy);
            Grid.SetRow(panel, Grid1.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(panel, Grid1.ColumnDefinitions.Count);
            Grid1.Children.Add(panel);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            string _copyText = null;
            int row = 0, col = 0;
            if (CollectionList.Length > 0)
            {
                foreach (double item in CollectionList)
                {
                    if (col == CollectionList.GetLength(1))
                    {
                        _copyText = _copyText.Remove(_copyText.LastIndexOf("\t"), 1);
                        if (row == CollectionList.GetLength(0))
                        {
                            break;
                        }
                        row++;
                        col = 0;
                        _copyText += Environment.NewLine;
                    }
                    _copyText += item + "\t";
                    col++;
                }
                _copyText = _copyText.Remove(_copyText.LastIndexOf("\t"), 1);
            }
            else
            {
                _copyText = "0";
            }
            Clipboard.SetText(_copyText);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnProperty(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Drager == "Move")
            {
                DragMove();
            }
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Drager = "Null";
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Drager = "Move";
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logo.ContextMenu.IsOpen = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                App.HelpMe();
            }
        }

        #endregion
        
    }
}
