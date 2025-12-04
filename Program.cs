using System;

namespace LabWork
{
    public class ArithmeticProgression
    {
        public double A0 { get; set; }
        public double D { get; set; }
        private int _n;

        public int N
        {
            get => _n;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("N must be greater than 0.");
                }
                _n = value;
            }
        }

        public ArithmeticProgression(double a0, double d, int n)
        {
            A0 = a0;
            D = d;
            N = n;
        }

        public double Sum()
        {
            return N * (2 * A0 + (N - 1) * D) / 2.0;
        }

        public override string ToString()
        {
            return $"A0 = {A0}, D = {D}, N = {N}, Sum = {Sum()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of progressions: ");
            int count = int.Parse(Console.ReadLine());

            ArithmeticProgression[] progressions = new ArithmeticProgression[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nProgression #{i + 1}");

                Console.Write("Enter A0: ");
                double a0 = double.Parse(Console.ReadLine());

                Console.Write("Enter D: ");
                double d = double.Parse(Console.ReadLine());

                Console.Write("Enter N: ");
                int n = int.Parse(Console.ReadLine());

                progressions[i] = new ArithmeticProgression(a0, d, n);
            }

            // пошук максимальної суми
            ArithmeticProgression maxProg = progressions[0];

            for (int i = 1; i < count; i++)
            {
                if (progressions[i].Sum() > maxProg.Sum())
                {
                    maxProg = progressions[i];
                }
            }

            Console.WriteLine("\nProgression with the largest sum:");
            Console.WriteLine(maxProg);
        }
    }
}
