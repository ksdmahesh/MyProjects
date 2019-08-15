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
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Window, INotifyPropertyChanged
    {
        #region private variables

        private Styler _styler = new Styler();

        private List<double> listItem = new List<double>();

        private double[,] mathList;

        private int _check;

        private int _index;

        private string _slectiveItemList;

        private string _drager;

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

        public string SlectiveItemList
        {
            get
            {
                return _slectiveItemList;
            }
            set
            {
                _slectiveItemList = value;
                OnPropertyChanged("SlectiveItemList");
            }
        }

        #endregion

        #region public constructor

        public List(Styler styler)
        {
            InitializeComponent();
            Styler = styler;
            listItem.Clear();
            mathList = new double[Styler.MatList[0].GetLength(0), Styler.MatList[0].GetLength(1)];
            foreach (double item in Styler.Lists[0])
            {
                listItem.Add(item);
            }
            Number.Text = Convert.ToString(listItem.Count);
            m.Text = Styler.MatList[0].GetLength(0).ToString();
            n.Text = Styler.MatList[0].GetLength(1).ToString();
            DataContext = this;
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Styler.windowState = WindowState.Normal;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Styler.Indexes = Index();
            if (Styler.StatVisible)
            {
                ContentList contentList = new ContentList(Convert.ToInt32(Number.Text), Styler.Indexes, Styler);
                contentList.ShowDialog();
            }
            else if (Styler.MatVisible)
            {
                double[,] replace = new double[Convert.ToInt32(m.Text), Convert.ToInt32(n.Text)];
                try
                {
                    if (Styler.MatList[Styler.Indexes].GetLength(0) != Convert.ToInt32(m.Text) || Styler.MatList[Styler.Indexes].GetLength(1) != Convert.ToInt32(n.Text))
                    {
                        Styler.MatList.RemoveAt(Styler.Indexes);
                        Styler.MatList.Insert(Styler.Indexes, replace);
                    }
                }
                catch (Exception)
                {
                    Styler.MatList.RemoveAt(Styler.Indexes);
                    Styler.MatList.Insert(Styler.Indexes, replace);
                }
                ContentList contentList = new ContentList(Convert.ToInt32(m.Text), Convert.ToInt32(n.Text), Styler.Indexes, Styler);
                contentList.ShowDialog();
            }
        }

        private int Index()
        {
            switch (VariableNames.Text)
            {
                case "A":
                    {
                        _index = 0;
                        break;
                    }
                case "B":
                    {
                        _index = 1;
                        break;
                    }
                case "C":
                    {
                        _index = 2;
                        break;
                    }
                case "D":
                    {
                        _index = 3;
                        break;
                    }
                case "E":
                    {
                        _index = 4;
                        break;
                    }
                case "F":
                    {
                        _index = 5;
                        break;
                    }
            }
            return _index;
        }

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckerStat(sender);
        }

        private void VariableNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Styler.StatVisible)
            {
                try
                {
                    listItem.Clear();
                    foreach (double item in Styler.Lists[VariableNames.SelectedIndex])
                    {
                        listItem.Add(item);
                    }
                    Number.Text = Convert.ToString(listItem.Count);
                }
                catch (Exception) { }
            }
            else if (Styler.MatVisible)
            {
                try
                {
                    m.Text = Styler.MatList[VariableNames.SelectedIndex].GetLength(0).ToString();
                    n.Text = Styler.MatList[VariableNames.SelectedIndex].GetLength(1).ToString();
                }
                catch (Exception) { }
            }
        }

        private void MatOrStat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (MatOrStat.SelectedIndex == 0)
                {
                    SlectiveItemList = "Mat";
                    Styler.MatVisible = true;
                    Styler.StatVisible = false;
                    CheckersMat();
                }
                else if (MatOrStat.SelectedIndex == 1)
                {
                    SlectiveItemList = "Stat";
                    Styler.MatVisible = false;
                    Styler.StatVisible = true;
                    CheckerStat(Number);
                }
            }
            catch (Exception) { }
        }

        private void m_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckersMat();
        }

        private void n_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckersMat();
        }

        private void CheckersMat()
        {
            if (Styler.MatVisible && !string.IsNullOrEmpty(m.Text) && !string.IsNullOrEmpty(n.Text))
            {
                if (m.Text.Length > 0 && n.Text.Length > 0)
                {
                    if (int.TryParse(m.Text, out _check) && int.TryParse(n.Text, out _check))
                    {
                        if (Convert.ToInt32(m.Text) > 0 && Convert.ToInt32(n.Text) > 0)
                        {
                            Styler.IsEnabled = true;
                        }
                        else
                        {
                            Styler.IsEnabled = false;
                        }
                    }
                    else
                    {
                        Styler.IsEnabled = false;
                    }
                }
                else
                {
                    Styler.IsEnabled = false;
                }
            }
            else
            {
                Styler.IsEnabled = false;
            }
        }

        private void CheckerStat(object sender)
        {
            if (Styler.StatVisible)
            {
                if (Number.Text.Length > 0)
                {
                    if (int.TryParse((sender as TextBox).Text, out _check))
                    {
                        if (Convert.ToInt32((sender as TextBox).Text) > 0)
                        {
                            Styler.IsEnabled = true;
                        }
                        else
                        {
                            Styler.IsEnabled = false;
                        }
                    }
                    else
                    {
                        Styler.IsEnabled = false;
                    }
                }
                else
                {
                    Styler.IsEnabled = false;
                }
            }
            else
            {
                Styler.IsEnabled = false;
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
