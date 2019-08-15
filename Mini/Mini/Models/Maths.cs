using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mini.Models
{
    #region public enums

    public enum Mode
    {
        Degree, Radians, Gradient
    }

    public enum Number
    {
        Binary, Decimal, Octal, HexaDecimal
    }

    public enum Converter
    {
        Angle, Area, Base, Energy, Length, Power, Pressure, Temperature, Time, Velocity, Volume, Weight
    }

    public enum Angle
    {
        Degree, Gradian, Radian
    }

    public enum Base
    {
        Binary, Decimal, HexaDecimal, Octal
    }

    public enum Area
    {
        Acres, Hectares, SquareCentimeter, SquareFeet, SquareInch, SquareKilometer, SquareMeter, SquareMile, SquareMillimeter, SquareYard
    }

    public enum Energy
    {
        BritishThermalUnit, Calorie, ElectronVolts, FootPound, Joule, KiloCalorie, KiloJoule
    }

    public enum Length
    {
        Angstorm, Centimeter, Chain, Fathom, Feet, Hand, Inch, Kilometer, Link, Meter, Microns, Mile, Millimeter, Nanometer, NauticalMile, PICA, Rods, Span, Yard
    }

    public enum Powers
    {
        BTUminute, FootPound, Horsepower, Kilowatt, Watt
    }

    public enum Pressure
    {
        Atmosphere, Bar, KiloPascal, MillimeterOfMercury, Pascal, PoundPerSquareInch
    }

    public enum Temperature
    {
        DegreeCelsius, DegreeFahrenheit, Kelvin
    }

    public enum Time
    {
        Day, Hour, MicroSecond, MilliSecond, Minute, Second, Week
    }

    public enum Velocity
    {
        CentimeterPerSecond, FeetPerSecond, KilometerPerHour, Knots, Mach, MeterPerSecond, MilesPerHour
    }

    public enum Volume
    {
        CubicCentimeter, CubicFeet, CubicInch, CubicMeter, CubicYard, FluidOunceUK, FluidounceUS, GallonUK, GallonUS, Liter, PintUK, PintUS, QuartUK, QuartUS
    }

    public enum Weight
    {
        Carat, CentiGram, DeciGram, DekaGram, Gram, HectoGram, KiloGram, LongTon, MilliGram, Ounce, Pound, ShortTon, Stone, Tonne
    }

    #endregion

    public class MathFunctions
    {
        #region private variables

        double power = 0, result = 0;

        List<string> _constants = new List<string>(){
"Speed of light c",
"Permeability of vacuum μ0",
"Permittivity of vacuum ε0",
"Gravitation constant G",
"Planck constant h",
"Angular Planck constant",
"Charge/Quantum ratio",
"Elementary charge e",
"Quantum/Charge ratio",
"Fine structure constant α",
"Inverse of fine structure constant",
"Boltzmann constant k",
"Planck mass mp",
"Planck time tp",
"Planck length lp",
"Planck temperature",
"Impedance of vacuum Z0",
"Magnetic flux quantum Φ0",
"Josephson constant KJ",
"von Klitzing constant RK",
"Conductance quantum G0",
"Inverse of conductance quantum",
"Stefan-Boltzmann const. σ",
"Rydberg constant R∞",
"Hartree energy EH",
"Bohr radius",
"Bohr magneton μB",
"Bohr magneton in Hz/T",
"Quantum of circulation",
"Richardson constant",
"Classical electron radius re",
"Thomson cross section σe",
"Boltzmann constant k",
"Boltzmann constant in eV/K",
"Avogadro's number NA",
"Molar Planck constant",
"Molar Planck constant by c",
"Electron molar mass",
"Electron molar charge",
"Faraday constant F",
"Molar gas constant R",
"Molar volume of ideal gas Vm",
"Electron volt",
"Electron volt to mass",
"Electron volt to atomic units u",
"Electron volt to frequency",
"Joul to eV",
"Mass to eV",
"Atomic unit u to eV",
"Frequency (1 Hz) to eV",
"Light-year ly"
       };

        List<double> _values = new List<double>() { 
        2.997924580*Math.Pow(10,+8 ),
12.566370614*Math.Pow(10,-7     ),
8.854187817*Math.Pow(10,-12     ),
6.6738480*Math.Pow(10,-11          ),
6.6260695729*Math.Pow(10,-34       ),
1.05457172647*Math.Pow(10,-34      ),
2.41798934853*Math.Pow(10,+14      ),
1.60217656535*Math.Pow(10,-19      ),
4.1356675210*Math.Pow(10,-15       ),
7.297352569824*Math.Pow(10,-3      ),
137.03599907445                    ,
1.380648813*Math.Pow(10,-23        ),
2.1765113*Math.Pow(10,-8           ),
5.3910632*Math.Pow(10,-44          ),
1.61619997*Math.Pow(10,-35         ),
1.41683385*Math.Pow(10,+32         ),
376.730313461                  ,
2.06783375846*Math.Pow(10,-15      ),
4.8359787011*Math.Pow(10,14        ),
2.5812807443484*Math.Pow(10,+4     ),
7.748091734625*Math.Pow(10,-5      ),
1.2906403721742*Math.Pow(10,+4     ),
5.67037321*Math.Pow(10,-8          ),
1.097373156853955*Math.Pow(10,+7   ),
4.3597443419*Math.Pow(10,-18       ),
5.291772109217*Math.Pow(10,-11     ),
9.2740096820*Math.Pow(10,-24       ),
1.39962455531*Math.Pow(10,+10      ),
3.636947552024*Math.Pow(10,-4      ),
1.20173*Math.Pow(10,+6             ),
2.817940326727*Math.Pow(10,-15     ),
0.665245873413*Math.Pow(10,-28     ),
1.380648813*Math.Pow(10,-23        ),
8.617332478*Math.Pow(10,-5         ),
6.0221412927*Math.Pow(10,+23       ),
3.990312717628*Math.Pow(10,-10     ),
0.11962656577984                   ,
5.485799094622*Math.Pow(10,-7      ),
-9.6485336521*Math.Pow(10,+4       ),
+9.6485336521*Math.Pow(10,+4       ),
8.314462175                        ,
22.41396820*Math.Pow(10,-3         ),
1.60217656535*Math.Pow(10,-19      ),
1.78266184539*Math.Pow(10,-36      ),
1.07354415024*Math.Pow(10,-9       ),
2.41798934853*Math.Pow(10,+14      ),
6.2415093414*Math.Pow(10,+18       ),
5.6095888512*Math.Pow(10,+35       ),
931.49406121*Math.Pow(10,+6        ),
4.13566751691*Math.Pow(10,-15      ),
9.4607304725808*Math.Pow(10,+15    )
        };

        List<string> _units = new List<string>() { 
"m.s-1"                    +" or m/s",
"kg.m.s-2.A-2"             +" or H/m or N/A2",
"kg-1.m-3.s4.A2"           +" or F/m",
"kg-1.m3.s-2"              ,   
"kg.m2.s-1"                +" or J.s",
"kg.m2.s-1"                +" or J.s",
"kg-1.m-2.s2.A"            +" or A/J",
"s.A"                      +" or C",
"kg.m2.s-2.A-1"            +" or J/A",
"Dimensionless"            ,   
"Dimensionless"            ,   
"kg.m2.s-2.K-1"            +" or J/K",
"kg"                       ,   
"s"                        ,   
"m"                        ,   
"K"                        ,   
"kg.m2.s-3.A-2"            +" or Ω",
"kg.m2.s-2.A-1"            +" or Wb",
"kg-1.m-2.s2.A"            +" or Hz/V",
"kg.m2.s-3.A-2"            +" or Ω",
"kg-1.m-2.s3.A2"           +" or S",
"kg.m2.s-3.A-2"            +" or Ω",
"kg.s-3.K-4"               +" or W/m2.K4",
"m-1"                      +" or m-1 ",
"kg.m2.s-2"                +" or J",
"m"                        +" or m",
"m2.A"                     +" or J/T",
"kg-1.s.A"                 +" or Hz/T",
"m2.s-1"                   +" or m2/s",
"A.m-2.K-2 "               ,   
"m"                        ,   
"m2"                       ,   
"kg.m2.s-2.K-1"            +" or J/K",
"kg.m2.s-3.A-1.K-1"        +" or V/K",
"mol-1"                    +" or count/mol",
"kg.m2.s-1.mol-1"          +" or J.s/mol",
"kg.m3.s-2.mol-1"          +" or J.m/mol",
"kg.mol-1"                 +" or kg/mol",
"s.A.mol-1"                +" or C/mol",
"s.A.mol-1"                +" or C/mol",
"kg.m2.s-2.K-1.mol-1"      +" or J/K.mol",
"m3.mol-1"                 +" or m3/mol",
"kg.m2.s-2"                +" or J",
"kg"                       ,  
"u",                          
"s-1"                      +" or Hz",
"eV",                         
"eV",                         
"eV",                         
"eV",                         
"m"                        +" or ~9.5 Pm"
        };

        #endregion

        #region public constructors

        public MathFunctions()
        {

        }

        #endregion

        #region private methods

        private ArrayList FindedList(ArrayList From, ArrayList To, int start, int end, string Method, ArrayList _numbersTemp)
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
                To[start] = MathStack(_numbersTemp)[0];
            }
            return To;
        }

        private ArrayList MathStack(ArrayList input)
        {
            double _temp1;
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

        private bool PerfectRoot(int a, out double ans, out double power, out List<double> _getFactors)
        {
            List<double> _primeList = new List<double>();
            _getFactors = new List<double>();
            _primeList = ListOfPrime(a);
            ans = 0;
            if (this.power == 0)
            {
                power = 0;
            }
            else
            {
                power = this.power;
            }
            _primeList.Sort();
            foreach (double item in _primeList)
            {
                if (!Math.Pow(a, 1d / item).ToString().Contains("."))
                {
                    ans = Math.Round(Math.Pow(a, 1d / item));
                    power += item;
                    _getFactors.Clear();
                    return true;
                }
                else if (!(a / item).ToString().Contains("."))
                {
                    _getFactors.Add(item);
                }
            }
            return false;
        }

        private void validate(double input)
        {
            if (input.ToString().Contains('.'))
            {
                if (input.ToString().Split('.').Length > 2)
                {
                    throw new InvalidOperationException("invalid input");
                }
            }
            if (input.ToString().Contains(','))
            {
                throw new InvalidOperationException("invalid input");
            }
        }

        private double Acres(Area b, double input)
        {
            switch (b)
            {
                case Area.Acres:
                    {
                        return input;
                    }
                case Area.Hectares:
                    {
                        return input * 0.40468564224d;
                    }
                case Area.SquareCentimeter:
                    {
                        return input * 40468564.224d;
                    }
                case Area.SquareFeet:
                    {
                        return input * 43560;
                    }
                case Area.SquareInch:
                    {
                        return input * 6272640;
                    }
                case Area.SquareKilometer:
                    {
                        return input * 0.0040468564224d;
                    }
                case Area.SquareMeter:
                    {
                        return input * 4046.8564224d;
                    }
                case Area.SquareMile:
                    {
                        return input * 0.0015625;
                    }
                case Area.SquareMillimeter:
                    {
                        return input * 4046856422.4d;
                    }
                case Area.SquareYard:
                    {
                        return input * 4840;
                    }
            }
            return 0;
        }

        private string Decimal(Base b, string input)
        {
            switch (b)
            {
                case Base.Binary:
                    {
                        Regex reg = new Regex("[0-9]", RegexOptions.IgnoreCase);
                        MatchCollection count = reg.Matches(input);
                        if (count.Count != input.Length)
                        {
                            throw new InvalidOperationException("please input integer");
                        }
                        return BinaryToDecimal(Convert.ToInt32(input));
                    }
                case Base.Decimal:
                    {
                        Regex reg = new Regex("[0-9]", RegexOptions.IgnoreCase);
                        MatchCollection count = reg.Matches(input);
                        if (count.Count != input.Length)
                        {
                            throw new InvalidOperationException("please input integer");
                        }
                        return input.ToString();
                    }
                case Base.HexaDecimal:
                    {
                        Regex reg = new Regex("[A-F0-9]", RegexOptions.IgnoreCase);
                        MatchCollection count = reg.Matches(input);
                        if (count.Count != input.Length)
                        {
                            throw new InvalidOperationException("please input integer");
                        }
                        return HexaToDecimal(Convert.ToString(input));
                    }
                case Base.Octal:
                    {
                        Regex reg = new Regex("[0-9]", RegexOptions.IgnoreCase);
                        MatchCollection count = reg.Matches(input);
                        if (count.Count != input.Length)
                        {
                            throw new InvalidOperationException("please input integer");
                        }
                        return OctalToDecimal(Convert.ToInt32(input));
                    }
            }
            return "0";
        }

        private double BritishThermalUnit(Energy b, double input)
        {
            switch (b)
            {
                case Energy.BritishThermalUnit:
                    {
                        return input;
                    }
                case Energy.Calorie:
                    {
                        return input * 251.9957963122194d;
                    }
                case Energy.ElectronVolts:
                    {
                        return input * 6585142025517001000000d;
                    }
                case Energy.FootPound:
                    {
                        return input * 778.1693709678747d;
                    }
                case Energy.Joule:
                    {
                        return input * 1055.056d;
                    }
                case Energy.KiloCalorie:
                    {
                        return input * 0.2519957963122194d;
                    }
                case Energy.KiloJoule:
                    {
                        return input * 1.055056d;
                    }
            }
            return 0;
        }

        private double Fathom(Length b, double input)
        {
            switch (b)
            {
                case Length.Angstorm:
                    {
                        return input * 18288000000d;
                    }
                case Length.Centimeter:
                    {
                        return input * 182.88d;
                    }
                case Length.Chain:
                    {
                        return input * 0.0909090909090909d;
                    }
                case Length.Fathom:
                    {
                        return input;
                    }
                case Length.Feet:
                    {
                        return input * 6d;
                    }
                case Length.Hand:
                    {
                        return input * 18d;
                    }
                case Length.Inch:
                    {
                        return input * 72d;
                    }
                case Length.Kilometer:
                    {
                        return input * 0.0018288d;
                    }
                case Length.Link:
                    {
                        return input * 9.090909090909091d;
                    }
                case Length.Meter:
                    {
                        return input * 1.8288d;
                    }
                case Length.Microns:
                    {
                        return input * 1828800d;
                    }
                case Length.Mile:
                    {
                        return input * 0.0011363636363636d;
                    }
                case Length.Millimeter:
                    {
                        return input * 1828.8d;
                    }
                case Length.Nanometer:
                    {
                        return input * 1828800000d;
                    }
                case Length.NauticalMile:
                    {
                        return input * 0.0009874730021598272d;
                    }
                case Length.PICA:
                    {
                        return input * 433.6200043362d;
                    }
                case Length.Rods:
                    {
                        return input * 0.3636363636363636d;
                    }
                case Length.Span:
                    {
                        return input * 8d;
                    }
                case Length.Yard:
                    {
                        return input * 2d;
                    }
            }
            return 0;
        }

        private double BTUPerMinute(Powers b, double input)
        {
            switch (b)
            {
                case Powers.BTUminute:
                    {
                        return input;
                    }
                case Powers.FootPound:
                    {
                        return input * 778.1693709678747d;
                    }
                case Powers.Horsepower:
                    {
                        return input * 0.0235808900293295d;
                    }
                case Powers.Kilowatt:
                    {
                        return input * 0.0175842666666667d;
                    }
                case Powers.Watt:
                    {
                        return input * 17.58426666666667d;
                    }
            }
            return 0;
        }

        private double Atmosphere(Pressure b, double input)
        {
            switch (b)
            {
                case Pressure.Atmosphere:
                    {
                        return input;
                    }
                case Pressure.Bar:
                    {
                        return input * 1.01325d;
                    }
                case Pressure.KiloPascal:
                    {
                        return input * 101.325d;
                    }
                case Pressure.MillimeterOfMercury:
                    {
                        return input * 760.1275318829707d;
                    }
                case Pressure.Pascal:
                    {
                        return input * 101325;
                    }
                case Pressure.PoundPerSquareInch:
                    {
                        return input * 14.69594940039221d;
                    }
            }
            return 0;
        }

        private double Celsius(Temperature b, double input)
        {
            switch (b)
            {
                case Temperature.DegreeCelsius:
                    {
                        return input;
                    }
                case Temperature.DegreeFahrenheit:
                    {
                        return ((input * 1.8d) + 32d);
                    }
                case Temperature.Kelvin:
                    {
                        return input + 273.15d;
                    }
            }
            return 0;
        }

        private double Day(Time b, double input)
        {
            switch (b)
            {
                case Time.Day:
                    {
                        return input;
                    }
                case Time.Hour:
                    {
                        return input * 24;
                    }
                case Time.MicroSecond:
                    {
                        return input * 86400000000;
                    }
                case Time.MilliSecond:
                    {
                        return input * 86400000;
                    }
                case Time.Minute:
                    {
                        return input * 1440;
                    }
                case Time.Second:
                    {
                        return input * 86400;
                    }
                case Time.Week:
                    {
                        return input * 0.1428571428571429d;
                    }
            }
            return 0;
        }

        private double Knots(Velocity b, double input)
        {
            switch (b)
            {
                case Velocity.CentimeterPerSecond:
                    {
                        return input * 51.44444444444444d;
                    }
                case Velocity.FeetPerSecond:
                    {
                        return input * 1.687809857101196d;
                    }
                case Velocity.KilometerPerHour:
                    {
                        return input * 1.852d;
                    }
                case Velocity.Knots:
                    {
                        return input;
                    }
                case Velocity.Mach:
                    {
                        return input * 0.0015117677734015d;
                    }
                case Velocity.MeterPerSecond:
                    {
                        return input * 0.5144444444444444;
                    }
                case Velocity.MilesPerHour:
                    {
                        return input * 1.150779448023543d;
                    }
            }
            return 0;
        }

        private double CubicFeet(Volume b, double input)
        {
            switch (b)
            {
                case Volume.CubicCentimeter:
                    {
                        return input * 28316.846592d;
                    }
                case Volume.CubicFeet:
                    {
                        return input;
                    }
                case Volume.CubicInch:
                    {
                        return input * 1728;
                    }
                case Volume.CubicMeter:
                    {
                        return input * 0.028316846592d;
                    }
                case Volume.CubicYard:
                    {
                        return input * 0.037037037037037d;
                    }
                case Volume.FluidOunceUK:
                    {
                        return input * 996.6136734468521d;
                    }
                case Volume.FluidounceUS:
                    {
                        return input * 957.5064935064935d;
                    }
                case Volume.GallonUK:
                    {
                        return input * 6.228835459042826d;
                    }
                case Volume.GallonUS:
                    {
                        return input * 7.480519480519481d;
                    }
                case Volume.Liter:
                    {
                        return input * 28.316846592d;
                    }
                case Volume.PintUK:
                    {
                        return input * 49.83068367234261d;
                    }
                case Volume.PintUS:
                    {
                        return input * 59.84415584415584d;
                    }
                case Volume.QuartUK:
                    {
                        return input * 24.9153418361713d;
                    }
                case Volume.QuartUS:
                    {
                        return input * 29.92207792207792d;
                    }
            }
            return 0;
        }

        private double KiloGram(Weight b, double input)
        {
            switch (b)
            {
                case Weight.Carat:
                    {
                        return input * 5000;
                    }
                case Weight.CentiGram:
                    {
                        return input * 100000;
                    }
                case Weight.DeciGram:
                    {
                        return input * 10000;
                    }
                case Weight.DekaGram:
                    {
                        return input * 100;
                    }
                case Weight.Gram:
                    {
                        return input * 1000;
                    }
                case Weight.HectoGram:
                    {
                        return input * 10;
                    }
                case Weight.KiloGram:
                    {
                        return input;
                    }
                case Weight.LongTon:
                    {
                        return input * 0.0009842065276110606d;
                    }
                case Weight.MilliGram:
                    {
                        return input * 1000000;
                    }
                case Weight.Ounce:
                    {
                        return input * 35.27396194958041d;
                    }
                case Weight.Pound:
                    {
                        return input * 2.204622621848776d;
                    }
                case Weight.ShortTon:
                    {
                        return input * 0.0011023113109244d;
                    }
                case Weight.Stone:
                    {
                        return input * 0.1574730444177697;
                    }
                case Weight.Tonne:
                    {
                        return input * 0.001;
                    }
            }
            return 0;
        }

        #endregion

        #region public methods

        public string BODMAS(ArrayList _numbersList)
        {
            int _count = 0, _count1 = 0, _check = 0;
            string _result, _temp = null;
            ArrayList _numbersTemp = new ArrayList();
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
                    while (_numbersList[_numbersList.IndexOf(")", _count) - 2].ToString() == "(")
                    {
                        try
                        {
                            if (int.TryParse(_numbersList[_numbersList.IndexOf(")", _count) - 3].ToString(), out _check))
                            {
                                _numbersList.Insert(_numbersList.IndexOf(")", _count) - 2, "*");
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (int.TryParse(_numbersList[_numbersList.IndexOf(")", _count) + 1].ToString(), out _check))
                            {
                                _numbersList.Insert(_numbersList.IndexOf(")", _count) + 1, "*");
                            }
                        }
                        catch (Exception) { }
                        _numbersList.RemoveAt(_numbersList.IndexOf(")", _count) - 2);
                        _numbersList.RemoveAt(_numbersList.IndexOf(")", _count));
                    }
                    while (_numbersList.IndexOf("(", _count1) < _numbersList.IndexOf(")", _numbersList.IndexOf("(", _count1)))
                    {
                        _numbersTemp.Clear();
                        int startIndex = _numbersList.IndexOf("(", _count1) + 1;
                        int endIndex = _numbersList.IndexOf(")", _numbersList.IndexOf("(", _count1));
                        if (!_numbersList.GetRange(startIndex, endIndex - startIndex).Contains("("))
                        {
                            _numbersTemp = FindedList(_numbersList, _numbersTemp, startIndex, endIndex, "add", _numbersTemp);
                        }
                        if (!_numbersTemp.Contains("(") && !_numbersTemp.Contains(")") && _numbersTemp.Count > 0)
                        {
                            _numbersList = FindedList(_numbersTemp, _numbersList, startIndex, endIndex, "remove", _numbersTemp);
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
                _numbersList = MathStack(_numbersList);
            }
            _result = null;
            foreach (double item in _numbersList)
            {
                _result += item;
            }
            return _result;
        }

        public Complex[] TwoEquation(Complex a1, Complex a2, Complex a, Complex b1, Complex b2, Complex b)
        {
            return new Complex[] { (((a * b2) - (b * a2)) / ((b2 * a1) - (a2 * b1))), (((a * b1) - (b * a1)) / ((a2 * b1) - (b2 * a1))) };
        }

        public Complex[] Threeequation(Complex[] a, Complex[] b, Complex[] c)
        {
            Complex[] ans1 = new Complex[3];
            Complex[] ans2 = new Complex[3];
            Complex x, y, z;
            ans1[0] = (a[0] * b[1]) - (a[1] * b[0]);
            ans1[1] = (a[2] * b[1]) - (a[1] * b[2]);
            ans1[2] = (a[3] * b[1]) - (b[3] * a[1]);
            ans2[0] = (a[0] * c[1]) - (a[1] * c[0]);
            ans2[1] = (a[2] * c[1]) - (a[1] * c[2]);
            ans2[2] = (a[3] * c[1]) - (a[1] * c[3]);
            x = ((ans1[2] * ans2[1]) - (ans2[2] * ans1[1])) / ((ans2[1] * ans1[0]) - (ans1[1] * ans2[0]));
            z = ((ans1[2] * ans2[0]) - (ans2[2] * ans1[0])) / ((ans1[1] * ans2[0]) - (ans2[1] * ans1[0]));
            y = (a[3] - ((a[0] * x) + (a[2] * z))) / a[1];
            return new Complex[] { x, y, z };
        }

        public Complex Power(Complex a, Complex b)
        {
            return Complex.Pow(a, b);
        }

        public Complex Log(Complex a, double b)
        {
            return Complex.Log(a, b);
        }

        public Complex Exp(Complex a)
        {
            return Complex.Exp(a);
        }

        public Complex InverseLog(Complex a)
        {
            return Complex.Pow(Convert.ToDouble(10), a);
        }

        public Complex Sin(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Sin(a);
        }

        public Complex Cos(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Cos(a);
        }

        public Complex Tan(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Tan(a);
        }

        public Complex Sinh(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Sinh(a);
        }

        public Complex Cosh(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Cosh(a);
        }

        public Complex Tanh(Complex a, Mode mode)
        {
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = RadToDeg(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToGrad(RadToDeg(a));
                        break;
                    }
            }
            return Complex.Tanh(a);
        }

        public Complex ASin(Complex a, Mode mode)
        {
            a = Complex.Asin(a);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex ACos(Complex a, Mode mode)
        {
            a = Complex.Acos(a);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex ATan(Complex a, Mode mode)
        {
            a = Complex.Atan(a);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex ASinh(Complex a, Mode mode)
        {
            a = Complex.Log(a + Complex.Sqrt((a * a) + 1), Math.E);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex ACosh(Complex a, Mode mode)
        {
            a = Complex.Log(a + Complex.Sqrt((a * a) - 1), Math.E);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex ATanh(Complex a, Mode mode)
        {
            a = Complex.Log(Complex.Sqrt(1 + a), Math.E) - Complex.Log(Complex.Sqrt(1 - a), Math.E);
            switch (mode)
            {
                case Mode.Degree:
                    {
                        a = DegToRad(a);
                        break;
                    }
                case Mode.Radians:
                    {
                        break;
                    }
                case Mode.Gradient:
                    {
                        a = DegToRad(GradToDeg(a));
                        break;
                    }
            }
            return a;
        }

        public Complex DegToRad(Complex a)
        {
            return (a * 180d) / Math.PI;
        }

        public Complex RadToDeg(Complex a)
        {
            return Math.PI * a / 180d;
        }

        public Complex DegToGrad(Complex a)
        {
            return 0.900000000000001d * a;
        }

        public Complex GradToDeg(Complex a)
        {
            return a / 0.900000000000001d;
        }

        public double MinuteToDeg(double a)
        {
            return (a / 60d);
        }

        public double DegToMinute(double a)
        {
            return a * 60d;
        }

        public double SecondsToDeg(double a)
        {
            return (a / 3600d);
        }

        public double DegToSeconds(double a)
        {
            return (a * 3600d);
        }

        public string MixVsFraction(double a, double b = 1)
        {
        lab:
            if (a % 10 != 0)
            {
                a = (a * 10);
                b = 10 * b;
                goto lab;
            }
            a = a / 10;
            b = b / 10;
            try
            {
                for (int i = Convert.ToInt32(Math.Min(a, b)); i > 0; i--)
                {
                    if (Convert.ToInt32(Math.Min(a, b)) % i == 0)
                    {
                        if (Convert.ToInt32(Math.Max(a, b)) % i == 0)
                        {
                            //mixed
                            if ((a / i) > (b / i))
                            {
                                return (Math.Floor((a / i) / (b / i))).ToString() + "(" + ((a / i) % (b / i)).ToString() + "/" + (b / i).ToString() + ")";
                            }
                            //fraction
                            else { return (a / i).ToString() + "/" + (b / i).ToString(); }
                        }
                    }
                }
                return (a + "/" + b);
            }
            catch (Exception)
            {
                return (a + "/" + b);
            }
        }

        public byte And(byte a, byte b)
        {
            return Convert.ToByte(a & b);
        }

        public byte Or(byte a, byte b)
        {
            return Convert.ToByte(a | b);
        }

        public byte Xor(byte a, byte b)
        {
            return Convert.ToByte(a ^ b);
        }

        public byte Not(byte a)
        {
            if (a == 1)
            {
                return 0;
            }
            else if (a == 0)
            {
                return 1;
            }
            return 0;
        }

        public bool Prime(double a)
        {
            int j = 0;
            for (int i = 1; i <= a; i++)
            {
                if (a % i == 0)
                {
                    j++;
                }
            }
            if (j == 2) { return true; }
            else { return false; }
        }

        public List<double> ListOfPrime(int a)
        {
            int b = 0;
            List<double> temp = new List<double>();
            for (int i = 1; i <= a; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        b++;
                    }
                }
                if (b == 2)
                {
                    temp.Add(i);
                }
                b = 0;
            }
            return temp;
        }

        public int Permutation(int a, int b)
        {
            if (b == -1)
            {
                throw new NullReferenceException();
            }
            int c = 1;
            for (int i = 0; i < b; i++)
            {
                c = c * (a - i);
            }
            return c;
        }

        public int Combination(int a, int b)
        {
            if (b == -1)
            {
                throw new NullReferenceException();
            }
            int c = 1, d = 1;
            for (int i = 0; i < b; i++)
            {
                c = c * (a - i);
                d = d * (b - i);
            }
            return (c / d);
        }

        public int Fact(int a)
        {
            int b = 1;
            for (int i = 1; i <= a; i++)
            {
                b = b * i;
            }
            return b;
        }

        public Complex Add(Complex a, Complex b)
        {
            return a + b;
        }

        public Complex Sub(Complex a, Complex b)
        {
            return a - b;
        }

        public Complex Mul(Complex a, Complex b)
        {
            return a * b;
        }

        public Complex Abs(Complex a)
        {
            return Complex.Abs(a);
        }

        public double Modulus(double a, double b)
        {
            return a % b;
        }

        public ArrayList LCM(int a)
        {
            try
            {
                if (a == 0) { return null; }
                int b = a;
                ArrayList temp = new ArrayList();
                ArrayList result = new ArrayList();
            lab1:
                int o = 0;
                int q;
                int j;
                long k = 1;
                int l = 1;
                a = 1;
            lab:
                for (int i = 1; i <= b; i++)
                {
                    if (a % i == 0 && a > i)
                    {
                        l++;
                    }
                    else if (a == i)
                    {
                        if (l == 2)
                        {
                            temp.Add(a);
                            k = k * a;
                            o++;
                        }
                        else if (l > 2)
                        {
                            for (j = 2; j <= b; j++)
                            {
                                double y = 1 / Convert.ToDouble(j);
                                double z = Math.Pow(Convert.ToDouble(a), y);
                                if (z % 1 == 0)
                                {
                                    for (q = 0; q < temp.Count; q++)
                                    {
                                        if (a % Convert.ToInt32(temp[q]) == 0)
                                        {
                                            k = k * (a / Convert.ToInt32(temp[q]));
                                            temp.RemoveAt(q);
                                            temp.Insert(q, a);
                                        }
                                    }
                                }
                            }
                        }
                        a = a + 1;
                        l = 1;
                        goto lab;
                    }
                    if (a > b)
                    {
                        result.Add(k);
                        break;
                    }
                }
                b = b - 1;
                if (b == 0) { goto lab3; }
                goto lab1;
            lab3:
                result.Reverse();
                return result;
            }
            catch (Exception) { return null; }
        }

        public string DecimalToBinary(int a)
        {
            ArrayList list = new ArrayList();
            string c = null;
        lab:
            if (a > 2)
            {
                list.Add(Convert.ToInt32(a % 2));
                a = a / 2;
                goto lab;
            }
            list.Add(Convert.ToInt32(a % 2));
            list.Add(Convert.ToInt32(a / 2));
            list.Reverse();
            foreach (var item in list) { c = c + item.ToString(); }
            return c;
        }

        public string DecimalToOctal(int a)
        {
            ArrayList list = new ArrayList();
            string c = null;
        lab1:
            if (a >= 8)
            {
                list.Add(Convert.ToInt32(a % 8));
                a = a / 8;
                goto lab1;
            }
            list.Add(Convert.ToInt32(a));
            list.Reverse();
            foreach (var item in list) { c = c + item.ToString(); }
            return c;
        }

        public string DecimalToHexa(int a)
        {
            ArrayList list = new ArrayList();
            string c = null;
        lab2:
            if (a >= 16)
            {
                if (a % 16 >= 10)
                {
                    if (a % 16 == 10) { list.Add('A'); }
                    if (a % 16 == 11) { list.Add('B'); }
                    if (a % 16 == 12) { list.Add('C'); }
                    if (a % 16 == 13) { list.Add('D'); }
                    if (a % 16 == 14) { list.Add('E'); }
                    if (a % 16 == 15) { list.Add('F'); }
                }
                else
                {
                    list.Add(Convert.ToInt32(a % 16));
                }
                a = a / 16;
                goto lab2;
            }
            else if (a >= 10)
            {
                if (a == 10) { list.Add('A'); a = 0; }
                if (a == 11) { list.Add('B'); a = 0; }
                if (a == 12) { list.Add('C'); a = 0; }
                if (a == 13) { list.Add('D'); a = 0; }
                if (a == 14) { list.Add('E'); a = 0; }
                if (a == 15) { list.Add('F'); a = 0; }
            }
            if (a != 0)
            {
                list.Add(Convert.ToInt32(a));
            }
            list.Reverse();
            foreach (var item in list) { c = c + item.ToString(); }
            return c;
        }

        public string BinaryToDecimal(int a)
        {
            ArrayList list = new ArrayList();
            int pow = 0;
            int count = a.ToString().Length - 1;
            char[] d = a.ToString().ToCharArray();
        lab3:
            if (pow <= a.ToString().Length - 1)
            {
                list.Add(Convert.ToInt32((d[pow]) - 48) * Math.Pow(Convert.ToDouble(2), Convert.ToDouble(count)));
                pow++;
                count--;
                goto lab3;
            }
            a = 0;
            foreach (var item in list) { a = a + Convert.ToInt32(item); }
            return a.ToString();
        }

        public string OctalToDecimal(int a)
        {
            ArrayList list = new ArrayList();
            int pow = 0;
            int count = a.ToString().Length - 1;
            char[] d = a.ToString().ToCharArray();
        lab3:
            if (pow <= a.ToString().Length - 1)
            {
                list.Add(Convert.ToInt32((d[pow]) - 48) * Math.Pow(Convert.ToDouble(8), Convert.ToDouble(count)));
                pow++;
                count--;
                goto lab3;
            }
            a = 0;
            foreach (var item in list) { a = a + Convert.ToInt32(item); }
            return a.ToString();
        }

        public string HexaToDecimal(string a)
        {
            ArrayList list = new ArrayList();
            int pow = 0;
            int e = a.ToString().Length - 1;
            char[] itemList = a.ToCharArray();
        lab3:
            if (pow <= a.ToString().Length - 1)
            {
                if (!char.IsDigit(itemList[pow]))
                {
                    if (itemList[pow] == 'A' || itemList[pow] == 'a') { list.Add(Convert.ToInt32(10) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                    if (itemList[pow] == 'B' || itemList[pow] == 'b') { list.Add(Convert.ToInt32(11) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                    if (itemList[pow] == 'C' || itemList[pow] == 'c') { list.Add(Convert.ToInt32(12) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                    if (itemList[pow] == 'D' || itemList[pow] == 'd') { list.Add(Convert.ToInt32(13) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                    if (itemList[pow] == 'E' || itemList[pow] == 'e') { list.Add(Convert.ToInt32(14) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                    if (itemList[pow] == 'F' || itemList[pow] == 'f') { list.Add(Convert.ToInt32(15) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e))); }
                }
                if (char.IsDigit(itemList[pow]))
                {
                    list.Add(Convert.ToInt32((itemList[pow]) - 48) * Math.Pow(Convert.ToDouble(16), Convert.ToDouble(e)));
                }
                pow++;
                e--;
                goto lab3;
            }
            a = null;
            foreach (var item in list) { a = (Convert.ToInt32(a) + Convert.ToInt32(item)).ToString(); }
            return a;
        }

        public double[,] MatrixAdd(double[,] A, double[,] B)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            double[,] C = new double[row, col];
            int rw1 = 0;
            int cl1 = 0;
            int row1 = B.GetLength(0);
            int col1 = B.GetLength(1);
            if (row != row1 || col != col1)
            {
                throw new InvalidOperationException("invalid row and column length");
            }
            if (row == row1 && col == col1)
            {
                while (rw1 < row)
                {
                    C[rw1, cl1] = A[rw1, cl1] + B[rw1, cl1];
                    cl1++;
                    if (cl1 == col)
                    {
                        cl1 = 0;
                        rw1++;
                    }
                }
            }
            return C;
        }

        public double[,] MatrixSub(double[,] A, double[,] B)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            double[,] C = new double[row, col];
            int rw1 = 0;
            int cl1 = 0;
            int row1 = B.GetLength(0);
            int col1 = B.GetLength(1);
            if (row != row1 || col != col1)
            {
                throw new InvalidOperationException("invalid row and column length");
            }
            if (row == row1 && col == col1)
            {
                while (rw1 < row)
                {
                    C[rw1, cl1] = A[rw1, cl1] - B[rw1, cl1];
                    cl1++;
                    if (cl1 == col)
                    {
                        cl1 = 0;
                        rw1++;
                    }
                }
            }
            return C;
        }

        public double[,] MatrixMul(double[,] A, double[,] B)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            double[,] ANS = null;
            ArrayList D = new ArrayList();
            int rw1 = 0;
            int cl1 = 0;
            double ans = 0;
            int row1 = B.GetLength(0);
            int col1 = B.GetLength(1);
            if ((row == 0 && col == 0) || (row1 == 0 && col1 == 0))
            {
                throw new InvalidOperationException("null values in list of matrix");
            }
            else if (row != col1 || row1 != col)
            {
                throw new InvalidOperationException("unable to multiple invalid rows and columns");
            }
            else if (row == 1 && col1 == 1 && row1 == 1 && col == 1)
            {
                throw new InvalidOperationException("unable to multiple invalid rows and columns");
            }
            int rw11 = 0;
            int cl11 = 0;
            double[,] E = null;
            double[,] C = null;
            if (row > col && row1 < col1)
            {
                E = new double[row, col1];
                C = new double[row, col1];
                ANS = new double[row, col1];
                rw11 = 0;
                cl11 = 0;
                while (rw11 < row && cl11 < col1)
                {
                    if (rw11 < B.GetLength(0) && cl11 < B.GetLength(1))
                    {
                        C[rw11, cl11] = B[rw11, cl11];
                    }
                    else
                    {
                        C[rw11, cl11] = 0;
                    }
                    cl11++;
                    if (cl11 == col1)
                    {
                        cl11 = 0;
                        rw11++;
                    }
                }
                rw11 = 0;
                cl11 = 0;
                while (rw11 < row && cl11 < col1)
                {
                    if (rw11 < A.GetLength(0) && cl11 < A.GetLength(1))
                    {
                        E[rw11, cl11] = A[rw11, cl11];
                    }
                    else
                    {
                        E[rw11, cl11] = 0;
                    }
                    cl11++;
                    if (cl11 == col1)
                    {
                        cl11 = 0;
                        rw11++;
                    }
                }
            }
            if (row <= col && row1 >= col1)
            {
                E = new double[row1, col];
                C = new double[row1, col];
                ANS = new double[row, col1];
                rw11 = 0;
                cl11 = 0;
                while (rw11 < row1 && cl11 < col)
                {
                    if (rw11 < B.GetLength(0) && cl11 < B.GetLength(1))
                    {
                        C[rw11, cl11] = B[rw11, cl11];
                    }
                    else
                    {
                        C[rw11, cl11] = 0;
                    }
                    cl11++;
                    if (cl11 == col)
                    {
                        cl11 = 0;
                        rw11++;
                    }
                }
                rw11 = 0;
                cl11 = 0;
                while (rw11 < row1 && cl11 < col)
                {
                    if (rw11 < A.GetLength(0) && cl11 < A.GetLength(1))
                    {
                        E[rw11, cl11] = A[rw11, cl11];
                    }
                    else
                    {
                        E[rw11, cl11] = 0;
                    }
                    cl11++;
                    if (cl11 == col)
                    {
                        cl11 = 0;
                        rw11++;
                    }
                }
            }
            rw1 = 0;
            cl1 = 0;
            rw11 = 0;
            if (row == col1)
            {
                while (rw11 < C.GetLength(0))
                {
                    D.Add(E[rw11, cl1] * C[cl1, rw1]);
                    rw1++;
                    if (rw1 == C.GetLength(0))
                    {
                        rw1 = 0;
                        cl1++;
                    }
                    if (cl1 == C.GetLength(1))
                    {
                        cl1 = 0;
                        rw1 = 0;
                        rw11++;
                    }
                }
            }
            rw1 = 0;
            cl1 = 0;
            int main = C.GetLength(0);
            int hmm = 0;
            int qu = 0;
            int qu1 = 0;
            ArrayList Z = new ArrayList(2);
            while (true)
            {
                if (hmm == D.Count - 1) { break; }
                hmm = qu1 - main;
                int ing = 0;
                while (ing < main)
                {
                    hmm = hmm + main;
                    ans = ans + Convert.ToDouble(D[hmm]);
                    ing++;
                }
                if (qu == main - 1)
                {
                    qu = -1;
                    qu1 = hmm;
                }
                qu++;
                qu1++;
                Z.Add(ans);
                ans = 0;
            }
            for (int i = 0; i < main; i++)
            {
                if (i == A.GetLength(0) && i == B.GetLength(1))
                {
                    Z.RemoveAt(i);
                }
            }
            hmm = 0;
            while (rw1 < ANS.GetLength(0) && cl1 < ANS.GetLength(1))
            {
                ANS[rw1, cl1] = Convert.ToDouble(Z[hmm]);
                cl1++;
                hmm++;
                if (cl1 == ANS.GetLength(1))
                {
                    cl1 = 0;
                    rw1++;
                }
            }
            return ANS;
        }

        public double[] PolarToRectangular(double r, double Theta, Mode mode)
        {
            double a = r * Cos(Theta, mode).Real;
            double b = r * Sin(Theta, mode).Real;
            return new double[] { a, b };
        }

        public double[] RectangularToPolar(double a, double b, Mode mode)
        {
            double r = Math.Sqrt(Convert.ToDouble(a * a) + Convert.ToDouble(b * b));
            double Theta = ATan(b / a, mode).Real;
            return new double[] { r, Theta };
        }

        public double Lcm(List<double> a)
        {
            a.Sort();
            a.Reverse();
            int temp = 1, count = 0;
            List<double> list = ListOfPrime(Convert.ToInt32(a[0]));
            while (count < list.Count)
            {
                if (!a.Any(content => content != 1))
                {
                    break;
                }
                if (a.Any(content => !(content / list[count]).ToString().Contains(".")))
                {
                    for (int i = 0; i < a.Count; i++)
                    {
                        if (!(a[i] / list[count]).ToString().Contains("."))
                        {
                            a[i] = a[i] / list[count];
                        }
                    }
                    temp *= Convert.ToInt32(list[count]);
                }
                else
                {
                    count++;
                }
            }
            return temp;
        }

        public double Hcf(List<double> a)
        {
            a.Sort();
            a.Reverse();
            List<double> list = ListOfPrime(Convert.ToInt32(a[0] / 2));
            int count = 0;
            double temp = 1;
            while (count < list.Count)
            {
                if (a.Any(content => content == 1))
                {
                    break;
                }
                if (a.All(content => !(content / list[count]).ToString().Contains(".")))
                {
                    for (int i = 0; i < a.Count; i++)
                    {
                        a[i] = a[i] / list[count];
                    }
                    temp *= list[count];
                }
                else
                {
                    count++;
                }
            }
            return temp;
        }

        public string Mean(List<double> list)
        {
            double resultOut = 0;
            resultOut = list.Sum();
            resultOut = resultOut / list.Count;
            return resultOut.ToString();
        }

        public string Variance(List<double> list)
        {
            List<double> list1 = new List<double>();
            list1.AddRange(list);
            string meanValue = Mean(list1);
            for (int i = 0; i < list.Count; i++)
            {
                list1[i] = Math.Pow(list1[i] - Convert.ToDouble(meanValue), 2);
            }
            if (meanValue.Contains("."))
            {
                meanValue = (list1.Sum() / (list1.Count - 1)).ToString();
            }
            else
            {
                meanValue = (list1.Sum() / list1.Count).ToString();
            }
            return meanValue;
        }

        public string StandardDeviation(string variance)
        {
            double outValue = 0;
            if (double.TryParse(variance, out outValue))
            {
                return Math.Pow(Convert.ToDouble(variance), 1d / 2d).ToString();
            }
            return "0";
        }

        public string MeanSquare(List<double> list)
        {
            List<double> list1 = new List<double>();
            list1.AddRange(list);
            double resultOut = 0;
            resultOut = list1.Sum(a => a = Math.Pow(a, 2));
            resultOut = resultOut / list.Count;
            return resultOut.ToString();
        }

        public string SquareSum(List<double> list)
        {
            List<double> list1 = new List<double>();
            list1.AddRange(list);
            return list1.Sum(a => a = Math.Pow(a, 2)).ToString();
        }

        public string Sum(List<double> list)
        {
            List<double> list1 = new List<double>();
            list1.AddRange(list);
            return list1.Sum().ToString();
        }

        public double AngleToAngle(Angle a, Angle b, double input)
        {
            validate(input);
            switch (a)
            {
                case Angle.Degree:
                    {
                        switch (b)
                        {
                            case Angle.Degree:
                                {
                                    return input;
                                }
                            case Angle.Gradian:
                                {
                                    return GradToDeg(input).Real;
                                }
                            case Angle.Radian:
                                {
                                    return RadToDeg(input).Real;
                                }
                        }
                        break;
                    }
                case Angle.Gradian:
                    {
                        switch (b)
                        {
                            case Angle.Degree:
                                {
                                    return DegToGrad(input).Real;
                                }
                            case Angle.Gradian:
                                {
                                    return input;
                                }
                            case Angle.Radian:
                                {
                                    return RadToDeg(DegToGrad(input)).Real;
                                }
                        }
                        break;
                    }
                case Angle.Radian:
                    {
                        switch (b)
                        {
                            case Angle.Degree:
                                {
                                    return DegToRad(input).Real;
                                }
                            case Angle.Gradian:
                                {
                                    return DegToRad(GradToDeg(input)).Real;
                                }
                            case Angle.Radian:
                                {
                                    return input;
                                }
                        }
                        break;
                    }
            }
            return 0;
        }

        public string BaseToBase(Base a, Base b, string input)
        {
            switch (b)
            {
                case Base.Binary:
                    {
                        return DecimalToBinary(Convert.ToInt32(Decimal(a, input)));
                    }
                case Base.Decimal:
                    {
                        return Decimal(a, input);
                    }
                case Base.HexaDecimal:
                    {
                        return DecimalToHexa(Convert.ToInt32(Decimal(a, input)));
                    }
                case Base.Octal:
                    {
                        return DecimalToOctal(Convert.ToInt32(Decimal(a, input)));
                    }
            }
            return "0";
        }

        public double AreaToArea(Area a, Area b, double input)
        {
            validate(input);
            switch (a)
            {
                case Area.Acres:
                    {
                        return Acres(b, input);
                    }
                case Area.Hectares:
                    {
                        return Acres(b, input) * 2.471053814671653d;
                    }
                case Area.SquareCentimeter:
                    {
                        return Acres(b, input) / 40468564.224d;
                    }
                case Area.SquareFeet:
                    {
                        return Acres(b, input) / 43560d;
                    }
                case Area.SquareInch:
                    {
                        return Acres(b, input) / 6272640d;
                    }
                case Area.SquareKilometer:
                    {
                        return Acres(b, input) / 0.0040468564224d;
                    }
                case Area.SquareMeter:
                    {
                        return Acres(b, input) / 4046.8564224d;
                    }
                case Area.SquareMile:
                    {
                        return Acres(b, input) * (1 / 0.0015625d);
                    }
                case Area.SquareMillimeter:
                    {
                        return Acres(b, input) * (1 / 4046856422.4d);
                    }
                case Area.SquareYard:
                    {
                        return Acres(b, input) * (1 / 4840d);
                    }
            }
            return 0;
        }

        public double EnergyToEnergy(Energy a, Energy b, double input)
        {
            validate(input);
            switch (a)
            {
                case Energy.BritishThermalUnit:
                    {
                        return BritishThermalUnit(b, input);
                    }
                case Energy.Calorie:
                    {
                        return BritishThermalUnit(b, input) / 251.9957963122194d;
                    }
                case Energy.ElectronVolts:
                    {
                        return BritishThermalUnit(b, input) / 6585142025517001000000d;
                    }
                case Energy.FootPound:
                    {
                        return BritishThermalUnit(b, input) / 778.1693709678747d;
                    }
                case Energy.Joule:
                    {
                        return BritishThermalUnit(b, input) / 1055.056d;
                    }
                case Energy.KiloCalorie:
                    {
                        return BritishThermalUnit(b, input) / 0.2519957963122194d;
                    }
                case Energy.KiloJoule:
                    {
                        return BritishThermalUnit(b, input) / 1.055056d;
                    }
            }
            return 0;
        }

        public double LengthToLength(Length a, Length b, double input)
        {
            validate(input);
            switch (a)
            {
                case Length.Angstorm:
                    {
                        return Fathom(b, input) / 18288000000;
                    }
                case Length.Centimeter:
                    {
                        return Fathom(b, input) / 182.88d;
                    }
                case Length.Chain:
                    {
                        return Fathom(b, input) / 0.0909090909090909d;
                    }
                case Length.Fathom:
                    {
                        return Fathom(b, input);
                    }
                case Length.Feet:
                    {
                        return Fathom(b, input) / 6;
                    }
                case Length.Hand:
                    {
                        return Fathom(b, input) / 18;
                    }
                case Length.Inch:
                    {
                        return Fathom(b, input) / 72;
                    }
                case Length.Kilometer:
                    {
                        return Fathom(b, input) / 0.0018288d;
                    }
                case Length.Link:
                    {
                        return Fathom(b, input) / 9.090909090909091d;
                    }
                case Length.Meter:
                    {
                        return Fathom(b, input) / 1.8288d;
                    }
                case Length.Microns:
                    {
                        return Fathom(b, input) / 1828800;
                    }
                case Length.Mile:
                    {
                        return Fathom(b, input) / 0.0011363636363636d;
                    }
                case Length.Millimeter:
                    {
                        return Fathom(b, input) / 1828.8d;
                    }
                case Length.Nanometer:
                    {
                        return Fathom(b, input) / 1828800000;
                    }
                case Length.NauticalMile:
                    {
                        return Fathom(b, input) / 0.0009874730021598272d;
                    }
                case Length.PICA:
                    {
                        return Fathom(b, input) / 433.6200043362d;
                    }
                case Length.Rods:
                    {
                        return Fathom(b, input) / 0.3636363636363636d;
                    }
                case Length.Span:
                    {
                        return Fathom(b, input) / 8;
                    }
                case Length.Yard:
                    {
                        return Fathom(b, input) / 2;
                    }
            }
            return 0;
        }

        public double PowerToPower(Powers a, Powers b, double input)
        {
            validate(input);
            switch (a)
            {
                case Powers.BTUminute:
                    {
                        return BTUPerMinute(b, input);
                    }
                case Powers.FootPound:
                    {
                        return BTUPerMinute(b, input) / 778.1693709678747d;
                    }
                case Powers.Horsepower:
                    {
                        return BTUPerMinute(b, input) / 0.0235808900293295d;
                    }
                case Powers.Kilowatt:
                    {
                        return BTUPerMinute(b, input) / 0.0175842666666667d;
                    }
                case Powers.Watt:
                    {
                        return BTUPerMinute(b, input) / 17.58426666666667d;
                    }
            }
            return 0;
        }

        public double PressureToPressure(Pressure a, Pressure b, double input)
        {
            validate(input);
            switch (a)
            {
                case Pressure.Atmosphere:
                    {
                        return Atmosphere(b, input);
                    }
                case Pressure.Bar:
                    {
                        return Atmosphere(b, input) / 1.01325d;
                    }
                case Pressure.KiloPascal:
                    {
                        return Atmosphere(b, input) / 101.325d;
                    }
                case Pressure.MillimeterOfMercury:
                    {
                        return Atmosphere(b, input) / 760.1275318829707d;
                    }
                case Pressure.Pascal:
                    {
                        return Atmosphere(b, input) / 101325;
                    }
                case Pressure.PoundPerSquareInch:
                    {
                        return Atmosphere(b, input) / 14.69594940039221d;
                    }
            }
            return 0;
        }

        public double TemperatureToTemperature(Temperature a, Temperature b, double input)
        {
            validate(input);
            switch (a)
            {
                case Temperature.DegreeCelsius:
                    {
                        return Celsius(b, input);
                    }
                case Temperature.DegreeFahrenheit:
                    {
                        return (Celsius(b, input) - 32d) / 1.8d;
                    }
                case Temperature.Kelvin:
                    {
                        return Celsius(b, input) - 273.15d;
                    }
            }
            return 0;
        }

        public double TimeToTime(Time a, Time b, double input)
        {
            validate(input);
            switch (a)
            {
                case Time.Day:
                    {
                        return Day(b, input);
                    }
                case Time.Hour:
                    {
                        return Day(b, input) / 24d;
                    }
                case Time.MicroSecond:
                    {
                        return Day(b, input) / 86400000000;
                    }
                case Time.MilliSecond:
                    {
                        return Day(b, input) / 86400000;
                    }
                case Time.Minute:
                    {
                        return Day(b, input) / 1440;
                    }
                case Time.Second:
                    {
                        return Day(b, input) / 86400;
                    }
                case Time.Week:
                    {
                        return Day(b, input) / 0.1428571428571429d;
                    }
            }
            return 0;
        }

        public double VelocityToVelocity(Velocity a, Velocity b, double input)
        {
            validate(input);
            switch (a)
            {
                case Velocity.CentimeterPerSecond:
                    {
                        return Knots(b, input) / 51.44444444444444d;
                    }
                case Velocity.FeetPerSecond:
                    {
                        return Knots(b, input) / 1.687809857101196d;
                    }
                case Velocity.KilometerPerHour:
                    {
                        return Knots(b, input) / 1.852d;
                    }
                case Velocity.Knots:
                    {
                        return Knots(b, input);
                    }
                case Velocity.Mach:
                    {
                        return Knots(b, input) / 0.0015117677734015d;
                    }
                case Velocity.MeterPerSecond:
                    {
                        return Knots(b, input) / 0.5144444444444444d;
                    }
                case Velocity.MilesPerHour:
                    {
                        return Knots(b, input) / 1.150779448023543d;
                    }
            }
            return 0;
        }

        public double VolumeToVolume(Volume a, Volume b, double input)
        {
            validate(input);
            switch (a)
            {
                case Volume.CubicCentimeter:
                    {
                        return CubicFeet(b, input) / 28316.846592d;
                    }
                case Volume.CubicFeet:
                    {
                        return CubicFeet(b, input);
                    }
                case Volume.CubicInch:
                    {
                        return CubicFeet(b, input) / 1728;
                    }
                case Volume.CubicMeter:
                    {
                        return CubicFeet(b, input) / 0.028316846592d;
                    }
                case Volume.CubicYard:
                    {
                        return CubicFeet(b, input) / 0.037037037037037d;
                    }
                case Volume.FluidOunceUK:
                    {
                        return CubicFeet(b, input) / 996.6136734468521d;
                    }
                case Volume.FluidounceUS:
                    {
                        return CubicFeet(b, input) / 957.5064935064935d;
                    }
                case Volume.GallonUK:
                    {
                        return CubicFeet(b, input) / 6.228835459042826d;
                    }
                case Volume.GallonUS:
                    {
                        return CubicFeet(b, input) / 7.480519480519481d;
                    }
                case Volume.Liter:
                    {
                        return CubicFeet(b, input) / 28.316846592d;
                    }
                case Volume.PintUK:
                    {
                        return CubicFeet(b, input) / 49.83068367234261d;
                    }
                case Volume.PintUS:
                    {
                        return CubicFeet(b, input) / 59.84415584415584d;
                    }
                case Volume.QuartUK:
                    {
                        return CubicFeet(b, input) / 24.9153418361713d;
                    }
                case Volume.QuartUS:
                    {
                        return CubicFeet(b, input) / 29.92207792207792d;
                    }
            }
            return 0;
        }

        public double WeightToWeight(Weight a, Weight b, double input)
        {
            validate(input);
            switch (a)
            {
                case Weight.Carat:
                    {
                        return KiloGram(b, input) / 5000;
                    }
                case Weight.CentiGram:
                    {
                        return KiloGram(b, input) / 100000;
                    }
                case Weight.DeciGram:
                    {
                        return KiloGram(b, input) / 10000;
                    }
                case Weight.DekaGram:
                    {
                        return KiloGram(b, input) / 100;
                    }
                case Weight.Gram:
                    {
                        return KiloGram(b, input) / 1000;
                    }
                case Weight.HectoGram:
                    {
                        return KiloGram(b, input) / 10;
                    }
                case Weight.KiloGram:
                    {
                        return KiloGram(b, input);
                    }
                case Weight.LongTon:
                    {
                        return KiloGram(b, input) / 0.0009842065276110606d;
                    }
                case Weight.MilliGram:
                    {
                        return KiloGram(b, input) / 1000000;
                    }
                case Weight.Ounce:
                    {
                        return KiloGram(b, input) / 35.27396194958041d;
                    }
                case Weight.Pound:
                    {
                        return KiloGram(b, input) / 2.204622621848776d;
                    }
                case Weight.ShortTon:
                    {
                        return KiloGram(b, input) / 0.0011023113109244d;
                    }
                case Weight.Stone:
                    {
                        return KiloGram(b, input) / 0.1574730444177697;
                    }
                case Weight.Tonne:
                    {
                        return KiloGram(b, input) / 0.001;
                    }
            }
            return 0;
        }

        public double APSum(double a, double b, double c)
        {
            result = 0;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b == 0 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                result += i;
                i = i + b;
            }
            return result;
        }

        public double APDiff(double a, double b, double c)
        {
            result = 0;
            bool temp = true;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b == 0 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                if (temp)
                {
                    result += i;
                    temp = false;
                }
                else
                {
                    result -= i;
                    temp = true;
                }
                i = i + b;
            }
            return result;
        }

        public double GPSum(double a, double b, double c)
        {
            result = 0;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b <= 1 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                result += i;
                i = i * b;
            }
            return result;
        }

        public double GPDiff(double a, double b, double c)
        {
            result = 0;
            bool temp = true;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b <= 1 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                if (temp)
                {
                    result += i;
                    temp = false;
                }
                else
                {
                    result -= i;
                    temp = true;
                }
                i = i * b;
            }
            return result;
        }

        public double HPSum(double a, double b, double c)
        {
            result = 0;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b == 0 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                result += 1d / i;
                i = i + b;
            }
            return result;
        }

        public double HPDiff(double a, double b, double c)
        {
            result = 0;
            bool temp = true;
            if (a.ToString().Contains(",") || b.ToString().Contains(",") || c.ToString().Contains(",") || a == 0 || b == 0 || c <= a)
            {
                throw new InvalidOperationException("Invalid Input");
            }
            for (double i = a; i <= c; )
            {
                if (temp)
                {
                    result += 1d / i;
                    temp = false;
                }
                else
                {
                    result -= 1d / i;
                    temp = true;
                }
                i = i + b;
            }
            return result;
        }

        public string Constants(int a)
        {
            return _constants[a];
        }

        public double Values(int a)
        {
            return _values[a];
        }

        public string Units(int a)
        {
            return _units[a];
        }

        public List<string> GetList()
        {
            return _constants;
        }

        #endregion
    }
}
