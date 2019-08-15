using MiniPro.Dialogs;
using MiniPro.Models;
using MiniPro.Solutions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MiniPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region private variables

        private int _count, _start, _indexList;

        private bool _drag, _keyDown, _isClicked = true, _need = false;

        private double tryParse = 0;

        private string _content, _result, _currentChar, _temp, _value, _final;

        private Styler _styler = new Styler();

        private Solve _solve;

        private List<string> _mainList = new List<string>();

        private Dictionary<int, string> variable = new Dictionary<int, string>();

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private MathFunctions _mathFunction = new MathFunctions();

        private List<string> _qList = new List<string>();

        private List<string> _aList = new List<string>();

        private string _currentText;

        #endregion

        #region public propertiexs

        public string CurrentText
        {
            get
            {
                return _currentText;
            }
            set
            {
                _currentText = value;
                PropertyChangedNotifier("CurrentText");
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
                PropertyChangedNotifier("QList");
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
                PropertyChangedNotifier("AList");
            }
        }

        public MathFunctions MathFunctioon
        {
            get
            {
                return _mathFunction;
            }
            set
            {
                _mathFunction = value;
                PropertyChangedNotifier("MathFunctioon");
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
                PropertyChangedNotifier("Result");
            }
        }

        public string Final
        {
            get
            {
                return _final;
            }
            set
            {
                _final = value;
                PropertyChangedNotifier("Final");
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
                PropertyChangedNotifier("Temp");
            }
        }

        public string CurrentChar
        {
            get
            {
                return _currentChar;
            }
            set
            {
                _currentChar = value;
                PropertyChangedNotifier("CurrentChar");
            }
        }

        public string Contents
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                PropertyChangedNotifier("Content");
            }
        }

        public bool IsClicked
        {
            get
            {
                return _isClicked;
            }
            set
            {
                _isClicked = value;
                PropertyChangedNotifier("IsClicked");
            }
        }

        public bool Drag
        {
            get
            {
                return _drag;
            }
            set
            {
                _drag = value;
                PropertyChangedNotifier("Drag");
            }
        }

        public Styler Styler
        {
            get
            {
                return _styler;
            }
        }

        public Solve Solve
        {
            get
            {
                return _solve;
            }
            set
            {
                _solve = value;
                PropertyChangedNotifier("Solve");
            }
        }

        public List<string> MainList
        {
            get
            {
                return _mainList;
            }
            set
            {
                _mainList = value;
                PropertyChangedNotifier("MainList");
            }
        }

        #endregion

        #region public constructor

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            QuestionBox.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(QuestionBox_MouseLeftButtonDown), true);
            string calcButtonNames = "& | ^ ∠ A B C D E F Pow( Log( Ln( e 1 2 3 4 5 6 7 8 9 0 Pol( Rect( ! i P Co LCM( HCF( Abs( Modulus( Matrix Add( Sub( Mul( . ( ) , Inv( Prime( ListOfPrime( + - * / M G T m k f p n Sin( ASin( Cos( ACos( Tan( ATan( Sinh( ASinh( Cosh( ACosh( Tanh( ATanh(";
            string[] Main = " & ; | ; ^ ; ∠ ;A;B;C;D;E;F; Pow( , ) ; Log( ,10 ) ; Ln( ,e ) ;e;1;2;3;4;5;6;7;8;9;0;,; Pol(  ) ; Rect(  ) ;i;!;P;Co; LCM(  ) ; HCF(  ) ; Abs(  ) ; Modulus(  ) ; Matrix ; Add(  ) ; Sub(  ) ; Mul(  ) ;.; ( ; ) ; Inv(  ) ; Prime(  ) ; ListOfPrime(  ) ; + ; - ; * ; / ;M;G;T;m;k;f;p;n; Sin(  ) ; ASin(  ) ; Cos(  ) ; ACos(  ) ; Tan(  ) ; ATan(  ) ; Sinh(  ) ; ASinh(  ) ; Cosh(  ) ; ACosh(  ) ; Tanh(  ) ; ATanh(  ) ".Split(';');
            foreach (string item in calcButtonNames.Split(' '))
            {
                Styler.ListCollection.Add(item);
            }
            Styler.ListCollection.Sort();
            foreach (string item in Styler.ListCollection)
            {
                MainList.Add(Main.FirstOrDefault(a => a.TrimStart(' ').StartsWith(item)));
            }
            MainList.Insert(58, "P");
            MainList.RemoveAt(59);
            MainList.Insert(52, "M");
            MainList.RemoveAt(53);
            foreach (string item in Styler.ListCollection)
            {
                StackPanel stack = new StackPanel();
                stack.Children.Add(new TextBlock() { Text = item, Margin = new Thickness(5, 0, 0, 0) });
                Intellisense.Items.Add(stack);
            }
            Intellisense.SelectedIndex = 0;
            ((StackPanel)Intellisense.SelectedValue).Background = Brushes.LightSkyBlue;
            QuestionBox.Focus();
            for (int i = 0; i < 6; i++)
            {
                Styler.Lists.Add(new List<double>());
            }
            for (int i = 0; i < 6; i++)
            {
                Styler.MatList.Add(new double[0, 0]);
            }
            _solve = new Solve(Styler);
            QList.Add(null);
            AList.Add(null);
        }

        #endregion

        #region public event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private methods

        private void PercentExe()
        {
            EqualExe();
            if (Result != null)
            {
                if (Result != "[Collections to view Expand]" && !Result.Contains("i") && !Result.Contains("+") && !Result.Contains("∠"))
                {
                    if (double.TryParse(Result, out tryParse))
                    {
                        Result = (tryParse / 100).ToString();
                    }
                }
            }
            Styler.IsVisible = false;
            if (Result != null)
            {
                QList.Add(QuestionBox.Text);
                AList.Add(Result);
                _need = false;
                while (QList.Count > 10)
                {
                    QList.RemoveAt(1);
                    AList.RemoveAt(1);
                }
            }
        }

        private void EqualExe()
        {
            Styler.IsVisible = false;
            try
            {
                if (Esc())
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length + 1) + QuestionBox.Text.Substring(_start + 1);
                    QuestionBox.SelectionStart = _start - _value.Length;
                }
                else
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length + 1) + Final + QuestionBox.Text.Substring(_start + 1);
                    QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                }
                _start = QuestionBox.SelectionStart;
            }
            catch (Exception) { }
            _value = null;
            Final = null;
            try
            {
                Solve.Question = QuestionBox.Text;
                Result = Solve.Result();
                if (Result == "[Collections to view Expand]")
                {
                    Styler.ExpandVisible = true;
                }
                else if (Result != null)
                {
                    if (Result.Contains("(") && Result.Contains(")") && Result.Contains(","))
                    {
                        if (Result.Substring(Result.IndexOf(",") + 1).Contains("-"))
                        {
                            Result = Result.Remove(Result.IndexOf("-"), 1);
                            Result = Result.Replace(",", "-i");
                        }
                        else
                        {
                            Result = Result.Replace(",", "+i");
                        }
                        Result = Result.Substring(1, Result.Length - 2);
                        while (Result.Any(a => a == ' '))
                        {
                            Result = Result.Remove(Result.IndexOf(' '), 1);
                        }
                        if (double.TryParse(Result.Substring(Result.IndexOf("i") + 1), out tryParse))
                        {
                            if (tryParse == 0)
                            {
                                Result = Result.Remove(Result.IndexOf("i") - 1);
                            }
                        }
                    }
                    Styler.ExpandVisible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Result = null;
            }
        }

        private void Remove()
        {
            string changed = QuestionBox.Text;
            _keyDown = false;
            if (QuestionBox.Text.Substring(_start - 1, 1) != " ")
            {
                QuestionBox.Text = changed.Substring(0, _start - 1) + changed.Substring(_start);
                QuestionBox.SelectionStart = _start - 1;
            }
            else
            {
                QuestionBox.Text = changed.Substring(0, changed.Substring(0, _start - 1).LastIndexOf(" ")) + changed.Substring(_start);
                QuestionBox.SelectionStart = changed.Substring(0, _start - 1).LastIndexOf(" ");
            }
            _start = QuestionBox.SelectionStart;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if ((sender as DispatcherTimer).Interval == new TimeSpan(0, 0, 0, 0, 850))
            {
                if (Contents != null)
                {
                    Selected(Contents, Contents.Length);
                }
                Contents = null;
                (sender as DispatcherTimer).IsEnabled = false;
                (sender as DispatcherTimer).Stop();
            }
        }

        private void Timer(bool start, string first, string last)
        {
            if (start)
            {
                Contents = last;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 850);
                dispatcherTimer.IsEnabled = true;
                dispatcherTimer.Start();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
            }
            else
            {
                if (Contents != null && first != "ExecAng")
                {
                    Contents = first;
                    Selected(Contents, Contents.Length);
                    Contents = null;
                }
                dispatcherTimer.IsEnabled = false;
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
            }
        }

        private void Selected(string content, int length)
        {
            try
            {
                _start++;
                EnterOrSpace();
                _value = null;
                Final = null;
                _keyDown = false;
                if (_value != null)
                {
                    string changed = QuestionBox.Text.Substring(0, _start + 1);
                    QuestionBox.Text = QuestionBox.Text.Remove(changed.LastIndexOf(_value), _value.Length);
                    QuestionBox.SelectionStart = (_start + 1) - _value.Length;
                    _value = null;
                    _start = QuestionBox.SelectionStart;
                }
            }
            catch (Exception) { }
            _value = null;
            _keyDown = false;
            Styler.IsVisible = false;
            if (content != "ExecPol" && content != "ExecRect" && content != "ExecAng")
            {
                if (Temp != null)
                {
                    content = content + Temp;
                    Temp = null;
                }
                CurrentChar = content;
                _start = QuestionBox.SelectionStart;
                QuestionBox.Text = QuestionBox.Text.Substring(0, _start) + content + QuestionBox.Text.Substring(_start);
                QuestionBox.Select(_start + length, 0);
                QuestionBox.Focus();
            }
            else if (content == "ExecPol")
            {
                EqualExe();
                try
                {
                    if (Result.Contains("i"))
                    {
                        double first = _mathFunction.RectangularToPolar(Convert.ToDouble(Result.Substring(0, Result.IndexOf("i") - 1)), Convert.ToDouble(Result.Substring(Result.IndexOf("i") + 1)), Solve.Mode)[0];
                        double second = ((_mathFunction.PolarToRectangular(Convert.ToDouble(Result.Substring(0, Result.IndexOf("i") - 1)), Convert.ToDouble(Result.Substring(Result.IndexOf("i") + 1)), Solve.Mode)[1]));
                        Result = first + "∠" + second;
                    }
                }
                catch (Exception) { }
            }
            else if (content == "ExecRect")
            {
                EqualExe();
                try
                {
                    if (Result.Contains("∠"))
                    {
                        double first = _mathFunction.PolarToRectangular(Convert.ToDouble(Result.Substring(0, Result.IndexOf("∠"))), Convert.ToDouble(Result.Substring(Result.IndexOf("∠") + 1)), Solve.Mode)[0];
                        double second = _mathFunction.PolarToRectangular(Convert.ToDouble(Result.Substring(0, Result.IndexOf("∠"))), Convert.ToDouble(Result.Substring(Result.IndexOf("∠") + 1)), Solve.Mode)[1];
                        Result = first + ((second.ToString().Contains("-")) ? "-i" : "+i") + ((second.ToString().Contains("-")) ? second.ToString().Replace("-", "") : second.ToString());
                    }
                }
                catch (Exception) { }
            }
            else if (content == "ExecAng")
            {
                PercentExe();
            }
        }

        private bool Back()
        {
            Styler.IsVisible = false;
            if (_value != null)
            {
                try
                {
                    if (!Styler.ListCollection.Any(a => string.Compare(a, _value, true) == 0))
                    {
                        if (_value.Length > 1)
                        {
                            int i = 0;
                            Final = null;
                            for (i = 0; i < _value.Length; i++)
                            {
                                if (!Styler.ListCollection.Any(a => string.Compare(a, _value.Substring(i, 1), true) == 0))
                                {
                                    break;
                                }
                                else
                                {
                                    Final += MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value.Substring(i, 1), true) == 0))];
                                }
                            }
                            if (i < _value.Length)
                            {
                                IsClicked = true;
                            }
                            else
                            {
                                QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                                QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                                return true;
                            }
                        }
                        IsClicked = true;
                        QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + QuestionBox.Text.Substring(_start);
                        if (Final != null)
                        {
                            QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                        }
                        else
                        {
                            QuestionBox.SelectionStart = _start;
                        }
                    }
                    Final = MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value, true) == 0))];
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                    QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                }
                catch (Exception) { _value = null; }
                //if (QuestionBox.Text.Length > (_start + 1) - (_value.Length))
                //{
                //    QuestionBox.SelectionStart = (_start + 1) - (_value.Length);
                //    _value = null;
                //    return true;
                //}
                //else
                //{
                //    QuestionBox.SelectionStart = QuestionBox.Text.Length;
                //    _value = null;
                //}
                _value = null;
                return true;
            }
            if (_start > 0)
            {
                if (QuestionBox.Text.Substring(_start - 1, 1) == " ")
                {
                    _start--;
                    while (_start > 0)
                    {
                        _start--;
                        if (QuestionBox.Text.Substring(_start - 1, 1) == " ")
                        {
                            break;
                        }
                    }
                }
            }
            QuestionBox.SelectionStart = _start;
            QuestionBox.SelectionLength = 0;
            QuestionBox.Select(_start, 0);
            Styler.IsVisible = false;
            return false;
        }

        private bool Next()
        {
            Styler.IsVisible = false;
            if (_value != null && _value != "")
            {
                try
                {
                    if (!Styler.ListCollection.Any(a => string.Compare(a, _value, true) == 0))
                    {
                        if (_value.Length > 1)
                        {
                            int i = 0;
                            Final = null;
                            for (i = 0; i < _value.Length; i++)
                            {
                                if (!Styler.ListCollection.Any(a => string.Compare(a, _value.Substring(i, 1), true) == 0))
                                {
                                    break;
                                }
                                else
                                {
                                    Final += MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value.Substring(i, 1), true) == 0))];
                                }
                            }
                            if (i < _value.Length)
                            {
                                IsClicked = true;
                            }
                            else
                            {
                                QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                                QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                                return true;
                            }
                        }
                        IsClicked = true;
                        QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + QuestionBox.Text.Substring(_start);
                        QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                        return true;
                    }
                }
                catch (Exception) { _value = null; }
                Final = MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value, true) == 0))];
                QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                _value = null;
                return true;
            }
            if (_start < QuestionBox.Text.Length)
            {
                if (QuestionBox.Text.Substring(_start, 1) == " ")
                {
                    while (_start < QuestionBox.Text.Length)
                    {
                        _start++;
                        if (QuestionBox.Text.Substring(_start, 1) == " ")
                        {
                            break;
                        }
                    }
                }
            }
            QuestionBox.SelectionStart = _start;
            QuestionBox.SelectionLength = 0;
            QuestionBox.Select(_start, 0);
            Styler.IsVisible = false;
            return false;
        }

        private void Down()
        {
            if (Styler.IsVisible)
            {
                try
                {
                    ((StackPanel)Intellisense.SelectedValue).Background = Brushes.Transparent;
                }
                catch (Exception)
                {
                }
                if (_count < Intellisense.Items.Count - 1)
                {
                    _count++;
                }
                Intellisense.SelectedIndex = _count;
                Intellisense.ScrollIntoView(Intellisense.SelectedItem);
                ((StackPanel)Intellisense.SelectedValue).Background = Brushes.LightSkyBlue;
            }
        }

        private void Up()
        {
            if (Styler.IsVisible)
            {
                try
                {
                    ((StackPanel)Intellisense.SelectedValue).Background = Brushes.Transparent;
                }
                catch (Exception)
                {
                }
                if (_count > 0)
                {
                    _count--;
                }
                Intellisense.SelectedIndex = _count;
                Intellisense.ScrollIntoView(Intellisense.SelectedItem);
                ((StackPanel)Intellisense.SelectedValue).Background = Brushes.LightSkyBlue;
            }
        }

        private bool Esc()
        {
            Styler.IsVisible = false;
            if (_value != null)
            {
                try
                {
                    if (!Styler.ListCollection.Any(a => string.Compare(a, _value, true) == 0))
                    {
                        if (_value.Length > 1)
                        {
                            int i = 0;
                            Final = null;
                            for (i = 0; i < _value.Length; i++)
                            {
                                if (!Styler.ListCollection.Any(a => string.Compare(a, _value.Substring(i, 1), true) == 0))
                                {
                                    break;
                                }
                                else
                                {
                                    Final += MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value.Substring(i, 1), true) == 0))];
                                }
                            }
                            if (i < _value.Length)
                            {
                                IsClicked = true;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        IsClicked = true;
                        return true;
                    }
                    Final = MainList[Styler.ListCollection.IndexOf(Styler.ListCollection.FirstOrDefault(a => string.Compare(a, _value, true) == 0))];
                }
                catch (Exception) { }
            }
            return false;
        }

        private Dictionary<int, string> Executed(int i, string temp)
        {
            Dictionary<int, string> variable1 = new Dictionary<int, string>();
            for (int j = 0; j < temp.Length; j++)
            {
                variable1.Add(i - 1, temp.Substring(j, 1));
                i++;
            }
            return variable1;
        }

        private void MouseClick(bool init)
        {
            if (init)
            {
                variable.Clear();
                string temp = null;
                for (int i = 1; i <= QuestionBox.Text.Length; i++)
                {
                    temp += QuestionBox.Text.Substring(i - 1, 1);
                    if (temp != " " && QuestionBox.Text.Substring(i - 1, 1) == " " && temp.Contains(" "))
                    {
                        variable.Add(i, temp);
                        temp = null;
                    }
                    else if (temp.Length > 0 && !temp.Contains(" "))
                    {
                        try
                        {

                            if (QuestionBox.Text.Substring(i, 1) == " ")
                            {
                                foreach (var item in Executed(i + 2 - temp.Length, temp))
                                {
                                    variable.Add(item.Key, item.Value);
                                }
                                temp = null;
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            else if (!init)
            {
                try
                {
                    if (_value != null)
                    {
                        if (Esc())
                        {
                            QuestionBox.Text = QuestionBox.Text.Substring(0, (_start - _value.Length) + 1 == -1 ? 0 : (_start - _value.Length) + 1) + QuestionBox.Text.Substring(_start + 1);
                            QuestionBox.SelectionStart = _start - _value.Length;
                        }
                        else
                        {
                            QuestionBox.Text = QuestionBox.Text.Substring(0, (_start - _value.Length) + 1 == -1 ? 0 : (_start - _value.Length) + 1) + QuestionBox.Text.Substring(_start + 1);
                            QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                        }
                        _value = null;
                    }
                }
                catch (Exception) { }
                _start = QuestionBox.SelectionStart;
                if (_start > 0)
                {
                    foreach (var item in variable)
                    {
                        if (_start <= item.Key)
                        {
                            _start = item.Key;
                            break;
                        }
                    }
                }
                QuestionBox.SelectionStart = _start;
                QuestionBox.Select(_start, 0);
            }
        }

        private bool KeySetterResult(KeyEventArgs e)
        {
            Regex regEx = new Regex("[{A-Z}{0-9}\\&\\|\\^\\+\\-\\*\\(\\/\\)\\,\\.\\!]", RegexOptions.IgnoreCase);
            if (regEx.IsMatch(e.Key.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //click

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AND_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " & ";
            Selected(Contents, Contents.Length);
        }

        private void OR_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " | ";
            Selected(Contents, Contents.Length);
        }

        private void NOT_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " ^ ";
            Selected(Contents, Contents.Length);
        }

        private void A_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "A";
            Selected(Contents, Contents.Length);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "B";
            Selected(Contents, Contents.Length);
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "C";
            Selected(Contents, Contents.Length);
        }

        private void D_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "D";
            Selected(Contents, Contents.Length);
        }

        private void E_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "E";
            Selected(Contents, Contents.Length);
        }

        private void F_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "F";
            Selected(Contents, Contents.Length);
        }

        private void LCM_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " LCM( ";
            Selected(Contents, Contents.Length);
        }

        private void HCF_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " HCF( ";
            Selected(Contents, Contents.Length);
        }

        private void Abs_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Abs( ";
            Selected(Contents, Contents.Length);
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            Temp = ", ) ";
            Contents = " Modulus( ";
            Selected(Contents, Contents.Length);
        }

        private void Mat_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " Matrix ";
            Selected(Contents, Contents.Length);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Add( ";
            Selected(Contents, Contents.Length);
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Sub( ";
            Selected(Contents, Contents.Length);
        }

        private void Mul_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Mul( ";
            Selected(Contents, Contents.Length);
        }

        private void Pie_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "π";
            Selected(Contents, Contents.Length);
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = ",";
            Selected(Contents, Contents.Length);
        }

        private void Prime_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Prime( ";
            Selected(Contents, Contents.Length);
        }

        private void Inv_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " Inv( ";
            Selected(Contents, Contents.Length);
        }

        private void OpenBracket_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " ( ";
            Selected(Contents, Contents.Length);
        }

        private void CloseBracket_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " ) ";
            Selected(Contents, Contents.Length);
        }

        private void Clr_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            QuestionBox.Text = null;
            QuestionBox.Focus();
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = ".";
            Selected(Contents, Contents.Length);
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " * ";
            Selected(Contents, Contents.Length);
        }

        private void Slash_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " / ";
            Selected(Contents, Contents.Length);
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " + ";
            Selected(Contents, Contents.Length);
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = " - ";
            Selected(Contents, Contents.Length);
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "0";
            Selected(Contents, Contents.Length);
        }

        private void Permutation_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "P";
            Selected(Contents, Contents.Length);
        }

        private void Combination_Click(object sender, RoutedEventArgs e)
        {
            Temp = null;
            Contents = "Co";
            Selected(Contents, Contents.Length);
        }

        private void ListOfPrime_Click(object sender, RoutedEventArgs e)
        {
            Temp = " ) ";
            Contents = " ListOfPrime( ";
            Selected(Contents, Contents.Length);
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            QuestionBox.Text = null;
            Result = null;
            Styler.windowState = WindowState.Minimized;
            Styler.MatVisible = true;
            Styler.StatVisible = false;
            Dialogs.List list = new Dialogs.List(Styler);
            list.ShowDialog();
        }

        private void Others_Click(object sender, RoutedEventArgs e)
        {
            Others others = new Others(Styler, Solve.Mode);
            others.ShowDialog();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            QuestionBox.SelectAll();
            QuestionBox.Copy();
            QuestionBox.SelectionStart = _start;
            QuestionBox.SelectionLength = 0;
        }

        private void AnsCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(AnswerBox.Text);
        }

        //preview left

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            QuestionBox.Focus();
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Styler.Name == "Grid1")
            {
                DragMove();
            }
        }

        private void Four_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "4", "μ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "4", "μ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "μ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Fact_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "!", "i");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "!", "i");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "i";
                Selected(Contents, Contents.Length);
            }
        }

        private void Five_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "5", "m");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "5", "m");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "m";
                Selected(Contents, Contents.Length);
            }
        }

        private void Six_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "6", "k");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "6", "k");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "k";
                Selected(Contents, Contents.Length);
            }
        }

        private void One_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "1", "f");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "1", "f");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "f";
                Selected(Contents, Contents.Length);
            }
        }

        private void Two_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "2", "p");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "2", "p");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "p";
                Selected(Contents, Contents.Length);
            }
        }

        private void Three_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "3", "n");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "3", "n");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "n";
                Selected(Contents, Contents.Length);
            }
        }

        private void QuestionBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MouseClick(false);
        }

        private void SquareOrRoot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = null;
                Timer(true, " √( ", "²");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(false, " √( ", "²");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = null;
                Contents = "²";
                Selected(Contents, Contents.Length);
            }
        }

        private void CubeOrRoot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = null;
                Timer(true, " ³√( ", "³");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(false, " ³√( ", "³");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = null;
                Contents = "³";
                Selected(Contents, Contents.Length);
            }
        }

        private void NthRoot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = ", ) ";
                Timer(true, " √( ", " Pow( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = ", ) ";
                Timer(false, " √( ", " Pow( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = ", ) ";
                Contents = " Pow( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Log_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = "10, ) ";
                Timer(true, " Log( ", " Pow( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = ",10 ) ";
                Timer(false, " Log( ", " Pow( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = "10, ) ";
                Contents = " Pow( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Ln_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(true, " Ln( ", " Pow( e,");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(false, " Ln( ", " Pow( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Contents = " Pow( e,";
                Selected(Contents, Contents.Length);
            }
        }

        private void Sin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Sin( ", " ASin( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Sin( ", " ASin( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ASin( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Cos_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Cos( ", " ACos( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Cos( ", " ACos( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ACos( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Tan_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Tan( ", " ATan( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Tan( ", " ATan( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ATan( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Sinh_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Sinh( ", " ASinh( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Sinh( ", " ASinh( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ASinh( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Cosh_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Cosh( ", " ACosh( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Cosh( ", " ACosh( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ACosh( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Tanh_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = " ) ";
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, " Tanh( ", " ATanh( ");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, " Tanh( ", " ATanh( ");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = " ATanh( ";
                Selected(Contents, Contents.Length);
            }
        }

        private void Seven_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "7", "M");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "7", "M");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "M";
                Selected(Contents, Contents.Length);
            }
        }

        private void Eight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "8", "G");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "8", "G");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "G";
                Selected(Contents, Contents.Length);
            }
        }

        private void Nine_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Temp = null;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Timer(true, "9", "T");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Timer(false, "9", "T");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Contents = "T";
                Selected(Contents, Contents.Length);
            }
        }

        private void QuestionBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            QuestionBox.CaretBrush = Brushes.Transparent;
        }

        private void QuestionBox_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            QuestionBox.CaretBrush = Brushes.Black;
            QuestionBox.SelectionStart = _start;
        }

        private void QuestionBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            QuestionBox.ScrollToHorizontalOffset(QuestionBox.HorizontalOffset - e.Delta);
        }

        private void EscOrTab()
        {
            Styler.IsVisible = false;
            try
            {
                if (Esc())
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + QuestionBox.Text.Substring(_start);
                    QuestionBox.SelectionStart = _start - _value.Length;
                }
                else
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                    QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                }
            }
            catch (Exception) { }
        }

        private void QuestionBox_KeyDown(object sender, KeyEventArgs e)
        {
            _keyDown = true;
            try
            {
                if (e.Key == Key.Escape)
                {
                    if (Styler.IsVisible == false)
                    {
                        QuestionBox.Text = null;
                    }
                    else
                    {
                        EscOrTab();
                    }
                    _value = null;
                    Final = null;
                    e.Handled = true;
                }
                else if (e.Key == Key.Tab)
                {
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter)
                {
                    EnterOrSpace();
                    _value = null;
                    Final = null;
                    e.Handled = true;
                }
            }
            catch (Exception)
            {
                Styler.IsVisible = false;
            }
        }

        private void EnterOrSpace()
        {
            if (Intellisense.SelectedIndex != -1 && Styler.IsVisible)
            {
                Final = MainList[Intellisense.SelectedIndex];
                if (_value == null)
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start) + Final + QuestionBox.Text.Substring(_start);
                }
                else
                {
                    QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                }
                try
                {
                    QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                }
                catch (Exception) { }
            }
            else
            {
                try
                {
                    if (Esc())
                    {
                        QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + QuestionBox.Text.Substring(_start);
                        QuestionBox.SelectionStart = _start - _value.Length;
                    }
                    else
                    {
                        QuestionBox.Text = QuestionBox.Text.Substring(0, _start - _value.Length) + Final + QuestionBox.Text.Substring(_start);
                        QuestionBox.SelectionStart = _start + (Final.Contains(' ') ? (Final.Length - _value.Length) + 1 : (Final.Length - _value.Trim().Length));
                    }
                }
                catch (Exception) { }
            }
            Styler.IsVisible = false;
        }

        private void QuestionBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _keyDown = true;
            try
            {
                e.Handled = KeySetterResult(e);
                IsClicked = KeySetterResult(e);
                _start = QuestionBox.SelectionStart;
                if (e.Key == Key.Space || e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
                {
                    EnterOrSpace();
                    _value = null;
                    Final = null;
                    e.Handled = true;
                }
                else if (e.Key == Key.Back)
                {
                    if (!string.IsNullOrEmpty(_value))
                    {
                        _keyDown = false;
                        _value = _value.Substring(0, _value.Length - 1);
                        _count = Styler.ListCollection.FindIndex(a => a.StartsWith(_value, StringComparison.OrdinalIgnoreCase));
                        try
                        {
                            ((StackPanel)Intellisense.SelectedValue).Background = Brushes.Transparent;
                        }
                        catch (Exception) { }
                        Intellisense.SelectedIndex = _count;
                        Intellisense.ScrollIntoView(Intellisense.SelectedValue);
                        ((StackPanel)Intellisense.SelectedValue).Background = Brushes.LightSkyBlue;
                    }
                    else
                    {
                        e.Handled = true;
                        Remove();
                    }
                }
                else if (e.Key == Key.Delete)
                {
                    e.Handled = true;
                }
                else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                {
                    if (Keyboard.IsKeyDown(Key.J))
                    {
                        Styler.IsVisible = true;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.Down))
                    {
                        e.Handled = true;
                    }
                    if (Keyboard.IsKeyDown(Key.D5))
                    {
                        EnterOrSpace();
                        _value = null;
                        Final = null;
                        PercentExe();
                        e.Handled = true;
                    }
                }
                else if (e.Key == Key.Left)
                {
                    if (Back())
                    {
                        e.Handled = true;
                    }
                    _value = null;
                }
                else if (e.Key == Key.Right)
                {
                    if (Next())
                    {
                        e.Handled = true;
                    }
                    _value = null;
                }
                else if (e.Key == Key.Down)
                {
                    if (Styler.IsVisible)
                    {
                        Down();
                    }
                    else
                    {
                        if (_need)
                        {
                            if (!QList.Contains(QuestionBox.Text))
                            {
                                QList.RemoveAt(0);
                                AList.RemoveAt(0);
                                QList.Insert(0, QuestionBox.Text);
                                AList.Insert(0, null);
                                while (QList.Count > 10)
                                {
                                    QList.RemoveAt(1);
                                    AList.RemoveAt(1);
                                }
                            }
                        }
                        _indexList = ((_indexList + 1 < QList.Count) ? _indexList + 1 : 0);
                        HistoryResult(_indexList);
                    }
                }
                else if (e.Key == Key.Up)
                {
                    if (Styler.IsVisible)
                    {
                        Up();
                    }
                    else
                    {
                        if (_need)
                        {
                            if (!QList.Contains(QuestionBox.Text))
                            {
                                QList.RemoveAt(0);
                                AList.RemoveAt(0);
                                QList.Insert(0, QuestionBox.Text);
                                AList.Insert(0, null);
                                while (QList.Count > 10)
                                {
                                    QList.RemoveAt(1);
                                    AList.RemoveAt(1);
                                }
                            }
                        }
                        _indexList = ((_indexList - 1 >= 0) ? _indexList - 1 : QList.Count - 1);
                        HistoryResult(_indexList);
                    }
                }
                else if (e.Key == Key.OemPlus)
                {
                    EnterOrSpace();
                    _value = null;
                    Final = null;
                    e.Handled = true;
                    EqualExe();
                    if (Result != null)
                    {
                        QList.Add(QuestionBox.Text);
                        AList.Add(Result);
                        _need = false;
                        while (QList.Count > 10)
                        {
                            QList.RemoveAt(1);
                            AList.RemoveAt(1);
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void HistoryResult(int index)
        {
            QuestionBox.Text = QList[index];
            Result = AList[index];
            Styler.IsVisible = false;
            QuestionBox.SelectionStart = 0;
        }

        private void QuestionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_keyDown)
            {
                try
                {
                    Regex regEx = new Regex("[{A-Z}{0-9}\\&\\|\\^\\+\\-\\*\\(\\/\\)\\,\\.\\!]", RegexOptions.IgnoreCase);
                    if (regEx.IsMatch(QuestionBox.Text.Substring(_start, 1)))
                    {
                        Styler.IsVisible = true;
                    }
                    else
                    {
                        Styler.IsVisible = false;
                    }
                }
                catch (Exception) { }
                if (!IsClicked)
                {
                    try
                    {
                        _value += QuestionBox.Text.Substring(_start, 1);
                        _count = Styler.ListCollection.FindIndex(a => a.StartsWith(_value, StringComparison.OrdinalIgnoreCase));
                        try
                        {
                            ((StackPanel)Intellisense.SelectedValue).Background = Brushes.Transparent;
                        }
                        catch (Exception) { }
                        Intellisense.SelectedIndex = _count;
                        Intellisense.ScrollIntoView(Intellisense.SelectedValue);
                        ((StackPanel)Intellisense.SelectedValue).Background = Brushes.LightSkyBlue;
                    }
                    catch (Exception) { }
                }
            }
            _keyDown = false;
            MouseClick(true);
        }

        private void QuestionBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                QuestionBox.CaretBrush = Brushes.Black;
                Drag = true;
            }
            else if (e.LeftButton == MouseButtonState.Released)
            {
                Drag = false;
                QuestionBox.CaretBrush = Brushes.Black;
            }
        }

        private void QuestionBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                QuestionBox.ReleaseMouseCapture();
                QuestionBox.CaretBrush = Brushes.Transparent;
            }
        }

        private void Pol_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(true, " Pol( ", "ExecPol");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(false, " Pol( ", "ExecPol");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Contents = "ExecPol";
                Selected(Contents, Contents.Length);
            }
        }

        private void Rect_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(true, " Rect( ", "ExecRect");
            }
            else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Timer(false, " Rect( ", "ExecRect");
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                Temp = " ) ";
                Contents = "ExecRect";
                Selected(Contents, Contents.Length);
            }
        }

        private void Percent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Temp = null;
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Timer(true, "ExecAng", "∠");
                }
                else if (e.LeftButton == MouseButtonState.Released && e.RightButton != MouseButtonState.Pressed)
                {
                    PercentExe();
                    Timer(false, "ExecAng", "∠");
                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    Contents = "∠";
                    Selected(Contents, Contents.Length);
                }
            }
            catch (Exception) { }
            Styler.IsVisible = false;
        }

        //mouse enter

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Styler.Name = "null";
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Styler.Name = "Grid1";
        }

        //RadioButton checked

        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if (Solve != null)
            {
                if ((sender as RadioButton).Content.ToString() == "Deg")
                {
                    Solve.Mode = Mode.Degree;
                }
                else if ((sender as RadioButton).Content.ToString() == "Rad")
                {
                    Solve.Mode = Mode.Radians;
                }
                else if ((sender as RadioButton).Content.ToString() == "Grad")
                {
                    Solve.Mode = Mode.Gradient;
                }
            }
        }

        private void AnswerBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            AnswerBox.ScrollToHorizontalOffset(QuestionBox.HorizontalOffset - e.Delta);
        }

        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            Expanded expanded = new Expanded(Solve.Collection, Styler);
            expanded.ShowDialog();
        }

        private void PropertyChangedNotifier(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        private void Stat(string type)
        {
            try
            {
                if (QuestionBox.Text == "A" || QuestionBox.Text == "B" || QuestionBox.Text == "C" || QuestionBox.Text == "D" || QuestionBox.Text == "E" || QuestionBox.Text == "F")
                {
                    if (type == "SD")
                    {
                        Result = MathFunctioon.StandardDeviation(MathFunctioon.Variance(Styler.Lists[Index(QuestionBox.Text)]));
                    }
                    else if (type == "Var")
                    {
                        Result = MathFunctioon.Variance(Styler.Lists[Index(QuestionBox.Text)]);
                    }
                    else if (type == "Mean")
                    {
                        Result = MathFunctioon.Mean(Styler.Lists[Index(QuestionBox.Text)]);
                    }
                    else if (type == "MeanSquare")
                    {
                        Result = MathFunctioon.MeanSquare(Styler.Lists[Index(QuestionBox.Text)]);
                    }
                    else if (type == "Sum")
                    {
                        Result = MathFunctioon.Sum(Styler.Lists[Index(QuestionBox.Text)]);
                    }
                    else if (type == "SumOfSquare")
                    {
                        Result = MathFunctioon.SquareSum(Styler.Lists[Index(QuestionBox.Text)]);
                    }
                }
                else
                {
                    Result = null;
                    MessageBox.Show("Please input A or B or C...F it should not contains any characters");
                }
            }
            catch (Exception)
            {
                Result = null;
                MessageBox.Show("Please input A or B or C...F it should not contains any characters");
            }
            if (Result != null)
            {
                QList.Add(QuestionBox.Text);
                AList.Add(Result);
                _need = false;
                while (QList.Count > 10)
                {
                    QList.RemoveAt(1);
                    AList.RemoveAt(1);
                }
            }
        }

        private int Index(string value)
        {
            switch (value)
            {
                case "A":
                    {
                        return 0;
                    }
                case "B":
                    {
                        return 1;
                    }
                case "C":
                    {
                        return 2;
                    }
                case "D":
                    {
                        return 3;
                    }
                case "E":
                    {
                        return 4;
                    }
                case "F":
                    {
                        return 5;
                    }
            }
            throw new InvalidOperationException("invalid input");
        }

        private void SD_Click(object sender, RoutedEventArgs e)
        {
            Stat("SD");
        }

        private void Variance_Click(object sender, RoutedEventArgs e)
        {
            Stat("Var");
        }

        private void Mean_Click(object sender, RoutedEventArgs e)
        {
            Stat("Mean");
        }

        private void MeanSquare_Click(object sender, RoutedEventArgs e)
        {
            Stat("MeanSquare");
        }

        private void SumOfVariables_Click(object sender, RoutedEventArgs e)
        {
            Stat("Sum");
        }

        private void SumOfSquareOfVariables_Click(object sender, RoutedEventArgs e)
        {
            Stat("SumOfSquare");
        }

        private void Angle_Click(object sender, RoutedEventArgs e)
        {
            Styler.IsVisible = false;
            if (!string.IsNullOrEmpty(QuestionBox.Text) && int.TryParse(QuestionBox.Text, out _count))
            {
                Result = ((Convert.ToInt32(QuestionBox.Text) - 2) * 180 / (Convert.ToInt32(QuestionBox.Text))).ToString();
            }
            else
            {
                Result = null;
                MessageBox.Show("Input Number of Sides like 1 or 2 or 3....", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (Result != null)
            {
                QList.Add(QuestionBox.Text);
                AList.Add(Result);
                _need = false;
                while (QList.Count > 10)
                {
                    QList.RemoveAt(1);
                    AList.RemoveAt(1);
                }
            }
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            if (Result != null)
            {
                QList.Add(QuestionBox.Text);
                AList.Add(Result);
                _need = false;
                while (QList.Count > 10)
                {
                    QList.RemoveAt(1);
                    AList.RemoveAt(1);
                }
            }
        }

        private void QuestionBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            _need = true;
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
