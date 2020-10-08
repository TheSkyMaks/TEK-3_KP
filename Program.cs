using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace TEK_3_KP
{
    /// <summary>
    /// Class "Program" calculate variables what you need for MatLab
    /// </summary>
    /// <param name="YourVariant">Variant of your coursework</param>
    /// <param name="Model">Model of your transistor</param>
    /// <param name="Ic_max">Maximum collector current</param>
    /// <param name="Vce_max">Maximum collector-emitter voltage</param>
    /// <param name="Vbe_max">Maximum base-emitter voltage</param>
    /// <param name="P_max">Maximum power in the collector-emitter circuit</param>
    /// <param name="E">EMF power supply</param>
    /// <param name="F">frequency</param>
    /// <param name="Rin">input resistance of the AC stage</param>
    /// <param name="Rout">the output resistance of the AC stage</param>
    /// <param name="Rc">collector resistance</param>
    /// <param name="C1">value of separating tanks</param>
    /// <param name="C2">values of separating tanks</param>
    /// <param name="mode">Mode of your calculate(1. auto 2. manually)</param>
    internal class Program
    {
        internal string YourVariant { get; set; }
        internal string Model { get; set; }
        internal int Ic_max { get; set; }
        internal int Vce_max { get; set; }
        internal int Vbe_max { get; set; }
        internal double P_max { get; set; }
        internal int E { get; set; }
        internal double F { get; set; }
        internal double Rin { get; set; }
        internal double Rout { get; set; }
        internal int Rc { get; set; }
        internal double C1 { get; set; }
        internal double C2 { get; set; }

        internal int mode;

        /// <summary>
        /// Start of program
        /// </summary>
        internal void Start()
        {
            Console.Clear();
            EnterMode();
            Calculate_Rc();
            Enter_Rin_Rout();
            Calculate_C1_C2();
            Results();
        }

        /// <summary>
        /// Choose your mode of your calculate(1. auto 2. manually)
        /// </summary>
        internal void EnterMode()
        {
            Console.WriteLine("Enter mode of imput: \n 1. auto \n 2. manually");
            mode = TakeInt32_FromConsole();
            Console.Clear();
            if (mode == 1)
            {
                TakeVariant();
                Console.Clear();
                InputVariablesMode_1();
            }
            else if (mode == 2)
            {
                TakeVariables();
                Console.Clear();
                InputVariablesMode_2();
            }
            else
            {
                EnterMessageToConsole("\nError: Enter correct number. Example: 1");
                EnterMode();
            }
        }

        /// <summary>
        /// Calculate collector resistance
        /// </summary>
        internal void Calculate_Rc()
        {
            Rc = ((int)(E / (0.8 * (Ic_max / 1000.0)))) + 1;
            Console.WriteLine($"\nRc = {Rc}");
        }

        /// <summary>
        /// Takes input and output resistance of the AC stage from console
        /// </summary>
        internal void Enter_Rin_Rout()
        {
            Console.Write("\nEnter Rin: ");
            Rin = TakeDouble_FromConsole();

            Console.Write("\nEnter Rout: ");
            Rout = TakeDouble_FromConsole();
        }

        /// <summary>
        /// Calculate values of separating tanks
        /// </summary>
        internal void Calculate_C1_C2()
        {
            var V = 2.0 * 3.14 * F;
            C1 = (int)(10000.0 / (V * Rin)) + 1;
            C2 = (int)(1000.0 / (V * Rout)) + 1;
        }
        
        /// <summary>
        /// Take variables from console if your mode == 1
        /// </summary>
        internal void TakeVariables()
        {
            Console.Write("Enter E: ");
            E = TakeInt32_FromConsole();

            Console.Write("\nEnter Icmax: ");
            Ic_max = TakeInt32_FromConsole();

            Console.Write("\nEnter f: ");
            F = TakeDouble_FromConsole();
        }

        /// <summary>
        /// Take number of your variant 
        /// </summary>
        internal void TakeVariant()
        {
            Console.Write("Enter your variant: ");
            int variantFromConsole = TakeInt32_FromConsole();
            if (variantFromConsole > 999)
            {
                EnterMessageToConsole("Error: Enter variant correct. Example: 130");
                TakeVariant();
                return;
            }
            var variant = variantFromConsole.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
            if (variant.Length < 3)
            {
                int[] arr = new int[3];
                for (int i = 0; i < variant.Length; i++)
                {
                    arr[i] = variant[^(i + 1)];
                }
                Array.Reverse(arr);
                variant = arr;
            }
            Variant(variant);
        }

        /// <summary>
        /// Calls variable assignment functions by your variant
        /// </summary>
        /// <param name="variant">array of numbers with your variant</param>
        private void Variant(int[] variant)
        {
            YourVariant = String.Concat(variant);
            GetFirstVariables(variant[0]);
            GetSecondVariables(variant[1]);
            GetThirdVariables(variant[2]);
            Console.Clear();
            InputVariablesMode_1();
        }

        /// <summary>
        /// variable assignment
        /// </summary>
        public void FirstVariables(string model, int ic_max, int vce_max, int vbe_max, double p_max)
        {
            Model = model;
            Ic_max = ic_max;
            Vce_max = vce_max;
            Vbe_max = vbe_max;
            P_max = p_max;
        }

        /// <summary>
        /// Get variables by first number of your variant
        /// </summary>
        /// <param name="number">First number of your variant</param>
        private void GetFirstVariables(int number)
        {
            switch (number)
            {
                case 0:
                    FirstVariables(
                        model: "2N2102",
                        ic_max: 1000,
                        vce_max: 65,
                        vbe_max: 7,
                        p_max: 5
                        );
                    break;
                case 1:
                    FirstVariables(
                        model: "2N2218",
                        ic_max: 800,
                        vce_max: 30,
                        vbe_max: 5,
                        p_max: 3
                        );
                    break;
                case 2:
                    FirstVariables(
                       model: "2N2221",
                       ic_max: 800,
                       vce_max: 30,
                       vbe_max: 5,
                       p_max: 1.2
                       );
                    break;
                case 3:
                    FirstVariables(
                       model: "2N2270",
                       ic_max: 1000,
                       vce_max: 45,
                       vbe_max: 7,
                       p_max: 5
                       );
                    break;
                case 4:
                    FirstVariables(
                       model: "2N3019",
                       ic_max: 1000,
                       vce_max: 80,
                       vbe_max: 7,
                       p_max: 5
                       );
                    break;
                case 5:
                    FirstVariables(
                       model: "2N3053",
                       ic_max: 700,
                       vce_max: 40,
                       vbe_max: 5,
                       p_max: 5
                       );
                    break;
                case 6:
                    FirstVariables(
                       model: "2N3114",
                       ic_max: 200,
                       vce_max: 150,
                       vbe_max: 5,
                       p_max: 5
                       );
                    break;
                case 7:
                    FirstVariables(
                       model: "2N3300",
                       ic_max: 500,
                       vce_max: 30,
                       vbe_max: 5,
                       p_max: 3
                       );
                    break;
                case 8:
                    FirstVariables(
                       model: "2N3498",
                       ic_max: 500,
                       vce_max: 100,
                       vbe_max: 6,
                       p_max: 5
                       );
                    break;
                case 9:
                    FirstVariables(
                       model: "2N3500",
                       ic_max: 300,
                       vce_max: 150,
                       vbe_max: 6,
                       p_max: 5
                       );
                    break;
                default:
                    EnterMessageToConsole($"Error in {Convert.ToString(MethodBase.GetCurrentMethod())}" +
                        "\nPress Enter to restart program");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }

        /// <summary>
        /// Get variables by second number of your variant
        /// </summary>
        /// <param name="number">Second number of your variant</param>
        private void GetSecondVariables(int number)
        {
            switch (number)
            {
                case 0:
                    E = 6;
                    break;
                case 1:
                    E = 7;
                    break;
                case 2:
                    E = 8;
                    break;
                case 3:
                    E = 9;
                    break;
                case 4:
                    E = 10;
                    break;
                case 5:
                    E = 11;
                    break;
                case 6:
                    E = 12;
                    break;
                case 7:
                    E = 13;
                    break;
                case 8:
                    E = 14;
                    break;
                case 9:
                    E = 15;
                    break;
                default:
                    EnterMessageToConsole($"Error in {Convert.ToString(MethodBase.GetCurrentMethod())}" +
                        "\nPress Enter to restart program");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }

        /// <summary>
        /// Get variables by third number of your variant
        /// </summary>
        /// <param name="number">Third number of your variant</param>
        private void GetThirdVariables(int number)
        {
            switch (number)
            {
                case 0:
                    F = 1.0;
                    break;
                case 1:
                    F = 1.5;
                    break;
                case 2:
                    F = 2.0;
                    break;
                case 3:
                    F = 2.5;
                    break;
                case 4:
                    F = 3.0;
                    break;
                case 5:
                    F = 3.5;
                    break;
                case 6:
                    F = 4.0;
                    break;
                case 7:
                    F = 4.5;
                    break;
                case 8:
                    F = 5.0;
                    break;
                case 9:
                    F = 5.5;
                    break;
                default:
                    EnterMessageToConsole($"Error in {Convert.ToString(MethodBase.GetCurrentMethod())}" +
                        "\nPress Enter to restart program");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }

        /// <summary>
        /// Output to the console variables of your variant
        /// </summary>
        internal void InputVariablesMode_1()
        {
            EnterMessageToConsole(
                "Input values: " +
                $"\nVariant = {YourVariant}" +
                $"\nModel = {Model}" +
                $"\nIc max = {Ic_max}" +
                $"\nVce max = {Vce_max}" +
                $"\nVbe max = {Vbe_max}" +
                $"\nPmax = {P_max}" +
                $"\nE = {E}" +
                $"\nF = {F}"
                );
        }

        /// <summary>
        /// Output to the console the variables you entered
        /// </summary>
        internal void InputVariablesMode_2()
        {
            EnterMessageToConsole(
                   "Input values: " +
                   $"\nIc max = {Ic_max}" +
                   $"\nE = {E}" +
                   $"\nF = {F}"
                   );

        }

        /// <summary>
        /// Output the result of program
        /// </summary>
        internal void Results()
        {
            Console.Clear();
            if (mode == 1)
            {
                InputVariablesMode_1();
            }
            else if (mode == 2)
            {
                InputVariablesMode_2();
            }

            EnterMessageToConsole(
            $"\nRin = {Rin}" +
            $"\nRin = {Rout}" +
            "\n------------------------------------\n\nResults:" +
            $"\nRc = {Rc}" +
            $"\nC1 = {C1}" +
            $"\nC2 = {C2}" +
            "\n\n------------------------------------\nPress Enter to restart program");
            Console.ReadLine();
            Start();
        }

        /// <summary>
        /// Output message to the console
        /// </summary>
        /// <param name="message">Your message</param>
        internal void EnterMessageToConsole(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(message);
            }
            Console.WriteLine(message);
        }

        /// <summary>
        /// Gets an int number from the console
        /// </summary>
        /// <returns>int32 number</returns>
        internal int TakeInt32_FromConsole()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int variable))
                {
                    return variable;
                }
                else
                {
                    EnterMessageToConsole("\nError: Enter integer number. Example: 3");
                }
            }
        }

        /// <summary>
        /// Gets an double number from the console
        /// </summary>
        /// <returns>double number</returns>
        internal double TakeDouble_FromConsole()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out double variable))
                {
                    return variable;
                }
                else
                {
                    EnterMessageToConsole("\nError: Enter fractional number. Example: 3.14");
                }
            }
        }
    }
}
