using MiniPro.Models;
using MiniPro.Solutions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Others.xaml
    /// </summary>
    public partial class Others : Window, INotifyPropertyChanged
    {

        #region private variable

        private object _focus = new object();

        private int _start = 0;

        private Mode _solve = new Mode();

        private string _drager, inputItemText;

        MathFunctions _mathFunctions = new MathFunctions();

        private List<object> _items = new List<object>();

        private ObservableCollection<string> _angle = new ObservableCollection<string>() { "Degree", "Gradian", "Radian" };

        private ObservableCollection<string> _area = new ObservableCollection<string>() { "Acres", "Hectares", "Square centimeter", "Square feet", "Square inch", "Square kilometer", "Square meter", "Square mile", "Square millimeter", "Square yard" };

        private ObservableCollection<string> _base = new ObservableCollection<string>() { "Binary", "Decimal", "Hexa Decimal", "Octal" };

        private ObservableCollection<string> _energy = new ObservableCollection<string>() { "British Thermal Unit", "Calorie", "Electron-volts", "Foot-pound", "Joule", "Kilocalorie", "Kilojoule" };

        private ObservableCollection<string> _length = new ObservableCollection<string>() { "Angstorm", "Centimeter", "Chain", "Fathom", "Feet", "Hand", "Inch", "Kilometer", "Link", "Meter", "Microns", "Mile", "Millimeter", "Nanometer", "Nautical Mile", "PICA", "Rods", "Span", "Yard" };

        private ObservableCollection<string> _power = new ObservableCollection<string>() { "BTU/minute", "Foot-Pound/minute", "Horsepower", "Kilowatt", "Watt" };

        private ObservableCollection<string> _pressure = new ObservableCollection<string>() { "Atmosphere", "Bar", "Kilo Pascal", "Millimeter of mercury", "Pascal", "Pound per square inch(PSI)" };

        private ObservableCollection<string> _temperature = new ObservableCollection<string>() { "Degree Celsius", "Degree Fahrenheit", "Kelvin" };

        private ObservableCollection<string> _time = new ObservableCollection<string>() { "Day", "Hour", "Microsecond", "Millisecond", "Minute", "Second", "Week" };

        private ObservableCollection<string> _velocity = new ObservableCollection<string>() { "Centimeter per second", "Feet per second", "Kilometer per hour", "Knots", "Mach(at std. atm)", "Meter per second", "Miles per hour" };

        private ObservableCollection<string> _volume = new ObservableCollection<string>() { "Cubic centimeter", "Cubic feet", "Cubic inch", "Cubic meter", "Cubic yard", "Fluid ounce (UK)", "Fluid ounce (US)", "Gallon (UK)", "Gallon (US)", "Liter", "Pint (UK)", "Pint (US)", "Quart (UK)", "Quart (US)" };

        private ObservableCollection<string> _weight = new ObservableCollection<string>() { "Carat", "Centigram", "Decigram", "Dekagram", "Gram", "Hectogram", "Kilogram", "Long ton", "Milligram", "Ounce", "Pound", "Short ton", "Stone", "Tonne" };

        private Styler _styler = new Styler();

        private double tryParse = 0;

        private string _result;

        private Complex _temp1 = new Complex();

        private List<Complex> _comp = new List<Complex>();

        #endregion

        #region public properties

        public Object Focused
        {
            get
            {
                return _focus;
            }
            set
            {
                _focus = value;
                PropertyChangedNotifier("Focused");
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
                PropertyChangedNotifier("Drager");
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
                PropertyChangedNotifier("Styler");
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
                PropertyChangedNotifier("Solve");
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

        #endregion

        #region public constructor

        public Others()
        {
            InitializeComponent();
            this.DataContext = this;
            ConstantListBox.ItemsSource = _mathFunctions.GetList();
            ConstantListBox.SelectedIndex = 0;
        }

        public Others(Styler styler, Mode solve)
            : this()
        {
            Styler = styler;
            Solve = solve;
            Styler.ItemSources = new ObservableCollection<ObservableCollection<string>>() { _angle, _area, _base, _energy, _length, _power, _pressure, _temperature, _time, _velocity, _volume, _weight };
            Styler.ItemSource = _angle;
            from.SelectedIndex = 0;
            to.SelectedIndex = 0;
        }

        #endregion

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region private methods

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
                                Result = _mathFunctions.AngleToAngle(Styler.Angle1, Styler.Angle2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Area:
                            {
                                Result = _mathFunctions.AreaToArea(Styler.Area1, Styler.Area2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Energy:
                            {
                                Result = _mathFunctions.EnergyToEnergy(Styler.Energy1, Styler.Energy2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Length:
                            {
                                Result = _mathFunctions.LengthToLength(Styler.Length1, Styler.Length2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Power:
                            {
                                Result = _mathFunctions.PowerToPower(Styler.Power1, Styler.Power2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Pressure:
                            {
                                Result = _mathFunctions.PressureToPressure(Styler.Pressure1, Styler.Pressure2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Temperature:
                            {
                                Result = _mathFunctions.TemperatureToTemperature(Styler.Temperature1, Styler.Temperature2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Time:
                            {
                                Result = _mathFunctions.TimeToTime(Styler.Time1, Styler.Time2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Velocity:
                            {
                                Result = _mathFunctions.VelocityToVelocity(Styler.Velocity1, Styler.Velocity2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Volume:
                            {
                                Result = _mathFunctions.VolumeToVolume(Styler.Volume1, Styler.Volume2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                        case Converter.Weight:
                            {
                                Result = _mathFunctions.WeightToWeight(Styler.Weight1, Styler.Weight2, System.Convert.ToDouble(ConvertText.Text)).ToString();
                                break;
                            }
                    }
                }
                try
                {
                    if (Styler.Converter == Converter.Base && !string.IsNullOrEmpty(ConvertText.Text))
                    {
                        Result = _mathFunctions.BaseToBase(Styler.Base1, Styler.Base2, ConvertText.Text).ToString();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                if (!string.IsNullOrEmpty(ConvertText.Text))
                {
                    MessageBox.Show("Invalid Input");
                }
                Result = "0";
            }
        }

        private void PropertyChangedNotifier(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void ConvertText_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetResult();
        }

        private void APResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(AP1.Text, out tryParse) && double.TryParse(AP2.Text, out tryParse) && double.TryParse(AP3.Text, out tryParse))
                {
                    if (APSelection.SelectedIndex == 0)
                    {
                        OutAP.Text = _mathFunctions.APSum(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                    else if (APSelection.SelectedIndex == 1)
                    {
                        OutAP.Text = _mathFunctions.APDiff(System.Convert.ToDouble(AP1.Text), System.Convert.ToDouble(AP2.Text), System.Convert.ToDouble(AP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GPResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(GP1.Text, out tryParse) && double.TryParse(GP2.Text, out tryParse) && double.TryParse(GP3.Text, out tryParse))
                {
                    if (GPSelection.SelectedIndex == 0)
                    {
                        OutGP.Text = _mathFunctions.GPSum(System.Convert.ToDouble(GP1.Text), System.Convert.ToDouble(GP2.Text), System.Convert.ToDouble(GP3.Text)).ToString();
                    }
                    else if (GPSelection.SelectedIndex == 1)
                    {
                        OutGP.Text = _mathFunctions.GPDiff(System.Convert.ToDouble(GP1.Text), System.Convert.ToDouble(GP2.Text), System.Convert.ToDouble(GP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void HPResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse(HP1.Text, out tryParse) && double.TryParse(HP2.Text, out tryParse) && double.TryParse(HP3.Text, out tryParse))
                {
                    if (HPSelect.SelectedIndex == 0)
                    {
                        OutHP.Text = _mathFunctions.HPSum(System.Convert.ToDouble(HP1.Text), System.Convert.ToDouble(HP2.Text), System.Convert.ToDouble(HP3.Text)).ToString();
                    }
                    else if (HPSelect.SelectedIndex == 1)
                    {
                        OutHP.Text = _mathFunctions.HPDiff(System.Convert.ToDouble(HP1.Text), System.Convert.ToDouble(HP2.Text), System.Convert.ToDouble(HP3.Text)).ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                            value = new Complex(_mathFunctions.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Solve)[0], _mathFunctions.PolarToRectangular(System.Convert.ToDouble(value.ToString().Substring(0, value.ToString().IndexOf("∠"))), System.Convert.ToDouble(value.ToString().Substring(value.ToString().IndexOf("∠") + 1)), Solve)[1]);
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

        private void TwoEquationSolve_Click(object sender, RoutedEventArgs e)
        {
            bool Call = true;
            try
            {
                List<String> tempList = new List<string>() { A1.Text, A2.Text, A0.Text, B1.Text, B2.Text, B0.Text };
                GetItems(tempList);
                foreach (Complex item in _mathFunctions.TwoEquation(_comp[0], _comp[1], _comp[2], _comp[3], _comp[4], _comp[5]))
                {
                    if (Call)
                    {
                        OutX.Text = item.ToString();
                        Call = false;
                    }
                    else
                    {
                        OutY.Text = item.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThreeEquationSolve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<String> tempList = new List<string>() { AT1.Text, AT2.Text, AT3.Text, AT0.Text, BT1.Text, BT2.Text, BT3.Text, BT0.Text, CT1.Text, CT2.Text, CT3.Text, CT0.Text };
                GetItems(tempList);
                Complex[] res = _mathFunctions.Threeequation(new Complex[] { _comp[0], _comp[1], _comp[2], _comp[3] }, new Complex[] { _comp[4], _comp[5], _comp[6], _comp[7] }, new Complex[] { _comp[8], _comp[9], _comp[10], _comp[11] });
                OutTX.Text = res[0].ToString();
                OutTY.Text = res[1].ToString();
                OutTZ.Text = res[2].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ConstListItems()
        {
            if (!string.IsNullOrWhiteSpace(InputConst.Text))
            {
                if (double.TryParse(InputConst.Text, out tryParse) && !InputConst.Text.Contains(","))
                {
                    ListValue.Text = (System.Convert.ToDouble(InputConst.Text) * _mathFunctions.Values(ConstantListBox.SelectedIndex)).ToString();
                    UnitValue.Text = _mathFunctions.Units(ConstantListBox.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("Invalid Input");
                }
            }
        }

        private void ConstantListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConstListItems();
        }

        private void InputConst_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConstListItems();
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Drager = "Null";
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Drager = "Move";
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Drager == "Move")
            {
                DragMove();
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

        private void InputConst_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((sender as TextBox).Name.Contains("AP") || (sender as TextBox).Name.Contains("InputConst") || (sender as TextBox).Name.Contains("GP") || (sender as TextBox).Name.Contains("HP") || (sender as TextBox).Name.Contains("ConvertText"))
            {
                HandleText(e, false);
            }
            else
            {
                HandleText(e, true);
            }
        }

        private void InputConst_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    e.Handled = true;
                }
            }
        }

        private void Angler_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in TwoQ.Children)
            {
                if (item is TextBox)
                {
                    if (((TextBox)item).IsFocused)
                    {
                        _start = (item as TextBox).SelectionStart;
                        (item as TextBox).Text = (item as TextBox).Text.Substring(0, _start) + "∠" + (item as TextBox).Text.Substring(_start);
                        (item as TextBox).SelectionStart = _start + 1;
                        Focused = (item as TextBox);
                        break;
                    }
                }
            }
        }

        private void Angler_Click(object sender, RoutedEventArgs e)
        {
            if (Focused is TextBox)
            {
                ((TextBox)Focused).Focus();
            }
        }

        private void AngleThree_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in ThreeQ.Children)
            {
                if (item is TextBox)
                {
                    if (((TextBox)item).IsFocused)
                    {
                        _start = (item as TextBox).SelectionStart;
                        (item as TextBox).Text = (item as TextBox).Text.Substring(0, _start) + "∠" + (item as TextBox).Text.Substring(_start);
                        (item as TextBox).SelectionStart = _start + 1;
                        Focused = (item as TextBox);
                        break;
                    }
                }
            }
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
