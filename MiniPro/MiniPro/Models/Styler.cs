using MiniPro.BaseClasses;
using MiniPro.Solutions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniPro.Models
{
    public class Styler : Notifier
    {
        #region private variables

        private string _windowIconPath = App.WindowIconPath;

        private string _helpIconPath = App.HelpIconPath;

        private string _proTitle = App.TitleName;

        private string _name;

        private string _selectedItem;

        private ObservableCollection<string> _itemSource = new ObservableCollection<string>();

        ObservableCollection<ObservableCollection<string>> _itemSources = new ObservableCollection<ObservableCollection<string>>();

        private List<string> _listCollection = new List<string>();

        private List<double> _list = new List<double>();

        private List<double> _list1 = new List<double>();

        private List<List<double>> _lists = new List<List<double>>();

        private List<double[,]> _matList = new List<double[,]>();

        private bool _isVisible, _isEnabled, _isInnerEnabled;

        private double _opacity = 1.0, _inverse = 0.8;

        private List<int> _listCount = new List<int>();

        private double _value;

        private int _indexes = 0;

        private WindowState _windowState;

        private bool _manual;

        private bool _statVisible;

        private bool _matVisible = true;

        private bool _expandVisible;

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

        public string WindowIconPath
        {
            get
            {
                return _windowIconPath;
            }
            set
            {
                _windowIconPath = value;
                OnPropertyChanged("WindowIconPath");
            }
        }

        public string HelpIconPath
        {
            get
            {
                return _helpIconPath;
            }
            set
            {
                _helpIconPath = value;
                OnPropertyChanged("HelpIconPath");
            }
        }

        public string ProTitle
        {
            get
            {
                return _proTitle;
            }
            set
            {
                _proTitle = value;
                OnPropertyChanged("ProTitle");
            }
        }

        public string SelectedItem
        {
            get
            {

                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
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

        public List<string> ListCollection
        {
            get
            {
                return _listCollection;
            }
            set
            {
                _listCollection = value;
                OnPropertyChanged("ListCollection");
            }
        }

        public List<double> List
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }

        public bool StatVisible
        {
            get
            {
                return _statVisible;
            }
            set
            {
                _statVisible = value;
                OnPropertyChanged("StatVisible");
            }
        }

        public bool MatVisible
        {
            get
            {
                return _matVisible;
            }
            set
            {
                _matVisible = value;
                OnPropertyChanged("MatVisible");
            }
        }

        public List<double> List1
        {
            get
            {
                return _list1;
            }
            set
            {
                _list1 = value;
                OnPropertyChanged("List1");
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

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public bool IsInnerEnabled
        {
            get
            {
                return _isInnerEnabled;
            }
            set
            {
                _isInnerEnabled = value;
                OnPropertyChanged("IsInnerEnabled");
            }
        }

        public List<int> ListCount
        {
            get
            {
                return _listCount;
            }
            set
            {
                _listCount = value;
                OnPropertyChanged("ListCount");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                _opacity = value;
                OnPropertyChanged("Opacity");
            }
        }

        public double Inverse
        {
            get
            {
                return _inverse;
            }
            set
            {
                _inverse = value;
                OnPropertyChanged("Inverse");
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

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public WindowState windowState
        {
            get
            {
                return _windowState;
            }
            set
            {
                _windowState = value;
                OnPropertyChanged("windowState");
            }
        }

        public bool manual
        {
            get
            {
                return _manual;
            }
            set
            {
                _manual = value;
                OnPropertyChanged("manual");
            }
        }

        public bool ExpandVisible
        {
            get
            {
                return _expandVisible;
            }
            set
            {
                _expandVisible = value;
                OnPropertyChanged("ExpandVisible");
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

        #endregion

    }
}
