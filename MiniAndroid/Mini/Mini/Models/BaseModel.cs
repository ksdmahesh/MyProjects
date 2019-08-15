using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mini.Models
{

    public enum Layout
    {
        Main, List, ListItems, Others
    }

    public enum SpinnerItems
    {
        StatMat, A_F, Constants, AP, GP, HP, Conv, Conv_From, Conv_To
    }

    public class BaseModel
    {
        #region private variables

        private string _content, _qText, _aText, _temp, _matrixM, _matrixN, _statCount;

        private bool _isMain = true, _isOthers, _isList, _isListItems, _isDegree = true, _isRadient, _isGradient;

        private int _startIndex, _keyIndex, _valueIndex, _spinnerIndex;

        private Layout _layout = new Layout();

        private Dictionary<object, string[]> _items = new Dictionary<object, string[]>();

        private ArrayAdapter<string> _arrayAdapter;

        private Dictionary<int, List<double>> _statLists = new Dictionary<int, List<double>>();

        private Dictionary<int, double[,]> _matList = new Dictionary<int, double[,]>();

        private List<double> _tempStatList = new List<double>();

        private MathFunctions _mathFunctions = new MathFunctions();

        private string _toConvert;

        private double[,] _tempMatList = new double[0, 0];

        private Converter _converter = new Converter();

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

        #endregion

        #region public properties

        public Angle Angle1
        {
            get
            {
                return _angle1;
            }
            set
            {
                _angle1 = value;
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
            }
        }

        public string ToConvert
        {
            get
            {
                return _toConvert;
            }
            set
            {
                _toConvert = value;
            }
        }

        public MathFunctions MathFunctions
        {
            get
            {
                return _mathFunctions;
            }
            set
            {
                _mathFunctions = value;
            }
        }

        public double[,] TempMatList
        {
            get
            {
                return _tempMatList;
            }
            set
            {
                _tempMatList = value;
            }
        }

        public List<double> TempStatList
        {
            get
            {
                return _tempStatList;
            }
            set
            {
                _tempStatList = value;
            }
        }

        public string MatrixM
        {
            get
            {
                return _matrixM;
            }
            set
            {
                _matrixM = value;
            }
        }

        public string MatrixN
        {
            get
            {
                return _matrixN;
            }
            set
            {
                _matrixN = value;
            }
        }

        public string StatCount
        {
            get
            {
                return _statCount;
            }
            set
            {
                _statCount = value;
            }
        }

        public bool IsDegree
        {
            get
            {
                return _isDegree;
            }
            set
            {
                _isDegree = value;
            }
        }

        public bool IsRadient
        {
            get
            {
                return _isRadient;
            }
            set
            {
                _isRadient = value;
            }
        }

        public bool IsGradient
        {
            get
            {
                return _isGradient;
            }
            set
            {
                _isGradient = value;
            }
        }

        public bool IsMain
        {
            get
            {
                return _isMain;
            }
            set
            {
                _isMain = value;
                _isList = !value;
                _isOthers = !value;
                _isListItems = !value;
            }
        }

        public bool IsOthers
        {
            get
            {
                return _isOthers;
            }
            set
            {
                _isOthers = value;
                _isList = !value;
                _isMain = !value;
                _isListItems = !value;
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
                _isMain = !value;
                _isOthers = !value;
                _isListItems = !value;
            }
        }

        public bool IsListItems
        {
            get
            {
                return _isListItems;
            }
            set
            {
                _isListItems = value;
                _isList = !value;
                _isOthers = !value;
                _isMain = !value;
            }
        }

        public Dictionary<int, List<double>> StatLists
        {
            get
            {
                return _statLists;
            }
            set
            {
                _statLists = value;
            }
        }

        public Dictionary<int, double[,]> MatLists
        {
            get
            {
                return _matList;
            }
            set
            {
                _matList = value;
            }
        }

        public Dictionary<object, string[]> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public ArrayAdapter<string> ArrayAdapter
        {
            get
            {
                return _arrayAdapter;
            }
            set
            {
                _arrayAdapter = value;
                _arrayAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
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
            }
        }

        public string QText
        {
            get
            {
                return _qText;
            }
            set
            {
                _qText = value;
            }
        }

        public string AText
        {
            get
            {
                return _aText;
            }
            set
            {
                _aText = value;
            }
        }

        public int StartIndex
        {
            get
            {
                return _startIndex;
            }
            set
            {
                _startIndex = value;
            }
        }

        public int KeyIndex
        {
            get
            {
                return _keyIndex;
            }
            set
            {
                _keyIndex = value;
            }
        }

        public int ValueIndex
        {
            get
            {
                return _valueIndex;
            }
            set
            {
                _valueIndex = value;
            }
        }

        public int SpinnerIndex
        {
            get
            {
                return _spinnerIndex;
            }
            set
            {
                _spinnerIndex = value;
            }
        }

        #endregion

        #region public constructor

        public BaseModel()
        {
            _items.Add(SpinnerItems.StatMat, new string[] { "Matrix", "Statistics" });
            _items.Add(SpinnerItems.A_F, new String[] { "A", "B", "C", "D", "E", "F" });
            _items.Add(SpinnerItems.Constants, MathFunctions.GetList().ToArray());
            _items.Add(SpinnerItems.AP, new String[] { "Sum", "Sum-Difference" });
            _items.Add(SpinnerItems.GP, _items[SpinnerItems.AP]);
            _items.Add(SpinnerItems.HP, _items[SpinnerItems.AP]);
            _items.Add(SpinnerItems.Conv, new string[] { "Angle", "Area", "Base", "Energy", "Length", "Power", "Pressure", "Temperature", "Time", "Velocity", "Volume", "Weight/Mass" });
            _items.Add(Converter.Angle, new String[] { "Degree", "Gradian", "Radian" });
            _items.Add(Converter.Area, new string[] { "Acres", "Hectares", "Square centimeter", "Square feet", "Square inch", "Square kilometer", "Square meter", "Square mile", "Square millimeter", "Square yard" });
            _items.Add(Converter.Base, new String[] { "Binary", "Decimal", "Hexa Decimal", "Octal" });
            _items.Add(Converter.Energy, new string[] { "British Thermal Unit", "Calorie", "Electron-volts", "Foot-pound", "Joule", "Kilocalorie", "Kilojoule" });
            _items.Add(Converter.Length, new String[] { "Angstorm", "Centimeter", "Chain", "Fathom", "Feet", "Hand", "Inch", "Kilometer", "Link", "Meter", "Microns", "Mile", "Millimeter", "Nanometer", "Nautical Mile", "PICA", "Rods", "Span", "Yard" });
            _items.Add(Converter.Power, new string[] { "BTU/minute", "Foot-Pound/minute", "Horsepower", "Kilowatt", "Watt" });
            _items.Add(Converter.Pressure, new String[] { "Atmosphere", "Bar", "Kilo Pascal", "Millimeter of mercury", "Pascal", "Pound per square inch(PSI)" });
            _items.Add(Converter.Temperature, new string[] { "Degree Celsius", "Degree Fahrenheit", "Kelvin" });
            _items.Add(Converter.Time, new String[] { "Day", "Hour", "Microsecond", "Millisecond", "Minute", "Second", "Week" });
            _items.Add(Converter.Velocity, new string[] { "Centimeter per second", "Feet per second", "Kilometer per hour", "Knots", "Mach(at std. atm)", "Meter per second", "Miles per hour" });
            _items.Add(Converter.Volume, new String[] { "Cubic centimeter", "Cubic feet", "Cubic inch", "Cubic meter", "Cubic yard", "Fluid ounce (UK)", "Fluid ounce (US)", "Gallon (UK)", "Gallon (US)", "Liter", "Pint (UK)", "Pint (US)", "Quart (UK)", "Quart (US)" });
            _items.Add(Converter.Weight, new string[] { "Carat", "Centigram", "Decigram", "Dekagram", "Gram", "Hectogram", "Kilogram", "Long ton", "Milligram", "Ounce", "Pound", "Short ton", "Stone", "Tonne" });
            _items.Add(SpinnerItems.Conv_From, _items[Converter.Angle]);
            _items.Add(SpinnerItems.Conv_To, _items[Converter.Angle]);
            for (int i = 0; i < 6; i++)
            {
                MatLists.Add(i, new double[0, 0]);
                StatLists.Add(i, new List<double>());
            }
        }

        #endregion
    }
}