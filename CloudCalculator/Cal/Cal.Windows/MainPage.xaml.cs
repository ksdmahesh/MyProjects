using Cal.Models;
using MiniPro.Solutions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Cal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        #region private variables

        private Dictionary<int, string> variable = new Dictionary<int, string>();

        private Solve _solver;

        private Styler _styler = new Styler();

        private List<Complex> _comp = new List<Complex>();

        private double tryParse;

        private string inputItemText, _key;

        private int start = 0, qStart = 0, _count = 0, _indexList;

        private List<object> _items = new List<object>();

        private Complex _temp1 = new Complex();

        private bool _isHyp = true, _need;

        private Brush color = new SolidColorBrush();

        #endregion

        #region public properties
        public Solve Solver
        {
            get
            {
                return _solver;
            }
            set
            {
                _solver = value;
                OnProperty("Solver");
            }
        }
        public Styler Styler
        {
            get
            {
                return _styler;
            }
        }

        #endregion

        #region public constructor

        public MainPage()
        {
            this.InitializeComponent();
            ConstantListBox.ItemsSource = Styler.Math.GetList();
            ConstantListBox.SelectedIndex = 0;
            Styler.ItemSources = new ObservableCollection<ObservableCollection<string>>() { Styler.Math._angle, Styler.Math._area, Styler.Math._base, Styler.Math._energy, Styler.Math._length, Styler.Math._power, Styler.Math._pressure, Styler.Math._temperature, Styler.Math._time, Styler.Math._velocity, Styler.Math._volume, Styler.Math._weight };
            Styler.ItemSource = Styler.Math._angle;
            from.ItemsSource = Styler.Math._angle;
            to.ItemsSource = Styler.Math._angle;
            from.SelectedIndex = 0;
            to.SelectedIndex = 0;
            this.DataContext = this;
            QABox.AddHandler(PointerPressedEvent, new PointerEventHandler(QABox_PointerPressed), true);
            QABox.AddHandler(PointerReleasedEvent, new PointerEventHandler(QABox_PointerPressed), true);
            QBox.AddHandler(TappedEvent, new TappedEventHandler(QBox_Tapped), true);
            QBox.ContextMenuOpening += QBox_ContextMenuOpening;
            QABox.AddHandler(TappedEvent, new TappedEventHandler(QABox_Tapped), true);
            QBox.AddHandler(PointerPressedEvent, new PointerEventHandler(QBox_PointerPressed), true);
            ABox.AddHandler(TappedEvent, new TappedEventHandler(ABox_Tapped), true);
            color = Hyperbola.Background;
            for (int i = 0; i < 6; i++)
            {
                Styler.Lists.Add(new List<double>());
            }
            for (int i = 0; i < 6; i++)
            {
                Styler.MatList.Add(new double[0, 0]);
            }
            _solver = new Solve(Styler);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        void QBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
        }

        private void QABox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            QBox.ReleasePointerCaptures();
        }

        private void QBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            QBox.ReleasePointerCaptures();
            MouseClick(false);
        }

        private void QBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            QBox.Focus(FocusState.Keyboard);
            QBox.ReleasePointerCaptures();
            MouseClick(false);
            e.Handled = true;
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
                for (int i = 1; i <= QBox.Text.Length; i++)
                {
                    temp += QBox.Text.Substring(i - 1, 1);
                    if (temp != " " && QBox.Text.Substring(i - 1, 1) == " " && temp.Contains(" "))
                    {
                        variable.Add(i, temp);
                        temp = null;
                    }
                    else if (temp.Length > 0 && !temp.Contains(" "))
                    {
                        try
                        {

                            if (QBox.Text.Substring(i, 1) == " ")
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
                start = QBox.SelectionStart;
                if (start > 0)
                {
                    foreach (var item in variable)
                    {
                        if (start <= item.Key)
                        {
                            start = item.Key;
                            break;
                        }
                    }
                }
                QBox.SelectionStart = start;
            }
            start = QBox.SelectionStart;
        }

        #endregion

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private methods

        private void PercentExe()
        {
            EqualExe();
            if (Styler.MainResult != null)
            {
                if (Styler.MainResult != "[Collections to view Expand]" && !Styler.MainResult.Contains("i") && !Styler.MainResult.Contains("+") && !Styler.MainResult.Contains("∠"))
                {
                    if (double.TryParse(Styler.MainResult, out tryParse))
                    {
                        Styler.MainResult = (tryParse / 100).ToString();
                    }
                }
            }
            if (Styler.MainResult != null)
            {
                Styler.QList.Add(QBox.Text);
                Styler.AList.Add(Styler.MainResult);
                _need = false;
                while (Styler.QList.Count > 10)
                {
                    Styler.QList.RemoveAt(1);
                    Styler.AList.RemoveAt(1);
                }
            }
        }

        private void EqualExe()
        {
            try
            {
                Solver.Question = QBox.Text;
                Styler.MainResult = Solver.Result();
                if (Styler.MainResult == "[Collections to view Expand]")
                {
                    //Styler.ExpandVisible = true;
                }
                else if (!string.IsNullOrWhiteSpace(Styler.MainResult))
                {
                    if (Styler.MainResult.Contains("(") && Styler.MainResult.Contains(")") && Styler.MainResult.Contains(","))
                    {
                        if (Styler.MainResult.Substring(Styler.MainResult.IndexOf(",") + 1).Contains("-"))
                        {
                            Styler.MainResult = Styler.MainResult.Remove(Styler.MainResult.IndexOf("-"), 1);
                            Styler.MainResult = Styler.MainResult.Replace(",", "-i");
                        }
                        else
                        {
                            Styler.MainResult = Styler.MainResult.Replace(",", "+i");
                        }
                        Styler.MainResult = Styler.MainResult.Substring(1, Styler.MainResult.Length - 2);
                        while (Styler.MainResult.Any(a => a == ' '))
                        {
                            Styler.MainResult = Styler.MainResult.Remove(Styler.MainResult.IndexOf(' '), 1);
                        }
                        if (double.TryParse(Styler.MainResult.Substring(Styler.MainResult.IndexOf("i") + 1), out tryParse))
                        {
                            if (tryParse == 0)
                            {
                                Styler.MainResult = Styler.MainResult.Remove(Styler.MainResult.IndexOf("i") - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Styler.MessageBox(ex.Message);
                Styler.MainResult = null;
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

        private void Stat(string type)
        {
            try
            {
                if (QBox.Text == "A" || QBox.Text == "B" || QBox.Text == "C" || QBox.Text == "D" || QBox.Text == "E" || QBox.Text == "F")
                {
                    if (type == "SD")
                    {
                        Styler.MainResult = Styler.Math.StandardDeviation(Styler.Math.Variance(Styler.Lists[Index(QBox.Text)]));
                    }
                    else if (type == "Var")
                    {
                        Styler.MainResult = Styler.Math.Variance(Styler.Lists[Index(QBox.Text)]);
                    }
                    else if (type == "Mean")
                    {
                        Styler.MainResult = Styler.Math.Mean(Styler.Lists[Index(QBox.Text)]);
                    }
                    else if (type == "MeanSquare")
                    {
                        Styler.MainResult = Styler.Math.MeanSquare(Styler.Lists[Index(QBox.Text)]);
                    }
                    else if (type == "Sum")
                    {
                        Styler.MainResult = Styler.Math.Sum(Styler.Lists[Index(QBox.Text)]);
                    }
                    else if (type == "SumOfSquare")
                    {
                        Styler.MainResult = Styler.Math.SquareSum(Styler.Lists[Index(QBox.Text)]);
                    }
                }
                else
                {
                    Styler.MainResult = null;
                    Styler.MessageBox("Please input A or B or C...F it should not contains any characters");
                }
            }
            catch (Exception)
            {
                Styler.MainResult = null;
                Styler.MessageBox("Please input A or B or C...F it should not contains any characters");
            }
            if (Styler.MainResult != null)
            {
                Styler.QList.Add(QBox.Text);
                Styler.AList.Add(Styler.MainResult);
                _need = false;
                while (Styler.QList.Count > 10)
                {
                    Styler.QList.RemoveAt(1);
                    Styler.AList.RemoveAt(1);
                }
            }
        }

        private void OnProperty(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void QABox_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            QBox.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            e.Handled = true;
        }

        private void QBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Styler.Reverse.Length <= QBox.Text.Length)
                {
                    Styler.Reverse = QBox.Text;
                }
            }
            catch (Exception)
            {
                Styler.Reverse = QBox.Text;
            }
            MouseClick(true);
        }

        private void Convert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (((ComboBoxItem)(sender as ComboBox).SelectedItem).Content.ToString())
                {
                    case "Angle":
                        {
                            Styler.Converter = Converter.Angle;
                            break;
                        }
                    case "Area":
                        {
                            Styler.Converter = Converter.Area;
                            break;
                        }
                    case "Base":
                        {
                            Styler.Converter = Converter.Base;
                            break;
                        }
                    case "Energy":
                        {
                            Styler.Converter = Converter.Energy;
                            break;
                        }
                    case "Length":
                        {
                            Styler.Converter = Converter.Length;
                            break;
                        }
                    case "Power":
                        {
                            Styler.Converter = Converter.Power;
                            break;
                        }
                    case "Pressure":
                        {
                            Styler.Converter = Converter.Pressure;
                            break;
                        }
                    case "Temperature":
                        {
                            Styler.Converter = Converter.Temperature;
                            break;
                        }
                    case "Time":
                        {
                            Styler.Converter = Converter.Time;
                            break;
                        }
                    case "Velocity":
                        {
                            Styler.Converter = Converter.Velocity;
                            break;
                        }
                    case "Volume":
                        {
                            Styler.Converter = Converter.Volume;
                            break;
                        }
                    case "Weight/Mass":
                        {
                            Styler.Converter = Converter.Weight;
                            break;
                        }
                }
                from.SelectedIndex = 0;
                to.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void from_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is string)
                switch ((sender as ComboBox).SelectedItem.ToString())
                {
                    case "Degree": { Styler.Angle1 = Angle.Degree; break; }
                    case "Gradian": { Styler.Angle1 = Angle.Gradian; break; }
                    case "Radian": { Styler.Angle1 = Angle.Radian; break; }
                    case "Acres": { Styler.Area1 = Area.Acres; break; }
                    case "Hectares": { Styler.Area1 = Area.Hectares; break; }
                    case "Square centimeter": { Styler.Area1 = Area.SquareCentimeter; break; }
                    case "Square feet": { Styler.Area1 = Area.SquareFeet; break; }
                    case "Square inch": { Styler.Area1 = Area.SquareInch; break; }
                    case "Square kilometer": { Styler.Area1 = Area.SquareKilometer; break; }
                    case "Square meter": { Styler.Area1 = Area.SquareMeter; break; }
                    case "Square mile": { Styler.Area1 = Area.SquareMile; break; }
                    case "Square millimeter": { Styler.Area1 = Area.SquareMillimeter; break; }
                    case "Square yard": { Styler.Area1 = Area.SquareYard; break; }
                    case "Binary": { Styler.Base1 = Base.Binary; break; }
                    case "Decimal": { Styler.Base1 = Base.Decimal; break; }
                    case "Hexa Decimal": { Styler.Base1 = Base.HexaDecimal; break; }
                    case "Octal": { Styler.Base1 = Base.Octal; break; }
                    case "British Thermal Unit": { Styler.Energy1 = Energy.BritishThermalUnit; break; }
                    case "Calorie": { Styler.Energy1 = Energy.Calorie; break; }
                    case "Electron-volts": { Styler.Energy1 = Energy.ElectronVolts; break; }
                    case "Foot-pound": { Styler.Energy1 = Energy.FootPound; break; }
                    case "Joule": { Styler.Energy1 = Energy.Joule; break; }
                    case "Kilocalorie": { Styler.Energy1 = Energy.KiloCalorie; break; }
                    case "Kilojoule": { Styler.Energy1 = Energy.KiloJoule; break; }
                    case "Angstorm": { Styler.Length1 = Length.Angstorm; break; }
                    case "Centimeter": { Styler.Length1 = Length.Centimeter; break; }
                    case "Chain": { Styler.Length1 = Length.Chain; break; }
                    case "Fathom": { Styler.Length1 = Length.Fathom; break; }
                    case "Feet": { Styler.Length1 = Length.Feet; break; }
                    case "Hand": { Styler.Length1 = Length.Hand; break; }
                    case "Inch": { Styler.Length1 = Length.Inch; break; }
                    case "Kilometer": { Styler.Length1 = Length.Kilometer; break; }
                    case "Link": { Styler.Length1 = Length.Link; break; }
                    case "Meter": { Styler.Length1 = Length.Meter; break; }
                    case "Microns": { Styler.Length1 = Length.Microns; break; }
                    case "Mile": { Styler.Length1 = Length.Mile; break; }
                    case "Millimeter": { Styler.Length1 = Length.Millimeter; break; }
                    case "Nanometer": { Styler.Length1 = Length.Nanometer; break; }
                    case "Nautical Mile": { Styler.Length1 = Length.NauticalMile; break; }
                    case "PICA": { Styler.Length1 = Length.PICA; break; }
                    case "Rods": { Styler.Length1 = Length.Rods; break; }
                    case "Span": { Styler.Length1 = Length.Span; break; }
                    case "Yard": { Styler.Length1 = Length.Yard; break; }
                    case "BTU/minute": { Styler.Power1 = Powers.BTUminute; break; }
                    case "Foot-Pound/minute": { Styler.Power1 = Powers.FootPound; break; }
                    case "Horsepower": { Styler.Power1 = Powers.Horsepower; break; }
                    case "Kilowatt": { Styler.Power1 = Powers.Kilowatt; break; }
                    case "Watt": { Styler.Power1 = Powers.Watt; break; }
                    case "Atmosphere": { Styler.Pressure1 = Pressure.Atmosphere; break; }
                    case "Bar": { Styler.Pressure1 = Pressure.Bar; break; }
                    case "Kilo Pascal": { Styler.Pressure1 = Pressure.KiloPascal; break; }
                    case "Millimeter of mercury": { Styler.Pressure1 = Pressure.MillimeterOfMercury; break; }
                    case "Pascal": { Styler.Pressure1 = Pressure.Pascal; break; }
                    case "Pound per square inch(PSI)": { Styler.Pressure1 = Pressure.PoundPerSquareInch; break; }
                    case "Degree Celsius": { Styler.Temperature1 = Temperature.DegreeCelsius; break; }
                    case "Degree Fahrenheit": { Styler.Temperature1 = Temperature.DegreeFahrenheit; break; }
                    case "Kelvin": { Styler.Temperature1 = Temperature.Kelvin; break; }
                    case "Day": { Styler.Time1 = Time.Day; break; }
                    case "Hour": { Styler.Time1 = Time.Hour; break; }
                    case "Microsecond": { Styler.Time1 = Time.MicroSecond; break; }
                    case "Millisecond": { Styler.Time1 = Time.MilliSecond; break; }
                    case "Minute": { Styler.Time1 = Time.Minute; break; }
                    case "Second": { Styler.Time1 = Time.Second; break; }
                    case "Week": { Styler.Time1 = Time.Week; break; }
                    case "Centimeter per second": { Styler.Velocity1 = Velocity.CentimeterPerSecond; break; }
                    case "Feet per second": { Styler.Velocity1 = Velocity.FeetPerSecond; break; }
                    case "Kilometer per hour": { Styler.Velocity1 = Velocity.KilometerPerHour; break; }
                    case "Knots": { Styler.Velocity1 = Velocity.Knots; break; }
                    case "Mach(at std. atm)": { Styler.Velocity1 = Velocity.Mach; break; }
                    case "Meter per second": { Styler.Velocity1 = Velocity.MeterPerSecond; break; }
                    case "Miles per hour": { Styler.Velocity1 = Velocity.MilesPerHour; break; }
                    case "Cubic centimeter": { Styler.Volume1 = Volume.CubicCentimeter; break; }
                    case "Cubic feet": { Styler.Volume1 = Volume.CubicFeet; break; }
                    case "Cubic inch": { Styler.Volume1 = Volume.CubicInch; break; }
                    case "Cubic meter": { Styler.Volume1 = Volume.CubicMeter; break; }
                    case "Cubic yard": { Styler.Volume1 = Volume.CubicYard; break; }
                    case "Fluid ounce (UK)": { Styler.Volume1 = Volume.FluidOunceUK; break; }
                    case "Fluid ounce (US)": { Styler.Volume1 = Volume.FluidounceUS; break; }
                    case "Gallon (UK)": { Styler.Volume1 = Volume.GallonUK; break; }
                    case "Gallon (US)": { Styler.Volume1 = Volume.GallonUS; break; }
                    case "Liter": { Styler.Volume1 = Volume.Liter; break; }
                    case "Pint (UK)": { Styler.Volume1 = Volume.PintUK; break; }
                    case "Pint (US)": { Styler.Volume1 = Volume.PintUS; break; }
                    case "Quart (UK)": { Styler.Volume1 = Volume.QuartUK; break; }
                    case "Quart (US)": { Styler.Volume1 = Volume.QuartUS; break; }
                    case "Carat": { Styler.Weight1 = Weight.Carat; break; }
                    case "Centigram": { Styler.Weight1 = Weight.CentiGram; break; }
                    case "Decigram": { Styler.Weight1 = Weight.DeciGram; break; }
                    case "Dekagram": { Styler.Weight1 = Weight.DekaGram; break; }
                    case "Gram": { Styler.Weight1 = Weight.Gram; break; }
                    case "Hectogram": { Styler.Weight1 = Weight.HectoGram; break; }
                    case "Kilogram": { Styler.Weight1 = Weight.KiloGram; break; }
                    case "Long ton": { Styler.Weight1 = Weight.LongTon; break; }
                    case "Milligram": { Styler.Weight1 = Weight.MilliGram; break; }
                    case "Ounce": { Styler.Weight1 = Weight.Ounce; break; }
                    case "Pound": { Styler.Weight1 = Weight.Pound; break; }
                    case "Short ton": { Styler.Weight1 = Weight.ShortTon; break; }
                    case "Stone": { Styler.Weight1 = Weight.Stone; break; }
                    case "Tonne": { Styler.Weight1 = Weight.Tonne; break; }
                }
            GetResult();
        }

        private void to_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is string)
                switch ((sender as ComboBox).SelectedItem.ToString())
                {
                    case "Degree": { Styler.Angle2 = Angle.Degree; break; }
                    case "Gradian": { Styler.Angle2 = Angle.Gradian; break; }
                    case "Radian": { Styler.Angle2 = Angle.Radian; break; }
                    case "Acres": { Styler.Area2 = Area.Acres; break; }
                    case "Hectares": { Styler.Area2 = Area.Hectares; break; }
                    case "Square centimeter": { Styler.Area2 = Area.SquareCentimeter; break; }
                    case "Square feet": { Styler.Area2 = Area.SquareFeet; break; }
                    case "Square inch": { Styler.Area2 = Area.SquareInch; break; }
                    case "Square kilometer": { Styler.Area2 = Area.SquareKilometer; break; }
                    case "Square meter": { Styler.Area2 = Area.SquareMeter; break; }
                    case "Square mile": { Styler.Area2 = Area.SquareMile; break; }
                    case "Square millimeter": { Styler.Area2 = Area.SquareMillimeter; break; }
                    case "Square yard": { Styler.Area2 = Area.SquareYard; break; }
                    case "Binary": { Styler.Base2 = Base.Binary; break; }
                    case "Decimal": { Styler.Base2 = Base.Decimal; break; }
                    case "Hexa Decimal": { Styler.Base2 = Base.HexaDecimal; break; }
                    case "Octal": { Styler.Base2 = Base.Octal; break; }
                    case "British Thermal Unit": { Styler.Energy2 = Energy.BritishThermalUnit; break; }
                    case "Calorie": { Styler.Energy2 = Energy.Calorie; break; }
                    case "Electron-volts": { Styler.Energy2 = Energy.ElectronVolts; break; }
                    case "Foot-pound": { Styler.Energy2 = Energy.FootPound; break; }
                    case "Joule": { Styler.Energy2 = Energy.Joule; break; }
                    case "Kilocalorie": { Styler.Energy2 = Energy.KiloCalorie; break; }
                    case "Kilojoule": { Styler.Energy2 = Energy.KiloJoule; break; }
                    case "Angstorm": { Styler.Length2 = Length.Angstorm; break; }
                    case "Centimeter": { Styler.Length2 = Length.Centimeter; break; }
                    case "Chain": { Styler.Length2 = Length.Chain; break; }
                    case "Fathom": { Styler.Length2 = Length.Fathom; break; }
                    case "Feet": { Styler.Length2 = Length.Feet; break; }
                    case "Hand": { Styler.Length2 = Length.Hand; break; }
                    case "Inch": { Styler.Length2 = Length.Inch; break; }
                    case "Kilometer": { Styler.Length2 = Length.Kilometer; break; }
                    case "Link": { Styler.Length2 = Length.Link; break; }
                    case "Meter": { Styler.Length2 = Length.Meter; break; }
                    case "Microns": { Styler.Length2 = Length.Microns; break; }
                    case "Mile": { Styler.Length2 = Length.Mile; break; }
                    case "Millimeter": { Styler.Length2 = Length.Millimeter; break; }
                    case "Nanometer": { Styler.Length2 = Length.Nanometer; break; }
                    case "Nautical Mile": { Styler.Length2 = Length.NauticalMile; break; }
                    case "PICA": { Styler.Length2 = Length.PICA; break; }
                    case "Rods": { Styler.Length2 = Length.Rods; break; }
                    case "Span": { Styler.Length2 = Length.Span; break; }
                    case "Yard": { Styler.Length2 = Length.Yard; break; }
                    case "BTU/minute": { Styler.Power2 = Powers.BTUminute; break; }
                    case "Foot-Pound/minute": { Styler.Power2 = Powers.FootPound; break; }
                    case "Horsepower": { Styler.Power2 = Powers.Horsepower; break; }
                    case "Kilowatt": { Styler.Power2 = Powers.Kilowatt; break; }
                    case "Watt": { Styler.Power2 = Powers.Watt; break; }
                    case "Atmosphere": { Styler.Pressure2 = Pressure.Atmosphere; break; }
                    case "Bar": { Styler.Pressure2 = Pressure.Bar; break; }
                    case "Kilo Pascal": { Styler.Pressure2 = Pressure.KiloPascal; break; }
                    case "Millimeter of mercury": { Styler.Pressure2 = Pressure.MillimeterOfMercury; break; }
                    case "Pascal": { Styler.Pressure2 = Pressure.Pascal; break; }
                    case "Pound per square inch(PSI)": { Styler.Pressure2 = Pressure.PoundPerSquareInch; break; }
                    case "Degree Celsius": { Styler.Temperature2 = Temperature.DegreeCelsius; break; }
                    case "Degree Fahrenheit": { Styler.Temperature2 = Temperature.DegreeFahrenheit; break; }
                    case "Kelvin": { Styler.Temperature2 = Temperature.Kelvin; break; }
                    case "Day": { Styler.Time2 = Time.Day; break; }
                    case "Hour": { Styler.Time2 = Time.Hour; break; }
                    case "Microsecond": { Styler.Time2 = Time.MicroSecond; break; }
                    case "Millisecond": { Styler.Time2 = Time.MilliSecond; break; }
                    case "Minute": { Styler.Time2 = Time.Minute; break; }
                    case "Second": { Styler.Time2 = Time.Second; break; }
                    case "Week": { Styler.Time2 = Time.Week; break; }
                    case "Centimeter per second": { Styler.Velocity2 = Velocity.CentimeterPerSecond; break; }
                    case "Feet per second": { Styler.Velocity2 = Velocity.FeetPerSecond; break; }
                    case "Kilometer per hour": { Styler.Velocity2 = Velocity.KilometerPerHour; break; }
                    case "Knots": { Styler.Velocity2 = Velocity.Knots; break; }
                    case "Mach(at std. atm)": { Styler.Velocity2 = Velocity.Mach; break; }
                    case "Meter per second": { Styler.Velocity2 = Velocity.MeterPerSecond; break; }
                    case "Miles per hour": { Styler.Velocity2 = Velocity.MilesPerHour; break; }
                    case "Cubic centimeter": { Styler.Volume2 = Volume.CubicCentimeter; break; }
                    case "Cubic feet": { Styler.Volume2 = Volume.CubicFeet; break; }
                    case "Cubic inch": { Styler.Volume2 = Volume.CubicInch; break; }
                    case "Cubic meter": { Styler.Volume2 = Volume.CubicMeter; break; }
                    case "Cubic yard": { Styler.Volume2 = Volume.CubicYard; break; }
                    case "Fluid ounce (UK)": { Styler.Volume2 = Volume.FluidOunceUK; break; }
                    case "Fluid ounce (US)": { Styler.Volume2 = Volume.FluidounceUS; break; }
                    case "Gallon (UK)": { Styler.Volume2 = Volume.GallonUK; break; }
                    case "Gallon (US)": { Styler.Volume2 = Volume.GallonUS; break; }
                    case "Liter": { Styler.Volume2 = Volume.Liter; break; }
                    case "Pint (UK)": { Styler.Volume2 = Volume.PintUK; break; }
                    case "Pint (US)": { Styler.Volume2 = Volume.PintUS; break; }
                    case "Quart (UK)": { Styler.Volume2 = Volume.QuartUK; break; }
                    case "Quart (US)": { Styler.Volume2 = Volume.QuartUS; break; }
                    case "Carat": { Styler.Weight2 = Weight.Carat; break; }
                    case "Centigram": { Styler.Weight2 = Weight.CentiGram; break; }
                    case "Decigram": { Styler.Weight2 = Weight.DeciGram; break; }
                    case "Dekagram": { Styler.Weight2 = Weight.DekaGram; break; }
                    case "Gram": { Styler.Weight2 = Weight.Gram; break; }
                    case "Hectogram": { Styler.Weight2 = Weight.HectoGram; break; }
                    case "Kilogram": { Styler.Weight2 = Weight.KiloGram; break; }
                    case "Long ton": { Styler.Weight2 = Weight.LongTon; break; }
                    case "Milligram": { Styler.Weight2 = Weight.MilliGram; break; }
                    case "Ounce": { Styler.Weight2 = Weight.Ounce; break; }
                    case "Pound": { Styler.Weight2 = Weight.Pound; break; }
                    case "Short ton": { Styler.Weight2 = Weight.ShortTon; break; }
                    case "Stone": { Styler.Weight2 = Weight.Stone; break; }
                    case "Tonne": { Styler.Weight2 = Weight.Tonne; break; }
                }
            GetResult();
        }

        private void GetResult()
        {
            if (!string.IsNullOrWhiteSpace(ConvertText.Text))
            {
                if (double.TryParse(ConvertText.Text, out tryParse))
                {
                    switch (Styler.Converter)
                    {
                        case Converter.Angle:
                            {
                                Styler.Result = Styler.Math.AngleToAngle(Styler.Angle1, Styler.Angle2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Area:
                            {
                                Styler.Result = Styler.Math.AreaToArea(Styler.Area1, Styler.Area2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Energy:
                            {
                                Styler.Result = Styler.Math.EnergyToEnergy(Styler.Energy1, Styler.Energy2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Length:
                            {
                                Styler.Result = Styler.Math.LengthToLength(Styler.Length1, Styler.Length2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Power:
                            {
                                Styler.Result = Styler.Math.PowerToPower(Styler.Power1, Styler.Power2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Pressure:
                            {
                                Styler.Result = Styler.Math.PressureToPressure(Styler.Pressure1, Styler.Pressure2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Temperature:
                            {
                                Styler.Result = Styler.Math.TemperatureToTemperature(Styler.Temperature1, Styler.Temperature2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Time:
                            {
                                Styler.Result = Styler.Math.TimeToTime(Styler.Time1, Styler.Time2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Velocity:
                            {
                                Styler.Result = Styler.Math.VelocityToVelocity(Styler.Velocity1, Styler.Velocity2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Volume:
                            {
                                Styler.Result = Styler.Math.VolumeToVolume(Styler.Volume1, Styler.Volume2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Weight:
                            {
                                Styler.Result = Styler.Math.WeightToWeight(Styler.Weight1, Styler.Weight2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                    }
                }
                try
                {
                    if (Styler.Converter == Converter.Base && !string.IsNullOrEmpty(ConvertText.Text))
                    {
                        Styler.Result = Styler.Math.BaseToBase(Styler.Base1, Styler.Base2, ConvertText.Text).ToString();
                    }
                }
                catch (Exception ex)
                {
                    Styler.MessageBox(ex.Message);

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ConvertText.Text))
                {
                    Styler.MessageBox("Invalid Input");
                }
                Styler.Result = "0";
            }
        }

        private void ConvertText_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetResult();
        }

        private void InputConst_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConstListItems();
        }

        private void ConstListItems()
        {
            if (!string.IsNullOrWhiteSpace(InputConst.Text))
            {
                if (double.TryParse(InputConst.Text, out tryParse) && !InputConst.Text.Contains(","))
                {
                    ListValue.Text = (System.Convert.ToDouble(InputConst.Text) * Styler.Math.Values(ConstantListBox.SelectedIndex)).ToString();
                    UnitValue.Text = Styler.Math.Units(ConstantListBox.SelectedIndex);
                }
                else
                {
                    Styler.MessageBox("Invalid Input");
                }
            }
        }

        private void ConstantListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConstListItems();
        }

        private void InputConst_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CompOrInt(false, e);
        }

        private void CompOrInt(bool comp, KeyRoutedEventArgs e)
        {
            if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Shift) != CoreVirtualKeyStates.Down && Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.LeftShift) != CoreVirtualKeyStates.Down && Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.RightShift) != CoreVirtualKeyStates.Down)
            {
                if (!comp)
                {
                    if (e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9 || e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.NumberPad9 || e.Key == VirtualKey.Decimal || e.Key.ToString() == "190" || e.Key == VirtualKey.Tab)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (comp)
                {
                    if (e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9 || e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.NumberPad9 || e.Key == VirtualKey.Decimal || e.Key.ToString() == "189" || e.Key.ToString() == "190" || e.Key == VirtualKey.I || e.Key == VirtualKey.Tab || e.Key.ToString() == "191")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                if (!comp)
                {
                    if (e.Key == VirtualKey.Left || e.Key == VirtualKey.Right || e.Key == VirtualKey.Up || e.Key == VirtualKey.Down || e.Key == VirtualKey.Tab)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (comp)
                {
                    if (e.Key == VirtualKey.Tab || e.Key.ToString() == "187" || e.Key == VirtualKey.Number8 || e.Key == VirtualKey.Number9 || e.Key == VirtualKey.Number0)
                    {
                        e.Handled = false;
                    }
                    else if (e.Key == VirtualKey.I)
                    {
                        try
                        {
                            _key = ((TextBox)e.OriginalSource).Text;
                            start = ((TextBox)e.OriginalSource).SelectionStart;
                            ((TextBox)e.OriginalSource).Text = _key.Substring(0, start) + "∠" + _key.Substring(start);
                            ((TextBox)e.OriginalSource).SelectionStart = start + 1;
                        }
                        catch (Exception) { }
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
        }

        private void APResult_Click(object sender, RoutedEventArgs e)
        {
            SerialCall(ApOrGporHp.SelectedIndex);
        }

        private void SerialCall(int a)
        {
            switch (a)
            {
                case 0:
                    {
                        GetAp();
                        break;
                    }
                case 1:
                    {
                        GetGp();
                        break;
                    }
                case 2:
                    {
                        GetHp();
                        break;
                    }
            }
        }

        private void GetAp()
        {
            try
            {
                if (double.TryParse(AP1.Text, out tryParse) && double.TryParse(AP2.Text, out tryParse) && double.TryParse(AP3.Text, out tryParse))
                {
                    if (APSelection.SelectedIndex == 0)
                    {
                        OutAP.Text = Styler.Math.APSum(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                    else if (APSelection.SelectedIndex == 1)
                    {
                        OutAP.Text = Styler.Math.APDiff(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { Styler.MessageBox(ex.Message); }
        }

        private void GetGp()
        {
            try
            {
                if (double.TryParse(AP1.Text, out tryParse) && double.TryParse(AP2.Text, out tryParse) && double.TryParse(AP3.Text, out tryParse))
                {
                    if (APSelection.SelectedIndex == 0)
                    {
                        OutAP.Text = Styler.Math.GPSum(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                    else if (APSelection.SelectedIndex == 1)
                    {
                        OutAP.Text = Styler.Math.GPDiff(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { Styler.MessageBox(ex.Message); }
        }

        private void GetHp()
        {
            try
            {
                if (double.TryParse(AP1.Text, out tryParse) && double.TryParse(AP2.Text, out tryParse) && double.TryParse(AP3.Text, out tryParse))
                {
                    if (APSelection.SelectedIndex == 0)
                    {
                        OutAP.Text = Styler.Math.HPSum(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                    else if (APSelection.SelectedIndex == 1)
                    {
                        OutAP.Text = Styler.Math.HPDiff(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { Styler.MessageBox(ex.Message); }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                Styler.IsVisible = false;
            }
            else if ((sender as ComboBox).SelectedIndex == 1)
            {
                Styler.IsVisible = true;
            }
        }

        private void TwoEqu()
        {
            bool Call = true;
            try
            {
                List<String> tempList = new List<string>() { AT1.Text, AT2.Text, AT0.Text, BT1.Text, BT2.Text, BT0.Text };
                GetItems(tempList);
                foreach (Complex item in Styler.Math.TwoEquation(_comp[0], _comp[1], _comp[2], _comp[3], _comp[4], _comp[5]))
                {
                    if (Call)
                    {
                        OutTX.Text = item.ToString();
                        Call = false;
                    }
                    else
                    {
                        OutTY.Text = item.ToString();
                    }
                }
            }
            catch (Exception ex) { Styler.MessageBox(ex.Message); }
        }

        private void ThreeEqu()
        {
            try
            {
                List<String> tempList = new List<string>() { AT1.Text, AT2.Text, AT3.Text, AT0.Text, BT1.Text, BT2.Text, BT3.Text, BT0.Text, CT1.Text, CT2.Text, CT3.Text, CT0.Text };
                GetItems(tempList);
                Complex[] res = Styler.Math.Threeequation(new Complex[] { _comp[0], _comp[1], _comp[2], _comp[3] }, new Complex[] { _comp[4], _comp[5], _comp[6], _comp[7] }, new Complex[] { _comp[8], _comp[9], _comp[10], _comp[11] });
                OutTX.Text = res[0].ToString();
                OutTY.Text = res[1].ToString();
                OutTZ.Text = res[2].ToString();
            }
            catch (Exception ex) { Styler.MessageBox(ex.Message); }
        }

        private object Run(object value)
        {
            if (value.ToString().Contains("i") || value.ToString().Contains('∠'))
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
                                value = new Complex(0, System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("i"))));
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
                            value = new Complex(Styler.Math.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Styler.Solve)[0], Styler.Math.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Styler.Solve)[1]);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid Input");
                }
            }
            if (value is Complex)
            {
                return (Complex)value;
            }
            if (value is double || double.TryParse(value.ToString(), out tryParse))
            {
                if (!tryParse.ToString().Contains(','))
                {
                    return tryParse;
                }
                return value.ToString();
            }
            if (value is string)
            {
                return value.ToString();
            }
            return null;
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

        private List<Complex> GetItems(List<string> listItems)
        {
            _comp.Clear();
            foreach (string item in listItems)
            {
                if (!string.IsNullOrWhiteSpace(item) && item.Length > 0)
                {
                    inputItemText = item;
                    _items.Clear();
                    inputItemText = Regex.Replace(inputItemText, "\\+", " + ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\-", " - ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\*", " * ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\/", " / ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\(", " ( ", RegexOptions.Singleline);
                    inputItemText = Regex.Replace(inputItemText, "\\)", " ) ", RegexOptions.Singleline);
                    foreach (string item1 in inputItemText.Split(' '))
                    {
                        _items.Add(Run(item1));
                    }
                    var val = Maths(_items)[0];
                    if (val is Complex)
                    {
                        _comp.Add((Complex)val);
                    }
                    else if (val is double)
                    {
                        _comp.Add(new Complex(System.Convert.ToDouble(val), System.Convert.ToDouble(0)));
                    }
                }
                else
                {
                    throw new InvalidOperationException("Inalid Operation");
                }
            }
            return _comp;
        }

        private void TwoEquationSolve_Click(object sender, RoutedEventArgs e)
        {
            if (PolEqu.SelectedIndex == 0)
            {
                TwoEqu();
            }
            else if (PolEqu.SelectedIndex == 1)
            {
                ThreeEqu();
            }
        }

        private void AT1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CompOrInt(true, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Selected(string value, int length)
        {
            if (start > QBox.Text.Length)
            {
                start = QBox.Text.Length;
                QBox.SelectionStart = start;
            }
            try
            {
                string changed = null;
                qStart = QBox.SelectionStart;
                changed = QBox.Text;
                start = QBox.SelectionStart + length;
                QBox.Text = changed.Substring(0, qStart) + value + Styler.Temp + changed.Substring(qStart);
                qStart = qStart + length;
                QBox.SelectionStart = qStart;
                QBox.Focus(FocusState.Keyboard);
            }
            catch (Exception) { }
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "²";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "³";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Power_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = ", ) ";
            Styler.Content = " Pow( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void SQRT_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " √( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void CBRT_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " ³√( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void NRT_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = ", ) ";
            Styler.Content = " √( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void LOG_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = ",10 ) ";
            Styler.Content = " Log( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void LN_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Ln( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void InvLog_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Pow( 10,";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void InvLn_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Pow( e,";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void LCM_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " LCM( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void HCF_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " HCF( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = ", ) ";
            Styler.Content = " Modulus( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void PI_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "π";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Prime_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Prime( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void ListOfPrime_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " ListOfPrime( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Inv_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Inv( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Abs_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Abs( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Permutation_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "P";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Combination_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "Co";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Fact_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "!";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void A_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "A";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "B";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "C";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void D_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "D";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void E_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "E";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void F_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "F";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Add( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Sub( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Mul_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Mul( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = ",";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Pol_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Pol( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Rect_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = " ) ";
            Styler.Content = " Rect( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Ang_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "∠";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Img_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "i";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Matrix_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " Matrix ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Hyperbola_Click(object sender, RoutedEventArgs e)
        {
            if (_isHyp)
            {
                Sin.Content = "Sinh";
                Cos.Content = "Cosh";
                Tan.Content = "Tanh";
                ASin.Content = "ASinh";
                ACos.Content = "ACosh";
                ATan.Content = "ATanh";
                Hyperbola.Background = new SolidColorBrush(Colors.Gray);
                _isHyp = false;
            }
            else if (!_isHyp)
            {
                Sin.Content = "Sin";
                Cos.Content = "Cos";
                Tan.Content = "Tan";
                ASin.Content = "ASin";
                ACos.Content = "ACos";
                ATan.Content = "ATan";
                Hyperbola.Background = color;
                _isHyp = true;
            }
        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            if (Sin.Content.ToString() == "Sin")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Sin( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (Sin.Content.ToString() == "Sinh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Sinh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            if (Cos.Content.ToString() == "Cos")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Cos( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (Cos.Content.ToString() == "Cosh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Cosh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            if (Tan.Content.ToString() == "Tan")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Tan( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (Tan.Content.ToString() == "Tanh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " Tanh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void ASin_Click(object sender, RoutedEventArgs e)
        {
            if (ASin.Content.ToString() == "ASin")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ASin( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (ASin.Content.ToString() == "ASinh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ASinh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void ACos_Click(object sender, RoutedEventArgs e)
        {
            if (ACos.Content.ToString() == "ACos")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ACos( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (ACos.Content.ToString() == "ACosh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ACosh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void ATan_Click(object sender, RoutedEventArgs e)
        {
            if (ATan.Content.ToString() == "ATan")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ATan( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
            else if (ATan.Content.ToString() == "ATanh")
            {
                Styler.Temp = " ) ";
                Styler.Content = " ATanh( ";
                Selected(Styler.Content, Styler.Content.Length);
            }
        }

        private void OpenBrace_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " ( ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void CloseBrace_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " ) ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Deg_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "°";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Clr_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            QBox.Text = "";
            QBox.Focus(FocusState.Keyboard);
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "1";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "2";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "3";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "4";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "5";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Mega_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "M";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Giga_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "G";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Tera_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "T";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Micro_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "μ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Subtract_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " - ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Adder_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " + ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = ".";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void ListVariables_Click(object sender, RoutedEventArgs e)
        {
            Styler.IsHome = false;
            Styler.IsList = true;
            Styler.IsPopUp = false;
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "6";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "7";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "8";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "9";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "0";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Milli_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "m";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Kilo_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "k";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Femto_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "f";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Pico_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "p";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Nano_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = "n";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Multi_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " * ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Div_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " / ";
            Selected(Styler.Content, Styler.Content.Length);
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

        private void SumOfVar_Click(object sender, RoutedEventArgs e)
        {
            Stat("Sum");
        }

        private void SumOfSquareOfVar_Click(object sender, RoutedEventArgs e)
        {
            Stat("SumOfSquare");
        }

        private void PolyAng_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(QBox.Text) && int.TryParse(QBox.Text, out _count))
            {
                Styler.MainResult = ((System.Convert.ToInt32(QBox.Text) - 2) * 180 / (System.Convert.ToInt32(QBox.Text))).ToString();
            }
            else
            {
                Styler.MainResult = null;
                Styler.MessageBox("Input Number of Sides like 1 or 2 or 3....");
            }
            if (Styler.MainResult != null)
            {
                Styler.QList.Add(QBox.Text);
                Styler.AList.Add(Styler.MainResult);
                _need = false;
                while (Styler.QList.Count > 10)
                {
                    Styler.QList.RemoveAt(1);
                    Styler.AList.RemoveAt(1);
                }
            }
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            if (Styler.MainResult != null)
            {
                Styler.QList.Add(QBox.Text);
                Styler.AList.Add(Styler.MainResult);
                _need = false;
                while (Styler.QList.Count > 10)
                {
                    Styler.QList.RemoveAt(1);
                    Styler.AList.RemoveAt(1);
                }
            }
        }

        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            PercentExe();
        }

        private void ComplexRect_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            try
            {
                if (Styler.MainResult.Contains("∠"))
                {
                    double first = Styler.Math.PolarToRectangular(System.Convert.ToDouble(Styler.MainResult.Substring(0, Styler.MainResult.IndexOf("∠"))), System.Convert.ToDouble(Styler.MainResult.Substring(Styler.MainResult.IndexOf("∠") + 1)), Solver.Mode)[0];
                    double second = Styler.Math.PolarToRectangular(System.Convert.ToDouble(Styler.MainResult.Substring(0, Styler.MainResult.IndexOf("∠"))), System.Convert.ToDouble(Styler.MainResult.Substring(Styler.MainResult.IndexOf("∠") + 1)), Solver.Mode)[1];
                    Styler.MainResult = first + ((second.ToString().Contains("-")) ? "-i" : "+i") + ((second.ToString().Contains("-")) ? second.ToString().Replace("-", "") : second.ToString());
                }
            }
            catch (Exception) { }
        }

        private void ComplexPol_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            try
            {
                if (Styler.MainResult.Contains("i"))
                {
                    double first = Styler.Math.RectangularToPolar(System.Convert.ToDouble(Styler.MainResult.Substring(0, Styler.MainResult.IndexOf("i") - 1)), System.Convert.ToDouble(Styler.MainResult.Substring(Styler.MainResult.IndexOf("i") + 1)), Solver.Mode)[0];
                    double second = ((Styler.Math.PolarToRectangular(System.Convert.ToDouble(Styler.MainResult.Substring(0, Styler.MainResult.IndexOf("i") - 1)), System.Convert.ToDouble(Styler.MainResult.Substring(Styler.MainResult.IndexOf("i") + 1)), Solver.Mode)[1]));
                    Styler.MainResult = first + "∠" + second;
                }
            }
            catch (Exception) { }
        }

        private void AND_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " & ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void OR_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " | ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void XOR_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " ^ ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void NOT_Click(object sender, RoutedEventArgs e)
        {
            Styler.Temp = null;
            Styler.Content = " ~ ";
            Selected(Styler.Content, Styler.Content.Length);
        }

        private void Fractional_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            if (Styler.MainResult != null)
            {
                if (Styler.MainResult != "[Collections to view Expand]" && !Styler.MainResult.Contains("i") && !Styler.MainResult.Contains("+") && !Styler.MainResult.Contains("∠"))
                {
                    if (double.TryParse(Styler.MainResult, out tryParse))
                    {
                        string mixVsFrac = Styler.Math.MixVsFraction(tryParse);
                        string mix = mixVsFrac;
                        if (mixVsFrac.Contains("(") && mixVsFrac.Contains(")"))
                        {
                            mix = (System.Convert.ToDouble(mixVsFrac.Substring(0, mixVsFrac.IndexOf("("))) * System.Convert.ToDouble(mixVsFrac.Substring(mixVsFrac.IndexOf("/") + 1, mixVsFrac.Length - mixVsFrac.IndexOf(")"))) + System.Convert.ToDouble(mixVsFrac.Substring(mixVsFrac.IndexOf("(") + 1, mixVsFrac.IndexOf("/") - (mixVsFrac.IndexOf("(") + 1)))).ToString() + "/" + mixVsFrac.Substring(mixVsFrac.IndexOf("/") + 1, mixVsFrac.Length - mixVsFrac.IndexOf(")"));
                        }
                        Styler.MainResult = mix;
                    }
                }
            }
        }

        private void Mixed_Click(object sender, RoutedEventArgs e)
        {
            EqualExe();
            if (Styler.MainResult != null)
            {
                if (Styler.MainResult != "[Collections to view Expand]" && !Styler.MainResult.Contains("i") && !Styler.MainResult.Contains("+") && !Styler.MainResult.Contains("∠"))
                {
                    if (double.TryParse(Styler.MainResult, out tryParse))
                    {
                        Styler.MainResult = Styler.Math.MixVsFraction(tryParse);
                    }
                }
            }
        }

        private void Call()
        {
            Styler.Mode = MatOrStat.SelectedIndex;
            Styler.Item = ListIndex.SelectedIndex;
            Styler.IsEnable = false;
            try
            {
                Styler.M = System.Convert.ToInt32(m.Text);
                Styler.N = System.Convert.ToInt32(n.Text);
            }
            catch (Exception)
            {
                Styler.S = System.Convert.ToInt32(Stats.Text);
            }
            GetFields.Children.Clear();
            if (Styler.IsStat)
            {
                if (int.TryParse(Stats.Text, out _count))
                {
                    if (System.Convert.ToInt32(Stats.Text) > 0)
                    {
                        MakeStats(System.Convert.ToInt32(Stats.Text), Styler.Lists[ListIndex.SelectedIndex]);
                        Styler.IsEnable = true;
                    }
                }
            }
            else if (Styler.IsMat)
            {
                if (int.TryParse(m.Text, out _count) && int.TryParse(n.Text, out _count))
                {
                    if (System.Convert.ToInt32(m.Text) > 0 && System.Convert.ToInt32(n.Text) > 0)
                    {
                        MakeMat(System.Convert.ToInt32(m.Text), System.Convert.ToInt32(n.Text), Styler.MatList[ListIndex.SelectedIndex]);
                        Styler.IsEnable = true;
                    }
                }
            }
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Call();
        }

        private void MakeStats(int size, List<double> item)
        {
            GetFields.RowDefinitions.Clear();
            GetFields.Children.Clear();
            RowDefinition rowDefinition1 = new RowDefinition() { Height = GridLength.Auto };
            StackPanel stackPanel = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(5, 10, 5, 10) };
            GetFields.RowDefinitions.Add(rowDefinition1);
            GetFields.Children.Add(stackPanel);
            _count = 0;
            if (Styler.Lists[ListIndex.SelectedIndex].Count > 0 && Styler.Lists[ListIndex.SelectedIndex].Count == size)
            {
                while (_count < size)
                {
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 5, 10), Height = 25, Name = "TextBox" + _count, };
                    textBox.TextChanged += textBox_TextChanged;
                    textBox.KeyDown += textBox_KeyDown;
                    try
                    {
                        textBox.Text = System.Convert.ToString(item[_count]);
                    }
                    catch (Exception) { }
                    stackPanel.Children.Add(textBox);
                    _count++;
                }
            }
            else
            {
                while (_count < size)
                {
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 5, 10), Height = 25, Name = "TextBox" + _count };
                    textBox.TextChanged += textBox_TextChanged;
                    textBox.KeyDown += textBox_KeyDown;
                    try
                    {
                        textBox.Text = "0";
                    }
                    catch (Exception) { }
                    stackPanel.Children.Add(textBox);
                    _count++;
                }
            }
        }

        void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CompOrInt(false, e);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Styler.IsEnable = true;
            foreach (var item in ((((sender as TextBox).Parent) as StackPanel).Children))
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text))
                    {
                        Styler.IsEnable = false;
                        break;
                    }
                }
            }
        }

        private void MakeMat(int _m, int _n, double[,] item)
        {
            int _row = 0;
            int _col = 0;
            GetFields.Children.Clear();
            GetFields.RowDefinitions.Clear();
            RowDefinition rowDefinition1 = new RowDefinition() { Height = GridLength.Auto };
            StackPanel stackPanel = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(5, 10, 5, 10) };
            GetFields.RowDefinitions.Add(rowDefinition1);
            GetFields.Children.Add(stackPanel);
            if (Styler.MatList[ListIndex.SelectedIndex].GetLength(0) > 0 && Styler.MatList[ListIndex.SelectedIndex].GetLength(1) > 0 && Styler.MatList[ListIndex.SelectedIndex].GetLength(0) == _m && Styler.MatList[ListIndex.SelectedIndex].GetLength(1) == _n)
            {
                while (true)
                {
                    Grid grid = new Grid();
                    TextBlock textBlockContent = new TextBlock() { Text = "a" + (_row + 1) + (_col + 1), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(10, 10, 5, 10) };
                    TextBox textBox = new TextBox() { Margin = new Thickness(5, 10, 10, 10), Height = 25 };
                    textBox.TextChanged += new TextChangedEventHandler(MatTextChanged);
                    textBox.KeyDown += math_KeyDown;
                    Grid.SetColumn(textBlockContent, 0);
                    Grid.SetColumn(textBox, 1);
                    try
                    {
                        textBox.Text = System.Convert.ToString(item[_row, _col]);
                    }
                    catch (Exception) { }
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Add(textBlockContent);
                    grid.Children.Add(textBox);
                    stackPanel.Children.Add(grid);
                    if (_col == _n - 1)
                    {
                        _col = -1;
                        if (_row == _m - 1)
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
                    textBox.KeyDown += math_KeyDown;
                    Grid.SetColumn(textBlockContent, 0);
                    Grid.SetColumn(textBox, 1);
                    try
                    {
                        textBox.Text = "0";
                    }
                    catch (Exception) { }
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    grid.Children.Add(textBlockContent);
                    grid.Children.Add(textBox);
                    stackPanel.Children.Add(grid);
                    if (_col == _n - 1)
                    {
                        _col = -1;
                        if (_row == _m - 1)
                        {
                            break;
                        }
                        _row++;
                    }
                    _col++;
                }
            }
        }

        void math_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CompOrInt(false, e);
        }

        private void MatTextChanged(object sender, TextChangedEventArgs e)
        {
            Styler.IsEnable = true;
            foreach (var item in ((((sender as TextBox).Parent) as Grid).Children))
            {
                if (item is TextBox)
                {
                    if (string.IsNullOrEmpty((item as TextBox).Text))
                    {
                        Styler.IsEnable = false;
                        break;
                    }
                }
            }
        }

        private void m_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.Shift) != CoreVirtualKeyStates.Down && Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.LeftShift) != CoreVirtualKeyStates.Down && Window.Current.CoreWindow.GetAsyncKeyState(VirtualKey.RightShift) != CoreVirtualKeyStates.Down)
            {
                if (e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9 || e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.Number9 || e.Key == VirtualKey.Tab)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (e.Key == VirtualKey.Tab)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void MatOrStat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (MatOrStat.SelectedIndex == 0)
                {
                    Styler.IsMat = true;
                    Styler.IsStat = false;
                    Revert(0);
                }
                else if (MatOrStat.SelectedIndex == 1)
                {
                    Styler.IsStat = true;
                    Styler.IsMat = false;
                    Revert(1);
                }
            }
            catch (Exception) { }
        }

        private void Revert(int indexers)
        {
            if (indexers == 0)
            {
                m.Text = Styler.MatList[ListIndex.SelectedIndex].GetLength(0).ToString();
                n.Text = Styler.MatList[ListIndex.SelectedIndex].GetLength(1).ToString();
                Call();
            }
            else if (indexers == 1)
            {
                Stats.Text = Styler.Lists[ListIndex.SelectedIndex].Count.ToString();
                Call();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Styler.IsHome = true;
            Styler.IsList = false;
            Styler.IsPopUp = false;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (Styler.Mode == 0)
            {
                double[,] mItem = new double[Styler.M, Styler.N];
                int row = 0, col = 0;
                foreach (var item in ((GetFields.Children[0] as StackPanel).Children))
                {
                    foreach (var item1 in (item as Grid).Children)
                    {
                        if (item1 is TextBox)
                        {
                            mItem[row, col] = System.Convert.ToDouble((item1 as TextBox).Text);
                            if (col == mItem.GetLength(1) - 1)
                            {
                                col = -1;
                                if (row == mItem.GetLength(0) - 1)
                                {
                                    break;
                                }
                                row++;
                            }
                            col++;
                        }
                    }
                }
                Styler.MatList[Styler.Item] = mItem;
                Styler.MessageBox("Matrix added successfully");
            }
            else if (Styler.Mode == 1)
            {
                List<double> sItem = new List<double>();
                foreach (var item in (GetFields.Children[0] as StackPanel).Children)
                {
                    if (item is TextBox)
                    {
                        sItem.Add(System.Convert.ToDouble((item as TextBox).Text));
                    }
                }
                Styler.Lists[Styler.Item] = sItem;
                Styler.MessageBox("List added successfully");
            }
        }

        private void ListIndex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (MatOrStat.SelectedIndex == 0)
                {
                    Revert(0);
                }
                else if (MatOrStat.SelectedIndex == 1)
                {
                    Revert(1);
                }
            }
            catch (Exception) { }
        }

        private void ABox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ABox.Text == "[Collections to view Expand]")
            {
                Styler.IsHome = false;
                Styler.IsList = false;
                Styler.PopUps(Solver.Collection, Grid1);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Styler.Deg)
                {
                    Solver.Mode = Mode.Degree;
                }
                else if (Styler.Rad)
                {
                    Solver.Mode = Mode.Radians;
                }
                else if (Styler.Grad)
                {
                    Solver.Mode = Mode.Gradient;
                }
            }
            catch (Exception) { }
        }

        private void QBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Back)
            {
                Remove();
                start = QBox.SelectionStart;
            }
            e.Handled = true;
        }

        private bool NextStep()
        {
            if (start < QBox.Text.Length)
            {
                if (QBox.Text.Substring(start, 1) == " ")
                {
                    while (start < QBox.Text.Length)
                    {
                        start++;
                        if (QBox.Text.Substring(start, 1) == " ")
                        {
                            break;
                        }
                    }
                }
            }
            QBox.SelectionStart = start + 1;
            QBox.SelectionLength = 0;
            QBox.Select(start + 1, 0);
            return false;
        }

        private bool BackStep()
        {
            if (start > QBox.Text.Length)
            {
                start = QBox.Text.Length;
            }
            try
            {
                if (start > 0)
                {
                    if (QBox.Text.Substring(start - 1, 1) == " ")
                    {
                        start--;
                        while (start > 0)
                        {
                            start--;
                            if (QBox.Text.Substring(start - 1, 1) == " ")
                            {
                                break;
                            }
                        }
                    }
                }
                QBox.SelectionStart = start - 1;
                QBox.SelectionLength = 0;
                QBox.Select(start - 1, 0);
            }
            catch (Exception) { }
            return false;
        }

        void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (QBox.FocusState == FocusState.Keyboard)
            {
                try
                {
                    if (args.VirtualKey == VirtualKey.Left)
                    {
                        BackStep();
                        start = QBox.SelectionStart;
                        args.Handled = true;
                    }
                    else if (args.VirtualKey == VirtualKey.Right)
                    {
                        NextStep();
                        start = QBox.SelectionStart;
                        args.Handled = true;
                    }
                    else if (args.VirtualKey == VirtualKey.Up)
                    {
                        Up();
                    }
                    else if (args.VirtualKey == VirtualKey.Down)
                    {
                        Down();
                    }
                    else if (args.VirtualKey == VirtualKey.Delete)
                    {
                        QBox.Text = Styler.Reverse;
                        QBox.SelectionStart = start;
                        args.Handled = true;
                    }
                    else if (args.VirtualKey == VirtualKey.Escape)
                    {
                        QBox.Text = "";
                    }
                }
                catch (Exception) { if (start > 0) { start = QBox.SelectionStart + 1; QBox.SelectionStart = start; } }
            }
        }

        private void Remove()
        {
            try
            {
                string changed = Styler.Reverse;
                if (changed.Substring(start - 1, 1) != " ")
                {
                    QBox.Text = changed.Substring(0, start - 1) + changed.Substring(start);
                    QBox.SelectionStart = start - 1;
                }
                else
                {
                    QBox.Text = changed.Substring(0, changed.Substring(0, start - 1).LastIndexOf(" ")) + changed.Substring(start);
                    QBox.SelectionStart = changed.Substring(0, start - 1).LastIndexOf(" ");
                }
            }
            catch (Exception) { }
            Styler.Reverse = QBox.Text;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            Up();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Down();
        }

        private void Down()
        {
            if (_need)
            {
                if (!Styler.QList.Contains(QBox.Text))
                {
                    Styler.QList.RemoveAt(0);
                    Styler.AList.RemoveAt(0);
                    Styler.QList.Insert(0, QBox.Text);
                    Styler.AList.Insert(0, null);
                    while (Styler.QList.Count > 10)
                    {
                        Styler.QList.RemoveAt(1);
                        Styler.AList.RemoveAt(1);
                    }
                }
            }
            _indexList = ((_indexList + 1 < Styler.QList.Count) ? _indexList + 1 : 0);
            HistoryResult(_indexList);
        }

        private void Up()
        {
            if (_need)
            {
                if (!Styler.QList.Contains(QBox.Text))
                {
                    Styler.QList.RemoveAt(0);
                    Styler.AList.RemoveAt(0);
                    Styler.QList.Insert(0, QBox.Text);
                    Styler.AList.Insert(0, null);
                    while (Styler.QList.Count > 10)
                    {
                        Styler.QList.RemoveAt(1);
                        Styler.AList.RemoveAt(1);
                    }
                }
            }
            _indexList = ((_indexList - 1 >= 0) ? _indexList - 1 : Styler.QList.Count - 1);
            HistoryResult(_indexList);
        }

        private void HistoryResult(int index)
        {
            QBox.Text = Styler.QList[index];
            Styler.MainResult = Styler.AList[index];
            Styler.IsVisible = false;
            QBox.SelectionStart = 0;
        }

        private void ViewOnDesk_Click(object sender, RoutedEventArgs e)
        {
            Browse();
        }

        private async void Browse()
        {
            try
            {
                FolderPicker folderPicker = new FolderPicker();
                folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                folderPicker.FileTypeFilter.Add(".exe");
                StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                try
                {
                    StorageFile cc = await folder.GetFileAsync("CloudCalculator.exe");
                    StorageFile h = await folder.GetFileAsync("Help.pdf");
                    await cc.DeleteAsync(StorageDeleteOption.PermanentDelete);
                    await h.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
                catch (Exception) { }
                IStorageFile writeCC = await folder.CreateFileAsync("CloudCalculator.exe");
                IStorageFile writePdf = await folder.CreateFileAsync("Help.pdf");
                StorageFile readCC = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("CloudCalculator.exe");
                StorageFile readPdf = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Help.pdf");
                await readCC.CopyAndReplaceAsync(writeCC);
                await readPdf.CopyAndReplaceAsync(writePdf);
                Styler.MessageBox("Import Completed");
            }
            catch (Exception ex)
            {
                Styler.MessageBox(ex.Message);
            }
        }

        #endregion

    }
}
