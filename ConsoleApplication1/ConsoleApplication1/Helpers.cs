using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace System
{
    #region public struct

    public struct UnitValue : IFormattable
    {
        public UnitValue(double value, string unit)
        {
            this.value = value;
            this.unit = unit;
        }

        public double value { get; }

        public string unit { get; }

        public static implicit operator UnitValue(float value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(double value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(uint value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(ulong value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(sbyte value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(byte value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(long value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(ushort value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(int value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static implicit operator UnitValue(short value)
        {
            return new UnitValue(value, string.Empty);
        }

        public static explicit operator UnitValue(decimal value)
        {
            return new UnitValue(Convert.ToDouble(value), string.Empty);
        }

        public static explicit operator UnitValue(BigInteger value)
        {
            return new UnitValue(Convert.ToDouble(value), string.Empty);
        }

        public override string ToString()
        {
            return "{ (" + value + ", " + unit + ") }";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "{ (" + value + ", " + unit + ") }";
        }
    }

    #endregion

    #region public enums

    public enum Mode
    {
        Degree, Radians, Gradient
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

    public enum Constants
    {
        [Description("Speed of light c")]
        SpeedOfLight,
        [Description("Permeability of vacuum μ0")]
        PermeabilityOfVacuum,
        [Description("Permittivity of vacuum ε0")]
        PermittivityOfVacuum,
        [Description("Gravitation constant G")]
        GravitationConstant,
        [Description("Planck constant h")]
        PlanckConstant,
        [Description("Angular Planck constant")]
        AngularPlanckConstant,
        [Description("Charge/Quantum ratio")]
        ChargeToQuantumRatio,
        [Description("Elementary charge e")]
        ElementaryCharge,
        [Description("Quantum/Charge ratio")]
        QuantumToChargeRatio,
        [Description("Fine structure constant α")]
        FineStructureConstant,
        [Description("Inverse of fine structure constant")]
        InverseOfFineStructureConstant,
        [Description("Boltzmann constant k")]
        BoltzmannConstant,
        [Description("Planck mass mp")]
        PlanckMass,
        [Description("Planck time tp")]
        PlanckTime,
        [Description("Planck length lp")]
        PlanckLength,
        [Description("Planck temperature")]
        PlanckTemperature,
        [Description("Impedance of vacuum Z0")]
        ImpedanceOfVacuum,
        [Description("Magnetic flux quantum Φ0")]
        MagneticFluxQuantum,
        [Description("Josephson constant KJ")]
        JosephsonConstant,
        [Description("von Klitzing constant RK")]
        VonKlitzingConstant,
        [Description("Conductance quantum G0")]
        ConductanceQuantum,
        [Description("Inverse of conductance quantum")]
        InverseOfConductanceQuantum,
        [Description("Stefan-Boltzmann constant σ")]
        StefanBoltzmannConstant,
        [Description("Rydberg constant R∞")]
        RydbergConstant,
        [Description("Hartree energy EH")]
        HartreeEnergy,
        [Description("Bohr radius")]
        BohrRadius,
        [Description("Bohr magneton μB")]
        BohrMagneton,
        [Description("Quantum of circulation")]
        QuantumOfCirculation,
        [Description("Richardson constant")]
        RichardsonConstant,
        [Description("Classical electron radius re")]
        ClassicalElectronRadius,
        [Description("Thomson cross section σe")]
        ThomsonCrossSection,
        [Description("Avogadro's number NA")]
        AvogadrosNumber,
        [Description("Molar Planck constant")]
        MolarPlanckConstant,
        [Description("Electron molar mass")]
        ElectronMolarMass,
        [Description("Electron molar charge")]
        ElectronMolarCharge,
        [Description("Faraday constant F")]
        FaradayConstant,
        [Description("Molar gas constant R")]
        MolarGasConstant,
        [Description("Molar volume of ideal gas Vm")]
        MolarVolumeOfIdealGas,
        [Description("Electron volt")]
        ElectronVolt,
        [Description("Electron volt to mass")]
        ElectronVoltToMass,
        [Description("Electron volt to atomic units u")]
        ElectronVoltToAtomicUnits,
        [Description("Electron volt to frequency")]
        ElectronVoltToFrequency,
        [Description("Joul to eV")]
        JoulToElectronVolt,
        [Description("Mass to eV")]
        MassToElectronVolt,
        [Description("Atomic unit u to eV")]
        AtomicUnitToElectronVolt,
        [Description("Frequency (1 Hz) to eV")]
        FrequencyToElectronVolt,
        [Description("Light-year ly")]
        LightYear
    }

    #endregion

    public static class Helpers
    {
        /// <summary>
        /// return value as T type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Convert<T>(this object value)
        {
            T output;
            try
            {
                output = (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                output = default(T);
            }
            return output;
        }

        /// <summary>
        /// return enum value with respect to string
        /// </summary>
        /// <typeparam name="T">enum Type</typeparam>
        /// <param name="value">enum value in string</param>
        /// <returns>enum value</returns>
        public static T GetEnum<T>(this string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), Regex.Replace(value, "[^a-z0-9/_]", "", RegexOptions.IgnoreCase), true);
            }
            catch (Exception)
            {
                try
                {
                    return (T)Enum.GetValues(typeof(T)).GetValue(0);
                }
                catch (Exception)
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// return enum value with respect to int or char
        /// </summary>
        /// <typeparam name="T">enum Type</typeparam>
        /// <param name="index">index in int or char</param>
        /// <returns>enum value</returns>
        public static T GetEnum<T>(this int index)
        {
            try
            {
                return (T)(object)index;
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// return enum value with respect to description
        /// </summary>
        /// <typeparam name="T">enum Type</typeparam>
        /// <param name="description">enum description in string</param>
        /// <returns>enum value</returns>
        public static T GetEnumByDesc<T>(this string description)
        {
            try
            {
                foreach (FieldInfo item in typeof(T).GetFields())
                {
                    if ((Attribute.GetCustomAttribute(item, typeof(Attribute), true) as DescriptionAttribute).Description == description)
                    {
                        return GetEnum<T>(item.Name);
                    }
                }
            }
            catch (Exception)
            {
                return (T)Enum.GetValues(typeof(T)).GetValue(0);
            }
            return default(T);
        }

        /// <summary>
        /// return enum description with respect to value
        /// </summary>
        /// <typeparam name="T">enum Type</typeparam>
        /// <param name="value">enum value in string</param>
        /// <returns>enum description</returns>
        public static string GetDescByEnum<T>(this string value)
        {
            try
            {
                foreach (MemberInfo item in typeof(T).GetFields())
                {
                    if (item.Name == value)
                    {
                        return (Attribute.GetCustomAttribute(item, typeof(Attribute), true) as DescriptionAttribute).Description;
                    }
                }
            }
            catch (Exception) { }
            return null;
        }

        /// <summary>
        /// return Not Nullble Object corresponds to respected object 
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="parameters">initializing constructor by passing parameters</param>
        /// <returns>Not Nullble Object</returns>
        public static dynamic ToDefault<T>(this T value, object[] parameters = null)
        {
            if (value == null)
            {
                if (typeof(T).IsGenericType)
                {
                    return Activator.CreateInstance(Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
                }
                else
                {
                    return (typeof(T).FullName == "System.String" ? (object)string.Empty : (object)Activator.CreateInstance(typeof(T), parameters));
                }
            }
            return value;
        }

        /// <summary>
        /// return clone object of current object
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name=""></param>
        /// <returns>Clone Object</returns>
        public static T CloneTo<T>(this T value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                stream.Position = 0;
                return (T)serializer.Deserialize(stream);
            }
        }

        public static class Math
        {
            #region private variables

            static double power = 0, result = 0;

            static List<string> _constants = new List<string>(){
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

            static List<double> _values = new List<double>() {
        2.997924580*System.Math.Pow(10,+8),
12.566370614*System.Math.Pow(10,-7    ),
8.854187817*System.Math.Pow(10,-12    ),
6.6738480*System.Math.Pow(10,-11         ),
6.6260695729*System.Math.Pow(10,-34      ),
1.05457172647*System.Math.Pow(10,-34     ),
2.41798934853*System.Math.Pow(10,+14     ),
1.60217656535*System.Math.Pow(10,-19     ),
4.1356675210*System.Math.Pow(10,-15      ),
7.297352569824*System.Math.Pow(10,-3     ),
137.03599907445                   ,
1.380648813*System.Math.Pow(10,-23       ),
2.1765113*System.Math.Pow(10,-8          ),
5.3910632*System.Math.Pow(10,-44         ),
1.61619997*System.Math.Pow(10,-35        ),
1.41683385*System.Math.Pow(10,+32        ),
376.730313461                  ,
2.06783375846*System.Math.Pow(10,-15     ),
4.8359787011*System.Math.Pow(10,14       ),
2.5812807443484*System.Math.Pow(10,+4    ),
7.748091734625*System.Math.Pow(10,-5     ),
1.2906403721742*System.Math.Pow(10,+4    ),
5.67037321*System.Math.Pow(10,-8         ),
1.097373156853955*System.Math.Pow(10,+7  ),
4.3597443419*System.Math.Pow(10,-18      ),
5.291772109217*System.Math.Pow(10,-11    ),
9.2740096820*System.Math.Pow(10,-24      ),
1.39962455531*System.Math.Pow(10,+10     ),
3.636947552024*System.Math.Pow(10,-4     ),
1.20173*System.Math.Pow(10,+6            ),
2.817940326727*System.Math.Pow(10,-15    ),
0.665245873413*System.Math.Pow(10,-28    ),
1.380648813*System.Math.Pow(10,-23       ),
8.617332478*System.Math.Pow(10,-5        ),
6.0221412927*System.Math.Pow(10,+23      ),
3.990312717628*System.Math.Pow(10,-10    ),
0.11962656577984                  ,
5.485799094622*System.Math.Pow(10,-7     ),
-9.6485336521*System.Math.Pow(10,+4      ),
+9.6485336521*System.Math.Pow(10,+4      ),
8.314462175                       ,
22.41396820*System.Math.Pow(10,-3        ),
1.60217656535*System.Math.Pow(10,-19     ),
1.78266184539*System.Math.Pow(10,-36     ),
1.07354415024*System.Math.Pow(10,-9      ),
2.41798934853*System.Math.Pow(10,+14     ),
6.2415093414*System.Math.Pow(10,+18      ),
5.6095888512*System.Math.Pow(10,+35      ),
931.49406121*System.Math.Pow(10,+6       ),
4.13566751691*System.Math.Pow(10,-15     ),
9.4607304725808*System.Math.Pow(10,+15   )
        };

            static List<string> _units = new List<string>() {
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

            #region private methods

            private static List<object> FindedList(List<object> From, List<object> To, int start, int end, string Method, List<object> _numbersTemp)
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

            private static List<object> MathStack(List<object> input)
            {
                double _temp1;
                while (input.Contains("/"))
                {
                    _temp1 = System.Convert.ToDouble(input[input.IndexOf("/") - 1]) / System.Convert.ToDouble(input[input.IndexOf("/") + 1]);
                    input.RemoveAt(input.IndexOf("/") - 1);
                    input.RemoveAt(input.IndexOf("/") + 1);
                    input.Insert(input.IndexOf("/"), _temp1);
                    input.RemoveAt(input.IndexOf("/"));
                }
                while (input.Contains("*"))
                {
                    _temp1 = System.Convert.ToDouble(input[input.IndexOf("*") - 1]) * System.Convert.ToDouble(input[input.IndexOf("*") + 1]);
                    input.RemoveAt(input.IndexOf("*") - 1);
                    input.RemoveAt(input.IndexOf("*") + 1);
                    input.Insert(input.IndexOf("*"), _temp1);
                    input.RemoveAt(input.IndexOf("*"));
                }
                while (input.Contains("-"))
                {
                    _temp1 = System.Convert.ToDouble(input[input.IndexOf("-") - 1]) - System.Convert.ToDouble(input[input.IndexOf("-") + 1]);
                    input.RemoveAt(input.IndexOf("-") - 1);
                    input.RemoveAt(input.IndexOf("-") + 1);
                    input.Insert(input.IndexOf("-"), _temp1);
                    input.RemoveAt(input.IndexOf("-"));
                }
                while (input.Contains("+"))
                {
                    _temp1 = System.Convert.ToDouble(input[input.IndexOf("+") - 1]) + System.Convert.ToDouble(input[input.IndexOf("+") + 1]);
                    input.RemoveAt(input.IndexOf("+") - 1);
                    input.RemoveAt(input.IndexOf("+") + 1);
                    input.Insert(input.IndexOf("+"), _temp1);
                    input.RemoveAt(input.IndexOf("+"));
                }
                return input;
            }

            private static bool PerfectRoot(int a, out double ans, out double power, out List<double> _getFactors)
            {
                List<double> _primeList = new List<double>();
                _getFactors = new List<double>();
                _primeList = Algebra.ListOfPrime(a);
                ans = 0;
                if (Math.power == 0)
                {
                    power = 0;
                }
                else
                {
                    power = Math.power;
                }
                _primeList.Sort();
                foreach (double item in _primeList)
                {
                    if (!System.Math.Pow(a, 1d / item).ToString().Contains("."))
                    {
                        ans = System.Math.Round(System.Math.Pow(a, 1d / item));
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

            private static void validate(double input)
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

            private static double Acres(Area b, double input)
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

            private static string Decimal(Base b, string input)
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
                            return BaseConversion.BinaryToDecimal(System.Convert.ToInt32(input));
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
                            return BaseConversion.HexaToDecimal(System.Convert.ToString(input));
                        }
                    case Base.Octal:
                        {
                            Regex reg = new Regex("[0-9]", RegexOptions.IgnoreCase);
                            MatchCollection count = reg.Matches(input);
                            if (count.Count != input.Length)
                            {
                                throw new InvalidOperationException("please input integer");
                            }
                            return BaseConversion.OctalToDecimal(System.Convert.ToInt32(input));
                        }
                }
                return "0";
            }

            private static double BritishThermalUnit(Energy b, double input)
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

            private static double Fathom(Length b, double input)
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

            private static double BTUPerMinute(Powers b, double input)
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

            private static double Atmosphere(Pressure b, double input)
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

            private static double Celsius(Temperature b, double input)
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

            private static double Day(Time b, double input)
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

            private static double Knots(Velocity b, double input)
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

            private static double CubicFeet(Volume b, double input)
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

            private static double KiloGram(Weight b, double input)
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

            public static class Algebra
            {
                /// <summary>
                /// returns BODMAS
                /// </summary>
                /// <param name="evalList">input value like [1,+,2,-,3.......]</param>
                /// <returns>string</returns>
                public static string BODMAS(List<object> evalList)
                {
                    int _count = 0, _count1 = 0, _check = 0;
                    string _result, _temp = null;
                    List<object> _numbersTemp = new List<object>();
                    if (_temp != null)
                    {
                        evalList.Add(_temp);
                        _temp = null;
                    }
                    _count = 0;
                    while (evalList.Contains("(") && evalList.Contains(")"))
                    {
                        try
                        {
                            while (evalList[evalList.IndexOf(")", _count) - 2].ToString() == "(")
                            {
                                try
                                {
                                    if (int.TryParse(evalList[evalList.IndexOf(")", _count) - 3].ToString(), out _check))
                                    {
                                        evalList.Insert(evalList.IndexOf(")", _count) - 2, "*");
                                    }
                                }
                                catch (Exception) { }
                                try
                                {
                                    if (int.TryParse(evalList[evalList.IndexOf(")", _count) + 1].ToString(), out _check))
                                    {
                                        evalList.Insert(evalList.IndexOf(")", _count) + 1, "*");
                                    }
                                }
                                catch (Exception) { }
                                evalList.RemoveAt(evalList.IndexOf(")", _count) - 2);
                                evalList.RemoveAt(evalList.IndexOf(")", _count));
                            }
                            while (evalList.IndexOf("(", _count1) < evalList.IndexOf(")", evalList.IndexOf("(", _count1)))
                            {
                                _numbersTemp.Clear();
                                int startIndex = evalList.IndexOf("(", _count1) + 1;
                                int endIndex = evalList.IndexOf(")", evalList.IndexOf("(", _count1));
                                if (!evalList.GetRange(startIndex, endIndex - startIndex).Contains("("))
                                {
                                    _numbersTemp = FindedList(evalList, _numbersTemp, startIndex, endIndex, "add", _numbersTemp);
                                }
                                if (!_numbersTemp.Contains("(") && !_numbersTemp.Contains(")") && _numbersTemp.Count > 0)
                                {
                                    evalList = FindedList(_numbersTemp, evalList, startIndex, endIndex, "remove", _numbersTemp);
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
                    if (!evalList.Contains("(") && !evalList.Contains(")"))
                    {
                        evalList = MathStack(evalList);
                    }
                    _result = null;
                    foreach (double item in evalList)
                    {
                        _result += item;
                    }
                    return _result;
                }

                /// <summary>
                /// returns solution of two equation(a1X+a2Y=a & b1X+b2Y=b)
                /// </summary>
                /// <param name="a1">coeff. of X</param>
                /// <param name="a2">coeff. of Y</param>
                /// <param name="a">constant</param>
                /// <param name="b1">coeff. of X</param>
                /// <param name="b2">coeff. of Y</param>
                /// <param name="b">constant</param>
                /// <returns>array of complex numbers(x,y)</returns>
                public static Complex[] TwoEquation(Complex a1, Complex a2, Complex a, Complex b1, Complex b2, Complex b)
                {
                    return new Complex[] { (((a * b2) - (b * a2)) / ((b2 * a1) - (a2 * b1))), (((a * b1) - (b * a1)) / ((a2 * b1) - (b2 * a1))) };
                }

                /// <summary>
                /// returns solution of three equation(a=[A1X+A2Y+A3Z=A], b=[B1X+B2Y+B3Z=B] & c=[C1X+C2Y+C3Z=C])
                /// </summary>
                /// <param name="a">array of complex[A1,A2,A3,A]</param>
                /// <param name="b">array of complex[B1,B2,B3,B]</param>
                /// <param name="c">array of complex[C1,C2,C3,C]</param>
                /// <returns>array of complex[x,y,z]</returns>
                public static Complex[] ThreeEquation(Complex[] a, Complex[] b, Complex[] c)
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

                /// <summary>
                /// returns power of value^power
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="power">complex power</param>
                /// <returns></returns>
                public static Complex Power(Complex value, Complex power)
                {
                    return Complex.Pow(value, power);
                }

                /// <summary>
                /// returns Log of value to the baseValue
                /// </summary>
                /// <param name="value">value to eval</param>
                /// <param name="baseValue">base value</param>
                /// <returns></returns>
                public static Complex Log(Complex value, double baseValue)
                {
                    return Complex.Log(value, baseValue);
                }

                /// <summary>
                /// returns exponential of value
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static Complex Exp(Complex value)
                {
                    return Complex.Exp(value);
                }

                /// <summary>
                ///  returns Inverse Log of complex value
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static Complex InverseLog(Complex value)
                {
                    return Complex.Pow(System.Convert.ToDouble(10), value);
                }

                /// <summary>
                ///  returns value is prime or not 
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static bool Prime(double value)
                {
                    int j = 0;
                    for (int i = 1; i <= value; i++)
                    {
                        if (value % i == 0)
                        {
                            j++;
                        }
                    }
                    if (j == 2) { return true; }
                    else { return false; }
                }

                /// <summary>
                ///  returns list of prime of given number
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static List<double> ListOfPrime(int value)
                {
                    int b = 0;
                    List<double> temp = new List<double>();
                    for (int i = 1; i <= value; i++)
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

                /// <summary>
                /// returns nPr value
                /// </summary>
                /// <param name="n"></param>
                /// <param name="r"></param>
                /// <returns></returns>
                public static int Permutation(int n, int r)
                {
                    if (r == -1)
                    {
                        throw new NullReferenceException();
                    }
                    int c = 1;
                    for (int i = 0; i < r; i++)
                    {
                        c = c * (n - i);
                    }
                    return c;
                }

                /// <summary>
                /// returns nCr value
                /// </summary>
                /// <param name="n"></param>
                /// <param name="r"></param>
                /// <returns></returns>
                public static int Combination(int n, int r)
                {
                    if (r == -1)
                    {
                        throw new NullReferenceException();
                    }
                    int c = 1, d = 1;
                    for (int i = 0; i < r; i++)
                    {
                        c = c * (n - i);
                        d = d * (r - i);
                    }
                    return (c / d);
                }

                /// <summary>
                /// returns n! value
                /// </summary>
                /// <param name="n"></param>
                /// <returns></returns>
                public static int Fact(int n)
                {
                    int b = 1;
                    for (int i = 1; i <= n; i++)
                    {
                        b = b * i;
                    }
                    return b;
                }

                /// <summary>
                /// returns addition of two complex numbers
                /// </summary>
                /// <param name="a"></param>
                /// <param name="b"></param>
                /// <returns></returns>
                public static Complex Add(Complex a, Complex b)
                {
                    return a + b;
                }

                /// <summary>
                /// returns subtration of two numbers
                /// </summary>
                /// <param name="a"></param>
                /// <param name="b"></param>
                /// <returns></returns>
                public static Complex Sub(Complex a, Complex b)
                {
                    return a - b;
                }

                /// <summary>
                /// returns multiplication of two numbers
                /// </summary>
                /// <param name="a"></param>
                /// <param name="b"></param>
                /// <returns></returns>
                public static Complex Mul(Complex a, Complex b)
                {
                    return a * b;
                }

                /// <summary>
                /// returns Absolute number
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static Complex Abs(Complex value)
                {
                    return Complex.Abs(value);
                }

                /// <summary>
                /// returns n%r value
                /// </summary>
                /// <param name="n"></param>
                /// <param name="r"></param>
                /// <returns></returns>
                public static double Modulus(double n, double r)
                {
                    return n % r;
                }

                /// <summary>
                /// returns lcm up to given number[5=>1,2,3,4,5]
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public static List<object> Lcm(int value)
                {
                    try
                    {
                        if (value == 0) { return null; }
                        int b = value;
                        List<object> temp = new List<object>();
                        List<object> result = new List<object>();
                        lab1:
                        int o = 0;
                        int q;
                        int j;
                        long k = 1;
                        int l = 1;
                        value = 1;
                        lab:
                        for (int i = 1; i <= b; i++)
                        {
                            if (value % i == 0 && value > i)
                            {
                                l++;
                            }
                            else if (value == i)
                            {
                                if (l == 2)
                                {
                                    temp.Add(value);
                                    k = k * value;
                                    o++;
                                }
                                else if (l > 2)
                                {
                                    for (j = 2; j <= b; j++)
                                    {
                                        double y = 1 / System.Convert.ToDouble(j);
                                        double z = System.Math.Pow(System.Convert.ToDouble(value), y);
                                        if (z % 1 == 0)
                                        {
                                            for (q = 0; q < temp.Count; q++)
                                            {
                                                if (value % System.Convert.ToInt32(temp[q]) == 0)
                                                {
                                                    k = k * (value / System.Convert.ToInt32(temp[q]));
                                                    temp.RemoveAt(q);
                                                    temp.Insert(q, value);
                                                }
                                            }
                                        }
                                    }
                                }
                                value = value + 1;
                                l = 1;
                                goto lab;
                            }
                            if (value > b)
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

                /// <summary>
                /// returns lcm of set of numbers
                /// </summary>
                /// <param name="values"></param>
                /// <returns></returns>
                public static double Lcm(List<double> values)
                {
                    values.Sort();
                    values.Reverse();
                    int temp = 1, count = 0;
                    List<double> list = ListOfPrime(System.Convert.ToInt32(values[0]));
                    while (count < list.Count)
                    {
                        if (!values.Any(content => content != 1))
                        {
                            break;
                        }
                        if (values.Any(content => !(content / list[count]).ToString().Contains(".")))
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                if (!(values[i] / list[count]).ToString().Contains("."))
                                {
                                    values[i] = values[i] / list[count];
                                }
                            }
                            temp *= System.Convert.ToInt32(list[count]);
                        }
                        else
                        {
                            count++;
                        }
                    }
                    return temp;
                }

                /// <summary>
                /// returns hcf of set of numbers
                /// </summary>
                /// <param name="values"></param>
                /// <returns></returns>
                public static double Hcf(List<double> values)
                {
                    values.Sort();
                    values.Reverse();
                    List<double> list = ListOfPrime(System.Convert.ToInt32(values[0] / 2));
                    int count = 0;
                    double temp = 1;
                    while (count < list.Count)
                    {
                        if (values.Any(content => content == 1))
                        {
                            break;
                        }
                        if (values.All(content => !(content / list[count]).ToString().Contains(".")))
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                values[i] = values[i] / list[count];
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

                /// <summary>
                /// returns sum of ap 1+2+3+....+n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="diff">d</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double APSum(double first, double diff, double last)
                {
                    result = 0;
                    if (first.ToString().Contains(",") || diff.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || diff == 0 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
                    {
                        result += i;
                        i = i + diff;
                    }
                    return result;
                }

                /// <summary>
                /// returns sum and difference of ap 1-2+3-.....n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="diff">d</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double APDiff(double first, double diff, double last)
                {
                    result = 0;
                    bool temp = true;
                    if (first.ToString().Contains(",") || diff.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || diff == 0 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
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
                        i = i + diff;
                    }
                    return result;
                }

                /// <summary>
                /// returns sum of gp 1+2+4+....+n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="ratio">r</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double GPSum(double first, double ratio, double last)
                {
                    result = 0;
                    if (first.ToString().Contains(",") || ratio.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || ratio <= 1 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
                    {
                        result += i;
                        i = i * ratio;
                    }
                    return result;
                }

                /// <summary>
                /// returns sum and difference of gp 1-2+4-.....n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="ratio">r</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double GPDiff(double first, double ratio, double last)
                {
                    result = 0;
                    bool temp = true;
                    if (first.ToString().Contains(",") || ratio.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || ratio <= 1 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
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
                        i = i * ratio;
                    }
                    return result;
                }

                /// <summary>
                /// returns sum of hp 1+2+3+....+n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="diff">d</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double HPSum(double first, double diff, double last)
                {
                    result = 0;
                    if (first.ToString().Contains(",") || diff.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || diff == 0 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
                    {
                        result += 1d / i;
                        i = i + diff;
                    }
                    return result;
                }

                /// <summary>
                /// returns sum and difference of hp 1-2+3-.....n
                /// </summary>
                /// <param name="first">a</param>
                /// <param name="diff">d</param>
                /// <param name="last">n</param>
                /// <returns></returns>
                public static double HPDiff(double first, double diff, double last)
                {
                    result = 0;
                    bool temp = true;
                    if (first.ToString().Contains(",") || diff.ToString().Contains(",") || last.ToString().Contains(",") || first == 0 || diff == 0 || last <= first)
                    {
                        throw new InvalidOperationException("Invalid Input");
                    }
                    for (double i = first; i <= last;)
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
                        i = i + diff;
                    }
                    return result;
                }

                /// <summary>
                /// returns Const of given constant
                /// </summary>
                /// <param name="constants">Constants</param>
                /// <returns></returns>
                public static UnitValue Constants(Constants constants)
                {
                    try
                    {
                        UnitValue cons = new UnitValue(_values[_constants.IndexOf(GetDescByEnum<Constants>(constants.ToString()))], _units[_constants.IndexOf(GetDescByEnum<Constants>(constants.ToString()))]);
                        return cons;
                    }
                    catch (Exception) { }
                    return new UnitValue();
                }
            }

            public static class Matrix
            {
                /// <summary>
                /// returns addition of two matrices
                /// </summary>
                /// <param name="A"></param>
                /// <param name="B"></param>
                /// <returns></returns>
                public static double[,] Add(double[,] A, double[,] B)
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

                /// <summary>
                /// returns subtration of two matrices
                /// </summary>
                /// <param name="A"></param>
                /// <param name="B"></param>
                /// <returns></returns>
                public static double[,] Sub(double[,] A, double[,] B)
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

                /// <summary>
                /// returns multiplication of two matrices
                /// </summary>
                /// <param name="A"></param>
                /// <param name="B"></param>
                /// <returns></returns>
                public static double[,] Mul(double[,] A, double[,] B)
                {
                    int row = A.GetLength(0);
                    int col = A.GetLength(1);
                    double[,] ANS = null;
                    List<object> D = new List<object>();
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
                    List<object> Z = new List<object>(2);
                    while (true)
                    {
                        if (hmm == D.Count - 1) { break; }
                        hmm = qu1 - main;
                        int ing = 0;
                        while (ing < main)
                        {
                            hmm = hmm + main;
                            ans = ans + System.Convert.ToDouble(D[hmm]);
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
                        ANS[rw1, cl1] = System.Convert.ToDouble(Z[hmm]);
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
            }

            public static class Trigonometry
            {
                /// <summary>
                /// returns sin of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Sin(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Sin(value);
                }

                /// <summary>
                /// returns cos of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Cos(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Cos(value);
                }

                /// <summary>
                /// returns tan of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Tan(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Tan(value);
                }

                /// <summary>
                /// returns sinh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Sinh(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Sinh(value);
                }

                /// <summary>
                /// returns cosh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Cosh(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Cosh(value);
                }

                /// <summary>
                /// returns tanh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex Tanh(Complex value, Mode mode)
                {
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.RadToDeg(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToGrad(Conversion.RadToDeg(value));
                                break;
                            }
                    }
                    return Complex.Tanh(value);
                }

                /// <summary>
                /// returns inverse sin of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ASin(Complex value, Mode mode)
                {
                    value = Complex.Asin(value);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }

                /// <summary>
                /// returns inverse cos of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ACos(Complex value, Mode mode)
                {
                    value = Complex.Acos(value);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }

                /// <summary>
                /// returns inverse tan of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ATan(Complex value, Mode mode)
                {
                    value = Complex.Atan(value);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }

                /// <summary>
                /// returns inverse sinh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ASinh(Complex value, Mode mode)
                {
                    value = Complex.Log(value + Complex.Sqrt((value * value) + 1), System.Math.E);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }

                /// <summary>
                /// returns inverse cosh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ACosh(Complex value, Mode mode)
                {
                    value = Complex.Log(value + Complex.Sqrt((value * value) - 1), System.Math.E);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }

                /// <summary>
                /// returns inverse tanh of value
                /// </summary>
                /// <param name="value">complex value</param>
                /// <param name="mode">Degree or Radians or Gradient</param>
                /// <returns></returns>
                public static Complex ATanh(Complex value, Mode mode)
                {
                    value = Complex.Log(Complex.Sqrt(1 + value), System.Math.E) - Complex.Log(Complex.Sqrt(1 - value), System.Math.E);
                    switch (mode)
                    {
                        case Mode.Degree:
                            {
                                value = Conversion.DegToRad(value);
                                break;
                            }
                        case Mode.Radians:
                            {
                                break;
                            }
                        case Mode.Gradient:
                            {
                                value = Conversion.DegToRad(Conversion.GradToDeg(value));
                                break;
                            }
                    }
                    return value;
                }
            }

            public static class BaseConversion
            {
                public static byte And(byte a, byte b)
                {
                    return System.Convert.ToByte(a & b);
                }

                public static byte Or(byte a, byte b)
                {
                    return System.Convert.ToByte(a | b);
                }

                public static byte Xor(byte a, byte b)
                {
                    return System.Convert.ToByte(a ^ b);
                }

                public static byte Not(byte a)
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

                /// <summary>
                /// returns binary value of decimal
                /// </summary>
                /// <param name="dec"></param>
                /// <returns></returns>
                public static string DecimalToBinary(int dec)
                {
                    List<object> list = new List<object>();
                    string c = null;
                    lab:
                    if (dec > 2)
                    {
                        list.Add(System.Convert.ToInt32(dec % 2));
                        dec = dec / 2;
                        goto lab;
                    }
                    list.Add(System.Convert.ToInt32(dec % 2));
                    list.Add(System.Convert.ToInt32(dec / 2));
                    list.Reverse();
                    foreach (var item in list) { c = c + item.ToString(); }
                    return c;
                }

                /// <summary>
                /// returns octal value of decimal
                /// </summary>
                /// <param name="dec"></param>
                /// <returns></returns>
                public static string DecimalToOctal(int dec)
                {
                    List<object> list = new List<object>();
                    string c = null;
                    lab1:
                    if (dec >= 8)
                    {
                        list.Add(System.Convert.ToInt32(dec % 8));
                        dec = dec / 8;
                        goto lab1;
                    }
                    list.Add(System.Convert.ToInt32(dec));
                    list.Reverse();
                    foreach (var item in list) { c = c + item.ToString(); }
                    return c;
                }

                /// <summary>
                /// returns hexadecimal value of decimal
                /// </summary>
                /// <param name="dec"></param>
                /// <returns></returns>
                public static string DecimalToHexa(int dec)
                {
                    List<object> list = new List<object>();
                    string c = null;
                    lab2:
                    if (dec >= 16)
                    {
                        if (dec % 16 >= 10)
                        {
                            if (dec % 16 == 10) { list.Add('A'); }
                            if (dec % 16 == 11) { list.Add('B'); }
                            if (dec % 16 == 12) { list.Add('C'); }
                            if (dec % 16 == 13) { list.Add('D'); }
                            if (dec % 16 == 14) { list.Add('E'); }
                            if (dec % 16 == 15) { list.Add('F'); }
                        }
                        else
                        {
                            list.Add(System.Convert.ToInt32(dec % 16));
                        }
                        dec = dec / 16;
                        goto lab2;
                    }
                    else if (dec >= 10)
                    {
                        if (dec == 10) { list.Add('A'); dec = 0; }
                        if (dec == 11) { list.Add('B'); dec = 0; }
                        if (dec == 12) { list.Add('C'); dec = 0; }
                        if (dec == 13) { list.Add('D'); dec = 0; }
                        if (dec == 14) { list.Add('E'); dec = 0; }
                        if (dec == 15) { list.Add('F'); dec = 0; }
                    }
                    if (dec != 0)
                    {
                        list.Add(System.Convert.ToInt32(dec));
                    }
                    list.Reverse();
                    foreach (var item in list) { c = c + item.ToString(); }
                    return c;
                }

                /// <summary>
                /// returns decimal value of binary
                /// </summary>
                /// <param name="bin"></param>
                /// <returns></returns>
                public static string BinaryToDecimal(int bin)
                {
                    List<object> list = new List<object>();
                    int pow = 0;
                    int count = bin.ToString().Length - 1;
                    char[] d = bin.ToString().ToCharArray();
                    lab3:
                    if (pow <= bin.ToString().Length - 1)
                    {
                        list.Add(System.Convert.ToInt32((d[pow]) - 48) * System.Math.Pow(System.Convert.ToDouble(2), System.Convert.ToDouble(count)));
                        pow++;
                        count--;
                        goto lab3;
                    }
                    bin = 0;
                    foreach (var item in list) { bin = bin + System.Convert.ToInt32(item); }
                    return bin.ToString();
                }

                /// <summary>
                /// returns decimal value of octal
                /// </summary>
                /// <param name="oct"></param>
                /// <returns></returns>
                public static string OctalToDecimal(int oct)
                {
                    List<object> list = new List<object>();
                    int pow = 0;
                    int count = oct.ToString().Length - 1;
                    char[] d = oct.ToString().ToCharArray();
                    lab3:
                    if (pow <= oct.ToString().Length - 1)
                    {
                        list.Add(System.Convert.ToInt32((d[pow]) - 48) * System.Math.Pow(System.Convert.ToDouble(8), System.Convert.ToDouble(count)));
                        pow++;
                        count--;
                        goto lab3;
                    }
                    oct = 0;
                    foreach (var item in list) { oct = oct + System.Convert.ToInt32(item); }
                    return oct.ToString();
                }

                /// <summary>
                /// returns decimal value of hexadecimal
                /// </summary>
                /// <param name="hex"></param>
                /// <returns></returns>
                public static string HexaToDecimal(string hex)
                {
                    List<object> list = new List<object>();
                    int pow = 0;
                    int e = hex.ToString().Length - 1;
                    char[] itemList = hex.ToCharArray();
                    lab3:
                    if (pow <= hex.ToString().Length - 1)
                    {
                        if (!char.IsDigit(itemList[pow]))
                        {
                            if (itemList[pow] == 'A' || itemList[pow] == 'a') { list.Add(System.Convert.ToInt32(10) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                            if (itemList[pow] == 'B' || itemList[pow] == 'b') { list.Add(System.Convert.ToInt32(11) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                            if (itemList[pow] == 'C' || itemList[pow] == 'c') { list.Add(System.Convert.ToInt32(12) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                            if (itemList[pow] == 'D' || itemList[pow] == 'd') { list.Add(System.Convert.ToInt32(13) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                            if (itemList[pow] == 'E' || itemList[pow] == 'e') { list.Add(System.Convert.ToInt32(14) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                            if (itemList[pow] == 'F' || itemList[pow] == 'f') { list.Add(System.Convert.ToInt32(15) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e))); }
                        }
                        if (char.IsDigit(itemList[pow]))
                        {
                            list.Add(System.Convert.ToInt32((itemList[pow]) - 48) * System.Math.Pow(System.Convert.ToDouble(16), System.Convert.ToDouble(e)));
                        }
                        pow++;
                        e--;
                        goto lab3;
                    }
                    hex = null;
                    foreach (var item in list) { hex = (System.Convert.ToInt32(hex) + System.Convert.ToInt32(item)).ToString(); }
                    return hex;
                }
            }

            public static class Conversion
            {
                public static Complex DegToRad(Complex deg)
                {
                    return (deg * 180d) / System.Math.PI;
                }

                public static Complex RadToDeg(Complex rad)
                {
                    return System.Math.PI * rad / 180d;
                }

                public static Complex DegToGrad(Complex deg)
                {
                    return 0.900000000000001d * deg;
                }

                public static Complex GradToDeg(Complex grad)
                {
                    return grad / 0.900000000000001d;
                }

                public static double MinuteToDeg(double min)
                {
                    return (min / 60d);
                }

                public static double DegToMinute(double deg)
                {
                    return deg * 60d;
                }

                public static double SecondsToDeg(double sec)
                {
                    return (sec / 3600d);
                }

                public static double DegToSeconds(double deg)
                {
                    return (deg * 3600d);
                }

                public static string Mix_Fraction(double a, double b = 1)
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
                        for (int i = System.Convert.ToInt32(System.Math.Min(a, b)); i > 0; i--)
                        {
                            if (System.Convert.ToInt32(System.Math.Min(a, b)) % i == 0)
                            {
                                if (System.Convert.ToInt32(System.Math.Max(a, b)) % i == 0)
                                {
                                    //mixed
                                    if ((a / i) > (b / i))
                                    {
                                        return (System.Math.Floor((a / i) / (b / i))).ToString() + "(" + ((a / i) % (b / i)).ToString() + "/" + (b / i).ToString() + ")";
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

                public static double AngleToAngle(Angle from, Angle to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Angle.Degree:
                            {
                                switch (to)
                                {
                                    case Angle.Degree:
                                        {
                                            return input;
                                        }
                                    case Angle.Gradian:
                                        {
                                            return Conversion.GradToDeg(input).Real;
                                        }
                                    case Angle.Radian:
                                        {
                                            return Conversion.RadToDeg(input).Real;
                                        }
                                }
                                break;
                            }
                        case Angle.Gradian:
                            {
                                switch (to)
                                {
                                    case Angle.Degree:
                                        {
                                            return Conversion.DegToGrad(input).Real;
                                        }
                                    case Angle.Gradian:
                                        {
                                            return input;
                                        }
                                    case Angle.Radian:
                                        {
                                            return Conversion.RadToDeg(Conversion.DegToGrad(input)).Real;
                                        }
                                }
                                break;
                            }
                        case Angle.Radian:
                            {
                                switch (to)
                                {
                                    case Angle.Degree:
                                        {
                                            return Conversion.DegToRad(input).Real;
                                        }
                                    case Angle.Gradian:
                                        {
                                            return Conversion.DegToRad(Conversion.GradToDeg(input)).Real;
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

                public static string BaseToBase(Base from, Base to, string input)
                {
                    switch (to)
                    {
                        case Base.Binary:
                            {
                                return BaseConversion.DecimalToBinary(System.Convert.ToInt32(Decimal(from, input)));
                            }
                        case Base.Decimal:
                            {
                                return Decimal(from, input);
                            }
                        case Base.HexaDecimal:
                            {
                                return BaseConversion.DecimalToHexa(System.Convert.ToInt32(Decimal(from, input)));
                            }
                        case Base.Octal:
                            {
                                return BaseConversion.DecimalToOctal(System.Convert.ToInt32(Decimal(from, input)));
                            }
                    }
                    return "0";
                }

                public static double AreaToArea(Area from, Area to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Area.Acres:
                            {
                                return Acres(to, input);
                            }
                        case Area.Hectares:
                            {
                                return Acres(to, input) * 2.471053814671653d;
                            }
                        case Area.SquareCentimeter:
                            {
                                return Acres(to, input) / 40468564.224d;
                            }
                        case Area.SquareFeet:
                            {
                                return Acres(to, input) / 43560d;
                            }
                        case Area.SquareInch:
                            {
                                return Acres(to, input) / 6272640d;
                            }
                        case Area.SquareKilometer:
                            {
                                return Acres(to, input) / 0.0040468564224d;
                            }
                        case Area.SquareMeter:
                            {
                                return Acres(to, input) / 4046.8564224d;
                            }
                        case Area.SquareMile:
                            {
                                return Acres(to, input) * (1 / 0.0015625d);
                            }
                        case Area.SquareMillimeter:
                            {
                                return Acres(to, input) * (1 / 4046856422.4d);
                            }
                        case Area.SquareYard:
                            {
                                return Acres(to, input) * (1 / 4840d);
                            }
                    }
                    return 0;
                }

                public static double EnergyToEnergy(Energy from, Energy to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Energy.BritishThermalUnit:
                            {
                                return BritishThermalUnit(to, input);
                            }
                        case Energy.Calorie:
                            {
                                return BritishThermalUnit(to, input) / 251.9957963122194d;
                            }
                        case Energy.ElectronVolts:
                            {
                                return BritishThermalUnit(to, input) / 6585142025517001000000d;
                            }
                        case Energy.FootPound:
                            {
                                return BritishThermalUnit(to, input) / 778.1693709678747d;
                            }
                        case Energy.Joule:
                            {
                                return BritishThermalUnit(to, input) / 1055.056d;
                            }
                        case Energy.KiloCalorie:
                            {
                                return BritishThermalUnit(to, input) / 0.2519957963122194d;
                            }
                        case Energy.KiloJoule:
                            {
                                return BritishThermalUnit(to, input) / 1.055056d;
                            }
                    }
                    return 0;
                }

                public static double LengthToLength(Length from, Length to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Length.Angstorm:
                            {
                                return Fathom(to, input) / 18288000000;
                            }
                        case Length.Centimeter:
                            {
                                return Fathom(to, input) / 182.88d;
                            }
                        case Length.Chain:
                            {
                                return Fathom(to, input) / 0.0909090909090909d;
                            }
                        case Length.Fathom:
                            {
                                return Fathom(to, input);
                            }
                        case Length.Feet:
                            {
                                return Fathom(to, input) / 6;
                            }
                        case Length.Hand:
                            {
                                return Fathom(to, input) / 18;
                            }
                        case Length.Inch:
                            {
                                return Fathom(to, input) / 72;
                            }
                        case Length.Kilometer:
                            {
                                return Fathom(to, input) / 0.0018288d;
                            }
                        case Length.Link:
                            {
                                return Fathom(to, input) / 9.090909090909091d;
                            }
                        case Length.Meter:
                            {
                                return Fathom(to, input) / 1.8288d;
                            }
                        case Length.Microns:
                            {
                                return Fathom(to, input) / 1828800;
                            }
                        case Length.Mile:
                            {
                                return Fathom(to, input) / 0.0011363636363636d;
                            }
                        case Length.Millimeter:
                            {
                                return Fathom(to, input) / 1828.8d;
                            }
                        case Length.Nanometer:
                            {
                                return Fathom(to, input) / 1828800000;
                            }
                        case Length.NauticalMile:
                            {
                                return Fathom(to, input) / 0.0009874730021598272d;
                            }
                        case Length.PICA:
                            {
                                return Fathom(to, input) / 433.6200043362d;
                            }
                        case Length.Rods:
                            {
                                return Fathom(to, input) / 0.3636363636363636d;
                            }
                        case Length.Span:
                            {
                                return Fathom(to, input) / 8;
                            }
                        case Length.Yard:
                            {
                                return Fathom(to, input) / 2;
                            }
                    }
                    return 0;
                }

                public static double PowerToPower(Powers from, Powers to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Powers.BTUminute:
                            {
                                return BTUPerMinute(to, input);
                            }
                        case Powers.FootPound:
                            {
                                return BTUPerMinute(to, input) / 778.1693709678747d;
                            }
                        case Powers.Horsepower:
                            {
                                return BTUPerMinute(to, input) / 0.0235808900293295d;
                            }
                        case Powers.Kilowatt:
                            {
                                return BTUPerMinute(to, input) / 0.0175842666666667d;
                            }
                        case Powers.Watt:
                            {
                                return BTUPerMinute(to, input) / 17.58426666666667d;
                            }
                    }
                    return 0;
                }

                public static double PressureToPressure(Pressure from, Pressure to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Pressure.Atmosphere:
                            {
                                return Atmosphere(to, input);
                            }
                        case Pressure.Bar:
                            {
                                return Atmosphere(to, input) / 1.01325d;
                            }
                        case Pressure.KiloPascal:
                            {
                                return Atmosphere(to, input) / 101.325d;
                            }
                        case Pressure.MillimeterOfMercury:
                            {
                                return Atmosphere(to, input) / 760.1275318829707d;
                            }
                        case Pressure.Pascal:
                            {
                                return Atmosphere(to, input) / 101325;
                            }
                        case Pressure.PoundPerSquareInch:
                            {
                                return Atmosphere(to, input) / 14.69594940039221d;
                            }
                    }
                    return 0;
                }

                public static double TemperatureToTemperature(Temperature from, Temperature to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Temperature.DegreeCelsius:
                            {
                                return Celsius(to, input);
                            }
                        case Temperature.DegreeFahrenheit:
                            {
                                return (Celsius(to, input) - 32d) / 1.8d;
                            }
                        case Temperature.Kelvin:
                            {
                                return Celsius(to, input) - 273.15d;
                            }
                    }
                    return 0;
                }

                public static double TimeToTime(Time from, Time to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Time.Day:
                            {
                                return Day(to, input);
                            }
                        case Time.Hour:
                            {
                                return Day(to, input) / 24d;
                            }
                        case Time.MicroSecond:
                            {
                                return Day(to, input) / 86400000000;
                            }
                        case Time.MilliSecond:
                            {
                                return Day(to, input) / 86400000;
                            }
                        case Time.Minute:
                            {
                                return Day(to, input) / 1440;
                            }
                        case Time.Second:
                            {
                                return Day(to, input) / 86400;
                            }
                        case Time.Week:
                            {
                                return Day(to, input) / 0.1428571428571429d;
                            }
                    }
                    return 0;
                }

                public static double VelocityToVelocity(Velocity from, Velocity to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Velocity.CentimeterPerSecond:
                            {
                                return Knots(to, input) / 51.44444444444444d;
                            }
                        case Velocity.FeetPerSecond:
                            {
                                return Knots(to, input) / 1.687809857101196d;
                            }
                        case Velocity.KilometerPerHour:
                            {
                                return Knots(to, input) / 1.852d;
                            }
                        case Velocity.Knots:
                            {
                                return Knots(to, input);
                            }
                        case Velocity.Mach:
                            {
                                return Knots(to, input) / 0.0015117677734015d;
                            }
                        case Velocity.MeterPerSecond:
                            {
                                return Knots(to, input) / 0.5144444444444444d;
                            }
                        case Velocity.MilesPerHour:
                            {
                                return Knots(to, input) / 1.150779448023543d;
                            }
                    }
                    return 0;
                }

                public static double VolumeToVolume(Volume from, Volume to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Volume.CubicCentimeter:
                            {
                                return CubicFeet(to, input) / 28316.846592d;
                            }
                        case Volume.CubicFeet:
                            {
                                return CubicFeet(to, input);
                            }
                        case Volume.CubicInch:
                            {
                                return CubicFeet(to, input) / 1728;
                            }
                        case Volume.CubicMeter:
                            {
                                return CubicFeet(to, input) / 0.028316846592d;
                            }
                        case Volume.CubicYard:
                            {
                                return CubicFeet(to, input) / 0.037037037037037d;
                            }
                        case Volume.FluidOunceUK:
                            {
                                return CubicFeet(to, input) / 996.6136734468521d;
                            }
                        case Volume.FluidounceUS:
                            {
                                return CubicFeet(to, input) / 957.5064935064935d;
                            }
                        case Volume.GallonUK:
                            {
                                return CubicFeet(to, input) / 6.228835459042826d;
                            }
                        case Volume.GallonUS:
                            {
                                return CubicFeet(to, input) / 7.480519480519481d;
                            }
                        case Volume.Liter:
                            {
                                return CubicFeet(to, input) / 28.316846592d;
                            }
                        case Volume.PintUK:
                            {
                                return CubicFeet(to, input) / 49.83068367234261d;
                            }
                        case Volume.PintUS:
                            {
                                return CubicFeet(to, input) / 59.84415584415584d;
                            }
                        case Volume.QuartUK:
                            {
                                return CubicFeet(to, input) / 24.9153418361713d;
                            }
                        case Volume.QuartUS:
                            {
                                return CubicFeet(to, input) / 29.92207792207792d;
                            }
                    }
                    return 0;
                }

                public static double WeightToWeight(Weight from, Weight to, double input)
                {
                    validate(input);
                    switch (from)
                    {
                        case Weight.Carat:
                            {
                                return KiloGram(to, input) / 5000;
                            }
                        case Weight.CentiGram:
                            {
                                return KiloGram(to, input) / 100000;
                            }
                        case Weight.DeciGram:
                            {
                                return KiloGram(to, input) / 10000;
                            }
                        case Weight.DekaGram:
                            {
                                return KiloGram(to, input) / 100;
                            }
                        case Weight.Gram:
                            {
                                return KiloGram(to, input) / 1000;
                            }
                        case Weight.HectoGram:
                            {
                                return KiloGram(to, input) / 10;
                            }
                        case Weight.KiloGram:
                            {
                                return KiloGram(to, input);
                            }
                        case Weight.LongTon:
                            {
                                return KiloGram(to, input) / 0.0009842065276110606d;
                            }
                        case Weight.MilliGram:
                            {
                                return KiloGram(to, input) / 1000000;
                            }
                        case Weight.Ounce:
                            {
                                return KiloGram(to, input) / 35.27396194958041d;
                            }
                        case Weight.Pound:
                            {
                                return KiloGram(to, input) / 2.204622621848776d;
                            }
                        case Weight.ShortTon:
                            {
                                return KiloGram(to, input) / 0.0011023113109244d;
                            }
                        case Weight.Stone:
                            {
                                return KiloGram(to, input) / 0.1574730444177697;
                            }
                        case Weight.Tonne:
                            {
                                return KiloGram(to, input) / 0.001;
                            }
                    }
                    return 0;
                }

                public static double[] PolarToRectangular(double r, double Theta, Mode mode)
                {
                    double a = r * Trigonometry.Cos(Theta, mode).Real;
                    double b = r * Trigonometry.Sin(Theta, mode).Real;
                    return new double[] { a, b };
                }

                public static double[] RectangularToPolar(double a, double b, Mode mode)
                {
                    double r = System.Math.Sqrt(System.Convert.ToDouble(a * a) + System.Convert.ToDouble(b * b));
                    double Theta = Trigonometry.ATan(b / a, mode).Real;
                    return new double[] { r, Theta };
                }
            }

            public static class Statistics
            {
                public static string Mean(List<double> values)
                {
                    double resultOut = 0;
                    resultOut = values.Sum();
                    resultOut = resultOut / values.Count;
                    return resultOut.ToString();
                }

                public static string Variance(List<double> values)
                {
                    List<double> list1 = new List<double>();
                    list1.AddRange(values);
                    string meanValue = Mean(list1);
                    for (int i = 0; i < values.Count; i++)
                    {
                        list1[i] = System.Math.Pow(list1[i] - System.Convert.ToDouble(meanValue), 2);
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

                public static string StandardDeviation(string variance)
                {
                    double outValue = 0;
                    if (double.TryParse(variance, out outValue))
                    {
                        return System.Math.Pow(System.Convert.ToDouble(variance), 1d / 2d).ToString();
                    }
                    return "0";
                }

                public static string MeanSquare(List<double> values)
                {
                    List<double> list1 = new List<double>();
                    list1.AddRange(values);
                    double resultOut = 0;
                    resultOut = list1.Sum(a => a = System.Math.Pow(a, 2));
                    resultOut = resultOut / values.Count;
                    return resultOut.ToString();
                }

                public static string SquareSum(List<double> values)
                {
                    List<double> list1 = new List<double>();
                    list1.AddRange(values);
                    return list1.Sum(a => a = System.Math.Pow(a, 2)).ToString();
                }

                public static string Sum(List<double> values)
                {
                    List<double> list1 = new List<double>();
                    list1.AddRange(values);
                    return list1.Sum().ToString();
                }
            }

        }

    }
}