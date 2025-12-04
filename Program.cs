using System;

namespace LabWork
{
    public class ArithmeticProgression
    {
        private double _firstTerm;
        private double _difference;
        private int _numberOfTerms;

        public double FirstTerm
        {
            get => _firstTerm;
            set => _firstTerm = value;
        }

        public double Difference
        {
            get => _difference;
            set => _difference = value;
        }

        public int NumberOfTerms
        {
            get => _numberOfTerms;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Number of terms must be greater than 0");
                _numberOfTerms = value;
            }
        }

        public ArithmeticProgression(double firstTerm, double difference, int numberOfTerms)
        {
            FirstTerm = firstTerm;
            Difference = difference;
            NumberOfTerms = numberOfTerms;
        }

        public double Sum()
        {
            return _numberOfTerms * (2 * _firstTerm + (_numberOfTerms - 1) * _difference) / 2.0;
        }

        public override string ToString()
        {
            return $"a0 = {FirstTerm}, d = {Difference}, n = {NumberOfTerms}, Sum = {Sum()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of progressions: ");
            int k = int.Parse(Console.ReadLine());

            ArithmeticProgression[] progressions = new ArithmeticProgression[k];

            for (int i = 0; i < k; i++)
            {
                Console.WriteLine($"\nProgression #{i + 1}:");

                Console.Write("Enter the first term (a0): ");
                double a0 = double.Parse(Console.ReadLine());

                Console.Write("Enter the difference (d): ");
                double d = double.Parse(Console.ReadLine());

                Console.Write("Enter number of terms (n): ");
                int n = int.Parse(Console.ReadLine());

                progressions[i] = new ArithmeticProgression(a0, d, n);
            }

            ArithmeticProgression maxProg = progressions[0];

            foreach (var prog in progressions)
            {
                if (prog.Sum() > maxProg.Sum())
                    maxProg = prog;
            }

            Console.WriteLine("\nProgression with the largest sum:");
            Console.WriteLine(maxProg);
        }
    }
}
