using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Samples
{
    public class MathTricks : INotifyPropertyChanged
    {
        private string _input, _result, _temp = null;

        ArrayList _numbersList = new ArrayList();

        ArrayList _numbersTemp = new ArrayList();

        int _check, _count = 0, _count1 = 0;

        double _temp1;

        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                if (int.TryParse(_input.Substring(_input.Length - 1, 1), out _check))
                {
                    _temp += _input.Substring(_input.Length - 1, 1);
                }
                else
                {
                    if (_temp != null)
                    {
                        _numbersList.Add(_temp);
                        _temp = null;
                    }
                    _numbersList.Add(_input.Substring(_input.Length - 1, 1));
                }
                OnPropertyChanged("Input");
            }
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result()
        {
            if (_temp != null)
            {
                _numbersList.Add(_temp);
                _temp = null;
            }
            _count = 0;
            while (_numbersList.Contains("(") && _numbersList.Contains(")"))
            {
                try
                {
                    while (_numbersList.IndexOf(")", _count) == _numbersList.IndexOf("(", _numbersList.IndexOf(")", _count)) - 1)
                    {
                        _numbersList.Insert(_numbersList.IndexOf(")", _count), "*");
                        _numbersList.RemoveAt(_numbersList.IndexOf("(", _numbersList.IndexOf(")", _count)));
                        _numbersList.RemoveAt(_numbersList.IndexOf(")", _count));
                    }
                    while (_numbersList.IndexOf("(", _count1) < _numbersList.IndexOf(")", _numbersList.IndexOf("(", _count1)))
                    {
                        _numbersTemp.Clear();
                        int startIndex = _numbersList.IndexOf("(", _count1) + 1;
                        int endIndex = _numbersList.IndexOf(")", _numbersList.IndexOf("(", _count1));
                        _numbersTemp = FindedList(_numbersList, _numbersTemp, startIndex, endIndex, "add");
                        if (!_numbersTemp.Contains("(") && !_numbersTemp.Contains(")"))
                        {
                            _numbersList = FindedList(_numbersTemp, _numbersList, startIndex, endIndex, "remove");
                            break;
                        }
                        _count1++;
                    }
                    _count++;
                }
                catch (Exception)
                {
                    _count = 0; _count1 = 0;
                }
            }
            if (!_numbersList.Contains("(") && !_numbersList.Contains(")"))
            {
                _numbersList = Math(_numbersList);
            }
            _result = null;
            foreach (double item in _numbersList)
            {
                _result += item;
            }
            return _result;
        }

        public ArrayList FindedList(ArrayList From, ArrayList To, int start, int end, string Method)
        {
            if (Method == "add")
            {
                for (int i = start; i < end; i++)
                {
                    To.Add(From[i]);
                }
            }
            else if (Method == "remove")
            {
                try
                {
                    for (int i = end - 2; i >= start; i--)
                    {
                        To.RemoveAt(i);
                    }
                }
                catch (Exception)
                {
                    int j = 0;
                    for (int i = start; i < end; i = i + 2)
                    {
                        To.Insert(i, From[j]);
                        j = j + 2;
                    }
                }
                To[start] = Math(_numbersTemp)[0];
                To.RemoveAt(start + 1);
                To.RemoveAt(start - 1);
            }
            return To;
        }

        public ArrayList Math(ArrayList input)
        {
            while (input.Contains("/"))
            {
                _temp1 = Convert.ToDouble(input[input.IndexOf("/") - 1]) / Convert.ToDouble(input[input.IndexOf("/") + 1]);
                input.RemoveAt(input.IndexOf("/") - 1);
                input.RemoveAt(input.IndexOf("/") + 1);
                input.Insert(input.IndexOf("/"), _temp1);
                input.RemoveAt(input.IndexOf("/"));
            }
            while (input.Contains("*"))
            {
                _temp1 = Convert.ToDouble(input[input.IndexOf("*") - 1]) * Convert.ToDouble(input[input.IndexOf("*") + 1]);
                input.RemoveAt(input.IndexOf("*") - 1);
                input.RemoveAt(input.IndexOf("*") + 1);
                input.Insert(input.IndexOf("*"), _temp1);
                input.RemoveAt(input.IndexOf("*"));
            }
            while (input.Contains("-"))
            {
                _temp1 = Convert.ToDouble(input[input.IndexOf("-") - 1]) - Convert.ToDouble(input[input.IndexOf("-") + 1]);
                input.RemoveAt(input.IndexOf("-") - 1);
                input.RemoveAt(input.IndexOf("-") + 1);
                input.Insert(input.IndexOf("-"), _temp1);
                input.RemoveAt(input.IndexOf("-"));
            }
            while (input.Contains("+"))
            {
                _temp1 = Convert.ToDouble(input[input.IndexOf("+") - 1]) + Convert.ToDouble(input[input.IndexOf("+") + 1]);
                input.RemoveAt(input.IndexOf("+") - 1);
                input.RemoveAt(input.IndexOf("+") + 1);
                input.Insert(input.IndexOf("+"), _temp1);
                input.RemoveAt(input.IndexOf("+"));
            }
            return input;
        }
    }
}
