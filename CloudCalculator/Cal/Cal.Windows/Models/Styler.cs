using Cal.ViewModels;
using MiniPro.Solutions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Cal.Models
{
    public class Styler : Notifier
    {
        #region private variables

        private bool _home = true, _algebra, _others, _deg = true, _rad, _grad, _isVisible, _isStat, _isMat = true, _isEnable, _isHome = true, _isList, _isPopUp;

        private string _result, _content, _temp, _mainResult,_reverse;

        private int _indexes = 0, _mode, _item, _m, _n, _s;

        private ObservableCollection<string> _itemSource = new ObservableCollection<string>();

        private Mode _moder = new Mode();

        private MathFunctions _math = new MathFunctions();

        private Converter _converter = new Converter();

        private ObservableCollection<ObservableCollection<string>> _itemSources = new ObservableCollection<ObservableCollection<string>>();

        private Angle _angle1 = new Angle();

        private Area _area1 = new Area();

        private Base _base1 = new Base();

        private Energy _energy1 = new Energy();

        private Length _length1 = new Length();

        private Powers _power1 = new Powers();

        private Pressure _pressure1 = new Pressure();

        private Temperature _temperature1 = new Temperature();

        private Time _time1 = new Time();

        private Velocity _velocity1 = new Velocity();

        private Volume _volume1 = new Volume();

        private Weight _weight1 = new Weight();

        private Angle _angle2 = new Angle();

        private Area _area2 = new Area();

        private Base _base2 = new Base();

        private Energy _energy2 = new Energy();

        private Length _length2 = new Length();

        private Powers _power2 = new Powers();

        private Pressure _pressure2 = new Pressure();

        private Temperature _temperature2 = new Temperature();

        private Time _time2 = new Time();

        private Velocity _velocity2 = new Velocity();

        private Volume _volume2 = new Volume();

        private Weight _weight2 = new Weight();

        private Mode _solve = new Mode();

        private List<List<double>> _lists = new List<List<double>>();

        private List<string> _qList = new List<string>();

        private List<string> _aList = new List<string>();

        private List<double[,]> _matList = new List<double[,]>();

        #endregion

        #region public properties

        public string Reverse
        {
            get
            {
                return _reverse;
            }
            set
            {
                _reverse = value;
                OnPropertyChanged("Reverse");
            }
        }

        public bool IsPopUp
        {
            get
            {
                return _isPopUp;
            }
            set
            {
                _isPopUp = value;
                OnPropertyChanged("IsPopUp");
            }
        }

        public int S
        {
            get
            {
                return _s;
            }
            set
            {
                _s = value;
                OnPropertyChanged("S");
            }
        }

        public int M
        {
            get
            {
                return _m;
            }
            set
            {
                _m = value;
                OnPropertyChanged("M");
            }
        }

        public int N
        {
            get
            {
                return _n;
            }
            set
            {
                _n = value;
                OnPropertyChanged("N");
            }
        }

        public int Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public int Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                OnPropertyChanged("Mode");
            }
        }

        public bool IsHome
        {
            get
            {
                return _isHome;
            }
            set
            {
                _isHome = value;
                OnPropertyChanged("IsHome");
            }
        }

        public bool IsList
        {
            get
            {
                return _isList;
            }
            set
            {
                _isList = value;
                OnPropertyChanged("IsList");
            }
        }

        public bool IsEnable
        {
            get
            {
                return _isEnable;
            }
            set
            {
                _isEnable = value;
                OnPropertyChanged("IsEnable");
            }
        }

        public bool IsStat
        {
            get
            {
                return _isStat;
            }
            set
            {
                _isStat = value;
                OnPropertyChanged("IsStat");
            }
        }

        public bool IsMat
        {
            get
            {
                return _isMat;
            }
            set
            {
                _isMat = value;
                OnPropertyChanged("IsMat");
            }
        }

        public List<double[,]> MatList
        {
            get
            {
                return _matList;
            }
            set
            {
                _matList = value;
                OnPropertyChanged("MatList");
            }
        }

        public string MainResult
        {
            get
            {
                return _mainResult;
            }
            set
            {
                _mainResult = value;
                OnPropertyChanged("MainResult");
            }
        }

        public List<string> QList
        {
            get
            {
                return _qList;
            }
            set
            {
                _qList = value;
                OnPropertyChanged("QList");
            }
        }

        public List<string> AList
        {
            get
            {
                return _aList;
            }
            set
            {
                _aList = value;
                OnPropertyChanged("AList");
            }
        }

        public List<List<double>> Lists
        {
            get
            {
                return _lists;
            }
            set
            {
                _lists = value;
                OnPropertyChanged("Lists");
            }
        }

        public int Indexes
        {
            get
            {
                return _indexes;
            }
            set
            {
                _indexes = value;
                OnPropertyChanged("Indexes");
            }
        }

        public string Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value;
                OnPropertyChanged("Temp");
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public Mode Solve
        {
            get
            {
                return _solve;
            }
            set
            {
                _solve = value;
                OnPropertyChanged("Solve");
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        public MathFunctions Math
        {
            get
            {
                return _math;
            }
            set
            {
                _math = value;
                OnPropertyChanged("Math");
            }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {

                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public ObservableCollection<string> ItemSource
        {
            get
            {
                return _itemSource;
            }
            set
            {
                _itemSource = value;
                OnPropertyChanged("ItemSource");
            }
        }

        public bool Home
        {
            get
            {
                return _home;
            }
            set
            {
                _home = value;
                OnPropertyChanged("Home");
            }
        }

        public bool Algebra
        {
            get
            {
                return _algebra;
            }
            set
            {
                _algebra = value;
                OnPropertyChanged("Algebra");
            }
        }

        public bool Others
        {
            get
            {
                return _others;
            }
            set
            {
                _others = value;
                OnPropertyChanged("Others");
            }
        }

        public Mode Moder
        {
            get
            {
                return _moder;
            }
            set
            {
                _moder = value;
                OnPropertyChanged("Moder");
            }
        }

        public bool Deg
        {
            get
            {
                return _deg;
            }
            set
            {
                _deg = value;
                OnPropertyChanged("Deg");
            }
        }

        public bool Rad
        {
            get
            {
                return _rad;
            }
            set
            {
                _rad = value;
                OnPropertyChanged("Rad");
            }
        }

        public bool Grad
        {
            get
            {
                return _grad;
            }
            set
            {
                _grad = value;
                OnPropertyChanged("Grad");
            }
        }

        public Converter Converter
        {
            get
            {
                return _converter;
            }
            set
            {
                _converter = value;
                ItemSource = ItemSources[(int)value];
                OnPropertyChanged("Converter");
            }
        }

        public ObservableCollection<ObservableCollection<string>> ItemSources
        {
            get
            {
                return _itemSources;
            }
            set
            {
                _itemSources = value;
                OnPropertyChanged("ItemSources");
            }
        }

        public Angle Angle1
        {
            get
            {
                return _angle1;
            }
            set
            {
                _angle1 = value;
                OnPropertyChanged("Angle1");
            }
        }

        public Area Area1
        {
            get
            {
                return _area1;
            }
            set
            {
                _area1 = value;
                OnPropertyChanged("Area1");
            }
        }

        public Base Base1
        {
            get
            {
                return _base1;
            }
            set
            {
                _base1 = value;
                OnPropertyChanged("Base1");
            }
        }

        public Energy Energy1
        {
            get
            {
                return _energy1;
            }
            set
            {
                _energy1 = value;
                OnPropertyChanged("Energy1");
            }
        }

        public Length Length1
        {
            get
            {
                return _length1;
            }
            set
            {
                _length1 = value;
                OnPropertyChanged("Length1");
            }
        }

        public Powers Power1
        {
            get
            {
                return _power1;
            }
            set
            {
                _power1 = value;
                OnPropertyChanged("Power1");
            }
        }

        public Pressure Pressure1
        {
            get
            {
                return _pressure1;
            }
            set
            {
                _pressure1 = value;
                OnPropertyChanged("Pressure1");
            }
        }

        public Temperature Temperature1
        {
            get
            {
                return _temperature1;
            }
            set
            {
                _temperature1 = value;
                OnPropertyChanged("Temperature1");
            }
        }

        public Time Time1
        {
            get
            {
                return _time1;
            }
            set
            {
                _time1 = value;
                OnPropertyChanged("Time1");
            }
        }

        public Velocity Velocity1
        {
            get
            {
                return _velocity1;
            }
            set
            {
                _velocity1 = value;
                OnPropertyChanged("Velocity1");
            }
        }

        public Volume Volume1
        {
            get
            {
                return _volume1;
            }
            set
            {
                _volume1 = value;
                OnPropertyChanged("Volume1");
            }
        }

        public Weight Weight1
        {
            get
            {
                return _weight1;
            }
            set
            {
                _weight1 = value;
                OnPropertyChanged("Weight1");
            }
        }

        public Angle Angle2
        {
            get
            {
                return _angle2;
            }
            set
            {
                _angle2 = value;
                OnPropertyChanged("Angle2");
            }
        }

        public Area Area2
        {
            get
            {
                return _area2;
            }
            set
            {
                _area2 = value;
                OnPropertyChanged("Area2");
            }
        }

        public Base Base2
        {
            get
            {
                return _base2;
            }
            set
            {
                _base2 = value;
                OnPropertyChanged("Base2");
            }
        }

        public Energy Energy2
        {
            get
            {
                return _energy2;
            }
            set
            {
                _energy2 = value;
                OnPropertyChanged("Energy2");
            }
        }

        public Length Length2
        {
            get
            {
                return _length2;
            }
            set
            {
                _length2 = value;
                OnPropertyChanged("Length2");
            }
        }

        public Powers Power2
        {
            get
            {
                return _power2;
            }
            set
            {
                _power2 = value;
                OnPropertyChanged("Power2");
            }
        }

        public Pressure Pressure2
        {
            get
            {
                return _pressure2;
            }
            set
            {
                _pressure2 = value;
                OnPropertyChanged("Pressure2");
            }
        }

        public Temperature Temperature2
        {
            get
            {
                return _temperature2;
            }
            set
            {
                _temperature2 = value;
                OnPropertyChanged("Temperature2");
            }
        }

        public Time Time2
        {
            get
            {
                return _time2;
            }
            set
            {
                _time2 = value;
                OnPropertyChanged("Time2");
            }
        }

        public Velocity Velocity2
        {
            get
            {
                return _velocity2;
            }
            set
            {
                _velocity2 = value;
                OnPropertyChanged("Velocity2");
            }
        }

        public Volume Volume2
        {
            get
            {
                return _volume2;
            }
            set
            {
                _volume2 = value;
                OnPropertyChanged("Volume2");
            }
        }

        public Weight Weight2
        {
            get
            {
                return _weight2;
            }
            set
            {
                _weight2 = value;
                OnPropertyChanged("Weight2");
            }
        }

        #endregion

        #region public methods

        public async void MessageBox(string text)
        {
            MessageDialog m = new MessageDialog(text);
            await m.ShowAsync();
        }

        public void PopUps(double[,] list, Grid Grid1)
        {
            IsPopUp = true;
            Grid1.Children.Clear();
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
            OK.Click += OK_Click;
            panel.Children.Add(OK);
            Grid.SetRow(panel, Grid1.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(panel, Grid1.ColumnDefinitions.Count);
            Grid1.Children.Add(panel);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            IsHome = true;
            IsList = false;
            IsPopUp = false;
        }

        #endregion
    }
}
