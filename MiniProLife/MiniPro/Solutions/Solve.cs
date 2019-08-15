using MiniPro.BaseClasses;
using MiniPro.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MiniPro.Solutions
{
    public class Solve : Notifier
    {
        #region private variables

        private string _question;

        private List<string> _helper = new List<string>();

        private List<object> _filter = new List<object>();

        private Complex _temp1;

        List<object> tempList = new List<object>();

        MathFunctions _mathFunctions = new MathFunctions();

        private double tryParse = 0;

        private string _lastNumber;

        private int _outResult;

        private Styler _styler = new Styler();

        private double[,] _collection = new double[0, 0];

        private ArrayList _lcmList = new ArrayList();

        private Mode _mode = new Mode();

        private Number _number = new Number();

        #endregion

        #region public properties

        public Number Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public Mode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value; OnPropertyChanged("Mode");
            }
        }

        public string Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
            }
        }

        public List<string> Helper
        {
            get
            {
                return _helper;
            }
            set
            {
                _helper = value;
                OnPropertyChanged("Helper");
            }
        }

        public List<object> Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                OnPropertyChanged("Filter");
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

        public double[,] Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
                OnPropertyChanged("Collection");
            }
        }

        public ArrayList LCMList
        {
            get
            {
                return _lcmList;
            }
            set
            {
                _lcmList = value;
                OnPropertyChanged("LCMList");
            }
        }

        #endregion

        #region public constructor

        public Solve(Styler styler)
        {
            Styler = styler;
        }

        #endregion

        #region private methods

        private int Next(object input, int checkLast)
        {
            if (int.TryParse(Run(input).ToString(), out _outResult))
            {
                if (_outResult <= checkLast)
                {
                    return _outResult;
                }
            }
            return -1;
        }

        private object Run(object value)
        {
            Regex reg = new Regex("[A-F]", RegexOptions.Singleline);
            MatchCollection matchCount = reg.Matches(value.ToString());
            if (matchCount.Count == value.ToString().Length && value.ToString().Length > 1)
            {
                value = "0";
            }
            if (value.ToString().Contains('i') && !value.ToString().Contains('(') && value.ToString() != "Matrix" || value.ToString().Contains('∠'))
            {
                if (value.ToString().Contains('i') && !value.ToString().Contains('∠'))
                {
                    if (value.ToString().Split('i').Length > 2)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    else
                    {
                        if (value.ToString().IndexOf("i") != value.ToString().Length - 1)
                        {
                            throw new InvalidOperationException("Input like a+bi");
                        }
                        else
                        {
                            try
                            {
                                value = new Complex(0, Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("i"))));
                            }
                            catch (Exception)
                            {
                                value = new Complex(0, 1);
                            }
                        }
                    }
                }
                else if (!value.ToString().Contains('i') && value.ToString().Contains('∠'))
                {
                    if (value.ToString().Split('∠').Length > 2)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    else
                    {
                        if (!double.TryParse(value.ToString().Substring(0, value.ToString().IndexOf("∠")), out tryParse) || !double.TryParse(value.ToString().Substring(value.ToString().IndexOf("∠") + 1), out tryParse))
                        {
                            throw new InvalidOperationException("Input like r∠0");
                        }
                        else
                        {
                            value = new Complex(_mathFunctions.PolarToRectangular(Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Mode)[0], _mathFunctions.PolarToRectangular(Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Mode)[1]);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid Input");
                }
            }
            while (value.ToString().Contains('π'))
            {
                try
                {
                    value = Pi() * (Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf('π')))) + value.ToString().Substring(value.ToString().IndexOf('π') + 1);
                }
                catch (Exception)
                {
                    value = Pi() + value.ToString().Substring(value.ToString().IndexOf('π') + 1);
                }
            }
            if (value.ToString().Contains('0') || value.ToString().Contains('1') || value.ToString().Contains('2') || value.ToString().Contains('3') || value.ToString().Contains('4') || value.ToString().Contains('5') || value.ToString().Contains('6') || value.ToString().Contains('7') || value.ToString().Contains('8') || value.ToString().Contains('9'))
            {
                if ((value.ToString().Contains("A") || value.ToString().Contains("B") || value.ToString().Contains("C") || value.ToString().Contains("D") || value.ToString().Contains("E") || value.ToString().Contains("F")) && !value.ToString().Contains("Co"))
                {
                    value = "0";
                }
                while (value.ToString().Contains('²') || value.ToString().Contains('³'))
                {
                    try
                    {
                        if (value.ToString().Contains('²'))
                        {
                            try
                            {
                                value = Math.Pow(Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('²'))), 2).ToString() + value.ToString().Substring(value.ToString().IndexOf('²') + 1);
                            }
                            catch (Exception) { }
                        }
                        if (value.ToString().Contains('³'))
                        {
                            try
                            {
                                value = Math.Pow(Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('³'))), 3).ToString() + value.ToString().Substring(value.ToString().IndexOf('³') + 1);
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
                while (value.ToString().Contains('k') || value.ToString().Contains('M') || value.ToString().Contains('G') || value.ToString().Contains('T') || value.ToString().Contains('m') || value.ToString().Contains('μ') || value.ToString().Contains('n') || value.ToString().Contains('p') || value.ToString().Contains('f') || value.ToString().Contains("!") || value.ToString().Contains("P") || value.ToString().Contains("Co") || value.ToString().Contains("π"))
                {
                    if (value.ToString().Contains('k'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('k'))) * 1000).ToString() + value.ToString().Substring(value.ToString().IndexOf('k') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('M'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('M'))) * 1000000).ToString() + value.ToString().Substring(value.ToString().IndexOf('M') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('G'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('G'))) * 1000000000).ToString() + value.ToString().Substring(value.ToString().IndexOf('G') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('T'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('T'))) * 1000000000000).ToString() + value.ToString().Substring(value.ToString().IndexOf('T') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('m'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('m'))) * (1d / 1000d)).ToString() + value.ToString().Substring(value.ToString().IndexOf('m') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('μ'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('μ'))) * (1d / 1000000d)).ToString() + value.ToString().Substring(value.ToString().IndexOf('μ') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('n'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('n'))) * (1d / 1000000000d)).ToString() + value.ToString().Substring(value.ToString().IndexOf('n') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('p'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('p'))) * (1d / 1000000000000d)).ToString() + value.ToString().Substring(value.ToString().IndexOf('p') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('f'))
                    {
                        try
                        {
                            value = (Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf('f'))) * (1d / 1000000000000000d)).ToString() + value.ToString().Substring(value.ToString().IndexOf('f') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('!'))
                    {
                        try
                        {
                            value = _mathFunctions.Fact(Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf('!')))) + value.ToString().Substring(value.ToString().IndexOf('!') + 1);
                        }
                        catch (Exception) { }
                    }
                    if (value.ToString().Contains('P'))
                    {
                        try
                        {
                            value = _mathFunctions.Permutation(Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf('P'))), Next(value.ToString().Substring(value.ToString().IndexOf('P') + 1), Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf('P'))))).ToString();
                        }
                        catch (Exception) { Filter = new List<object>(); MessageBox.Show("check your series"); break; }
                    }
                    if (value.ToString().Contains("Co"))
                    {
                        try
                        {
                            value = _mathFunctions.Combination(Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf("Co"))), Next(value.ToString().Substring(value.ToString().IndexOf("Co") + 2), Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf("Co"))))).ToString();
                        }
                        catch (Exception) { Filter = new List<object>(); MessageBox.Show("check your series"); break; }
                    }
                    if (value.ToString().Contains('π'))
                    {
                        try
                        {
                            value = Pi() * (Convert.ToInt32(value.ToString().Substring(0, value.ToString().IndexOf('π')))) + value.ToString().Substring(value.ToString().IndexOf('π') + 1);
                        }
                        catch (Exception) { }
                    }
                }
            }
            if (value.ToString().Contains("(") && value.ToString().Contains(")") && value.ToString().Contains(","))
            {
                if (double.TryParse(value.ToString().Substring(1, value.ToString().IndexOf(",") - 1).Trim(), out tryParse) && double.TryParse(value.ToString().Substring(value.ToString().IndexOf(",") + 1, value.ToString().Length - value.ToString().IndexOf(",") - 2).Trim(), out tryParse))
                {
                    value = new Complex(Convert.ToDouble(value.ToString().Substring(1, value.ToString().IndexOf(",") - 1).Trim()), Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf(",") + 1, value.ToString().Length - value.ToString().IndexOf(",") - 2).Trim()));
                }
                else
                {
                    value = value.ToString();
                }
            }
            else if (double.TryParse(value.ToString(), out tryParse))
            {
                value = Convert.ToDouble(value);
            }
            else
            {
                value = value.ToString();
            }
            return value;
        }

        private Complex Pi()
        {
            switch (Mode)
            {
                case Mode.Degree:
                    {
                        return 180;
                    }
                case Mode.Radians:
                    {
                        return _mathFunctions.RadToDeg(180);
                    }
                case Mode.Gradient:
                    {
                        return _mathFunctions.DegToGrad(180);
                    }
            }
            return 180;
        }

        private void Exe(List<object> list)
        {
        lab:
            int start = 0;
            int end = list.FindIndex(a => a is string && a.ToString().Contains(")"));
            int index = 0;
            int oldValue = list.FindIndex(0, a => a.ToString().Contains(")"));
            List<object> temp = new List<object>();
            try
            {
                while (list.Any(a => a is string && a.ToString().Contains("(")) && list.Contains(")"))
                {
                    index = list.FindIndex(start, end, a => a is string && a.ToString().Contains("("));
                    if (index == -1 && list[start - 1].ToString().Contains("("))
                    {
                        temp = list.GetRange(start - 1, end + 2);
                        list = MakeList(temp, list, start - 1, end + 2);
                        start = 0;
                        end = list.FindIndex(a => a is string && a.ToString().Contains(")"));
                    }
                    end--;
                    start++;
                    if (start == oldValue)
                    {
                        end = list.FindIndex(start + 1, a => a is string && a.ToString().Contains(")")) - start;
                        oldValue = end + start;
                    }
                }
                if (list.Contains("Matrix"))
                {
                    int i = 0;
                    while (list.Any(a => a.ToString() == "+" || a.ToString() == "-" || a.ToString() == "*" || a.ToString() == "/"))
                    {
                        if (list[i].ToString() == "/")
                        {
                            Filter = new List<object>();
                            throw new InvalidOperationException("Sorry Divide is not performed on matrix now");
                        }
                        else if (list[i].ToString() == "+")
                        {
                            list.Insert(0, "Add(");
                            list.Insert(list.IndexOf("+") + 3, ")");
                            list.Insert(list.IndexOf("+"), ",");
                            list.RemoveAt(list.IndexOf("+"));
                        }
                        else if (list[i].ToString() == "-")
                        {
                            list.Insert(0, "Sub(");
                            list.Insert(list.IndexOf("-") + 3, ")");
                            list.Insert(list.IndexOf("-"), ",");
                            list.RemoveAt(list.IndexOf("-"));
                        }
                        else if (list[i].ToString() == "*")
                        {
                            list.Insert(0, "Mul(");
                            list.Insert(list.IndexOf("*") + 3, ")");
                            list.Insert(list.IndexOf("*"), ",");
                            list.RemoveAt(list.IndexOf("*"));
                        }
                        i++;
                    }
                    if (list.Contains("Matrix") && list.Count == 2)
                    {
                        Collection = Styler.MatList[Index(Filter[1].ToString())];
                        Filter = new List<object>() { "[Collections to view Expand]" };
                        list = new List<object>() { "[Collections to view Expand]" };
                    }
                    goto lab;
                }
                if (list.Count == 3 && list[0].ToString() == "(" && list[2].ToString() == ")")
                {
                    Filter = new List<object>();
                    Filter = list.GetRange(1, 1);
                }
                if (list.Count > 1 && !list.Contains("(") && !list.Contains(")"))
                {
                    if (list[0].ToString() == "-" || list[0].ToString() == "+" || list[0].ToString() == "/" || list[0].ToString() == "*")
                    {
                        list.Insert(0, 0.0);
                    }
                    list = Maths(list);
                }
                if (list.Count == 1)
                {
                    Filter = new List<object>();
                    Filter = list.GetRange(0, 1);
                }
            }
            catch (Exception e) { Filter = new List<object>(); MessageBox.Show(e.Message, "Apology", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private List<object> MakeList(List<object> from, List<object> to, int start, int end)
        {
            if (!from.Contains(","))
            {
                if (!from.Any(a => a is double && a.ToString().Contains(",")))
                {
                    if (from.Count > 3 && !from.Contains("Matrix"))
                    {
                        tempList = Maths(from.GetRange(1, from.Count - 2));
                        foreach (var item in tempList)
                        {
                            from.Insert(1, item);
                        }
                        from = from.GetRange(0, tempList.Count > 1 ? tempList.Count + 1 : tempList.Count + 1);
                        from.Add(")");
                        to.RemoveRange(start, end);
                        to.InsertRange(start, from);
                    }
                    else if (from.Contains("Matrix"))
                    {
                        int i = 0;
                        while (from.Any(a => a.ToString() == "+" || a.ToString() == "-" || a.ToString() == "*" || a.ToString() == "/"))
                        {
                            if (from[i].ToString() == "/")
                            {
                                Filter = new List<object>();
                                throw new InvalidOperationException("Sorry Divide is not performed on matrix now");
                            }
                            else if (from[i].ToString() == "+")
                            {
                                from.Insert(0, "Add(");
                                from.Insert(from.IndexOf("+") + 3, ")");
                                from.Insert(from.IndexOf("+"), ",");
                                from.RemoveAt(from.IndexOf("+"));
                            }
                            else if (from[i].ToString() == "-")
                            {
                                from.Insert(0, "Sub(");
                                from.Insert(from.IndexOf("-") + 3, ")");
                                from.Insert(from.IndexOf("-"), ",");
                                from.RemoveAt(from.IndexOf("-"));
                            }
                            else if (from[i].ToString() == "*")
                            {
                                from.Insert(0, "Mul(");
                                from.Insert(from.IndexOf("*") + 3, ")");
                                from.Insert(from.IndexOf("*"), ",");
                                from.RemoveAt(from.IndexOf("*"));
                            }
                            i++;
                        }
                        if (from.Contains("Matrix") && from.Count == 2)
                        {
                            Collection = Styler.MatList[Index(Filter[1].ToString())];
                            Filter = new List<object>() { "[Collections to view Expand]" };
                            from = new List<object>() { "[Collections to view Expand]" };
                        }
                        to = from.GetRange(0, from.Count);
                    }
                    else if (from[0].ToString().Contains("(") && from[0].ToString().Length > 1)
                    {
                        to.RemoveRange(start, end);
                        to.InsertRange(start, Scientific(from));
                    }
                    else
                    {
                        to.RemoveRange(start, end);
                        to.InsertRange(start, from.GetRange(1, 1));
                    }
                }
                else
                {
                    throw new InvalidOperationException("invalid input");
                }
            }
            else
            {
                to.RemoveRange(start, end);
                to.InsertRange(start, Scientific(from));
            }
            return to;
        }

        private List<object> Maths(List<object> input)
        {
            if (input[0].ToString() == "-" || input[0].ToString() == "+" || input[0].ToString() == "/" || input[0].ToString() == "*")
            {
                input.Insert(0, 0.0);
            }
            while (input.Contains("/"))
            {
                _temp1 = ((input[input.IndexOf("/") - 1] is double) ? (double)input[input.IndexOf("/") - 1] : (Complex)(input[input.IndexOf("/") - 1])) / ((input[input.IndexOf("/") + 1] is double) ? (double)input[input.IndexOf("/") + 1] : (Complex)(input[input.IndexOf("/") + 1]));
                input.RemoveAt(input.IndexOf("/") - 1);
                input.RemoveAt(input.IndexOf("/") + 1);
                input.Insert(input.IndexOf("/"), _temp1);
                input.RemoveAt(input.IndexOf("/"));
            }
            while (input.Contains("*"))
            {
                _temp1 = ((input[input.IndexOf("*") - 1] is double) ? (double)input[input.IndexOf("*") - 1] : (Complex)(input[input.IndexOf("*") - 1])) * ((input[input.IndexOf("*") + 1] is double) ? (double)input[input.IndexOf("*") + 1] : (Complex)(input[input.IndexOf("*") + 1]));
                input.RemoveAt(input.IndexOf("*") - 1);
                input.RemoveAt(input.IndexOf("*") + 1);
                input.Insert(input.IndexOf("*"), _temp1);
                input.RemoveAt(input.IndexOf("*"));
            }
            while (input.Contains("-"))
            {
                _temp1 = (((input[input.IndexOf("-") - 1]) is double) ? (double)(input[input.IndexOf("-") - 1]) : (Complex)(input[input.IndexOf("-") - 1])) - (((input[input.IndexOf("-") + 1]) is double) ? (double)(input[input.IndexOf("-") + 1]) : (Complex)(input[input.IndexOf("-") + 1]));
                input.RemoveAt(input.IndexOf("-") - 1);
                input.RemoveAt(input.IndexOf("-") + 1);
                input.Insert(input.IndexOf("-"), _temp1);
                input.RemoveAt(input.IndexOf("-"));
            }
            while (input.Contains("+"))
            {
                _temp1 = ((input[input.IndexOf("+") - 1] is double) ? (double)input[input.IndexOf("+") - 1] : (Complex)(input[input.IndexOf("+") - 1])) + ((input[input.IndexOf("+") + 1] is double) ? (double)input[input.IndexOf("+") + 1] : (Complex)(input[input.IndexOf("+") + 1]));
                input.RemoveAt(input.IndexOf("+") - 1);
                input.RemoveAt(input.IndexOf("+") + 1);
                input.Insert(input.IndexOf("+"), _temp1);
                input.RemoveAt(input.IndexOf("+"));
            }
            return input;
        }

        private List<object> AddorSuborMul(List<object> input)
        {
            Complex first = 0;
            Complex next = 0;
            try
            {
                if (input[1] is Complex)
                {
                    first = (Complex)input[1];
                }
                else if (input[1] is double)
                {
                    first = (double)input[1];
                }
                else
                {
                    throw new Exception();
                }
                if (input[3] is Complex)
                {
                    next = (Complex)input[3];
                }
                else if (input[3] is double)
                {
                    next = (double)input[3];
                }
                else
                {
                    throw new Exception();
                }
                if (input[0].ToString() == "Add(")
                {
                    input[1] = _mathFunctions.Add(first, next);
                }
                else if (input[0].ToString() == "Sub(")
                {
                    input[1] = _mathFunctions.Sub(first, next);
                }
                else if (input[0].ToString() == "Mul(")
                {
                    input[1] = _mathFunctions.Mul(first, next);
                }

            }
            catch (Exception)
            {
                if ((input[1].ToString() == "Matrix" && input[4].ToString() == "Matrix") || (input.Contains("[Collections to view Expand]") && input[input.FindIndex(0, a => a.ToString() == "[Collections to view Expand]") + 2].ToString() == "Matrix"))
                {
                    double[,] MatA = null, MatB = null;
                    if (input[1].ToString() != "[Collections to view Expand]")
                    {
                        string m1 = input[2].ToString(), m2 = input[5].ToString();
                        MatA = Styler.MatList[Index(m1)];
                        MatB = Styler.MatList[Index(m2)];
                    }
                    else
                    {
                        string m2 = input[4].ToString();
                        MatA = Collection;
                        MatB = Styler.MatList[Index(m2)];
                        input.Insert(input.Count - 2, "C");
                    }
                    if (input[0].ToString() == "Add(")
                    {
                        Collection = _mathFunctions.MatrixAdd(MatA, MatB);
                    }
                    else if (input[0].ToString() == "Sub(")
                    {
                        Collection = _mathFunctions.MatrixSub(MatA, MatB);
                    }
                    else if (input[0].ToString() == "Mul(")
                    {
                        Collection = _mathFunctions.MatrixMul(MatA, MatB);
                    }
                    input.RemoveRange(2, 2);
                    input[1] = "[Collections to view Expand]";
                }
                else
                {
                    Filter = new List<object>();
                    MessageBox.Show("invalid input", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            return input;
        }

        private List<object> Scientific(List<object> input)
        {
            input = Maths(input);
            int commaCount = 0, increment = 0;
            while (input.IndexOf(",", increment) != -1)
            {
                if (input[increment].ToString() == ",")
                {
                    commaCount++;
                }
                increment++;
            }
            List<double> doubleList = new List<double>();
            if (input[0].ToString() == "LCM(")
            {
                if (commaCount > 0)
                {
                    foreach (var item in input)
                    {
                        if (double.TryParse(item.ToString(), out tryParse))
                        {
                            doubleList.Add(Convert.ToDouble(item));
                        }
                    }
                    input[1] = _mathFunctions.Lcm(doubleList);
                    input.RemoveAll(a => a.ToString() != "LCM(" && a.ToString() != ")" && a != input[1]);
                }
                else
                {
                    Question = null;
                    if (Convert.ToInt32(input[1]) <= 40)
                    {
                        foreach (var item in _mathFunctions.LCM(Convert.ToInt32(input[1])))
                        {
                            Question += item + ",";
                        }
                        input[1] = Question.Substring(0, Question.Length - 1);
                    }
                    else
                    {
                        input[1] = "value should of 1 to 40";
                    }
                }
            }
            else if (input[0].ToString() == "HCF(")
            {
                if (commaCount >= 1)
                {
                    foreach (var item in input)
                    {
                        if (double.TryParse(item.ToString(), out tryParse))
                        {
                            doubleList.Add(Convert.ToDouble(item));
                        }
                    }
                    input[1] = _mathFunctions.Hcf(doubleList);
                    input.RemoveAll(a => a.ToString() != "HCF(" && a.ToString() != ")" && a != input[1]);
                }
            }
            else if (commaCount == 0)
            {
                Complex inputValue = 0;
                if (input[1] is Complex)
                {
                    inputValue = (Complex)input[1];
                }
                else if (input[1] is double)
                {
                    inputValue = (double)input[1];
                }
                if (input[0].ToString() == "Sin(")
                {
                    input[1] = _mathFunctions.Sin(inputValue, Mode);
                }
                else if (input[0].ToString() == "Cos(")
                {
                    input[1] = _mathFunctions.Cos(inputValue, Mode);
                }
                else if (input[0].ToString() == "Tan(")
                {
                    input[1] = _mathFunctions.Tan(inputValue, Mode);
                }
                else if (input[0].ToString() == "ASin(")
                {
                    input[1] = _mathFunctions.ASin(inputValue, Mode);
                }
                else if (input[0].ToString() == "ACos(")
                {
                    input[1] = _mathFunctions.ACos(inputValue, Mode);
                }
                else if (input[0].ToString() == "ATan(")
                {
                    input[1] = _mathFunctions.ATan(inputValue, Mode);
                }
                else if (input[0].ToString() == "Sinh(")
                {
                    input[1] = _mathFunctions.Sinh(inputValue, Mode);
                }
                else if (input[0].ToString() == "Cosh(")
                {
                    input[1] = _mathFunctions.Cosh(inputValue, Mode);
                }
                else if (input[0].ToString() == "Tanh(")
                {
                    input[1] = _mathFunctions.Tanh(inputValue, Mode);
                }
                else if (input[0].ToString() == "ASinh(")
                {
                    input[1] = _mathFunctions.ASinh(inputValue, Mode);
                }
                else if (input[0].ToString() == "ACosh(")
                {
                    input[1] = _mathFunctions.ACosh(inputValue, Mode);
                }
                else if (input[0].ToString() == "ATanh(")
                {
                    input[1] = _mathFunctions.ATanh(inputValue, Mode);
                }
                else if (input[0].ToString() == "√(")
                {
                    input[1] = _mathFunctions.Power(inputValue, 1d / 2d);
                }
                else if (input[0].ToString() == "²")
                {
                    input[1] = _mathFunctions.Power(inputValue, 2);
                }
                else if (input[0].ToString() == "³√(")
                {
                    input[1] = _mathFunctions.Power(inputValue, 1d / 3d);
                }
                else if (input[0].ToString() == "³")
                {
                    input[1] = _mathFunctions.Power(inputValue, 3);
                }
                else if (input[0].ToString() == "Abs(")
                {
                    input[1] = _mathFunctions.Abs(inputValue);
                }
                else if (input[0].ToString() == "Inv(")
                {
                    input[1] = _mathFunctions.Power(inputValue, -1);
                }
                else if (input[0].ToString() == "Ln(")
                {
                    input[1] = _mathFunctions.Log(inputValue, Math.E);
                }
                else
                {
                    throw new InvalidOperationException("invalid input");
                }
            }
            else if (commaCount == 1)
            {
                Complex first = 0, next = 0;
                try
                {
                    if (input[1] is Complex)
                    {
                        first = (Complex)input[1];
                    }
                    else if (input[1] is double)
                    {
                        first = (double)input[1];
                    }
                    if (input[3] is Complex)
                    {
                        next = (Complex)input[3];
                    }
                    else if (input[3] is double)
                    {
                        next = (double)input[3];
                    }
                }
                catch (Exception) { }
                if (input[0].ToString() == "Add(" || input[0].ToString() == "Sub(" || input[0].ToString() == "Mul(")
                {
                    input = AddorSuborMul(input);
                }
                else if (input[0].ToString() == "Pow(")
                {
                    input[1] = _mathFunctions.Power(first, next);
                }
                else if (input[0].ToString() == "Modulus(")
                {
                    input[1] = _mathFunctions.Modulus(first.Real, next.Real);
                }
                else if (input[0].ToString() == "Log(")
                {
                    input[1] = _mathFunctions.Log(first, next.Real);
                }
                else if (input[0].ToString() == "Pol(")
                {
                    input[1] = _mathFunctions.RectangularToPolar(first.Real, next.Real, Mode)[0] + "∠" + _mathFunctions.RectangularToPolar(first.Real, next.Real, Mode)[1];
                }
                else if (input[0].ToString() == "Rect(")
                {
                    input[1] = _mathFunctions.PolarToRectangular(first.Real, next.Real, Mode)[0] + "+i" + _mathFunctions.PolarToRectangular(first.Real, next.Real, Mode)[1];
                }
                else if (input[0].ToString() == "√(")
                {
                    input[1] = _mathFunctions.Power(first, 1 / next);
                }
                else
                {
                    throw new InvalidOperationException("invalid input");
                }
                input.RemoveRange(2, 2);
            }
            else if (commaCount > 1)
            {
                if (input[0].ToString() == "Add(")
                {
                    while (input.Contains(","))
                    {
                        tempList = input.GetRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        if (tempList.Contains("[Collections to view Expand]") && tempList.IndexOf("[Collections to view Expand]") != 1)
                        {
                            tempList.Remove("[Collections to view Expand]");
                            tempList.Remove(",");
                            tempList.Insert(0, "[Collections to view Expand]");
                            tempList.Insert(1, ",");
                        }
                        tempList.Insert(0, "Add(");
                        tempList.Add(")");
                        tempList = AddorSuborMul(tempList);
                        tempList.RemoveRange(2, 2);
                        tempList.RemoveAt(2);
                        tempList.RemoveAt(0);
                        input.RemoveRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        input.InsertRange(1, tempList);
                    }
                }
                else if (input[0].ToString() == "Sub(")
                {
                    while (input.Contains(","))
                    {
                        tempList = input.GetRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        if (tempList.Contains("[Collections to view Expand]") && tempList.IndexOf("[Collections to view Expand]") != 1)
                        {
                            tempList.Remove("[Collections to view Expand]");
                            tempList.Remove(",");
                            tempList.Insert(0, "[Collections to view Expand]");
                            tempList.Insert(1, ",");
                        }
                        tempList.Insert(0, "Sub(");
                        tempList.Add(")");
                        tempList = AddorSuborMul(tempList);
                        tempList.RemoveRange(2, 2);
                        tempList.RemoveAt(2);
                        tempList.RemoveAt(0);
                        input.RemoveRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        input.InsertRange(1, tempList);
                    }
                }
                else if (input[0].ToString() == "Mul(")
                {
                    while (input.Contains(","))
                    {
                        tempList = input.GetRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        if (tempList.Contains("[Collections to view Expand]") && tempList.IndexOf("[Collections to view Expand]") != 1)
                        {
                            tempList.Remove("[Collections to view Expand]");
                            tempList.Remove(",");
                            tempList.Insert(0, "[Collections to view Expand]");
                            tempList.Insert(1, ",");
                        }
                        tempList.Insert(0, "Mul(");
                        tempList.Add(")");
                        tempList = AddorSuborMul(tempList);
                        tempList.RemoveRange(2, 2);
                        tempList.RemoveAt(2);
                        tempList.RemoveAt(0);
                        input.RemoveRange(1, input.IndexOf(",", input.IndexOf(",") + 1) != -1 ? input.IndexOf(",", input.IndexOf(",") + 1) - 1 : input.Count - 2);
                        input.InsertRange(1, tempList);
                    }
                }
                else
                {
                    throw new InvalidOperationException("invalid input");
                }
            }
            else
            {
                throw new InvalidOperationException("invalid input");
            }
            input.RemoveAt(0);
            input.RemoveAt(input.Count - 1);
            return input;
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
            return -1;
        }

        private List<object> Alter(List<object> input)
        {
            List<object> output = new List<object>();
            _lastNumber = "";
            foreach (var item in input)
            {
                if (item is string && _lastNumber == ")" && item.ToString().Contains("("))
                {
                    _lastNumber = "";
                    output.Add("*");
                }
                else if (item is double)
                {
                    if (_lastNumber == ")")
                    {
                        output.Add("*");
                    }
                    _lastNumber = ")";
                }
                else if (item is string && item.ToString() == ")" && item.ToString() != "")
                {
                    _lastNumber = ")";
                }
                else if (item is string && item.ToString() == ")" && _lastNumber == ")")
                {
                    output.RemoveAt(Filter.Count - 1);
                }
                else if (item is string && item.ToString() == ")" || item.ToString() == "+" || item.ToString() == "*" || item.ToString() == "-" || item.ToString() == "/" || item.ToString() == ",")
                {
                    _lastNumber = "";
                }
                else if (item is string && _lastNumber == ")" && !item.ToString().Contains("("))
                {
                    if (item.ToString() != "+" || item.ToString() != "-" || item.ToString() != "/" || item.ToString() != "*")
                    {
                        output.Add("*");
                    }
                    _lastNumber = "";
                }
                output.Add(item);
            }
            return output;
        }

        private void AndOrXor()
        {
            double axon = 0;
            while (Filter.Contains("&"))
            {
                axon = _mathFunctions.And(Convert.ToByte(Filter[Filter.IndexOf("&") - 1]), Convert.ToByte(Filter[Filter.IndexOf("&") + 1]));
                Filter.RemoveAt(Filter.IndexOf("&") - 1);
                Filter.RemoveAt(Filter.IndexOf("&") + 1);
                Filter.Insert(Filter.IndexOf("&"), axon.ToString());
                Filter.RemoveAt(Filter.IndexOf("&"));
            }
            while (Filter.Contains("|"))
            {
                axon = _mathFunctions.Or(Convert.ToByte(Filter[Filter.IndexOf("|") - 1]), Convert.ToByte(Filter[Filter.IndexOf("|") + 1]));
                Filter.RemoveAt(Filter.IndexOf("|") - 1);
                Filter.RemoveAt(Filter.IndexOf("|") + 1);
                Filter.Insert(Filter.IndexOf("|"), axon.ToString());
                Filter.RemoveAt(Filter.IndexOf("|"));
            }
            while (Filter.Contains("^"))
            {
                axon = _mathFunctions.Xor(Convert.ToByte(Filter[Filter.IndexOf("^") - 1]), Convert.ToByte(Filter[Filter.IndexOf("^") + 1]));
                Filter.RemoveAt(Filter.IndexOf("^") - 1);
                Filter.RemoveAt(Filter.IndexOf("^") + 1);
                Filter.Insert(Filter.IndexOf("^"), axon.ToString());
                Filter.RemoveAt(Filter.IndexOf("^"));
            }
        }

        private void MainOut()
        {
            while (Filter.Any(a => a.ToString() == "e"))
            {
                Filter[Filter.FindIndex(a => a.ToString() == "e")] = Complex.Exp(1);
            }
            Filter = Alter(Filter);
            Exe(Filter);
        }

        #endregion

        #region public methods

        public string Result()
        {
            Filter.Clear();
            foreach (string item in Question.Split(' '))
            {
                if (!string.IsNullOrEmpty(item) && item != " ")
                {
                    if (!item.Contains(','))
                    {
                        Filter.Add(Run(item));
                    }
                    else
                    {
                        foreach (string item1 in item.Split(','))
                        {
                            if (item1 != "" && !string.IsNullOrEmpty(item1))
                            {
                                Filter.Add(Run(item1));
                            }
                            Filter.Add(",");
                        }
                        Filter.RemoveAt(Filter.Count - 1);
                    }
                }
            }
            if (Filter.Any(a => a.ToString().Contains("Prime")))
            {
                if (Filter.Count == 3 && int.TryParse(Filter[1].ToString(), out _outResult))
                {
                    if (Filter[0].ToString() == "Prime(")
                    {
                        Filter[1] = _mathFunctions.Prime(Convert.ToDouble(Filter[1])).ToString();
                        Filter.RemoveAt(2);
                        Filter.RemoveAt(0);
                    }
                    else if (Filter[0].ToString() == "ListOfPrime(")
                    {
                        _lastNumber = null;
                        foreach (double item in _mathFunctions.ListOfPrime(Convert.ToInt32(Filter[1])))
                        {
                            if (_lastNumber != null)
                            {
                                _lastNumber += "," + item;
                            }
                            else
                            {
                                _lastNumber += item;
                            }
                        }
                        Filter[1] = _lastNumber;
                        Filter.RemoveAt(2);
                        Filter.RemoveAt(0);
                        _lastNumber = null;
                    }
                }
                else
                {
                    Filter.Clear();
                    MessageBox.Show("input should contains a number or remove prime or list of prime in input", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (Filter.Any(a => a.ToString() == "Pol(" || a.ToString() == "Rect("))
            {
                if (Filter.Count == 5 && Filter[2].ToString() == "," && double.TryParse(Filter[1].ToString(), out tryParse) && double.TryParse(Filter[3].ToString(), out tryParse))
                {
                    MainOut();
                }
                else
                {
                    Filter = new List<object>();
                    MessageBox.Show("Input should like Pol(1,2) or Rect(1,2) or remove Pol( or Rect(", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (Filter.Any(a => a.ToString() == "Matrix"))
            {
                if (Filter.All(a => a.ToString() == "A" || a.ToString() == "B" || a.ToString() == "C" || a.ToString() == "D" || a.ToString() == "E" || a.ToString() == "F" || a.ToString() == "Matrix" || a.ToString() == "Add(" || a.ToString() == "Sub(" || a.ToString() == "Mul(" || a.ToString() == "+" || a.ToString() == "-" || a.ToString() == "/" || a.ToString() == "*" || a.ToString() == ")" || a.ToString() == ","))
                {
                    MainOut();
                }
                else
                {
                    Filter = new List<object>();
                    MessageBox.Show("Input should have +, -, *, /, Add, Sub, Mul, Matrix, A-F", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (Filter.Contains("^") || Filter.Contains("|") || Filter.Contains("&"))
            {
                if (!Filter.All(a => a.ToString() == "1" || a.ToString() == "0" || a.ToString() == "^" || a.ToString() == "&" || a.ToString() == "|"))
                {
                    Filter.Clear();
                    MessageBox.Show("input should have 1 or 0", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        AndOrXor();
                    }
                    catch (Exception)
                    {
                        Filter.Clear();
                        MessageBox.Show("invalid input", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MainOut();
            }
            try
            {
                if (double.TryParse(Filter[0].ToString(), out tryParse) && !Filter[0].ToString().Contains(","))
                {
                    return Math.Round(Convert.ToDouble(Filter[0]), 10).ToString();
                }
                else
                {
                    return Filter[0].ToString();
                }
            }
            catch (Exception) { return null; }
        }

        #endregion
    }
}
