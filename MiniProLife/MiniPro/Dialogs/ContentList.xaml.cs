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
    /// Interaction logic for ContentList.xaml
    /// </summary>
    public partial class ContentList : Window, INotifyPropertyChanged
    {
        #region private variables

        private Styler _styler = new Styler();

        private List<double> _tempListValue = new List<double>();

        private double _double;

        private int _count = 0, _row = 0, _col = 0;

        private double[,] _tempMatListValue;

        private string _drager;

        #endregion

        #region public constructor

        public ContentList()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ContentList(int count, int item, Styler styler)
            : this()
        {
            Styler = styler;
            called(count, Styler.Lists[item]);
            Styler.windowState = WindowState.Minimized;
        }

        public ContentList(int m, int n, int item, Styler styler)
            : this()
        {
            Styler = styler;
            TempMatListValue = new double[m, n];
            Mat(m, n, Styler.MatList[item]);
            Styler.windowState = WindowState.Minimized;
        }

        #endregion

        #region public properties

        public string Drager
        {
            get
            {
                return _drager;
            }
            set
            {
                _drager = value;
                OnPropertyChanged("Drager");
            }
        }

        public Styler Styler
        {
            get
            {
                return _styler;
            }
            set
            {
                _styler = value;
                OnPropertyChanged("Styler");
            }
        }

        public List<double> TempListValue
        {
            get
            {
                return _tempListValue;
            }
            set
            {
                _tempListValue = value;
                OnPropertyChanged("TempListValue");
            }
        }

        public double[,] TempMatListValue
        {
            get
            {
                return _tempMatListValue;
            }
            set
            {
                _tempMatListValue = value;
                OnPropertyChanged("TempMatListValue");
            }
        }

        #endregion

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private methods

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        private void Mat(int m, int n, double[,] item)
        {
            _count = 0;
            RowDefinition rowDefinition1 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition2 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition3 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition4 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition5 = new RowDefinition() { Height = GridLength.Auto };
            Grid1.RowDefinitions.Add(rowDefinition1);
            Grid1.RowDefinitions.Add(rowDefinition2);
            Grid1.RowDefinitions.Add(rowDefinition3);
            Grid1.RowDefinitions.Add(rowDefinition4);
            Grid1.RowDefinitions.Add(rowDefinition5);
            //TextBlock textBlock = new TextBlock() { Margin = new Thickness(5, 20, 20, 20), FontSize = 20, Text = "Sample 1 Alert:" };
            Separator separator = new Separator() { Margin = new Thickness(0, 25, 0, 25) };
            Separator separator1 = new Separator() { Margin = new Thickness(0, 25, 0, 25) };
            StackPanel stackPanel = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Center, Orientation = Orientation.Horizontal };
            StackPanel stackPanel1 = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch };
            ScrollViewer scrollViewer = new ScrollViewer() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, Height = 170, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Center };
            scrollViewer.Content = stackPanel1;
            Button Back = new Button() { Content = " Back ", Margin = new Thickness(0, 0, 5, 0) };
            Button Next = new Button() { Content = " Done ", Margin = new Thickness(5, 0, 5, 0) };
            Button Close = new Button() { Content = " Close ", Margin = new Thickness(5, 0, 0, 0) };
            Back.Click += Back_Click;
            Next.Click += Next_Click;
            Close.Click += Close_Click;
            stackPanel.Children.Add(Back);
            stackPanel.Children.Add(Next);
            stackPanel.Children.Add(Close);
            //Grid.SetRow(textBlock, 0);
            Grid.SetRow(separator, 1);
            Grid.SetRow(scrollViewer, 2);
            Grid.SetRow(separator1, 3);
            Grid.SetRow(stackPanel, 4);
            //Grid1.Children.Add(textBlock);
            Grid1.Children.Add(separator);
            Grid1.Children.Add(scrollViewer);
            Grid1.Children.Add(separator1);
            Grid1.Children.Add(stackPanel);
            double[,] tempList = new double[m, n];
            _row = 0;
            _col = 0;
            if (m == item.GetLength(0) && n == item.GetLength(1))
            {
                while (true)
                {
                    Grid grid = new Grid();
                    TextBlock textBlockContent = new TextBlock() { Text = "a" + (_row + 1) + (_col + 1), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 10, 5, 10) };
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 10, 10), Height = 25 };
                    textBox.TextChanged += new TextChangedEventHandler(MatTextChanged);
                    textBox.PreviewTextInput += new TextCompositionEventHandler(MatTextChanged);
                    Grid.SetColumn(textBlockContent, 0);
                    Grid.SetColumn(textBox, 1);
                    try
                    {
                        textBox.Text = Convert.ToString(item[_row, _col]);
                    }
                    catch (Exception) { }
                    Styler.ListCount.Add(textBox.Text.Length);
                    tempList[_row, _col] = item[_row, _col];
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Add(textBlockContent);
                    grid.Children.Add(textBox);
                    stackPanel1.Children.Add(grid);
                    if (_col == n - 1)
                    {
                        _col = -1;
                        if (_row == m - 1)
                        {
                            break;
                        }
                        _row++;
                    }
                    _col++;
                }
            }
            else
            {
                while (true)
                {
                    Grid grid = new Grid();
                    TextBlock textBlockContent = new TextBlock() { Text = "a" + (_row + 1) + (_col + 1), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 10, 5, 10) };
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 5, 10), Height = 25 };
                    textBox.TextChanged += new TextChangedEventHandler(MatTextChanged);
                    textBox.PreviewTextInput += new TextCompositionEventHandler(MatTextChanged);
                    Grid.SetColumn(textBlockContent, 0);
                    Grid.SetColumn(textBox, 1);
                    try
                    {
                        textBox.Text = "0";
                    }
                    catch (Exception) { }
                    Styler.ListCount.Add(textBox.Text.Length);
                    tempList[_row, _col] = item[_row, _col];
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Add(textBlockContent);
                    grid.Children.Add(textBox);
                    stackPanel1.Children.Add(grid);
                    if (_col == n - 1)
                    {
                        _col = -1;
                        if (_row == m - 1)
                        {
                            break;
                        }
                        _row++;
                    }
                    _col++;
                }
            }
            Styler.MatList.RemoveAt(Styler.Indexes);
            Styler.MatList.Insert(Styler.Indexes, tempList);
            if (TempMatListValue.Length == Styler.MatList[Styler.Indexes].Length)
            {
                _row = 0; _col = 0;
                foreach (double content in Styler.MatList[Styler.Indexes])
                {
                    TempMatListValue[_row, _col] = content;
                    if (_col == n - 1)
                    {
                        _col = -1;
                        if (_row == m - 1)
                        {
                            break;
                        }
                        _row++;
                    }
                    _col++;
                }
            }
            else
            {
                TempMatListValue = new double[m, n];
                _row = 0; _col = 0;
                foreach (double content in Styler.MatList[Styler.Indexes])
                {
                    TempMatListValue[_row, _col] = content;
                    if (_col == n - 1)
                    {
                        _col = -1;
                        if (_row == m - 1)
                        {
                            break;
                        }
                        _row++;
                    }
                    _col++;
                }
            }
        }

        private void MatTextChanged(object sender, TextCompositionEventArgs e)
        {
            HandleText(e, false);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Styler.windowState = WindowState.Normal;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (Styler.StatVisible)
            {
                Styler.Lists.RemoveAt(Styler.Indexes);
                Styler.Lists.Insert(Styler.Indexes, TempListValue);
            }
            else if (Styler.MatVisible)
            {
                Styler.MatList.RemoveAt(Styler.Indexes);
                Styler.MatList.Insert(Styler.Indexes, TempMatListValue);
            }
            Close();
            Styler.windowState = WindowState.Normal;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            List list = new List(Styler);
            list.ShowDialog();
        }

        private void called(int count, List<double> item)
        {
            _count = 0;
            RowDefinition rowDefinition1 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition2 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition3 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition4 = new RowDefinition() { Height = GridLength.Auto };
            RowDefinition rowDefinition5 = new RowDefinition() { Height = GridLength.Auto };
            Grid1.RowDefinitions.Add(rowDefinition1);
            Grid1.RowDefinitions.Add(rowDefinition2);
            Grid1.RowDefinitions.Add(rowDefinition3);
            Grid1.RowDefinitions.Add(rowDefinition4);
            Grid1.RowDefinitions.Add(rowDefinition5);
            //TextBlock textBlock = new TextBlock() { Margin = new Thickness(5, 20, 20, 20), FontSize = 20, Text = "Sample 1 Alert:" };
            Separator separator = new Separator() { Margin = new Thickness(0, 25, 0, 25) };
            Separator separator1 = new Separator() { Margin = new Thickness(0, 25, 0, 25) };
            StackPanel stackPanel = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Center, Orientation = Orientation.Horizontal };
            StackPanel stackPanel1 = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch };
            ScrollViewer scrollViewer = new ScrollViewer() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, Height = 170, VerticalContentAlignment = VerticalAlignment.Center };
            scrollViewer.Content = stackPanel1;
            Button Back = new Button() { Content = " Back ", Margin = new Thickness(0, 0, 5, 0) };
            Button Next = new Button() { Content = " Done ", Margin = new Thickness(5, 0, 5, 0) };
            Button Close = new Button() { Content = " Close ", Margin = new Thickness(5, 0, 0, 0) };
            Back.Click += Back_Click;
            Next.Click += Next_Click;
            Close.Click += Close_Click;
            stackPanel.Children.Add(Back);
            stackPanel.Children.Add(Next);
            stackPanel.Children.Add(Close);
           // Grid.SetRow(textBlock, 0);
            Grid.SetRow(separator, 1);
            Grid.SetRow(scrollViewer, 2);
            Grid.SetRow(separator1, 3);
            Grid.SetRow(stackPanel, 4);
            //Grid1.Children.Add(textBlock);
            Grid1.Children.Add(separator);
            Grid1.Children.Add(scrollViewer);
            Grid1.Children.Add(separator1);
            Grid1.Children.Add(stackPanel);
            List<double> tempList = new List<double>();
            if (count == item.Count)
            {
                while (_count < count)
                {
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 5, 10), Height = 25, Name = "TextBox" + _count };
                    textBox.TextChanged += textBox_TextChanged;
                    textBox.PreviewTextInput += textBox_PreviewTextInput;
                    try
                    {
                        textBox.Text = Convert.ToString(item[_count]);
                    }
                    catch (Exception) { }
                    Styler.ListCount.Add(textBox.Text.Length);
                    tempList.Add(item[_count]);
                    stackPanel1.Children.Add(textBox);
                    _count++;
                }
            }
            else
            {
                while (_count < count)
                {
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 5, 10), Height = 25, Name = "TextBox" + _count };
                    textBox.TextChanged += textBox_TextChanged;
                    textBox.PreviewTextInput += textBox_PreviewTextInput;
                    try
                    {
                        textBox.Text = "0";
                    }
                    catch (Exception) { }
                    Styler.ListCount.Add(textBox.Text.Length);
                    tempList.Add(Convert.ToDouble(textBox.Text));
                    stackPanel1.Children.Add(textBox);
                    _count++;
                }
            }
            Styler.Lists.RemoveAt(Styler.Indexes);
            Styler.Lists.Insert(Styler.Indexes, tempList);
            if (TempListValue.Count == Styler.Lists[Styler.Indexes].Count)
            {
                int calls = 0;
                foreach (double content in Styler.Lists[Styler.Indexes])
                {
                    TempListValue.RemoveAt(calls);
                    TempListValue.Insert(calls, content);
                    calls++;
                }
            }
            else
            {
                TempListValue.Clear();
                foreach (double content in Styler.Lists[Styler.Indexes])
                {
                    TempListValue.Add(content);
                }
            }
        }

        private void HandleText(TextCompositionEventArgs e, bool complex)
        {
            if (!complex)
            {
                if (!"0123456789.".Contains(e.Text))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!"0123456789.iI+-*/()".Contains(e.Text))
                {
                    e.Handled = true;
                }
            }
        }

        void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandleText(e, false);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int indexer = ((StackPanel)(sender as TextBox).Parent).Children.IndexOf(sender as TextBox);
            if ((sender as TextBox).Text.Length > 0)
            {
                if (double.TryParse((sender as TextBox).Text, out _double))
                {
                    Styler.ListCount[indexer] = (sender as TextBox).Text.Length;
                    TempListValue.RemoveAt(indexer);
                    TempListValue.Insert(indexer, Convert.ToDouble((sender as TextBox).Text));
                }
            }
            else
            {
                Styler.ListCount[indexer] = (sender as TextBox).Text.Length;
            }
            if (Styler.ListCount.Any(a => a == 0))
            {
                try
                {
                    ((Button)((StackPanel)Grid1.Children[Grid1.Children.Count - 1]).Children[1]).IsEnabled = false;
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    ((Button)((StackPanel)Grid1.Children[Grid1.Children.Count - 1]).Children[1]).IsEnabled = true;
                }
                catch (Exception) { }
            }
        }

        private void MatTextChanged(object sender, TextChangedEventArgs e)
        {
            int indexer = ((StackPanel)((Grid)(sender as TextBox).Parent).Parent).Children.IndexOf((Grid)(sender as TextBox).Parent);
            if ((sender as TextBox).Text.Length > 0)
            {
                if (double.TryParse((sender as TextBox).Text, out _double))
                {
                    Styler.ListCount[indexer] = (sender as TextBox).Text.Length;
                    _row = 0; _col = 0;
                    _count = 0;
                    while (_count < indexer)
                    {
                        if (_col == TempMatListValue.GetLength(1) - 1)
                        {
                            if (_row == TempMatListValue.GetLength(0) - 1)
                            {
                                break;
                            }
                            _col = -1;
                            _row++;
                        }
                        _col++;
                        _count++;
                    }
                    TempMatListValue[_row, _col] = Convert.ToDouble((sender as TextBox).Text);
                }
            }
            else
            {
                Styler.ListCount[indexer] = (sender as TextBox).Text.Length;
            }
            if (Styler.ListCount.Any(a => a == 0))
            {
                try
                {
                    ((Button)((StackPanel)Grid1.Children[Grid1.Children.Count - 1]).Children[1]).IsEnabled = false;
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    ((Button)((StackPanel)Grid1.Children[Grid1.Children.Count - 1]).Children[1]).IsEnabled = true;
                }
                catch (Exception) { }
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Drager == "Move")
            {
                DragMove();
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Drager = "Move";
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Drager = "Null";
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
